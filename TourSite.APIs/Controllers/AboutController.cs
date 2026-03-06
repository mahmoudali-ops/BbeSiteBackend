using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using System.Text.Json;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs._ِAbout;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Repository.Data.Contexts;

namespace TourSite.APIs.Controllers
{

    public class AboutController : BaseApiController
    {
        private readonly IAboutServicecs aboutService;
        private readonly BbeSiteDbContext _context;
        public IWebHostEnvironment _env { get; }


        public AboutController(IAboutServicecs _aboutService, BbeSiteDbContext context, IWebHostEnvironment env)
        {
            aboutService = _aboutService;
            _context = context;
            _env = env;
        }

        [HttpGet("GetAbout")]
        public async Task<IActionResult> GetAboutAsync([FromQuery] string? lang)
        {
            var about = await aboutService.GetboutAsync(lang);
            if (about == null)
            {
                return NotFound();
            }
            return Ok(about);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAbout([FromForm] AboutUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new APIErrerResponse(400, "Empty data"));
            }
            var result = await aboutService.UpdateAbout(dto);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAboutForUpdate()
        {
            var aboutDto = await _context.Abouts
                .Select(a => new AboutDto
                {
                    Id = a.Id,
                    ImageCover = a.ImageCover,
                    ReferneceName = a.ReferneceName,
                    MetaDescription = a.MetaDescription,
                    MetaKeyWords = a.MetaKeyWords,

                    aboutTranslationDtos = a.AboutTranslations
                        .Select(t => new AboutTranslationDto
                        {
                            Language = t.Language,
                            Title = t.Title,
                            Description = t.Description
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (aboutDto == null)
                return NotFound();

            return Ok(aboutDto);
        }


    }
}
