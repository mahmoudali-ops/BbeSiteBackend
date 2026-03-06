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

using TourSite.Core.DTOs.Career;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.Careers
{
    public class CareerService : ICareerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IConfiguration configuration;

        public IWebHostEnvironment env { get; }


        public CareerService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
            configuration = _configuration;
        }

        public async Task CreateCreereAsync(CareerUpdateDto dto)
        {

            if (!string.IsNullOrEmpty(dto.TranslationsJson))
            {
                dto.careerCardTranslations = JsonSerializer.Deserialize<List<CareerTranslationDto>>(dto.TranslationsJson);
            }



            // 🖼️ حفظ الصورة في wwwroot/images/categoryTours
            string imagePath = string.Empty;
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/career");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                // فتح الصورة باستخدام ImageSharp
                using (var image = await Image.LoadAsync(dto.ImageFile.OpenReadStream()))
                {
                    // تغيير الأبعاد
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max, // يحافظ على النسبة، لا يقطع الصورة
                        Size = new Size(1200, 900) // أقصى عرض وارتفاع مناسب للصورة الأفقية
                    }));
                    // حفظ WebP بجودة ممتازة وحجم صغير جدًا
                    await image.SaveAsync(fullPath, new WebpEncoder()
                    {
                        Quality = 80
                    });
                }

                imagePath = $"images/career/{fileName}";
            }



            // 🧩 إنشاء الكيان
            var career = new Career
            {
                ImageCover = imagePath,
                ReferneceName = Guid.NewGuid().ToString(), // يمكنك تعديل هذا حسب الحاجة
                careerCardTranslations = dto.careerCardTranslations.Select(t => new CareerCardTranslation
                {
                    Language = t.Language.ToLower(),
                    Description = t.Description,
                    SalaryPeriod = t.SalaryPeriod,
                    SalaryFrom = t.SalaryFrom,
                    SalaryTo = t.SalaryTo,
                    EmploymentType = t.EmploymentType,
                    JobTitle = t.JobTitle
                }).ToList(),


            };

            await unitOfWork.Repository<Career>().AddAsync(career);
            await unitOfWork.CompleteAsync();
        }

        public async Task<List<CareerDto>> GetaCareerAsync(string? lang = "en")
        {

            lang ??= "en";
            lang = lang.ToLower();

            var aboutdto = await unitOfWork.Repository<Career>()
                .Query()
                .Include(t => t.careerCardTranslations)
                .Select(t => new CareerDto
                {
                    Id = t.Id,
                    ImageCover = configuration["BaseUrl"] + t.ImageCover,
                    careerCardTranslationsDto = t.careerCardTranslations
                        .Where(tr => tr.Language.ToLower() == lang)
                        .Select(tr => new CareerTranslationDto
                        {
                           Id = tr.Id,
                            Language = tr.Language,
                            SalaryPeriod = tr.SalaryPeriod,
                            SalaryFrom = tr.SalaryFrom,
                            SalaryTo = tr.SalaryTo,
                            EmploymentType = tr.EmploymentType,
                            JobTitle = tr.JobTitle,
                            Description = tr.Description

                        })
                        .ToList()
                })
                .ToListAsync();


            return aboutdto;
        }

        public async Task<bool> UpdateCareerAsync(CareerUpdateDto dto, int id)
        {

            // ✅ جلب FAQ مع الترجمات (Tracking)
            var career = await unitOfWork.Repository<Career>()
                .Query()
                .Include(f => f.careerCardTranslations)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (career == null)
                return false;

            // ✅ فك JSON الخاص بالترجمات (لو موجود)
            if (!string.IsNullOrEmpty(dto.TranslationsJson))
            {
                dto.careerCardTranslations = JsonSerializer.Deserialize<List<CareerTranslationDto>>(
                    dto.TranslationsJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                ) ?? new List<CareerTranslationDto>();
            }

            // ✅ تحديث الصورة (لو تم رفع صورة جديدة)
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/career");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var image = await Image.LoadAsync(dto.ImageFile.OpenReadStream()))
                {
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max, // يحافظ على النسبة، لا يقطع الصورة
                        Size = new Size(1200, 900) // أقصى عرض وارتفاع مناسب للصورة الأفقية
                    }));
                    await image.SaveAsync(fullPath, new WebpEncoder
                    {
                        Quality = 80
                    });
                }

                career.ImageCover = $"images/career/{fileName}";
            }


            // ✅ تحديث / إضافة الترجمات حسب اللغة
            foreach (var translationDto in dto.careerCardTranslations)
            {
                var existingTranslation = career.careerCardTranslations
                    .FirstOrDefault(t =>
                        t.Language.ToLower() == translationDto.Language.ToLower());

                if (existingTranslation != null)
                {
                    // Update
                    existingTranslation.Description = translationDto.Description;
                    existingTranslation.JobTitle = translationDto.JobTitle;
                    existingTranslation.EmploymentType = translationDto.EmploymentType;
                    existingTranslation.SalaryFrom = translationDto.SalaryFrom;
                    existingTranslation.SalaryTo = translationDto.SalaryTo;
                    existingTranslation.SalaryPeriod = translationDto.SalaryPeriod;
                    existingTranslation.Language = translationDto.Language.ToLower();
                }
                else
                {
                    // Add
                    career.careerCardTranslations.Add(new CareerCardTranslation
                    {
                        CareerId = career.Id,
                        Language = translationDto.Language.ToLower(),
                        Description = translationDto.Description,
                        JobTitle = translationDto.JobTitle,
                        EmploymentType = translationDto.EmploymentType,
                        SalaryFrom = translationDto.SalaryFrom,
                        SalaryTo = translationDto.SalaryTo,
                        SalaryPeriod = translationDto.SalaryPeriod
                    });
                }
            }

            // ✅ تحديث الكيان وحفظ التغييرات
            unitOfWork.Repository<Career>().Update(career);
            await unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> DeleteaboutTeamAsync(int id)
        {
            var career = await unitOfWork.Repository<Career>()
                                      .Query()
                                      .FirstOrDefaultAsync(f => f.Id == id);
            if (career == null)
                return false;
            unitOfWork.Repository<Career>().Delete(career);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}
