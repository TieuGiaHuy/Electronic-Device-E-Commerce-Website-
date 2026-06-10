using BLL.DTOs.Promotion;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PromotionService(IPromotionRepository promotionRepository, IUnitOfWork unitOfWork)
        {
            _promotionRepository = promotionRepository;
            _unitOfWork = unitOfWork;
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
                UpdatedAt = p.UpdatedAt,
                IsDisabled = p.IsDisabled
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
                UpdatedAt = p.UpdatedAt,
                IsDisabled = p.IsDisabled
            };
        }

        public async Task<PromotionDto> CreatePromotionAsync(PromotionCreateDto promotion)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = Promotion.Create(
                    promotion.ProductVariantId,
                    promotion.SalePrice,
                    promotion.MaxReservedStock,
                    promotion.StartAt,
                    promotion.EndAt
                );

                var productPrice = await _unitOfWork.Repository<ProductVariant>().Query().Where(pv => pv.ProductVariantId == promotion.ProductVariantId)
                    .Select(pv => pv.Price).FirstOrDefaultAsync();
                if (entity.SalePrice >= productPrice)
                {
                    throw new InvalidOperationException("Sale price must be less than the product price.");
                }

                var created = await _promotionRepository.CreatePromotionAsync(entity);

                await _unitOfWork.CommitAsync();
                return new PromotionDto
                {
                    PromotionId = created.PromotionId,
                    ProductVariantId = created.ProductVariantId,
                    SalePrice = created.SalePrice,
                    MaxReservedStock = created.MaxReservedStock,
                    StartAt = created.StartAt,
                    EndAt = created.EndAt,
                    CreatedAt = created.CreatedAt,
                    UpdatedAt = created.UpdatedAt,
                    IsDisabled = created.IsDisabled
                };
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
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
                EndAt = promotion.EndAt,
                IsDisabled = promotion.IsDisabled
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
                UpdatedAt = p.UpdatedAt,
                IsDisabled = p.IsDisabled
            }).ToList();
        }

        public async Task<bool> SetPromotionDisabledAsync(int promotionId, bool isDisabled)
        {
            var p = await _promotionRepository.GetPromotionByIdAsync(promotionId);
            if (p == null) return false;
            p.IsDisabled = isDisabled;
            return await _promotionRepository.UpdatePromotionAsync(p);
        }
    }
}