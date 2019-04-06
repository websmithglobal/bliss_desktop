using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class TillManage
    {
        #region Private Fields

        private Guid _TillID;
        private Guid _EmployeeID;
        private decimal _PayIn;
        private decimal _PayOut;
        private decimal _Cash;
        private int _Currency5;
        private int _Currency10;
        private int _Currency20;
        private int _Currency50;
        private int _Currency100;
        private int _Currency200;
        private int _Currency500;
        private int _Currency1000;
        private int _Currency2000;
        private decimal _StartCash;
        private decimal _ExpectedCash;
        private decimal _EndCash;
        private decimal _Difference;
        private string _StartDateTime;
        private string _EndDateTime;
        private bool _IsTillDone;
        private bool _UpStreamStatus;
        private string _EnrtyDate;
        public string Mode { get; set; }
        public Guid RUserID { get; set; }
        public int RuserType { get; set; }
        #endregion

        #region Public Properties

        public Guid TillID
        {
            get { return _TillID; }
            set { _TillID = value; }
        }
        public Guid EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        public decimal PayIn
        {
            get { return _PayIn; }
            set { _PayIn = value; }
        }
        public decimal PayOut
        {
            get { return _PayOut; }
            set { _PayOut = value; }
        }
        public decimal Cash
        {
            get { return _Cash; }
            set { _Cash = value; }
        }
        public int Currency5
        {
            get { return _Currency5; }
            set { _Currency5 = value; }
        }
        public int Currency10
        {
            get { return _Currency10; }
            set { _Currency10 = value; }
        }
        public int Currency20
        {
            get { return _Currency20; }
            set { _Currency20 = value; }
        }
        public int Currency50
        {
            get { return _Currency50; }
            set { _Currency50 = value; }
        }
        public int Currency100
        {
            get { return _Currency100; }
            set { _Currency100 = value; }
        }
        public int Currency200
        {
            get { return _Currency200; }
            set { _Currency200 = value; }
        }
        public int Currency500
        {
            get { return _Currency500; }
            set { _Currency500 = value; }
        }
        public int Currency1000
        {
            get { return _Currency1000; }
            set { _Currency1000 = value; }
        }
        public int Currency2000
        {
            get { return _Currency2000; }
            set { _Currency2000 = value; }
        }
        public decimal StartCash
        {
            get { return _StartCash; }
            set { _StartCash = value; }
        }
        public decimal ExpectedCash
        {
            get { return _ExpectedCash; }
            set { _ExpectedCash = value; }
        }
        public decimal EndCash
        {
            get { return _EndCash; }
            set { _EndCash = value; }
        }
        public decimal Difference
        {
            get { return _Difference; }
            set { _Difference = value; }
        }
        public string StartDateTime
        {
            get { return _StartDateTime; }
            set { _StartDateTime = value; }
        }
        public string EndDateTime
        {
            get { return _EndDateTime; }
            set { _EndDateTime = value; }
        }
        public bool IsTillDone
        {
            get { return _IsTillDone; }
            set { _IsTillDone = value; }
        }
        public bool UpStreamStatus
        {
            get { return _UpStreamStatus; }
            set { _UpStreamStatus = value; }
        }
        public string EnrtyDate
        {
            get { return _EnrtyDate; }
            set { _EnrtyDate = value; }
        }
        #endregion
    }

    public class TillManageUpStream
    {
        public Guid TillID { get; set; }
        public string StartDate { get; set; } = "";
        public string FromDate { get; set; } = "";
        public string StartCash { get; set; } = "";
        public string PayIn { get; set; } = "";
        public string PayOut { get; set; } = "";
        public string Cash { get; set; } = "";
        public string Dispense1 { get; set; } = "";
        public string Dispense2 { get; set; } = "";
        public string Dispense3 { get; set; } = "";
        public string Dispense4 { get; set; } = "";
        public string Dispense5 { get; set; } = "";
        public string ExpectedCash { get; set; } = "";
        public string EndingCash { get; set; } = "";
        public string Difference { get; set; } = "";
        public Guid EmployeeID { get; set; }
        public Guid RUserID { get; set; }
        public string RuserType { get; set; } = "";
        public string Mode { get; set; } = "";
        public string ProductName { get; set; } = "";
    }
}
