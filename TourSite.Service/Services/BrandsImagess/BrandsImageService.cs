using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TourSite.Core;
using TourSite.Core.DTOs.AboutTeam;
using TourSite.Core.DTOs.BrandsImages;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.BrandsImagess
{
    public class BrandsImageService : IBrandImagesService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IConfiguration configuration;

        public IWebHostEnvironment env { get; }


        public BrandsImageService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
            configuration = _configuration;
        }
        public async Task CreateBrandImages(BrandsImagesCreateDto dto)
        {
          
            // 🖼️ حفظ الصورة في wwwroot/images/categoryTours
            string imagePath = string.Empty;
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/brandimages");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                // فتح الصورة باستخدام ImageSharp
                using (var image = await Image.LoadAsync(dto.ImageFile.OpenReadStream()))
                {
                    // تغيير الأبعاد
                    image.Mutate(x => x.Resize(1600, 900));

                    // حفظ WebP بجودة ممتازة وحجم صغير جدًا
                    await image.SaveAsync(fullPath, new WebpEncoder()
                    {
                        Quality = 80
                    });
                }

                imagePath = $"images/brandimages/{fileName}";
            }



            // 🧩 إنشاء الكيان
            var iamge = new BrandsImages
            {
                ImageUrl = imagePath,
                CreatedAt = dto.CreatedAt
            };

            await unitOfWork.Repository<BrandsImages>().AddAsync(iamge);
            await unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteBrandImage(int id)
        {
            var image = await unitOfWork.Repository<BrandsImages>()
                          .Query()
                          .FirstOrDefaultAsync(f => f.Id == id);

            if (image == null)
                return false;
            unitOfWork.Repository<BrandsImages>().Delete(image);
            await unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<List<BrandsImagesDto>> GetbrandImagesAsync(string? lang = "en")
        {


            lang ??= "en";
            lang = lang.ToLower();

            var images = await unitOfWork.Repository<BrandsImages>()
                .Query()
                .Select(t => new BrandsImagesDto
                {
                    Id = t.Id,
                    ImageUrl = configuration["BaseUrl"] + t.ImageUrl,
                    CreatedAt = t.CreatedAt
                })
                .ToListAsync();
            return images;
        }

 
    }
}
