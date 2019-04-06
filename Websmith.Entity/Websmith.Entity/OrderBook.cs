using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class OrderBook
    {
        public static decimal DiscAmountOrPercent = 0;
        public static int DiscAmountOrPercentID = 0;
        public static string DiscRemark = "";

        #region Private Fields

        private Guid _OrderID;
        private string _OrderNo;
        private string _OrderDate;
        private Guid _EmployeeID;
        private Guid _CustomerID;
        private string _Name;
        private string _MobileNo;
        private int _DeliveryType;
        private string _DeliveryTypeName;
        private string _PaymentMethod;
        private Guid _TableID;
        private string _TableName;
        private decimal _SubTotal;
        private decimal _ExtraCharge;
        private string _TaxLabel1;
        private decimal _TaxPercent1;
        private decimal _SGSTAmount;
        private string _TaxLabel2;
        private decimal _TaxPercent2;
        private decimal _CGSTAmount;
        private decimal _TotalTax;
        private decimal _Discount;
        private decimal _TipGratuity;
        private decimal _PayableAmount;
        private int _TableStatusID;
        private int _OrderActions;
        private string _OrderActionsName;
        private string _OrderSpecialRequest;
        private int _DiscountType;
        private string _DiscountRemark;
        private string _StartTime;
        private string _EndTime;
        private string _Mode;
        private string _SearchKey;
        private Guid _DiscountID = new Guid("00000000-0000-0000-0000-000000000000");
        private decimal _DiscountPer;
        private decimal _DeliveryCharge;
        private Int64 _TokenNo;
        private string _Address;
        private Int32 _OrderStatus;
        private int _IsSendToKitchen = 0;
        private int _HeaderStatus = 0;
        public string OrderDateFrom { get; set; }
        public string OrderDateTo { get; set; }
        public Guid RUserID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid ProductID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid CategoryID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public int RUserType { get; set; } = 0;
        public int IsPrint { get; set; } = 0;
        public string PaymentMethodText { get; set; }
        #endregion

        #region Public Properties

        public Guid OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }
        public string OrderNo
        {
            get { return _OrderNo; }
            set { _OrderNo = value; }
        }
        public string OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
        }
        public Guid EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        public Guid CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        public int DeliveryType
        {
            get { return _DeliveryType; }
            set { _DeliveryType = value; }
        }
        public string DeliveryTypeName
        {
            get { return _DeliveryTypeName; }
            set { _DeliveryTypeName = value; }
        }
        public string PaymentMethod
        {
            get { return _PaymentMethod; }
            set { _PaymentMethod = value; }
        }
        public Guid TableID
        {
            get { return _TableID; }
            set { _TableID = value; }
        }
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        public decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; }
        }
        public decimal ExtraCharge
        {
            get { return _ExtraCharge; }
            set { _ExtraCharge = value; }
        }
        public decimal SGSTAmount
        {
            get { return _SGSTAmount; }
            set { _SGSTAmount = value; }
        }
        public decimal CGSTAmount
        {
            get { return _CGSTAmount; }
            set { _CGSTAmount = value; }
        }
        public decimal TotalTax
        {
            get { return _TotalTax; }
            set { _TotalTax = value; }
        }
        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }
        public decimal TipGratuity
        {
            get { return _TipGratuity; }
            set { _TipGratuity = value; }
        }
        public decimal PayableAmount
        {
            get { return _PayableAmount; }
            set { _PayableAmount = value; }
        }
        public string SearchKey
        {
            get { return _SearchKey; }
            set { _SearchKey = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        public int TableStatusID
        {
            get { return _TableStatusID; }
            set { _TableStatusID = value; }
        }
        public int OrderActions
        {
            get { return _OrderActions; }
            set { _OrderActions = value; }
        }
        public string OrderActionsName
        {
            get { return _OrderActionsName; }
            set { _OrderActionsName = value; }
        }
        public string OrderSpecialRequest
        {
            get { return _OrderSpecialRequest; }
            set { _OrderSpecialRequest = value; }
        }
        public int DiscountType
        {
            get { return _DiscountType; }
            set { _DiscountType = value; }
        }
        public string DiscountRemark
        {
            get { return _DiscountRemark; }
            set { _DiscountRemark = value; }
        }
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private List<RecentOrderFilterComboItem> _roComboItem;
        public List<RecentOrderFilterComboItem> RecentOrderComboItem
        {
            get
            {
                if (_roComboItem == null)
                {
                    _roComboItem = new List<RecentOrderFilterComboItem>();
                    _roComboItem.Add(new RecentOrderFilterComboItem { ID = 0, Name = "Today" });
                    _roComboItem.Add(new RecentOrderFilterComboItem { ID = 1, Name = "Yesterday" });
                    _roComboItem.Add(new RecentOrderFilterComboItem { ID = 2, Name = "Custom" });
                }
                return _roComboItem;
            }
        }
        public string StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        public string EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        public string TaxLabel1
        {
            get
            {
                return _TaxLabel1;
            }

            set
            {
                _TaxLabel1 = value;
            }
        }

        public decimal TaxPercent1
        {
            get
            {
                return _TaxPercent1;
            }

            set
            {
                _TaxPercent1 = value;
            }
        }

        public string TaxLabel2
        {
            get
            {
                return _TaxLabel2;
            }

            set
            {
                _TaxLabel2 = value;
            }
        }

        public decimal TaxPercent2
        {
            get
            {
                return _TaxPercent2;
            }

            set
            {
                _TaxPercent2 = value;
            }
        }

        public Guid DiscountID
        {
            get
            {
                return _DiscountID;
            }

            set
            {
                _DiscountID = value;
            }
        }

        public decimal DiscountPer
        {
            get
            {
                return _DiscountPer;
            }

            set
            {
                _DiscountPer = value;
            }
        }

        public decimal DeliveryCharge
        {
            get
            {
                return _DeliveryCharge;
            }

            set
            {
                _DeliveryCharge = value;
            }
        }

        public Int64 TokenNo
        {
            get
            {
                return _TokenNo;
            }

            set
            {
                _TokenNo = value;
            }
        }

        public Int32 OrderStatus
        {
            get{return _OrderStatus;}
            set{_OrderStatus = value;}
        }
        public int IsSendToKitchen
        {
            get { return _IsSendToKitchen; }
            set { _IsSendToKitchen = value; }
        }

        public int HeaderStatus
        {
            get { return _HeaderStatus; }
            set { _HeaderStatus = value; }
        }
        #endregion
    }

    public class RecentOrderFilterComboItem
    {
        public int ID { set; get; }
        public string Name { set; get; }
    }
}
