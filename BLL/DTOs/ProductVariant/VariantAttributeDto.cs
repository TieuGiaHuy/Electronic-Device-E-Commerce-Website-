namespace BLL.DTOs.ProductVariant
{
    /// <summary>Một thông số của biến thể sản phẩm, dạng Tên - Giá trị (ví dụ "Bộ máy" - "Automatic").</summary>
    public class VariantAttributeDto
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}
