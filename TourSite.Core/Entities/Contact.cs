using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class Contact
    {
    [Key]
    public int Id { get; set; }

    public string ImageCover { get; set; }
    public string ReferneceName { get; set; }

    public string? MetaDescription { get; set; }
    public string? MetaKeyWords { get; set; }

        // Relations
    public ICollection<ContactTranslation>  contactTranslation  { get; set; }

    }
}
