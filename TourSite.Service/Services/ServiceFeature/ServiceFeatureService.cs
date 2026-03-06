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
using TourSite.Core.DTOs.ServiceCore;
using TourSite.Core.DTOs.ServicesFeature;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.ServiceFeature
{
    public class ServiceFeatureService : IServiceFeatureService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IConfiguration configuration;

        public IWebHostEnvironment env { get; }


        public ServiceFeatureService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
            configuration = _configuration;
        }
        public async Task CreateServiceFeatureAsync(ServicesFeatureUdateDto dto)
        {

            dto.servicesFeatureTranslationDtos ??= new List<ServicesFeatureTranslationDto>();

            if (!string.IsNullOrEmpty(dto.TranslationsJson))
            {
                dto.servicesFeatureTranslationDtos = JsonSerializer.Deserialize<List<ServicesFeatureTranslationDto>>(
                    dto.TranslationsJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                ) ?? new List<ServicesFeatureTranslationDto>();
            }

            // 🖼️ حفظ الصورة في wwwroot/images/categoryTours
            string imagePath = string.Empty;
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/servicefeature");
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
                        Size = new Size(1200, 1200) // أقصى عرض وارتفاع
                    }));

                    // حفظ WebP بجودة ممتازة وحجم صغير جدًا
                    await image.SaveAsync(fullPath, new WebpEncoder()
                    {
                        Quality = 80
                    });
                }

                imagePath = $"images/servicefeature/{fileName}";
            }



            // 🧩 إنشاء الكيان
            var service= new ServiceFeatures
            {
                ImageCover = imagePath,
                ServiceFeaturesTranslations = dto.servicesFeatureTranslationDtos.Select(t => new ServiceFeaturesTranslation
                {
                    Language = t.Language.ToLower(),
                    Description = t.Description,
                    Title = t.Title,
                    IncludeFirst = t.IncludeFirst,
                    IncludeSecond = t.IncludeSecond,

                }).ToList(),


            };

            await unitOfWork.Repository<ServiceFeatures>().AddAsync(service);
            await unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteaServiceFeatureAsync(int id)
        {

            var service = await unitOfWork.Repository<ServiceFeatures>()
                                                  .Query()
                                                  .FirstOrDefaultAsync(f => f.Id == id);
            if (service == null)
                return false;
            unitOfWork.Repository<ServiceFeatures>().Delete(service);
            await unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<List<ServicesFeatureDto>> GetServiceCoreAsync(string? lang = "en")
        {


            lang ??= "en";
            lang = lang.ToLower();

            var aboutdto = await unitOfWork.Repository<ServiceFeatures>()
                .Query()
                .Include(t => t.ServiceFeaturesTranslations)
                .Select(t => new ServicesFeatureDto
                {
                    Id = t.Id,
                    ImageCover = configuration["BaseUrl"] + t.ImageCover,
                    servicesFeatureTranslationDtos = t.ServiceFeaturesTranslations
                        .Where(tr => tr.Language.ToLower() == lang)
                        .Select(tr => new ServicesFeatureTranslationDto
                        {
                            Id = tr.Id,
                            Language = tr.Language,
                            Title = tr.Title,
                            Description = tr.Description,
                            IncludeFirst = tr.IncludeFirst,
                            IncludeSecond = tr.IncludeSecond
                        })
                        .ToList()
                })
                .ToListAsync();


            return aboutdto;
        }

        public async Task<bool> UpdateServiceFeatureAsync(ServicesFeatureUdateDto dto, int id)
        {

            // ✅ جلب FAQ مع الترجمات (Tracking)
            var serviceFeature = await unitOfWork.Repository<ServiceFeatures>()
                .Query()
                .Include(f => f.ServiceFeaturesTranslations)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (serviceFeature == null)
                return false;

            // ✅ فك JSON الخاص بالترجمات (لو موجود)
            if (!string.IsNullOrEmpty(dto.TranslationsJson))
            {
                dto.servicesFeatureTranslationDtos = JsonSerializer.Deserialize<List<ServicesFeatureTranslationDto>>(
                    dto.TranslationsJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                ) ?? new List<ServicesFeatureTranslationDto>();
            }

            // ✅ تحديث الصورة (لو تم رفع صورة جديدة)
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/servicefeature");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var image = await Image.LoadAsync(dto.ImageFile.OpenReadStream()))
                {
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

                serviceFeature.ImageCover = $"images/servicefeature/{fileName}";
            }


            // ✅ تحديث / إضافة الترجمات حسب اللغة
            foreach (var translationDto in dto.servicesFeatureTranslationDtos)
            {
                var existingTranslation = serviceFeature.ServiceFeaturesTranslations
                    .FirstOrDefault(t =>
                        t.Language.ToLower() == translationDto.Language.ToLower());

                if (existingTranslation != null)
                {
                    // Update
                    existingTranslation.Description = translationDto.Description;
                    existingTranslation.Title = translationDto.Title;
                    existingTranslation.Language = translationDto.Language.ToLower();
                    existingTranslation.IncludeFirst = translationDto.IncludeFirst;
                    existingTranslation.IncludeSecond = translationDto.IncludeSecond;
                }
                else
                {
                    // Add
                    serviceFeature.ServiceFeaturesTranslations.Add(new ServiceFeaturesTranslation
                    {
                        ServiceFeaturesId = serviceFeature.Id,
                        Language = translationDto.Language.ToLower(),
                        Description = translationDto.Description,
                        Title = translationDto.Title,
                        IncludeFirst = translationDto.IncludeFirst,
                        IncludeSecond = translationDto.IncludeSecond

                    });
                }
            }

            // ✅ تحديث الكيان وحفظ التغييرات
            unitOfWork.Repository<ServiceFeatures>().Update(serviceFeature);
            await unitOfWork.CompleteAsync();

            return true;
        }
    }
}
