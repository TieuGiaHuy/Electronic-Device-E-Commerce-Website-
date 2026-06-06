using BLL.DTOs.Product;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UserStaff.Razor.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;

        public CreateModel(IProductService productService)
        {
            _productService = productService;
        }
        [BindProperty]
        public ProductCreateDto ProductToCreate { get; set; } = new();
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await _productService.CreateProductAsync(ProductToCreate);
            if (result != null)
            {
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError(string.Empty, "An error occurred while creating the product. Please try again.");
            return Page();
        }
    }
}
