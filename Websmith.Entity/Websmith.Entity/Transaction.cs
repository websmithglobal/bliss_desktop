using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class Transaction
    {
        #region Private Fields
        private Guid _EmployeeID;
        private Guid _CategoryID;
        private Guid _TransactionID;
        private Guid _OrderID;
        private Guid _ProductID;
        private string _ProductName;
        private int _Quantity;
        private decimal _Rate;
        private decimal _TotalAmount;
        private int _IsSendToKitchen = 0;
        private int _IsUPStream;
        private int _Sort;
        private string _SpecialRequest;
        private string _StartDate;
        private string _EndDate;
        private string _Mode;
        public Guid RUserID { get; set; }
        public int RUserType { get; set; }
        public string OrderDateFrom { get; set; }
        public string OrderDateTo { get; set; }
        public string CategoryName { get; set; }
        #endregion

        #region Public Properties
        public Guid EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        public Guid CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        public Guid TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        public Guid OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }
        public Guid ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public decimal Rate
        {
            get { return _Rate; }
            set { _Rate = value; }
        }
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        public int Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }
        public int IsSendToKitchen
        {
            get { return _IsSendToKitchen; }
            set { _IsSendToKitchen = value; }
        }
        public int IsUPStream
        {
            get { return _IsUPStream; }
            set { _IsUPStream = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        public string SpecialRequest
        {
            get{return _SpecialRequest;}
            set{_SpecialRequest = value;}
        }
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        #endregion
    }
}
