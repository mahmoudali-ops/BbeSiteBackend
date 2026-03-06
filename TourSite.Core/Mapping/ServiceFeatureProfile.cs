using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.ServiceCore;
using TourSite.Core.DTOs.ServicesFeature;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class ServiceFeatureProfile : Profile
    {
        public ServiceFeatureProfile(IConfiguration configuration)
        {
            CreateMap<ServiceFeatures, ServicesFeatureDto>()
                .ForMember(dest => dest.ImageCover, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.ImageCover)
                ).
                ForMember(
                    dest => dest.servicesFeatureTranslationDtos,
                    opt => opt.MapFrom(
                        src => src.ServiceFeaturesTranslations
                    )
                );

            CreateMap<ServiceCoreTranslation, ServiceCoreTranlationDto>();
        }
    }
}
