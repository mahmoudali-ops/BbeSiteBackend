using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs.Career;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.FAQ;
using TourSite.Core.Servicies.Contract;
using TourSite.Repository.Data.Contexts;

namespace TourSite.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : BaseApiController
    {
        private readonly IFAQsService fAQsService;

        private readonly BbeSiteDbContext _context;
        public IWebHostEnvironment _env { get; }
        public FAQController(IFAQsService _fAQsService, BbeSiteDbContext context, IWebHostEnvironment env)
        {
            fAQsService = _fAQsService;
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetFAQByLang([FromQuery] string? lang)
        {
            var faq = await fAQsService.GetFAQAsync(lang);
            return Ok(faq);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateFaq([FromForm] FAQsUdateDTO dto, int id)
        {
            if (id <= 0) return BadRequest(new APIErrerResponse(400, "Id required .. can not be less than or equal 0"));
            var result = await fAQsService.UpdateFAQ(dto, id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no faq with this Id : {id}"));
            }
            return Ok(new { message = "FAQ  updated successfully" });
        }


        [HttpPost]
        public async Task<IActionResult> CreateFaq([FromForm] FAQsUdateDTO dto)
        {
            await fAQsService.CreateFAQAsync(dto);

            return Ok(new { message = "Price updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFaq(int id)
        {
            var result = await fAQsService.DeleteFAQAsync(id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no price with this Id : {id}"));
            }
            return Ok(new { message = "Price deleted successfully" });
        }


        [HttpGet("GetAll/{id}")]
        public async Task<IActionResult> GetAllFaq(int id)
        {

            var faqdto = await _context.FAQs
                .Include(t => t.fAQsTranslations)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (faqdto == null)
                return NotFound();


            var dto = new FAQsDto
            {
                ImageCover = faqdto.ImageCover,
                fAQsTranslationDTos = faqdto.fAQsTranslations?
        .Select(t => new FAQsTranslationDTo
        {
            Language = t.Language,
            Answer = t.Answer,
            Question = t.Question,
        }).ToList() ?? new()
            };

            return Ok(dto);

        }


    }

}
