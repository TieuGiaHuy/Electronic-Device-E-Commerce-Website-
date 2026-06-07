using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IPromotionRepository
    {
        Task<List<Promotion>> GetAllPromotionsAsync();
        Task<Promotion?> GetPromotionByIdAsync(int promotionId);
        Task<Promotion> CreatePromotionAsync(Promotion promotion);
        Task<bool> UpdatePromotionAsync(Promotion promotion);
        Task<bool> DeletePromotionAsync(int promotionId);
        Task<List<Promotion>> GetActivePromotionsAsync();
    }
}