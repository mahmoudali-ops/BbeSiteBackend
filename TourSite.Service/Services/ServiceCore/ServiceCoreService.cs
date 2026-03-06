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
using TourSite.Core.DTOs.ServiceCore;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.ServiceCores
{
    public class ServiceCoreService : IServiceCoreService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IConfiguration configuration;

        public IWebHostEnvironment env { get; }


        public ServiceCoreService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
            configuration = _configuration;
        }
        public async Task CreateServceCoreAsync(ServiceCoreUpdateDto dto)
        {

            dto.serviceCoreTranlationDtos ??= new List<ServiceCoreTranlationDto>();

            if (!string.IsNullOrEmpty(dto.TranslationsJson))
            {
                dto.serviceCoreTranlationDtos = JsonSerializer.Deserialize<List<ServiceCoreTranlationDto>>(
                    dto.TranslationsJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                ) ?? new List<ServiceCoreTranlationDto>();
            }

            // 🖼️ حفظ الصورة في wwwroot/images/categoryTours
            string imagePath = string.Empty;
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/servicecore");
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
                        Size = new Size(500, 500) // أقصى عرض وارتفاع
                    }));
                    // حفظ WebP بجودة ممتازة وحجم صغير جدًا
                    await image.SaveAsync(fullPath, new WebpEncoder()
                    {
                        Quality = 80
                    });
                }

                imagePath = $"images/servicecore/{fileName}";
            }



            // 🧩 إنشاء الكيان
            var serviceCore = new ServiceCore
            {
                ImageCover = imagePath,
                ServiceCoreTranslations = dto.serviceCoreTranlationDtos.Select(t => new ServiceCoreTranslation
                {
                    Language = t.Language.ToLower(),
                    Description = t.Description,
                    Title = t.Title,


                }).ToList(),


            };

            await unitOfWork.Repository<ServiceCore>().AddAsync(serviceCore);
            await unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteaServiceCoreAsync(int id)
        {
            var service = await unitOfWork.Repository<ServiceCore>()
                                                  .Query()
                                                  .FirstOrDefaultAsync(f => f.Id == id);
            if (service == null)
                return false;
            unitOfWork.Repository<ServiceCore>().Delete(service);
            await unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<List<ServiceCoreDto>> GetServiceCoreAsync(string? lang = "en")
        {

            lang ??= "en";
            lang = lang.ToLower();

            var aboutdto = await unitOfWork.Repository<ServiceCore>()
                .Query()
                .Include(t => t.ServiceCoreTranslations)
                .Select(t => new ServiceCoreDto
                {
                    Id = t.Id,
                    ImageCover = configuration["BaseUrl"] + t.ImageCover,
                    serviceCoreTranlationDtos = t.ServiceCoreTranslations
                        .Where(tr => tr.Language.ToLower() == lang)
                        .Select(tr => new ServiceCoreTranlationDto
                        {
                            Id = tr.Id,
                            Language = tr.Language,
                            Title = tr.Title,
                            Description = tr.Description

                        })
                        .ToList()
                })
                .ToListAsync();


            return aboutdto;
        }

        public async Task<bool> UpdateServiceCoreAsync(ServiceCoreUpdateDto dto, int id)
        {

            // ✅ جلب FAQ مع الترجمات (Tracking)
            var serviceCore = await unitOfWork.Repository<ServiceCore>()
                .Query()
                .Include(f => f.ServiceCoreTranslations)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (serviceCore == null)
                return false;

            // ✅ فك JSON الخاص بالترجمات (لو موجود)
            if (!string.IsNullOrEmpty(dto.TranslationsJson))
            {
                dto.serviceCoreTranlationDtos = JsonSerializer.Deserialize<List<ServiceCoreTranlationDto>>(
                    dto.TranslationsJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                ) ?? new List<ServiceCoreTranlationDto>();
            }

            // ✅ تحديث الصورة (لو تم رفع صورة جديدة)
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/servicecore");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var image = await Image.LoadAsync(dto.ImageFile.OpenReadStream()))
                {
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max, // يحافظ على النسبة، لا يقطع الصورة
                        Size = new Size(500, 500) // أقصى عرض وارتفاع
                    }));
                    await image.SaveAsync(fullPath, new WebpEncoder
                    {
                        Quality = 80
                    });
                }

                serviceCore.ImageCover = $"images/servicecore/{fileName}";
            }


            // ✅ تحديث / إضافة الترجمات حسب اللغة
            foreach (var translationDto in dto.serviceCoreTranlationDtos)
            {
                var existingTranslation = serviceCore.ServiceCoreTranslations
                    .FirstOrDefault(t =>
                        t.Language.ToLower() == translationDto.Language.ToLower());

                if (existingTranslation != null)
                {
                    // Update
                    existingTranslation.Description = translationDto.Description;
                    existingTranslation.Title = translationDto.Title;
                    existingTranslation.Language = translationDto.Language.ToLower();
                }
                else
                {
                    // Add
                    serviceCore.ServiceCoreTranslations.Add(new ServiceCoreTranslation
                    {
                        ServiceCoreId = serviceCore.Id,
                        Language = translationDto.Language.ToLower(),
                        Description = translationDto.Description,
                        Title = translationDto.Title

                    });
                }
            }

            // ✅ تحديث الكيان وحفظ التغييرات
            unitOfWork.Repository<ServiceCore>().Update(serviceCore);
            await unitOfWork.CompleteAsync();

            return true;
        }
    }
}
