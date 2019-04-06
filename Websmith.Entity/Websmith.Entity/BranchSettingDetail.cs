using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class BranchSettingDetail
    {
        public Guid BranchID { get; set; }
        public bool IsFranchise { get; set; }
        public bool IsReservationOn { get; set; }
        public bool IsOrderBookingOn { get; set; }
        public bool IsAutoAcceptOrderOn { get; set; }
        public bool IsAutoRoundOffTotalOn { get; set; }
        public int TaxGroupId { get; set; }
        public bool IsDemoVersion { get; set; }
        public string ExpireDate { get; set; }
        public string DemoCode { get; set; }
        public string Mode { get; set; }
        public int IsUPStream { get; set; } = 0;
    }
}
