using LikeService.Consumers;
using LikeService.Data;
using LikeService.Managers;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<RabbitDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDbConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit(x =>
{
    //x.AddEntityFrameworkOutbox<RabbitDbContext>(o =>
    //{
    //    o.QueryDelay = TimeSpan.FromSeconds(10);
    //    o.UsePostgres();
    //    o.UseBusOutbox();
    //});

    x.AddConsumersFromNamespaceContaining<CooRemovedConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("like", false));

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:UserName", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<ILikeManager, LikeManager>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServiceUrl"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.NameClaimType = "username";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(async () =>
{
    try
    {
        await MongoDbInitializer.InitDb(app);
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<RabbitDbContext>();
        dbContext.Database.Migrate();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
});

app.Run();
