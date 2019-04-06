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
    public partial class frmOutward : Form
    {
        string cmm = string.Empty;
        bool calc = true;
        //bool IsEditItem = false;

        public frmOutward()
        {
            InitializeComponent();
        }

        public frmOutward(string Inward_ID, string Mode)
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
                txtInvNo.Enabled = true;
                txtInvNo.Text = string.Empty;
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtRate.Text = string.Empty;
                txtTotAmt.Text = string.Empty;
                txtTotQty.Text = string.Empty;
                txtRemark.Text = string.Empty;
                txtFinalTotal.Text = string.Empty;
                dgvItem.Rows.Clear();
                FillComboVendor(); FillComboProduct(); FillComboUnitType();
                if (cmbEmp.Items.Count > 0) cmbEmp.SelectedIndex = -1;
                if (cmbProduct.Items.Count > 0) cmbProduct.SelectedIndex = -1;
                if (cmbUnitType.Items.Count > 0) cmbUnitType.SelectedIndex = -1;
                txtStockEdit.Text = string.Empty;
                txtStock.Text = string.Empty;
                getNewInvoiceNo();
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
                txtTotAmt.Text = string.Empty;
                txtTotQty.Text = string.Empty;
                FillComboProduct(); FillComboUnitType();
                if (cmbProduct.Items.Count > 0) cmbProduct.SelectedIndex = -1;
                if (cmbUnitType.Items.Count > 0) cmbUnitType.SelectedIndex = -1;
                txtStockEdit.Text = string.Empty;
                txtStock.Text = string.Empty;
                btnEdit.Enabled = true;
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
                txtRate.Text = string.IsNullOrEmpty(txtRate.Text) ? "0" : txtRate.Text;
                txtTotQty.Text = string.IsNullOrEmpty(txtTotQty.Text) ? "0" : txtTotQty.Text;
                txtTotAmt.Text = Math.Round(Convert.ToDecimal(txtTotQty.Text) * Convert.ToDecimal(txtRate.Text), 2, MidpointRounding.AwayFromZero).ToString();
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
                txtFinalTotal.Text = "0";
                for (int j = 0; j < dgvItem.RowCount; j++)
                {
                    txtFinalTotal.Text = (Convert.ToDecimal(txtFinalTotal.Text) + Convert.ToDecimal(dgvItem.Rows[j].Cells["colTotAmt"].Value.ToString())).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getNewInvoiceNo()
        {
            try
            {
                txtInvNo.Text = Convert.ToString(new DAL.OutwardMaster().getNewInvoiceNo());
                txtInvNo.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getProductStock(string ProductID)
        {
            try
            {
                if (cmm == "ADD")
                {
                    txtStock.Text = Convert.ToString(new DAL.OutwardMaster().getProductWiseStock(ProductID));
                }
                else
                {
                    txtStockEdit.Text = string.IsNullOrEmpty(txtStockEdit.Text) ? "0" : txtStockEdit.Text;
                    txtStock.Text = Convert.ToString(Convert.ToDecimal(new DAL.OutwardMaster().getProductWiseStock(ProductID)) + Convert.ToDecimal(txtStockEdit.Text));
                }
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
                ENT.EmployeeMasterList objENTVMD = new ENT.EmployeeMasterList();
                List<ENT.EmployeeMasterList> lstENTVMD = new List<ENT.EmployeeMasterList>();
                BindingSource bs = new BindingSource();
                objENTVMD.Mode = "GetAllRecord";
                lstENTVMD = new DAL.EmployeeMasterList().getEmployeeMasterList(objENTVMD);
                bs.DataSource = lstENTVMD;
                cmbEmp.DataSource = bs;
                cmbEmp.DisplayMember = "EmpName";
                cmbEmp.ValueMember = "EmployeeID";
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
                txtInvNo.Enabled = false;
                txtDate.Focus();
            }
            else
            {
                clearControl();
                EditItem();
                txtInvNo.Enabled = false;
                txtDate.Focus();
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
                ENT.OutwardMaster objENT = new ENT.OutwardMaster();
                objENT.OutwardID = new Guid(txtInwardID.Text);
                objENT.Mode = "GetByID";
                List<ENT.OutwardMaster> lstENT = new DAL.OutwardMaster().GetOutwardMaster(objENT);
                if (lstENT.Count > 0)
                {
                    txtInvNo.Text = lstENT[0].InvoiceNo.ToString();
                    txtDate.Text = lstENT[0].InvoiceDate;
                    cmbEmp.SelectedValue = lstENT[0].EmpID;
                    txtFinalTotal.Text = lstENT[0].FinalTotal.ToString();
                    txtRemark.Text = lstENT[0].Remark.Trim();

                    ENT.OutwardDetail objENTOD = new ENT.OutwardDetail();
                    objENTOD.OutwardIDF = new Guid(txtInwardID.Text);
                    objENTOD.Mode = "GetByID";
                    List<ENT.OutwardDetail> lstENTID = new DAL.OutwardDetail().GetOutwardDetail(objENTOD);
                    for (int i = 0; i < lstENTID.Count; i++)
                    {
                        dgvItem.Rows.Add();
                        dgvItem.Rows[i].Cells["colProduct"].Value = Convert.ToString(lstENTID[i].ProductName);
                        dgvItem.Rows[i].Cells["colProductId"].Value = Convert.ToString(lstENTID[i].ProductID);
                        dgvItem.Rows[i].Cells["colUnitType"].Value = Convert.ToString(lstENTID[i].UnitType);
                        dgvItem.Rows[i].Cells["colUnitTypeId"].Value = Convert.ToString(lstENTID[i].UnitTypeID);
                        dgvItem.Rows[i].Cells["colTotQty"].Value = Convert.ToString(lstENTID[i].Qty);
                        dgvItem.Rows[i].Cells["colRate"].Value = Convert.ToString(lstENTID[i].Rate);
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
                    MessageBox.Show("Outward ID is not valid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtInvNo.Text.Trim()))
                {
                    MessageBox.Show("Invoice No. is not valid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (new DAL.OutwardMaster().getDuplicateInvoiceByInvoiceNo(txtInwardID.Text.Trim(), txtInvNo.Text.Trim()) > 0)
                {
                    MessageBox.Show("Duplicate Invoice No. Found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInvNo.Focus();
                    return;
                }

                Control[] ctrl = { txtInvNo, cmbEmp, txtFinalTotal, dgvItem };
                if (GlobalVariable.CheckValidate(ctrl))
                {
                    ENT.OutwardMaster objENTIM = new ENT.OutwardMaster();
                    objENTIM.OutwardID = new Guid(txtInwardID.Text);
                    objENTIM.InvoiceNo = Convert.ToInt64(txtInvNo.Text.Trim());
                    objENTIM.InvoiceDate = GlobalVariable.ChangeDate(txtDate.Text);
                    objENTIM.EmpID = new Guid(Convert.ToString(cmbEmp.SelectedValue));
                    objENTIM.FinalTotal = string.IsNullOrEmpty(txtFinalTotal.Text.Trim()) ? 0 : Convert.ToDecimal(txtFinalTotal.Text);
                    objENTIM.Remark = string.IsNullOrEmpty(txtRemark.Text.Trim()) ? "" : txtRemark.Text.Trim();
                    objENTIM.Mode = cmm;
                    bool resultMaster = false;
                    bool resultDetail = false;
                    if (new DAL.OutwardMaster().InsertUpdateDeleteOutwardMaster(objENTIM))
                    {
                        if (cmm == "UPDATE")
                        {
                            ENT.OutwardDetail objENTDEL = new ENT.OutwardDetail();
                            objENTDEL.OutwardIDF = new Guid(txtInwardID.Text);
                            objENTDEL.Mode = "DELETE";
                            new DAL.OutwardDetail().InsertUpdateDeleteOutwardDetail(objENTDEL);
                        }

                        resultMaster = true;
                        for (int i = 0; i < dgvItem.RowCount; i++)
                        {
                            ENT.OutwardDetail objENTOD = new ENT.OutwardDetail();
                            objENTOD.OutwardDetailID = Guid.NewGuid();
                            objENTOD.OutwardIDF = new Guid(txtInwardID.Text);
                            objENTOD.ProductID = new Guid(Convert.ToString(dgvItem.Rows[i].Cells["colProductId"].Value));
                            objENTOD.UnitTypeID = new Guid(Convert.ToString(dgvItem.Rows[i].Cells["colUnitTypeId"].Value));
                            objENTOD.Qty = Convert.ToDecimal(dgvItem.Rows[i].Cells["colTotQty"].Value);
                            objENTOD.Rate = Convert.ToDecimal(dgvItem.Rows[i].Cells["colRate"].Value);
                            objENTOD.TotalAmount = Convert.ToDecimal(dgvItem.Rows[i].Cells["colTotAmt"].Value);
                            objENTOD.Sort = i;
                            objENTOD.IsUpStream = 0;
                            objENTOD.Mode = "ADD";
                            if (new DAL.OutwardDetail().InsertUpdateDeleteOutwardDetail(objENTOD))
                            { resultDetail = true; }
                            else { resultDetail = false; }
                        }
                    }
                    else { resultMaster = false; }

                    if (resultMaster && resultDetail)
                    {
                        MessageBox.Show("Outward Saved Successfull.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                        txtInwardID.Text = Guid.NewGuid().ToString();
                        cmm = "ADD";
                        txtDate.Focus();
                    }
                    else if (resultMaster)
                    {
                        MessageBox.Show("Outward Master Saved Successfull. But Outward Item Detail Not Saved Properly.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Outward ID is not valid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                Control[] ctrl = { cmbProduct, cmbUnitType, txtTotQty, txtRate, txtTotAmt };
                if (GlobalVariable.CheckValidate(ctrl))
                {
                    if (Convert.ToDecimal(txtTotQty.Text) < 0)
                    {
                        MessageBox.Show("Receive quantity must be greater than zero.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTotQty.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(txtStock.Text) < Convert.ToDecimal(txtTotQty.Text))
                    {
                        MessageBox.Show("Quantity must be less than stock quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTotQty.Focus();
                        return;
                    }

                    for (int j = 0; j < dgvItem.RowCount; j++)
                    {
                        if (dgvItem.Rows[j].Cells["colProductId"].Value.ToString().Trim() == Convert.ToString(cmbProduct.SelectedValue).Trim())
                        {
                            MessageBox.Show("Selected Product Already Added In List.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    dgvItem.Rows.Add();
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colProduct"].Value = Convert.ToString(cmbProduct.Text);
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colProductId"].Value = Convert.ToString(cmbProduct.SelectedValue);
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colUnitType"].Value = Convert.ToString(cmbUnitType.Text);
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colUnitTypeId"].Value = Convert.ToString(cmbUnitType.SelectedValue);
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colTotQty"].Value = txtTotQty.Text.Trim();
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colRate"].Value = txtRate.Text.Trim();
                    dgvItem.Rows[dgvItem.Rows.Count - 1].Cells["colTotAmt"].Value = txtTotAmt.Text.Trim();
                    clearItemControl();
                    if (MessageBox.Show("You want add more item.", "Conform", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    { cmbProduct.Focus(); }
                    else
                    { txtRemark.Focus(); }
                    btnEdit.Enabled = true;
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
                        string tmp_cmm = cmm; cmm = "";
                        int row = dgvItem.CurrentRow.Index;
                        cmbProduct.SelectedValue = new Guid(dgvItem.Rows[row].Cells["colProductId"].Value.ToString());
                        cmbUnitType.SelectedValue = new Guid(dgvItem.Rows[row].Cells["colUnitTypeId"].Value.ToString());
                        txtTotQty.Text = dgvItem.Rows[row].Cells["colTotQty"].Value.ToString();
                        txtStockEdit.Text = dgvItem.Rows[row].Cells["colTotQty"].Value.ToString();
                        txtRate.Text = dgvItem.Rows[row].Cells["colRate"].Value.ToString();
                        txtTotAmt.Text = dgvItem.Rows[row].Cells["colTotAmt"].Value.ToString();
                        btnEdit.Enabled = false;
                        calc = true;
                        cmm = tmp_cmm;
                        dgvItem.Rows.RemoveAt(row);
                        this.getProductStock(Convert.ToString(cmbProduct.SelectedValue).Trim());
                        this.CalculateTotal();
                        cmbProduct.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found for delete item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgvItem.Rows.Count > 0)
                {
                    if (e.KeyCode == Keys.Delete)
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

        private void txtTotQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtTotQty.Text.Contains(".")) && (e.KeyChar == '.'))
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

        private void cmbProduct_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmm == "ADD" || cmm == "UPDATE")
                {
                    if (Convert.ToString(cmbProduct.SelectedValue).Trim().Length == 36)
                    {
                        this.getProductStock(Convert.ToString(cmbProduct.SelectedValue).Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbEmp_KeyUp(object sender, KeyEventArgs e)
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
