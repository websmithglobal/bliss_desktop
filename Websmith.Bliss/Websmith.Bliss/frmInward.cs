using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.Bliss
{
    public partial class frmInward : Form
    {
        string cmm = string.Empty;
        bool calc = true;

        public frmInward()
        {
            InitializeComponent();
        }

        public frmInward(string Inward_ID, string Mode)
        {
            InitializeComponent();
            cmm = Mode;
            txtInwardID.Text = Inward_ID;
        }

        private void clearControl()
        {
            try
            {
                calc = false;
                txtInvNo.Text = string.Empty;
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtPONo.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtRecQty.Text = string.Empty;
                txtRejQty.Text = string.Empty;
                txtTotAmt.Text = string.Empty;
                txtTotQty.Text = string.Empty;
                txtRemark.Text = string.Empty;
                txtRondOff.Text = string.Empty;
                txtSubTot.Text = string.Empty;
                txtSubTotalAmount.Text = string.Empty;
                txtOtherCharge.Text = string.Empty;
                txtOtherChargeDet.Text = string.Empty;
                txtDiscAmt.Text = string.Empty;
                txtTaxAmt.Text = string.Empty;
                txtTotTaxAmt.Text = string.Empty;
                txtFinalTotal.Text = string.Empty;
                dgvItem.Rows.Clear();
                FillComboVendor(); FillComboProduct(); FillComboUnitType();
                calc = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearItemControl()
        {
            try
            {
                calc = false;
                txtRate.Text = string.Empty;
                txtRecQty.Text = string.Empty;
                txtRejQty.Text = string.Empty;
                txtSubTot.Text = string.Empty;
                txtTaxAmt.Text = string.Empty;
                txtTotAmt.Text = string.Empty;
                txtTotQty.Text = string.Empty;
                btnEdit.Enabled = true;
                FillComboProduct(); FillComboUnitType();
                calc = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculationItemTotal()
        {
            try
            {
                txtRecQty.Text = string.IsNullOrEmpty(txtRecQty.Text) ? "0" : txtRecQty.Text;
                txtRejQty.Text = string.IsNullOrEmpty(txtRejQty.Text) ? "0" : txtRejQty.Text;
                txtRate.Text = string.IsNullOrEmpty(txtRate.Text) ? "0" : txtRate.Text;
                txtTaxAmt.Text = string.IsNullOrEmpty(txtTaxAmt.Text) ? "0" : txtTaxAmt.Text;

                txtTotQty.Text = Math.Round(Convert.ToDecimal(txtRecQty.Text) - Convert.ToDecimal(txtRejQty.Text),2, MidpointRounding.AwayFromZero).ToString();
                txtSubTot.Text = Math.Round(Convert.ToDecimal(txtTotQty.Text) * Convert.ToDecimal(txtRate.Text), 2, MidpointRounding.AwayFromZero).ToString();
                txtTotAmt.Text = Math.Round(Convert.ToDecimal(txtSubTot.Text) + Convert.ToDecimal(txtTaxAmt.Text), 2, MidpointRounding.AwayFromZero).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateTotal()
        {
            try
            {
                decimal other_charge = string.IsNullOrEmpty(txtOtherCharge.Text) ? 0 : Convert.ToDecimal(txtOtherCharge.Text);
                decimal disc_amt = string.IsNullOrEmpty(txtDiscAmt.Text) ? 0 : Convert.ToDecimal(txtDiscAmt.Text);
                decimal tax_amt = string.IsNullOrEmpty(txtTotTaxAmt.Text) ? 0 : Convert.ToDecimal(txtTotTaxAmt.Text);

                txtSubTotalAmount.Text = "0";
                for (int j = 0; j < dgvItem.RowCount; j++)
                {
                    txtSubTotalAmount.Text = (Convert.ToDecimal(txtSubTotalAmount.Text) + Convert.ToDecimal(dgvItem.Rows[j].Cells["colTotAmt"].Value.ToString())).ToString();
                }

                decimal sub_total = string.IsNullOrEmpty(txtSubTotalAmount.Text) ? 0 : Convert.ToDecimal(txtSubTotalAmount.Text);
                decimal final_total = (sub_total + other_charge + tax_amt) - disc_amt;
                decimal round_off = final_total - Math.Round(final_total, 0, MidpointRounding.AwayFromZero);

                txtRondOff.Text = round_off.ToString();
                txtFinalTotal.Text = Math.Round(final_total - round_off, 2, MidpointRounding.AwayFromZero).ToString(); // round_off < 0 ? (final_total + round_off).ToString() : (final_total - round_off).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillComboVendor()
        {
            try
            {
                ENT.VendorMasterData objENTVMD = new ENT.VendorMasterData();
                List<ENT.VendorMasterData> lstENTVMD = new List<ENT.VendorMasterData>();
                BindingSource bs = new BindingSource();
                objENTVMD.Mode = "GetAllRecord";
                lstENTVMD = new DAL.VendorMasterData().getVendorMaster(objENTVMD);
                bs.DataSource = lstENTVMD;
                cmbVendor.DataSource = bs;
                cmbVendor.DisplayMember = "VendorName";
                cmbVendor.ValueMember = "VendorID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillComboProduct()
        {
            try
            {
                ENT.IngredientsMasterDetail objENTIMD = new ENT.IngredientsMasterDetail();
                List<ENT.IngredientsMasterDetail> lstENTIMD = new List<ENT.IngredientsMasterDetail>();
                BindingSource bs = new BindingSource();
                objENTIMD.Mode = "GetIngredientsForLookupCombo"; //GetProductForLookupCombo
                lstENTIMD = new DAL.IngredientsMasterDetail().getIngredientsMasterDetail(objENTIMD);
                bs.DataSource = lstENTIMD;
                cmbProduct.DataSource = bs;
                cmbProduct.DisplayMember = "IngredientName";
                cmbProduct.ValueMember = "IngredientsID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillComboUnitType()
        {
            try
            {
                ENT.IngredientUnitTypeDetail objENTIUTD = new ENT.IngredientUnitTypeDetail();
                List<ENT.IngredientUnitTypeDetail> lstENTIUTD = new List<ENT.IngredientUnitTypeDetail>();
                BindingSource bs = new BindingSource();
                objENTIUTD.Mode = "GetAllRecord";
                lstENTIUTD = new DAL.IngredientUnitTypeDetail().getIngredientUnitTypeDetail(objENTIUTD);
                bs.DataSource = lstENTIUTD;
                cmbUnitType.DataSource = bs;
                cmbUnitType.DisplayMember = "UnitType";
                cmbUnitType.ValueMember = "UnitTypeID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmInward_Load(object sender, EventArgs e)
        {
            if (cmm == "ADD")
            {
                clearControl();
                txtInvNo.Focus();
            }
            else
            {
                clearControl();
                EditItem();
            }
        }

        private void frmInward_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void EditItem()
        {
            try
            {
                calc = false;
                ENT.InwardMaster objENT = new ENT.InwardMaster();
                objENT.InwardID = new Guid(txtInwardID.Text);
                objENT.Mode = "GetByID";
                List<ENT.InwardMaster> lstENT = new DAL.InwardMaster().GetInwardMaster(objENT);
                if (lstENT.Count > 0)
                {
                    txtInvNo.Text = lstENT[0].InvoiceNo;
                    txtDate.Text = lstENT[0].InvoiceDate;
                    cmbVendor.SelectedValue = lstENT[0].VedorID;
                    txtPONo.Text = lstENT[0].PONo.Trim();
                    txtOtherChargeDet.Text = lstENT[0].OtherChargeDetail.Trim();
                    txtOtherCharge.Text = lstENT[0].OtherCharge.ToString();
                    txtDiscAmt.Text = lstENT[0].DiscountAmount.ToString();
                    txtTotTaxAmt.Text = lstENT[0].TaxAmount.ToString();
                    txtRondOff.Text = lstENT[0].RoundOffAmount.ToString();
                    txtFinalTotal.Text = lstENT[0].FinalTotal.ToString();
                    txtRemark.Text = lstENT[0].Remark.Trim();

                    ENT.InwardDetail objENTID = new ENT.InwardDetail();
                    objENTID.InwardIDF = new Guid(txtInwardID.Text);
                    objENTID.Mode = "GetByID";
                    List<ENT.InwardDetail> lstENTID = new DAL.InwardDetail().GetInwardDetail(objENTID);
                    for (int i = 0; i < lstENTID.Count; i++)
                    {
                        dgvItem.Rows.Add();
                        dgvItem.Rows[i].Cells["colProduct"].Value = Convert.ToString(lstENTID[i].ProductName);
                        dgvItem.Rows[i].Cells["colProductId"].Value = Convert.ToString(lstENTID[i].ProductID);
                        dgvItem.Rows[i].Cells["colUnitType"].Value = Convert.ToString(lstENTID[i].UnitType);
                        dgvItem.Rows[i].Cells["colUnitTypeId"].Value = Convert.ToString(lstENTID[i].UnitTypeID);
                        dgvItem.Rows[i].Cells["colRecQty"].Value = Convert.ToString(lstENTID[i].RecQty);
                        dgvItem.Rows[i].Cells["colRejQty"].Value = Convert.ToString(lstENTID[i].RejQty);
                        dgvItem.Rows[i].Cells["colTotQty"].Value = Convert.ToString(lstENTID[i].TotQty);
                        dgvItem.Rows[i].Cells["colRate"].Value = Convert.ToString(lstENTID[i].Rate);
                        dgvItem.Rows[i].Cells["colSubTot"].Value = Convert.ToString(lstENTID[i].SubTotal);
                        dgvItem.Rows[i].Cells["colTaxAmt"].Value = Convert.ToString(lstENTID[i].TaxAmount);
                        dgvItem.Rows[i].Cells["colTotAmt"].Value = Convert.ToString(lstENTID[i].TotalAmount);
                    }
                    this.CalculateTotal();
                }
                else
                {
                    MessageBox.Show("Record Not Found.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                calc = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInwardID.Text.Trim()))
                {
                    MessageBox.Show("Inward ID is not valid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (new DAL.InwardMaster().getDuplicateInvoiceByInvoiceNo(txtInwardID.Text.Trim(), txtInvNo.Text.Trim()) > 0)
                {
                    MessageBox.Show("Duplicate Invoice No. Found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInvNo.Focus();
                    return;
                }

                Control[] ctrl = { txtInvNo, cmbVendor, txtFinalTotal, dgvItem };
                if (GlobalVariable.CheckValidate(ctrl))
                {
                    ENT.InwardMaster objENTIM = new ENT.InwardMaster();
                    objENTIM.InwardID = new Guid(txtInwardID.Text);
                    objENTIM.InvoiceNo = txtInvNo.Text.Trim();
                    objENTIM.InvoiceDate = GlobalVariable.ChangeDate(txtDate.Text);
                    objENTIM.VedorID = new Guid(Convert.ToString(cmbVendor.SelectedValue));
                    objENTIM.PONo = string.IsNullOrEmpty(txtPONo.Text.Trim()) ? "" : txtPONo.Text.Trim();
                    objENTIM.OtherChargeDetail = string.IsNullOrEmpty(txtOtherChargeDet.Text.Trim()) ? "" : txtOtherChargeDet.Text.Trim();
                    objENTIM.OtherCharge = string.IsNullOrEmpty(txtOtherCharge.Text.Trim()) ? 0 : Convert.ToDecimal(txtOtherCharge.Text);
                    objENTIM.DiscountAmount = string.IsNullOrEmpty(txtDiscAmt.Text.Trim()) ? 0 : Convert.ToDecimal(txtDiscAmt.Text);
                    objENTIM.TaxAmount = string.IsNullOrEmpty(txtTotTaxAmt.Text.Trim()) ? 0 : Convert.ToDecimal(txtTotTaxAmt.Text);
                    objENTIM.RoundOffAmount = string.IsNullOrEmpty(txtRondOff.Text.Trim()) ? 0 : Convert.ToDecimal(txtRondOff.Text);
                    objENTIM.FinalTotal = string.IsNullOrEmpty(txtFinalTotal.Text.Trim()) ? 0 : Convert.ToDecimal(txtFinalTotal.Text);
                    objENTIM.Remark = string.IsNullOrEmpty(txtRemark.Text.Trim()) ? "" : txtRemark.Text.Trim();
                    objENTIM.Mode = cmm;
                    bool resultMaster = false;
                    bool resultDetail = false;
                    if (new DAL.InwardMaster().InsertUpdateDeleteInwardMaster(objENTIM))
                    {
                        if (cmm == "UPDATE")
                        {
                            ENT.InwardDetail objENTDEL = new ENT.InwardDetail();
                            objENTDEL.InwardIDF = new Guid(txtInwardID.Text);
                            objENTDEL.Mode = "DELETE";
                            new DAL.InwardDetail().InsertUpdateDeleteInwardDetail(objENTDEL);
                        }

                        resultMaster = true;
                        for (int i = 0; i < dgvItem.RowCount; i++)
                        {
                            ENT.InwardDetail objENTID = new ENT.InwardDetail();
                            objENTID.InwardDetailID = Guid.NewGuid();
                            objENTID.InwardIDF = new Guid(txtInwardID.Text);
                            objENTID.ProductID = new Guid(Convert.ToString(dgvItem.Rows[i].Cells["colProductId"].Value));
                            objENTID.UnitTypeID = new Guid(Convert.ToString(dgvItem.Rows[i].Cells["colUnitTypeId"].Value));
                            objENTID.RecQty = Convert.ToDecimal(dgvItem.Rows[i].Cells["colRecQty"].Value);
                            objENTID.RejQty = Convert.ToDecimal(dgvItem.Rows[i].Cells["colRejQty"].Value);
                            objENTID.TotQty = Convert.ToDecimal(dgvItem.Rows[i].Cells["colTotQty"].Value);
                            objENTID.Rate = Convert.ToDecimal(dgvItem.Rows[i].Cells["colRate"].Value);
                            objENTID.SubTotal = Convert.ToDecimal(dgvItem.Rows[i].Cells["colSubTot"].Value);
                            objENTID.TaxAmount = Convert.ToDecimal(dgvItem.Rows[i].Cells["colTaxAmt"].Value);
                            objENTID.TotalAmount = Convert.ToDecimal(dgvItem.Rows[i].Cells["colTotAmt"].Value);
                            objENTID.Sort = i;
                            objENTID.IsUpStream = 0;
                            objENTID.Mode = "ADD";
                            if (new DAL.InwardDetail().InsertUpdateDeleteInwardDetail(objENTID))
                            { resultDetail = true; }
                            else { resultDetail = false; }
                        }
                    }
                    else { resultMaster = false; }

                    if (resultMaster && resultDetail)
                    {
                        MessageBox.Show("Inward Saved Successfull.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                        txtInwardID.Text = Guid.NewGuid().ToString();
                        cmm = "ADD";
                        txtInvNo.Focus();
                    }
                    else if (resultMaster)
                    {
                        MessageBox.Show("Inward Master Saved Successfull. But Inward Item Detail Not Saved Properly.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Not Saved Successfull.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInwardID.Text.Trim()))
                {
                    MessageBox.Show("Inward ID is not valid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                Control[] ctrl = { cmbProduct, cmbUnitType, txtRecQty, txtRejQty, txtTotQty, txtRate, txtSubTot, txtTaxAmt, txtTotAmt };
                if (GlobalVariable.CheckValidate(ctrl))
                {
                    if (Convert.ToDecimal(txtTotQty.Text) < 0)
                    {
                        MessageBox.Show("Total quantity must be greater than zero.");
                        txtRecQty.Focus();
                    }

                    dgvItem.Rows.Add();
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colProduct"].Value = Convert.ToString(cmbProduct.Text);
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colProductId"].Value = Convert.ToString(cmbProduct.SelectedValue);
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colUnitType"].Value = Convert.ToString(cmbUnitType.Text);
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colUnitTypeId"].Value = Convert.ToString(cmbUnitType.SelectedValue);
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colRecQty"].Value = txtRecQty.Text.Trim();
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colRejQty"].Value = txtRejQty.Text.Trim();
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colTotQty"].Value = txtTotQty.Text.Trim();
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colRate"].Value = txtRate.Text.Trim();
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colSubTot"].Value = txtSubTot.Text.Trim();
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colTaxAmt"].Value = txtTaxAmt.Text.Trim();
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colTotAmt"].Value = txtTotAmt.Text.Trim();
                    if (MessageBox.Show("You want add more item.", "Conform", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    { cmbProduct.Focus(); }
                    else
                    { txtOtherChargeDet.Focus(); }
                    clearItemControl();
                }
                CalculateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvItem.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure? Edit selected item.", "Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        calc = false;
                        int row = dgvItem.CurrentRow.Index;
                        cmbProduct.SelectedValue = new Guid(dgvItem.Rows[row].Cells["colProductId"].Value.ToString());
                        cmbUnitType.SelectedValue = new Guid(dgvItem.Rows[row].Cells["colUnitTypeId"].Value.ToString());
                        txtRecQty.Text = dgvItem.Rows[row].Cells["colRecQty"].Value.ToString();
                        txtRejQty.Text = dgvItem.Rows[row].Cells["colRejQty"].Value.ToString();
                        txtTotQty.Text = dgvItem.Rows[row].Cells["colTotQty"].Value.ToString();
                        txtRate.Text = dgvItem.Rows[row].Cells["colRate"].Value.ToString();
                        txtSubTot.Text = dgvItem.Rows[row].Cells["colSubTot"].Value.ToString();
                        txtTaxAmt.Text = dgvItem.Rows[row].Cells["colTaxAmt"].Value.ToString();
                        txtTotAmt.Text = dgvItem.Rows[row].Cells["colTotAmt"].Value.ToString();
                        btnEdit.Enabled = false;
                        calc = true;
                        dgvItem.Rows.RemoveAt(row);
                        this.CalculateTotal();
                        cmbProduct.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found for edit item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dgvItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (dgvItem.Rows.Count > 0)
                    {
                        if (MessageBox.Show("Are you sure? Delete selected item.", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int row = dgvItem.CurrentRow.Index;
                            dgvItem.Rows.RemoveAt(row);
                            this.CalculateTotal();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Record not found for delete item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearItemControl();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtRecQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtRecQty.Text.Contains(".")) && (e.KeyChar == '.'))
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

        private void txtRejQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtRejQty.Text.Contains(".")) && (e.KeyChar == '.'))
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

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtRate.Text.Contains(".")) && (e.KeyChar == '.'))
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

        private void txtTaxAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtTaxAmt.Text.Contains(".")) && (e.KeyChar == '.'))
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

        private void txtRecQty_TextChanged(object sender, EventArgs e)
        {
            if (calc)
            { this.CalculationItemTotal(); }
        }

        private void txtOtherCharge_TextChanged(object sender, EventArgs e)
        {
            if (calc)
            { this.CalculateTotal(); }
        }

        private void txtRejQty_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal Rec_Qty = string.IsNullOrEmpty(txtRecQty.Text) ? 0 : Convert.ToDecimal(txtRecQty.Text);
                decimal Rej_Qty = string.IsNullOrEmpty(txtRejQty.Text) ? 0 : Convert.ToDecimal(txtRejQty.Text);
                if (Rec_Qty < Rej_Qty)
                {
                    MessageBox.Show("Receive quantity must be greater than reject quantity.");
                    txtRejQty.Focus();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cmbVendor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { SendKeys.Send("{F4}"); }
        }

        private void cmbUnitType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                frmUnitTypeMaster frmUTM = new frmUnitTypeMaster();
                frmUTM.ShowDialog();
                this.FillComboUnitType();
            }
        }

    }
}
