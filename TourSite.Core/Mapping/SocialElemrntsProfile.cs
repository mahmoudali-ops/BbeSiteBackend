using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs._ِAbout;
using TourSite.Core.DTOs.SocialElements;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class SocialElemrntsProfile : Profile
    {
        public SocialElemrntsProfile(IConfiguration configuration)
        {
            CreateMap<SocialElements, SocailElementsDto>()
                 .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.Logo)

                ) .ForMember(dest => dest.FacebookUrl, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.FacebookUrl)

                ) .ForMember(dest => dest.InstagramUrl, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.InstagramUrl)

                );

            
        }
    }
}
