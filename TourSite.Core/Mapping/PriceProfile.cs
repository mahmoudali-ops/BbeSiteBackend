using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Career;
using TourSite.Core.DTOs.Price;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class PriceProfile : Profile
    {
        public PriceProfile(IConfiguration configuration)
        {
            CreateMap<Price, PriceDto>()
                .ForMember(dest => dest.ImageCover, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.ImageCover)
                ).
                ForMember(
                    dest => dest.priceTranlationDtos,
                    opt => opt.MapFrom(
                        src => src.priceCardTranslations
                    )
                );

            CreateMap<PriceCardTranslation, PriceTranlationDto>();
        }
    }
}
