using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class Customer
    {
        public string billingAdd { get; set; } = string.Empty;
        public string cardName { get; set; } = string.Empty;
        public string cardNo { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;
        public string customerDob { get; set; } = string.Empty;
        public string customerEmail { get; set; } = string.Empty;
        public string customerName { get; set; } = string.Empty;
        public string customerPhone { get; set; } = string.Empty;
        public string cvv { get; set; } = string.Empty;
        public string expiryDate { get; set; } = string.Empty;
        public string guId { get; set; } = string.Empty;
        public int id { get; set; } = 0;
        public string shippingAdd { get; set; } = string.Empty;
        public string state { get; set; } = string.Empty;
        public string swipeUrl { get; set; } = string.Empty;
        public string zip { get; set; } = string.Empty;
    }

    public class ItemsList
    {
        public string ChefName { get; set; } = string.Empty;
        public string batchCode { get; set; } = string.Empty;
        public string catId { get; set; } = string.Empty;
        public string chefId { get; set; } = string.Empty;
        public List<ComboProductDetailItem> comboProductDetailItems { get; set; }
        public int complimentary { get; set; } = 0;
        public string complimentaryReason { get; set; } = string.Empty;
        public string course { get; set; } = string.Empty;
        public string discountId { get; set; } = string.Empty;
        public string discountName { get; set; } = string.Empty;
        public double discountValue { get; set; } = 0;
        public string employeeId { get; set; } = string.Empty;
        public int holdRelease { get; set; } = 0;
        public List<Ingredients> ingredientses { get; set; }
        public int isCombo { get; set; } = 0;
        public int isDrink { get; set; } 
        public bool isFire { get; set; }
        public int isSendKDS { get; set; } = 0;
        public string itemAId { get; set; } = string.Empty;
        public string itemId { get; set; } = string.Empty;
        public string itemName { get; set; } = string.Empty;
        public int kotCount { get; set; } = 0;
        public string negativeIng { get; set; } = string.Empty;
        public string positiveIng { get; set; } = string.Empty;
        public double price { get; set; } = 0;
        public int qty { get; set; } = 0;
        public string seatNo { get; set; } = string.Empty;
        public string serverId { get; set; } = string.Empty;
        public string serverName { get; set; } = string.Empty;
        public string specialRequest { get; set; } = string.Empty;
        public int spoil { get; set; } = 0;
        public string spoilReason { get; set; } = string.Empty;
        public int status { get; set; } = 0;
        public string taxPer { get; set; } = string.Empty;
        public string time { get; set; } = string.Empty;
        public int upsStatus { get; set; } = 0;
    }

    public class ComboProductDetailItem
    {
        public string comboSetId { get; set; } = string.Empty;
        public string ProductID { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string aID { get; set; } = string.Empty;
        public string cProductId { get; set; } = string.Empty;
        public string categoryId { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
    }

    public class Ingredients
    {
        public string aId { get; set; } = string.Empty;
        public string ingredientId { get; set; } = string.Empty;
        public string logic { get; set; } = string.Empty;
        public string modId { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string transactionId { get; set; } = string.Empty;
        public double price { get; set; } = 0;
        public int qty { get; set; } = 0;
    }

    public class Object
    {
        public string address { get; set; } = string.Empty;
        public int addressType { get; set; } = 0;
        public string batchCode { get; set; } = string.Empty;
        public double billTotal { get; set; } = 0;
        public double change { get; set; } = 0;
        public Customer customer { get; set; }
        public string customerId { get; set; } = string.Empty;
        public string customerMobile { get; set; } = string.Empty;
        public string customerName { get; set; } = string.Empty;
        public string date_long { get; set; } = string.Empty;
        public double discount { get; set; } = 0;
        public int discountType { get; set; } = 0;
        public int duplicatePrint { get; set; } = 0;
        public string employeeId { get; set; } = string.Empty;
        public string employeePasscode { get; set; } = string.Empty;
        public string endTime { get; set; } = string.Empty;
        public double extraCharge { get; set; } = 0;
        public string extraChargeReason { get; set; } = string.Empty;
        public string invoiceNo { get; set; } = string.Empty;
        public List<ItemsList> itemsList { get; set; }
        public string orderId { get; set; } = string.Empty;
        public int orderSize { get; set; } = 0;
        public int paymentType { get; set; } = 0;
        public string queueAheadTime { get; set; } = string.Empty;
        public double queueBalance { get; set; } = 0;
        public string queueCardNo { get; set; } = string.Empty;
        public int queueType { get; set; } = 0;
        public int runningOrder { get; set; } = 0;
        public string spRequest { get; set; } = string.Empty;
        public int status { get; set; } = 0;
        public double subTotal { get; set; } = 0;
        public string tableId { get; set; } = string.Empty;
        public string tableNo { get; set; } = string.Empty;
        public double tendered { get; set; } = 0;
        public string time { get; set; } = string.Empty;
        public double tip { get; set; } = 0;
        public int tokenCount { get; set; } = 0;
        public string tokenNo { get; set; } = string.Empty;
        public int type { get; set; } = 0;
        public int typeCount { get; set; } = 0;
        public int ups_status { get; set; } = 0;
    }

    public class SyncMaster
    {
        public int SyncCode { get; set; } = 0;
        public string batchCode { get; set; } = string.Empty;
        public string date { get; set; } = string.Empty;
        public string id { get; set; } = string.Empty;
    }

    public class NEW_ORDER_101
    {
        public string ackGuid { get; set; } = string.Empty;
        public string ipAddress { get; set; } = string.Empty;
        public List<Object> Object { get; set; }
        public int syncCode { get; set; } = 0;
        public SyncMaster syncMaster { get; set; }
    }
}
