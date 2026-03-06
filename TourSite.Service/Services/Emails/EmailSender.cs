using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Email;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.Emails
{
    using System.Net;
    using System.Net.Mail;

    public class EmailSender : IEmailSender_
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendToAdminAsync(EmailsDto e)
        {
            // Validate configuration values
            var fromEmail = _config["EmailSettings:From"];
            if (string.IsNullOrWhiteSpace(fromEmail))
                throw new InvalidOperationException("EmailSettings:From configuration is missing or empty.");

            var adminEmail = _config["EmailSettings:AdminEmail"];
            if (string.IsNullOrWhiteSpace(adminEmail))
                throw new InvalidOperationException("EmailSettings:AdminEmail configuration is missing or empty.");

            var smtpServer = _config["EmailSettings:SmtpServer"];
            if (string.IsNullOrWhiteSpace(smtpServer))
                throw new InvalidOperationException("EmailSettings:SmtpServer configuration is missing or empty.");

            var portStr = _config["EmailSettings:Port"];
            if (string.IsNullOrWhiteSpace(portStr) || !int.TryParse(portStr, out int port) || port <= 0)
                throw new InvalidOperationException("EmailSettings:Port configuration is missing, invalid, or must be greater than 0.");

            var username = _config["EmailSettings:Username"];
            if (string.IsNullOrWhiteSpace(username))
                throw new InvalidOperationException("EmailSettings:Username configuration is missing or empty.");

            var password = _config["EmailSettings:Password"];
            if (string.IsNullOrWhiteSpace(password))
                throw new InvalidOperationException("EmailSettings:Password configuration is missing or empty.");

            var mail = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = "🛎️ New Booking Request",
                IsBodyHtml = true
            };

            // To (Admin)
            mail.To.Add(adminEmail);

            // Optional: Reply-To
            mail.ReplyToList.Add(new MailAddress("info@toppickstravels.com"));

            // Null-safe string interpolation for DTO properties
            mail.Body = $@"
            <h2>New Booking Received</h2>
            <p><b>Name:</b> {e.FullName ?? "N/A"}</p>
            <p><b>Email:</b> {e.EmailAddress ?? "N/A"}</p>
            <p><b>Message:</b><br/>{e.Message ?? "N/A"}</p>
        ";

            using var smtp = new SmtpClient
            {
                Host = smtpServer,
                Port = port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(username, password)
            };

            await smtp.SendMailAsync(mail);
        }
    }
}
