using BLL.DTOs.ProductReview;
using BLL.Services.Interfaces;
using DAL.Repositories.Interfaces;

namespace BLL.Services
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly IProductReviewRepository _productReviewRepository;

        public ProductReviewService(IProductReviewRepository productReviewRepository)
        {
            _productReviewRepository = productReviewRepository;
        }

        public async Task<List<ProductReviewDto>> GetAllProductReviewsAsync()
        {
            var reviews = await _productReviewRepository.GetAllAsync();
            return reviews.Select(r => new ProductReviewDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = r.Customer?.FullName ?? string.Empty,
                ProductId = r.ProductId,
                ProductName = r.Product?.ProductName ?? string.Empty,
                Rating = r.Rating,
                Comment = r.Comment,
                IsFlagged = r.IsFlagged,
                IsDeleted = r.IsDeleted,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            }).ToList();
        }

        public async Task<ProductReviewDto?> GetProductReviewByIdAsync(int id)
        {
            var review = await _productReviewRepository.GetByIdAsync(id);
            if (review != null)
            {
                return new ProductReviewDto
                {
                    Id = review.Id,
                    CustomerId = review.CustomerId,
                    CustomerName = review.Customer?.FullName ?? string.Empty,
                    ProductId = review.ProductId,
                    ProductName = review.Product?.ProductName ?? string.Empty,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    IsFlagged = review.IsFlagged,
                    IsDeleted = review.IsDeleted,
                    CreatedAt = review.CreatedAt,
                    UpdatedAt = review.UpdatedAt
                };
            }
            return null;
        }

        public async Task<List<ProductReviewDto>> GetProductReviewsByProductIdAsync(int productId)
        {
            var reviews = await _productReviewRepository.GetByProductIdAsync(productId);
            return reviews.Select(r => new ProductReviewDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = r.Customer?.FullName ?? string.Empty,
                ProductId = r.ProductId,
                ProductName = r.Product?.ProductName ?? string.Empty,
                Rating = r.Rating,
                Comment = r.Comment,
                IsFlagged = r.IsFlagged,
                IsDeleted = r.IsDeleted,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            }).ToList();
        }

        public async Task<List<ProductReviewDto>> GetProductReviewsByCustomerIdAsync(int customerId)
        {
            var reviews = await _productReviewRepository.GetByCustomerIdAsync(customerId);
            return reviews.Select(r => new ProductReviewDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = r.Customer?.FullName ?? string.Empty,
                ProductId = r.ProductId,
                ProductName = r.Product?.ProductName ?? string.Empty,
                Rating = r.Rating,
                Comment = r.Comment,
                IsFlagged = r.IsFlagged,
                IsDeleted = r.IsDeleted,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            }).ToList();
        }

        public async Task<bool> FlagProductReviewAsync(int id)
        {
            return await _productReviewRepository.FlagAsync(id);
        }

        public async Task<bool> DeleteProductReviewAsync(int id)
        {
            return await _productReviewRepository.DeleteAsync(id);
        }
    }
}