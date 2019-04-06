using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class CategoryWiseProduct
    {
        #region Private Fields

        private Guid _DiscountID;
        private Guid _ProductID;
        private Guid _CategoryID;
        private string _ProductName;
        private decimal _Price;
        private string _ImgPath;
        private bool _IsUrl;
        private string _Calorie;
        private string _ShortDescription;
        private string _Description;
        private bool _IsNonVeg;
        private bool _IsTrendingItem;
        private string _ApproxCookingTime;
        private bool _IsAellergic;
        private string _Extras;
        private bool _IsVisibleToB2C;
        private string _ExpiryDateFrom;
        private string _ExpiryDateTo;
        private Guid _StationID;
        private string _SuggestiveItems;
        private bool _IsCold;
        private bool _IsDrink;
        private int _DiningOptions;
        private int _AllowPriceOverride;
        private bool _IsAgeValidation;
        private int _AgeForValidation;
        private decimal _OverridePrice;
        private bool _IsCombo;
        private bool _IsDisplayModifire;
        private string _ProductCode;
        private decimal _TaxPercentage;
        private int _Sort;
        private int _Priority;
        private int _CategoryWiseProduct_Id;
        private string _CategoryName;
        private Guid _RUserID;
        private int _RUserType;
        private string _Mode;
        public int IsUPStream { get; set; } = 0;
        #endregion

        #region Public Properties

        public Guid DiscountID
        {
            get { return _DiscountID; }
            set { _DiscountID = value; }
        }
        public Guid ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        public Guid CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        public string ImgPath
        {
            get { return _ImgPath; }
            set { _ImgPath = value; }
        }
        public bool IsUrl
        {
            get { return _IsUrl; }
            set { _IsUrl = value; }
        }
        public string Calorie
        {
            get { return _Calorie; }
            set { _Calorie = value; }
        }
        public string ShortDescription
        {
            get { return _ShortDescription; }
            set { _ShortDescription = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public bool IsNonVeg
        {
            get { return _IsNonVeg; }
            set { _IsNonVeg = value; }
        }
        public bool IsTrendingItem
        {
            get { return _IsTrendingItem; }
            set { _IsTrendingItem = value; }
        }
        public string ApproxCookingTime
        {
            get { return _ApproxCookingTime; }
            set { _ApproxCookingTime = value; }
        }
        public bool IsAellergic
        {
            get { return _IsAellergic; }
            set { _IsAellergic = value; }
        }
        public string Extras
        {
            get { return _Extras; }
            set { _Extras = value; }
        }
        public bool IsVisibleToB2C
        {
            get { return _IsVisibleToB2C; }
            set { _IsVisibleToB2C = value; }
        }
        public string ExpiryDateFrom
        {
            get { return _ExpiryDateFrom; }
            set { _ExpiryDateFrom = value; }
        }
        public string ExpiryDateTo
        {
            get { return _ExpiryDateTo; }
            set { _ExpiryDateTo = value; }
        }
        public Guid StationID
        {
            get { return _StationID; }
            set { _StationID = value; }
        }
        public string SuggestiveItems
        {
            get { return _SuggestiveItems; }
            set { _SuggestiveItems = value; }
        }
        public bool IsCold
        {
            get { return _IsCold; }
            set { _IsCold = value; }
        }
        public bool IsDrink
        {
            get { return _IsDrink; }
            set { _IsDrink = value; }
        }
        public int DiningOptions
        {
            get { return _DiningOptions; }
            set { _DiningOptions = value; }
        }
        public int AllowPriceOverride
        {
            get { return _AllowPriceOverride; }
            set { _AllowPriceOverride = value; }
        }
        public bool IsAgeValidation
        {
            get { return _IsAgeValidation; }
            set { _IsAgeValidation = value; }
        }
        public int AgeForValidation
        {
            get { return _AgeForValidation; }
            set { _AgeForValidation = value; }
        }
        public decimal OverridePrice
        {
            get { return _OverridePrice; }
            set { _OverridePrice = value; }
        }
        public bool IsCombo
        {
            get { return _IsCombo; }
            set { _IsCombo = value; }
        }
        public bool IsDisplayModifire
        {
            get { return _IsDisplayModifire; }
            set { _IsDisplayModifire = value; }
        }
        public string ProductCode
        {
            get { return _ProductCode; }
            set { _ProductCode = value; }
        }
        public decimal TaxPercentage
        {
            get { return _TaxPercentage; }
            set { _TaxPercentage = value; }
        }
        public int Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }
        public int Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }
        public int CategoryWiseProduct_Id
        {
            get { return _CategoryWiseProduct_Id; }
            set { _CategoryWiseProduct_Id = value; }
        }
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }
        public Guid RUserID
        {
            get { return _RUserID; }
            set { _RUserID = value; }
        }
        public int RUserType
        {
            get { return _RUserType; }
            set { _RUserType = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }

    public class ProductUpStream
    {
        #region Up Stream Properties
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string ImgPath { get; set; }
        public string Description { get; set; }
        public string RUserID { get; set; }
        public int RUserType { get; set; }
        public int IsDrink { get; set; }
        public string CategoryID { get; set; }
        public string ProductCode { get; set; }
        public string Mode { get; set; }
        public int IsUPStream { get; set; } = 0;
        #endregion
    }
}
