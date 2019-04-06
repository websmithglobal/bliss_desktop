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
    public partial class frmVendorMaster : Form
    {
        string strMode = "";
        DAL.VendorMasterData objDALVendor = new DAL.VendorMasterData();
        ENT.VendorMasterData objENTVendor = new ENT.VendorMasterData();
        List<ENT.VendorMasterData> lstENTVendor = new List<ENT.VendorMasterData>();
        List<ENT.VendorMasterView> lstENTView = new List<ENT.VendorMasterView>();

        ENT.VendorTaxsNo objENTTax = new ENT.VendorTaxsNo();

        public frmVendorMaster()
        {
            InitializeComponent();
        }

        private void clearData()
        {
            try
            {
                strMode = "ADD";
                btnUpdate.Enabled = true;
                txtVendorID.Text = string.Empty;
                txtVendorName.Text = string.Empty;
                txtMobileNo.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtContactPerson.Text = string.Empty;
                txtAddress.Text = string.Empty;
                chkSendEmail.Checked = false;
                chkSendSMS.Checked = false;
                txtPincode.Text = string.Empty;
                txtFax.Text = string.Empty;
                txtMinOrderAmt.Text = string.Empty;
                cmbShippingCharge.SelectedIndex = 0;
                cmbPaymentTerms.SelectedIndex = 0;
                chkStatus.Checked = false;
                txtRemark.Text = string.Empty;
                txtSearch.Text = string.Empty;
                grpEntry.Visible = false;
                grpView.Visible = true;

                this.getVendorMasterList(string.Empty);
                tabMain.SelectedIndex = 0;
                tabMain.TabPages[1].Hide();
                tabMain.TabPages[2].Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearTaxData()
        {
            try
            {
                txtTaxId.Text = string.Empty;
                txtTaxName.Text = string.Empty;
                txtTaxNo.Text = string.Empty;
                txtTaxPer.Text = string.Empty;
                //dgvTax.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveData()
        {
            try
            {
                Control[] ctrl = { txtVendorName, txtMobileNo};
                if (GlobalVariable.CheckValidate(ctrl))
                {
                    if (strMode == "ADD")
                    {
                        if (objDALVendor.getDuplicateVendorByName(txtVendorName.Text.Trim()) > 0)
                        {
                            MessageBox.Show("Duplicate vendor found. Please try another code.", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtContactPerson.Focus();
                            return;
                        }
                        objENTVendor.VendorID = Guid.NewGuid();
                        txtTaxVenderID.Text = objENTVendor.VendorID.ToString();
                        txtIngVendorID.Text = objENTVendor.VendorID.ToString();
                    }
                    else if (strMode == "UPDATE")
                    {
                        if (objDALVendor.getDuplicateVendorByName(txtVendorName.Text.Trim(), txtVendorID.Text.Trim()) > 0)
                        {
                            MessageBox.Show("Duplicate vendor code found. Please try another name.", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtContactPerson.Focus();
                            return;
                        }
                        objENTVendor.VendorID = new Guid(txtVendorID.Text.Trim());
                    }
                    objENTVendor.VendorName = txtVendorName.Text.Trim();
                    objENTVendor.MobileNo = txtMobileNo.Text.Trim();
                    objENTVendor.EmailID = txtEmail.Text.Trim();
                    objENTVendor.CompanyName = txtContactPerson.Text.Trim();
                    objENTVendor.VendorAddress = txtAddress.Text.Trim();
                    objENTVendor.IsSendPOInSMS=chkSendSMS.Checked;
                    objENTVendor.IsSendPOInMail=chkSendEmail.Checked;
                    objENTVendor.PinCode = txtPincode.Text;
                    objENTVendor.Fax = txtFax.Text;
                    objENTVendor.MinOrderAmt = txtMinOrderAmt.Text.Trim() == string.Empty ? 0 : Convert.ToDecimal(txtMinOrderAmt.Text);
                    objENTVendor.ShippingCharges = cmbShippingCharge.SelectedIndex;
                    objENTVendor.PaymentTermsID = cmbPaymentTerms.SelectedIndex;
                    objENTVendor.Status = chkStatus.Checked;
                    objENTVendor.Remarks = txtRemark.Text;
                    objENTVendor.RUserID = new Guid(GlobalVariable.BranchID);
                    objENTVendor.RUserType = GlobalVariable.RUserType;
                    objENTVendor.Mode = strMode;
                    if (objDALVendor.InsertUpdateDeleteVendorMaster(objENTVendor))
                    {
                        MessageBox.Show("Data Saved Successfully.", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.clearData();
                        this.clearTaxData();
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved.", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getVendorTaxList(string VendorID)
        {
            try
            {
                dgvTax.Rows.Clear();

                List<ENT.VendorTaxsNo> lstENTTax = new List<ENT.VendorTaxsNo>();
               
                objENTTax.VendorID = new Guid(VendorID);
                objENTTax.Mode = "GetRecordByVendorID";
                lstENTTax = new DAL.VendorTaxsNo().GetVendorTaxsNo(objENTTax);
                
                for (int i = 0; i < lstENTTax.Count; i++)
                {
                    dgvTax.Rows.Add();
                    dgvTax.Rows[i].Cells["colVendorTaxsNoID"].Value = lstENTTax[i].VendorTaxsNoID;
                    dgvTax.Rows[i].Cells["colVendorID"].Value = lstENTTax[i].VendorID;
                    dgvTax.Rows[i].Cells["colTaxName"].Value = lstENTTax[i].TaxName;
                    dgvTax.Rows[i].Cells["colTaxNo"].Value = lstENTTax[i].TaxNo;
                    dgvTax.Rows[i].Cells["colPercentage"].Value = lstENTTax[i].TaxPercentage;
                    DataGridViewButtonCell dgvbc = new DataGridViewButtonCell();
                    dgvbc.FlatStyle = FlatStyle.Flat;
                    dgvbc.Value = "Delete";
                    dgvbc.Style.BackColor = Color.Brown;
                    dgvbc.Style.ForeColor = Color.White;
                    dgvTax.Rows[i].Cells["colAction"] = dgvbc;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getVendorMasterList(string VendorName)
        {
            try
            {
                objENTVendor.Mode = "GetAllRecordView";
                lstENTView = objDALVendor.getVendorMasterView(objENTVendor);

                if (GlobalVariable.IsNumeric(VendorName))
                    lstENTView = lstENTView.Where(emp => emp.MobileNo.ToLower().StartsWith(VendorName.ToLower())).ToList();
                else
                    lstENTView = lstENTView.Where(emp => emp.VendorName.ToLower().StartsWith(VendorName.ToLower())).ToList();

                dgvEmployee.DataSource = lstENTView;

                dgvEmployee.Columns[0].Visible = false;
                dgvEmployee.Columns[1].Width = 200;
                dgvEmployee.Columns[2].Width = 230;
                dgvEmployee.Columns[3].Width = 150;
                dgvEmployee.Columns[4].Width = 150;
                dgvEmployee.Columns[5].Width = 150;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void getSelectIngredientList(string IngredientName)
        {
            try
            {
                dgvSelect.Rows.Clear();
                ENT.IngredientsMasterDetail objENTIMD = new ENT.IngredientsMasterDetail();
                List<ENT.IngredientsMasterDetail> lstENTIMD = new List<ENT.IngredientsMasterDetail>();
                objENTIMD.Mode = "GetAllRecord";
                lstENTIMD = new DAL.IngredientsMasterDetail().getIngredientsMasterDetail(objENTIMD);

                if (GlobalVariable.IsNumeric(IngredientName))
                    lstENTIMD = lstENTIMD.Where(emp => emp.IngredientName.ToLower().StartsWith(IngredientName.ToLower())).ToList();
                else
                    lstENTIMD = lstENTIMD.Where(emp => emp.IngredientName.ToLower().StartsWith(IngredientName.ToLower())).ToList();

                for (int i = 0; i < lstENTIMD.Count; i++)
                {
                    dgvSelect.Rows.Add();
                    dgvSelect.Rows[i].Cells["addIngredientsID"].Value = lstENTIMD[i].IngredientsID;
                    dgvSelect.Rows[i].Cells["addIngredient"].Value = lstENTIMD[i].IngredientName;
                    dgvSelect.Rows[i].Cells["addIngredientsIDInt"].Value = lstENTIMD[i].IngredientsMasterDetail_Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<ENT.IngredientUnitTypeDetail> FillComboInDgvPrinter(int id)
        {
            List<ENT.IngredientUnitTypeDetail> lstENTDiv = new List<ENT.IngredientUnitTypeDetail>();
            try
            {
                DAL.IngredientUnitTypeDetail objDALDiv = new DAL.IngredientUnitTypeDetail();
                ENT.IngredientUnitTypeDetail objENTDiv = new ENT.IngredientUnitTypeDetail();

                objENTDiv.Mode = "GetRecordByIDInt";
                objENTDiv.IngredientsMasterDetail_Id = id;
                lstENTDiv = objDALDiv.getIngredientUnitTypeDetail(objENTDiv);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if(lstENTDiv.Count==0)
                lstENTDiv.Add(new ENT.IngredientUnitTypeDetail { UnitTypeID = new Guid("00000000-0000-0000-0000-000000000000"), UnitType = "-Select-" });

            return lstENTDiv.OrderBy(ent => ent.UnitTypeID).ToList(); ;
        }

        private void GetIngredientWiseUnit()
        {
            try
            {
                ENT.IngredientUnitTypeDetail objENTUnit = new ENT.IngredientUnitTypeDetail();
                List<ENT.IngredientUnitTypeDetail> lstENTUnit = new List<ENT.IngredientUnitTypeDetail>();

                //objENTItem.Mode = "GetAll";
                //lstENTItem = objDALItem.GetItemChefMapping(objENTItem);

                //for (int i = 0; i < lstENTItem.Count; i++)
                //{
                //    for (int j = 0; j < dgvItemToChef.Rows.Count; j++)
                //    {
                //        if (dgvItemToChef.Rows[j].Cells["ProductID"].Value.ToString() == lstENTItem[i].ProductID.ToString() &&
                //            dgvItemToChef.Rows[j].Cells["CategoryID"].Value.ToString() == lstENTItem[i].CategoryID.ToString())
                //        {
                //            (dgvItemToChef.Rows[j].Cells["EmpID"] as DataGridViewComboBoxCell).Value = lstENTItem[i].EmployeeID;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            this.clearData();
        }

        private void frmEmployee_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();

                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            clearData();
            clearTaxData();
            getSelectIngredientList(string.Empty);
            grpEntry.Visible = true;
            grpView.Visible = false;
            tabMain.TabPages[1].Show();
            tabMain.TabPages[2].Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEmployee.Rows.Count <= 0)
                {
                    MessageBox.Show("No Record For Update", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int row = dgvEmployee.CurrentRow.Index;
                txtVendorID.Text = dgvEmployee.Rows[row].Cells[0].Value.ToString();
                if (txtVendorID.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Select Valid Employee.", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvEmployee.Focus();
                    return;
                }
                objENTVendor.Mode = "GetRecordByVendorID";
                objENTVendor.VendorID = new Guid(txtVendorID.Text);
                lstENTVendor = objDALVendor.getVendorMaster(objENTVendor);
                if (lstENTVendor.Count > 0)
                {
                    txtVendorID.Text = lstENTVendor[0].VendorID.ToString();
                    txtTaxVenderID.Text = lstENTVendor[0].VendorID.ToString();
                    txtIngVendorID.Text = lstENTVendor[0].VendorID.ToString();
                    txtVendorName.Text = Convert.ToString(lstENTVendor[0].VendorName);
                    txtContactPerson.Text = Convert.ToString(lstENTVendor[0].CompanyName);
                    txtMobileNo.Text = Convert.ToString(lstENTVendor[0].MobileNo);
                    chkSendSMS.Checked = lstENTVendor[0].IsSendPOInSMS;
                    txtEmail.Text = Convert.ToString(lstENTVendor[0].EmailID);
                    chkSendEmail.Checked = lstENTVendor[0].IsSendPOInMail;
                    txtAddress.Text = Convert.ToString(lstENTVendor[0].VendorAddress);
                    txtPincode.Text = Convert.ToString(lstENTVendor[0].PinCode);
                    txtFax.Text = Convert.ToString(lstENTVendor[0].Fax);
                    txtMinOrderAmt.Text = Convert.ToString(lstENTVendor[0].MinOrderAmt);
                    cmbShippingCharge.SelectedIndex = lstENTVendor[0].ShippingCharges;
                    cmbPaymentTerms.SelectedIndex = lstENTVendor[0].PaymentTermsID;
                    chkStatus.Checked = lstENTVendor[0].Status;
                    txtRemark.Text = Convert.ToString(lstENTVendor[0].Remarks);
                    btnUpdate.Enabled = false;
                    strMode = "UPDATE";
                    getVendorTaxList(txtVendorID.Text.Trim());
                    getSelectIngredientList(string.Empty);
                    grpEntry.Visible = true;
                    grpView.Visible = false;
                }
                else
                {
                    MessageBox.Show("Record Not Found.", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {
                if (dgvEmployee.Rows.Count <= 0)
                {
                    MessageBox.Show("No Record For Delete", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult dlgResult = MessageBox.Show("You want to delete seleted customer ?", "Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    int row = dgvEmployee.CurrentRow.Index;
                    txtVendorID.Text = dgvEmployee.Rows[row].Cells["EmpID"].Value.ToString();
                    if (txtVendorID.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Select Valid Vendor.", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvEmployee.Focus();
                        return;
                    }

                    objENTVendor.Mode = "DELETE";
                    objENTVendor.VendorID = new Guid(txtVendorID.Text.Trim());
                    result = objDALVendor.InsertUpdateDeleteVendorMaster(objENTVendor);
                    if (result)
                    {
                        MessageBox.Show("Data Deleted Successfully.", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.clearData();
                    }
                    else
                    {
                        MessageBox.Show("Record Not Delete.", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.clearData();
                    }
                }
                else
                {
                    this.clearData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.clearData();
            this.clearTaxData();
            dgvTax.Rows.Clear();
            dgvSelect.Rows.Clear();
            dgvSelected.Rows.Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.getVendorMasterList(txtSearch.Text);
        }

        private void txtIngSearch_TextChanged(object sender, EventArgs e)
        {
            this.getSelectIngredientList(txtIngSearch.Text);
        }

        private void btnAddTax_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTaxVenderID.Text.Trim()))
                {
                    MessageBox.Show("Vendor Not Found For Add Tax.", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtTaxName.Text.Trim()))
                {
                    MessageBox.Show("Tax Name Should Not Be Empty.", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTaxName.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtTaxPer.Text.Trim()))
                {
                    MessageBox.Show("Tax Percentage Should Not Be Empty.", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTaxPer.Focus();
                    return;
                }

                objENTTax.VendorTaxsNoID = Guid.NewGuid();
                objENTTax.VendorID = new Guid(txtVendorID.Text.Trim());
                objENTTax.TaxName = txtTaxName.Text.Trim();
                objENTTax.TaxNo = txtTaxNo.Text.Trim();
                objENTTax.TaxPercentage = Convert.ToDecimal(txtTaxPer.Text.Trim());
                objENTTax.RUserID = new Guid(GlobalVariable.BranchID);
                objENTTax.RUserType = GlobalVariable.RUserType;
                objENTTax.Mode = "ADD";
                if (new DAL.VendorTaxsNo().InsertUpdateDeleteVendorTaxsNo(objENTTax))
                {
                    getVendorTaxList(txtTaxVenderID.Text.Trim());
                    clearTaxData();
                    MessageBox.Show("Tax Saved Successfully.", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTax_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0)
                {
                    return;
                }
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (dgvTax.Rows.Count > 0 && dgvTax.Columns[e.ColumnIndex].Name == "colAction")
                {
                    if (MessageBox.Show("Are you sure! You want to delete this tax.", "Vendor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        objENTTax.VendorTaxsNoID = new Guid(dgvTax.Rows[e.RowIndex].Cells["colVendorTaxsNoID"].Value.ToString());
                        objENTTax.VendorID = new Guid(dgvTax.Rows[e.RowIndex].Cells["colVendorID"].Value.ToString());
                        objENTTax.Mode = "DELETE";
                        if (new DAL.VendorTaxsNo().InsertUpdateDeleteVendorTaxsNo(objENTTax))
                        {
                            getVendorTaxList(txtTaxVenderID.Text.Trim());
                            MessageBox.Show("Tax Deleted Successfully.", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Problem In Delete of Tax.", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvSelect.Rows.Count; i++)
                {
                    dgvSelect.Rows[i].Cells["addAction"].Value = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvSelect.Rows.Count; i++)
                {
                    dgvSelect.Rows[i].Cells["addAction"].Value = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvSelect.RowCount; i++)
                {
                    bool isExist = false;
                    if (Convert.ToBoolean(dgvSelect.Rows[i].Cells["addAction"].Value) == true)
                    {
                        for (int j = 0; j < dgvSelected.RowCount; j++)
                        {
                            if (Convert.ToString(dgvSelected.Rows[j].Cells["IngIngredientsID"].Value) == Convert.ToString(dgvSelect.Rows[i].Cells["addIngredientsID"].Value))
                            {
                                isExist = true;
                                break;
                            }
                        }

                        if (!isExist)
                        {
                            dgvSelected.Rows.Add();
                            int row = dgvSelected.RowCount - 1;
                            dgvSelected.Rows[row].Cells["IngIngredientsID"].Value = dgvSelect.Rows[i].Cells["addIngredientsID"].Value;
                            dgvSelected.Rows[row].Cells["IngName"].Value = dgvSelect.Rows[i].Cells["addIngredient"].Value;
                            dgvSelected.Rows[row].Cells["IngQty"].Value = 0;
                            List<ENT.IngredientUnitTypeDetail> lstENTDiv = FillComboInDgvPrinter(Convert.ToInt32(dgvSelect.Rows[i].Cells["addIngredientsIDInt"].Value));
                            (dgvSelected.Rows[row].Cells["IngUnit"] as DataGridViewComboBoxCell).DataSource = lstENTDiv;
                            (dgvSelected.Rows[row].Cells["IngUnit"] as DataGridViewComboBoxCell).ValueMember = "UnitTypeID";
                            (dgvSelected.Rows[row].Cells["IngUnit"] as DataGridViewComboBoxCell).DisplayMember = "UnitType";
                            (dgvSelected.Rows[row].Cells["IngUnit"] as DataGridViewComboBoxCell).Value = lstENTDiv[0].UnitTypeID;

                            DataGridViewButtonCell bc = new DataGridViewButtonCell();
                            bc.FlatStyle = FlatStyle.Flat;
                            bc.Value = "Delete";
                            bc.Style.BackColor = Color.Red;
                            dgvSelected.Rows[row].Cells["IngAction"] = bc;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSelected_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0)
                {
                    return;
                }
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (dgvSelected.Rows.Count > 0 && dgvSelected.Columns[e.ColumnIndex].Name == "IngAction")
                {
                    string strOrderId = dgvSelected.Rows[e.RowIndex].Cells["IngIngredientsID"].Value.ToString();
                    if (strOrderId != string.Empty)
                    {
                        dgvSelected.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSelected_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dgvSelected.CurrentCell.ColumnIndex == 1 && e.Control is ComboBox)
                {
                    ComboBox comboBox = e.Control as ComboBox;
                    comboBox.SelectedIndexChanged -= LastColumnComboSelectionChanged;
                    comboBox.SelectedIndexChanged += LastColumnComboSelectionChanged;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LastColumnComboSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var currentcell = dgvSelected.CurrentCellAddress;
                var sendingCB = sender as DataGridViewComboBoxEditingControl;
                if (sendingCB.SelectedValue != null)
                {
                    MessageBox.Show(sendingCB.SelectedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
