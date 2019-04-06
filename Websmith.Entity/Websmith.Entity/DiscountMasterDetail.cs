using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class DiscountMasterDetail
    {
        public static Guid OrderID;
        public static Guid SelectedDiscountID = new Guid("00000000-0000-0000-0000-000000000000");
        public static int DiscountTypeID=0;
        public static decimal DiscountAmtPer=0;
        public static string DiscountRemark = "";

        #region Private Fields

        private Guid _DiscountID;
        private string _DiscountName;
        private int _DiscountType;
        private string _DiscountTypeName;
        private decimal _AmountOrPercentage;
        private int _QualificationType;
        private bool _IsTaxed;
        private string _Barcode;
        private string _DiscountCode;
        private bool _PasswordRequired;
        private bool _DisplayOnPOS;
        private bool _AutoApply;
        private bool _DisplayToCustomer;
        private bool _IsTimeBase;
        private bool _IsLoyaltyRewards;
        private int _DiscountMasterDetail_Id;
        private int _RootObject_Id;
        private string _Mode;
        public int IsUPStream { get; set; } = 0;
        #endregion

        #region Public Properties

        public Guid DiscountID
        {
            get { return _DiscountID; }
            set { _DiscountID = value; }
        }
        public string DiscountName
        {
            get { return _DiscountName; }
            set { _DiscountName = value; }
        }
        public int DiscountType
        {
            get { return _DiscountType; }
            set { _DiscountType = value; }
        }
        public string DiscountTypeName
        {
            get { return _DiscountTypeName; }
            set { _DiscountTypeName = value; }
        }
        public decimal AmountOrPercentage
        {
            get { return _AmountOrPercentage; }
            set { _AmountOrPercentage = value; }
        }
        public int QualificationType
        {
            get { return _QualificationType; }
            set { _QualificationType = value; }
        }
        public bool IsTaxed
        {
            get { return _IsTaxed; }
            set { _IsTaxed = value; }
        }
        public string Barcode
        {
            get { return _Barcode; }
            set { _Barcode = value; }
        }
        public string DiscountCode
        {
            get { return _DiscountCode; }
            set { _DiscountCode = value; }
        }
        public bool PasswordRequired
        {
            get { return _PasswordRequired; }
            set { _PasswordRequired = value; }
        }
        public bool DisplayOnPOS
        {
            get { return _DisplayOnPOS; }
            set { _DisplayOnPOS = value; }
        }
        public bool AutoApply
        {
            get { return _AutoApply; }
            set { _AutoApply = value; }
        }
        public bool DisplayToCustomer
        {
            get { return _DisplayToCustomer; }
            set { _DisplayToCustomer = value; }
        }
        public bool IsTimeBase
        {
            get { return _IsTimeBase; }
            set { _IsTimeBase = value; }
        }
        public bool IsLoyaltyRewards
        {
            get { return _IsLoyaltyRewards; }
            set { _IsLoyaltyRewards = value; }
        }
        public int DiscountMasterDetail_Id
        {
            get { return _DiscountMasterDetail_Id; }
            set { _DiscountMasterDetail_Id = value; }
        }
        public int RootObject_Id
        {
            get { return _RootObject_Id; }
            set { _RootObject_Id = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
