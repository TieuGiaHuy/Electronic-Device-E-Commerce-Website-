using DAL.Data;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductReviewRepository : IProductReviewRepository
    {
        private readonly AppDbContext _context;
        public ProductReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductReview>> GetAllAsync()
        {
            return await _context.ProductReviews.ToListAsync();
        }

        public async Task<ProductReview?> GetByIdAsync(int id)
        {
            return await _context.ProductReviews.FindAsync(id);
        }

        public async Task<List<ProductReview>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductReviews.Where(r => r.ProductId == productId).ToListAsync();
        }

        public async Task<List<ProductReview>> GetByUserIdAsync(int userId)
        {
            return await _context.ProductReviews.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<ProductReview> CreateAsync(ProductReview review)
        {
            _context.ProductReviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> UpdateAsync(ProductReview review)
        {
            var existing = await _context.ProductReviews.FindAsync(review.Id);
            if (existing != null)
            {
                existing.Update(review.Rating, review.Comment);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var review = await _context.ProductReviews.FirstOrDefaultAsync(r => r.Id == id);
            if (review != null)
            {
                review.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}