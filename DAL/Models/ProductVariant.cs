using DAL.Models.Base;
using Microsoft.VisualBasic;

namespace DAL.Models
{
    public class ProductVariant : IBaseEntity
    {
        public int ProductVariantId { get; set; }
        public string Sku { get; set; }
        public string Color { get; set; }
        public string? Cpu { get; set; }
        public string? Ram { get; set; }
        public string? Storage { get; set; }
        public string? ScreenSize { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductId { get; set; }
        virtual public Product? Product { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static ProductVariant Create(
            string sku, 
            string color, 
            string? cpu, 
            string? ram, 
            string? storage, 
            string? screenSize, 
            decimal price, 
            int stockQuantity, 
            string? imageUrl, 
            int productId)
        {
            var productVariant = new ProductVariant
            {
                Sku = sku,
                Color = color,
                Cpu = cpu,
                Ram = ram,
                Storage = storage,
                ScreenSize = screenSize,
                Price = price,
                StockQuantity = stockQuantity,
                ImageUrl = imageUrl,
                ProductId = productId,
                CreatedAt = DateTime.UtcNow
            };
            return productVariant;
        }
        public void Update(
            string sku, 
            string color, 
            string? cpu, 
            string? ram, 
            string? storage, 
            string? screenSize, 
            decimal price, 
            int stockQuantity, 
            string? imageUrl, 
            int productId)
        {
            Sku = sku;
            Color = color;
            Cpu = cpu;
            Ram = ram;
            Storage = storage;
            ScreenSize = screenSize;
            Price = price;
            StockQuantity = stockQuantity;
            ImageUrl = imageUrl;
            ProductId = productId;

            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
