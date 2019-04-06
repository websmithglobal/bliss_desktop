using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class FeatureDetail
    {
        #region Private Fields

        private int _FeatureCode;
        private string _FeatureName;
        private int _FeatureDetail_Id;
        private int _RoleMasterDetail_Id;
        private string _Mode;
        #endregion

        #region Public Properties

        public int FeatureCode
        {
            get { return _FeatureCode; }
            set { _FeatureCode = value; }
        }
        public string FeatureName
        {
            get { return _FeatureName; }
            set { _FeatureName = value; }
        }
        public int FeatureDetail_Id
        {
            get { return _FeatureDetail_Id; }
            set { _FeatureDetail_Id = value; }
        }
        public int RoleMasterDetail_Id
        {
            get { return _RoleMasterDetail_Id; }
            set { _RoleMasterDetail_Id = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
