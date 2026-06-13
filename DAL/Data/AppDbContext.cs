using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.ProductVariant> ProductVariants { get; set; }
        public DbSet<Models.Order> Orders { get; set; }
        public DbSet<Models.OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
