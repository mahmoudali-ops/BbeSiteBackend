using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs.AboutTeam;
using TourSite.Core.DTOs.BrandsImages;
using TourSite.Core.Servicies.Contract;

namespace TourSite.APIs.Controllers
{
    public class BrandImagesController : BaseApiController
    {
        private readonly IBrandImagesService brandImages;

        public BrandImagesController(IBrandImagesService _brandImages)
        {
            brandImages = _brandImages;
        }

        [HttpGet]
        public async Task<IActionResult> GetAboutTeamAsync([FromQuery] string? lang)
        {
            var aboutTeam = await brandImages.GetbrandImagesAsync(lang);
            if (aboutTeam == null)
            {
                return NotFound();
            }
            return Ok(aboutTeam);
        }
        [HttpPost]
        public async Task<IActionResult> AddBrandImage([FromForm] BrandsImagesCreateDto dto)
        {
            await brandImages.CreateBrandImages(dto);

            return Ok(new { message = "brandImages updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAboutTeam(int id)
        {
            var result = await brandImages.DeleteBrandImage(id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no brandImages with this Id : {id}"));
            }
            return Ok(new { message = "brandImages deleted successfully" });
        }
    }
}
