using DAL.Enums;
using DAL.Models.Base;
using DAL.Utilities;

namespace DAL.Models
{
    public class ProductVariant : IBaseEntity
    {
        public Guid ProductVariantId { get; set; }
        public string Sku { get; set; } = string.Empty;
        /// <summary>Màu sắc (ví dụ màu mặt số, màu vỏ hoặc màu dây).</summary>
        public string Color { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductId { get; set; }
        virtual public Product? Product { get; set; }

        /// <summary>
        /// Các thông số kỹ thuật dạng Tên - Giá trị (bộ máy, đường kính mặt, dung lượng pin...).
        /// Tự động nạp cùng biến thể (xem ProductVariantConfiguration).
        /// </summary>
        virtual public ICollection<VariantAttribute> Attributes { get; set; } = new List<VariantAttribute>();

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? PromotionId { get; set; } = null;
        virtual public Promotion? Promotion { get; set; }

        public static ProductVariant Create(
            string color,
            IEnumerable<KeyValuePair<string, string>>? attributes,
            decimal price,
            int stockQuantity,
            string? imageUrl,
            int productId,
            Guid? promotionId = null)
        {
            if (price < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(price));
            if (stockQuantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative.", nameof(stockQuantity));

            var attributeList = (attributes ?? Enumerable.Empty<KeyValuePair<string, string>>()).ToList();

            var productVariant = new ProductVariant
            {
                Sku = SkuGenerator.Generate(productId, color, attributeList),
                Color = color,
                Price = price,
                StockQuantity = stockQuantity,
                ImageUrl = imageUrl,
                ProductId = productId,
                PromotionId = promotionId,
                CreatedAt = DateTime.UtcNow
            };
            productVariant.SetAttributes(attributeList);
            return productVariant;
        }

        /// <summary>
        /// Cập nhật biến thể. Truyền promotionId = null sẽ gỡ khuyến mãi khỏi biến thể
        /// (khớp với hành vi của overload có promotionId trước đây).
        /// </summary>
        public void Update(
            string color,
            IEnumerable<KeyValuePair<string, string>>? attributes,
            decimal price,
            int stockQuantity,
            string? imageUrl,
            int productId,
            Guid? promotionId)
        {
            if (price < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(price));
            if (stockQuantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative.", nameof(stockQuantity));

            var attributeList = (attributes ?? Enumerable.Empty<KeyValuePair<string, string>>()).ToList();

            Sku = SkuGenerator.Generate(productId, color, attributeList);
            Color = color;
            Price = price;
            StockQuantity = stockQuantity;
            ImageUrl = imageUrl;
            ProductId = productId;
            PromotionId = promotionId;
            SetAttributes(attributeList);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Cập nhật biến thể và giữ nguyên khuyến mãi hiện có
        /// (khớp với overload không có promotionId trước đây).
        /// </summary>
        public void Update(
            string color,
            IEnumerable<KeyValuePair<string, string>>? attributes,
            decimal price,
            int stockQuantity,
            string? imageUrl,
            int productId)
        {
            Update(color, attributes, price, stockQuantity, imageUrl, productId, PromotionId);
        }

        /// <summary>Thay toàn bộ thông số bằng danh sách mới (bỏ qua dòng thiếu tên hoặc giá trị).</summary>
        public void SetAttributes(IEnumerable<KeyValuePair<string, string>>? attributes)
        {
            Attributes.Clear();
            if (attributes == null) return;

            int order = 0;
            foreach (var kv in attributes)
            {
                if (string.IsNullOrWhiteSpace(kv.Key) || string.IsNullOrWhiteSpace(kv.Value))
                    continue;
                Attributes.Add(VariantAttribute.Create(kv.Key, kv.Value, order++));
            }
        }

        /// <summary>Tra cứu nhanh giá trị thông số theo tên (không phân biệt hoa thường).</summary>
        public string? GetAttribute(string name)
        {
            return Attributes
                .FirstOrDefault(a => string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase))
                ?.Value;
        }

        /// <summary>
        /// Mô tả ngắn dùng trong đơn hàng: màu kèm một kích thước đặc trưng
        /// (đường kính mặt, kích thước màn hình hoặc độ rộng dây) nếu có.
        /// </summary>
        public string GetShortDescription()
        {
            var size = GetAttribute(WatchAttributeNames.CaseDiameter)
                       ?? GetAttribute(WatchAttributeNames.ScreenSize)
                       ?? GetAttribute("Độ rộng dây");
            return $"{Color} {size}".Trim();
        }

        public void Delete()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void StockIn(int increaseAmount)
        {
            if (increaseAmount < 0)
                throw new ArgumentException("The stock increase amount cannot be negative.", nameof(increaseAmount));

            StockQuantity += increaseAmount;
            UpdatedAt = DateTime.UtcNow;
        }

        public decimal GetPrice() 
        {
            if (Promotion != null && Promotion.IsActive())
            {
                if (Promotion.PromotionType == PromotionTypeEnum.Percentage && Promotion.Percentage.HasValue)
                {
                    return Price * (1 - Promotion.Percentage.Value / 100m);
                }
                else if (Promotion.PromotionType == PromotionTypeEnum.FixedAmount && Promotion.SalePrice.HasValue)
                {
                    return Price - Promotion.SalePrice.Value;
                }
            }

            return Price;
        }
    }
}
