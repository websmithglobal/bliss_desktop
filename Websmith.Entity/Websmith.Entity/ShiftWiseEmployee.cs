using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ShiftWiseEmployee
    {
        #region Private Fields
        private Guid _ShiftWiseEmployeeID;
        private Guid _EmployeeID;
        private Guid _ShiftID;
        private Guid _RUserID;
        private int _RUserType;
        private DateTime _CreatedDate;
        private bool _IsStatus;
        private int _ShiftDay;
        private string _Mode;
        public int IsUPStream { get; set; } = 0;
        #endregion

        #region Public Properties

        public Guid ShiftWiseEmployeeID
        {
            get { return _ShiftWiseEmployeeID; }
            set { _ShiftWiseEmployeeID = value; }
        }
        public Guid EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        public Guid ShiftID
        {
            get { return _ShiftID; }
            set { _ShiftID = value; }
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
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        public bool IsStatus
        {
            get { return _IsStatus; }
            set { _IsStatus = value; }
        }
        public int ShiftDay
        {
            get { return _ShiftDay; }
            set { _ShiftDay = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
