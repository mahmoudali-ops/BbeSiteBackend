using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class ServiceFeatures
    {
        public int Id { get; set; }

        public string ImageCover { get; set; }

        public ICollection<ServiceFeaturesTranslation> ServiceFeaturesTranslations { get; set; }

    }
}
