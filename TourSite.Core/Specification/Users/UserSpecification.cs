using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.Specification.Users
{
    public class UserSpecification : BaseSpecifications<User>
    {
        public UserSpecification(string id) : base(t => t.Id == id)
        {
        }
        public UserSpecification(UserSpeciParams specParams) : base(
            p =>
            (string.IsNullOrEmpty(specParams.Search) || p.FullName.ToLower().Contains(specParams.Search)))

        {
            ApplyPag(specParams.pageSize, specParams.pageIndex);
        }

      
    }
}
