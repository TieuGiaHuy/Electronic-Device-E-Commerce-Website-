using BLL.DTOs.ProductVariant;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
    }
}
