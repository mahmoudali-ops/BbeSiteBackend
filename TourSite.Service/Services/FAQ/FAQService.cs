using AutoMapper;
using AutoMapper.QueryableExtensions;
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
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.FAQ;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.FAQ
{
    public class FAQService : IFAQsService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IConfiguration configuration;

        public IWebHostEnvironment env { get; }


        public FAQService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
            configuration = _configuration;
        }
        #region
        //public async Task<FAQsDto?> GetAllFAQAsync(string? lang)
        //{
        //    lang = string.IsNullOrWhiteSpace(lang) ? "en" : lang.ToLower();

        //    return await unitOfWork.Repository<FAQs>()
        //        .Query()
        //        .AsNoTracking()
        //        .Where(f => f.fAQsTranslations.Any(t => t.Language == lang))
        //        .ProjectTo<FAQsDto>(
        //            mapper.ConfigurationProvider,
        //            new { lang }
        //        )
        //        .FirstOrDefaultAsync();
        //}

        //public async Task<FAQsDto?> GetFAQAsync(string? lang)
        //{
        //    lang = string.IsNullOrWhiteSpace(lang) ? "en" : lang.ToLower();

        //    var faq = await unitOfWork.Repository<FAQs>()
        //        .Query()
        //        .AsNoTracking()
        //        .Include(f => f.fAQsTranslations
        //            .Where(t => t.Language == lang))
        //        .OrderBy(f => f.Id)
        //        .FirstOrDefaultAsync();

        //    return faq is null ? null : mapper.Map<FAQsDto>(faq);
        //}

        //public async Task<bool> UpdateFAQ(FAQsUdateDTO dto, int id)
        //{
        //    // ✅ جلب FAQ مع الترجمات (Tracking)
        //    var faq = await unitOfWork.Repository<FAQs>()
        //        .Query()
        //        .Include(f => f.fAQsTranslations)
        //        .FirstOrDefaultAsync(f => f.Id == id);

        //    if (faq == null)
        //        return false;

        //    // ✅ فك JSON الخاص بالترجمات (لو موجود)
        //    if (!string.IsNullOrEmpty(dto.TranslationsJson))
        //    {
        //        dto.fAQsTranslationDTos = JsonSerializer.Deserialize<List<FAQsTranslationDTo>>(
        //            dto.TranslationsJson,
        //            new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            }
        //        ) ?? new List<FAQsTranslationDTo>();
        //    }

        //    // ✅ تحديث الصورة (لو تم رفع صورة جديدة)
        //    if (dto.ImageFile != null)
        //    {
        //        string uploadDir = Path.Combine(env.WebRootPath, "images/faqs");
        //        Directory.CreateDirectory(uploadDir);

        //        string fileName = Guid.NewGuid() + ".webp";
        //        string fullPath = Path.Combine(uploadDir, fileName);

        //        using (var image = await Image.LoadAsync(dto.ImageFile.OpenReadStream()))
        //        {
        //            image.Mutate(x => x.Resize(1600, 900));

        //            await image.SaveAsync(fullPath, new WebpEncoder
        //            {
        //                Quality = 80
        //            });
        //        }

        //        faq.ImageCover = $"images/faqs/{fileName}";
        //    }

        //    // ✅ تحديث البيانات الأساسية
        //    faq.ReferneceName = dto.ReferneceName;
        //    faq.MetaDescription = dto.MetaDescription;
        //    faq.MetaKeyWords = dto.MetaKeyWords;

        //    // ✅ تحديث / إضافة الترجمات حسب اللغة
        //    foreach (var translationDto in dto.fAQsTranslationDTos)
        //    {
        //        var existingTranslation = faq.fAQsTranslations
        //            .FirstOrDefault(t =>
        //                t.Language.ToLower() == translationDto.Language.ToLower());

        //        if (existingTranslation != null)
        //        {
        //            // Update
        //            existingTranslation.Question = translationDto.Question;
        //            existingTranslation.Answer = translationDto.Answer;
        //            existingTranslation.Language = translationDto.Language.ToLower();
        //        }
        //        else
        //        {
        //            // Add
        //            faq.fAQsTranslations.Add(new FAQsTranslation
        //            {
        //                FAQsId = faq.Id,
        //                Language = translationDto.Language.ToLower(),
        //                Question = translationDto.Question,
        //                Answer = translationDto.Answer
        //            });
        //        }
        //    }

        //    // ✅ تحديث الكيان وحفظ التغييرات
        //    unitOfWork.Repository<FAQs>().Update(faq);
        //    await unitOfWork.CompleteAsync();

        //    return true;
        //}

        #endregion d


        public async Task CreateFAQAsync(FAQsUdateDTO dto)
        {
            dto.fAQsTranslationDTos ??= new List<FAQsTranslationDTo>();

            if (!string.IsNullOrEmpty(dto.TranslationsJson))
            {
                dto.fAQsTranslationDTos = JsonSerializer.Deserialize<List<FAQsTranslationDTo>>(
                    dto.TranslationsJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                ) ?? new List<FAQsTranslationDTo>();
            }

            // 🖼️ حفظ الصورة
            string imagePath = string.Empty;

            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/faqs");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var image = await Image.LoadAsync(dto.ImageFile.OpenReadStream()))
                {
                    image.Mutate(x => x.Resize(1600, 900));

                    await image.SaveAsync(fullPath, new WebpEncoder()
                    {
                        Quality = 80
                    });
                }

                imagePath = $"images/faqs/{fileName}";
            }

            var faq = new FAQs
            {
                ImageCover = imagePath,
                ReferneceName = Guid.NewGuid().ToString(),


                fAQsTranslations = dto.fAQsTranslationDTos.Select(t => new FAQsTranslation
                {
                    Language = t.Language.ToLower(),
                    Question = t.Question,
                    Answer = t.Answer
                }).ToList()
            };

            await unitOfWork.Repository<FAQs>().AddAsync(faq);
            await unitOfWork.CompleteAsync();
        }



        public async Task<bool> DeleteFAQAsync(int id)
        {
            var faq = await unitOfWork.Repository<FAQs>()
                .Query()
                .FirstOrDefaultAsync(f => f.Id == id);

            if (faq == null)
                return false;

            unitOfWork.Repository<FAQs>().Delete(faq);
            await unitOfWork.CompleteAsync();

            return true;
        }


        public async Task<List<FAQsDto>> GetFAQAsync(string? lang = "en")
        {
            lang ??= "en";
            lang = lang.ToLower();

            var faqs = await unitOfWork.Repository<FAQs>()
                .Query()
                .Include(f => f.fAQsTranslations)
                .Select(f => new FAQsDto
                {
                    Id = f.Id,
                    ImageCover = configuration["BaseUrl"] + f.ImageCover,
                    ReferneceName = f.ReferneceName,
                    MetaDescription = f.MetaDescription,
                    MetaKeyWords = f.MetaKeyWords,

                    fAQsTranslationDTos = f.fAQsTranslations
                        .Where(t => t.Language.ToLower() == lang)
                        .Select(t => new FAQsTranslationDTo
                        {
                            Id = t.Id,
                            Language = t.Language,
                            Question = t.Question,
                            Answer = t.Answer
                        })
                        .ToList()
                })
                .ToListAsync();

            return faqs;
        }


        public async Task<bool> UpdateFAQ(FAQsUdateDTO dto, int id)
        {
            var faq = await unitOfWork.Repository<FAQs>()
                .Query()
                .Include(f => f.fAQsTranslations)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (faq == null)
                return false;

            if (!string.IsNullOrEmpty(dto.TranslationsJson))
            {
                dto.fAQsTranslationDTos = JsonSerializer.Deserialize<List<FAQsTranslationDTo>>(
                    dto.TranslationsJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                ) ?? new List<FAQsTranslationDTo>();
            }

            // 🖼️ تحديث الصورة
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/faqs");
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

                faq.ImageCover = $"images/faqs/{fileName}";
            }


            // تحديث الترجمات
            foreach (var translationDto in dto.fAQsTranslationDTos)
            {
                var existingTranslation = faq.fAQsTranslations
                    .FirstOrDefault(t => t.Language.ToLower() == translationDto.Language.ToLower());

                if (existingTranslation != null)
                {
                    existingTranslation.Question = translationDto.Question;
                    existingTranslation.Answer = translationDto.Answer;
                    existingTranslation.Language = translationDto.Language.ToLower();
                }
                else
                {
                    faq.fAQsTranslations.Add(new FAQsTranslation
                    {
                        FAQsId = faq.Id,
                        Language = translationDto.Language.ToLower(),
                        Question = translationDto.Question,
                        Answer = translationDto.Answer
                    });
                }
            }

            unitOfWork.Repository<FAQs>().Update(faq);
            await unitOfWork.CompleteAsync();

            return true;
        }




    }
}
