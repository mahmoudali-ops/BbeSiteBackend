using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs._ِAbout;
using TourSite.Core.DTOs.Services;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile(IConfiguration configuration)
        {
            CreateMap<Service, ServiceDto>()
                .ForMember(dest => dest.ImageCover, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.ImageCover)
                );

        }
    }
}
