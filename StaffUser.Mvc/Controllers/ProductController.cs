using BLL.DTOs.CartItem;
using BLL.DTOs.Product.Customer;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartItemService _cartItemService;
        public ProductController(IProductService productService, ICartItemService cartItemService)
        {
            _productService = productService;
            _cartItemService = cartItemService;
        }

        int productId = 0;

        public async Task<IActionResult> Index(int id)
        {
            productId = id;
            ProductCustomerDto? productCustomerDto = await _productService.GetProductCustomerByIdAsync(id);
            if (productCustomerDto == null)
            {
                return NotFound();
            }
            return View(productCustomerDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid variantId, int quantity)
        {
            var cartItem = new CartItemCreateDTO
            {
                ProductVariantId = variantId,
                Quantity = quantity,
                CustomerId = 1
            };
            await _cartItemService.CreateCartItemAsync(cartItem);
            return RedirectToAction(nameof(Index), new { id = productId });
        }
    }
}
