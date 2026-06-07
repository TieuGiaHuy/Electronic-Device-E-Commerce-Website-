

using BLL.DTOs.ProductVariant;

namespace BLL.Services.Interfaces
{
    public interface IProductVariantService
    {
        Task<List<ProductVariantDto>> GetAllProductVariantsAsync();
        Task<ProductVariantDto?> GetProductVariantByIdAsync(Guid productVariantId);
        Task<ProductVariantDto> CreateProductVariantAsync(ProductVariantCreateDto productVariant);
        Task<bool> UpdateProductVariantAsync(ProductVariantUpdateDto productVariant);
        Task<bool> DeleteProductVariantAsync(ProductVariantDeleteDto productVariant);


        Task<List<ProductVariantDto>> GetVariantsByProductIdAsync(int productId);
        Task<bool> StockInAsync(Guid variantId, int increaseAmount);
    }
}
