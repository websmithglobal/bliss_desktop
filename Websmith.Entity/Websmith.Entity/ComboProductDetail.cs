using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ComboProductDetail
    {
        #region Private Fields

        private Guid _ProductID;
        private bool _IsDefault;
        private string _ProductName;
        private int _ComboDetail_Id;
        private string _Mode;
        #endregion

        #region Public Properties

        public Guid ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        public bool IsDefault
        {
            get { return _IsDefault; }
            set { _IsDefault = value; }
        }
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        public int ComboDetail_Id
        {
            get { return _ComboDetail_Id; }
            set { _ComboDetail_Id = value; }
        }
        public int IsUPStream { get; set; } = 0;
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
