using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.Contact;
using TourSite.Core.DTOs.FAQ;

namespace TourSite.Core.Servicies.Contract
{
    public interface IContactService
    {
        Task<ContactDto> GetContactAsync(string? lang = "en");
        Task<Boolean> UpdateCotact(ContactUpdateDto dto);
    }
}
