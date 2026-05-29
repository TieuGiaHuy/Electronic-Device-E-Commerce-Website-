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
            var productVariants = await _context.ProductVariants.ToListAsync();
            return productVariants;  
        }

        public async Task<ProductVariant?> GetProductVariantByIdAsync(int productVariantId)
        {
            var productVariant = await _context.ProductVariants.FindAsync(productVariantId);
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
            var existingProductVariant = await _context.ProductVariants.FindAsync(productVariant.ProductVariantId);
            if (existingProductVariant != null)
            {
                var sku = existingProductVariant.Sku;
                var color = existingProductVariant.Color;
                var cpu = existingProductVariant.Cpu;
                var ram = existingProductVariant.Ram;
                var storage = existingProductVariant.Storage;
                var screenSize = existingProductVariant.ScreenSize;
                var price = existingProductVariant.Price;
                var stockQuantity = existingProductVariant.StockQuantity;
                var imageUrl = existingProductVariant.ImageUrl;
                var productId = existingProductVariant.ProductId;

                existingProductVariant.Update(sku, color, cpu, ram, storage, screenSize, price, stockQuantity, imageUrl, productId);
                _context.ProductVariants.Update(existingProductVariant);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public async Task<bool> DeleteProductVariantAsync(int productVariantId)
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
            var productVariants = await _context.ProductVariants
                .Where(pv => pv.ProductId == productId)
                .ToListAsync();
            return productVariants;
        }
    }
}
