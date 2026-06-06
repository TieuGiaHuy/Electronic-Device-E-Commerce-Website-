using BLL.DTOs.Product;
using BLL.DTOs.ProductVariant;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UserStaff.Razor.Pages.ProductVariants
{
    public class CreateModel : PageModel
    {
        private readonly IProductVariantService _productVariantService;
        private readonly IProductService _productService;

        public CreateModel(IProductVariantService productVariantService, IProductService productService)
        {
            _productVariantService = productVariantService;
            _productService = productService;
        }

        [BindProperty]
        public ProductVariantCreateDto ProductVariant { get; set; } = new();
        public ProductDto? Product { get; set; } = new();

        public async Task OnGetAsync(int productId)
        {
            Product = await _productService.GetProductByIdAsync(productId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _productVariantService.CreateProductVariantAsync(ProductVariant);
            if (result != null)
            {
                return RedirectToPage("./Index" , new { productId = ProductVariant.ProductId });
            }

            ModelState.AddModelError(string.Empty, "Tạo Product Variant thất bại.");
            return Page();
        }
    }
}
