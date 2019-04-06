using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class OutwardDetail
    {
        #region Private Fields
        private Guid _OutwardDetailID;
        private Guid _OutwardIDF;
        private Guid _ProductID;
        private Guid _UnitTypeID;
        private decimal _Qty;
        private decimal _Rate;
        private decimal _TotalAmount;
        private int _Sort;
        private int _IsUpStream;

        public string Mode { get; set; }
        public string ProductName { get; set; }
        public string UnitType { get; set; }
        #endregion

        #region Public Properties
        public Guid OutwardDetailID
        {
            get { return _OutwardDetailID; }
            set { _OutwardDetailID = value; }
        }
        public Guid OutwardIDF
        {
            get { return _OutwardIDF; }
            set { _OutwardIDF = value; }
        }
        public Guid ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        public Guid UnitTypeID
        {
            get { return _UnitTypeID; }
            set { _UnitTypeID = value; }
        }
        public decimal Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }
        public decimal Rate
        {
            get { return _Rate; }
            set { _Rate = value; }
        }
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        public int Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }
        public int IsUpStream
        {
            get { return _IsUpStream; }
            set { _IsUpStream = value; }
        }
        #endregion
    }
}
