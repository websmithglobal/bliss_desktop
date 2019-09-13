using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.Bliss
{
    public partial class frmStaffList : Form
    {
        DAL.EmployeeMasterList objDALEmployee = new DAL.EmployeeMasterList();
        ENT.EmployeeMasterList objENTEmployee = new ENT.EmployeeMasterList();
        List<ENT.EmployeeMasterList> lstENTEmployee = new List<ENT.EmployeeMasterList>();

        public frmStaffList()
        {
            InitializeComponent();
        }
        
        private void getEmployeeMasterList()
        {
            try
            {
                dgvEmployee.Rows.Clear();
                objENTEmployee.Mode = "GetStaffList";
                lstENTEmployee = objDALEmployee.getEmployeeMasterList(objENTEmployee);
                if (txtSearch.Text.Trim().Length > 0)
                {
                    if (!GlobalVariable.IsNumeric(txtSearch.Text.Trim()))
                        lstENTEmployee = lstENTEmployee.Where(cust => cust.EmpName.ToLower().StartsWith(txtSearch.Text.ToLower())).ToList();
                    else
                        lstENTEmployee = lstENTEmployee.Where(cust => cust.Mobile.ToString().StartsWith(txtSearch.Text.ToLower())).ToList();
                }
                
                for (int i = 0; i < lstENTEmployee.Count; i++)
                {
                    dgvEmployee.Rows.Add();
                    dgvEmployee.Rows[i].Cells["EmpID"].Value = lstENTEmployee[i].EmployeeID;
                    dgvEmployee.Rows[i].Cells["EmpCode"].Value = lstENTEmployee[i].EmpCode;
                    dgvEmployee.Rows[i].Cells["EmpName"].Value = lstENTEmployee[i].EmpName;
                    dgvEmployee.Rows[i].Cells["EmpMobile"].Value = lstENTEmployee[i].Mobile;
                    dgvEmployee.Rows[i].Cells["EmpEmail"].Value = lstENTEmployee[i].Email;
                    dgvEmployee.Rows[i].Cells["EmpRoleName"].Value = lstENTEmployee[i].RoleName;
                    dgvEmployee.Rows[i].Cells["EmpSalaryAmt"].Value = lstENTEmployee[i].SalaryAmt;
                    dgvEmployee.Rows[i].Cells["EmpSalaryTypeName"].Value = lstENTEmployee[i].SalaryTypeName;
                    DateTime dt;
                    //if (GlobalVariable.IsDate(lstENTEmployee[i].JoinDate))
                    //{
                    //    dt = Convert.ToDateTime(lstENTEmployee[i].JoinDate);
                    //    dgvEmployee.Rows[i].Cells["EmpJoinDate"].Value = dt.ToString("dd/MM/yyyy");
                    //}
                    //else
                    //{
                    //    dgvEmployee.Rows[i].Cells["EmpJoinDate"].Value = lstENTEmployee[i].JoinDate;
                    //}
                    dgvEmployee.Rows[i].Cells["EmpJoinDate"].Value = Convert.ToDateTime(lstENTEmployee[i].JoinDate);
                    dgvEmployee.Rows[i].Cells["EmpIsDisplayInKDS"].Value =Convert.ToBoolean(lstENTEmployee[i].IsDisplayInKDS);
                    dgvEmployee.Rows[i].Cells["EmpGenderName"].Value = lstENTEmployee[i].GenderName;
                    dgvEmployee.Rows[i].Cells["EmpTotalHourInADay"].Value = lstENTEmployee[i].TotalHourInADay;
                    dgvEmployee.Rows[i].Cells["EmpAddress"].Value = lstENTEmployee[i].Address;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Staff List", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmStaffList_Load(object sender, EventArgs e)
        {
            try
            {
                getEmployeeMasterList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Staff List", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                getEmployeeMasterList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Staff List", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Staff List", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedIndex == 0)
            {
                lblSearch.Visible = true;
                txtSearch.Visible = true;
                btnClear.Visible = true;
            }
            else
            {
                lblSearch.Visible = false;
                txtSearch.Visible = false;
                btnClear.Visible = false;
            }
        }

        private void frmStaffList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
