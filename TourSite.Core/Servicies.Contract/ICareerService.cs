using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.AboutTeam;
using TourSite.Core.DTOs.Career;

namespace TourSite.Core.Servicies.Contract
{
    public interface ICareerService
    {
        Task CreateCreereAsync(CareerUpdateDto dto);
        Task<List<CareerDto>> GetaCareerAsync(string? lang = "en");
        Task<Boolean> UpdateCareerAsync(CareerUpdateDto dto, int id);
        Task<Boolean> DeleteaboutTeamAsync(int id);
    }
}
