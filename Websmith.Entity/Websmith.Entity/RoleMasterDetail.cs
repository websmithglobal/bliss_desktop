using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class RoleMasterDetail
    {
        #region Private Fields

        private Guid _RoleID;
        private string _RoleName;
        private int _RoleMasterDetail_Id;
        private int _ModuleMasterDetail_Id;
        private string _Mode;
        #endregion

        #region Public Properties

        public Guid RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
        public int RoleMasterDetail_Id
        {
            get { return _RoleMasterDetail_Id; }
            set { _RoleMasterDetail_Id = value; }
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
