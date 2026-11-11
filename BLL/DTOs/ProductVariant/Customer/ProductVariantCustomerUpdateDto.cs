using BLL.DTOs.ProductVariant;
namespace BLL.DTOs.ProductVariant.Customer
{
    public class ProductVariantCustomerUpdateDto
    {
        public Guid ProductVariantId { get; set; }
        public string Color { get; set; } = string.Empty;
        /// <summary>Các thông số kỹ thuật (Tên - Giá trị), theo thứ tự hiển thị.</summary>
        public List<VariantAttributeDto> Attributes { get; set; } = new();

        public string? GetAttribute(string name) =>
            Attributes.FirstOrDefault(a => string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase))?.Value;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductId { get; set; }
        public Guid? PromotionId { get; set; }
        public DAL.Models.Promotion? Promotion { get; set; }
    }
}
