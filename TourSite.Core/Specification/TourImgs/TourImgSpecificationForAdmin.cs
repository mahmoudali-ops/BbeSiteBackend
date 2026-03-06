//using Store.Core.Specifications;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TourSite.Core.Entities;

//namespace TourSite.Core.Specification.TourImgs
//{
//    public class TourImgSpecificationForAdmin:BaseSpecifications<Price>
//    {
//        public TourImgSpecificationForAdmin(int id) : base(t => t.Id == id)
//        {
//            applyIncludes();
//        }
//        public TourImgSpecificationForAdmin(TourImgSpeciParams specParams) :
//            base()

//        {
//            ApplyPag(specParams.pageSize, specParams.pageIndex);
//            applyIncludes();
//        }

//        public void applyIncludes()
//        {
//            Includes.Add(t => t.Translations);
//            Includes.Add(t => t.Tour);
//            Includes.Add(t => t.Tour.Translations);


//        }
//    }
//}
