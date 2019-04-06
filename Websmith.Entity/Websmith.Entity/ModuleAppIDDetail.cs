using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ModuleAppIDDetail
    {

        #region Constructors

        public ModuleAppIDDetail()
        {
        }
        #endregion

        #region Private Fields

        private string _AppID;
        private string _DeviceName;
        private int _ModuleMasterDetail_Id;
        private string _Mode;
        #endregion

        #region Public Properties

        public string AppID
        {
            get { return _AppID; }
            set { _AppID = value; }
        }
        public string DeviceName
        {
            get { return _DeviceName; }
            set { _DeviceName = value; }
        }
        public int ModuleMasterDetail_Id
        {
            get { return _ModuleMasterDetail_Id; }
            set { _ModuleMasterDetail_Id = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
