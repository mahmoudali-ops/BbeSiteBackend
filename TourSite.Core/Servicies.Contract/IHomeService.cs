using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Contact;
using TourSite.Core.DTOs.Home;

namespace TourSite.Core.Servicies.Contract
{
    public interface IHomeService
    {
        Task<HomeDto> GetHomeAsync(string? lang = "en");
        Task<Boolean> UpdateHome(HomeUpdateDto dto);
    }
}
