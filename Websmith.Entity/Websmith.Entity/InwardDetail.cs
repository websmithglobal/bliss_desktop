using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class InwardDetail
    {

        #region Constructors
        public InwardDetail()
        {
        }
        #endregion
        #region Private Fields
        private Guid _InwardDetailID;
        private Guid _InwardIDF;
        private Guid _ProductID;
        private Guid _UnitTypeID;
        private decimal _RecQty;
        private decimal _RejQty;
        private decimal _TotQty;
        private decimal _Rate;
        private decimal _SubTotal;
        private decimal _TaxAmount;
        private decimal _TotalAmount;
        private int _Sort;
        private int _IsUpStream;
        private string _mode;

        public string ProductName { get; set; }
        public string UnitType { get; set; }
        #endregion
        #region Public Properties
        public Guid InwardDetailID
        {
            get { return _InwardDetailID; }
            set { _InwardDetailID = value; }
        }
        public Guid InwardIDF
        {
            get { return _InwardIDF; }
            set { _InwardIDF = value; }
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
        public decimal RecQty
        {
            get { return _RecQty; }
            set { _RecQty = value; }
        }
        public decimal RejQty
        {
            get { return _RejQty; }
            set { _RejQty = value; }
        }
        public decimal TotQty
        {
            get { return _TotQty; }
            set { _TotQty = value; }
        }
        public decimal Rate
        {
            get { return _Rate; }
            set { _Rate = value; }
        }
        public decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; }
        }
        public decimal TaxAmount
        {
            get { return _TaxAmount; }
            set { _TaxAmount = value; }
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
        public string Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }
        #endregion
    }
}
