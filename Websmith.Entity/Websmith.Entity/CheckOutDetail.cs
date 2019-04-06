using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class CheckOutDetail
    {
        #region Private Fields
        private Guid _TransactionID;
        private Guid _OrderID;
        private Guid _CustomerID;
        private Guid _TableID;
        private int _CRMMethod;
        private int _PaymentMethod;
        private decimal _OrderAmount;
        private decimal _PaidAmount;
        private decimal _ChangeAmount;
        private string _ChequeNo;
        private string _ChequeDate;
        private string _CardHolderName;
        private string _CardNumber;
        private string _ExpireDate;
        private string _CVV;
        private string _EntryDateTime;
        private Guid _RUserID;
        private int _RUserType;
        private int _IsUPStream;
        private bool _IsSwipe;
        private Guid _PaymentTransID;
        private Guid _PaymentID;
        private string _PaymentToken;
        private string _PaymentCardTypeID;
        private string _TransactionResult;
        private string _CustomerTransactionId;
        private string _TransactionAction;
        private string _TranstationMessage;
        private string _IsPartialAuthorized;
        private decimal _RequestedAmount;
        private string _SwipeStr;

        public int TableStatusID { get; set; }
        public int OrderActions { get; set; }
        public int OrderStatus { get; set; }
        public string Mode { get; set; }
        #endregion

        #region Public Properties

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
        public Guid CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
        public Guid TableID
        {
            get { return _TableID; }
            set { _TableID = value; }
        }
        public int CRMMethod
        {
            get { return _CRMMethod; }
            set { _CRMMethod = value; }
        }
        public int PaymentMethod
        {
            get { return _PaymentMethod; }
            set { _PaymentMethod = value; }
        }
        public decimal OrderAmount
        {
            get { return _OrderAmount; }
            set { _OrderAmount = value; }
        }
        public decimal PaidAmount
        {
            get { return _PaidAmount; }
            set { _PaidAmount = value; }
        }
        public decimal ChangeAmount
        {
            get { return _ChangeAmount; }
            set { _ChangeAmount = value; }
        }
        public string ChequeNo
        {
            get { return _ChequeNo; }
            set { _ChequeNo = value; }
        }
        public string ChequeDate
        {
            get { return _ChequeDate; }
            set { _ChequeDate = value; }
        }
        public string CardHolderName
        {
            get { return _CardHolderName; }
            set { _CardHolderName = value; }
        }
        public string CardNumber
        {
            get { return _CardNumber; }
            set { _CardNumber = value; }
        }
        public string ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        }
        public string CVV
        {
            get { return _CVV; }
            set { _CVV = value; }
        }
        public string EntryDateTime
        {
            get { return _EntryDateTime; }
            set { _EntryDateTime = value; }
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
        public int IsUPStream
        {
            get { return _IsUPStream; }
            set { _IsUPStream = value; }
        }
        public bool IsSwipe
        {
            get { return _IsSwipe; }
            set { _IsSwipe = value; }
        }
        public Guid PaymentTransID
        {
            get { return _PaymentTransID; }
            set { _PaymentTransID = value; }
        }
        public Guid PaymentID
        {
            get { return _PaymentID; }
            set { _PaymentID = value; }
        }
        public string PaymentToken
        {
            get { return _PaymentToken; }
            set { _PaymentToken = value; }
        }
        public string PaymentCardTypeID
        {
            get { return _PaymentCardTypeID; }
            set { _PaymentCardTypeID = value; }
        }
        public string TransactionResult
        {
            get { return _TransactionResult; }
            set { _TransactionResult = value; }
        }
        public string CustomerTransactionId
        {
            get { return _CustomerTransactionId; }
            set { _CustomerTransactionId = value; }
        }
        public string TransactionAction
        {
            get { return _TransactionAction; }
            set { _TransactionAction = value; }
        }
        public string TranstationMessage
        {
            get { return _TranstationMessage; }
            set { _TranstationMessage = value; }
        }
        public string IsPartialAuthorized
        {
            get { return _IsPartialAuthorized; }
            set { _IsPartialAuthorized = value; }
        }
        public decimal RequestedAmount
        {
            get { return _RequestedAmount; }
            set { _RequestedAmount = value; }
        }
        public string SwipeStr
        {
            get { return _SwipeStr; }
            set { _SwipeStr = value; }
        }
        #endregion
    }
}
