using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class RightDetail
    {

        #region Constructors

        public RightDetail()
        {
        }
        #endregion

        #region Private Fields

        private int _RightCode;
        private string _RightName;
        private int _SubFeatureDetail_Id;
        private string _Mode;
        #endregion

        #region Public Properties

        public int RightCode
        {
            get { return _RightCode; }
            set { _RightCode = value; }
        }
        public string RightName
        {
            get { return _RightName; }
            set { _RightName = value; }
        }
        public int SubFeatureDetail_Id
        {
            get { return _SubFeatureDetail_Id; }
            set { _SubFeatureDetail_Id = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
