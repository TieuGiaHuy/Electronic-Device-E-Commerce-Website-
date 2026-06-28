using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Utilities
{
    public class SkuGenerator
    {
        public static string Generate(string productCode, string color, string? cpu, string? ram, string? storage)
        {
            if (string.IsNullOrWhiteSpace(productCode))
                throw new ArgumentException("Product Code is NOT empty.");

            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("Color is NOT empty.");

            // 1. Chuẩn hóa mã sản phẩm và màu sắc (Xóa khoảng trắng, viết hoa)
            string cleanCode = productCode.Replace(" ", "").ToUpper();
            string cleanColor = color.Replace(" ", "").ToUpper();

            // Mẹo: Lấy 3 chữ cái đầu của màu để SKU ngắn gọn (Ví dụ: Silver -> SIL)
            if (cleanColor.Length > 3)
                cleanColor = cleanColor.Substring(0, 3);

            // 2. Kiểm tra các thuộc tính nullable, nếu có thì format thêm vào SKU
            string cleanCpu = !string.IsNullOrWhiteSpace(cpu) ? $"-{cpu.Replace(" ", "").ToUpper()}" : "";
            string cleanRam = !string.IsNullOrWhiteSpace(ram) ? $"-{ram.Replace(" ", "").ToUpper()}" : "";
            string cleanStorage = !string.IsNullOrWhiteSpace(storage) ? $"-{storage.Replace(" ", "").ToUpper()}" : "";

            // Kết quả dạng: IP15PM-BLA-A17PRO-8GB-256GB
            return $"{cleanCode}-{cleanColor}{cleanCpu}{cleanRam}{cleanStorage}";
        }
    }
}
