using Microsoft.AspNetCore.Mvc;
using StaffUser.Mvc.Models;
using BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StaffUser.Mvc.Utilities;

namespace StaffUser.Mvc.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductVariantService _productVariantService;
        private readonly IProductService _productService;

        public CartController(
            IProductVariantService productVariantService,
            IProductService productService)
        {
            _productVariantService = productVariantService;
            _productService = productService;
        }

        // View Cart Page
        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        // Fetch variants for quick selector modal on homepage
        [HttpGet]
        public async Task<IActionResult> GetProductVariants(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            var variants = await _productVariantService.GetVariantsByProductIdAsync(productId);
            var activeVariants = variants.Where(v => !v.IsDeleted).ToList();

            return Json(new
            {
                productId = product.ProductId,
                productName = product.ProductName,
                brand = product.Brand,
                variants = activeVariants.Select(v => new
                {
                    productVariantId = v.ProductVariantId,
                    sku = v.Sku,
                    color = v.Color,
                    cpu = v.Cpu,
                    ram = v.Ram,
                    storage = v.Storage,
                    screenSize = v.ScreenSize,
                    price = v.Price,
                    stockQuantity = v.StockQuantity,
                    imageUrl = v.ImageUrl
                })
            });
        }

        // Add item to cart (AJAX or standard form post)
        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid variantId, int quantity = 1)
        {
            if (quantity <= 0) quantity = 1;

            var variant = await _productVariantService.GetProductVariantByIdAsync(variantId);
            if (variant == null || variant.IsDeleted)
            {
                return Json(new { success = false, message = "Product variant not found." });
            }

            if (variant.StockQuantity < quantity)
            {
                return Json(new { success = false, message = $"Not enough stock. Available: {variant.StockQuantity}." });
            }

            var product = await _productService.GetProductByIdAsync(variant.ProductId);
            if (product == null)
            {
                return Json(new { success = false, message = "Product not found." });
            }

            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            var existingItem = cart.FirstOrDefault(i => i.ProductVariantId == variantId);

            if (existingItem != null)
            {
                var newQty = existingItem.Quantity + quantity;
                if (variant.StockQuantity < newQty)
                {
                    return Json(new { success = false, message = $"Cannot add more. Max stock available: {variant.StockQuantity}." });
                }
                existingItem.Quantity = newQty;
            }
            else
            {
                var specs = new List<string>();
                if (!string.IsNullOrEmpty(variant.Cpu)) specs.Add(variant.Cpu);
                if (!string.IsNullOrEmpty(variant.Ram)) specs.Add(variant.Ram + " RAM");
                if (!string.IsNullOrEmpty(variant.Storage)) specs.Add(variant.Storage);
                if (!string.IsNullOrEmpty(variant.ScreenSize)) specs.Add(variant.ScreenSize + " Screen");

                var imageUrl = !string.IsNullOrEmpty(variant.ImageUrl) 
                    ? variant.ImageUrl 
                    : product.ImageUrl?.Split(';', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

                cart.Add(new CartItem
                {
                    ProductVariantId = variant.ProductVariantId,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Brand = product.Brand,
                    VariantSku = variant.Sku,
                    VariantColor = variant.Color,
                    VariantSpecs = string.Join(", ", specs),
                    Price = variant.Price,
                    Quantity = quantity,
                    ImageUrl = imageUrl,
                    MaxStock = variant.StockQuantity
                });
            }

            HttpContext.Session.Set("Cart", cart);
            var totalCount = cart.Sum(i => i.Quantity);

            return Json(new { success = true, message = $"Product '{product.ProductName}' added to cart!", totalCount });
        }

        // Update quantity in cart
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(Guid variantId, int quantity)
        {
            if (quantity <= 0)
            {
                return Json(new { success = false, message = "Quantity must be at least 1." });
            }

            var variant = await _productVariantService.GetProductVariantByIdAsync(variantId);
            if (variant == null || variant.IsDeleted)
            {
                return Json(new { success = false, message = "Product variant not found." });
            }

            if (variant.StockQuantity < quantity)
            {
                return Json(new { success = false, message = $"Only {variant.StockQuantity} items are available in stock." });
            }

            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(i => i.ProductVariantId == variantId);
            if (item != null)
            {
                item.Quantity = quantity;
                HttpContext.Session.Set("Cart", cart);
            }

            var totalCount = cart.Sum(i => i.Quantity);
            var totalPrice = cart.Where(i => i.ProductVariantId == variantId).Sum(i => i.Price * i.Quantity);
            var cartTotal = cart.Sum(i => i.Price * i.Quantity);

            return Json(new { success = true, totalCount, totalPrice, cartTotal });
        }

        // Remove item from cart
        [HttpPost]
        public IActionResult Remove(Guid variantId)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(i => i.ProductVariantId == variantId);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.Set("Cart", cart);
            }

            var totalCount = cart.Sum(i => i.Quantity);
            var cartTotal = cart.Sum(i => i.Price * i.Quantity);

            return Json(new { success = true, totalCount, cartTotal });
        }
    }
}
