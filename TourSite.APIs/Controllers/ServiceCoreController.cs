using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs.Career;
using TourSite.Core.DTOs.ServiceCore;
using TourSite.Core.DTOs.ServicesFeature;
using TourSite.Core.Servicies.Contract;
using TourSite.Repository.Data.Contexts;
using TourSite.Service.Services.Careers;

namespace TourSite.APIs.Controllers
{
 
    public class ServiceCoreController : BaseApiController
    {
        private readonly IServiceCoreService _serviceCoreService;
        private readonly BbeSiteDbContext _context;
        public IWebHostEnvironment _env { get; }
        public ServiceCoreController(IServiceCoreService serviceCoreService, BbeSiteDbContext context, IWebHostEnvironment env)
        {
            _serviceCoreService = serviceCoreService;
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAboutTeamAsync([FromQuery] string? lang)
        {
            var aboutTeam = await _serviceCoreService.GetServiceCoreAsync(lang);
            if (aboutTeam == null)
            {
                return NotFound();
            }
            return Ok(aboutTeam);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAboutTeam([FromForm] ServiceCoreUpdateDto dto, int id)
        {
            var result = await _serviceCoreService.UpdateServiceCoreAsync(dto, id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Career with this Id : {id}"));
            }
            return Ok(new { message = "ServiceCore updated successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAboutTeam([FromForm] ServiceCoreUpdateDto dto)
        {
            await _serviceCoreService.CreateServceCoreAsync(dto);

            return Ok(new { message = "ServiceCore updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAboutTeam(int id)
        {
            var result = await _serviceCoreService.DeleteaServiceCoreAsync(id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no ServiceCore with this Id : {id}"));
            }
            return Ok(new { message = "ServiceCore deleted successfully" });
        }

        [HttpGet("GetAll/{id}")]
        public async Task<IActionResult> GetAllService(int id)
        {

            var servicedto = await _context.ServiceCores
                .Include(t => t.ServiceCoreTranslations)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (servicedto == null)
                return NotFound();


            var dto = new ServiceCoreDto
            {
                ImageCover = servicedto.ImageCover,
                serviceCoreTranlationDtos = servicedto.ServiceCoreTranslations?
        .Select(t => new ServiceCoreTranlationDto
        {
            Language = t.Language,
            Title = t.Title,
            Description = t.Description,

        }).ToList() ?? new()
            };

            return Ok(dto);

        }
    }
}
