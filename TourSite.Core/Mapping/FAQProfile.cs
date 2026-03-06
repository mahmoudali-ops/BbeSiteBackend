using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.FAQ;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class FAQProfile : Profile
    {
        public FAQProfile(IConfiguration configuration)
        {
            CreateMap<FAQsTranslation, FAQsTranslationDTo>();
            // CreateMap<Source, Destination>();
            // Example:
            CreateMap<FAQs, FAQsDto>()
      .ForMember(
          dest => dest.fAQsTranslationDTos,
          opt => opt.MapFrom(
              src => src.fAQsTranslations
          )
      )
                .ForMember(dest => dest.ImageCover, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.ImageCover)
                );

        }

    }
}
