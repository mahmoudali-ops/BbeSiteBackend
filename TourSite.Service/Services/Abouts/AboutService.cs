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
using TourSite.Core.DTOs._ِAbout;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.FAQ;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.Abouts
{
    public class AboutService : IAboutServicecs
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IConfiguration configuration;

        public IWebHostEnvironment env { get; }


        public AboutService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
            configuration = _configuration;
        }
        public async Task<AboutDto> GetboutAsync(string? lang = "en")
        {
            lang = string.IsNullOrWhiteSpace(lang) ? "en" : lang.ToLower();

            var about = await unitOfWork.Repository<About>()
                .Query()
                .AsNoTracking()
                .Include(f => f.AboutTranslations
                    .Where(t => t.Language == lang))
                .OrderBy(a => a.Id)
                .FirstOrDefaultAsync();

            return about is null ? null : mapper.Map<AboutDto>(about);
        }

        public async Task<bool> UpdateAbout(AboutUpdateDto dto)
        {
            // ✅ جلب FAQ مع الترجمات (Tracking)
            var about = await unitOfWork.Repository<About>()
                .Query()
                .Include(f => f.AboutTranslations)
                .FirstOrDefaultAsync();

            if (about == null)
                return false;

            // ✅ فك JSON الخاص بالترجمات (لو موجود)
            if (!string.IsNullOrEmpty(dto.TranslationsJson))
            {
                dto.aboutTranslationDtos = JsonSerializer.Deserialize<List<AboutTranslationDto>>(
                    dto.TranslationsJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                ) ?? new List<AboutTranslationDto>();
            }


            // ✅ تحديث الصورة (لو تم رفع صورة جديدة)
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/about");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var image = await Image.LoadAsync(dto.ImageFile.OpenReadStream()))
                {
                    // Resize مع الحفاظ على النسبة
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max, // يحافظ على النسبة، لا يقطع الصورة
                        Size = new Size(800, 1200) // أقصى عرض وارتفاع
                    }));

                    await image.SaveAsync(fullPath, new WebpEncoder
                    {
                        Quality = 80
                    });
                }



                about.ImageCover = $"images/about/{fileName}";
            }

            // ✅ تحديث البيانات الأساسية
            about.ReferneceName = dto.ReferneceName;
            about.MetaDescription = dto.MetaDescription;
            about.MetaKeyWords = dto.MetaKeyWords;

            // ✅ تحديث / إضافة الترجمات حسب اللغة
            foreach (var translationDto in dto.aboutTranslationDtos)
            {
                var existingTranslation = about.AboutTranslations
                    .FirstOrDefault(t =>
                        t.Language.ToLower() == translationDto.Language.ToLower());

                if (existingTranslation != null)
                {
                    // Update
                    existingTranslation.Title = translationDto.Title;
                    existingTranslation.Description = translationDto.Description;
                    existingTranslation.Language = translationDto.Language.ToLower();
                }
                else
                {
                    // Add
                    about.AboutTranslations.Add(new AboutTranslation
                    {
                        AboutId = about.Id,
                        Language = translationDto.Language.ToLower(),
                        Title = translationDto.Title,
                        Description = translationDto.Description
                    });
                }
            }

            // ✅ تحديث الكيان وحفظ التغييرات
            unitOfWork.Repository<About>().Update(about);
            await unitOfWork.CompleteAsync();

            return true;
        }


    }
}
