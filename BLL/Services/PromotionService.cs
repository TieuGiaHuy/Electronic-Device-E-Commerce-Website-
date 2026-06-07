using BLL.DTOs.Promotion;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BLL.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task<List<PromotionDto>> GetAllPromotionsAsync()
        {
            var items = await _promotionRepository.GetAllPromotionsAsync();
            return items.Select(p => new PromotionDto
            {
                PromotionId = p.PromotionId,
                ProductVariantId = p.ProductVariantId,
                SalePrice = p.SalePrice,
                MaxReservedStock = p.MaxReservedStock,
                StartAt = p.StartAt,
                EndAt = p.EndAt,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();
        }

        public async Task<PromotionDto?> GetPromotionByIdAsync(int promotionId)
        {
            var p = await _promotionRepository.GetPromotionByIdAsync(promotionId);
            if (p == null) return null;
            return new PromotionDto
            {
                PromotionId = p.PromotionId,
                ProductVariantId = p.ProductVariantId,
                SalePrice = p.SalePrice,
                MaxReservedStock = p.MaxReservedStock,
                StartAt = p.StartAt,
                EndAt = p.EndAt,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            };
        }

        public async Task<PromotionDto> CreatePromotionAsync(PromotionCreateDto promotion)
        {
            var entity = Promotion.Create(
                promotion.ProductVariantId,
                promotion.SalePrice,
                promotion.MaxReservedStock,
                promotion.StartAt,
                promotion.EndAt
            );

            var created = await _promotionRepository.CreatePromotionAsync(entity);

            return new PromotionDto
            {
                PromotionId = created.PromotionId,
                ProductVariantId = created.ProductVariantId,
                SalePrice = created.SalePrice,
                MaxReservedStock = created.MaxReservedStock,
                StartAt = created.StartAt,
                EndAt = created.EndAt,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<bool> UpdatePromotionAsync(PromotionDto promotion)
        {
            var entity = new Promotion
            {
                PromotionId = promotion.PromotionId,
                ProductVariantId = promotion.ProductVariantId,
                SalePrice = promotion.SalePrice,
                MaxReservedStock = promotion.MaxReservedStock,
                StartAt = promotion.StartAt,
                EndAt = promotion.EndAt
            };
            return await _promotionRepository.UpdatePromotionAsync(entity);
        }

        public async Task<bool> DeletePromotionAsync(int promotionId)
        {
            return await _promotionRepository.DeletePromotionAsync(promotionId);
        }

        public async Task<List<PromotionDto>> GetActivePromotionsAsync()
        {
            var items = await _promotionRepository.GetActivePromotionsAsync();
            return items.Select(p => new PromotionDto
            {
                PromotionId = p.PromotionId,
                ProductVariantId = p.ProductVariantId,
                SalePrice = p.SalePrice,
                MaxReservedStock = p.MaxReservedStock,
                StartAt = p.StartAt,
                EndAt = p.EndAt,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();
        }
    }
}