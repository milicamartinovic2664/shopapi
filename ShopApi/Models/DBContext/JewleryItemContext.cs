using Microsoft.EntityFrameworkCore;

namespace ShopApi.Models.DBContext
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JewleryItem>()
                .HasOne(p => p.Manufacturer);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<JewleryItem> JewleryItems { get; set; } = null!;
        public DbSet<Manufacturer> Manufacturers { get; set; } = null!;
    }
}
