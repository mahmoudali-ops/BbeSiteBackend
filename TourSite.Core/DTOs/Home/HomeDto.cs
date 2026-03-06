using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.DTOs.Home
{
    public class HomeDto
    {
        public int Id { get; set; }

        public string MainCover { get; set; }
        public string MultiLangImage { get; set; }
        public string TeamImage { get; set; }
        public string HelpImage { get; set; }

        public ICollection<HomeTranslationDto> homeTranslationDtos { get; set; }
    }
}
