using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class OrderMaster
    {
        public Guid OrderID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid CustomerID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string OrderDate { get; set; } = "";
        public Guid EmployeeID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public int OrderStatus { get; set; } = 0;
        public string OrderStatusReason { get; set; } = "";
        public Guid OrderStatusBy { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid TableID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string InvoiceNo { get; set; } = "0";
        public Guid DiscountID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string DiscountAmount { get; set; } = "0";
        public string DiscountReason { get; set; } = "";
        public Guid DiscountReasonBy { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string SpecialRequest { get; set; } = "";
        public string TipAmount { get; set; } = "0";
        public string TaxOnDelivery { get; set; } = "0";
        public string DeliveryCharge { get; set; } = "0";
        public int SplitBillType { get; set; } = 0;
        public int PayMode { get; set; } = 0;
        public string SubTotal { get; set; } = "0";
        public string FinalTotal { get; set; } = "0";
        public Guid RUserID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public int RUserType { get; set; } = 0;
        public string Tendered { get; set; } = "0";
        public string Change { get; set; } = "0";
        public int OrderTypeID { get; set; } = 0;
        public int IsSplit { get; set; } = 0;
        public double ExtraCharge { get; set; } = 0;
        public string ExtraChargeReason { get; set; } = "";
        public int QueueType { get; set; } = 0;
        public double QueueBalance { get; set; } = 0;
        public string QueueCardNo { get; set; } = "";
        public string QueueAheadTime { get; set; } = "";
        public string CustomerMobileNo { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public string NoOfPax { get; set; } = "0";
        public string StartTime { get; set; } = "";
        public string EndTime { get; set; } = "";
        public Guid ModuleID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string TaxLable1 { get; set; } = "";
        public string TaxPercentage1 { get; set; } = "";
        public string TaxAmount1 { get; set; } = "";
        public string TaxLable2 { get; set; } = "";
        public string TaxPercentage2 { get; set; } = "0";
        public string TaxAmount2 { get; set; } = "0";
        public string OrderTaxTotal { get; set; } = "0";
        public string Mode { get; set; } = "";
        public int IsUPStream { get; set; } = 0;
    }

    public class OrderMasterTax
    {
        public Guid OrderTaxID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string TaxPercenatge { get; set; } = "0";
        public Guid OrderID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid TaxFormulaID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string TaxAmount { get; set; } = "0";
        public Guid RUserID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public int RUserType { get; set; } = 0;
        public string TaxName { get; set; } = "";
        public string Mode { get; set; } = "";
    }

    public class OrderTransaction
    {
        public Guid OrderID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid OrderTransactionID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid CategoryID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid ProductID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string Quantity { get; set; } = "0";
        public string ItemPrice { get; set; } = "0";
        public string SpecialRemarks { get; set; } = "";
        public int IsIngredientUsed { get; set; } = 0;
        public Guid CouponID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public int SeatNo { get; set; } = 0;
        public int ItemStatus { get; set; } = 0;
        public string ItemStatusReason { get; set; } = "";
        public Guid ItemStatusBy { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public bool IsCombo { get; set; } = false;
        public Guid ItemTaxID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string ItemTaxAmount { get; set; } = "0";
        public string StartDate { get; set; } = "";
        public string EndDate { get; set; } = "";
        public Guid EmployeeID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid ChefID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid RUserID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public int RUserType { get; set; } = 0;
        public string InvoiceNo { get; set; } = "0";
        public string ProductName { get; set; } = "";
        public int Spoil { get; set; } = 0;
        public int Complimentary { get; set; } = 0;
        public string ComplimentaryReason { get; set; } = "";
        public string SpoilReason { get; set; } = "";
        public Guid Discountid { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string Discountvalue { get; set; } = "0";
        public string DiscountReason { get; set; } = "";
        public Guid DiscountReasonBy { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string DateTime { get; set; } = "";
        public string Course { get; set; } = "0";
        public string TaxPer { get; set; } = "0";
        public string Mode { get; set; } = "";
    }

    public class OrderIngredient
    {
        public Guid OrderIngredientsID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid TransactionID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid IngredientsID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string Quantity { get; set; } = "0";
        public string Rate { get; set; } = "0";
        public string Amount { get; set; } = "0";
        public Guid RUserID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public int RUserType { get; set; } = 0;
        public string LogicText { get; set; } = "";
        public string IngredientName { get; set; } = "";
        public Guid ModifierID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string Mode { get; set; } = "";
    }

    public class OrderCombo
    {
        public Guid OrderComboID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid OrderID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid ProductID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid CProductID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid RUserID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public int RUserType { get; set; } = 0;
        public string ProductName { get; set; } = "";
        public Guid OrderTransactionID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid ComboSetID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid CategoryID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string Mode { get; set; } = "";
    }

    public class OrderPayment
    {
        public Guid OrderPaymentID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid OrderID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid PaymentTransID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public Guid PaymentID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string PaymentToken { get; set; } = "";
        public bool IsSwipe { get; set; } = false;
        public string PaymentCardTypeID { get; set; } = "";
        public string CardNo { get; set; } = "";
        public string Amount { get; set; } = "";
        public string ExpireDate { get; set; } = "";
        public string CVV { get; set; } = "";
        public string SwipeStr { get; set; } = "";
        public string CardHolderName { get; set; } = "";
        public string TransactionResult { get; set; } = "";
        public string CustomerTransactionId { get; set; } = "";
        public string TransactionAction { get; set; } = "";
        public string TranstationMessage { get; set; } = "";
        public string IsPartialAuthorized { get; set; } = "";
        public string RequestedAmount { get; set; } = "";
        public Guid BranchID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string Mode { get; set; } = "";
    }

    public class ManageOrder
    {
        public List<OrderMaster> OrderMaster { get; set; }
        public List<OrderMasterTax> OrderMasterTax { get; set; }
        public List<OrderTransaction> OrderTransaction { get; set; }
        public List<OrderIngredient> OrderIngredients { get; set; }
        public List<OrderCombo> OrderCombo { get; set; }
        public List<OrderPayment> OrderPayment { get; set; }
    }
}
