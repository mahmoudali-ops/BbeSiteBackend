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
using TourSite.Core.DTOs.SocialElements;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.SocialELements
{
    public class SocialELementsaService : ISocialElemtsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IConfiguration configuration;

        public IWebHostEnvironment env { get; }


        public SocialELementsaService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
            configuration = _configuration;
        }
        public async Task<SocailElementsDto> GetSocialElemtsAsync()
        {


            var faq = await unitOfWork.Repository<SocialElements>()
                .Query()
                .AsNoTracking()
                .OrderBy(s => s.Id)
                .FirstOrDefaultAsync();

            return faq is null ? null : mapper.Map<SocailElementsDto>(faq);
        }

        public async Task<bool> UpdateSocailEleemts(SocialElementsUpdate dto)
        {
            // ✅ جلب FAQ مع الترجمات (Tracking)
            var social = await unitOfWork.Repository<SocialElements>()
                .Query()
                .FirstOrDefaultAsync();

            if (social == null)
                return false;
            // ✅ تحديث الصورة (لو تم رفع صورة جديدة)
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/logo");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + ".webp";
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var image = await Image.LoadAsync(dto.ImageFile.OpenReadStream()))
                {
                    image.Mutate(x => x.Resize(500, 500));

                    await image.SaveAsync(fullPath, new WebpEncoder
                    {
                        Quality = 90
                    });
                }
                social.Logo = $"images/logo/{fileName}";
            }
            // ✅ تحديث البيانات الأساسية
            social.FacebookUrl = dto.FacebookUrl;
            social.InstagramUrl = dto.InstagramUrl;
            social.Email = dto.Email;
            unitOfWork.Repository<SocialElements>().Update(social);
            await unitOfWork.CompleteAsync();

            return true;
        }
    }
}
