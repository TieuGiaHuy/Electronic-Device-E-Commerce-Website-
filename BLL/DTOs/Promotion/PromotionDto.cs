namespace BLL.DTOs.Promotion
{
    public class PromotionDto
    {
        public int PromotionId { get; set; }
        public int ProductVariantId { get; set; }
        public decimal SalePrice { get; set; }
        public int MaxReservedStock { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}