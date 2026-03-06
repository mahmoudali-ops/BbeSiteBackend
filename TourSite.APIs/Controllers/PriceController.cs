using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs.Career;
using TourSite.Core.DTOs.Price;
using TourSite.Core.Servicies.Contract;
using TourSite.Repository.Data.Contexts;
using TourSite.Service.Services.Careers;

namespace TourSite.APIs.Controllers
{

    public class PriceController : BaseApiController
    {
        private readonly IPriceService priceService;
        private readonly BbeSiteDbContext _context;
        public IWebHostEnvironment _env { get; }
        public PriceController(IPriceService _priceService, BbeSiteDbContext context, IWebHostEnvironment env)
        {
            priceService = _priceService;
            _context = context;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> GetPriceAsync([FromQuery] string? lang)
        {
            var aboutTeam = await priceService.GetPriceAsync(lang);
            if (aboutTeam == null)
            {
                return NotFound();
            }
            return Ok(aboutTeam);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAboutTeam([FromForm] PriceUpdateDto dto, int id)
        {
            var result = await priceService.UpdatePriceAsync(dto, id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Price with this Id : {id}"));
            }
            return Ok(new { message = "Price updated successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAboutTeam([FromForm] PriceUpdateDto dto)
        {
            await priceService.CreatePriceAsync(dto);

            return Ok(new { message = "Price updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAboutTeam(int id)
        {
            var result = await priceService.DeletPriceAsync(id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no price with this Id : {id}"));
            }
            return Ok(new { message = "Price deleted successfully" });
        }


        [HttpGet("GetAll/{id}")]
        public async Task<IActionResult> GetAllPrice(int id)
        {

            var pricedto = await _context.Prices
                .Include(t => t.priceCardTranslations)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (pricedto == null)
                return NotFound();


            var dto = new PriceDto
            {
                ImageCover = pricedto.ImageCover,


                priceTranlationDtos = pricedto.priceCardTranslations?
        .Select(t => new PriceTranlationDto
        {
            Language = t.Language,
            Title = t.Title,
            Description = t.Description,
            PriceService = t.PriceService,
            Discount = t.Discount,
            IncludeFirst=t.IncludeFirst,
            IncludeSecond=t.IncludeSecond,
            IncludeThird=t.IncludeThird,
            IncludeForth=t.IncludeForth,
        }).ToList() ?? new()
            };

            return Ok(dto);

        }
    }
}
