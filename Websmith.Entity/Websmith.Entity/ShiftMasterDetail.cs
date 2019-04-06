using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ShiftMasterDetail
    {
        #region Private Fields
        private Guid _ShiftDetailsID;
        private string _ShiftFromTime;
        private string _ShiftToTime;
        private int _ShiftDay;
        private decimal _FirstSlot;
        private decimal _SecondSlot;
        private decimal _FinalSlot;
        private string _ShiftDiff;
        private int _ShiftMaster_ID;
        private string _Mode;
        #endregion

        #region Public Properties

        public Guid ShiftDetailsID
        {
            get { return _ShiftDetailsID; }
            set { _ShiftDetailsID = value; }
        }
        public string ShiftFromTime
        {
            get { return _ShiftFromTime; }
            set { _ShiftFromTime = value; }
        }
        public string ShiftToTime
        {
            get { return _ShiftToTime; }
            set { _ShiftToTime = value; }
        }
        public int ShiftDay
        {
            get { return _ShiftDay; }
            set { _ShiftDay = value; }
        }
        public decimal FirstSlot
        {
            get { return _FirstSlot; }
            set { _FirstSlot = value; }
        }
        public decimal SecondSlot
        {
            get { return _SecondSlot; }
            set { _SecondSlot = value; }
        }
        public decimal FinalSlot
        {
            get { return _FinalSlot; }
            set { _FinalSlot = value; }
        }
        public string ShiftDiff
        {
            get { return _ShiftDiff; }
            set { _ShiftDiff = value; }
        }
        public int ShiftMaster_ID
        {
            get { return _ShiftMaster_ID; }
            set { _ShiftMaster_ID = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
