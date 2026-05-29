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
    }
}
