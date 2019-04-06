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
    public partial class frmCheckInfoExtra : Form
    {
        DAL.CustomerMasterData objDALCustomers = new DAL.CustomerMasterData();
        ENT.CustomerMasterData objENTCustomers = new ENT.CustomerMasterData();
        List<ENT.CustomerMasterData> lstENTCustomers = new List<ENT.CustomerMasterData>();
        ENT.CheckInfo objENTCheck = new ENT.CheckInfo();

        public frmCheckInfoExtra()
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

        private void frmSelectCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                this.HideShowType(false, false, false);
                this.fillComboTable();
                this.getSearchCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                frmCustomer frmCust = new frmCustomer();
                frmCust.WindowState = FormWindowState.Normal;
                frmCust.ShowDialog();
                this.getSearchCustomer();
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
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
                if (dgvCustomers.RowCount > 0)
                {
                    int row = dgvCustomers.CurrentRow.Index;
                    if (row >= 0)
                    {
                        objENTCheck.CustomerID = new Guid(dgvCustomers.Rows[row].Cells["CustID"].Value.ToString());
                        objENTCheck.CustomerName = dgvCustomers.Rows[row].Cells["CustName"].Value.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Select one customer from list.");
                        dgvCustomers.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Search customer from list OR Add new customer.");
                    txtSearchName.Focus();
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

        private void btnGuest_Click(object sender, EventArgs e)
        {

        }
    }
}
