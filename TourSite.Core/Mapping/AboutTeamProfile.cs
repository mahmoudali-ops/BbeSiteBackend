using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs._ِAbout;
using TourSite.Core.DTOs.AboutTeam;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class AboutTeamProfile : Profile
    {
        public AboutTeamProfile(IConfiguration configuration)
        {
            CreateMap<AboutTeam, AboutTeamDto>()
                .ForMember(dest => dest.ImageCover, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.ImageCover)
                ).
                ForMember(
                    dest => dest.aboutTeamTranlationDtos,
                    opt => opt.MapFrom(
                        src => src.AboutTeamTranslations
                    )
                );

            CreateMap<AboutTeamTranslation, AboutTeamTranlationDto>();
        }
    }
}
