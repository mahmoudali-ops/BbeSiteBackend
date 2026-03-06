using Store.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.FAQ;
using TourSite.Core.Entities;
using TourSite.Core.Specification.CatgeoryTour;

namespace TourSite.Core.Servicies.Contract
{
    public interface IFAQsService
    {
        Task<Boolean> UpdateFAQ(FAQsUdateDTO dto, int id);
        Task CreateFAQAsync(FAQsUdateDTO dto);
        Task<List<FAQsDto>> GetFAQAsync(string? lang = "en");
        Task<Boolean> DeleteFAQAsync(int id);

    }

}
