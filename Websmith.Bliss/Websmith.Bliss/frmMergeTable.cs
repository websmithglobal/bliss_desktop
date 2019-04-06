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
    public partial class frmMergeTable : Form
    {
        string OrderID;
        string TableID;
        string strMode;

        ENT.OrderBook objENTOrder = new ENT.OrderBook();
        List<ENT.OrderBook> lstENTOrder = new List<ENT.OrderBook>();
        DAL.OrderBook objDALOrder = new DAL.OrderBook();

        ENT.MergeTable objENTMT = new ENT.MergeTable();
        List<ENT.MergeTable> lstENTMT = new List<ENT.MergeTable>();
        DAL.MergeTable objDALMT = new DAL.MergeTable();

        public frmMergeTable()
        {
            InitializeComponent();
        }

        public frmMergeTable(string OrderIdPassed, string TableIdPassed)
        {
            InitializeComponent();
            OrderID = OrderIdPassed;
            TableID = TableIdPassed;
        }

        private void frmMergeTable_Load(object sender, EventArgs e)
        {
            try
            {
                tcMain.TabPages.Remove(tpReservation);
                this.GetOrderDetail();
                objENTMT = new ENT.MergeTable();
                objENTMT.OrderID = new Guid(txtOrderID.Text.Trim());
                objENTMT.Mode = "GetRecordByOrderID";
                if (objDALMT.GetTableCountInOrder(objENTMT) <= 1)
                { btnRelease.Enabled = false; btnApplyTran.Enabled = true; }
                else
                { btnRelease.Enabled = true; btnApplyTran.Enabled = false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearData()
        {
            try
            {
                txtOrderID.Text = lstENTOrder[0].OrderID.ToString();
                lblInvoiceNo.Text = "Invoice No. : " + lstENTOrder[0].OrderNo.ToString();
                txtTableID.Text = lstENTOrder[0].TableID.ToString();
                lblTableNo.Text = "Table : " + lstENTOrder[0].TableName;
                txtCustomerID.Text = lstENTOrder[0].CustomerID.ToString();
                lblCustomerName.Text = "Name : " + lstENTOrder[0].Name;
                lblPhoneNo.Text = "Phone No. : " + lstENTOrder[0].MobileNo.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetOrderDetail()
        {
            try
            {
                objENTOrder.Mode = "GetRecordByOrderAndTableID";
                objENTOrder.OrderID = new Guid(OrderID);
                objENTOrder.TableID = new Guid(TableID);
                lstENTOrder = objDALOrder.getOrder(objENTOrder);
                if (lstENTOrder.Count > 0)
                {
                    txtOrderID.Text = lstENTOrder[0].OrderID.ToString();
                    lblInvoiceNo.Text = "Invoice No. : " + lstENTOrder[0].OrderNo.ToString();
                    txtTableID.Text = lstENTOrder[0].TableID.ToString();
                    lblTableNo.Text = "Table : " + lstENTOrder[0].TableName;
                    txtCustomerID.Text = lstENTOrder[0].CustomerID.ToString();
                    lblCustomerName.Text = "Name : " + lstENTOrder[0].Name;
                    lblPhoneNo.Text = "Phone No. : " + lstENTOrder[0].MobileNo.ToString();
                    this.getTableVacantForMerge();
                    this.getTableVacantForTransfer();
                    strMode = "ADD";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Merge With

        private void getTableVacantForMerge()
        {
            try
            {
                DAL.TableMasterDetail objDALTable = new DAL.TableMasterDetail();
                ENT.TableMasterDetail objENTTable = new ENT.TableMasterDetail();
                List<ENT.TableMasterDetail> lstENTTable = new List<ENT.TableMasterDetail>();

                //objENTTable.Mode = "GetTableForViewByEmpID";
                objENTTable.Mode = "GetTableForViewByClassID";
                objENTTable.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                objENTTable.ClassID = new Guid(GlobalVariable.ClassID);
                lstENTTable = objDALTable.getTableMasterDetail(objENTTable);
                lstENTTable = lstENTTable.Where(tbl => tbl.StatusID == Convert.ToInt32(GlobalVariable.TableStatus.Vacant)).ToList();

                int pnl_width = 0;
                int x = 0;
                int y = 12;
                int i = 0;
                int col = 0;
                double row = 0;

                if (pnlMergeWith.Width > 530)
                {
                    col = 5;
                    pnl_width = (pnlMergeWith.Width - 524) / 2;
                    if (lstENTTable.Count <= col)
                        row = lstENTTable.Count == 0 ? 0 : 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstENTTable.Count) / col, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    col = 3;
                    pnl_width = (pnlMergeWith.Width - 432) / 2;
                    if (lstENTTable.Count <= col)
                        row = lstENTTable.Count == 0 ? 0 : 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstENTTable.Count) / col, 2, MidpointRounding.AwayFromZero);
                }

                for (int h = 0; h < row; h++)
                {
                    x = pnl_width;
                    for (int v = 0; v < col; v++)
                    {
                        CheckBox chkTableName = new CheckBox();
                        chkTableName.AutoSize = true;
                        chkTableName.Location = new System.Drawing.Point(10, 32);
                        chkTableName.Name = "lblVCTableName" + i;
                        chkTableName.Tag = lstENTTable[i].TableID;
                        chkTableName.Text = lstENTTable[i].TableName;
                        chkTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        chkTableName.ForeColor = Color.White;

                        Panel pnlButton = new Panel();
                        pnlButton.Location = new Point(x, y);
                        pnlButton.Click += new EventHandler(pnlButtonMerge_ClickEvent);
                        pnlButton.Tag = lstENTTable[i].TableID;
                        pnlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)))));
                        pnlButton.Size = new System.Drawing.Size(100, 84);
                        pnlButton.BackColor = Color.Goldenrod; // Color.Green;
                        pnlButton.BorderStyle = BorderStyle.FixedSingle;
                        pnlButton.TabIndex = i;
                        pnlButton.Controls.Add(chkTableName);
                        pnlMergeWith.Controls.Add(pnlButton);
                        if (i == lstENTTable.Count - 1)
                        {
                            break;
                        }
                        i++;
                        x = x + 106;
                    }
                    y = y + 90;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnlButtonMerge_ClickEvent(object sender, EventArgs e)
        {
            Panel button = sender as Panel;
            List<Control> allCheckBox = new List<Control>();
            foreach (CheckBox chk in GlobalVariable.FindControlRecursive(allCheckBox, button, typeof(CheckBox)))
            {
                if (chk.Checked)
                    chk.Checked = false;
                else
                    chk.Checked = true;
            }
        }
        
        private void btnApplyMerge_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtOrderID.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Order is not valid.", "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtTableID.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Table is not valid for this order.", "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool result=false;
                List<Control> allCheckBox = new List<Control>();
                foreach (CheckBox chk in GlobalVariable.FindControlRecursive(allCheckBox, pnlMergeWith, typeof(CheckBox)))
                {
                    if (chk.Checked)
                    {
                        objENTMT.ID = Guid.NewGuid();
                        objENTMT.OrderID = new Guid(txtOrderID.Text.Trim());
                        objENTMT.TableID = new Guid(chk.Tag.ToString());
                        objENTMT.TableStatusID = Convert.ToInt32(GlobalVariable.TableStatus.Occupied);
                        objENTMT.IsVacant = 0;
                        objENTMT.Mode = strMode;
                        result = objDALMT.InsertUpdateDeleteMergeTable(objENTMT);
                    }
                }

                if (result)
                {
                    MessageBox.Show("Table is merged successfully.", "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Atleast one table must be selected.", "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlgResult = MessageBox.Show("Are you sure to release this table.", "Merge Table", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }

                if (txtOrderID.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Order is not valid.", "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtTableID.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Table is not valid for this order.", "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                objENTMT = new ENT.MergeTable();
                objENTMT.OrderID = new Guid(txtOrderID.Text.Trim());
                objENTMT.TableID = new Guid(txtTableID.Text.ToString());
                objENTMT.TableStatusID = Convert.ToInt32(GlobalVariable.TableStatus.Vacant);
                objENTMT.Mode = "DELETE_BY_ORDERID_TABLEID";
                
                if (objDALMT.InsertUpdateDeleteMergeTable(objENTMT))
                {
                    MessageBox.Show("Table is released successfully.", "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCloseMerge_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Transfer To

        private void ClearCheckboxTransfer()
        {
            try
            {
                List<Control> allCheckBox = new List<Control>();
                foreach (CheckBox chk in GlobalVariable.FindControlRecursive(allCheckBox, pnlTransferTo, typeof(CheckBox)))
                {
                    chk.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getTableVacantForTransfer()
        {
            try
            {
                DAL.TableMasterDetail objDALTable = new DAL.TableMasterDetail();
                ENT.TableMasterDetail objENTTable = new ENT.TableMasterDetail();
                List<ENT.TableMasterDetail> lstENTTable = new List<ENT.TableMasterDetail>();

                //objENTTable.Mode = "GetTableForViewByEmpID";
                objENTTable.Mode = "GetTableForViewByClassID";
                objENTTable.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                objENTTable.ClassID = new Guid(GlobalVariable.ClassID);
                lstENTTable = objDALTable.getTableMasterDetail(objENTTable);
                lstENTTable = lstENTTable.Where(tbl => tbl.StatusID == Convert.ToInt32(GlobalVariable.TableStatus.Vacant)).ToList();

                int pnl_width = 0;
                int x = 0;
                int y = 12;
                int i = 0;
                int col = 0;
                double row = 0;

                if (pnlMergeWith.Width > 530)
                {
                    col = 5;
                    pnl_width = (pnlMergeWith.Width - 524) / 2;
                    if (lstENTTable.Count <= col)
                        row = lstENTTable.Count == 0 ? 0 : 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstENTTable.Count) / col, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    col = 3;
                    pnl_width = (pnlMergeWith.Width - 432) / 2;
                    if (lstENTTable.Count <= col)
                        row = lstENTTable.Count == 0 ? 0 : 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstENTTable.Count) / col, 2, MidpointRounding.AwayFromZero);
                }

                for (int h = 0; h < row; h++)
                {
                    x = pnl_width;
                    for (int v = 0; v < col; v++)
                    {
                        CheckBox chkTableName = new CheckBox();
                        chkTableName.AutoSize = true;
                        chkTableName.Location = new System.Drawing.Point(10, 32);
                        chkTableName.Name = "lblVCTableName" + i;
                        chkTableName.Tag = lstENTTable[i].TableID;
                        chkTableName.Text = lstENTTable[i].TableName;
                        chkTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        chkTableName.ForeColor = Color.White;
                        chkTableName.Click += new EventHandler(chkTableNameTransfer_CheckedChanged);

                        Panel pnlButton = new Panel();
                        pnlButton.Location = new Point(x, y);
                        pnlButton.Click += new EventHandler(pnlButtonTransfer_ClickEvent);
                        pnlButton.Tag = lstENTTable[i].TableID;
                        pnlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)))));
                        pnlButton.Size = new System.Drawing.Size(100, 84);
                        pnlButton.BackColor = Color.Goldenrod;
                        pnlButton.BorderStyle = BorderStyle.FixedSingle;
                        pnlButton.TabIndex = i;
                        pnlButton.Controls.Add(chkTableName);
                        pnlTransferTo.Controls.Add(pnlButton);
                        if (i == lstENTTable.Count - 1)
                        {
                            break;
                        }
                        i++;
                        x = x + 106;
                    }
                    y = y + 90;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnlButtonTransfer_ClickEvent(object sender, EventArgs e)
        {
            try
            {
                Panel button = sender as Panel;
                this.ClearCheckboxTransfer();
                List<Control> allCheckBox = new List<Control>();
                foreach (CheckBox chk in GlobalVariable.FindControlRecursive(allCheckBox, button, typeof(CheckBox)))
                {
                    chk.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkTableNameTransfer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;
                this.ClearCheckboxTransfer();
                chk.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApplyTran_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOrderID.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Order is not valid.", "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtTableID.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Table is not valid for this order.", "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool result = false;
                List<Control> allCheckBox = new List<Control>();
                foreach (CheckBox chk in GlobalVariable.FindControlRecursive(allCheckBox, pnlTransferTo, typeof(CheckBox)))
                {
                    if (chk.Checked)
                    {
                        objENTMT.OrderID = new Guid(txtOrderID.Text.Trim());
                        objENTMT.TableID = new Guid(chk.Tag.ToString());
                        objENTMT.OldTableID = new Guid(txtTableID.Text.Trim());
                        objENTMT.TableStatusID = Convert.ToInt32(GlobalVariable.TableStatus.Occupied);
                        objENTMT.Mode = "UPDATE";
                        result = objDALMT.InsertUpdateDeleteMergeTable(objENTMT);
                    }
                }
                if (result)
                {
                    MessageBox.Show("Table is transfered successfully.", "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Merge Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCloseTran_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void frmMergeTable_KeyDown(object sender, KeyEventArgs e)
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
