using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class CustomerMasterData
    {
        public Guid CustomerID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public int IsUPStream { get; set; } = 0;
        public string ShippingAddress { get; set; }
        public string CardNo { get; set; }
        public Guid RUserID { get; set; }
        public int RUserType { get; set; } = 0;
        public string Mode { get; set; }
    }

    public class CustomerMaster
    {
        public Guid CustomerID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public int IsUPStream { get; set; } = 0;
        public string ShippingAddress { get; set; }
        public string CardNo { get; set; }
        public Guid RUserID { get; set; }
        public string Mode { get; set; }
    }
}
