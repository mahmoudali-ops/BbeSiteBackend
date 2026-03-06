//using Store.Core.Specifications;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TourSite.Core.Entities;

//namespace TourSite.Core.Specification.Tours
//{
//    public class TourSpecification : BaseSpecifications<Tour>
//    {
//        public TourSpecification(int id) : base(t => t.Id == id)
//        {
//            applyIncludes();

//        }
//        public TourSpecification(TourSpecParams specParams) :
//            base(p => p.IsActive == true)
//        {

//            applyIncludes();

//            ApplyPag(specParams.pageSize, specParams.pageIndex);
//        }

//        public void applyIncludes()
//        {
//            Includes.Add(t => t.Translations);

//            Includes.Add(t => t.TourImgs);
//            IncludeStrings.Add("TourImgs.Translations");


//            Includes.Add(t => t.Included);
//            Includes.Add(t => t.NotIncluded);
//            Includes.Add(t => t.Highlights);

//            Includes.Add(t => t.Destination);
//            IncludeStrings.Add("Destination.Translations");

//            Includes.Add(t => t.Category);
//            IncludeStrings.Add("Category.Translations");


//        }


//    }
//}
