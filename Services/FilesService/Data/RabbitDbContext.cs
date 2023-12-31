using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace FilesService.Data
{
    public class RabbitDbContext : DbContext
    {
        public RabbitDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }
    }
}
