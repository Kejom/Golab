using MassTransit;
using Models.Contracts;
using Models.Db;
using MongoDB.Entities;

namespace CooService.Consumers
{
    public class CommentAddedConsumer : IConsumer<CommentAdded>
    {
        public async Task Consume(ConsumeContext<CommentAdded> context)
        {
            var coo = await DB.Find<Coo>().OneAsync(context.Message.CooId);
            if (coo is null) return;
            coo.Comments++;
            await coo.SaveAsync();
        }
    }
}
