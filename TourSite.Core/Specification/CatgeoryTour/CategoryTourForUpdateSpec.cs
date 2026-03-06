//using Store.Core.Specifications;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TourSite.Core.Entities;

//namespace TourSite.Core.Specification.CatgeoryTour
//{
//    public class CategoryTourForUpdateSpec : BaseSpecifications<Contact>
//    {
//        public CategoryTourForUpdateSpec(int id) : base(t => t.Id == id)
//        {
//            // Include الترجمات فقط، لأننا محتاجين نحدثها
//            Includes.Add(c => c.Translations);

//            // ممكن نضيف includes إضافية لو في حاجة تانية محتاجين نحدثها
//            // مثلاً الصور أو tours لو المستقبل هيحتاج
//            // Includes.Add(c => c.Tours);
//        }
//    }
//}
