namespace BLL.DTOs.Promotion
{
    public class PromotionCreateDto
    {
        public Guid ProductVariantId { get; set; }
        public decimal SalePrice { get; set; }
        public int MaxReservedStock { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
    }
}