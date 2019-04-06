using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ShiftMaster
    {
        #region Private Fields
        private Guid _ShiftID;
        private string _ShiftName;
        private int _ShiftMaster_Id;
        private string _Mode;
        public List<ShiftMasterDetail> ShiftMasterDetail { get; set; }
        
        #endregion

        #region Public Properties

        public Guid ShiftID
        {
            get { return _ShiftID; }
            set { _ShiftID = value; }
        }
        public string ShiftName
        {
            get { return _ShiftName; }
            set { _ShiftName = value; }
        }
        public int ShiftMaster_Id
        {
            get { return _ShiftMaster_Id; }
            set { _ShiftMaster_Id = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
