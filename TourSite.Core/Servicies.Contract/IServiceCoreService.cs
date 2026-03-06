using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Career;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.FAQ;
using TourSite.Core.DTOs.ServiceCore;
using TourSite.Core.DTOs.Services;

namespace TourSite.Core.Servicies.Contract
{
    public interface IServiceCoreService
    {
        Task CreateServceCoreAsync(ServiceCoreUpdateDto dto);
        Task<List<ServiceCoreDto>> GetServiceCoreAsync(string? lang = "en");
        Task<Boolean> UpdateServiceCoreAsync(ServiceCoreUpdateDto dto, int id);
        Task<Boolean> DeleteaServiceCoreAsync(int id);
    }
}
