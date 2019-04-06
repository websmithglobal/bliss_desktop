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
    public partial class frmSelectChefKitchenRouting : Form
    {
        public frmSelectChefKitchenRouting()
        {
            InitializeComponent();
        }

        public frmSelectChefKitchenRouting(string CateID, string ProdID, string CateName, string ProdName)
        {
            InitializeComponent();
            txtCategoryID.Text = CateID;
            txtProductID.Text = ProdID;
            txtCategoryName.Text = CateName;
            txtProductName.Text = ProdName;
        }

        private List<ENT.EmployeeMasterList> GetChef()
        {
            List<ENT.EmployeeMasterList> lstENTEmp = new List<ENT.EmployeeMasterList>();
            try
            {
                DAL.EmployeeMasterList objDALEmp = new DAL.EmployeeMasterList();
                ENT.EmployeeMasterList objENTEmp = new ENT.EmployeeMasterList();

                objENTEmp.Mode = "GetByIsDisplayInKDS";
                lstENTEmp = objDALEmp.getEmployeeMasterList(objENTEmp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lstENTEmp;
        }

        private void FillGridStaff()
        {
            try
            {
                List<ENT.EmployeeMasterList> lstChef = GetChef();
                for (int i = 0; i < lstChef.Count; i++)
                {
                    dgvChef.Rows.Add();
                    dgvChef.Rows[i].Cells["StaffName"].Value = lstChef[i].EmpName;
                    dgvChef.Rows[i].Cells["Shift"].Value = lstChef[i].EmployeeShift;
                    dgvChef.Rows[i].Cells["StaffID"].Value = lstChef[i].EmployeeID;
                    dgvChef.Rows[i].Cells["ShiftID"].Value = lstChef[i].ShiftID;
                    dgvChef.Rows[i].Cells["SelectChef"].Value = false;
                }
                this.GetSelectedChefForItem();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetSelectedChefForItem()
        {
            try
            {
                DAL.ItemChefMapping objDALItem = new DAL.ItemChefMapping();
                ENT.ItemChefMapping objENTItem = new ENT.ItemChefMapping();
                List<ENT.ItemChefMapping> lstENTItem = new List<ENT.ItemChefMapping>();

                objENTItem.Mode = "GetByCategoryIDProductID";
                objENTItem.CategoryID = new Guid(txtCategoryID.Text);
                objENTItem.ProductID = new Guid(txtProductID.Text);
                lstENTItem = objDALItem.GetItemChefMapping(objENTItem);

                for (int i = 0; i < lstENTItem.Count; i++)
                {
                    for (int j = 0; j < dgvChef.Rows.Count; j++)
                    {
                        if (dgvChef.Rows[j].Cells["StaffID"].Value.ToString() == lstENTItem[i].EmployeeID.ToString())
                        {
                            dgvChef.Rows[j].Cells["SelectChef"].Value = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSelectChefKitchenRouting_Load(object sender, EventArgs e)
        {
            FillGridStaff();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool Result = false;
                DAL.ItemChefMapping objDALICM = new DAL.ItemChefMapping();
                ENT.ItemChefMapping objENTICM = new ENT.ItemChefMapping();

                objENTICM.CategoryID = new Guid(txtCategoryID.Text);
                objENTICM.ProductID = new Guid(txtProductID.Text);
                objENTICM.Mode = "DELETE";
                Result = objDALICM.InsertUpdateDeleteItemChefMapping(objENTICM);

                for (int i = 0; i < dgvChef.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvChef.Rows[i].Cells["SelectChef"].Value) == true)
                    {
                        objENTICM.ItemChefMappingID = Guid.NewGuid();
                        objENTICM.CategoryID = new Guid(txtCategoryID.Text);
                        objENTICM.ProductID = new Guid(txtProductID.Text);
                        objENTICM.EmployeeID = new Guid(dgvChef.Rows[i].Cells["StaffID"].Value.ToString());
                        objENTICM.Mode = "ADD";
                        Result = objDALICM.InsertUpdateDeleteItemChefMapping(objENTICM);
                    }
                }
                if (Result)
                {
                    MessageBox.Show("Saved Successfully.", "Routing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
