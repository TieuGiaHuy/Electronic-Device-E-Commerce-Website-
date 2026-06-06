using BLL.DTOs.Product;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UserStaff.Razor.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        public async Task OnGetAsync()
        {
            Products = await _productService.GetAllProductsAsync();
        }
    }
}
