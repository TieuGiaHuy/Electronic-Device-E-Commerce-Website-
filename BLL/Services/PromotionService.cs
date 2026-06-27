using AutoMapper;
using BLL.DTOs.Pagination;
using BLL.DTOs.Promotion;
using BLL.DTOs.Promotion.PromotionList;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PromotionService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PromotionListResponeDto> GetPromotionsListAsync(PromotionListRequest request)
        {
            // Query
            var promotionsQuery = _unitOfWork.Repository<Promotion>().Query();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                promotionsQuery = promotionsQuery.Where(p => p.Name.Contains(request.SearchTerm));
            }

            // Pagination
            var totalItems = await promotionsQuery.CountAsync();

            var activeCount = await promotionsQuery
                .Where(p => !p.IsDeleted && !p.IsDisabled)
                .Where(p =>
                // Stock
                    (!p.IsLimitedTime && p.IsReservedStock && p.MaxReservedStock > 0) ||
                    // Time
                    (p.IsLimitedTime && p.StartAt.HasValue && p.EndAt.HasValue && p.StartAt.Value <= DateTime.UtcNow && p.EndAt.Value >= DateTime.UtcNow && !p.IsReservedStock) ||
                    // Both
                    (p.IsLimitedTime && p.StartAt.HasValue && p.EndAt.HasValue && p.StartAt.Value <= DateTime.UtcNow && p.EndAt.Value >= DateTime.UtcNow && p.IsReservedStock && p.MaxReservedStock > 0)
                )
                .CountAsync();

            var upcomingCount = await promotionsQuery
                .Where(p => !p.IsDeleted && !p.IsDisabled)
                .Where(p =>
                    p.IsLimitedTime && p.StartAt.HasValue && p.EndAt.HasValue &&
                    p.StartAt.Value > DateTime.UtcNow
                )
                .CountAsync();

            var expiredOrDisabledCount = await promotionsQuery
                .Where(p =>
                    p.IsDisabled ||
                    p.IsDeleted ||
                    (p.IsLimitedTime && p.EndAt.HasValue && p.EndAt.Value < DateTime.UtcNow)
                )
                .CountAsync();

            var promotions = await promotionsQuery
                .Skip((request.Pagination.Page - 1) * request.Pagination.PageSize)
                .Take(request.Pagination.PageSize)
                .ToListAsync();
            var promotionDtos = _mapper.Map<List<PromotionDto>>(promotions);

            // Return response
            return new PromotionListResponeDto
            {
                ActiveCount = activeCount,
                Upcoming = upcomingCount,
                ExpiredOrDisabledCount = expiredOrDisabledCount,
                Pagination = new PaginatedResponse<PromotionDto>
                {
                    Items = promotionDtos,
                    TotalCount = totalItems,
                    Page = request.Pagination.Page,
                    PageSize = request.Pagination.PageSize
                }
            };
        }
    }
}