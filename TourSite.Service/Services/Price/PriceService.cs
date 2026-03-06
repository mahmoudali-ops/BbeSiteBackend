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
using TourSite.Core.DTOs.Career;
using TourSite.Core.DTOs.Price;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.Prices
{
    public class PriceService : IPriceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IConfiguration configuration;

        public IWebHostEnvironment env { get; }


        public PriceService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
            configuration = _configuration;
        }
        public async Task CreatePriceAsync(PriceUpdateDto dto)
        {
            // تأكد أن القائمة ليست null قبل الاستخدام
            dto.priceTranlationDtos ??= new List<PriceTranlationDto>();

            if (!string.IsNullOrEmpty(dto.TranslationsJson))
            {
                dto.priceTranlationDtos = JsonSerializer.Deserialize<List<PriceTranlationDto>>(
                    dto.TranslationsJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                ) ?? new List<PriceTranlationDto>();
            }

            // 🖼️ حفظ الصورة في wwwroot/images/categoryTours
            string imagePath = string.Empty;
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/price");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                // فتح الصورة باستخدام ImageSharp
                using (var image = await Image.LoadAsync(dto.ImageFile.OpenReadStream()))
                {
                    // تغيير الأبعاد
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max, // يحافظ على النسبة بدون قص
                        Size = new Size(1200, 900)
                    }));
                    // حفظ WebP بجودة ممتازة وحجم صغير جدًا
                    await image.SaveAsync(fullPath, new WebpEncoder()
                    {
                        Quality = 80
                    });
                }

                imagePath = $"images/price/{fileName}";
            }

            // 🧩 إنشاء الكيان
            var price = new Price
            {
                ImageCover = imagePath,
                ReferneceName = Guid.NewGuid().ToString(),
  
                priceCardTranslations = dto.priceTranlationDtos.Select(t => new PriceCardTranslation
                {
                    Language = t.Language.ToLower(),
                    Title = t.Title,
                    Description = t.Description,
                    Discount = t.Discount,
                    PriceService = t.PriceService,
                    IncludeFirst = t.IncludeFirst,
                    IncludeSecond = t.IncludeSecond,
                    IncludeThird = t.IncludeThird,
                    IncludeForth = t.IncludeForth,

                }).ToList(),


            };

            await unitOfWork.Repository<Price>().AddAsync(price);
            await unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeletPriceAsync(int id)
        {
            var price = await unitOfWork.Repository<Price>()
                                                 .Query()
                                                 .FirstOrDefaultAsync(f => f.Id == id);
            if (price == null)
                return false;
            unitOfWork.Repository<Price>().Delete(price);
            await unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<List<PriceDto>> GetPriceAsync(string? lang = "en")
        {

            lang ??= "en";
            lang = lang.ToLower();
            var pricedto = await unitOfWork.Repository<Price>()
                .Query()
                .Include(t => t.priceCardTranslations)
                .Select(t => new PriceDto
                {
                    Id = t.Id,
                    ImageCover = configuration["BaseUrl"] + t.ImageCover,
                    priceTranlationDtos = t.priceCardTranslations
                        .Where(tr => tr.Language.ToLower() == lang)
                        .Select(tr => new PriceTranlationDto
                        {
                             Id = tr.Id,
                                Language = tr.Language,
                                Discount = tr.Discount,
                                PriceService = tr.PriceService,
                                IncludeFirst = tr.IncludeFirst,
                                IncludeSecond = tr.IncludeSecond,
                                IncludeThird = tr.IncludeThird,
                                IncludeForth = tr.IncludeForth,
                                Title = tr.Title,
                                Description = tr.Description

                        })
                        .ToList()
                })
                .ToListAsync();


            return  pricedto; 
        }

        public async Task<bool> UpdatePriceAsync(PriceUpdateDto dto, int id)
        {

            // ✅ جلب FAQ مع الترجمات (Tracking)
            var price = await unitOfWork.Repository<Price>()
                .Query()
                .Include(f => f.priceCardTranslations)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (price == null)
                return false;

            // ✅ فك JSON الخاص بالترجمات (لو موجود)
            if (!string.IsNullOrEmpty(dto.TranslationsJson))
            {
                dto.priceTranlationDtos = JsonSerializer.Deserialize<List<PriceTranlationDto>>(
                    dto.TranslationsJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                ) ?? new List<PriceTranlationDto>();
            }

            // ✅ تحديث الصورة (لو تم رفع صورة جديدة)
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/price");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var image = await Image.LoadAsync(dto.ImageFile.OpenReadStream()))
                {
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max, // يحافظ على النسبة بدون قص
                        Size = new Size(1200, 900)
                    }));
                    await image.SaveAsync(fullPath, new WebpEncoder
                    {
                        Quality = 80
                    });
                }

                price.ImageCover = $"images/price/{fileName}";
            }


            // ✅ تحديث / إضافة الترجمات حسب اللغة
            foreach (var translationDto in dto.priceTranlationDtos)
            {
                var existingTranslation = price.priceCardTranslations
                    .FirstOrDefault(t =>
                        t.Language.ToLower() == translationDto.Language.ToLower());

                if (existingTranslation != null)
                {
                    // Update
                    existingTranslation.Description = translationDto.Description;
                    existingTranslation.Title = translationDto.Title;   
                    existingTranslation.Discount = translationDto.Discount;
                    existingTranslation.PriceService = translationDto.PriceService;
                    existingTranslation.IncludeFirst = translationDto.IncludeFirst;
                    existingTranslation.IncludeSecond = translationDto.IncludeSecond;
                    existingTranslation.IncludeThird = translationDto.IncludeThird;
                    existingTranslation.IncludeForth = translationDto.IncludeForth;
                    existingTranslation.Description = translationDto.Description;
                    existingTranslation.Language = translationDto.Language.ToLower();
                }
                else
                {
                    // Add
                    price.priceCardTranslations.Add(new PriceCardTranslation
                    {
                        PriceId = price.Id,
                        Language = translationDto.Language.ToLower(),
                        Description = translationDto.Description,
                        Title= translationDto.Title,
                        Discount = translationDto.Discount,
                        PriceService = translationDto.PriceService,
                        IncludeFirst = translationDto.IncludeFirst,
                        IncludeSecond = translationDto.IncludeSecond,
                        IncludeThird = translationDto.IncludeThird,
                        IncludeForth = translationDto.IncludeForth

                    });
                }
            }

            // ✅ تحديث الكيان وحفظ التغييرات
            unitOfWork.Repository<Price>().Update(price);
            await unitOfWork.CompleteAsync();

            return true;
        }
    }
}
