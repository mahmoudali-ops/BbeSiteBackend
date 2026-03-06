using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.Contact
{
    public class ContactTranlationDto
    {
        public int Id { get; set; }

        public string Language { get; set; } // "en", "ar", etc.

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
