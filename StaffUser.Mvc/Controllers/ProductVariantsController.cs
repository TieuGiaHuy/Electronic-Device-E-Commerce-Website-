using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Models;
using BLL.Services.Interfaces;
using BLL.DTOs.ProductVariant.Customer;
using BLL.DTOs.Product.Customer;

namespace Customer.Mvc.Controllers
{
    public class ProductVariantsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductVariantService productVariantService;

        public ProductVariantsController(AppDbContext context, IProductVariantService productVariantService)
        {
            _context = context;
            this.productVariantService = productVariantService;
        }

        // GET: ProductVariants
        public IActionResult Index(int productId)
        {
            // Lấy dữ liệu từ DB, nhớ loại bỏ các sản phẩm đã bị Xóa mềm (IsDeleted)
            // var variants = _context.ProductVariants
            //     .Where(v => v.ProductId == productId && !v.IsDeleted)
            //     .Select(v => new ProductVariantCustomerDto
            //     {
            //         ProductVariantId = v.ProductVariantId,
            //         Sku = v.Sku,
            //         Color = v.Color,
            //         Cpu = v.Cpu,
            //         Ram = v.Ram,
            //         Storage = v.Storage,
            //         ScreenSize = v.ScreenSize,
            //         Price = v.Price,
            //         ImageUrl = v.ImageUrl ?? "/images/default-product.png", // Ảnh mặc định nếu null
            //         IsAvailable = v.StockQuantity > 0
            //     }).ToList();

            // Dữ liệu mẫu (Mock Data) để bạn chạy thử nghiệm giao diện theo layout
            var mockVariants = new List<ProductVariantCustomerDto>
            {
                new ProductVariantCustomerDto {
                    ProductVariantId = Guid.NewGuid(), Sku = "IP15PM-NAT-256", Color = "Natural Titanium",
                    Cpu = "A17 Pro", Ram = "8GB", Storage = "256GB", ScreenSize = "6.7 inch",
                    Price = 28990000, ImageUrl = "https://picsum.photos/240/240"
                },
                new ProductVariantCustomerDto {
                    ProductVariantId = Guid.NewGuid(), Sku = "IP15PM-BLA-512", Color = "Black Titanium",
                    Cpu = "A17 Pro", Ram = "8GB", Storage = "512GB", ScreenSize = "6.7 inch",
                    Price = 34490000, ImageUrl = "https://picsum.photos/240/240"
                }
            };

            ViewData["Title"] = "Product Variants";
            return View(mockVariants);
        }

        public IActionResult Details(int id)
        {
            // Trong thực tế, bạn sẽ gọi tầng BLL để lấy dữ liệu:
            // ProductCustomerDto productDto = _productService.GetProductDetailForCustomer(id);
            // if (productDto == null || productDto.IsDeleted) return NotFound();

            // -----------------------------------------------------------------
            // DỮ LIỆU GIẢ LẬP (MOCK DATA) chuẩn cấu trúc DTO của bạn để test giao diện
            // -----------------------------------------------------------------
            var productDto = new ProductCustomerDto
            {
                ProductId = id,
                ProductName = "Samsung Galaxy S26 Ultra 5G",
                Brand = "Samsung",
                CategoryId = 1,
                CategoryName = "Phones",
                ImageUrl = "https://picsum.photos/400/400?random=10",
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            productDto.ProductVariants = new List<ProductVariantCustomerDto>
            {
                // Nhóm cấu hình 256GB
                new ProductVariantCustomerDto {
                    ProductVariantId = Guid.NewGuid(), ProductId = id, Sku = "S26U-TIM-256", ProductCode = "S26U",
                    Color = "Tím Cobalt", Storage = "256GB", Ram = "12GB", Cpu = "Snapdragon 8 Elite", ScreenSize = "6.9 inch",
                    Price = 29790000, StockQuantity = 15, ImageUrl = "https://picsum.photos/400/400?random=1", IsDeleted = false
                },
                new ProductVariantCustomerDto {
                    ProductVariantId = Guid.NewGuid(), ProductId = id, Sku = "S26U-DEN-256", ProductCode = "S26U",
                    Color = "Đen Classic", Storage = "256GB", Ram = "12GB", Cpu = "Snapdragon 8 Elite", ScreenSize = "6.9 inch",
                    Price = 29790000, StockQuantity = 5, ImageUrl = "https://picsum.photos/400/400?random=2", IsDeleted = false
                },
                // Nhóm cấu hình 512GB
                new ProductVariantCustomerDto {
                    ProductVariantId = Guid.NewGuid(), ProductId = id, Sku = "S26U-TIM-512", ProductCode = "S26U",
                    Color = "Tím Cobalt", Storage = "512GB", Ram = "12GB", Cpu = "Snapdragon 8 Elite", ScreenSize = "6.9 inch",
                    Price = 33990000, StockQuantity = 8, ImageUrl = "https://picsum.photos/400/400?random=1", IsDeleted = false
                },
                new ProductVariantCustomerDto {
                    ProductVariantId = Guid.NewGuid(), ProductId = id, Sku = "S26U-XAN-512", ProductCode = "S26U",
                    Color = "Xanh Sky", Storage = "512GB", Ram = "12GB", Cpu = "Snapdragon 8 Elite", ScreenSize = "6.9 inch",
                    Price = 33990000, StockQuantity = 0, ImageUrl = "https://picsum.photos/400/400?random=3", IsDeleted = false // Hết hàng
                }
            };
            // -----------------------------------------------------------------

            ViewData["Title"] = productDto.ProductName;
            return View(productDto);
        }

        // GET: ProductVariants/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Brand");
            return View();
        }

        // POST: ProductVariants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductVariantId,ProductCode,Sku,Color,Cpu,Ram,Storage,ScreenSize,Price,StockQuantity,ImageUrl,ProductId,IsDeleted,CreatedAt,UpdatedAt")] ProductVariant productVariant)
        {
            if (ModelState.IsValid)
            {
                productVariant.ProductVariantId = Guid.NewGuid();
                _context.Add(productVariant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Brand", productVariant.ProductId);
            return View(productVariant);
        }

        // GET: ProductVariants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productVariant = await _context.ProductVariants.FindAsync(id);
            if (productVariant == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Brand", productVariant.ProductId);
            return View(productVariant);
        }

        // POST: ProductVariants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProductVariantId,ProductCode,Sku,Color,Cpu,Ram,Storage,ScreenSize,Price,StockQuantity,ImageUrl,ProductId,IsDeleted,CreatedAt,UpdatedAt")] ProductVariant productVariant)
        {
            if (id != productVariant.ProductVariantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productVariant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductVariantExists(productVariant.ProductVariantId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Brand", productVariant.ProductId);
            return View(productVariant);
        }

        // GET: ProductVariants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productVariant = await _context.ProductVariants
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductVariantId == id);
            if (productVariant == null)
            {
                return NotFound();
            }

            return View(productVariant);
        }

        // POST: ProductVariants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productVariant = await _context.ProductVariants.FindAsync(id);
            if (productVariant != null)
            {
                _context.ProductVariants.Remove(productVariant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductVariantExists(Guid id)
        {
            return _context.ProductVariants.Any(e => e.ProductVariantId == id);
        }
    }
}
