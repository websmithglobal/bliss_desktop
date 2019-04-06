using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ViewKOTRoutingPrint
    {
        #region Private Fields
        private Guid _TransactionID;
        private Guid _OrderID;
        private Guid _CategoryID;
        private string _CategoryName;
        private Guid _ProductID;
        private string _ProductName;
        private Guid _EmployeeID;
        private string _EmpName;
        private string _EmpCode;
        private Guid _DeviceID;
        private string _DeviceName;
        private Guid _PrinterID;
        private string _PrinterName;
        private string _PrinterIP;
        private int _Quantity;
        private string _SpecialRequest;
        private string _OrderNo;
        private DateTime _OrderDate;
        private Guid _CustomerID;
        private string _Name;
        private int _DeliveryType;
        private string _DeliveryTypeName;
        private Guid _TableID;
        private string _TableName;
        private int _OrderActions;
        private int _TokenNo;
        private int _OrderStatus;
        private int _IsPrint;
        private int _IsSendToKitchen;
        private int _KOTCount;
        private int _HeaderStatus;
        private string _Mode;
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
        public Guid CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
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
        public Guid EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        public string EmpName
        {
            get { return _EmpName; }
            set { _EmpName = value; }
        }
        public string EmpCode
        {
            get { return _EmpCode; }
            set { _EmpCode = value; }
        }
        public Guid DeviceID
        {
            get { return _DeviceID; }
            set { _DeviceID = value; }
        }
        public string DeviceName
        {
            get { return _DeviceName; }
            set { _DeviceName = value; }
        }
        public Guid PrinterID
        {
            get { return _PrinterID; }
            set { _PrinterID = value; }
        }
        public string PrinterName
        {
            get { return _PrinterName; }
            set { _PrinterName = value; }
        }
        public string PrinterIP
        {
            get { return _PrinterIP; }
            set { _PrinterIP = value; }
        }
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public string SpecialRequest
        {
            get { return _SpecialRequest; }
            set { _SpecialRequest = value; }
        }
        public string OrderNo
        {
            get { return _OrderNo; }
            set { _OrderNo = value; }
        }
        public DateTime OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
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
        public int OrderActions
        {
            get { return _OrderActions; }
            set { _OrderActions = value; }
        }
        public int TokenNo
        {
            get { return _TokenNo; }
            set { _TokenNo = value; }
        }
        public int OrderStatus
        {
            get { return _OrderStatus; }
            set { _OrderStatus = value; }
        }
        public int IsPrint
        {
            get { return _IsPrint; }
            set { _IsPrint = value; }
        }
        public int IsSendToKitchen
        {
            get { return _IsSendToKitchen; }
            set { _IsSendToKitchen = value; }
        }
        public int KOTCount
        {
            get { return _KOTCount; }
            set { _KOTCount = value; }
        }
        public int HeaderStatus
        {
            get { return _HeaderStatus; }
            set { _HeaderStatus = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
