using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class TillPayOut
    {
        
        #region Private Fields

        private Guid _PayOutID;
        private Guid _TillID;
        private Guid _EmployeeID;
        private decimal _Amount;
        private string _Reason;
        private string _EntryDateTime;
        #endregion

        #region Public Properties

        public Guid PayOutID
        {
            get { return _PayOutID; }
            set { _PayOutID = value; }
        }
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
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        public string Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }
        public string EntryDateTime
        {
            get { return _EntryDateTime; }
            set { _EntryDateTime = value; }
        }
        public string Mode { get; set; }
        #endregion
    }
}
