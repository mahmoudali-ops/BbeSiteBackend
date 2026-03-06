using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs._ِAbout;
using TourSite.Core.DTOs.AboutTeam;

namespace TourSite.Core.Servicies.Contract
{
    public interface IAboutTeamService
    {
        Task CreateAboutTeam(AboutTeamUpdateDto dto);
        Task<List<AboutTeamDto>> GetaboutTeamAsync(string? lang = "en");
        Task<Boolean> UpdateAboutTeam(AboutTeamUpdateDto dto, int id);
        Task<Boolean> DeleteaboutTeamAsync(int id);
    }
}
