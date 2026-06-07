using DAL.Models.Base;

namespace DAL.Models
{
    public class Promotion : IBaseEntity
    {
        public int PromotionId { get; set; }
        public Guid ProductVariantId { get; set; }
        public decimal SalePrice { get; set; }
        public int MaxReservedStock { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public virtual ProductVariant? ProductVariant { get; set; }

        public static Promotion Create(Guid productVariantId, decimal salePrice, int maxReservedStock, DateTime startAt, DateTime endAt)
        {
            return new Promotion
            {
                ProductVariantId = productVariantId,
                SalePrice = salePrice,
                MaxReservedStock = maxReservedStock,
                StartAt = startAt,
                EndAt = endAt,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(decimal salePrice, int maxReservedStock, DateTime startAt, DateTime endAt)
        {
            SalePrice = salePrice;
            MaxReservedStock = maxReservedStock;
            StartAt = startAt;
            EndAt = endAt;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}