using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Career;
using TourSite.Core.DTOs.Price;

namespace TourSite.Core.Servicies.Contract
{
    public interface IPriceService
    {
        Task CreatePriceAsync(PriceUpdateDto dto);
        Task<List<PriceDto>> GetPriceAsync(string? lang = "en");
        Task<Boolean> UpdatePriceAsync(PriceUpdateDto dto, int id);
        Task<Boolean> DeletPriceAsync(int id);
    }
}
