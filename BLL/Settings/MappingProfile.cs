using AutoMapper;
using BLL.DTOs.Promotion;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Settings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Promotion, PromotionDto>().ReverseMap();
        }
    }
}
