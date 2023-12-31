using Models.Db;
using MongoDB.Driver;
using MongoDB.Entities;

namespace FilesService.Data
{
    public class MongoDbInitializer
    {
        public static async Task InitDb(WebApplication app)
        {
            await DB.InitAsync("GolabDb", MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Index<AvatarImage>()
                .Key(x => x.UserId, KeyType.Text)
                .CreateAsync();
        }
    }
}
