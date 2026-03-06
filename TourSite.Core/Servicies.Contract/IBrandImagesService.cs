using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.AboutTeam;
using TourSite.Core.DTOs.BrandsImages;

namespace TourSite.Core.Servicies.Contract
{
    public interface IBrandImagesService
    {
        Task CreateBrandImages(BrandsImagesCreateDto dto);
        Task<List<BrandsImagesDto>> GetbrandImagesAsync(string? lang = "en");
        Task<Boolean> DeleteBrandImage(int id);
    }
}
