using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class ServiceFeaturesTranslation
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Language { get; set; } // en, ar, de, nl ...

        public string Description { get; set; }

        public string IncludeFirst { get; set; }

        public string IncludeSecond { get; set; }

        // FK
        public int ServiceFeaturesId { get; set; }

        [ForeignKey(nameof(ServiceFeaturesId))]
        public ServiceFeatures ServiceFeatures { get; set; }




    }
}
