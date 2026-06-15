using DAL.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Promotion : IBaseEntity
    {
        [Key]
        public Guid PromotionId { get; set; }
        public Guid ProductVariantId { get; set; }
        [Range(0, double.MaxValue)]
        public decimal SalePrice { get; set; }
        [Range(0, int.MaxValue)]
        public int MaxReservedStock { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDisabled { get; set; }
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
                CreatedAt = DateTime.UtcNow,
                IsDisabled = false
            };
        }

        public void Update(Guid productVariantId, decimal salePrice, int maxReservedStock, DateTime startAt, DateTime endAt)
        {
            ProductVariantId = productVariantId;
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