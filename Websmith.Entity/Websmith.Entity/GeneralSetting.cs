using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class GeneralSetting
    {
        #region Private Fields
        private Guid _PaymentGatewayID;
        private string _PaymentGatewayName;
        private string _PrintHeader;
        private string _PrintFooter;
        private bool _DuplicatePrint;
        private int _KOTCount;
        private string _OrderPrefix;
        private int _KOTFontSize;
        private bool _KOTServerName;
        private bool _KOTDateTime;
        private bool _KOTOrderType;
        private bool _KDSWithoutDisplay;
        private bool _RoundingTotal;
        private bool _DisplayCardNo;
        private bool _PrintOnPaymentDone;
        private bool _RunningOrderDisplayOnKOT;
        private bool _KDSWithoutPrinter;
        private bool _CustomerNameOnKOT;
        private string _DateTimeFormat;
        private string _Language;
        private int _TillCur1;
        private int _TillCur2;
        private int _TillCur3;
        private int _TillCur4;
        private int _TillCur5;
        private int _TillCur6;
        private int _TillCur7;
        private int _TillCur8;
        private int _TillCur9;
        private bool _DineIn;
        private bool _TakeOut;
        private bool _Delivery;
        private bool _OrderAhead;
        private bool _Queue;
        private bool _PartyEvent;
        private string _TaxLabel1;
        private decimal _TaxPercentage1;
        private string _TaxLabel2;
        private decimal _TaxPercentage2;
        private string _Mode;
        #endregion

        #region Public Properties

        public Guid PaymentGatewayID
        {
            get { return _PaymentGatewayID; }
            set { _PaymentGatewayID = value; }
        }
        public string PaymentGatewayName
        {
            get { return _PaymentGatewayName; }
            set { _PaymentGatewayName = value; }
        }
        public string PrintHeader
        {
            get { return _PrintHeader; }
            set { _PrintHeader = value; }
        }
        public string PrintFooter
        {
            get { return _PrintFooter; }
            set { _PrintFooter = value; }
        }
        public bool DuplicatePrint
        {
            get { return _DuplicatePrint; }
            set { _DuplicatePrint = value; }
        }
        public int KOTCount
        {
            get { return _KOTCount; }
            set { _KOTCount = value; }
        }
        public string OrderPrefix
        {
            get { return _OrderPrefix; }
            set { _OrderPrefix = value; }
        }
        public int KOTFontSize
        {
            get { return _KOTFontSize; }
            set { _KOTFontSize = value; }
        }
        public bool KOTServerName
        {
            get { return _KOTServerName; }
            set { _KOTServerName = value; }
        }
        public bool KOTDateTime
        {
            get { return _KOTDateTime; }
            set { _KOTDateTime = value; }
        }
        public bool KOTOrderType
        {
            get { return _KOTOrderType; }
            set { _KOTOrderType = value; }
        }
        public bool KDSWithoutDisplay
        {
            get { return _KDSWithoutDisplay; }
            set { _KDSWithoutDisplay = value; }
        }
        public bool RoundingTotal
        {
            get { return _RoundingTotal; }
            set { _RoundingTotal = value; }
        }
        public bool DisplayCardNo
        {
            get { return _DisplayCardNo; }
            set { _DisplayCardNo = value; }
        }
        public bool PrintOnPaymentDone
        {
            get { return _PrintOnPaymentDone; }
            set { _PrintOnPaymentDone = value; }
        }
        public bool RunningOrderDisplayOnKOT
        {
            get { return _RunningOrderDisplayOnKOT; }
            set { _RunningOrderDisplayOnKOT = value; }
        }
        public bool KDSWithoutPrinter
        {
            get { return _KDSWithoutPrinter; }
            set { _KDSWithoutPrinter = value; }
        }
        public bool CustomerNameOnKOT
        {
            get { return _CustomerNameOnKOT; }
            set { _CustomerNameOnKOT = value; }
        }
        public string DateTimeFormat
        {
            get { return _DateTimeFormat; }
            set { _DateTimeFormat = value; }
        }
        public string Language
        {
            get { return _Language; }
            set { _Language = value; }
        }
        public int TillCur1
        {
            get { return _TillCur1; }
            set { _TillCur1 = value; }
        }
        public int TillCur2
        {
            get { return _TillCur2; }
            set { _TillCur2 = value; }
        }
        public int TillCur3
        {
            get { return _TillCur3; }
            set { _TillCur3 = value; }
        }
        public int TillCur4
        {
            get { return _TillCur4; }
            set { _TillCur4 = value; }
        }
        public int TillCur5
        {
            get { return _TillCur5; }
            set { _TillCur5 = value; }
        }
        public int TillCur6
        {
            get { return _TillCur6; }
            set { _TillCur6 = value; }
        }
        public int TillCur7
        {
            get { return _TillCur7; }
            set { _TillCur7 = value; }
        }
        public int TillCur8
        {
            get { return _TillCur8; }
            set { _TillCur8 = value; }
        }
        public int TillCur9
        {
            get { return _TillCur9; }
            set { _TillCur9 = value; }
        }
        public bool DineIn
        {
            get { return _DineIn; }
            set { _DineIn = value; }
        }
        public bool TakeOut
        {
            get { return _TakeOut; }
            set { _TakeOut = value; }
        }
        public bool Delivery
        {
            get { return _Delivery; }
            set { _Delivery = value; }
        }
        public bool OrderAhead
        {
            get { return _OrderAhead; }
            set { _OrderAhead = value; }
        }
        public bool Queue
        {
            get { return _Queue; }
            set { _Queue = value; }
        }
        public bool PartyEvent
        {
            get { return _PartyEvent; }
            set { _PartyEvent = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        public Guid RUserID { get; set; }
        public int RUserType { get; set; }

        public string TaxLabel1
        {
            get
            {
                return _TaxLabel1;
            }

            set
            {
                _TaxLabel1 = value;
            }
        }

        public decimal TaxPercentage1
        {
            get
            {
                return _TaxPercentage1;
            }

            set
            {
                _TaxPercentage1 = value;
            }
        }

        public string TaxLabel2
        {
            get
            {
                return _TaxLabel2;
            }

            set
            {
                _TaxLabel2 = value;
            }
        }

        public decimal TaxPercentage2
        {
            get
            {
                return _TaxPercentage2;
            }

            set
            {
                _TaxPercentage2 = value;
            }
        }
        #endregion
    }
}
