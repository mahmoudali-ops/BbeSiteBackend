using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourSite.APIs.Errors;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Core.Specification.Users;

namespace TourSite.APIs.Controllers
{

    public class UserController : BaseApiController
    {
        private readonly IUserService usersService;
        public UserController(IUserService _toursService)
        {
            usersService = _toursService;
        }
        [HttpGet]
        //    [Authorize]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserSpeciParams userSpecParams)
        {
            var users = await usersService.GetAllUserAsync(userSpecParams);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (id == null) return BadRequest(new APIErrerResponse(400, "Id required .. can not be null"));

            var user = await usersService.GetUserByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound(new APIErrerResponse(404, $"There is no User with this Id : {id}"));
            }
            return Ok(user);
        }

        [HttpGet("categories/images")]
        public IActionResult GetAllCategoryImages([FromQuery] string folder)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", $"{folder}");

            if (!Directory.Exists(folderPath))
                return NotFound("Folder not found.");

            var files = Directory.GetFiles(folderPath)
                                 .Select(f => new
                                 {
                                     FileName = Path.GetFileName(f),
                                     Url = $"/images/categories/{Path.GetFileName(f)}"
                                 });

            return Ok(files);
        }
        [HttpGet("images/all")]
        public IActionResult GetAllImages()
        {
            var rootFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(rootFolder))
                return NotFound("Images root folder not found.");

            // قراءة كل الصور من كل المجلدات الفرعية
            var files = Directory.GetDirectories(rootFolder)
                                 .SelectMany(dir => Directory.GetFiles(dir)
                                     .Select(file => new
                                     {
                                         FileName = Path.GetFileName(file),
                                         // URL مع اسم المجلد
                                         Url = $"/images/{Path.GetFileName(Path.GetDirectoryName(file))}/{Path.GetFileName(file)}",
                                         Folder = Path.GetFileName(Path.GetDirectoryName(file))
                                     })
                                 );

            return Ok(files);
        }



    }
}
