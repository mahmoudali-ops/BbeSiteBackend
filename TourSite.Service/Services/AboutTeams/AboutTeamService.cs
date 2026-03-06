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
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TourSite.Core;
using TourSite.Core.DTOs._ِAbout;
using TourSite.Core.DTOs.AboutTeam;
using TourSite.Core.Entities;
using TourSite.Core.Helper;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.AboutTeams
{
    public class AboutTeamService : IAboutTeamService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IConfiguration configuration;

        public IWebHostEnvironment env { get; }


        public AboutTeamService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
            configuration = _configuration;
        }

     
        public async Task<List<AboutTeamDto>> GetaboutTeamAsync(string? lang = "en")
        {

            lang ??= "en";
            lang = lang.ToLower();

            var aboutdto = await unitOfWork.Repository<AboutTeam>()
                .Query()
                .Include(t => t.AboutTeamTranslations)
                .Select(t => new AboutTeamDto
                {
                    Id = t.Id,
                    ImageCover = configuration["BaseUrl"] + t.ImageCover,
                    aboutTeamTranlationDtos = t.AboutTeamTranslations
                        .Where(tr => tr.Language.ToLower() == lang)
                        .Select(tr => new AboutTeamTranlationDto
                        {
                            Id = tr.Id,
                            Language = tr.Language,
                            Name = tr.Name,
                            Position = tr.Position,
                            Description = tr.Description
                        })
                        .ToList()
                })
                .ToListAsync();


            return aboutdto;
        }

        public async Task CreateAboutTeam(AboutTeamUpdateDto dto)
        {
            if (!string.IsNullOrEmpty(dto.TranslationsJson))
            {
                dto.aboutTeamTranlationDtos = JsonSerializer.Deserialize<List<AboutTeamTranlationDto>>(dto.TranslationsJson);
            }

            // 🖼️ حفظ الصورة في wwwroot/images/categoryTours
            string imagePath = string.Empty;
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/aboutTeam");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                // فتح الصورة باستخدام ImageSharp
                using (var image = await Image.LoadAsync(dto.ImageFile.OpenReadStream()))
                {
                    // تغيير الأبعاد
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max, // يحافظ على النسبة
                        Size = new Size(800, 1000) // نقلل الطول شوية
                    }));
                    // حفظ WebP بجودة ممتازة وحجم صغير جدًا
                    await image.SaveAsync(fullPath, new WebpEncoder()
                    {
                        Quality = 80
                    });
                }

                imagePath = $"images/aboutTeam/{fileName}";
            }



            // 🧩 إنشاء الكيان
            var abouteam = new AboutTeam
            {
                ImageCover = imagePath,
                AboutTeamTranslations = dto.aboutTeamTranlationDtos.Select(t => new AboutTeamTranslation
                {
                    Language = t.Language.ToLower(),
                    Name = t.Name,
                    Position = t.Position,
                    AboutTeamId = t.Id,
                    Description = t.Description
                }).ToList(),


            };

            await unitOfWork.Repository<AboutTeam>().AddAsync(abouteam);
            await unitOfWork.CompleteAsync();
        }

        public async Task<bool> UpdateAboutTeam(AboutTeamUpdateDto dto, int id)
        {
            // ✅ جلب FAQ مع الترجمات (Tracking)
            var aboutteam = await unitOfWork.Repository<AboutTeam>()
                .Query()
                .Include(f => f.AboutTeamTranslations)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (aboutteam == null)
                return false;

            // ✅ فك JSON الخاص بالترجمات (لو موجود)
            if (!string.IsNullOrEmpty(dto.TranslationsJson))
            {
                dto.aboutTeamTranlationDtos = JsonSerializer.Deserialize<List<AboutTeamTranlationDto>>(
                    dto.TranslationsJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                ) ?? new List<AboutTeamTranlationDto>();
            }

            // ✅ تحديث الصورة (لو تم رفع صورة جديدة)
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/aboutTeam");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var image = await Image.LoadAsync(dto.ImageFile.OpenReadStream()))
                {
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max, // يحافظ على النسبة
                        Size = new Size(800, 1000) // نقلل الطول شوية
                    }));
                    await image.SaveAsync(fullPath, new WebpEncoder
                    {
                        Quality = 80
                    });
                }

                aboutteam.ImageCover = $"images/aboutTeam/{fileName}";
            }

       
            // ✅ تحديث / إضافة الترجمات حسب اللغة
            foreach (var translationDto in dto.aboutTeamTranlationDtos)
            {
                var existingTranslation = aboutteam.AboutTeamTranslations
                    .FirstOrDefault(t =>
                        t.Language.ToLower() == translationDto.Language.ToLower());

                if (existingTranslation != null)
                {
                    // Update
                    existingTranslation.Description = translationDto.Description;
                    existingTranslation.Name = translationDto.Name;
                    existingTranslation.Position = translationDto.Position;

                    existingTranslation.Language = translationDto.Language.ToLower();
                }
                else
                {
                    // Add
                    aboutteam.AboutTeamTranslations.Add(new AboutTeamTranslation
                    {
                        AboutTeamId = aboutteam.Id,
                        Language = translationDto.Language.ToLower(),
                        Description = translationDto.Description,
                        Name = translationDto.Name,
                        Position = translationDto.Position

                    });
                }
            }

            // ✅ تحديث الكيان وحفظ التغييرات
            unitOfWork.Repository<AboutTeam>().Update(aboutteam);
            await unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> DeleteaboutTeamAsync(int id)
        {
            var aboutteam = await unitOfWork.Repository<AboutTeam>()
                .Query()
                .FirstOrDefaultAsync(f => f.Id == id);

            if (aboutteam == null)
                return false;
            unitOfWork.Repository<AboutTeam>().Delete(aboutteam);
            await unitOfWork.CompleteAsync();
            return true;

        }
    }
}
