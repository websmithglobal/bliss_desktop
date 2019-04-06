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
    public partial class frmDiscountSelect : Form
    {
        ENT.DiscountMasterDetail objENTDMD = new ENT.DiscountMasterDetail();
        DAL.DiscountMasterDetail objDALDMD = new DAL.DiscountMasterDetail();
        List<ENT.DiscountMasterDetail> lstENTDMD = new List<ENT.DiscountMasterDetail>();
        int DeliveryTypeID = 0;

        public frmDiscountSelect()
        {
            InitializeComponent();
        }

        public frmDiscountSelect(int DelTypeID)
        {
            InitializeComponent();
            DeliveryTypeID = DelTypeID;
        }

        public void GetDiscountDetail()
        {
            try
            {
                listView.Items.Clear();
                objENTDMD.Mode = "GetListForSelectDiscount";
                lstENTDMD = objDALDMD.getDiscountMasterDetail(objENTDMD);
                for (int i = 0; i < lstENTDMD.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = lstENTDMD[i].DiscountName;
                    item.SubItems.Add(lstENTDMD[i].DiscountTypeName.ToString());
                    item.SubItems.Add(lstENTDMD[i].AmountOrPercentage.ToString());
                    item.SubItems.Add(lstENTDMD[i].AutoApply.ToString());
                    item.SubItems.Add(lstENTDMD[i].DiscountID.ToString());
                    item.SubItems.Add(lstENTDMD[i].DiscountType.ToString());
                    listView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDiscountSelect_Load(object sender, EventArgs e)
        {
            try
            {
                if (DeliveryTypeID == Convert.ToInt32(GlobalVariable.DiscountType.OnOrder))
                {
                    this.Text = "Order Discount";
                }
                else if (DeliveryTypeID == Convert.ToInt32(GlobalVariable.DiscountType.OnItem))
                {
                    this.Text = "Item Discount";
                }

                chkCustom.Checked = false;
                txtAmt.Enabled = false;
                rdoAmt.Enabled = false;
                rdoPer.Enabled = false;
                listView.Enabled = true;
                GetDiscountDetail();

                if (ENT.DiscountMasterDetail.DiscountTypeID == 1)
                    rdoAmt.Checked = true;
                else if (ENT.DiscountMasterDetail.DiscountTypeID == 2)
                    rdoPer.Checked = true;
                else
                { rdoAmt.Checked = false; rdoPer.Checked = false; }

                txtAmt.Text = Convert.ToString(ENT.DiscountMasterDetail.DiscountAmtPer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Discount", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void chkCustom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCustom.Checked)
                {
                    txtAmt.Enabled = true;
                    rdoAmt.Enabled = true;
                    rdoPer.Enabled = true;
                    listView.Enabled = false;
                }
                else
                {
                    txtAmt.Enabled = false;
                    rdoAmt.Enabled = false;
                    rdoPer.Enabled = false;
                    listView.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Discount", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count > 0)
                {
                    txtAmt.Text = listView.SelectedItems[0].SubItems[2].Text;
                    txtDiscountID.Text = listView.SelectedItems[0].SubItems[4].Text;

                    if (Convert.ToInt32(listView.SelectedItems[0].SubItems[5].Text) == 1)
                    {
                        rdoAmt.Checked = true;
                    }
                    else if (Convert.ToInt32(listView.SelectedItems[0].SubItems[5].Text) == 2)
                    {
                        rdoPer.Checked = true;
                    }

                    //string s1 = listView.SelectedItems[0].SubItems[0].Text;
                    //string s2 = listView.SelectedItems[0].SubItems[1].Text;
                    //string s3 = listView.SelectedItems[0].SubItems[2].Text;
                    //string s4 = listView.SelectedItems[0].SubItems[3].Text;
                    //string s5 = listView.SelectedItems[0].SubItems[4].Text;
                    //string s6 = listView.SelectedItems[0].SubItems[5].Text;
                    //MessageBox.Show(s1 + Environment.NewLine + s2 + Environment.NewLine + s3 + Environment.NewLine + s4 + Environment.NewLine + s5 + Environment.NewLine + s6);
                }
                else
                {
                    MessageBox.Show("Please select an item before assigning a value.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Discount", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAmt.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Amount Or Percentage should not empty.", "Discount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmt.Focus();
                    return;
                }
                if (rdoAmt.Checked == false && rdoPer.Checked == false)
                {
                    MessageBox.Show("Amount Or Percentage must be selected.", "Discount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rdoPer.Focus();
                    return;
                }
                if (rdoPer.Checked)
                {
                    if (Convert.ToDecimal(txtAmt.Text.Trim()) > 100)
                    {
                        MessageBox.Show("Percentage must be less than 100.", "Discount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtAmt.Focus();
                        return;
                    }
                }

                if (ENT.DiscountMasterDetail.OrderID == null)
                {
                    MessageBox.Show("Select valid order.");
                    return;
                }
                if (DeliveryTypeID == Convert.ToInt32(GlobalVariable.DiscountType.OnOrder))
                {
                    if (rdoAmt.Checked == true)
                    {
                        ENT.DiscountMasterDetail.SelectedDiscountID = txtDiscountID.Text.Trim() == string.Empty ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(txtDiscountID.Text);
                        ENT.DiscountMasterDetail.DiscountAmtPer = Convert.ToDecimal(txtAmt.Text.Trim());
                        ENT.DiscountMasterDetail.DiscountTypeID = 1;
                        ENT.DiscountMasterDetail.DiscountRemark = txtRemark.Text.Trim();
                    }
                    else if (rdoPer.Checked == true)
                    {
                        ENT.DiscountMasterDetail.SelectedDiscountID = txtDiscountID.Text.Trim() == string.Empty ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(txtDiscountID.Text);
                        ENT.DiscountMasterDetail.DiscountAmtPer = Convert.ToDecimal(txtAmt.Text.Trim());
                        ENT.DiscountMasterDetail.DiscountTypeID = rdoAmt.Checked == true ? 1 : rdoPer.Checked == true ? 2 : 0;
                        ENT.DiscountMasterDetail.DiscountRemark = txtRemark.Text.Trim();
                    }
                }
                else if (DeliveryTypeID == Convert.ToInt32(GlobalVariable.DiscountType.OnItem))
                {
                    ENT.DiscountMasterDetail.SelectedDiscountID = txtDiscountID.Text.Trim() == string.Empty ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(txtDiscountID.Text);
                    ENT.OrderBook.DiscAmountOrPercent = Convert.ToDecimal(txtAmt.Text.Trim());
                    ENT.OrderBook.DiscAmountOrPercentID = rdoAmt.Checked == true ? 1 : rdoPer.Checked == true ? 2 : 0;
                    ENT.OrderBook.DiscRemark = txtRemark.Text.Trim();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Discount", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtAmt.Text.Contains(".")) && (e.KeyChar == '.'))
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                ENT.OrderBook.DiscAmountOrPercent = 0;
                ENT.OrderBook.DiscAmountOrPercentID = 1;
                ENT.OrderBook.DiscRemark = "";
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Discount", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDiscountSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
