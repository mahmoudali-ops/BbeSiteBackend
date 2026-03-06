using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs._ِAbout;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.FAQ;

namespace TourSite.Core.Servicies.Contract
{
    public interface IAboutServicecs
    {
        Task<AboutDto> GetboutAsync(string? lang = "en");
        Task<Boolean> UpdateAbout(AboutUpdateDto dto);

    }
}
