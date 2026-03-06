using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Store.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core;
using TourSite.Core.DTOs.Email;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Core.Specification.Emails;


namespace TourSite.Service.Services.Emails
{
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IEmailSender_ _emailSender;
        public EmailService(IUnitOfWork _unitOfWork, IMapper _mapper, IEmailSender_ _EmailSender)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            _emailSender = _EmailSender;
        }


        public async Task<PageinationResponse<EmailsDto>> GetAllEmailsToursAsync(EmailSpecParams SpecParams)
        {
            var spec = new EmailSpecification(SpecParams);
            var data = mapper.Map<IEnumerable<EmailsDto>>(await unitOfWork.Repository<Email>().GetAllSpecAsync(spec));
            var CountSpec = new EmailCountSpecifications(SpecParams);
            var Count = await unitOfWork.Repository<Email>().GetCountAsync(CountSpec);

            return new PageinationResponse<EmailsDto>(SpecParams.pageIndex, SpecParams.pageSize, Count, data);
        }

        public async Task<EmailsDto> GetCatEmailByIdAsync(int id)
        {
            var spec = new EmailSpecification(id);
            return mapper.Map<EmailsDto>(await unitOfWork.Repository<Email>().GetByIdSpecAsync(spec));
        }

        public async Task AddEmailAsync(EmailsDto EmasilDto)
        {

            var email = new Email
            {
                FullName = EmasilDto.FullName,
                EmailAddress = EmasilDto.EmailAddress,
                CreatedAt = EmasilDto.CreatedAt,
                Subject= EmasilDto.Subject,
                Message= EmasilDto.Message,
            };

            await unitOfWork.Repository<Email>().AddAsync(email);
            await unitOfWork.CompleteAsync();

           // await _emailSender.SendToAdminAsync(EmasilDto);
        }

        public async Task<bool> DeleteEmailAsync(int id)
        {
            var spec = new EmailSpecification(id);

            var tour = unitOfWork.Repository<Email>().GetByIdSpecTEntityAsync(spec);

            if (tour == null)
            {
                return false;
            }

            unitOfWork.Repository<Email>().Delete(tour);

            await unitOfWork.CompleteAsync();

            return true;
        }
    }
}
