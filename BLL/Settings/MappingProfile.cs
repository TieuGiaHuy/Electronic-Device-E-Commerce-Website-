using AutoMapper;
using BLL.DTOs.CartItem;
using BLL.DTOs.Product.Customer;
using BLL.DTOs.ProductVariant;
using BLL.DTOs.ProductVariant.Customer;
using BLL.DTOs.ProductVariant.Staff;
using BLL.DTOs.Promotion;
using DAL.Models;

namespace BLL.Settings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Promotion, PromotionDto>().ReverseMap();
            CreateMap<VariantAttribute, VariantAttributeDto>();
            CreateMap<CartItem, CartItemDTO>();
            CreateMap<ProductVariant, CartItemProductVariantDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.ProductName))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Product!.Brand))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes.OrderBy(a => a.DisplayOrder)));

            CreateMap<ProductVariant, ProductVariantStaffDto>()
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes.OrderBy(a => a.DisplayOrder)));
            CreateMap<ProductVariant, ProductVariantStaffCreateDto>()
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes.OrderBy(a => a.DisplayOrder)));
            CreateMap<ProductVariant, ProductVariantCustomerDto>()
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes.OrderBy(a => a.DisplayOrder)));

            CreateMap<Product, ProductCustomerDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
                .ForMember(dest => dest.ProductVariants, opt => opt.MapFrom(src => src.ProductVariants));
        }
    }
}
