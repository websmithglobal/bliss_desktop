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
    public partial class frmAddProduct : Form
    {
        ENT.CategoryWiseProduct objENTProd = new ENT.CategoryWiseProduct();
        DAL.CategoryWiseProduct objDALProd = new DAL.CategoryWiseProduct();
        List<ENT.CategoryWiseProduct> lstENTProd = new List<ENT.CategoryWiseProduct>();

        public frmAddProduct()
        {
            InitializeComponent();
        }

        public frmAddProduct(string CategoryID)
        {
            InitializeComponent();
            txtCategoryID.Text = CategoryID;
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            try
            {
                txtProductName.Text = "";
                txtPrice.Text = "";
                txtDescr.Text = "";
                txtCode.Text = "";
                chkIsDrink.Checked = false;
                txtProductName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Add Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductName.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("Product name should not empty.", "Add Product",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProductName.Focus();
                    return;
                }
                if (txtPrice.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("Price should not empty.", "Add Product",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                    return;
                }
                objENTProd.DiscountID = new Guid("00000000-0000-0000-0000-000000000000");
                objENTProd.ProductID = Guid.NewGuid();
                objENTProd.CategoryID = new Guid(txtCategoryID.Text);
                objENTProd.ProductName = txtProductName.Text.Trim();
                objENTProd.Price = Convert.ToDecimal(txtPrice.Text);
                objENTProd.ShortDescription = txtDescr.Text.Trim();
                objENTProd.ProductCode = txtCode.Text.Trim();
                objENTProd.IsDrink = chkIsDrink.Checked;
                objENTProd.ImgPath = "";
                objENTProd.Priority = 0;
                objENTProd.Sort = 0;
                objENTProd.TaxPercentage = 0;
                objENTProd.RUserID = new Guid(GlobalVariable.BranchID);
                objENTProd.RUserType = GlobalVariable.RUserType;
                objENTProd.Mode = "ADD";
                if (objDALProd.InsertUpdateDeleteProduct(objENTProd))
                {
                    MessageBox.Show("Product Added Successfully.", "Add Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Problem In Add Product.", "Add Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Add Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtPrice.Text.Contains(".")) && (e.KeyChar == '.'))
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

        private void frmAddProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
