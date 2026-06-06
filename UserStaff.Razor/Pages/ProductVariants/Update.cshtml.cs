using BLL.DTOs.Product;
using BLL.DTOs.ProductVariant;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UserStaff.Razor.Pages.ProductVariants
{
    public class UpdateModel : PageModel
    {
        private readonly IProductVariantService _productVariantService;
        private readonly IProductService _productService;

        public UpdateModel(IProductVariantService productVariantService, IProductService productService)
        {
            _productVariantService = productVariantService;
            _productService = productService;
        }

        [BindProperty]
        public ProductVariantUpdateDto ProductVariant { get; set; } = new();
        public ProductDto? Product { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid id, int productId)
        {
            Product = await _productService.GetProductByIdAsync(productId);

            var variantDto = await _productVariantService.GetProductVariantByIdAsync(id);
            if (variantDto != null)
            {
                ProductVariant = new ProductVariantUpdateDto
                {
                    ProductVariantId = variantDto.ProductVariantId,
                    ProductId = variantDto.ProductId,
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
                return RedirectToPage("./Index", new { productId = ProductVariant.ProductId});
            }

            ModelState.AddModelError(string.Empty, "Cập nhật Product Variant thất bại.");
            return Page();
        }
    }
}
