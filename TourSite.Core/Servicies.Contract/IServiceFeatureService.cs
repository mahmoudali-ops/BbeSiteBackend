using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.ServiceCore;
using TourSite.Core.DTOs.ServicesFeature;

namespace TourSite.Core.Servicies.Contract
{
    public interface IServiceFeatureService
    {
        Task CreateServiceFeatureAsync(ServicesFeatureUdateDto dto);
        Task<List<ServicesFeatureDto>> GetServiceCoreAsync(string? lang = "en");
        Task<Boolean> UpdateServiceFeatureAsync(ServicesFeatureUdateDto dto, int id);
        Task<Boolean> DeleteaServiceFeatureAsync(int id);
    }
}
