using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs._ِAbout;
using TourSite.Core.DTOs.AboutTeam;
using TourSite.Core.DTOs.Career;
using TourSite.Core.Servicies.Contract;
using TourSite.Repository.Data.Contexts;

namespace TourSite.APIs.Controllers
{
  
    public class CareerController : BaseApiController
    {
        private readonly ICareerService _CareerService;

        private readonly BbeSiteDbContext _context;
        public IWebHostEnvironment _env { get; }
        public CareerController(ICareerService aboutTeamService, BbeSiteDbContext context, IWebHostEnvironment env)
        {
            _CareerService = aboutTeamService;
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetCareer([FromQuery] string? lang)
        {
            var aboutTeam = await _CareerService.GetaCareerAsync(lang);
            if (aboutTeam == null)
            {
                return NotFound();
            }
            return Ok(aboutTeam);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCareer([FromForm] CareerUpdateDto dto, int id)
        {
            var result = await _CareerService.UpdateCareerAsync(dto, id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Career with this Id : {id}"));
            }
            return Ok(new { message = "Career updated successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCareer([FromForm] CareerUpdateDto dto)
        {
            await _CareerService.CreateCreereAsync(dto);

            return Ok(new { message = "Career updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCareer(int id)
        {
            var result = await _CareerService.DeleteaboutTeamAsync(id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Career with this Id : {id}"));
            }
            return Ok(new { message = "Career deleted successfully" });
        }

        [HttpGet("GetAll/{id}")]
        public async Task<IActionResult> GetAllCareer(int id)
        {

            var carrerdto = await _context.Careers
                .Include(t => t.careerCardTranslations)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (carrerdto == null)
                return NotFound();


            var dto = new CareerDto
            {
                ImageCover = carrerdto.ImageCover,


                careerCardTranslationsDto = carrerdto.careerCardTranslations?
        .Select(t => new CareerTranslationDto
        {
            Language = t.Language,
            EmploymentType = t.EmploymentType,
            SalaryFrom = t.SalaryFrom,
            SalaryTo = t.SalaryTo,
            JobTitle = t.JobTitle,
            SalaryPeriod = t.SalaryPeriod,
            Description = t.Description,
             CreatedAt = t.CreatedAt,
             UpdatedAt = t.UpdatedAt



        }).ToList() ?? new()
            };

            return Ok(dto);

        }
    }
    
}
