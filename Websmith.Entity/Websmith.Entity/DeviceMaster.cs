using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class DeviceMaster
    {
        public enum DeviceTypeName
        {
            POS = 1,
            KDS = 2,
            PRINTER = 3
        }
        
        #region Private Fields
        private Guid _DeviceID;
        private string _DeviceName;
        private string _DeviceIP;
        private string _DeviceType;
        private int _DeviceTypeID;
        private int _DeviceStatus;
        private string _Mode;
        #endregion

        #region Public Properties

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
        public string DeviceIP
        {
            get { return _DeviceIP; }
            set { _DeviceIP = value; }
        }
        public string DeviceType
        {
            get { return _DeviceType; }
            set { _DeviceType = value; }
        }
        public int DeviceTypeID
        {
            get { return _DeviceTypeID; }
            set { _DeviceTypeID = value; }
        }
        public int DeviceStatus
        {
            get { return _DeviceStatus; }
            set { _DeviceStatus = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
