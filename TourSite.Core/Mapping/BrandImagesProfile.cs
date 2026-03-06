using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.AboutTeam;
using TourSite.Core.DTOs.BrandsImages;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class BrandImagesProfile : Profile
    {
        public BrandImagesProfile(IConfiguration configuration)
        {
            CreateMap<BrandsImages, BrandsImagesDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.ImageUrl)
                );

        }
    }
}
