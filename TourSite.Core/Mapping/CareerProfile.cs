using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.AboutTeam;
using TourSite.Core.DTOs.Career;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class CareerProfile : Profile
    {
        public CareerProfile(IConfiguration configuration)
        {
            CreateMap<Career, CareerDto>()
                .ForMember(dest => dest.ImageCover, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.ImageCover)
                ).
                ForMember(
                    dest => dest.careerCardTranslationsDto,
                    opt => opt.MapFrom(
                        src => src.careerCardTranslations
                    )
                );

            CreateMap<CareerCardTranslation, CareerTranslationDto>();
        }
    }
}
