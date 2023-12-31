using MassTransit;
using Models.Contracts;
using Models.Db;
using MongoDB.Entities;

namespace CooService.Consumers
{
    public class CooLikeRemovedConsumer : IConsumer<CooLikeRemoved>
    {
        public async Task Consume(ConsumeContext<CooLikeRemoved> context)
        {
            var coo = await DB.Find<Coo>().OneAsync(context.Message.CooId);
            if (coo is null || coo.Likes == 0) return;
            coo.Likes--;
            await coo.SaveAsync();
        }
    }
}
