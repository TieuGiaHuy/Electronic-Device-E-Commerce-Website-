namespace BLL.DTOs.Promotion
{
    public class PromotionDto
    {
        public int PromotionId { get; set; }
        public Guid ProductVariantId { get; set; }
        public decimal SalePrice { get; set; }
        public int MaxReservedStock { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // New: persisted enable/disable flag
        public bool IsDisabled { get; set; }
    }
}