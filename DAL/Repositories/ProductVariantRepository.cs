using DAL.Data;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly AppDbContext _context;
        public ProductVariantRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProductVariant>> GetAllProductVariantsAsync()
        {
            var productVariants = await _context.ProductVariants
                .Include(pv => pv.Promotion)
                .ToListAsync();
            return productVariants;  
        }

        public async Task<ProductVariant?> GetProductVariantByIdAsync(Guid productVariantId)
        {
            var productVariant = await _context.ProductVariants
                .Include(pv => pv.Promotion)
                .FirstOrDefaultAsync(pv => pv.ProductVariantId == productVariantId);
            return productVariant;
        }
        public async Task<ProductVariant> CreateProductVariantAsync(ProductVariant productVariant)
        {
            _context.ProductVariants.Add(productVariant);
            await _context.SaveChangesAsync();
            return productVariant;
        }

        public async Task<bool> UpdateProductVariantAsync(ProductVariant productVariant)
        {
            // Nạp kèm thông số hiện có để SetAttributes thay thế đúng (xóa dòng cũ, thêm dòng mới)
            var existingProductVariant = await _context.ProductVariants
                .Include(pv => pv.Attributes)
                .FirstOrDefaultAsync(pv => pv.ProductVariantId == productVariant.ProductVariantId);

            if (existingProductVariant == null)
                return false;

            var newAttributes = productVariant.Attributes
                .OrderBy(a => a.DisplayOrder)
                .Select(a => new KeyValuePair<string, string>(a.Name, a.Value))
                .ToList();

            existingProductVariant.Update(
                productVariant.Color,
                newAttributes,
                productVariant.Price,
                productVariant.StockQuantity,
                productVariant.ImageUrl,
                productVariant.ProductId,
                productVariant.PromotionId);

            // existingProductVariant đang được theo dõi nên không cần gọi Update() trên cả đồ thị
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductVariantAsync(Guid productVariantId)
        {
            var existingProductVariant = await _context.ProductVariants.FindAsync(productVariantId);
            if (existingProductVariant != null)
            {
                existingProductVariant.Delete();
                _context.ProductVariants.Update(existingProductVariant);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<ProductVariant>> GetVariantsByProductIdAsync(int productId)
        {
            var variants = await _context.ProductVariants
                .Where(pv => pv.ProductId == productId).Include(pv => pv.Promotion)
                .ToListAsync();
            return variants;
        }

        public async Task<bool> StockInAsync(Guid variantId, int increaseAmount)
        {
            var existingVariant = await _context.ProductVariants.FindAsync(variantId);
            if (existingVariant == null)
                return false;

            existingVariant.StockIn(increaseAmount);
            _context.ProductVariants.Update(existingVariant);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
