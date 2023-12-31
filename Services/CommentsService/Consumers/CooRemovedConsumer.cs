using MassTransit;
using Models.Contracts;
using Models.Db;
using MongoDB.Entities;

namespace CommentsService.Consumers
{
    public class CooRemovedConsumer : IConsumer<CooRemoved>
    {
        public async Task Consume(ConsumeContext<CooRemoved> context)
        {
            var cooId = context.Message.CooId;
            await DB.DeleteAsync<Comment>(c => c.CooId == cooId);
        }
    }
}
