using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class InwardMaster
    {
        #region Private Fields
        private Guid _InwardID;
        private string _InvoiceNo;
        private string _InvoiceDate;
        private Guid _VedorID;
        private string _PONo;
        private string _OtherChargeDetail;
        private decimal _OtherCharge;
        private decimal _DiscountAmount;
        private decimal _TaxAmount;
        private decimal _RoundOffAmount;
        private decimal _FinalTotal;
        private string _Remark;
        private int _IsUpStream;
        private string _VendorName;
        private string _MobileNo;
        private string _mode;

        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        #endregion

        #region Public Properties
        public Guid InwardID
        {
            get { return _InwardID; }
            set { _InwardID = value; }
        }
        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }
        public string InvoiceDate
        {
            get { return _InvoiceDate; }
            set { _InvoiceDate = value; }
        }
        public Guid VedorID
        {
            get { return _VedorID; }
            set { _VedorID = value; }
        }
        public string PONo
        {
            get { return _PONo; }
            set { _PONo = value; }
        }
        public string OtherChargeDetail
        {
            get { return _OtherChargeDetail; }
            set { _OtherChargeDetail = value; }
        }
        public decimal OtherCharge
        {
            get { return _OtherCharge; }
            set { _OtherCharge = value; }
        }
        public decimal DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        public decimal TaxAmount
        {
            get { return _TaxAmount; }
            set { _TaxAmount = value; }
        }
        public decimal RoundOffAmount
        {
            get { return _RoundOffAmount; }
            set { _RoundOffAmount = value; }
        }
        public decimal FinalTotal
        {
            get { return _FinalTotal; }
            set { _FinalTotal = value; }
        }
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        public int IsUpStream
        {
            get { return _IsUpStream; }
            set { _IsUpStream = value; }
        }
        public string VendorName
        {
            get { return _VendorName; }
            set { _VendorName = value; }
        }
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        public string Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }
        #endregion
    }
}
