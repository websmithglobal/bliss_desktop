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
    public partial class frmTillCheckout : Form
    {
        ENT.TillManage objENTTill;
        DAL.TillManage objDALTill = new DAL.TillManage();
        List<ENT.TillManage> lstENTTill = new List<ENT.TillManage>();
        string strMode = "";
        int Flag = 0;

        public frmTillCheckout()
        {
            InitializeComponent();
        }

        public frmTillCheckout(string strTillID)
        {
            InitializeComponent();
            txtTillId.Text = strTillID;
        }

        public void EnterAmount(string Num)
        {
            if (Flag == 5)
            {
                if (txtFive.Text.Trim() == "0")
                {
                    txtFive.Text = Num;
                }
                else
                {
                    txtFive.Text += Num;
                }                
            }
            else if (Flag == 10)
            {
                if (txtTen.Text.Trim() == "0")
                {
                    txtTen.Text = Num;
                }
                else
                {
                    txtTen.Text += Num;
                }               
            }
            else if (Flag == 20)
            {
                if (txtTwenty.Text.Trim() == "0")
                {
                    txtTwenty.Text = Num;
                }
                else
                {
                    txtTwenty.Text += Num;
                }
            }
            else if (Flag == 50)
            {
                if (txtFifty.Text.Trim() == "0")
                {
                    txtFifty.Text = Num;
                }
                else
                {
                    txtFifty.Text += Num;
                }
            }
            else if (Flag == 100)
            {
                if (txtHundred.Text.Trim() == "0")
                {
                    txtHundred.Text = Num;
                }
                else
                {
                    txtHundred.Text += Num;
                }
            }
            else if (Flag == 200)
            {
                if (txtTwoHundred.Text.Trim() == "0")
                {
                    txtTwoHundred.Text = Num;
                }
                else
                {
                    txtTwoHundred.Text += Num;
                }
            }
            else if (Flag == 500)
            {
                if (txtFiveHundred.Text.Trim() == "0")
                {
                    txtFiveHundred.Text = Num;
                }
                else
                {
                    txtFiveHundred.Text += Num;
                }
            }
            else if (Flag == 1000)
            {
                if (txtOneThousand.Text.Trim() == "0")
                {
                    txtOneThousand.Text = Num;
                }
                else
                {
                    txtOneThousand.Text += Num;
                }
            }
            else if (Flag == 2000)
            {
                if (txtTwoThousand.Text.Trim() == "0")
                {
                    txtTwoThousand.Text = Num;
                }
                else
                {
                    txtTwoThousand.Text += Num;
                }
            }
            GetTotal();
        }

        private void GetGeneralSetting()
        {
            try
            {
                ENT.GeneralSetting objENT = new ENT.GeneralSetting();
                DAL.GeneralSetting objDAL = new DAL.GeneralSetting();
                List<ENT.GeneralSetting> lstENT = new List<ENT.GeneralSetting>();
                objENT.Mode = "GetAll";
                lstENT = objDAL.GetGeneralSetting(objENT);
                if (lstENT.Count > 0)
                {
                    lblFive.Text = Convert.ToString(lstENT[0].TillCur1);
                    lblTen.Text = Convert.ToString(lstENT[0].TillCur2);
                    lblTwenty.Text = Convert.ToString(lstENT[0].TillCur3);
                    lblFifty.Text = Convert.ToString(lstENT[0].TillCur4);
                    lblHundred.Text = Convert.ToString(lstENT[0].TillCur5);
                    lblTwoHundred.Text = Convert.ToString(lstENT[0].TillCur6);
                    lblFiveHundred.Text = Convert.ToString(lstENT[0].TillCur7);
                    lblOneThousand.Text = Convert.ToString(lstENT[0].TillCur8);
                    lblTwoThousand.Text = Convert.ToString(lstENT[0].TillCur9);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "General Setting", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetTotal()
        {
            try
            {
                decimal decAmount = 0;
                decimal decAmountTotal = 0;
                txtEndCash.Text = txtEndCash.Text.Trim() == string.Empty ? "0" : txtEndCash.Text.Trim();
                txtFive.Text = txtFive.Text.Trim() == string.Empty ? "0" : txtFive.Text.Trim();
                txtTen.Text = txtTen.Text.Trim() == string.Empty ? "0" : txtTen.Text.Trim();
                txtTwenty.Text = txtTwenty.Text.Trim() == string.Empty ? "0" : txtTwenty.Text.Trim();
                txtFifty.Text = txtFifty.Text.Trim() == string.Empty ? "0" : txtFifty.Text.Trim();
                txtHundred.Text = txtHundred.Text.Trim() == string.Empty ? "0" : txtHundred.Text.Trim();
                txtTwoHundred.Text = txtTwoHundred.Text.Trim() == string.Empty ? "0" : txtTwoHundred.Text.Trim();
                txtFiveHundred.Text = txtFiveHundred.Text.Trim() == string.Empty ? "0" : txtFiveHundred.Text.Trim();
                txtOneThousand.Text = txtOneThousand.Text.Trim() == string.Empty ? "0" : txtOneThousand.Text.Trim();
                txtTwoThousand.Text = txtTwoThousand.Text.Trim() == string.Empty ? "0" : txtTwoThousand.Text.Trim();

                decAmount += Convert.ToDecimal(txtFive.Text) * Convert.ToDecimal(lblFive.Text);
                decAmountTotal += Convert.ToDecimal(txtEndCash.Text) + decAmount;
                decAmount += Convert.ToDecimal(txtTen.Text) * Convert.ToDecimal(lblTen.Text);
                decAmountTotal += Convert.ToDecimal(txtEndCash.Text) + decAmount;
                decAmount += Convert.ToDecimal(txtTwenty.Text) * Convert.ToDecimal(lblTwenty.Text);
                decAmountTotal += Convert.ToDecimal(txtEndCash.Text) + decAmount;
                decAmount += Convert.ToDecimal(txtFifty.Text) * Convert.ToDecimal(lblFifty.Text);
                decAmountTotal += Convert.ToDecimal(txtEndCash.Text) + decAmount;
                decAmount += Convert.ToDecimal(txtHundred.Text) * Convert.ToDecimal(lblHundred.Text);
                decAmountTotal += Convert.ToDecimal(txtEndCash.Text) + decAmount;
                decAmount += Convert.ToDecimal(txtTwoHundred.Text) * Convert.ToDecimal(lblTwoHundred.Text);
                decAmountTotal += Convert.ToDecimal(txtEndCash.Text) + decAmount;
                decAmount += Convert.ToDecimal(txtFiveHundred.Text) * Convert.ToDecimal(lblFiveHundred.Text);
                decAmountTotal += Convert.ToDecimal(txtEndCash.Text) + decAmount;
                decAmount += Convert.ToDecimal(txtOneThousand.Text) * Convert.ToDecimal(lblOneThousand.Text);
                decAmountTotal += Convert.ToDecimal(txtEndCash.Text) + decAmount;
                decAmount += Convert.ToDecimal(txtTwoThousand.Text) * Convert.ToDecimal(lblTwoThousand.Text);
                decAmountTotal += Convert.ToDecimal(txtEndCash.Text) + decAmount;
                txtEndCash.Text = decAmount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Till Checkout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getDifferenceTill()
        {
            try
            {
                objENTTill = new ENT.TillManage();
                objENTTill.Mode = "GetByIsTillDone";
                objENTTill.IsTillDone = false;
                lstENTTill = objDALTill.getTillManage(objENTTill);
                if (lstENTTill.Count > 0)
                {
                    lblEndCash.Text = "Approx End Cash : " + lstENTTill[0].Difference.ToString();
                }
                else
                {
                    lblEndCash.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Till Manage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmTill_Load(object sender, EventArgs e)
        {
            strMode = "UPDATE_CHECKOUT";
            Flag = 5;
            this.GetGeneralSetting();
            getDifferenceTill();
            txtFive.Focus();
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            this.EnterAmount(btn.Text.Trim());
        }
        
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEndCash.Text = string.Empty;
            txtFive.Text = string.Empty;
            txtTen.Text = string.Empty;
            txtTwenty.Text = string.Empty;
            txtFifty.Text = string.Empty;
            txtHundred.Text = string.Empty;
            txtTwoHundred.Text = string.Empty;
            txtFiveHundred.Text = string.Empty;
            txtOneThousand.Text = string.Empty;
            txtTwoThousand.Text = string.Empty;
            Flag = 5;
            txtFive.Focus();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if ((!txt.Text.Contains(".")) && (e.KeyChar == '.'))
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

        private void txtFive_TextChanged(object sender, EventArgs e)
        {
            GetTotal();
        }
        
        private void txtFive_Click(object sender, EventArgs e)
        {
            Flag = 5;
        }

        private void txtTen_Click(object sender, EventArgs e)
        {
            Flag = 10;
        }

        private void txtTwenty_Click(object sender, EventArgs e)
        {
            Flag = 20;
        }

        private void txtFifty_Click(object sender, EventArgs e)
        {
            Flag = 50;
        }

        private void txtHundred_Click(object sender, EventArgs e)
        {
            Flag = 100;
        }

        private void txtTwoHundred_Click(object sender, EventArgs e)
        {
            Flag = 200;
        }

        private void txtFiveHundred_Click(object sender, EventArgs e)
        {
            Flag = 500;
        }

        private void txtOneThousand_Click(object sender, EventArgs e)
        {
            Flag = 1000;
        }

        private void txtTwoThousand_Click(object sender, EventArgs e)
        {
            Flag = 2000;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTillId.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Till Id should not empty.", "Till Manage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTillId.Focus();
                    return;
                }
                if (txtEndCash.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("End cash should not empty.", "Till Manage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEndCash.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtEndCash.Text.Trim()) < 0)
                {
                    MessageBox.Show("End cash must be greater than zero.", "Till Manage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEndCash.Focus();
                    return;
                }

                objENTTill = new ENT.TillManage();
                objENTTill.TillID = new Guid(txtTillId.Text);
                objENTTill.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                objENTTill.Currency5 = txtFive.Text=="" ? 0 : Convert.ToInt32(txtFive.Text);
                objENTTill.Currency10 = txtTen.Text == "" ? 0 : Convert.ToInt32(txtTen.Text);
                objENTTill.Currency20 = txtTwenty.Text == "" ? 0 : Convert.ToInt32(txtTwenty.Text);
                objENTTill.Currency50 = txtFifty.Text == "" ? 0 : Convert.ToInt32(txtFifty.Text);
                objENTTill.Currency100 = txtHundred.Text == "" ? 0 : Convert.ToInt32(txtHundred.Text);
                objENTTill.Currency200 = txtTwoHundred.Text == "" ? 0 : Convert.ToInt32(txtTwoHundred.Text);
                objENTTill.Currency500 = txtFiveHundred.Text == "" ? 0 : Convert.ToInt32(txtFiveHundred.Text);
                objENTTill.Currency1000 = txtOneThousand.Text == "" ? 0 : Convert.ToInt32(txtOneThousand.Text);
                objENTTill.Currency2000 = txtTwoThousand.Text == "" ? 0 : Convert.ToInt32(txtTwoThousand.Text);
                objENTTill.EndCash = txtEndCash.Text == "" ? 0 : Convert.ToInt32(txtEndCash.Text);
                objENTTill.EndDateTime = GlobalVariable.ChangeDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                objENTTill.IsTillDone = true;
                objENTTill.Mode = strMode;
                if (objDALTill.InsertUpdateDeleteTillManage(objENTTill))
                {
                    MessageBox.Show("Till amount is set successfully.", "Till Manage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFive.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Till Checkout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmTillCheckout_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }

            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
