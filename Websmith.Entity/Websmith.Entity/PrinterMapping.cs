using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class PrinterMapping
    {
        #region Private Fields
        private Guid _PrinterMappingID;
        private Guid _DeviceID;
        private string _DeviceName;
        private Guid _PrinterID;
        private string _PrinterName;
        private string _DeviceIP;
        private int _PartID;
        private int _DeviceTypeID;
        private string _Mode;
        #endregion

        #region Public Properties

        public Guid PrinterMappingID
        {
            get { return _PrinterMappingID; }
            set { _PrinterMappingID = value; }
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
        public string DeviceIP
        {
            get { return _DeviceIP; }
            set { _DeviceIP = value; }
        }
        public int PartID
        {
            get { return _PartID; }
            set { _PartID = value; }
        }
        public int DeviceTypeID
        {
            get { return _DeviceTypeID; }
            set { _DeviceTypeID = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
