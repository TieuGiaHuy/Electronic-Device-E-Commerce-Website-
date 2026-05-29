using BLL.DTOs.ProductVariant;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BLL.Services
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IProductVariantRepository _productVariantRepository;

        public ProductVariantService(IProductVariantRepository productVariantRepository)
        {
            _productVariantRepository = productVariantRepository;
        }

        public async Task<List<ProductVariantDto>> GetAllProductVariantsAsync()
        {
            var productVariants = await _productVariantRepository.GetAllProductVariantsAsync();
            return productVariants.Select(pv => new ProductVariantDto
            {
                ProductVariantId = pv.ProductVariantId,
                Sku = pv.Sku,
                Color = pv.Color,
                Cpu = pv.Cpu,
                Ram = pv.Ram,
                Storage = pv.Storage,
                ScreenSize = pv.ScreenSize,
                Price = pv.Price,
                StockQuantity = pv.StockQuantity,
                ImageUrl = pv.ImageUrl,
                ProductId = pv.ProductId,
                IsDeleted = pv.IsDeleted,
                CreatedAt = pv.CreatedAt,
                UpdatedAt = pv.UpdatedAt
            }).ToList();
        }

        public async Task<ProductVariantDto?> GetProductVariantByIdAsync(int productVariantId)
        {
            var productVariant = await _productVariantRepository.GetProductVariantByIdAsync(productVariantId);
            if (productVariant != null)
            {
                return new ProductVariantDto
                {
                    ProductVariantId = productVariant.ProductVariantId,
                    Sku = productVariant.Sku,
                    Color = productVariant.Color,
                    Cpu = productVariant.Cpu,
                    Ram = productVariant.Ram,
                    Storage = productVariant.Storage,
                    ScreenSize = productVariant.ScreenSize,
                    Price = productVariant.Price,
                    StockQuantity = productVariant.StockQuantity,
                    ImageUrl = productVariant.ImageUrl,
                    ProductId = productVariant.ProductId,
                    IsDeleted = productVariant.IsDeleted,
                    CreatedAt = productVariant.CreatedAt,
                    UpdatedAt = productVariant.UpdatedAt
                };
            }
            return null;
        }
        public async Task<ProductVariantDto> CreateProductVariantAsync(ProductVariantCreateDto productVariant)
        {
            var newVariant = ProductVariant.Create(
                productVariant.Sku,
                productVariant.Color,
                productVariant.Cpu,
                productVariant.Ram,
                productVariant.Storage,
                productVariant.ScreenSize,
                productVariant.Price,
                productVariant.StockQuantity,
                productVariant.ImageUrl,
                productVariant.ProductId
            );
            var createdVariant = await _productVariantRepository.CreateProductVariantAsync(newVariant);

            return new ProductVariantDto
            {
                ProductVariantId = createdVariant.ProductVariantId,
                Sku = createdVariant.Sku,
                Color = createdVariant.Color,
                Cpu = createdVariant.Cpu,
                Ram = createdVariant.Ram,
                Storage = createdVariant.Storage,
                ScreenSize = createdVariant.ScreenSize,
                Price = createdVariant.Price,
                StockQuantity = createdVariant.StockQuantity,
                ImageUrl = createdVariant.ImageUrl,
                ProductId = createdVariant.ProductId,
                IsDeleted = createdVariant.IsDeleted,
                CreatedAt = createdVariant.CreatedAt,
                UpdatedAt = createdVariant.UpdatedAt
            };
        }

        public async Task<bool> UpdateProductVariantAsync(ProductVariantUpdateDto productVariant)
        {
            var existingVariant = new ProductVariant
            {
                ProductVariantId = productVariant.ProductVariantId
            };

            existingVariant.Update(
                productVariant.Sku,
                productVariant.Color,
                productVariant.Cpu,
                productVariant.Ram,
                productVariant.Storage,
                productVariant.ScreenSize,
                productVariant.Price,
                productVariant.StockQuantity,
                productVariant.ImageUrl,
                existingVariant.ProductId
            );

            return await _productVariantRepository.UpdateProductVariantAsync(existingVariant);
        }

        public async Task<bool> DeleteProductVariantAsync(ProductVariantDeleteDto productVariant)
        {
            return await _productVariantRepository.DeleteProductVariantAsync(productVariant.ProductVariantId);
        }

        public async Task<List<ProductVariantDto>> GetVariantsByProductIdAsync(int productId)
        {
            var variants = await _productVariantRepository.GetVariantsByProductIdAsync(productId);
            return variants.Select(v => new ProductVariantDto
            {
                ProductVariantId = v.ProductVariantId,
                Sku = v.Sku,
                Color = v.Color,
                Cpu = v.Cpu,
                Ram = v.Ram,
                Storage = v.Storage,
                ScreenSize = v.ScreenSize,
                Price = v.Price,
                StockQuantity = v.StockQuantity,
                ImageUrl = v.ImageUrl,
                ProductId = v.ProductId,
                IsDeleted = v.IsDeleted,
                CreatedAt = v.CreatedAt,
                UpdatedAt = v.UpdatedAt
            }).ToList();
        }
    }
}
