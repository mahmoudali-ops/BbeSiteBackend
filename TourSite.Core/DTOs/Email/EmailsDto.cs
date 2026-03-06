using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.DTOs.Email
{
    public class EmailsDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        [MaxLength(200)]
        public string EmailAddress { get; set; }

        public string Message { get; set; }

        public string Subject { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
