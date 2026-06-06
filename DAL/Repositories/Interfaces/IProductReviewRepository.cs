using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IProductReviewRepository
    {
        Task<List<ProductReview>> GetAllAsync();
        Task<ProductReview?> GetByIdAsync(int id);
        Task<List<ProductReview>> GetByProductIdAsync(int productId);
        Task<List<ProductReview>> GetByUserIdAsync(int userId);
        Task<ProductReview> CreateAsync(ProductReview review);
        Task<bool> UpdateAsync(ProductReview review);
        Task<bool> DeleteAsync(int id);
    }
}