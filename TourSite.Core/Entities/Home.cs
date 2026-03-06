using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class Home
    {
        [Key]
        public int Id { get; set; }

        public string MainCover { get; set; }
        public string MultiLangImage { get; set; }
        public string TeamImage { get; set; }
        public string HelpImage { get; set; }

        public ICollection<HomeTranslation> HomeTranslation { get; set; }

    }
}
