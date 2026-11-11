namespace DAL.Models
{
    /// <summary>
    /// Một thông số (Tên - Giá trị) của biến thể sản phẩm.
    /// Ví dụ: "Bộ máy" = "Automatic", "Đường kính mặt" = "40mm", "Dung lượng pin" = "300mAh".
    /// Dùng bảng này thay cho các cột cố định để mỗi loại đồng hồ có bộ thông số riêng.
    /// </summary>
    public class VariantAttribute
    {
        public Guid VariantAttributeId { get; set; }

        public Guid ProductVariantId { get; set; }
        virtual public ProductVariant? ProductVariant { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        /// <summary>Thứ tự hiển thị (nhỏ hiển thị trước).</summary>
        public int DisplayOrder { get; set; }

        public static VariantAttribute Create(string name, string value, int displayOrder = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Attribute name cannot be empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Attribute value cannot be empty.", nameof(value));

            // Không gán VariantAttributeId: EF Core tự sinh Guid khi thêm mới.
            return new VariantAttribute
            {
                Name = name.Trim(),
                Value = value.Trim(),
                DisplayOrder = displayOrder
            };
        }
    }
}
