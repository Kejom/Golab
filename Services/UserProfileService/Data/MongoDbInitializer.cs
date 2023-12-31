using Models.Db;
using MongoDB.Driver;
using MongoDB.Entities;

namespace UserProfileService.Data
{
    public class MongoDbInitializer
    {
        public static async Task InitDb(WebApplication app)
        {
            await DB.InitAsync("GolabDb", MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));
        }
    }
}
