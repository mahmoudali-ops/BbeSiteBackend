using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs._ِAbout;
using TourSite.Core.DTOs.Contact;
using TourSite.Core.DTOs.FAQ;
using TourSite.Core.Servicies.Contract;
using TourSite.Repository.Data.Contexts;
using TourSite.Service.Services.FAQ;

namespace TourSite.APIs.Controllers
{
    public class ContactController : BaseApiController
    { 
        private readonly IContactService contactService;
        private readonly BbeSiteDbContext _context;
        public IWebHostEnvironment _env { get; }

        public ContactController(IContactService _contactService, BbeSiteDbContext context, IWebHostEnvironment env)
        {
            contactService = _contactService;
            _context = context;
            _env = env;

        }

        [HttpGet("getcontact")]
        public async Task<IActionResult> GetContact([FromQuery] string? lang)
        {
            var faq = await contactService.GetContactAsync(lang);
            return Ok(faq);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdataCategoryTour([FromForm] ContactUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new APIErrerResponse(400, "Invalid data"));
            }
            var result = await contactService.UpdateCotact(dto);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Contact with this Id : "));
            }
            return Ok(new { message = "Contact  updated successfully" });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllContactData()
        {
            var contactDto = await _context.Contacts
                .Select(a => new ContactDto
                {
                    Id = a.Id,
                    ImageCover = a.ImageCover,
                    ReferneceName = a.ReferneceName,
                    MetaDescription = a.MetaDescription,
                    MetaKeyWords = a.MetaKeyWords,


                    contactTranlationDtos = a.contactTranslation
                        .Select(t => new ContactTranlationDto
                        {
                            Language = t.Language,
                            Title = t.Title,
                            Description = t.Description
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (contactDto == null)
                return NotFound();

            return Ok(contactDto);
        }
    }
}
