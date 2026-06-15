using BLL.DTOs.Promotion;

namespace BLL.Services.Interfaces
{
    public interface IPromotionService
    {
        Task<List<PromotionDto>> GetAllPromotionsAsync();
        Task<PromotionDto?> GetPromotionByIdAsync(Guid promotionId);
        Task<PromotionDto> CreatePromotionAsync(PromotionCreateDto promotion);
        Task<bool> UpdatePromotionAsync(PromotionDto promotion);
        Task<bool> DeletePromotionAsync(Guid promotionId);
        Task<List<PromotionDto>> GetActivePromotionsAsync();
        Task<bool> SetPromotionDisabledAsync(Guid promotionId, bool isDisabled);
    }
}