using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class TaxGroupDetail
    {
        #region Private Fields

        private Guid _TaxGroupID;
        private string _Name;
        private decimal _Percentage;
        private Guid _ParentID;
        private Guid _PartnerID;
        private string _Action;
        private string _Sign;
        private int _Sort;
        private int _TaxOnProductType;
        private string _Mode;
        public int IsUPStream { get; set; } = 0;
        #endregion

        #region Public Properties

        public Guid TaxGroupID
        {
            get { return _TaxGroupID; }
            set { _TaxGroupID = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public decimal Percentage
        {
            get { return _Percentage; }
            set { _Percentage = value; }
        }
        public Guid ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        public Guid PartnerID
        {
            get { return _PartnerID; }
            set { _PartnerID = value; }
        }
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }
        public string Sign
        {
            get { return _Sign; }
            set { _Sign = value; }
        }
        public int Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }
        public int TaxOnProductType
        {
            get { return _TaxOnProductType; }
            set { _TaxOnProductType = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
