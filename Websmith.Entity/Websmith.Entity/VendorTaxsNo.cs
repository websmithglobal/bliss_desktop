using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class VendorTaxsNo
    {
        public Guid VendorTaxsNoID { get; set; }
        public Guid VendorID { get; set; }
        public string TaxName { get; set; }
        public string TaxNo { get; set; }
        public decimal TaxPercentage { get; set; }
        public Guid RUserID { get; set; }
        public int RUserType { get; set; }
        public string CreatedDate { get; set; }
        public bool IsStatus { get; set; }
        public string Mode { get; set; }
    }
}
