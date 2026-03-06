using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class Email
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(150)]
        public string FullName { get; set; }

        [MaxLength(200)]
        public string EmailAddress { get; set; }

        public string Message { get; set; }

        public string Subject { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
