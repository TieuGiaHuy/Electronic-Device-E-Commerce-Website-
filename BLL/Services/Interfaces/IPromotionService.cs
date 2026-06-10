using BLL.DTOs.Promotion;

namespace BLL.Services.Interfaces
{
    public interface IPromotionService
    {
        Task<List<PromotionDto>> GetAllPromotionsAsync();
        Task<PromotionDto?> GetPromotionByIdAsync(int promotionId);
        Task<PromotionDto> CreatePromotionAsync(PromotionCreateDto promotion);
        Task<bool> UpdatePromotionAsync(PromotionDto promotion);
        Task<bool> DeletePromotionAsync(int promotionId);
        Task<List<PromotionDto>> GetActivePromotionsAsync();
        Task<bool> SetPromotionDisabledAsync(int promotionId, bool isDisabled);
    }
}