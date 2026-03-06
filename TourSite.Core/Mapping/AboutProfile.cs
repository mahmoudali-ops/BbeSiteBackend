using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs._ِAbout;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class AboutProfile : Profile
    {
        public AboutProfile(IConfiguration configuration)
        {
            CreateMap<About, AboutDto>()
                .ForMember(dest => dest.ImageCover, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.ImageCover)
                ).
                ForMember(
                    dest => dest.aboutTranslationDtos,
                    opt => opt.MapFrom(
                        src => src.AboutTranslations
                    )
                );

            CreateMap<AboutTranslation, AboutTranslationDto>();
        }
    }
}
