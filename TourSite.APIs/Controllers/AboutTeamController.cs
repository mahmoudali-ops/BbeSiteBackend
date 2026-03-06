using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs.AboutTeam;
using TourSite.Core.DTOs.Career;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Repository.Data.Contexts;

namespace TourSite.APIs.Controllers
{

    public class AboutTeamController : BaseApiController
    {
        private readonly IAboutTeamService _aboutTeamService;
        private readonly BbeSiteDbContext _context;
        public IWebHostEnvironment _env { get; }
        public AboutTeamController(IAboutTeamService aboutTeamService, BbeSiteDbContext context, IWebHostEnvironment env)
        {
            _aboutTeamService = aboutTeamService;
            _context = context;
            _env= env;
        }

        [HttpGet("GetAboutTeam")]
        public async Task<IActionResult> GetAboutTeamAsync([FromQuery] string? lang)
        {
            var aboutTeam = await _aboutTeamService.GetaboutTeamAsync(lang);
            if (aboutTeam == null)
            {
                return NotFound();
            }
            return Ok(aboutTeam);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAboutTeam([FromForm] AboutTeamUpdateDto dto, int id)
        {
            var result = await _aboutTeamService.UpdateAboutTeam(dto, id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no AboutTeam with this Id : {id}"));
            }
            return Ok(new { message = "AboutTeam updated successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAboutTeam([FromForm] AboutTeamUpdateDto dto)
        {
            await _aboutTeamService.CreateAboutTeam(dto);
        
            return Ok(new { message = "AboutTeam updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAboutTeam(int id)
        {
            var result = await _aboutTeamService.DeleteaboutTeamAsync(id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no AboutTeam with this Id : {id}"));
            }
            return Ok(new { message = "AboutTeam deleted successfully" });
        }


        [HttpGet("GetAll/{id}")]
        public async Task<IActionResult> GetAllAboutTeam(int id)
        {

            var aboutdto = await _context.AboutTeams
                .Include(t => t.AboutTeamTranslations)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (aboutdto == null)
                return NotFound();


            var dto = new AboutTeamDto
            {
                ImageCover = aboutdto.ImageCover,


                aboutTeamTranlationDtos = aboutdto.AboutTeamTranslations?
        .Select(t => new AboutTeamTranlationDto
        {
            Language = t.Language,            
            Description = t.Description,
            Name = t.Name,
            Position = t.Position
        }).ToList() ?? new()
            };

            return Ok(dto);

        }
    }
}
