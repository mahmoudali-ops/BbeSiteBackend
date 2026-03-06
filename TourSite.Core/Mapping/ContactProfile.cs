using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Career;
using TourSite.Core.DTOs.Contact;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class ContactProfile : Profile
    {
        public ContactProfile(IConfiguration configuration)
        {
            CreateMap<Contact, ContactDto>()
                .ForMember(dest => dest.ImageCover, opt => opt.MapFrom(src => configuration["BaseUrl"] + src.ImageCover)
                ).
                ForMember(
                    dest => dest.contactTranlationDtos,
                    opt => opt.MapFrom(
                        src => src.contactTranslation
                    )
                );

            CreateMap<ContactTranslation, ContactTranlationDto>();
        }
    }
}
