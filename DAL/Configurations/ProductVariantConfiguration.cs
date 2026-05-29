using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.HasKey(pv => pv.ProductVariantId);
            builder.Property(pv => pv.Sku).IsRequired().HasMaxLength(50);
            builder.Property(pv => pv.Color).IsRequired().HasMaxLength(50);
            builder.Property(pv => pv.Cpu).HasMaxLength(100);
            builder.Property(pv => pv.Ram).HasMaxLength(50);
            builder.Property(pv => pv.Storage).HasMaxLength(50);
            builder.Property(pv => pv.ScreenSize).HasMaxLength(50);
            builder.Property(pv => pv.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(pv => pv.StockQuantity).IsRequired();
            builder.Property(pv => pv.ImageUrl).HasMaxLength(200);

            // Check xóa mềm
            builder.HasQueryFilter(pv => !pv.IsDeleted);

            // Thêm index để tăng hiệu suất tìm kiếm theo SKU
            builder.HasIndex(pv => pv.Sku).IsUnique().HasDatabaseName("IX_ProductVariant_Sku");
            builder.HasIndex(pv => pv.Price).HasDatabaseName("IX_ProductVariant_Price");

            // db seeding (tùy chọn)
                builder.HasData(
                    new ProductVariant
                    {
                        ProductVariantId = 1,
                        Sku = "LAPTOP-001",
                        Color = "Black",
                        Cpu = "Intel Core i7",
                        Ram = "16GB",
                        Storage = "512GB SSD",
                        ScreenSize = "15.6 inch",
                        Price = 999.99m,
                        StockQuantity = 50,
                        ImageUrl = "https://example.com/images/laptop-001.jpg",
                        ProductId = 1,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow
                    },
                    new ProductVariant
                    {
                        ProductVariantId = 2,
                        Sku = "LAPTOP-002",
                        Color = "Silver",
                        Cpu = "AMD Ryzen 7",
                        Ram = "16GB",
                        Storage = "1TB SSD",
                        ScreenSize = "14 inch",
                        Price = 899.99m,
                        StockQuantity = 30,
                        ImageUrl = "https://example.com/images/laptop-002.jpg",
                        ProductId = 1,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow
                    }
                );
        }
    }
}
