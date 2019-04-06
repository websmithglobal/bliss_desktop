using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class TableMasterDetail
    {
        #region Public Fields
        public Guid OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public decimal PayableAmount { get; set; }
        #endregion

        #region Private Fields

        private Guid _TableID;
        private Guid _EmployeeID;
        private string _TableName;
        private int _NoOfSeats;
        private string _Location;
        private Guid _ClassID;
        private int _Sort;
        private int _RootObject_Id;
        private int _StatusID;
        private string _Mode;
        public int IsUPStream { get; set; } = 0;
        #endregion

        #region Public Properties

        public Guid TableID
        {
            get { return _TableID; }
            set { _TableID = value; }
        }
        public Guid EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        public int NoOfSeats
        {
            get { return _NoOfSeats; }
            set { _NoOfSeats = value; }
        }
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }
        public Guid ClassID
        {
            get { return _ClassID; }
            set { _ClassID = value; }
        }
        public int Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }
        public int RootObject_Id
        {
            get { return _RootObject_Id; }
            set { _RootObject_Id = value; }
        }
        public int StatusID
        {
            get { return _StatusID; }
            set { _StatusID = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion

        
    }
}
