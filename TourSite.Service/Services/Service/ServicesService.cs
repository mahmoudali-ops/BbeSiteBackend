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
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.FAQ;
using TourSite.Core.DTOs.Services;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.Service
{
    public class ServicesService : IServiceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IConfiguration configuration;

        public IWebHostEnvironment env { get; }
        public ServicesService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
            configuration = _configuration;
        }
        public async Task<ServiceDto> GetServiceAsync()
        {

            var faq = await unitOfWork.Repository<TourSite.Core.Entities.Service>()
                .Query()
                .AsNoTracking()
                .FirstOrDefaultAsync();


            return faq is null ? null : mapper.Map<ServiceDto>(faq);
        }

        public async Task<bool> UpdateService(ServiceUpateDto dto, int id)
        {

            // ✅ جلب FAQ مع الترجمات (Tracking)
            var faq = await unitOfWork.Repository<TourSite.Core.Entities.Service>()
                .Query()
                .FirstOrDefaultAsync(f => f.Id == id);

            if (faq == null)
                return false;

     

            // ✅ تحديث الصورة (لو تم رفع صورة جديدة)
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/service");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var image = await Image.LoadAsync(dto.ImageFile.OpenReadStream()))
                {
                    image.Mutate(x => x.Resize(1600, 900));

                    await image.SaveAsync(fullPath, new WebpEncoder
                    {
                        Quality = 80
                    });
                }

                faq.ImageCover = $"images/service/{fileName}";
            }

            // ✅ تحديث البيانات الأساسية
            faq.ReferneceName = dto.ReferneceName;
            faq.MetaDescription = dto.MetaDescription;
            faq.MetaKeyWords = dto.MetaKeyWords;

   

            // ✅ تحديث الكيان وحفظ التغييرات
            unitOfWork.Repository<TourSite.Core.Entities.Service>().Update(faq);
            await unitOfWork.CompleteAsync();

            return true;
        }
    }
}
