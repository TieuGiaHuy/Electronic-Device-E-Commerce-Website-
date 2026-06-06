using BLL.DTOs.Product;

namespace BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductCreateDto productCreateDto);
        Task<bool> UpdateProductAsync(ProductUpdateDto productUpdateDto);
        Task<bool> DeleteProductAsync(ProductDeleteDto productDeleteDto);

        Task<ProductDto?> GetProductWithVariantsAsync(Guid productVariantId);
        Task<List<ProductDto>> SearchByNameAsync(string keyword);
        Task<List<ProductDto>> GetProductsByCategoryIdAsync(int categoryId);

    }
}
