using DAL.Models.Base;
using Microsoft.VisualBasic;

namespace DAL.Models
{
    public class Product : IBaseEntity
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Brand { get; set; }

        public int CategoryId { get; set; }
        virtual public Category Category { get; set; }
        virtual public ICollection<ProductVariant> ProductVariants { get; set; }

        public string Sku { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string? Cpu { get; set; }
        public string? Ram { get; set; }
        public string? Storage { get; set; }
        public string? ScreenSize { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static Product Create(string productName, string brand, int categoryId, string sku, string color, string? cpu, string? ram, string? storage, string? screenSize, decimal price, int stockQuantity, string? imageUrl) {
            return new Product {
                ProductName = productName,
                Brand = brand,
                CategoryId = categoryId,
                Sku = sku,
                Color = color,
                Cpu = cpu,
                Ram = ram,
                Storage = storage,
                ScreenSize = screenSize,
                Price = price,
                StockQuantity = stockQuantity,
                ImageUrl = imageUrl,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Delete() {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update(string productName, string brand, int categoryId, string sku, string color, string? cpu, string? ram, string? storage, string? screenSize, decimal price, int stockQuantity, string? imageUrl) {
            ProductName = productName;
            Brand = brand;
            CategoryId = categoryId;
            Sku = sku;
            Color = color;
            Cpu = cpu;
            Ram = ram;
            Storage = storage;
            ScreenSize = screenSize;
            Price = price;
            StockQuantity = stockQuantity;
            ImageUrl = imageUrl;

            UpdatedAt = DateTime.UtcNow;
        }
    }
}
