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
    public partial class frmCheckInfo : Form
    {
        DAL.CustomerMasterData objDALCustomers = new DAL.CustomerMasterData();
        ENT.CustomerMasterData objENTCustomers = new ENT.CustomerMasterData();
        List<ENT.CustomerMasterData> lstENTCustomers = new List<ENT.CustomerMasterData>();

        ENT.CheckInfo objENTCheck = new ENT.CheckInfo();

        public frmCheckInfo()
        {
            InitializeComponent();
        }

        private void getSearchCustomer()
        {
            try
            {
                dgvCustomers.Rows.Clear();
                objENTCustomers.Mode = "GetAllRecord";
                lstENTCustomers = objDALCustomers.getCustomerData(objENTCustomers);
                lstENTCustomers = lstENTCustomers.Where(cust => cust.Name.ToLower().StartsWith(txtSearchName.Text.ToLower()) && cust.MobileNo.ToLower().StartsWith(txtSearchMobile.Text.ToLower())).ToList();

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
                MessageBox.Show(ex.Message.ToString(), "Check Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fillComboTable()
        {
            try
            {
                DAL.TableMasterDetail objDALTable = new DAL.TableMasterDetail();
                ENT.TableMasterDetail objENTTable = new ENT.TableMasterDetail();
                List<ENT.TableMasterDetail> lstENTTable = new List<ENT.TableMasterDetail>();

                BindingSource bs = new BindingSource();
                objENTTable.Mode = "GetRecordByEmployeeID";
                objENTTable.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                lstENTTable = objDALTable.getTableMasterDetail(objENTTable);
                bs.DataSource = lstENTTable.Select(c => new { c.TableID, c.TableName }).ToList();
                cmbTable.DataSource = bs;
                cmbTable.DisplayMember = "TableName";
                cmbTable.ValueMember = "TableID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearData()
        {
            try
            {
                this.HideShowButton(true, false, true, false);
                this.HideShowType(false, false, false);
                this.getSearchCustomer();
                this.fillComboTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HideShowType(bool _grpDineIn, bool _grpDelivery, bool _grpQueue)
        {
            try
            {
                grpDineIn.Visible = _grpDineIn;
                grpDelivery.Visible = _grpDelivery;
                grpQueue.Visible = _grpQueue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HideShowButton(bool _AddNew, bool _SearchCustomer, bool _Guest, bool _ProceedOrder)
        {
            try
            {
                btnAddNew.Visible = _AddNew;
                btnSearch.Visible = _SearchCustomer;
                btnGuest.Visible = _Guest;
                btnProceedOrder.Visible = _ProceedOrder;
                grpSearch.Visible = _Guest;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCustomerMaster_Load(object sender, EventArgs e)
        {
            try
            {
                this.ClearData();
                dgvCustomers.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalVariable.IsOk = false;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProceedOrder_Click(object sender, EventArgs e)
        {
            try
            {
                #region Save CustomerMasterData
                ENT.CustomerMasterData objENTCustomer = new ENT.CustomerMasterData();
                DAL.CustomerMasterData objDALCustomer = new DAL.CustomerMasterData();
               
                if (objDALCustomer.getDuplicateCustomerByID(txtCustomerId.Text.ToString()) > 0)
                { objENTCustomer.Mode = "UPDATE"; }
                else
                { objENTCustomer.Mode = "ADD"; }
                objENTCustomer.CustomerID = new Guid(txtCustomerId.Text);
                objENTCustomer.Name = txtCustomerName.Text.Trim();
                objENTCustomer.MobileNo = Convert.ToString(txtPhone.Text);
                objENTCustomer.EmailID = Convert.ToString(txtEmail.Text);
                objENTCustomer.Birthdate = GlobalVariable.IsDate(txtBirthdate.Text) == false ? null : GlobalVariable.ChangeDate(txtBirthdate.Text);
                objENTCustomer.Address = Convert.ToString(txtBillAdd.Text);
                objENTCustomer.RUserID = new Guid(GlobalVariable.BranchID);
                objENTCustomer.RUserType = "0";

                if (!objDALCustomer.InsertUpdateDeleteCustomer(objENTCustomer))
                {
                    MessageBox.Show("Customer not added in database.");
                    return;
                }
               
                #endregion

                GlobalVariable.objCheckInfo = null;
                if (rdoDineIn.Checked == true)
                {
                    #region Dine In
                    objENTCheck.DelieveryType = 1;
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
                    }
                    #endregion
                }
                else if (rdoTakeOut.Checked == true)
                {
                    objENTCheck.DelieveryType = 2;
                    objENTCheck.DelieveryTypeName = rdoTakeOut.Text;
                }
                else if (rdoDelivery.Checked == true)
                {
                    #region Delivery
                    objENTCheck.DelieveryType = 3;
                    objENTCheck.DelieveryTypeName = rdoDelivery.Text;
                    if (rdoShipping.Checked == true)
                    {
                        objENTCheck.DeliveryAddressType = 1;
                        objENTCheck.DeliveryAddressTypeName = rdoShipping.Text;
                    }
                    else if (rdoBilling.Checked == true)
                    {
                        objENTCheck.DeliveryAddressType = 2;
                        objENTCheck.DeliveryAddressTypeName = rdoBilling.Text;
                    }
                    else
                    {
                        MessageBox.Show("Select one of the address option.");
                        rdoShipping.Focus();
                        return;
                    }
                    #endregion
                }
                else if (rdoPartyEvent.Checked == true)
                {
                    objENTCheck.DelieveryType = 4;
                    objENTCheck.DelieveryTypeName = rdoPartyEvent.Text;
                }
                else if (rdoQueue.Checked == true)
                {
                    #region Queue
                    objENTCheck.DelieveryType = 5;
                    objENTCheck.DelieveryTypeName = rdoQueue.Text;
                    if (rdoQueueDineIn.Checked == true)
                    {
                        objENTCheck.QueueType = 1;
                        objENTCheck.QueueTypeName = rdoQueueDineIn.Text;
                    }
                    else if (rdoQueueTakeOut.Checked == true)
                    {
                        objENTCheck.QueueType = 2;
                        objENTCheck.QueueTypeName = rdoQueueTakeOut.Text;
                    }
                    else
                    {
                        MessageBox.Show("Select one of the queue option.");
                        rdoQueueDineIn.Focus();
                        return;
                    }

                    if (chkCurrentQueue.Checked == true)
                    {
                        objENTCheck.IsCurrentQueue = true;
                    }
                    else if (GlobalVariable.IsDate(txtDateTime.Text))
                    {
                        objENTCheck.QueueDatetime = GlobalVariable.ChangeDateTime(txtDateTime.Text);
                    }
                    else
                    {
                        MessageBox.Show("Check the checkbox of current queue. OR Enter valid date.");
                        txtDateTime.Focus();
                        return;
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("Select one option.");
                    rdoDineIn.Focus();
                    return;
                }

                #region Select Customer
                if (txtCustomerName.Text.Trim() != string.Empty)
                {
                    objENTCheck.CustomerID = txtCustomerId.Text.Trim()==string.Empty ? Guid.NewGuid() : new Guid(txtCustomerId.Text.Trim());
                    objENTCheck.CustomerName = txtCustomerName.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Enter customer name.");
                    txtCustomerName.Focus();
                    return;
                }
                #endregion

                GlobalVariable.IsOk = true;
                GlobalVariable.objCheckInfo = objENTCheck;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rdoDineIn_CheckedChanged(object sender, EventArgs e)
        {
            this.HideShowType(true, false, false);
        }

        private void rdoTakeOut_CheckedChanged(object sender, EventArgs e)
        {
            this.HideShowType(false, false, false);
        }

        private void rdoDelivery_CheckedChanged(object sender, EventArgs e)
        {
            this.HideShowType(false, true, false);
        }

        private void rdoPartyEvent_CheckedChanged(object sender, EventArgs e)
        {
            this.HideShowType(false, false, false);
        }

        private void rdoQueue_CheckedChanged(object sender, EventArgs e)
        {
            this.HideShowType(false, false, true);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.HideShowButton(false, true, false, true);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.HideShowButton(true, false, true, false);
        }

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.getSearchCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchMobile_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.getSearchCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCustomers_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgvCustomers.Rows.Count > 0)
                    {
                        int row = dgvCustomers.CurrentRow.Index;
                        txtCustomerId.Text = Convert.ToString(dgvCustomers.Rows[row].Cells["CustID"].Value);
                        DAL.CustomerMasterData objDALCustomers = new DAL.CustomerMasterData();
                        ENT.CustomerMasterData objENTCustomers = new ENT.CustomerMasterData();
                        List<ENT.CustomerMasterData> lstENTCustomers = new List<ENT.CustomerMasterData>();

                        objENTCustomers.Mode = "GetRecordByID";
                        objENTCustomers.CustomerID = new Guid(txtCustomerId.Text);
                        lstENTCustomers = objDALCustomers.getCustomerData(objENTCustomers);
                        if (lstENTCustomers.Count > 0)
                        {
                            txtCustomerName.Text = Convert.ToString(lstENTCustomers[0].Name);
                            txtPhone.Text = Convert.ToString(lstENTCustomers[0].MobileNo);
                            txtEmail.Text = Convert.ToString(lstENTCustomers[0].EmailID);
                            txtBirthdate.Text = GlobalVariable.IsDate(lstENTCustomers[0].Birthdate)==true ? Convert.ToDateTime(lstENTCustomers[0].Birthdate).ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                            txtBillAdd.Text = Convert.ToString(lstENTCustomers[0].Name);
                        }
                        this.HideShowButton(false, true, false, true);
                    }
                    else
                    {
                        MessageBox.Show("Record not found.");
                        txtSearchName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            try
            {
                txtCustomerId.Text = Guid.NewGuid().ToString();
                txtCustomerName.Text = "Guest";
                txtPhone.Text = "0000000000";
                this.HideShowButton(false, true, false, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }
    }
}
