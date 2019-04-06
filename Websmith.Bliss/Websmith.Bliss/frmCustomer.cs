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
    public partial class frmCustomer : Form
    {
        bool fldVisible = false;
        string strMode = "";
        DAL.CustomerMasterData objDALCustomers = new DAL.CustomerMasterData();
        ENT.CustomerMasterData objENTCustomers = new ENT.CustomerMasterData();
        List<ENT.CustomerMasterData> lstENTCustomers = new List<ENT.CustomerMasterData>();
        
        public frmCustomer()
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
                lstENTCustomers = lstENTCustomers.Where(cust => cust.Name.ToLower().StartsWith(strSearch.ToLower())).ToList();
                
                for (int i = 0; i < lstENTCustomers.Count; i++)
                {
                    dgvCustomers.Rows.Add();
                    dgvCustomers.Rows[i].Cells["CustID"].Value = lstENTCustomers[i].CustomerID;
                    dgvCustomers.Rows[i].Cells["CustName"].Value = lstENTCustomers[i].Name;
                    dgvCustomers.Rows[i].Cells["CustPhoneNo"].Value = lstENTCustomers[i].MobileNo;
                    dgvCustomers.Rows[i].Cells["CustEmail"].Value = lstENTCustomers[i].EmailID;
                    dgvCustomers.Rows[i].Cells["CustAddress"].Value = lstENTCustomers[i].Address;
                    if (lstENTCustomers[i].Birthdate != null && lstENTCustomers[i].Birthdate != string.Empty)
                    {
                        DateTime date = DateTime.ParseExact(lstENTCustomers[i].Birthdate, "dd/MM/yyyy hh:mm:ss tt", null);
                        dgvCustomers.Rows[i].Cells["CustBirthdate"].Value = date.ToString("dd/MM/yyyy");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void fillComboTable()
        {
            try
            {
                //ENT.TableMasterDetail objENTTable = new ENT.TableMasterDetail();
                //List<ENT.TableMasterDetail> lstENTTable = new List<ENT.TableMasterDetail>();
                //DAL.TableMasterDetail objDALTable = new DAL.TableMasterDetail();
                //BindingSource bs = new BindingSource();
                //objENTTable.Mode = "GetAllRecord";
                //lstENTTable = objDALTable.getTableMasterDetail(objENTTable);
                //bs.DataSource = lstENTTable;
                //cmbTable.DataSource = bs;
                //cmbTable.DisplayMember = "TableName";
                //cmbTable.ValueMember = "TableID";
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
                    tabMainEditor.RowStyles[6].SizeType = SizeType.Absolute;
                    tabMainEditor.RowStyles[6].Height = tabMainEditor.RowStyles[5].Height * 2;

                    tabMainEditor.RowStyles[7].SizeType = SizeType.Absolute;
                    tabMainEditor.RowStyles[7].Height = tabMainEditor.RowStyles[5].Height;
                    
                    //lnkViewMore.Text = "Less";
                    fldVisible = false;
                }
                else
                {
                    tabMainEditor.RowStyles[6].SizeType = SizeType.Absolute;
                    tabMainEditor.RowStyles[6].Height = 0;

                    tabMainEditor.RowStyles[7].SizeType = SizeType.Absolute;
                    tabMainEditor.RowStyles[7].Height = 0;
                    
                    //lnkViewMore.Text = "View More";
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
                txtCustId.Text = string.Empty;
                txtName.Text = string.Empty;
                txtMobileNo.Text = string.Empty;
                txtEmailId.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtBirthdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtSearch.Text = string.Empty;
                fldVisible = true;
                this.fieldVisible();
                this.fillComboTable();
                this.getSearchCustomer(string.Empty);
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
                Control[] ctrl = { txtName, txtMobileNo, txtEmailId, txtAddress, txtBirthdate};
                if (GlobalVariable.CheckValidate(ctrl))
                {
                    if (strMode == "ADD")
                        objENTCustomers.CustomerID = Guid.NewGuid();
                    else if (strMode == "UPDATE")
                        objENTCustomers.CustomerID = new Guid(txtCustId.Text.Trim());

                    objENTCustomers.Name = txtName.Text.Trim();
                    objENTCustomers.MobileNo = txtMobileNo.Text.Trim();
                    objENTCustomers.EmailID = txtEmailId.Text.Trim();
                    objENTCustomers.Address = txtAddress.Text.Trim();
                    objENTCustomers.Birthdate = GlobalVariable.ChangeDate(txtBirthdate.Text.ToString());
                    objENTCustomers.RUserID = new Guid(GlobalVariable.BranchID);
                    objENTCustomers.RUserType = "0";
                    objENTCustomers.Mode = strMode;
                    result = objDALCustomers.InsertUpdateDeleteCustomer(objENTCustomers);
                    if (result)
                    {
                        MessageBox.Show("Data Saved Successfully.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.clearData();
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

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                this.clearData();
                GlobalVariable.Theme(this);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int row = dgvCustomers.CurrentRow.Index;
                txtCustId.Text = dgvCustomers.Rows[row].Cells["CustID"].Value.ToString();
                if (txtCustId.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Select Valid Customer.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvCustomers.Focus();
                    return;
                }
                txtName.Text = dgvCustomers.Rows[row].Cells["CustName"].Value.ToString();
                txtMobileNo.Text = dgvCustomers.Rows[row].Cells["CustPhoneNo"].Value.ToString();
                txtEmailId.Text = dgvCustomers.Rows[row].Cells["CustEmail"].Value.ToString();
                txtBirthdate.Text = Convert.ToDateTime(dgvCustomers.Rows[row].Cells["CustBirthdate"].Value).ToString("dd/MM/yyyy");
                txtAddress.Text = dgvCustomers.Rows[row].Cells["CustAddress"].Value.ToString();
                strMode = "UPDATE";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.saveData();
            txtName.Focus();
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (this.saveData())
            {
                this.Close();
            }            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {
                DialogResult dlgResult = MessageBox.Show("You want to delete seleted customer ?", "Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    int row = dgvCustomers.CurrentRow.Index;
                    txtCustId.Text = dgvCustomers.Rows[row].Cells["CustID"].Value.ToString();
                    if (txtCustId.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Select Valid Customer.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvCustomers.Focus();
                        return;
                    }

                    objENTCustomers.Mode = "DELETE";
                    objENTCustomers.CustomerID = new Guid(txtCustId.Text.Trim());
                    result = objDALCustomers.InsertUpdateDeleteCustomer(objENTCustomers);
                    if (result)
                    {
                        MessageBox.Show("Data Deleted Successfully.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.clearData();
                    }
                    else
                    {
                        MessageBox.Show("Data Not Delete.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { SendKeys.Send("{Tab}"); }
        }
    }
}
