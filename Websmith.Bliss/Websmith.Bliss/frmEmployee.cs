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
    public partial class frmEmployee : Form
    {
        string strMode = "";
        DAL.EmployeeMasterList objDALEmployee = new DAL.EmployeeMasterList();
        ENT.EmployeeMasterList objENTEmployee = new ENT.EmployeeMasterList();
        List<ENT.EmployeeMasterList> lstENTEmployee = new List<ENT.EmployeeMasterList>();

        public frmEmployee()
        {
            InitializeComponent();
        }

        private void fillComboRole()
        {
            try
            {
                ENT.RoleMasterDetail objENTRole = new ENT.RoleMasterDetail();
                List<ENT.RoleMasterDetail> lstENTRole = new List<ENT.RoleMasterDetail>();
                DAL.RoleMasterDetail objDALRole = new DAL.RoleMasterDetail();
                BindingSource bs = new BindingSource();
                objENTRole.Mode = "GetAllRecord";
                lstENTRole = objDALRole.getRoleMasterDetail(objENTRole);
                bs.DataSource = lstENTRole;
                cmbRole.DataSource = bs;
                cmbRole.DisplayMember = "RoleName";
                cmbRole.ValueMember = "RoleID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fillComboShift()
        {
            try
            {
                ENT.ShiftMaster objENTShift = new ENT.ShiftMaster();
                List<ENT.ShiftMaster> lstENTShift = new List<ENT.ShiftMaster>();
                DAL.ShiftMaster objDALShift = new DAL.ShiftMaster();
                BindingSource bs = new BindingSource();
                objENTShift.Mode = "GetAllRecord";
                lstENTShift = objDALShift.getShiftMaster(objENTShift);
                bs.DataSource = lstENTShift;
                cmbShift.DataSource = bs;
                cmbShift.DisplayMember = "ShiftName";
                cmbShift.ValueMember = "ShiftID";
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
                btnUpdate.Enabled = true;
                txtEmpID.Text = string.Empty;
                txtEmpName.Text = string.Empty;
                txtMobileNo.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtCode.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtSalary.Text = string.Empty;
                txtJoinDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                chkKDS.Checked = false;
                rdoMale.Checked = true;
                cmbSalaryType.SelectedIndex = 0;
                this.fillComboRole();
                this.fillComboShift();
                txtSearch.Text = string.Empty;
                this.getEmployeeMasterList(string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveData()
        {
            try
            {
                Control[] ctrl = { cmbRole, txtEmpName, txtMobileNo, txtCode, cmbSalaryType, cmbShift};
                if (GlobalVariable.CheckValidate(ctrl))
                {
                    

                    if (strMode == "ADD")
                    {
                        if (objDALEmployee.getDuplicateEmployeeByCode(txtCode.Text.Trim()) > 0)
                        {
                            MessageBox.Show("Duplicate employee code found. Please try another code.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCode.Focus();
                            return;
                        }
                        objENTEmployee.EmployeeID = Guid.NewGuid();
                        objENTEmployee.ClassID = new Guid(GlobalVariable.ClassID);
                    }
                    else if (strMode == "UPDATE")
                    {
                        if (objDALEmployee.getDuplicateEmployeeByIDCode(txtCode.Text.Trim(), txtEmpID.Text.Trim()) > 0)
                        {
                            MessageBox.Show("Duplicate employee code found. Please try another code.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCode.Focus();
                            return;
                        }
                        objENTEmployee.EmployeeID = new Guid(txtEmpID.Text.Trim());
                    }
                    objENTEmployee.RoleID = new Guid(cmbRole.SelectedValue.ToString());
                    objENTEmployee.RoleName = Convert.ToString(cmbRole.Text);
                    if (rdoMale.Checked) { objENTEmployee.Gender = 1; } else if (rdoFemale.Checked) { objENTEmployee.Gender = 2; } else { objENTEmployee.Gender = 3; }
                    objENTEmployee.EmpName = txtEmpName.Text.Trim();
                    objENTEmployee.Mobile = txtMobileNo.Text.Trim();
                    objENTEmployee.Email = txtEmail.Text.Trim();
                    objENTEmployee.EmpCode = txtCode.Text.Trim();
                    objENTEmployee.Password = txtCode.Text.Trim();
                    objENTEmployee.Address = txtAddress.Text.Trim();
                    objENTEmployee.SalaryAmt = Convert.ToDecimal(txtSalary.Text);
                    objENTEmployee.SalaryType = Convert.ToInt32(cmbSalaryType.SelectedIndex + 1);
                    objENTEmployee.ShiftID = new Guid(cmbShift.SelectedValue.ToString());
                    objENTEmployee.JoinDate = GlobalVariable.ChangeDate(txtJoinDate.Text.ToString());
                    objENTEmployee.IsDisplayInKDS = chkKDS.Checked == true ? 1 : 0;
                    objENTEmployee.RUserID = new Guid(GlobalVariable.BranchID);
                    objENTEmployee.RUserType = GlobalVariable.RUserType;
                  
                    objENTEmployee.Mode = strMode;
                    if (objDALEmployee.InsertUpdateDeleteEmployee(objENTEmployee))
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
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getEmployeeMasterList(string EmployeeName)
        {
            try
            {
                dgvEmployee.Rows.Clear();
                objENTEmployee.Mode = "GetAllRecord";
                lstENTEmployee = objDALEmployee.getEmployeeMasterList(objENTEmployee);
                lstENTEmployee = lstENTEmployee.Where(emp => emp.EmpName.ToLower().StartsWith(EmployeeName.ToLower())).ToList();

                for (int i = 0; i < lstENTEmployee.Count; i++)
                {
                    dgvEmployee.Rows.Add();
                    dgvEmployee.Rows[i].Cells["EmpID"].Value = lstENTEmployee[i].EmployeeID;
                    dgvEmployee.Rows[i].Cells["EmpCode"].Value = lstENTEmployee[i].EmpCode;
                    dgvEmployee.Rows[i].Cells["EmpName"].Value = lstENTEmployee[i].EmpName;
                    dgvEmployee.Rows[i].Cells["EmpMobile"].Value = lstENTEmployee[i].Mobile;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            this.clearData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEmployee.Rows.Count <= 0)
                {
                    MessageBox.Show("No Record For Update", "Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int row = dgvEmployee.CurrentRow.Index;
                txtEmpID.Text = dgvEmployee.Rows[row].Cells["EmpID"].Value.ToString();
                if (txtEmpID.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Select Valid Employee.", "Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvEmployee.Focus();
                    return;
                }
                objENTEmployee.Mode = "GetRecordByID";
                objENTEmployee.EmployeeID = new Guid(txtEmpID.Text);
                lstENTEmployee = objDALEmployee.getEmployeeMasterList(objENTEmployee);
                if (lstENTEmployee.Count > 0)
                {
                    txtEmpID.Text = lstENTEmployee[0].EmployeeID.ToString();
                    cmbRole.SelectedValue = lstENTEmployee[0].RoleID;
                    txtEmpName.Text = lstENTEmployee[0].EmpName.ToString();
                    if (Convert.ToInt32(lstENTEmployee[0].Gender) == 1)
                        rdoMale.Checked = true;
                    else if (Convert.ToInt32(lstENTEmployee[0].Gender) == 2)
                        rdoFemale.Checked = true;
                    else
                        rdoOther.Checked = true;
                    txtMobileNo.Text = lstENTEmployee[0].Mobile;
                    txtEmail.Text = lstENTEmployee[0].Email;
                    txtCode.Text = lstENTEmployee[0].EmpCode;
                    txtSalary.Text = Convert.ToString(lstENTEmployee[0].SalaryAmt);
                    cmbSalaryType.SelectedIndex = lstENTEmployee[0].SalaryType - 1;
                    cmbShift.SelectedValue = lstENTEmployee[0].ShiftID;
                    txtAddress.Text = Convert.ToString(lstENTEmployee[0].Address);
                    txtJoinDate.Text = lstENTEmployee[0].JoinDate;
                    if (Convert.ToInt32(lstENTEmployee[0].IsDisplayInKDS) == 1)
                        chkKDS.Checked = true;
                    else if (Convert.ToInt32(lstENTEmployee[0].Gender) == 0)
                        chkKDS.Checked = false;
                    btnUpdate.Enabled = false;
                    strMode = "UPDATE";
                }
                else
                {
                    MessageBox.Show("Record Not Found.", "Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Employee", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {
                if (dgvEmployee.Rows.Count <= 0)
                {
                    MessageBox.Show("No Record For Delete", "Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult dlgResult = MessageBox.Show("You want to delete seleted customer ?", "Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    int row = dgvEmployee.CurrentRow.Index;
                    txtEmpID.Text = dgvEmployee.Rows[row].Cells["EmpID"].Value.ToString();
                    if (txtEmpID.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Select Valid Employee.", "Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvEmployee.Focus();
                        return;
                    }

                    objENTEmployee.Mode = "DELETE";
                    objENTEmployee.EmployeeID = new Guid(txtEmpID.Text.Trim());
                    result = objDALEmployee.InsertUpdateDeleteEmployee(objENTEmployee);
                    if (result)
                    {
                        MessageBox.Show("Data Deleted Successfully.", "Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.clearData();
                    }
                    else
                    {
                        MessageBox.Show("Record Not Delete.", "Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.getEmployeeMasterList(txtSearch.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
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
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
