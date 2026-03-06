//using Store.Core.Specifications;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TourSite.Core.Entities;
//using TourSite.Core.Specification.Tours;

//namespace TourSite.Core.Specification.CatgeoryTour
//{
//    public class CatTourSpecification :BaseSpecifications<Contact>
//    {
//        public CatTourSpecification(int id) : base(t => t.Id == id)
//        {
//            applyIncludes();
//        }
//        public CatTourSpecification(CatTourSpecParams specParams)
//                 : base(p => p.IsActive == true)
//        {
//            ApplyPag(specParams.pageSize, specParams.pageIndex);
//            applyIncludes();
//        }

//        public void applyIncludes()
//        {
//            Includes.Add(c => c.Translations); // Category translations

//            Includes.Add(c => c.Tours);// tours

//            // بدل العبارات اللي كانت تسبب خطأ، نستخدم string paths
//            IncludeStrings.Add("Tours.Translations");
//            IncludeStrings.Add("Tours.TourImgs");
//            IncludeStrings.Add("Tours.TourImgs.Translations");
//            IncludeStrings.Add("Tours.Destination");
//            IncludeStrings.Add("Tours.Destination.Translations");

//        }
//    }
//}
