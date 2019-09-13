using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class TimeSheetWiseDiscount
    {
        #region Private Fields
        private DateTime _FromTime;
        private DateTime _ToTime;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private int _Day;
        private int _DiscountMasterDetail_Id;
        private string _Mode;
        public int IsUPStream { get; set; } = 0;
        #endregion

        #region Public Properties

        public DateTime FromTime
        {
            get { return _FromTime; }
            set { _FromTime = value; }
        }
        public DateTime ToTime
        {
            get { return _ToTime; }
            set { _ToTime = value; }
        }
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        public int Day
        {
            get { return _Day; }
            set { _Day = value; }
        }
        public int DiscountMasterDetail_Id
        {
            get { return _DiscountMasterDetail_Id; }
            set { _DiscountMasterDetail_Id = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
