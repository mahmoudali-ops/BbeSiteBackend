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
using TourSite.Core.DTOs.Contact;
using TourSite.Core.DTOs.FAQ;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IConfiguration configuration;

        public IWebHostEnvironment env { get; }


        public ContactService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
            configuration = _configuration;
        }
        public async Task<ContactDto> GetContactAsync(string? lang = "en")
        {
            lang = string.IsNullOrWhiteSpace(lang) ? "en" : lang.ToLower();

            var contact = await unitOfWork.Repository<Contact>()
                .Query()
                .AsNoTracking()
                .Include(f => f.contactTranslation
                    .Where(t => t.Language == lang))
                .OrderBy(c => c.Id)
                .FirstOrDefaultAsync();

            return contact is null ? null : mapper.Map<ContactDto>(contact);
        }

        public async Task<bool> UpdateCotact(ContactUpdateDto dto)
        {
            // ✅ جلب FAQ مع الترجمات (Tracking)
            var contact = await unitOfWork.Repository<Contact>()
                .Query()
                .Include(f => f.contactTranslation)
                .FirstOrDefaultAsync();

            if (contact == null)
                return false;

            // ✅ فك JSON الخاص بالترجمات (لو موجود)
            if (!string.IsNullOrEmpty(dto.TranslationsJson))
            {
                dto.contactTranlationDtos = JsonSerializer.Deserialize<List<ContactTranlationDto>>(
                    dto.TranslationsJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                ) ?? new List<ContactTranlationDto>();
            }

            // ✅ تحديث الصورة (لو تم رفع صورة جديدة)
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/contact");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var image = await Image.LoadAsync(dto.ImageFile.OpenReadStream()))
                {
                    // Resize مع الحفاظ على النسبة
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max, // يحافظ على النسبة، لا يقطع الصورة
                        Size = new Size(900, 1000) // نقلل الطول شوية
                    }));

                    await image.SaveAsync(fullPath, new WebpEncoder
                    {
                        Quality = 80
                    });
                }


                contact.ImageCover = $"images/contact/{fileName}";
            }

            // ✅ تحديث البيانات الأساسية
            contact.ReferneceName = dto.ReferneceName;
            contact.MetaDescription = dto.MetaDescription;
            contact.MetaKeyWords = dto.MetaKeyWords;
            


            // ✅ تحديث / إضافة الترجمات حسب اللغة
            foreach (var translationDto in dto.contactTranlationDtos)
            {
                var existingTranslation = contact.contactTranslation
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
                    contact.contactTranslation.Add(new ContactTranslation
                    {
                        ContactId = contact.Id,
                        Language = translationDto.Language.ToLower(),
                        Title = translationDto.Title,
                        Description = translationDto.Description

                    });
                }
            }

            // ✅ تحديث الكيان وحفظ التغييرات
            unitOfWork.Repository<Contact>().Update(contact);
            await unitOfWork.CompleteAsync();

            return true;
        }
    }
}
