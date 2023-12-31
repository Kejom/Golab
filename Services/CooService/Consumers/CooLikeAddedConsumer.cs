using MassTransit;
using Models.Contracts;
using Models.Db;
using MongoDB.Entities;

namespace CooService.Consumers
{
    public class CooLikeAddedConsumer : IConsumer<CooLikeAdded>
    {
        public async Task Consume(ConsumeContext<CooLikeAdded> context)
        {
            var id = context.Message.CooId;

            var coo = await DB.Find<Coo>().OneAsync(id);
            if (coo is null) return;

            coo.Likes++;
            await coo.SaveAsync();
        }
    }
}
