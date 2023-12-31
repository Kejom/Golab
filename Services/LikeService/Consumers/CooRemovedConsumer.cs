using MassTransit;
using Models.Contracts;
using Models.Db;
using MongoDB.Entities;

namespace LikeService.Consumers
{
    public class CooRemovedConsumer : IConsumer<CooRemoved>
    {
        public async Task Consume(ConsumeContext<CooRemoved> context)
        {
            var cooId = context.Message.CooId;
            await DB.DeleteAsync<Like>(l => l.CooId == cooId);
        }
    }
}
