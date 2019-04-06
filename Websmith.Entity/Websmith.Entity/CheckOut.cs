using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class CheckOut
    {
        public decimal PayableAmount { get; set; }
        public string CRMMethod { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class PaymentDetail
    {
        public int CRMMethod { get; set; }
        public int PaymentMethod { get; set; }

        public string OrderID { get; set; }
        public string CustomerID { get; set; }
        public string TableID { get; set; }
        public decimal PayAmount { get; set; }

        public decimal SalesAmountTotal { get; set; }

        public decimal DiscManual { get; set; }
        public decimal DiscReward { get; set; }
        public decimal DiscStamp { get; set; }
        public decimal DiscTotal { get; set; }

        public decimal PMGiftCard { get; set; }
        public decimal PMCash { get; set; }
        public decimal PMCreditCard { get; set; }
        public decimal PMCheque { get; set; }
        public decimal PMTotal { get; set; }

        public decimal SumSubTotal { get; set; }
        public decimal SumTaxTotal { get; set; }
        public decimal SumTotal { get; set; }

        public decimal OTReceived { get; set; }
        public decimal OTChange { get; set; }
        public decimal OTTip { get; set; }
        public decimal OTBalance { get; set; }

        public decimal Discount { get; set; }
        public decimal DiscountType { get; set; }
        public decimal DiscountPer { get; set; }

        public decimal ExtraCharge { get; set; }
        public decimal Tax1 { get; set; }
        public decimal Tax2 { get; set; }
    }
}
