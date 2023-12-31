using Models.Db;
using MongoDB.Driver;
using MongoDB.Entities;

namespace CommentsService.Data
{
    public class MongoDbInitializer
    {
        public static async Task InitDb(WebApplication app)
        {
            await DB.InitAsync("GolabDb", MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Index<Comment>()
                .Key(x => x.UserId, KeyType.Text)
                .Key(x => x.CooId, KeyType.Text)
                .Key(x => x.Created, KeyType.Descending)
                .CreateAsync();
        }
    }
}
