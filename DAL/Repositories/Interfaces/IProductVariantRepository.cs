using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IProductVariantRepository
    {
        Task<List<ProductVariant>> GetAllProductVariantsAsync();
        Task<ProductVariant?> GetProductVariantByIdAsync(int productVariantId);
        Task<ProductVariant> CreateProductVariantAsync(ProductVariant productVariant);
        Task<bool> UpdateProductVariantAsync(ProductVariant productVariant);
        Task<bool> DeleteProductVariantAsync(int productVariantId);


        Task<List<ProductVariant>> GetVariantsByProductIdAsync(int productId);
    }
}
