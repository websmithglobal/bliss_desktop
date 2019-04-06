using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class VendorMasterData
    {
        public Guid VendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string CompanyName { get; set; }
        public Guid RUserID { get; set; }
        public int RUserType { get; set; }
        public string Mode { get; set; }
        public int IsUPStream { get; set; } = 0;
        public bool Status { get; set; }
        public string ContactPersonName { get; set; }
        public string PinCode { get; set; }
        public string Fax { get; set; }
        public bool IsSendPOInMail { get; set; }
        public bool IsSendPOInSMS { get; set; }
        public decimal MinOrderAmt { get; set; }
        public int ShippingCharges { get; set; }
        public int PaymentTermsID { get; set; }
        public string Remarks { get; set; }
    }

    public class VendorMasterView
    {
        public Guid VendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string CompanyName { get; set; }
    }
}
