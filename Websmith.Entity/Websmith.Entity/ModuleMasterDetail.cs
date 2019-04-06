using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ModuleMasterDetail
    {
        #region Private Fields
        private Guid _ModuleID;
        private string _ModuleName;
        private int _NoOfModule;
        private int _ModuleMasterDetail_Id;
        public List<object> RoleMasterDetail { get; set; }
        public List<object> ModuleAppIDDetail { get; set; }
        private string _Mode;
        #endregion

        #region Public Properties

        public Guid ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }
        public string ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        }
        public int NoOfModule
        {
            get { return _NoOfModule; }
            set { _NoOfModule = value; }
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
