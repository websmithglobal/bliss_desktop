using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.Bliss
{
    public partial class frmTill : Form
    {
        ENT.TillManage objENTTill;
        DAL.TillManage objDALTill = new DAL.TillManage();
        List<ENT.TillManage> lstENTTill = new List<ENT.TillManage>();
        string strMode = "";

        public frmTill()
        {
            InitializeComponent();
        }

        private void EnterAmount(string code)
        {
            if (txtAmount.Text.Length < 15)
            {
                txtAmount.Text += code.Trim();
            }
        }

        private void getCurrentTill()
        {
            try
            {
                objENTTill = new ENT.TillManage();
                objENTTill.Mode = "GetByIsTillDone";
                objENTTill.IsTillDone = false;
                lstENTTill = objDALTill.getTillManage(objENTTill);
                if (lstENTTill.Count > 0)
                {
                    txtTillId.Text = lstENTTill[0].TillID.ToString();
                    txtAmount.Text = Convert.ToString(lstENTTill[0].StartCash);
                    lblTillWasSetOn.Text = "Till was set ON : " + lstENTTill[0].StartDateTime;
                    pnlNumeric.Enabled = true;
                    btnPayIn.Enabled = true;
                    btnPayOut.Enabled = true;
                    btnCashLog.Enabled = true;
                    btnCheckout.Enabled = true;
                    strMode = "UPDATE";
                }
                else
                {
                    btnPayIn.Enabled = false;
                    btnPayOut.Enabled = false;
                    btnCashLog.Enabled = false;
                    btnCheckout.Enabled = false;
                    lblTillWasSetOn.Text = "Till is not set.";
                    txtTillId.Text = Guid.NewGuid().ToString();
                    strMode = "ADD";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Till Manage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmTill_Load(object sender, EventArgs e)
        {
            try
            {
                txtTillId.Text = string.Empty;
                txtAmount.Text = string.Empty;
                getCurrentTill();
                txtAmount.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Till Manage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            this.EnterAmount(btnOne.Text.Trim());
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            this.EnterAmount(btnTwo.Text.Trim());
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            this.EnterAmount(btnThree.Text.Trim());
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            this.EnterAmount(btnFour.Text.Trim());
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            this.EnterAmount(btnFive.Text.Trim());
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            this.EnterAmount(btnSix.Text.Trim());
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            this.EnterAmount(btnSeven.Text.Trim());
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            this.EnterAmount(btnEight.Text.Trim());
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            this.EnterAmount(btnNine.Text.Trim());
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            this.EnterAmount(btnZero.Text.Trim());
        }

        private void btnPoint_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text.Contains("."))
            {
                return;
            }
            this.EnterAmount(btnPoint.Text.Trim());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAmount.Text = string.Empty;
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtAmount.Text.Contains(".")) && (e.KeyChar == '.'))
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

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (txtTillId.Text.Trim() == string.Empty)
            {
                return;
            }
            frmTillCheckout frmTC = new frmTillCheckout(txtTillId.Text.Trim());
            frmTC.ShowDialog();
            try
            {
                objENTTill = new ENT.TillManage();
                objENTTill.Mode = "GetByID";
                objENTTill.TillID = new Guid(txtTillId.Text);
                lstENTTill = objDALTill.getTillManage(objENTTill);
                if (lstENTTill.Count > 0)
                {
                    if (lstENTTill[0].IsTillDone == true)
                    {
                        btnSet.Enabled = false;
                        btnPayIn.Enabled = false;
                        btnPayOut.Enabled = false;
                        btnCheckout.Enabled = false;
                        //btnCashLog.Enabled = false;
                        pnlNumeric.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Till Manage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPayIn_Click(object sender, EventArgs e)
        {
            frmTillPayIn frmPI = new frmTillPayIn(txtTillId.Text.Trim());
            frmPI.ShowDialog();
        }

        private void btnPayOut_Click(object sender, EventArgs e)
        {
            frmTillPayOut frmPI = new frmTillPayOut(txtTillId.Text.Trim());
            frmPI.ShowDialog();  
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAmount.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Amount should not empty.", "Till Manage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmount.Focus();
                    return;
                }

                if (objDALTill.getDuplicateTillByIsTillDone() > 0 && strMode == "ADD")
                {
                    MessageBox.Show("Please chakc out current till and then set new till.", "Till Manage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmount.Focus();
                    return;
                }
                objENTTill = new ENT.TillManage();
                objENTTill.TillID = new Guid(txtTillId.Text);
                objENTTill.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                objENTTill.StartCash = Convert.ToDecimal(txtAmount.Text);
                objENTTill.StartDateTime = GlobalVariable.ChangeDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                objENTTill.EnrtyDate = GlobalVariable.ChangeDate(DateTime.Now.ToString("dd/MM/yyyy"));
                objENTTill.RUserID = new Guid(GlobalVariable.BranchID);
                if (DAL.BranchSettingDetail.IsFranchise(GlobalVariable.BranchID))
                { objENTTill.RuserType = Convert.ToInt32(ENT.APIStream.R_USER_TYPE.FRANCHISE); }
                else { objENTTill.RuserType = Convert.ToInt32(ENT.APIStream.R_USER_TYPE.BRANCH); }
                objENTTill.IsTillDone = false;
                objENTTill.Mode = strMode;
                if (objDALTill.InsertUpdateDeleteTillManage(objENTTill))
                {
                    MessageBox.Show("Till amount is set successfully.", "Till Manage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    getCurrentTill();
                    txtAmount.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Till Manage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCashLog_Click(object sender, EventArgs e)
        {
            frmTillSummary frmTC = new frmTillSummary(txtTillId.Text.Trim());
            frmTC.ShowDialog();
        }

        private void frmTill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
