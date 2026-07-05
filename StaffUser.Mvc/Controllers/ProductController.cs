using BLL.DTOs.Product.Customer;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Mvc.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int id)
        {
            ProductCustomerDto? productCustomerDto = await _productService.GetProductCustomerByIdAsync(id);
            if (productCustomerDto == null)
            {
                return NotFound();
            }
            return View(productCustomerDto);
        }
    }
}
