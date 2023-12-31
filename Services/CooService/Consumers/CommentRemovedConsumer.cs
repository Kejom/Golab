using MassTransit;
using Models.Contracts;
using Models.Db;
using MongoDB.Entities;

namespace CooService.Consumers
{
    public class CommentRemovedConsumer : IConsumer<CommentRemoved>
    {
        public async Task Consume(ConsumeContext<CommentRemoved> context)
        {
            var coo = await DB.Find<Coo>().OneAsync(context.Message.CooId);
            if (coo is null || coo.Comments == 0) return;
            coo.Comments--;
            await coo.SaveAsync();
        }
    }
}
