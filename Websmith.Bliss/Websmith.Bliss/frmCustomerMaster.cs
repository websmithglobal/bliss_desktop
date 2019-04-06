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
    public partial class frmCustomerMaster : Form
    {
        bool fldVisible = false;
        string strMode = "";
        DAL.CustomerMasterData objDALCustomers = new DAL.CustomerMasterData();
        ENT.CustomerMasterData objENTCustomers = new ENT.CustomerMasterData();
        List<ENT.CustomerMasterData> lstENTCustomers = new List<ENT.CustomerMasterData>();

        public frmCustomerMaster()
        {
            InitializeComponent();
        }

        private void getSearchCustomer(string strSearch)
        {
            try
            {
                dgvCustomers.Rows.Clear();
                objENTCustomers.Mode = "GetAllRecord";
                lstENTCustomers = objDALCustomers.getCustomerData(objENTCustomers);

                if (!GlobalVariable.IsNumeric(strSearch))
                    lstENTCustomers = lstENTCustomers.Where(cust => cust.Name.ToLower().StartsWith(strSearch.ToLower())).ToList();
                else
                    lstENTCustomers = lstENTCustomers.Where(cust => cust.MobileNo.ToLower().StartsWith(strSearch.ToLower())).ToList();

                for (int i = 0; i < lstENTCustomers.Count; i++)
                {
                    dgvCustomers.Rows.Add();
                    dgvCustomers.Rows[i].Cells["CustID"].Value = lstENTCustomers[i].CustomerID;
                    dgvCustomers.Rows[i].Cells["CustName"].Value = lstENTCustomers[i].Name;
                    dgvCustomers.Rows[i].Cells["CustPhoneNo"].Value = lstENTCustomers[i].MobileNo;
                    dgvCustomers.Rows[i].Cells["CustEmail"].Value = lstENTCustomers[i].EmailID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getCustomerEdit()
        {
            try
            {
                if (dgvCustomers.Rows.Count > 0)
                {
                    int row = dgvCustomers.CurrentRow.Index;
                    txtCustId.Text = Convert.ToString(dgvCustomers.Rows[row].Cells["CustID"].Value);
                    DAL.CustomerMasterData objDALCustomers = new DAL.CustomerMasterData();
                    ENT.CustomerMasterData objENTCustomers = new ENT.CustomerMasterData();
                    List<ENT.CustomerMasterData> lstENTCustomers = new List<ENT.CustomerMasterData>();

                    if (txtCustId.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Select valid customer from list.");
                        dgvCustomers.Focus();
                        return;
                    }

                    objENTCustomers.Mode = "GetRecordByID";
                    objENTCustomers.CustomerID = new Guid(txtCustId.Text);
                    lstENTCustomers = objDALCustomers.getCustomerData(objENTCustomers);
                    if (lstENTCustomers.Count > 0)
                    {
                        txtName.Text = Convert.ToString(lstENTCustomers[0].Name);
                        txtPhone.Text = Convert.ToString(lstENTCustomers[0].MobileNo);
                        txtEmailId.Text = Convert.ToString(lstENTCustomers[0].EmailID);
                        txtAddress.Text = Convert.ToString(lstENTCustomers[0].Address);
                        txtCardNo.Text = Convert.ToString(lstENTCustomers[0].CardNo);
                        txtShippingAddress.Text = Convert.ToString(lstENTCustomers[0].ShippingAddress);
                        strMode = "UPDATE";
                        tabMain.SelectedIndex = 1;
                    }
                }
                else
                {
                    MessageBox.Show("Record not found.");
                    txtSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fillComboTable()
        {
            try
            {
                ENT.TableMasterDetail objENTTable = new ENT.TableMasterDetail();
                List<ENT.TableMasterDetail> lstENTTable = new List<ENT.TableMasterDetail>();
                DAL.TableMasterDetail objDALTable = new DAL.TableMasterDetail();
                BindingSource bs = new BindingSource();
                //objENTTable.Mode = "GetRecordByEmpIDForCombo";
                objENTTable.Mode = "GetRecordByClassIDForCombo";
                objENTTable.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                objENTTable.ClassID = new Guid(GlobalVariable.ClassID);
                lstENTTable = objDALTable.getTableMasterDetail(objENTTable);
                bs.DataSource = lstENTTable;
                cmbTable.DataSource = bs;
                cmbTable.DisplayMember = "TableName";
                cmbTable.ValueMember = "TableID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fieldVisible()
        {
            try
            {
                if (fldVisible)
                {
                    tabMoreInfo.Visible = fldVisible;
                    lnkViewMore.Text = "Less";
                    fldVisible = false;
                }
                else
                {
                    tabMoreInfo.Visible = fldVisible;
                    lnkViewMore.Text = "View Info";
                    fldVisible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearData()
        {
            try
            {
                strMode = "ADD";
                rdoDineIn.Checked = true;
                txtCustId.Text = string.Empty;
                txtName.Text = string.Empty;
                txtPhone.Text = string.Empty;
                txtEmailId.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtSearch.Text = string.Empty;
                this.fieldVisible();
                fldVisible = true;
                this.fillComboTable();
                this.getSearchCustomer(string.Empty);
                tabMain.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool saveData()
        {
            bool result = false;
            try
            {
                Control[] ctrl = { txtName, txtPhone };
                if (GlobalVariable.CheckValidate(ctrl))
                {
                    if (strMode == "ADD")
                    {
                        objENTCustomers.CustomerID = Guid.NewGuid();
                        txtCustId.Text = objENTCustomers.CustomerID.ToString();
                    }
                    else if (strMode == "UPDATE")
                    {
                        objENTCustomers.CustomerID = new Guid(txtCustId.Text.Trim());
                    }
                    
                    objENTCustomers.Name = txtName.Text.Trim();
                    objENTCustomers.MobileNo = txtPhone.Text.Trim();
                    objENTCustomers.EmailID = txtEmailId.Text.Trim();
                    objENTCustomers.Address = txtAddress.Text.Trim();
                    objENTCustomers.CardNo = txtCardNo.Text.Trim();
                    objENTCustomers.ShippingAddress = txtShippingAddress.Text.Trim();
                    objENTCustomers.RUserID = new Guid(GlobalVariable.BranchID);
                    objENTCustomers.RUserType = GlobalVariable.RUserType;
                    
                    objENTCustomers.Mode = strMode;
                    result = objDALCustomers.InsertUpdateDeleteCustomer(objENTCustomers);
                    if (result)
                    {
                        //MessageBox.Show("Data Saved Successfully.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        private void frmCustomerMaster_Load(object sender, EventArgs e)
        {
            try
            {
                this.clearData();
                dgvCustomers.Focus();
                //GlobalVariable.Theme(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveData();
                this.clearData();
                txtName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.saveData())
                {
                    ENT.CheckInfo objENTCheck = new ENT.CheckInfo();
                    GlobalVariable.objCheckInfo = null;
                    
                    #region Select Customer
                    if (txtName.Text.Trim() != string.Empty)
                    {
                        objENTCheck.CustomerID = new Guid(txtCustId.Text.Trim());
                        objENTCheck.CustomerName = txtName.Text.Trim();
                    }
                    else
                    {
                        MessageBox.Show("Enter customer name.");
                        txtName.Focus();
                        return;
                    }
                    #endregion

                    if (rdoDineIn.Checked == true)
                    {
                        objENTCheck.DelieveryType = Convert.ToInt32(GlobalVariable.DeliveryType.DineIn);
                        objENTCheck.DelieveryTypeName = rdoDineIn.Text;
                        if (cmbTable.SelectedIndex < 0)
                        {
                            MessageBox.Show("Select available table for customer.");
                            cmbTable.Focus();
                            return;
                        }
                        else
                        {
                            objENTCheck.TableID = new Guid(cmbTable.SelectedValue.ToString());
                            objENTCheck.TableName = cmbTable.Text.ToString();
                            objENTCheck.TableStatusID = Convert.ToInt32(GlobalVariable.TableStatus.Occupied);
                        }
                    }
                    else if (rdoDelivery.Checked == true)
                    {
                        objENTCheck.DelieveryType = Convert.ToInt32(GlobalVariable.DeliveryType.Delivery);
                        objENTCheck.DelieveryTypeName = rdoDelivery.Text;
                        objENTCheck.TableStatusID = Convert.ToInt32(GlobalVariable.TableStatus.Vacant);
                    }
                    else if (rdoTakeOut.Checked == true)
                    {
                        objENTCheck.DelieveryType = Convert.ToInt32(GlobalVariable.DeliveryType.TakeOut);
                        objENTCheck.DelieveryTypeName = rdoTakeOut.Text;
                        objENTCheck.TableStatusID = Convert.ToInt32(GlobalVariable.TableStatus.Vacant);
                    }
                    
                    GlobalVariable.IsOk = true;
                    GlobalVariable.objCheckInfo = objENTCheck;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCloseNew_Click(object sender, EventArgs e)
        {
            GlobalVariable.IsOk = false;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GlobalVariable.IsOk = false;
            this.Close();
        }

        private void frmCustomerMaster_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (tabMain.SelectedIndex == 0)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (dgvCustomers.Rows.Count > 0)
                        {
                            this.getCustomerEdit();
                        }
                        else
                        {
                            MessageBox.Show("Record not found.");
                            txtSearch.Focus();
                        }
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        SendKeys.Send("{Tab}");
                    }
                }
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.getSearchCustomer(txtSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkViewMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.fieldVisible();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCustomers_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvCustomers.Rows.Count > 0)
                {
                    this.getCustomerEdit();
                }
                else
                {
                    MessageBox.Show("Record not found.");
                    txtSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCardNo_KeyPress(object sender, KeyPressEventArgs e)
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
}
