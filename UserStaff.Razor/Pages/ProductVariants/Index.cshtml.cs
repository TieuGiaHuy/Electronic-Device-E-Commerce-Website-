using BLL.DTOs.ProductVariant;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace UserStaff.Razor.Pages.ProductVariants
{
    public class IndexModel : PageModel
    {
        private readonly IProductVariantService _productVariantService;

        public IndexModel(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService;
        }

        public List<ProductVariantDto> ProductVariants { get; set; } = new List<ProductVariantDto>();
        public int productId;

        public async Task OnGetAsync(int productId)
        {
            ProductVariants = await _productVariantService.GetVariantsByProductIdAsync(productId);
            this.productId = productId;
        }

        public async Task<JsonResult> OnPostDeleteAsync(Guid id)
        {
            try
            {
                // 1. Tìm biến thể trong Database theo ID truyền lên
                var deleteStatus = await _productVariantService.DeleteProductVariantAsync(new ProductVariantDeleteDto { ProductVariantId = id });

                if (!deleteStatus)
                {
                    return new JsonResult(new { success = false, message = "Biến thể không tồn tại hoặc đã bị xóa trước đó!" });
                }

                // 4. Trả về kết quả dạng JSON cho AJAX hiển thị Toast thành công
                return new JsonResult(new { success = true, message = "Xóa thành công biến thể!" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        public async Task<JsonResult> OnPostStockInAsync(Guid id, int increaseAmount)
        {
            try
            {
                var stockInStatus = await _productVariantService.StockInAsync(id, increaseAmount);
                if (!stockInStatus)
                {
                    return new JsonResult(new { success = false, message = "Biến thể không tồn tại hoặc đã bị xóa trước đó!" });
                }

                TempData["SuccessMessage"] = "Nhập kho thành công!";

                return new JsonResult(new { success = true, message = "Nhập kho thành công!" });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi hệ thống: " + ex.Message;
                return new JsonResult(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }
}
