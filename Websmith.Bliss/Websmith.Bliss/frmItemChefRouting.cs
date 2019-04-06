using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.Bliss
{
    public partial class frmItemChefRouting : Form
    {
        public frmItemChefRouting()
        {
            InitializeComponent();
        }

        #region ChefToKDS Mapping

        private void GetSelectedKDSForChef()
        {
            try
            {
                DAL.ChefKDSMapping objDALKDS = new DAL.ChefKDSMapping();
                ENT.ChefKDSMapping objENTKDS = new ENT.ChefKDSMapping();
                List<ENT.ChefKDSMapping> lstENTKDS = new List<ENT.ChefKDSMapping>();

                objENTKDS.Mode = "GetAll";
                lstENTKDS = objDALKDS.GetChefKDSMapping(objENTKDS);

                for (int i = 0; i < lstENTKDS.Count; i++)
                {
                    for (int j = 0; j < dgvChefToKDS.Rows.Count; j++)
                    {
                        if (dgvChefToKDS.Rows[j].Cells["EmployeeID"].Value.ToString() == lstENTKDS[i].EmployeeID.ToString())
                        {
                            (dgvChefToKDS.Rows[j].Cells["DeviceName"] as DataGridViewComboBoxCell).Value = lstENTKDS[i].DeviceID;
                            break;
                        }
                    }                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillGridChefToKDS()
        {
            try
            {
                DAL.EmployeeMasterList objDALEmp = new DAL.EmployeeMasterList();
                ENT.EmployeeMasterList objENTEmp = new ENT.EmployeeMasterList();
                List<ENT.EmployeeMasterList> lstENTEmp = new List<ENT.EmployeeMasterList>();

                objENTEmp.Mode = "GetByIsDisplayInKDS";
                lstENTEmp = objDALEmp.getEmployeeMasterList(objENTEmp);

                dgvChefToKDS.Rows.Clear();
                for (int i = 0; i < lstENTEmp.Count; i++)
                {
                    dgvChefToKDS.Rows.Add();
                    dgvChefToKDS.Rows[i].Cells["EmployeeID"].Value = lstENTEmp[i].EmployeeID;
                    dgvChefToKDS.Rows[i].Cells["EmpName"].Value = lstENTEmp[i].EmpName;
                    (dgvChefToKDS.Rows[i].Cells["DeviceName"] as DataGridViewComboBoxCell).DataSource = addComboInDgvChefToKDS();
                    (dgvChefToKDS.Rows[i].Cells["DeviceName"] as DataGridViewComboBoxCell).ValueMember = "DeviceID";
                    (dgvChefToKDS.Rows[i].Cells["DeviceName"] as DataGridViewComboBoxCell).DisplayMember = "DeviceName";
                    (dgvChefToKDS.Rows[i].Cells["DeviceName"] as DataGridViewComboBoxCell).Value = new Guid("00000000-0000-0000-0000-000000000000");
                }
                GetSelectedKDSForChef();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<ENT.DeviceMaster> addComboInDgvChefToKDS()
        {
            List<ENT.DeviceMaster> lstENTDiv = new List<ENT.DeviceMaster>();
            try
            {
                DAL.DeviceMaster objDALDiv = new DAL.DeviceMaster();
                ENT.DeviceMaster objENTDiv = new ENT.DeviceMaster();

                objENTDiv.Mode = "GetByTypeID";
                objENTDiv.DeviceTypeID = Convert.ToInt32(ENT.DeviceMaster.DeviceTypeName.KDS);
                lstENTDiv = objDALDiv.getDeviceMaster(objENTDiv);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            lstENTDiv.Add(new ENT.DeviceMaster { DeviceID = new Guid("00000000-0000-0000-0000-000000000000"), DeviceName = "-Select-" });
            return lstENTDiv.OrderBy(ent => ent.DeviceID).ToList(); ;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool Result = false;
                DAL.ChefKDSMapping objDALKDS = new DAL.ChefKDSMapping();
                ENT.ChefKDSMapping objENTKDS = new ENT.ChefKDSMapping();
                for (int i = 0; i < dgvChefToKDS.Rows.Count; i++)
                {
                    if (Convert.ToString(dgvChefToKDS.Rows[i].Cells["DeviceName"].Value) != "00000000-0000-0000-0000-000000000000")
                    {
                        if (dgvChefToKDS.Rows[i].Cells["DeviceName"].Value == null)
                        {
                            continue;
                        }
                        objENTKDS.ChefKDSMappingID = Guid.NewGuid();
                        objENTKDS.EmployeeID = new Guid(dgvChefToKDS.Rows[i].Cells["EmployeeID"].Value.ToString());
                        objENTKDS.DeviceID = new Guid(dgvChefToKDS.Rows[i].Cells["DeviceName"].Value.ToString());
                        if (objDALKDS.getDuplicateChefKDSMapping(objENTKDS) > 0)
                        {
                            objENTKDS.Mode = "UPDATE";
                            Result = objDALKDS.InsertUpdateDeleteChefKDSMapping(objENTKDS);
                        }
                        else
                        {
                            objENTKDS.Mode = "ADD";
                            Result = objDALKDS.InsertUpdateDeleteChefKDSMapping(objENTKDS);
                        }
                    }
                    else
                    {
                        objENTKDS.EmployeeID = new Guid(dgvChefToKDS.Rows[i].Cells["EmployeeID"].Value.ToString());
                        objENTKDS.Mode = "DELETE";
                        Result = objDALKDS.InsertUpdateDeleteChefKDSMapping(objENTKDS);
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

        #endregion

        #region ItemToChef Mapping

        private void GetSelectedChefForItem()
        {
            try
            {
                DAL.ItemChefMapping objDALItem = new DAL.ItemChefMapping();
                ENT.ItemChefMapping objENTItem = new ENT.ItemChefMapping();
                List<ENT.ItemChefMapping> lstENTItem = new List<ENT.ItemChefMapping>();

                objENTItem.Mode = "GetAll";
                lstENTItem = objDALItem.GetItemChefMapping(objENTItem);

                for (int i = 0; i < lstENTItem.Count; i++)
                {
                    for (int j = 0; j < dgvItemToChef.Rows.Count; j++)
                    {
                        if (dgvItemToChef.Rows[j].Cells["ProductID"].Value.ToString() == lstENTItem[i].ProductID.ToString() &&
                            dgvItemToChef.Rows[j].Cells["CategoryID"].Value.ToString() == lstENTItem[i].CategoryID.ToString())
                        {
                            (dgvItemToChef.Rows[j].Cells["EmpID"] as DataGridViewComboBoxCell).Value = lstENTItem[i].EmployeeID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillGridItemToChef()
        {
            try
            {
                DAL.CategoryWiseProduct objDALCat = new DAL.CategoryWiseProduct();
                ENT.CategoryWiseProduct objENTCat = new ENT.CategoryWiseProduct();
                List<ENT.CategoryWiseProduct> lstENTCat = new List<ENT.CategoryWiseProduct>();

                objENTCat.Mode = "GetForItemChefMapping";
                lstENTCat = objDALCat.getCategoryWiseProduct(objENTCat);

                List<ENT.EmployeeMasterList> lstENTEmp = addComboInDgvItemToChef();

                dgvItemToChef.Rows.Clear();
                for (int i = 0; i < lstENTCat.Count; i++)
                {
                    dgvItemToChef.Rows.Add();
                    dgvItemToChef.Rows[i].Cells["CategoryName"].Value = lstENTCat[i].CategoryName;
                    dgvItemToChef.Rows[i].Cells["ProdName"].Value = lstENTCat[i].ProductName;
                    dgvItemToChef.Rows[i].Cells["ProductID"].Value = lstENTCat[i].ProductID;
                    dgvItemToChef.Rows[i].Cells["CategoryID"].Value = lstENTCat[i].CategoryID;
                    dgvItemToChef.Rows[i].Cells["AssignChef"].Value = "Select Chef";

                    //(dgvItemToChef.Rows[i].Cells["EmpID"] as DataGridViewComboBoxCell).DataSource = lstENTEmp;
                    //(dgvItemToChef.Rows[i].Cells["EmpID"] as DataGridViewComboBoxCell).ValueMember = "EmployeeID";
                    //(dgvItemToChef.Rows[i].Cells["EmpID"] as DataGridViewComboBoxCell).DisplayMember = "EmpName";
                    //(dgvItemToChef.Rows[i].Cells["EmpID"] as DataGridViewComboBoxCell).Value = new Guid("00000000-0000-0000-0000-000000000000");
                }
                GetSelectedChefForItem();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<ENT.EmployeeMasterList> addComboInDgvItemToChef()
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
            lstENTEmp.Add(new ENT.EmployeeMasterList { EmployeeID = new Guid("00000000-0000-0000-0000-000000000000"), EmpName = "-Select-" });
            return lstENTEmp.OrderBy(ent=> ent.EmployeeID).ToList();
        }

        private void btnSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                bool Result = false;
                DAL.ItemChefMapping objDALICM = new DAL.ItemChefMapping();
                ENT.ItemChefMapping objENTICM = new ENT.ItemChefMapping();
                for (int i = 0; i < dgvItemToChef.Rows.Count; i++)
                {
                    if (Convert.ToString(dgvItemToChef.Rows[i].Cells["EmpID"].Value) != "00000000-0000-0000-0000-000000000000")
                    {
                        if (dgvItemToChef.Rows[i].Cells["EmpID"].Value == null)
                        {
                            continue;
                        }
                        objENTICM.ItemChefMappingID = Guid.NewGuid();
                        objENTICM.CategoryID = new Guid(dgvItemToChef.Rows[i].Cells["CategoryID"].Value.ToString());
                        objENTICM.ProductID = new Guid(dgvItemToChef.Rows[i].Cells["ProductID"].Value.ToString());
                        objENTICM.EmployeeID = new Guid(dgvItemToChef.Rows[i].Cells["EmpID"].Value.ToString());

                        if (objDALICM.getDuplicateItemChefMapping(objENTICM) > 0)
                        {
                            objENTICM.Mode = "UPDATE";
                            Result = objDALICM.InsertUpdateDeleteItemChefMapping(objENTICM);
                        }
                        else
                        {
                            objENTICM.Mode = "ADD";
                            Result = objDALICM.InsertUpdateDeleteItemChefMapping(objENTICM);
                        }
                    }
                    else
                    {
                        objENTICM.CategoryID = new Guid(dgvItemToChef.Rows[i].Cells["CategoryID"].Value.ToString());
                        objENTICM.ProductID = new Guid(dgvItemToChef.Rows[i].Cells["ProductID"].Value.ToString());
                        objENTICM.Mode = "DELETE";
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

        #endregion
        
        private void frmItemChefRouting_Load(object sender, EventArgs e)
        {
            try
            {
                FillGridChefToKDS();
                FillGridItemToChef();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }              
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmItemChefRouting_KeyDown(object sender, KeyEventArgs e)
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

        private void dgvItemToChef_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0)
                {
                    return;
                }
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (dgvItemToChef.Rows.Count > 0)
                {
                    if (dgvItemToChef.Columns[e.ColumnIndex].Name == "AssignChef")
                    {
                        string CategoryID = dgvItemToChef.Rows[e.RowIndex].Cells["CategoryID"].Value.ToString();
                        string ProductID = dgvItemToChef.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
                        string CategoryName = dgvItemToChef.Rows[e.RowIndex].Cells["CategoryName"].Value.ToString();
                        string ProdName = dgvItemToChef.Rows[e.RowIndex].Cells["ProdName"].Value.ToString();
                        frmSelectChefKitchenRouting frmRouting = new frmSelectChefKitchenRouting(CategoryID, ProductID, CategoryName.Trim(), ProdName.Trim());
                        frmRouting.ShowDialog();
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
