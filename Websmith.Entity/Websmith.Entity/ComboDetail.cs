using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ComboDetail
    {
        #region Private Fields

        private Guid _ComboSetID;
        private string _ComboSetName;
        private Guid _CategoryID;
        private Guid _CProductID;
        private int _ComboDetail_Id;
        private int _CategoryWiseProduct_Id;
        private string _Mode;
        public int IsUPStream { get; set; } = 0;
        #endregion

        #region Public Properties

        public Guid ComboSetID
        {
            get { return _ComboSetID; }
            set { _ComboSetID = value; }
        }
        public string ComboSetName
        {
            get { return _ComboSetName; }
            set { _ComboSetName = value; }
        }
        public Guid CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        public Guid CProductID
        {
            get { return _CProductID; }
            set { _CProductID = value; }
        }
        public int ComboDetail_Id
        {
            get { return _ComboDetail_Id; }
            set { _ComboDetail_Id = value; }
        }
        public int CategoryWiseProduct_Id
        {
            get { return _CategoryWiseProduct_Id; }
            set { _CategoryWiseProduct_Id = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
