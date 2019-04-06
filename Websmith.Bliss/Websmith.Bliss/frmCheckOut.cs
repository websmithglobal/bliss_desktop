using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;
using System.Drawing.Printing;

namespace Websmith.Bliss
{
    public partial class frmCheckOut : Form
    {
        bool cmdEnable = false;
        List<ENT.BranchMasterSetting> lstENTBranch = new List<ENT.BranchMasterSetting>();
        DAL.CheckOutDetail objDALCOD = new DAL.CheckOutDetail();
        ENT.CheckOutDetail objENTSAVE = new ENT.CheckOutDetail();
        
        public frmCheckOut()
        {
            InitializeComponent();
        }

        public frmCheckOut(ENT.PaymentDetail Entity)
        {
            InitializeComponent();
            txtOrderID.Text = Entity.OrderID;
            //txtCustomerID.Text = Entity.CustomerID;
            //txtTableID.Text = Entity.TableID;

            //txtPCPayableAmount.Text = Entity.SumSubTotal.ToString();
            //txtCashOrderAmt.Text = Entity.PayAmount.ToString();
            //txtCCOrderAmt.Text = Entity.PayAmount.ToString();
            //txtCQOrderAmt.Text = Entity.PayAmount.ToString();            
        }

        private void PrintReceipt()
        {
            try
            {
                ENT.OrderBook objENT = new ENT.OrderBook();
                DAL.OrderBook objDAL = new DAL.OrderBook();
                List<ENT.OrderBook> lstOrder = new List<ENT.OrderBook>();
                objENT.OrderID = new Guid(txtOrderID.Text.Trim());
                objENT.Mode = "GetRecordByOrderID";
                lstOrder = objDAL.getOrder(objENT);
                if (lstOrder.Count > 0)
                {
                    PrintReceipt pr = new PrintReceipt(txtOrderID.Text.Trim(), lstOrder[0].IsPrint);
                    pr.PrintOrderReceipt();
                }
                else
                {
                    PrintReceipt pr = new PrintReceipt(txtOrderID.Text.Trim());
                    pr.PrintOrderReceipt();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateReport()
        {
            try
            {
                lblOrderSubTotal.Text = txtPCPayableAmount.Text;
                lblExtraChareg.Text = txtExtraCharge.Text;
                lblDTManual.Text = txtDiscount.Text;
                lblDTTotal.Text = txtDiscount.Text;
                if (rdoCash.Checked == true)
                {
                    #region Cash
                    lblPMTTotal.Text = Convert.ToString(txtCashPaidAmt.Text);
                    lblPDCash.Text = Convert.ToString(txtCashPaidAmt.Text);
                    lblPDCreditCard.Text = "";
                    lblPDCheque.Text = "";
                    lblReceived.Text = Convert.ToString(txtCashPaidAmt.Text);
                    lblChange.Text = Convert.ToString(txtCashChangeAmt.Text);
                    #endregion
                }
                else if (rdoCreditCard.Checked == true)
                {
                    #region Credit Card
                    lblPMTTotal.Text = Convert.ToString(txtCCPaidAmt.Text);
                    lblPDCreditCard.Text = Convert.ToString(txtCCPaidAmt.Text);
                    lblPDCash.Text = "";
                    lblPDCheque.Text = "";
                    lblReceived.Text = Convert.ToString(txtCCPaidAmt.Text);
                    lblChange.Text = Convert.ToString(txtCCChangeAmt.Text);
                    #endregion
                }
                else if (rdoCheque.Checked == true)
                {
                    #region Cheque
                    lblPMTTotal.Text = Convert.ToString(txtCQPaidAmt.Text);
                    lblPDCheque.Text = Convert.ToString(txtCQPaidAmt.Text);
                    lblPDCreditCard.Text = "";
                    lblPDCash.Text = "";
                    lblReceived.Text = Convert.ToString(txtCQPaidAmt.Text);
                    lblChange.Text = Convert.ToString(txtCQChangeAmt.Text);
                    #endregion
                }
                lblSumSubTot.Text = txtTotalAmount.Text == "" ? "0" : txtTotalAmount.Text;
                lblSumTax.Text = txtTotalTax.Text == "" ? "0" : txtTotalTax.Text;
                lblTip.Text = txtTip.Text == "" ? "0" : txtTip.Text;
                lblDeliCharge.Text = txtDeliveryCharge.Text;
                lblSumTotal.Text = Convert.ToString(Convert.ToDecimal(lblSumSubTot.Text) + Convert.ToDecimal(lblSumTax.Text));
                lblPDPaidAmt.Text = txtPayAmt.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void getBranchMasterSetting()
        {
            try
            {
                ENT.BranchMasterSetting objENTBranch = new ENT.BranchMasterSetting();
                DAL.BranchMasterSetting objDALBranch = new DAL.BranchMasterSetting();

                objENTBranch.Mode = "GetBranchMasterAndSettingByID";
                objENTBranch.BranchID = new Guid(GlobalVariable.BranchID);
                lstENTBranch = objDALBranch.getBranchMasterSetting(objENTBranch);

                if (lstENTBranch.Count > 0)
                {
                    lblCurencySymbol.Text = lstENTBranch[0].CurrencySymbol;
                    if (GlobalVariable.objCheckInfo.DelieveryType == Convert.ToInt32(GlobalVariable.DeliveryType.Delivery))
                    { txtDeliveryCharge.Text = Convert.ToString(lstENTBranch[0].DeliveryCharges); }
                    else
                    { txtDeliveryCharge.Text = "0"; }
                    
                    //lblChangeCurSymbol.Text = lstENTBranch[0].CurrencySymbol;
                }
                else
                {
                    lblCurencySymbol.Text = "";
                    //lblChangeCurSymbol.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetOrderTaxDiscount(string orderID)
        {
            try
            {
                DAL.OrderBook objDALOrder = new DAL.OrderBook();
                ENT.OrderBook objENTOrder = new ENT.OrderBook();
                List<ENT.OrderBook> lstENTOrder = new List<ENT.OrderBook>();

                objENTOrder.Mode = "GetRecordByOrderID";
                objENTOrder.OrderID = new Guid(orderID);
                lstENTOrder = objDALOrder.getOrder(objENTOrder);
                if (lstENTOrder.Count > 0)
                {
                    txtCustomerID.Text =Convert.ToString(lstENTOrder[0].CustomerID);
                    txtTableID.Text = Convert.ToString(lstENTOrder[0].TableID);
                    txtPCPayableAmount.Text = Convert.ToString(lstENTOrder[0].SubTotal);
                    txtExtraCharge.Text = Convert.ToString(lstENTOrder[0].ExtraCharge);
                    lblTax1.Text = lstENTOrder[0].TaxLabel1;
                    txtSGSTPer.Text = Convert.ToString(lstENTOrder[0].TaxPercent1);
                    txtSGST.Text = Convert.ToString(lstENTOrder[0].SGSTAmount);
                    lblTax2.Text = lstENTOrder[0].TaxLabel2;
                    txtCGSTPer.Text = Convert.ToString(lstENTOrder[0].TaxPercent2);
                    txtCGST.Text = Convert.ToString(lstENTOrder[0].CGSTAmount);
                    txtTotalTax.Text = Convert.ToString(lstENTOrder[0].TotalTax);

                    if (lstENTOrder[0].DiscountType == 1)
                        rdoDiscAmt.Checked = true;
                    else if (lstENTOrder[0].DiscountType == 2)
                        rdoDiscPer.Checked = true;

                    txtDiscountType.Text = Convert.ToString(lstENTOrder[0].DiscountType);
                    txtDiscountPer.Text = Convert.ToString(lstENTOrder[0].DiscountPer);
                    txtDiscount.Text = Convert.ToString(lstENTOrder[0].Discount);
                    txtTip.Text = Convert.ToString(lstENTOrder[0].TipGratuity);
                    txtDeliveryCharge.Text = Convert.ToString(lstENTOrder[0].DeliveryCharge);
                    txtPayAmt.Text = Convert.ToString(lstENTOrder[0].PayableAmount);
                    this.CalcTotal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    discTotal = Math.Round((subTotal * discPer) / 100,2,MidpointRounding.AwayFromZero);
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
                txtPayAmt.Text = Convert.ToString(SubTotalDiscount + Convert.ToDecimal(txtTotalTax.Text) + tip+ deliveryCharge);

                txtCashOrderAmt.Text = txtPayAmt.Text;
                txtCashPaidAmt.Text = txtPayAmt.Text;
                txtCCOrderAmt.Text = txtPayAmt.Text;
                txtCCPaidAmt.Text = txtPayAmt.Text;
                txtCQOrderAmt.Text = txtPayAmt.Text;
                txtCQPaidAmt.Text = txtPayAmt.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateOrderAmount()
        {
            try
            {
                #region Save Discount
                ENT.OrderBook objENTOB = new ENT.OrderBook();
                DAL.OrderBook objDALOB = new DAL.OrderBook();
                objENTOB.OrderID = new Guid(txtOrderID.Text);
                objENTOB.SubTotal = Convert.ToDecimal(txtPCPayableAmount.Text);
                objENTOB.ExtraCharge = txtExtraCharge.Text.Trim().Length > 0 ? Convert.ToDecimal(txtExtraCharge.Text) : 0;
                objENTOB.DiscountType = rdoDiscAmt.Checked == true ? 1 : rdoDiscPer.Checked == true ? 2 : 0;
                objENTOB.DiscountPer = txtDiscountPer.Text.Trim().Length > 0 ? Convert.ToDecimal(txtDiscountPer.Text) : 0;
                objENTOB.Discount = txtDiscount.Text.Trim().Length > 0 ? Convert.ToDecimal(txtDiscount.Text) : 0;
                objENTOB.DiscountID = txtDiscountID.Text.Trim() == string.Empty ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(txtDiscountID.Text);
                objENTOB.TaxLabel1 = lblTax1.Text.Trim();
                objENTOB.TaxPercent1 = Convert.ToDecimal(txtSGSTPer.Text);
                objENTOB.SGSTAmount = Convert.ToDecimal(txtSGST.Text);
                objENTOB.TaxLabel2 = lblTax2.Text.Trim();
                objENTOB.TaxPercent2 = Convert.ToDecimal(txtCGSTPer.Text);
                objENTOB.CGSTAmount = Convert.ToDecimal(txtCGST.Text);
                objENTOB.TotalTax = Convert.ToDecimal(txtTotalTax.Text);
                objENTOB.TipGratuity = txtTip.Text.Trim().Length > 0 ? Convert.ToDecimal(txtTip.Text) : 0;
                objENTOB.PayableAmount = Convert.ToDecimal(txtPayAmt.Text);
                objENTOB.DiscountRemark = txtDiscRemark.Text.Trim();
                objENTOB.DeliveryCharge = txtDeliveryCharge.Text.Trim().Length > 0 ? Convert.ToDecimal(txtDeliveryCharge.Text) : 0;
                objENTOB.Mode = "UPDATE_DISCOUNT";
                if (objDALOB.InsertUpdateDeleteOrder(objENTOB))
                {
                    //MessageBox.Show("Save successfully.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Problem in save.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                cmdEnable = false;
                this.getBranchMasterSetting();
                rdoDiscPer.Checked = true;
                btnViewPrint.Enabled = false;
                btnApply.Enabled = true;
                tcCheckOutDetail.TabPages.Remove(tabCodStep4);
                visiblePanelPaymentMethod(true, false, false);
                GetOrderTaxDiscount(txtOrderID.Text);
                cmdEnable = true;
                txtTip.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnDiscount_Click(object sender, EventArgs e)
        {
            try
            {
                ENT.DiscountMasterDetail.OrderID = new Guid(txtOrderID.Text);
                ENT.DiscountMasterDetail.DiscountTypeID = rdoDiscAmt.Checked == true ? 1 : rdoDiscPer.Checked == true ? 2 : 0;
                ENT.DiscountMasterDetail.DiscountAmtPer= txtDiscountPer.Text== string.Empty ? 0: Convert.ToDecimal(txtDiscountPer.Text);
                frmDiscountSelect frmDS = new frmDiscountSelect(Convert.ToInt32(GlobalVariable.DiscountType.OnOrder));
                frmDS.ShowDialog();

                #region Set Discount Value And Calculate
                if (ENT.DiscountMasterDetail.DiscountTypeID == 1)
                    rdoDiscAmt.Checked = true;
                else if (ENT.DiscountMasterDetail.DiscountTypeID == 2)
                    rdoDiscPer.Checked = true;
                else
                { rdoDiscPer.Checked = false; rdoDiscAmt.Checked = false; }

                if (rdoDiscAmt.Checked)
                {
                    txtDiscountType.Text = Convert.ToString(ENT.DiscountMasterDetail.DiscountTypeID);
                    txtDiscountPer.Text = "0";
                    txtDiscount.Text = Convert.ToString(ENT.DiscountMasterDetail.DiscountAmtPer);
                }
                else if (rdoDiscPer.Checked)
                {
                    txtDiscountType.Text = Convert.ToString(ENT.DiscountMasterDetail.DiscountTypeID);
                    txtDiscountPer.Text = Convert.ToString(ENT.DiscountMasterDetail.DiscountAmtPer);
                }
                txtDiscRemark.Text = ENT.DiscountMasterDetail.DiscountRemark;
                txtDiscountID.Text = ENT.DiscountMasterDetail.SelectedDiscountID.ToString();
                this.CalcTotal();
                #endregion

                this.UpdateOrderAmount();
                txtTip.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rdoCash_CheckedChanged(object sender, EventArgs e)
        {
            //lblPaymentMethod.Text = rdoCash.Text;
            visiblePanelPaymentMethod(true, false, false);
        }

        private void rdoCreditCard_CheckedChanged(object sender, EventArgs e)
        {
            //lblPaymentMethod.Text = rdoCreditCard.Text;
            visiblePanelPaymentMethod(false, true, false);
        }

        private void rdoCheque_CheckedChanged(object sender, EventArgs e)
        {
            //lblPaymentMethod.Text = rdoCheque.Text;
            visiblePanelPaymentMethod(false, false, true);
        }

        private void visiblePanelPaymentMethod(bool boolCash, bool boolCreditCard, bool boolCheque)
        {
            pnlCash.Visible = boolCash;
            pnlCheque.Visible = boolCreditCard;
            pnlCreditCard.Visible = boolCheque;
        }

        private void txtCashPaidAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtCashPaidAmt.Text.Contains(".")) && (e.KeyChar == '.'))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtCCPaidAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtCCPaidAmt.Text.Contains(".")) && (e.KeyChar == '.'))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtCCCardNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtCCCardNo.Text.Contains(".")) && (e.KeyChar == '.'))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtCQAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtCQPaidAmt.Text.Contains(".")) && (e.KeyChar == '.'))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtCCPaidAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCCOrderAmt.Text.Trim() != string.Empty && txtCCPaidAmt.Text.Trim() != string.Empty)
                {
                    txtCCChangeAmt.Text = "";
                    txtCCChangeAmt.Text = (Convert.ToDecimal(txtCCOrderAmt.Text) - Convert.ToDecimal(txtCCPaidAmt.Text)).ToString();
                    //txtPCChange.Text = txtCCChangeAmt.Text.Trim();
                }
                else
                { txtCCChangeAmt.Text = "0"; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCQAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCQOrderAmt.Text.Trim() != string.Empty && txtCQPaidAmt.Text.Trim() != string.Empty)
                {
                    txtCQChangeAmt.Text = "";
                    txtCQChangeAmt.Text = (Convert.ToDecimal(txtCQOrderAmt.Text) - Convert.ToDecimal(txtCQPaidAmt.Text)).ToString();
                    //txtPCChange.Text = txtCQChangeAmt.Text.Trim();
                }
                else
                { txtCQChangeAmt.Text = "0"; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCashPaidAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCashOrderAmt.Text.Trim() != string.Empty && txtCashPaidAmt.Text.Trim() != string.Empty)
                {
                    txtCashChangeAmt.Text = "";
                    txtCashChangeAmt.Text = (Convert.ToDecimal(txtCashOrderAmt.Text) - Convert.ToDecimal(txtCashPaidAmt.Text)).ToString();
                    //txtPCChange.Text = txtCashChangeAmt.Text.Trim();
                }
                else
                { txtCashChangeAmt.Text = "0"; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintReceipt();
        }

        private void frmCheckOut_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnNextStep1_Click(object sender, EventArgs e)
        {
            try
            {
                tcCheckOutDetail.SelectedIndex = 1;  // Move to Step-2
                rdoOther.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBackStep2_Click(object sender, EventArgs e)
        {
            tcCheckOutDetail.SelectedIndex = 0;   // Move to Step-1
        }

        private void btnNextStep2_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoGiftCard.Checked == false && rdoRewardPoint.Checked == false && rdoOther.Checked == false)
                {
                    MessageBox.Show("Atleast one CRM Method must be select.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (rdoGiftCard.Checked)
                    objENTSAVE.CRMMethod = Convert.ToInt32(GlobalVariable.CheckOutCRMMethod.GiftCard);
                else if (rdoRewardPoint.Checked)
                    objENTSAVE.CRMMethod = Convert.ToInt32(GlobalVariable.CheckOutCRMMethod.RewardPoint);
                else if (rdoOther.Checked)
                    objENTSAVE.CRMMethod = Convert.ToInt32(GlobalVariable.CheckOutCRMMethod.Other);

                tcCheckOutDetail.SelectedIndex = 2;   // Move to Step-3
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBackStep3_Click(object sender, EventArgs e)
        {
            tcCheckOutDetail.SelectedIndex = 1;  // Move to Step-2
        }

        private void btnBackStep4_Click(object sender, EventArgs e)
        {
            tcCheckOutDetail.SelectedIndex = 3;  // Move to Step-3
        }

        private void btnCloseStep4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnViewPrint_Click(object sender, EventArgs e)
        {
            tcCheckOutDetail.SelectedIndex = 1;
            GenerateReport();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                objENTSAVE.TransactionID = Guid.NewGuid();
                objENTSAVE.OrderID = new Guid(txtOrderID.Text);
                objENTSAVE.CustomerID = new Guid(txtCustomerID.Text);
                objENTSAVE.TableID = txtTableID.Text == "" ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(txtTableID.Text);

                if (rdoCash.Checked == false && rdoCreditCard.Checked == false && rdoCheque.Checked == false)
                {
                    MessageBox.Show("Atleast one Payment Method must be select.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rdoCash.Focus();
                    return;
                }
                if (rdoCash.Checked == true)
                {
                    #region Cash
                    if (txtCashOrderAmt.Text == string.Empty)
                    {
                        MessageBox.Show("Order amount should not be empty.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCashOrderAmt.Focus();
                        return;
                    }
                    if (txtCashPaidAmt.Text == string.Empty)
                    {
                        MessageBox.Show("Paid amount should not be empty.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCashPaidAmt.Focus();
                        return;
                    }
                    objENTSAVE.PaymentMethod = Convert.ToInt32(GlobalVariable.CheckOutPaymentMethod.Cash);
                    objENTSAVE.OrderAmount = Convert.ToDecimal(txtCashOrderAmt.Text);
                    objENTSAVE.PaidAmount = Convert.ToDecimal(txtCashPaidAmt.Text);
                    objENTSAVE.ChangeAmount = Convert.ToDecimal(txtCashChangeAmt.Text);
                    #endregion
                }
                else if (rdoCreditCard.Checked == true)
                {
                    #region Credit Card
                    if (txtCCOrderAmt.Text == string.Empty)
                    {
                        MessageBox.Show("Order amount should not be empty.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCCOrderAmt.Focus();
                        return;
                    }
                    if (txtCCPaidAmt.Text == string.Empty)
                    {
                        MessageBox.Show("Paid amount should not be empty.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCCPaidAmt.Focus();
                        return;
                    }
                    if (txtCCCardHolderName.Text == string.Empty)
                    {
                        MessageBox.Show("Card holder name should not be empty.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCCCardHolderName.Focus();
                        return;
                    }
                    if (txtCCCardNo.Text == string.Empty)
                    {
                        MessageBox.Show("Card holder name should not be empty.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCCCardNo.Focus();
                        return;
                    }

                    objENTSAVE.PaymentMethod = Convert.ToInt32(GlobalVariable.CheckOutPaymentMethod.CreditCard);
                    objENTSAVE.CardHolderName = txtCCCardHolderName.Text;
                    objENTSAVE.CardNumber = txtCCCardNo.Text;
                    objENTSAVE.OrderAmount = Convert.ToDecimal(txtCCOrderAmt.Text);
                    objENTSAVE.PaidAmount = Convert.ToDecimal(txtCCPaidAmt.Text);
                    objENTSAVE.ChangeAmount = Convert.ToDecimal(txtCCChangeAmt.Text);
                    #endregion
                }
                else if (rdoCheque.Checked == true)
                {
                    #region Cheque
                    if (txtCQOrderAmt.Text == string.Empty)
                    {
                        MessageBox.Show("Order amount should not be empty.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCQOrderAmt.Focus();
                        return;
                    }
                    if (txtCQPaidAmt.Text == string.Empty)
                    {
                        MessageBox.Show("Cheque amount should not be empty.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCQPaidAmt.Focus();
                        return;
                    }
                    if (txtCQChequeNo.Text == string.Empty)
                    {
                        MessageBox.Show("Cheque number should not be empty.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCQChequeNo.Focus();
                        return;
                    }
                    if (!GlobalVariable.IsDate(txtCQDate.Text))
                    {
                        MessageBox.Show("Enter valid cheque date.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCCCardNo.Focus();
                        return;
                    }

                    objENTSAVE.PaymentMethod = Convert.ToInt32(GlobalVariable.CheckOutPaymentMethod.Cheque);
                    objENTSAVE.ChequeNo = txtCQChequeNo.Text;
                    objENTSAVE.ChequeDate = GlobalVariable.ChangeDate(txtCQDate.Text);
                    objENTSAVE.OrderAmount = Convert.ToDecimal(txtCQOrderAmt.Text);
                    objENTSAVE.PaidAmount = Convert.ToDecimal(txtCQPaidAmt.Text);
                    objENTSAVE.ChangeAmount = Convert.ToDecimal(txtCQChangeAmt.Text);
                    #endregion
                }

                objENTSAVE.TableStatusID = Convert.ToInt32(GlobalVariable.TableStatus.Vacant);
                objENTSAVE.OrderActions = Convert.ToInt32(GlobalVariable.OrderActions.Paid);
                objENTSAVE.OrderStatus = Convert.ToInt32(GlobalVariable.OrderStatus.CLOSE);
                objENTSAVE.EntryDateTime = GlobalVariable.ChangeDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                objENTSAVE.Mode = "ADD";
                if (objDALCOD.InsertUpdateDeleteCheckOutDetail(objENTSAVE))
                {
                    if (rdoCash.Checked == true)
                    {
                        ENT.TillManage objENTTill = new ENT.TillManage();
                        DAL.TillManage objDALTill = new DAL.TillManage();
                        objENTTill.Mode = "UPDATE_CASH_PAYMENT";
                        objENTTill.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                        objENTTill.Cash = txtCashOrderAmt.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtCashOrderAmt.Text);
                        objDALTill.InsertUpdateDeleteTillManage(objENTTill);
                    }

                    //tcCheckOutDetail.SelectedIndex = 3;
                    tcCheckOutDetail.TabPages.Add(tabCodStep4);
                    tcCheckOutDetail.TabPages.Remove(tabCodStep3);
                    tcCheckOutDetail.TabPages.Remove(tabCodStep2);
                    tcCheckOutDetail.TabPages.Remove(tabCodStep1);

                    panel12.Enabled = false;
                    btnBackStep3.Enabled = false;
                    btnViewPrint.Enabled = true;
                    btnApply.Enabled = false;
                    GenerateReport();

                    List<ENT.GeneralSetting> lstSetting = GlobalVariable.GetGeneralSetting();
                    if (lstSetting.Count > 0)
                    {
                        if (lstSetting[0].PrintOnPaymentDone)
                        { PrintReceipt(); }
                    }
                    
                    MessageBox.Show("Order payment successfully done.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCloseStep4.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCloseStep1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtTip.Text.Contains(".")) && (e.KeyChar == '.'))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtDeliveryCharge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtDeliveryCharge.Text.Contains(".")) && (e.KeyChar == '.'))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtTip_Leave(object sender, EventArgs e)
        {
            if (cmdEnable)
            {
                CalcTotal();
                UpdateOrderAmount();
            }
        }

        private void txtDeliveryCharge_Leave(object sender, EventArgs e)
        {
            if (cmdEnable)
            {
                CalcTotal();
                UpdateOrderAmount();
            }
        }
    }
}
