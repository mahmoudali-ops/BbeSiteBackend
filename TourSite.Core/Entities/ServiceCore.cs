using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class ServiceCore
    {
        [Key]
        public int Id { get; set; }

        public string ImageCover { get; set; }


        public ICollection<ServiceCoreTranslation> ServiceCoreTranslations { get; set; }
    }
}
