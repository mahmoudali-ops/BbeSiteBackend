using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs.FAQ;
using TourSite.Core.DTOs.Services;
using TourSite.Core.Servicies.Contract;
using TourSite.Service.Services.FAQ;

namespace TourSite.APIs.Controllers
{

    public class ServiceController : BaseApiController
    {
        private readonly IServiceService serviceService;
        public ServiceController(IServiceService _serviceService)
        {
            serviceService = _serviceService;
        }


        [HttpGet]
        public async Task<IActionResult> GetFAQByLang()
        {
            var faq = await serviceService.GetServiceAsync();
            return Ok(faq);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdataCategoryTour([FromForm] ServiceUpateDto dto, int id)
        {
            if (id <= 0) return BadRequest(new APIErrerResponse(400, "Id required .. can not be less than or equal 0"));
            var result = await serviceService.UpdateService(dto, id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Service with this Id : {id}"));
            }
            return Ok(new { message = "Service  updated successfully" });
        }
    }
}
