using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Contact;
using TourSite.Core.DTOs.ServiceCore;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class ServiceCoreProfile:Profile
    {
        public ServiceCoreProfile(IConfiguration configuration)
        {
            CreateMap<ServiceCore, ServiceCoreDto>()
                .ForMember(dest => dest.ImageCover, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.ImageCover)
                ).
                ForMember(
                    dest => dest.serviceCoreTranlationDtos,
                    opt => opt.MapFrom(
                        src => src.ServiceCoreTranslations
                    )
                );

            CreateMap<ServiceCoreTranslation, ServiceCoreTranlationDto>();
        }
    }
}
