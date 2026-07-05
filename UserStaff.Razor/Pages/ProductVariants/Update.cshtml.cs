using BLL.DTOs.Product;
using BLL.DTOs.ProductVariant;
using BLL.DTOs.Promotion;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UserStaff.Razor.Pages.ProductVariants
{
    public class UpdateModel : PageModel
    {
        private readonly IProductVariantService _productVariantService;
        private readonly IProductService _productService;
        private readonly IPromotionService _promotionService;

        public UpdateModel(IProductVariantService productVariantService, IProductService productService, IPromotionService promotionService)
        {
            _productVariantService = productVariantService;
            _productService = productService;
            _promotionService = promotionService;
        }

        [BindProperty]
        public ProductVariantUpdateDto ProductVariant { get; set; } = new();
        public ProductDto? Product { get; set; } = new();
        [BindProperty]
        public List<PromotionDto> PromotionDtos { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid id, int productId)
        {
            Product = await _productService.GetProductByIdAsync(productId);
            PromotionDtos = await _promotionService.GetActiveAndUpcomingPromotionsAsync();
            var variantDto = await _productVariantService.GetProductVariantByIdAsync(id);
            if (variantDto != null)
            {
                ProductVariant = new ProductVariantUpdateDto
                {
                    ProductVariantId = variantDto.ProductVariantId,
                    ProductId = variantDto.ProductId,
                    PromotionId = variantDto.PromotionId,
                    Promotion = variantDto.Promotion,
                    ProductCode = variantDto.ProductCode,
                    Color = variantDto.Color,
                    Cpu = variantDto.Cpu,
                    Ram = variantDto.Ram,
                    Storage = variantDto.Storage,
                    ScreenSize = variantDto.ScreenSize,
                    Price = variantDto.Price,
                    StockQuantity = variantDto.StockQuantity,
                    ImageUrl = variantDto.ImageUrl
                };
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _productVariantService.UpdateProductVariantAsync(ProductVariant);
            if (result)
            {
                TempData["SuccessMessage"] = "The variant was updated successfully!";
                return RedirectToPage("./Index", new { productId = ProductVariant.ProductId});
            }

            TempData["ErrorMessage"] = "Failed to update the variant!";
            ModelState.AddModelError(string.Empty, "Failed to update the Product Variant.");
            return Page();
        }
    }
}
