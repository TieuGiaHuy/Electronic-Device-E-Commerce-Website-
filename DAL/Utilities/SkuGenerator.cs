using System.Security.Cryptography;
using System.Text;

namespace DAL.Utilities
{
    public class SkuGenerator
    {
        /// <summary>
        /// Sinh SKU dạng: {ProductId 9 số}-{3 chữ cái đầu của màu}-{mã băm 8 ký tự của các thông số}
        /// Ví dụ: 000000001-ĐEN-3F9A12BC
        /// Mã băm lấy từ toàn bộ thông số (không phụ thuộc thứ tự nhập), nên hai biến thể cùng sản phẩm
        /// và cùng màu nhưng khác thông số sẽ có SKU khác nhau, và SKU luôn ngắn (dưới 50 ký tự).
        /// </summary>
        public static string Generate(int productId, string color, IEnumerable<KeyValuePair<string, string>>? attributes)
        {
            if (productId <= 0)
                throw new ArgumentException("Product ID must be a positive integer.", nameof(productId));

            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("Color cannot be empty.", nameof(color));

            string cleanCode = productId.ToString().PadLeft(9, '0');

            string cleanColor = color.Replace(" ", "").ToUpperInvariant();
            if (cleanColor.Length > 3)
                cleanColor = cleanColor.Substring(0, 3);

            var segments = new List<string> { cleanCode, cleanColor };

            // Chuẩn hóa thông số: bỏ dòng rỗng, chữ thường, sắp xếp theo tên rồi giá trị
            var normalized = (attributes ?? Enumerable.Empty<KeyValuePair<string, string>>())
                .Where(a => !string.IsNullOrWhiteSpace(a.Key) && !string.IsNullOrWhiteSpace(a.Value))
                .Select(a => $"{a.Key.Trim().ToLowerInvariant()}={a.Value.Trim().ToLowerInvariant()}")
                .OrderBy(s => s, StringComparer.Ordinal)
                .ToList();

            if (normalized.Count > 0)
            {
                var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(string.Join("|", normalized)));
                segments.Add(Convert.ToHexString(bytes).Substring(0, 8));
            }

            return string.Join("-", segments);
        }
    }
}
