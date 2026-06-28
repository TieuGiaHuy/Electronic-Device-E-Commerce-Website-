using BLL.DTOs.Promotion;
using BLL.DTOs.Promotion.PromotionList;

namespace BLL.Services.Interfaces
{
    public interface IPromotionService
    {
        public Task<PromotionListResponeDto> GetPromotionsListAsync(PromotionListRequest request);
    }
}