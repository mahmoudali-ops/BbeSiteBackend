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
using TourSite.Core.DTOs.Contact;
using TourSite.Core.DTOs.Home;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.Homes
{
    public class HomeService : IHomeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IConfiguration configuration;

        public IWebHostEnvironment env { get; }


        public HomeService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
            configuration = _configuration;
        }
        public async Task<HomeDto> GetHomeAsync(string? lang = "en")
        {
            lang = string.IsNullOrWhiteSpace(lang) ? "en" : lang.ToLower();

            var home = await unitOfWork.Repository<Home>()
                .Query()
                .AsNoTracking()
                .Include(f => f.HomeTranslation
                    .Where(t => t.Language == lang))
                .OrderBy(h => h.Id)
                .FirstOrDefaultAsync();

            return home is null ? null : mapper.Map<HomeDto>(home);
        }

        public async Task<bool> UpdateHome(HomeUpdateDto dto)
        {

            // ✅ جلب FAQ مع الترجمات (Tracking)
            var home = await unitOfWork.Repository<Home>()
                .Query()
                .Include(f => f.HomeTranslation)
                .FirstOrDefaultAsync();

            if (home == null)
                return false;

            // ✅ فك JSON الخاص بالترجمات (لو موجود)
            if (!string.IsNullOrEmpty(dto.TranslationsJson))
            {
                dto.homeTranslationDtos = JsonSerializer.Deserialize<List<HomeTranslationDto>>(
                    dto.TranslationsJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                ) ?? new List<HomeTranslationDto>();
            }

            // ✅ تحديث الصورة (لو تم رفع صورة جديدة)
            if (dto.MainCoverImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/home");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var image = await Image.LoadAsync(dto.MainCoverImageFile.OpenReadStream()))
                {
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max, // يحافظ على النسبة
                        Size = new Size(800, 1200) // الحجم الأصلي مناسب
                    }));
                    await image.SaveAsync(fullPath, new WebpEncoder
                    {
                        Quality = 80
                    });
                }

                home.MainCover = $"images/home/{fileName}";
            }

            if (dto.TeamImageImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/home");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var image = await Image.LoadAsync(dto.TeamImageImageFile.OpenReadStream()))
                {
                    // Resize مع الحفاظ على النسبة
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max, // يحافظ على النسبة، لا يقطع الصورة
                        Size = new Size(1200, 1200) // أقصى عرض وارتفاع
                    }));

                    await image.SaveAsync(fullPath, new WebpEncoder
                    {
                        Quality = 80
                    });
                }


                home.TeamImage = $"images/home/{fileName}";
            }

            if (dto.HelpImageImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/home");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var image = await Image.LoadAsync(dto.HelpImageImageFile.OpenReadStream()))
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


                home.HelpImage = $"images/home/{fileName}";
            }

            if (dto.MultiLangImageImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/home");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var image = await Image.LoadAsync(dto.MultiLangImageImageFile.OpenReadStream()))
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


                home.MultiLangImage = $"images/home/{fileName}";
            }





            // ✅ تحديث / إضافة الترجمات حسب اللغة
            foreach (var translationDto in dto.homeTranslationDtos)
            {
                var existingTranslation = home.HomeTranslation
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
                    home.HomeTranslation.Add(new HomeTranslation
                    {
                        HomeId = home.Id,
                        Language = translationDto.Language.ToLower(),
                        Title = translationDto.Title,
                        Description = translationDto.Description

                    });
                }
            }

            // ✅ تحديث الكيان وحفظ التغييرات
            unitOfWork.Repository<Home>().Update(home);
            await unitOfWork.CompleteAsync();

            return true;
        }
    }
}
