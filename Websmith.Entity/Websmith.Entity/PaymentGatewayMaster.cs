using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class PaymentGatewayMaster
    {
        #region Private Fields

        private Guid _PaymentGatewayID;
        private string _PaymentGatewayName;
        private string _MerchantID;
        private string _TokenKey;
        private string _UserName;
        private string _Password;
        private string _ResponseUrl;
        private string _ATOMTransactionType;
        private string _Productid;
        private string _Version;
        private string _ServiceID;
        private string _ApplicationProfileId;
        private string _MerchantProfileId;
        private string _MerchantProfileName;
        private string _Mode;
        public int IsUPStream { get; set; } = 0;
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
        public string MerchantID
        {
            get { return _MerchantID; }
            set { _MerchantID = value; }
        }
        public string TokenKey
        {
            get { return _TokenKey; }
            set { _TokenKey = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public string ResponseUrl
        {
            get { return _ResponseUrl; }
            set { _ResponseUrl = value; }
        }
        public string ATOMTransactionType
        {
            get { return _ATOMTransactionType; }
            set { _ATOMTransactionType = value; }
        }
        public string Productid
        {
            get { return _Productid; }
            set { _Productid = value; }
        }
        public string Version
        {
            get { return _Version; }
            set { _Version = value; }
        }
        public string ServiceID
        {
            get { return _ServiceID; }
            set { _ServiceID = value; }
        }
        public string ApplicationProfileId
        {
            get { return _ApplicationProfileId; }
            set { _ApplicationProfileId = value; }
        }
        public string MerchantProfileId
        {
            get { return _MerchantProfileId; }
            set { _MerchantProfileId = value; }
        }
        public string MerchantProfileName
        {
            get { return _MerchantProfileName; }
            set { _MerchantProfileName = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
