using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Email;

namespace TourSite.Core.Servicies.Contract
{
    public interface IEmailSender_
    {
        Task SendToAdminAsync(EmailsDto dto);

    }
}
