using AzureTangyFunc.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureTangyFunc.Data
{
    public class AzureTangyDbContext : DbContext
    {
        public DbSet<SalesRequest> SalesRequests { get; set; }
        public DbSet<GroceryItem> GroceryItems { get; set; }

        public AzureTangyDbContext(DbContextOptions<AzureTangyDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SalesRequest>(entity =>
            {
                entity.HasKey(c => c.Id);
            });

            modelBuilder.Entity<GroceryItem>(entity =>
            {
                entity.HasKey(c => c.Id);
            });
        }
    }
}
