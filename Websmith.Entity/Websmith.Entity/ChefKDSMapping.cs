using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ChefKDSMapping
    {

        #region Constructors

        public ChefKDSMapping()
        {
        }
        #endregion

        #region Private Fields

        private Guid _ChefKDSMappingID;
        private Guid _EmployeeID;
        private Guid _DeviceID;
        private string _Mode;
        #endregion

        #region Public Properties

        public Guid ChefKDSMappingID
        {
            get { return _ChefKDSMappingID; }
            set { _ChefKDSMappingID = value; }
        }
        public Guid EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        public Guid DeviceID
        {
            get { return _DeviceID; }
            set { _DeviceID = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
