using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class OutwardMaster
    {
        #region Private Fields
        private Guid _OutwardID;
        private Int64 _InvoiceNo;
        private string _InvoiceDate;
        private Guid _EmpID;
        private decimal _FinalTotal;
        private string _Remark;
        private int _IsUpStream;
        public string Mode { get; set; }
        public string EmpName { get; set; }
        public string Mobile { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        #endregion

        #region Public Properties
        public Guid OutwardID
        {
            get { return _OutwardID; }
            set { _OutwardID = value; }
        }
        public Int64 InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }
        public string InvoiceDate
        {
            get { return _InvoiceDate; }
            set { _InvoiceDate = value; }
        }
        public Guid EmpID
        {
            get { return _EmpID; }
            set { _EmpID = value; }
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
        #endregion
    }
}
