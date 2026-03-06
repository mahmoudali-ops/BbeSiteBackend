using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.DTOs.Contact
{
    public class ContactDto
    {
        public int Id { get; set; }

        public string ImageCover { get; set; }
        public string ReferneceName { get; set; }

        public string? MetaDescription { get; set; }
        public string? MetaKeyWords { get; set; }

        // Relations
        public ICollection<ContactTranlationDto> contactTranlationDtos { get; set; }

    }
}
