using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs._ِAbout;
using TourSite.Core.DTOs.Home;
using TourSite.Core.DTOs.SocialElements;
using TourSite.Core.Servicies.Contract;
using TourSite.Repository.Data.Contexts;
using TourSite.Service.Services.Homes;

namespace TourSite.APIs.Controllers
{

    public class SocailElememtsController : BaseApiController
    {
        private readonly ISocialElemtsService socialElemtsService   ;
        private readonly BbeSiteDbContext _context;
        public IWebHostEnvironment _env { get; }
        public SocailElememtsController(ISocialElemtsService _socialElemtsService, BbeSiteDbContext context, IWebHostEnvironment env)
        {
            socialElemtsService = _socialElemtsService;
            _context = context;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> GetFAQByLang()
        {
            var socialDto = await _context.SocialElements
             .Select(a => new SocailElementsDto
             {
                 Id = a.Id,
                 Email = a.Email,
                 Logo = a.Logo,
                 FacebookUrl = a.FacebookUrl,
                 InstagramUrl = a.InstagramUrl


             })
             .FirstOrDefaultAsync();

            if (socialDto == null)
                return NotFound();

            return Ok(socialDto);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdataCategoryTour([FromForm] SocialElementsUpdate dto)
        {
            if (dto == null)
            {
                return BadRequest(new APIErrerResponse(400, "Empty data"));
            }
            var result = await socialElemtsService.UpdateSocailEleemts(dto);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no SocialElements with this Id : "));
            }
            return Ok(new { message = "SocialElements  updated successfully" });
        }

    }
}
