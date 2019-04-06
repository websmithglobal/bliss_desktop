using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class SubFeatureDetail
    {

        #region Constructors

        public SubFeatureDetail()
        {
        }
        #endregion

        #region Private Fields

        private int _SubFeatureCode;
        private string _SubFeatureName;
        private int _SubFeatureDetail_Id;
        private int _FeatureDetail_Id;
        private string _Mode;
        #endregion

        #region Public Properties

        public int SubFeatureCode
        {
            get { return _SubFeatureCode; }
            set { _SubFeatureCode = value; }
        }
        public string SubFeatureName
        {
            get { return _SubFeatureName; }
            set { _SubFeatureName = value; }
        }
        public int SubFeatureDetail_Id
        {
            get { return _SubFeatureDetail_Id; }
            set { _SubFeatureDetail_Id = value; }
        }
        public int FeatureDetail_Id
        {
            get { return _FeatureDetail_Id; }
            set { _FeatureDetail_Id = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
