using Models.Db;
using MongoDB.Driver;
using MongoDB.Entities;

namespace CooService.Data
{
    public class MongoDbInitializer
    {
        public static async Task InitDb(WebApplication app)
        {
            await DB.InitAsync("GolabDb", MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Index<Coo>()
                .Key(x => x.UserId, KeyType.Text)
                .Key(x => x.Created, KeyType.Descending)
                .CreateAsync();
        }
    }
}
