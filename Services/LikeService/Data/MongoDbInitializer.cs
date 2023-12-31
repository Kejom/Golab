using Models.Db;
using MongoDB.Driver;
using MongoDB.Entities;

namespace LikeService.Data
{
    public class MongoDbInitializer
    {
        public static async Task InitDb(WebApplication app)
        {
            await DB.InitAsync("GolabDb", MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Index<Like>()
                .Key(x => x.UserId, KeyType.Text)
                .Key(x => x.CooId, KeyType.Text)
                .CreateAsync();
        }
    }
}
