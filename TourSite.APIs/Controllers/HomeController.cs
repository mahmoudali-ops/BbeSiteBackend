using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs._ِAbout;
using TourSite.Core.DTOs.FAQ;
using TourSite.Core.DTOs.Home;
using TourSite.Core.Servicies.Contract;
using TourSite.Repository.Data.Contexts;
using TourSite.Service.Services.FAQ;

namespace TourSite.APIs.Controllers
{
    public class HomeController : BaseApiController
    {
        private readonly IHomeService _homeService;
        private readonly BbeSiteDbContext _context;

        public HomeController(IHomeService homeService, BbeSiteDbContext context)
        {
            _homeService = homeService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetHomeData([FromQuery] string? lang)
        {
            var faq = await _homeService.GetHomeAsync(lang);
            return Ok(faq);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdataHome([FromForm] HomeUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new APIErrerResponse(400, "Empty data"));
            }
            var result = await _homeService.UpdateHome(dto);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no home with this Id : "));
            }
            return Ok(new { message = "Home  updated successfully" });
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetHome()
        {
            var homeDto = await _context.Homes
                .Select(a => new HomeDto
                {
                    Id = a.Id,
                    MainCover = a.MainCover,
                    MultiLangImage = a.MultiLangImage,
                    TeamImage = a.TeamImage,
                    HelpImage = a.HelpImage,


                    homeTranslationDtos = a.HomeTranslation
                        .Select(t => new HomeTranslationDto
                        {
                            Language = t.Language,
                            Title = t.Title,
                            Description = t.Description
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (homeDto == null)
                return NotFound();

            return Ok(homeDto);
        }



    }
}
