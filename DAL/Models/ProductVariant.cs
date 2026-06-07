using DAL.Models.Base;
using DAL.Utilities;

namespace DAL.Models
{
    public class ProductVariant : IBaseEntity
    {
        public Guid ProductVariantId { get; set; }
        public string ProductCode { get; set; }
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
            string productCode, 
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
            if (price < 0)
                throw new ArgumentException("Giá tiền không được âm.", nameof(price));
            if (stockQuantity < 0)
                throw new ArgumentException("Số lượng tồn kho không được âm.", nameof(stockQuantity));

            string sku = SkuGenerator.Generate(productCode, color, cpu, ram, storage);

            var productVariant = new ProductVariant
            {
                ProductCode = productCode,
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
            string productCode, 
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
            if (price < 0)
                throw new ArgumentException("Giá tiền không được âm.", nameof(price));
            if (stockQuantity < 0)
                throw new ArgumentException("Số lượng tồn kho không được âm.", nameof(stockQuantity));

            string sku = SkuGenerator.Generate(productCode, color, cpu, ram, storage);

            Sku = sku;
            ProductCode = productCode;
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

        public void StockIn(int increaseAmount)
        {
            StockQuantity += increaseAmount;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
