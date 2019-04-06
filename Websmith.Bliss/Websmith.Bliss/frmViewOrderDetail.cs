using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.Bliss
{
    public partial class frmViewOrderDetail : Form
    {
        string orderID = "";

        public frmViewOrderDetail()
        {
            InitializeComponent();
        }

        public frmViewOrderDetail(string OrderID)
        {
            InitializeComponent();
            orderID = OrderID;
        }

        private void CalcTotal()
        {
            try
            {
                decimal discTotal = 0;

                int discType = txtDiscountType.Text == "" ? 0 : Convert.ToInt32(txtDiscountType.Text);
                decimal discPer = txtDiscountPer.Text == "" ? 0 : Convert.ToDecimal(txtDiscountPer.Text);
                decimal discountTotal = txtDiscount.Text == "" ? 0 : Convert.ToDecimal(txtDiscount.Text);
                decimal charge = txtExtraCharge.Text == "" ? 0 : Convert.ToDecimal(txtExtraCharge.Text);
                decimal tip = txtTip.Text == "" ? 0 : Convert.ToDecimal(txtTip.Text);
                decimal deliveryCharge = txtDeliveryCharge.Text == "" ? 0 : Convert.ToDecimal(txtDeliveryCharge.Text);

                decimal subTotal = Convert.ToDecimal(txtPCPayableAmount.Text) + charge;
                txtSubTotal.Text = subTotal.ToString();
                if (discType == 2)
                    discTotal = Math.Round((subTotal * discPer) / 100, 2, MidpointRounding.AwayFromZero);
                else if (discType == 1)
                    discTotal = discountTotal;
                else
                    discTotal = 0;

                decimal SubTotalDiscount = subTotal - discTotal;
                txtTotalAmount.Text = SubTotalDiscount.ToString();

                txtDiscount.Text = discTotal.ToString();
                txtExtraCharge.Text = charge.ToString();
                txtTip.Text = tip.ToString();
                txtDeliveryCharge.Text = deliveryCharge.ToString();

                txtCGST.Text = Math.Round((Convert.ToDecimal(SubTotalDiscount) * Convert.ToDecimal(txtCGSTPer.Text)) / 100, 2, MidpointRounding.AwayFromZero).ToString();
                txtSGST.Text = Math.Round((Convert.ToDecimal(SubTotalDiscount) * Convert.ToDecimal(txtSGSTPer.Text)) / 100, 2, MidpointRounding.AwayFromZero).ToString();
                txtTotalTax.Text = Math.Round(Convert.ToDecimal(txtCGST.Text) + Convert.ToDecimal(txtSGST.Text), 2, MidpointRounding.AwayFromZero).ToString();
                txtPayAmt.Text = Convert.ToString(SubTotalDiscount + Convert.ToDecimal(txtTotalTax.Text) + tip + deliveryCharge);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetTaxFromGeneralSetting()
        {
            try
            {
                DAL.GeneralSetting objDAL = new DAL.GeneralSetting();
                ENT.GeneralSetting objENT = new ENT.GeneralSetting();
                List<ENT.GeneralSetting> lstENT = new List<ENT.GeneralSetting>();
                objENT.Mode = "GetAll";
                lstENT = objDAL.GetGeneralSetting(objENT);
                if (lstENT.Count > 0)
                {
                    lblTax1.Text = lstENT[0].TaxLabel1 + ":";
                    txtSGSTPer.Text = Convert.ToString(lstENT[0].TaxPercentage1);
                    lblTax2.Text = lstENT[0].TaxLabel2 + ":"; ;
                    txtCGSTPer.Text = Convert.ToString(lstENT[0].TaxPercentage2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetOrderTransactionData()
        {
            btnEdit.Visible = false;
            DAL.OrderBook objDALOrder = new DAL.OrderBook();
            ENT.OrderBook objENTOrder = new ENT.OrderBook();
            List<ENT.OrderBook> lstENTOrder = new List<ENT.OrderBook>();

            objENTOrder.Mode = "GetRecordByOrderID";
            objENTOrder.OrderID = new Guid(orderID);
            lstENTOrder = objDALOrder.getOrder(objENTOrder);
            if (lstENTOrder.Count > 0)
            {
                txtOrderId.Text = Convert.ToString(lstENTOrder[0].OrderID);
                txtInvoiceNo.Text = Convert.ToString(lstENTOrder[0].OrderNo);
                txtDateTime.Text = lstENTOrder[0].OrderDate != null ? Convert.ToDateTime(lstENTOrder[0].OrderDate).ToString("dd/MM/yyyy hh:mm tt") : DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                txtCustId.Text = Convert.ToString(lstENTOrder[0].CustomerID);
                txtCustName.Text = Convert.ToString(lstENTOrder[0].Name);
                txtTableId.Text = Convert.ToString(lstENTOrder[0].TableID);

                GlobalVariable.objCheckInfo.DelieveryType = lstENTOrder[0].DeliveryType;
                GlobalVariable.objCheckInfo.DelieveryTypeName = lstENTOrder[0].DeliveryTypeName;
                GlobalVariable.objCheckInfo.TableID = lstENTOrder[0].TableID;

                if (lstENTOrder[0].DeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.DineIn))
                {
                    txtTableNo.Text = "Type : " + lstENTOrder[0].DeliveryTypeName;
                    ENT.MergeTable objENTMT = new ENT.MergeTable();
                    List<ENT.MergeTable> lstENTMT = new List<ENT.MergeTable>();
                    DAL.MergeTable objDALMT = new DAL.MergeTable();
                    objENTMT.OrderID = new Guid(txtOrderId.Text.Trim());
                    objENTMT.Mode = "GetTableByOrderID";
                    lstENTMT = objDALMT.getMergeTable(objENTMT);
                    for (int i = 0; i < lstENTMT.Count; i++)
                    {
                        txtTableNo.Text = txtTableNo.Text + " / " + lstENTMT[i].TableName;
                    }

                }
                else
                    txtTableNo.Text = "Type : " + lstENTOrder[0].DeliveryTypeName;

                txtPCPayableAmount.Text = Convert.ToString(lstENTOrder[0].SubTotal);
                txtExtraCharge.Text = Convert.ToString(lstENTOrder[0].ExtraCharge);
                txtDiscountType.Text = Convert.ToString(lstENTOrder[0].DiscountType);
                txtDiscountPer.Text = Convert.ToString(lstENTOrder[0].DiscountPer);
                txtDiscount.Text = Convert.ToString(lstENTOrder[0].Discount);
                lblTax1.Text = Convert.ToString(lstENTOrder[0].TaxLabel1);
                txtSGSTPer.Text = Convert.ToString(lstENTOrder[0].TaxPercent1);
                txtSGST.Text = Convert.ToString(lstENTOrder[0].SGSTAmount);
                lblTax2.Text = Convert.ToString(lstENTOrder[0].TaxLabel2);
                txtCGSTPer.Text = Convert.ToString(lstENTOrder[0].TaxPercent2);
                txtCGST.Text = Convert.ToString(lstENTOrder[0].CGSTAmount);
                txtTotalTax.Text = Convert.ToString(lstENTOrder[0].TotalTax);
                txtTip.Text = Convert.ToString(lstENTOrder[0].TipGratuity);
                txtDeliveryCharge.Text = Convert.ToString(lstENTOrder[0].DeliveryCharge);
                txtPayAmt.Text = Convert.ToString(lstENTOrder[0].PayableAmount);
                
                if (lstENTOrder[0].OrderActions == Convert.ToInt32(GlobalVariable.OrderActions.Paid))
                {
                    btnEdit.Visible = false;
                }

                DAL.Transaction objDALTrans = new DAL.Transaction();
                ENT.Transaction objENTTrans = new ENT.Transaction();
                List<ENT.Transaction> lstENTTrans = new List<ENT.Transaction>();

                objENTTrans.Mode = "GetRecordByOrderID";
                objENTTrans.OrderID = new Guid(orderID);
                lstENTTrans = objDALTrans.getOrderTransaction(objENTTrans);

                dgvItem.Rows.Clear();
                for (int i = 0; i < lstENTTrans.Count; i++)
                {
                    dgvItem.Rows.Add();
                    dgvItem.Rows[i].Cells["ordProdID"].Value = lstENTTrans[i].ProductID;
                    dgvItem.Rows[i].Cells["ordItemName"].Value = lstENTTrans[i].ProductName;
                    dgvItem.Rows[i].Cells["ordQty"].Value = lstENTTrans[i].Quantity;
                    dgvItem.Rows[i].Cells["ordRate"].Value = lstENTTrans[i].Rate;
                    dgvItem.Rows[i].Cells["ordTotal"].Value = lstENTTrans[i].TotalAmount;
                    dgvItem.Rows[i].Cells["ordTransID"].Value = lstENTTrans[i].TransactionID;
                }
                this.CalcTotal();
            }
        }

        private void frmViewOrderDetail_Load(object sender, EventArgs e)
        {
            this.GetTaxFromGeneralSetting();
            this.GetOrderTransactionData();
        }
        
        private void btnWalkIn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmViewOrderDetail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
