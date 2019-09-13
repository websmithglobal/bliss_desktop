using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class BranchMasterSetting
    {
        #region Private Fields
        private Guid _BranchID;
        private Guid _RestaurantID;
        private string _ContactNoForService;
        private int _DeliveryCharges;
        private string _DeliveryTime;
        private string _PickupTime;
        private string _CurrencyName;
        private string _CurrencySymbol;
        private string _WorkingDays;
        private string _TagLine;
        private string _Footer;
        private int _DeliveryAreaRedius;
        private string _DeliveryAreaTitle;
        private int _DistanceType;
        private string _DistanceName;
        private int _FreeDeliveryUpto;
        private string _BranchName;
        private string _BranchEmailID;
        private string _MobileNo;
        private DateTime _LastSyncDate;
        private string _VatNo;
        private string _CSTNo;
        private string _ServiceTaxNo;
        private string _TinGSTNo;
        private string _Address;
        private string _SubAreaStreet;
        private string _PinCode;
        private string _VersionCode;
        private int _BranchMasterSetting_Id;
        private int _RootObject_Id;
        private string _Mode;
        public int IsUPStream { get; set; } = 0;
        public BranchSettingDetail BranchSettingDetail { get; set; }
        public List<object> UserLanguagDetail { get; set; }
        #endregion

        #region BranchSettingDetail
        public bool IsFranchise { get; set; }
        public bool IsReservationOn { get; set; }
        public bool IsOrderBookingOn { get; set; }
        public bool IsAutoAcceptOrderOn { get; set; }
        public bool IsAutoRoundOffTotalOn { get; set; }
        public int TaxGroupId { get; set; }
        public bool IsDemoVersion { get; set; }
        public string ExpireDate { get; set; }
        #endregion

        #region Public Properties

        public Guid BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value; }
        }
        public Guid RestaurantID
        {
            get { return _RestaurantID; }
            set { _RestaurantID = value; }
        }
        public string ContactNoForService
        {
            get { return _ContactNoForService; }
            set { _ContactNoForService = value; }
        }
        public int DeliveryCharges
        {
            get { return _DeliveryCharges; }
            set { _DeliveryCharges = value; }
        }
        public string DeliveryTime
        {
            get { return _DeliveryTime; }
            set { _DeliveryTime = value; }
        }
        public string PickupTime
        {
            get { return _PickupTime; }
            set { _PickupTime = value; }
        }
        public string CurrencyName
        {
            get { return _CurrencyName; }
            set { _CurrencyName = value; }
        }
        public string CurrencySymbol
        {
            get { return _CurrencySymbol; }
            set { _CurrencySymbol = value; }
        }
        public string WorkingDays
        {
            get { return _WorkingDays; }
            set { _WorkingDays = value; }
        }
        public string TagLine
        {
            get { return _TagLine; }
            set { _TagLine = value; }
        }
        public string Footer
        {
            get { return _Footer; }
            set { _Footer = value; }
        }
        public int DeliveryAreaRedius
        {
            get { return _DeliveryAreaRedius; }
            set { _DeliveryAreaRedius = value; }
        }
        public string DeliveryAreaTitle
        {
            get { return _DeliveryAreaTitle; }
            set { _DeliveryAreaTitle = value; }
        }
        public int DistanceType
        {
            get { return _DistanceType; }
            set { _DistanceType = value; }
        }
        public string DistanceName
        {
            get { return _DistanceName; }
            set { _DistanceName = value; }
        }
        public int FreeDeliveryUpto
        {
            get { return _FreeDeliveryUpto; }
            set { _FreeDeliveryUpto = value; }
        }
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        public string BranchEmailID
        {
            get { return _BranchEmailID; }
            set { _BranchEmailID = value; }
        }
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        public DateTime LastSyncDate
        {
            get { return _LastSyncDate; }
            set { _LastSyncDate = value; }
        }
        public string VatNo
        {
            get { return _VatNo; }
            set { _VatNo = value; }
        }
        public string CSTNo
        {
            get { return _CSTNo; }
            set { _CSTNo = value; }
        }
        public string ServiceTaxNo
        {
            get { return _ServiceTaxNo; }
            set { _ServiceTaxNo = value; }
        }
        public string TinGSTNo
        {
            get { return _TinGSTNo; }
            set { _TinGSTNo = value; }
        }
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        public string SubAreaStreet
        {
            get { return _SubAreaStreet; }
            set { _SubAreaStreet = value; }
        }
        public string PinCode
        {
            get { return _PinCode; }
            set { _PinCode = value; }
        }
        public string VersionCode
        {
            get { return _VersionCode; }
            set { _VersionCode = value; }
        }
        public int BranchMasterSetting_Id
        {
            get { return _BranchMasterSetting_Id; }
            set { _BranchMasterSetting_Id = value; }
        }
        public int RootObject_Id
        {
            get { return _RootObject_Id; }
            set { _RootObject_Id = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode  = value; }
        }
        #endregion
    }
}
