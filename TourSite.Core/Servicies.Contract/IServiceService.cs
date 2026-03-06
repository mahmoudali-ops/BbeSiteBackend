


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Services;

namespace TourSite.Core.Servicies.Contract
{
    public interface IServiceService
    {
        Task<ServiceDto> GetServiceAsync();
        Task<Boolean> UpdateService(ServiceUpateDto dto, int id);
    }
}
