using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // 1. Cấu hình tên bảng nếu muốn khác tên DbSet có dùng naming convention nên phải comment lại tên bảng
            //builder.ToTable("Products");

            // 2. Cấu hình Khóa chính và các ràng buộc cột
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.ProductName).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Brand).IsRequired().HasMaxLength(100);

            // 3. Cấu hình quan hệ (Relationship)
            builder.HasOne(p => p.Category)
                   .WithMany()
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.ProductVariants)
                   .WithOne(pv => pv.Product)
                   .HasForeignKey(pv => pv.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 4. Cấu hình xóa mềm (Soft Delete)
            builder.HasQueryFilter(p => !p.IsDeleted);

            // 5. Cấu hình chỉ mục (Index)s
            builder.HasIndex(p => p.ProductName).HasDatabaseName("IX_Product_ProductName");

            // Db seed dữ liệu mẫu (Data Seeding)
            builder.HasData(
                new Product { ProductId = 1, ProductName = "Laptop Dell XPS 13", Brand = "Dell", CategoryId = 1, IsDeleted = false, CreatedAt = DateTime.UtcNow },
                new Product { ProductId = 2, ProductName = "Smartphone Samsung Galaxy S21", Brand = "Samsung", CategoryId = 2, IsDeleted = false, CreatedAt = DateTime.UtcNow }
            );
        }
    }
}
