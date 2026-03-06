using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.FAQ;
using TourSite.Core.DTOs.SocialElements;

namespace TourSite.Core.Servicies.Contract
{
    public interface ISocialElemtsService
    {
        Task<SocailElementsDto> GetSocialElemtsAsync();
        Task<Boolean> UpdateSocailEleemts(SocialElementsUpdate dto);
    }
}
