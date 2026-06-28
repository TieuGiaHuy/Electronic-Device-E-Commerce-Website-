using BLL.DTOs.Product.Staff;
using BLL.DTOs.ProductVariant.Staff;
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
        public ProductVariantStaffCreateDto ProductVariant { get; set; } = new();
        public ProductStaffDto? Product { get; set; } = new();

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
                TempData["SuccessMessage"] = "The variant was created successfully!";
                return RedirectToPage("./Index" , new { productId = ProductVariant.ProductId });
            }
            
            TempData["ErrorMessage"] = "Failed to create the variant!";
            ModelState.AddModelError(string.Empty, "Failed to create the Product Variant.");
            return Page();
        }
    }
}
