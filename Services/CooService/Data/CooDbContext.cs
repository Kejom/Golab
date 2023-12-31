using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CooService.Data
{
    public class CooDbContext: DbContext
    {
        public CooDbContext(DbContextOptions options) : base(options)
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
