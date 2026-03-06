using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs.Career;
using TourSite.Core.DTOs.ServiceCore;
using TourSite.Core.DTOs.ServicesFeature;
using TourSite.Core.Servicies.Contract;
using TourSite.Repository.Data.Contexts;
using TourSite.Service.Services.ServiceCores;

namespace TourSite.APIs.Controllers
{
    
    public class ServiceFeatureController : BaseApiController
    {
        private readonly IServiceFeatureService _serviceFeatureService;

        private readonly BbeSiteDbContext _context;
        public IWebHostEnvironment _env { get; }
        public ServiceFeatureController(IServiceFeatureService serviceFeatureService, BbeSiteDbContext context, IWebHostEnvironment env)
        {
            _serviceFeatureService = serviceFeatureService;
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAService([FromQuery] string? lang)
        {
            var aboutTeam = await _serviceFeatureService.GetServiceCoreAsync(lang);
            if (aboutTeam == null)
            {
                return NotFound();
            }
            return Ok(aboutTeam);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateService([FromForm] ServicesFeatureUdateDto dto, int id)
        {
            var result = await _serviceFeatureService.UpdateServiceFeatureAsync(dto, id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no ServiceFeature with this Id : {id}"));
            }
            return Ok(new { message = "ServiceFeature updated successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromForm] ServicesFeatureUdateDto dto)
        {
            await _serviceFeatureService.CreateServiceFeatureAsync(dto);

            return Ok(new { message = "ServiceFeature updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var result = await _serviceFeatureService.DeleteaServiceFeatureAsync(id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no ServiceFeature with this Id : {id}"));
            }
            return Ok(new { message = "ServiceFeature deleted successfully" });
        }

        [HttpGet("GetAll/{id}")]
        public async Task<IActionResult> GetAllService(int id)
        {

            var servicedto = await _context.ServiceFeatures
                .Include(t => t.ServiceFeaturesTranslations)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (servicedto == null)
                return NotFound();


            var dto = new ServicesFeatureDto
            {
                ImageCover = servicedto.ImageCover,
                servicesFeatureTranslationDtos = servicedto.ServiceFeaturesTranslations?
        .Select(t => new ServicesFeatureTranslationDto
        {
            Language = t.Language,
            Title = t.Title,
            Description = t.Description,
            IncludeFirst = t.IncludeFirst,
            IncludeSecond = t.IncludeSecond
        }).ToList() ?? new()
            };

            return Ok(dto);

        }
    }
}
