using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Contact;
using TourSite.Core.DTOs.Home;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class HomeProfile : Profile
    {
        public HomeProfile(IConfiguration configuration)
        {
            CreateMap<Home, HomeDto>()
                .ForMember(dest => dest.MainCover, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.MainCover)
               
                )
                .ForMember(dest => dest.MultiLangImage, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.MultiLangImage)
               
                )
                .ForMember(dest => dest.HelpImage, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.HelpImage)
               
                )
                .ForMember(dest => dest.TeamImage, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.TeamImage)
               
                ).
                ForMember(
                    dest => dest.homeTranslationDtos,
                    opt => opt.MapFrom(
                        src => src.HomeTranslation
                    )
                );

            CreateMap<HomeTranslation, HomeTranslationDto>();
        }

    }
}
