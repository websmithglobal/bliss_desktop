using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class CategoryDetail
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ImgPath { get; set; }
        public string MainCategoryID { get; set; }
        public string ClassMasterID { get; set; }
        public int Priority { get; set; }
        public List<object> SubCategoryDetail { get; set; }
        public List<object> CategoryWiseProduct { get; set; }
        public List<object> TaxGroupDetail { get; set; }
        public string Mode { get; set; }
    }
}
