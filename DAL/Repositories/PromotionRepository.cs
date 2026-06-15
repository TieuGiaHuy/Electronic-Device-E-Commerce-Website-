using DAL.Data;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly AppDbContext _context;

        public PromotionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Promotion>> GetAllPromotionsAsync()
        {
            return await _context.Set<Promotion>()
                .Include(p => p.ProductVariant)
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<Promotion?> GetPromotionByIdAsync(Guid promotionId)
        {
            return await _context.Set<Promotion>().FindAsync(promotionId);
        }

        public async Task<Promotion> CreatePromotionAsync(Promotion promotion)
        {
            _context.Set<Promotion>().Add(promotion);
            await _context.SaveChangesAsync();
            return promotion;
        }

        public async Task<bool> UpdatePromotionAsync(Promotion promotion)
        {
            var existing = await _context.Set<Promotion>().FindAsync(promotion.PromotionId);
            if (existing == null) return false;

            existing.Update(promotion.ProductVariantId, promotion.SalePrice, promotion.MaxReservedStock, promotion.StartAt, promotion.EndAt);
            _context.Set<Promotion>().Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePromotionAsync(Guid promotionId)
        {
            var existing = await _context.Set<Promotion>().FindAsync(promotionId);
            if (existing == null) return false;

            existing.Delete();
            _context.Set<Promotion>().Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Promotion>> GetActivePromotionsAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.Set<Promotion>()
                .Include(p => p.ProductVariant)
                .Where(p => !p.IsDeleted && p.StartAt <= now && p.EndAt >= now)
                .ToListAsync();
        }
    }
}