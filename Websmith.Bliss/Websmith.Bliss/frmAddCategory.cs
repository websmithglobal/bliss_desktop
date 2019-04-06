using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.Bliss
{
    public partial class frmAddCategory : Form
    {
        ENT.CategoryMaster objENTCat = new ENT.CategoryMaster();
        DAL.CategoryMaster objDALCat = new DAL.CategoryMaster();
        List<ENT.CategoryMaster> lstENTCat = new List<ENT.CategoryMaster>();

        public frmAddCategory()
        {
            InitializeComponent();
        }

        public frmAddCategory(string CategoryParentID)
        {
            InitializeComponent();
            txtParentID.Text = CategoryParentID;
        }

        private void frmAddCategory_Load(object sender, EventArgs e)
        {
            try
            {
                txtCategoryName.Text = "";
                txtCategoryName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Add Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryName.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("Category name should not empty.", "Add Category", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCategoryName.Focus();
                    return;
                }

                objENTCat.ParentID = new Guid(txtParentID.Text);
                objENTCat.Mode = "GetCategoryByParentID";
                lstENTCat = objDALCat.getCategoryMaster(objENTCat);
                
                if (lstENTCat.Count > 0)
                {
                    if (objDALCat.getDuplicateCategoryByName(txtCategoryName.Text.Trim(), lstENTCat[0].IsCategory) > 0)
                    {
                        MessageBox.Show("Duplicate category name found.", "Add Category",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCategoryName.Focus();
                        return;
                    }

                    objENTCat.CategoryID = Guid.NewGuid();
                    objENTCat.ParentID = new Guid(txtParentID.Text);
                    objENTCat.CategoryName = txtCategoryName.Text.Trim();
                    objENTCat.ClassMasterID = lstENTCat[0].ClassMasterID;
                    objENTCat.ImgPath = "";
                    objENTCat.Priority = 0;
                    objENTCat.IsCategory = lstENTCat[0].IsCategory;
                    objENTCat.RUserID = new Guid(GlobalVariable.BranchID);
                    objENTCat.RUserType = GlobalVariable.RUserType;
                    objENTCat.Mode = "ADD";
                    if (objDALCat.InsertUpdateDeleteCategoryMaster(objENTCat))
                    {
                        MessageBox.Show("Category Added Successfully.", "Add Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Problem In Add Category.", "Add Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    objENTCat.CategoryID = new Guid(txtParentID.Text);
                    objENTCat.Mode = "GetRecordByCategoryID";
                    lstENTCat = objDALCat.getCategoryMaster(objENTCat);

                    if (objDALCat.getDuplicateCategoryByName(txtCategoryName.Text.Trim(), 2) > 0)
                    {
                        MessageBox.Show("Duplicate category name found.", "Add Category",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCategoryName.Focus();
                        return;
                    }

                    if (lstENTCat.Count > 0)
                    {
                        objENTCat.CategoryID = Guid.NewGuid();
                        objENTCat.ParentID = new Guid(txtParentID.Text);
                        objENTCat.CategoryName = txtCategoryName.Text.Trim();
                        objENTCat.ClassMasterID = lstENTCat[0].ClassMasterID;
                        objENTCat.ImgPath = "";
                        objENTCat.Priority = 0;
                        objENTCat.IsCategory = 2;
                        objENTCat.RUserID = new Guid(GlobalVariable.BranchID);
                        objENTCat.RUserType = GlobalVariable.RUserType;
                        objENTCat.Mode = "ADD";
                        if (objDALCat.InsertUpdateDeleteCategoryMaster(objENTCat))
                        {
                            MessageBox.Show("Category Added Successfully.", "Add Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Problem In Add Category.", "Add Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Add Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

    }
}
