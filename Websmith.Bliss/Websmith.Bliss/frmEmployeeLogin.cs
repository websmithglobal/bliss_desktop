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
    public partial class frmEmployeeLogin : Form
    {
        string BranchID = string.Empty;
        List<ENT.EmployeeMasterList> lstEmpMaster = new List<ENT.EmployeeMasterList>();
        ENT.EmployeeMasterList objEmpMaster = new ENT.EmployeeMasterList();

        public frmEmployeeLogin()
        {
            InitializeComponent();
        }

        public frmEmployeeLogin(ENT.LoginResponse objRO)
        {
            InitializeComponent();
            lstEmpMaster = objRO.EmployeeMasterList;
        }

        private void GetBranchMasterSetting()
        {
            try
            {
                ENT.BranchMasterSetting objENTBMS = new ENT.BranchMasterSetting();
                List<ENT.BranchMasterSetting> lstENTBMS = new List<ENT.BranchMasterSetting>();
                DAL.BranchMasterSetting objDALBMS = new DAL.BranchMasterSetting();

                objENTBMS.Mode = "GetAllRecord";
                lstENTBMS = objDALBMS.getBranchMasterSetting(objENTBMS);
                if (lstENTBMS.Count > 0)
                {
                    GlobalVariable.BranchID = lstENTBMS[0].BranchID.ToString();
                    GlobalVariable.BranchName = lstENTBMS[0].BranchName;
                    GlobalVariable.BranchAddress = lstENTBMS[0].Address;
                    if (DAL.BranchSettingDetail.IsFranchise(GlobalVariable.BranchID))
                    { GlobalVariable.RUserType = Convert.ToInt32(ENT.APIStream.R_USER_TYPE.FRANCHISE); }
                    else { GlobalVariable.RUserType = Convert.ToInt32(ENT.APIStream.R_USER_TYPE.BRANCH); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string sysDate = DateTime.Now.ToString("dd/MMM/yyyy");
                string expiryDate = "31/JAN/2020";
                DateTime dd = Convert.ToDateTime(sysDate);
                DateTime dd1 = Convert.ToDateTime(expiryDate);
                if (dd >= dd1)
                {
                    MessageBox.Show("Your subscription is expire, please contact administrator for renew subscription.", "Expiry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblExpiryMessage.Text = "Your subscription is expire, please contact administrator for renew subscription.";
                    return;
                }

                if (txtCode.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Please Enter Code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Focus();
                    return;
                }
                if (txtCode.Text.Trim().Length != 4)
                {
                    MessageBox.Show("Please Enter Four Digit Code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Focus();
                    return;
                }
                GlobalVariable.objEmployeeMasterList = null;
                ENT.EmployeeMasterList objEntEmp = new ENT.EmployeeMasterList();
                List<ENT.EmployeeMasterList> lstEmp = new List<ENT.EmployeeMasterList>();
                DAL.EmployeeMasterList objDALEmp = new DAL.EmployeeMasterList();

                objEntEmp.Mode = "GetRecordByEmpCode";
                objEntEmp.EmpCode = txtCode.Text.Trim();
                lstEmp = objDALEmp.getEmployeeMasterList(objEntEmp);
                //lstEmp = lstEmpMaster.Where(emp => emp.EmpCode.Equals(txtCode.Text.Trim())).ToList();
                if (lstEmp.Count > 0)
                {
                    this.GetBranchMasterSetting();
                    GlobalVariable.objEmployeeMasterList = lstEmp[0];
                    GlobalVariable.EmployeeID = lstEmp[0].EmployeeID.ToString();
                    GlobalVariable.EmployeeCode = lstEmp[0].EmpCode.ToString();
                    GlobalVariable.EmployeeName = lstEmp[0].EmpName.ToString();
                    GlobalVariable.ClassID = lstEmp[0].ClassID.ToString();

                    frmOrderBook frmMDI = new frmOrderBook();
                    frmMDI.FormClosed += new FormClosedEventHandler(EmployeeLogin_FromClosed);
                    frmMDI.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Code Is Incorrect.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Text = string.Empty;
                    txtCode.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Numeric Pad Button

        private void EnterCode(string code)
        {
            if (txtCode.Text.Length < 4)
            {
                txtCode.Text += code.Trim();
            }
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            this.EnterCode(btnOne.Text.Trim());
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            this.EnterCode(btnTwo.Text.Trim());
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            this.EnterCode(btnThree.Text.Trim());
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            this.EnterCode(btnFour.Text.Trim());
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            this.EnterCode(btnFive.Text.Trim());
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            this.EnterCode(btnSix.Text.Trim());
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            this.EnterCode(btnSeven.Text.Trim());
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            this.EnterCode(btnEight.Text.Trim());
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            this.EnterCode(btnNine.Text.Trim());
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            this.EnterCode(btnZero.Text.Trim());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCode.Text = string.Empty;
        }

        #endregion

        private void EmployeeLogin_FromClosed(object obj, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmBranchLogin frmBL = new frmBranchLogin();
            frmBL.FormClosed += new FormClosedEventHandler(BranchLogin_FormClosed);
            frmBL.Show();
            this.Hide();
        }

        private void BranchLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void frmEmployeeLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { SendKeys.Send("{Tab}"); }
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtCode.Text.Contains(".")) && (e.KeyChar == '.'))
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

        private void frmEmployeeLogin_Load(object sender, EventArgs e)
        {
            try
            {
                string sysDate = DateTime.Now.ToString("dd/MMM/yyyy");
                string expiryDate = "31/JAN/2020";  //"31/DEC/2019"; For Bigbite
                DateTime dd = Convert.ToDateTime(sysDate);
                DateTime dd1 = Convert.ToDateTime(expiryDate);
                if (dd >= dd1)
                {
                    MessageBox.Show("Your subscription is over, please contact administrator for renew subscription.", "Expiry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblExpiryMessage.Text = "Your subscription is over, please contact administrator for renew subscription.";
                }

                if (CheckActivationOrDemo())
                {
                    btnBack.Enabled = true;
                    btnLogin.Visible = true;
                    btnRegister.Visible = false;
                }
                else
                {
                    btnBack.Enabled = true;
                    btnLogin.Visible = false;
                    btnRegister.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckActivationOrDemo()
        {
            bool IsActivated = false;
            lblExpiryMessage.Text = "";
            lblDemoCode.Text = "";
            try
            {
                lblDemoCode.Visible = false;
                ENT.BranchSettingDetail objENT = new ENT.BranchSettingDetail();
                List<ENT.BranchSettingDetail> lstENT = new List<ENT.BranchSettingDetail>();
                objENT.Mode = "GetAllRecord";
                lstENT = new DAL.BranchSettingDetail().getBranchSettingDetail(objENT);
                if (lstENT.Count > 0)
                {
                    BranchID = lstENT[0].BranchID.ToString();
                    // check is demo version or not
                    if (!lstENT[0].IsDemoVersion)
                    {
                        // check is activated or not
                        if (string.IsNullOrEmpty(lstENT[0].DemoCode))
                        {
                            // this code for not activated software
                            string strCode = new DAL.SecurityManager().GetSerial().ToString();
                            MessageBox.Show("Your Demo Version Is Expire. Your Licence Code Is : " + strCode, "Expiry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            lblDemoCode.Text = "Your Demo Version Is Expire. Your Licence Code Is : " + strCode;
                            lblDemoCode.Visible = true;
                            IsActivated = false;
                        }
                        else
                        {
                            // this code for activated software but expire activation or not
                            string SerialNo = DAL.SecurityManager.Base64Decode(DAL.SecurityManager.Base64Decode(lstENT[0].DemoCode));
                            long strCode = Convert.ToInt64(SerialNo);
                            if (!new DAL.SecurityManager().CheckKey(strCode))
                            {
                                string strSerial = new DAL.SecurityManager().GetSerial().ToString();
                                MessageBox.Show("Your Software Licence Is Expire. Your Licence Code Is : " + strCode, "Expiry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                lblDemoCode.Text = "Your Software Licence Is Expire. Your Licence Code Is : " + strCode;
                                lblDemoCode.Visible = true;
                                IsActivated = false;
                            }
                            else
                            {
                                lblDemoCode.Visible = false;
                                IsActivated = true;
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(lstENT[0].ExpireDate))
                        {
                            DateTime dtSysDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                            string strExpDate = DAL.SecurityManager.Base64Decode(DAL.SecurityManager.Base64Decode(lstENT[0].ExpireDate));
                            DateTime dtExpDate = new DateTime(Convert.ToInt32(strExpDate.Substring(6, 4)), Convert.ToInt32(strExpDate.Substring(3, 2)), Convert.ToInt32(strExpDate.Substring(0, 2)));  // 14/12/2018
                            if (dtSysDate >= dtExpDate)
                            {
                                MessageBox.Show("Your subscription is over, please contact administrator for renew subscription.", "Expiry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                lblExpiryMessage.Text = "Your subscription is over, please contact administrator for renew subscription.";
                                IsActivated = false;
                            }
                            else
                            {
                                IsActivated = true;
                            }
                        }
                        else
                        {
                            IsActivated = false;
                        }
                    }
                }
                else
                {
                    IsActivated = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsActivated = false;
            }
            return IsActivated;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BranchID))
            {
                frmActivation frmA = new frmActivation(BranchID);
                frmA.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please check branch setting detail because branch not found.");
                return;
            }

            if (CheckActivationOrDemo())
            {
                btnBack.Enabled = true;
                btnLogin.Visible = true;
                btnRegister.Visible = false;
            }
            else
            {
                btnBack.Enabled = true;
                btnLogin.Visible = false;
                btnRegister.Visible = true;
            }
        }
    }
}
