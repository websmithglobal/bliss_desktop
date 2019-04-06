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
    public partial class frmUnitTypeMaster : Form
    {
        ENT.IngredientUnitTypeDetail objENTUTM = new ENT.IngredientUnitTypeDetail();
        DAL.IngredientUnitTypeDetail objDALUTM = new DAL.IngredientUnitTypeDetail();
        List<ENT.IngredientUnitTypeDetail> lstENTUTM = new List<ENT.IngredientUnitTypeDetail>();

        public frmUnitTypeMaster()
        {
            InitializeComponent();
        }
        
        private void frmAddCategory_Load(object sender, EventArgs e)
        {
            try
            {
                txtUnitTypeName.Text = "";
                txtUnitTypeName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Unit Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUnitTypeName.Text.Trim()))
                {
                    if (objDALUTM.getDuplicateIngredientUnitTypeByName(txtUnitTypeName.Text.Trim())>0)
                    {
                        MessageBox.Show("Duplicate unit type found. Try another name.", "Unit Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUnitTypeName.Focus();
                        return;
                    }
                    objENTUTM.UnitTypeID = Guid.NewGuid();
                    objENTUTM.UnitType = txtUnitTypeName.Text.Trim();
                    objENTUTM.Qty = 0;
                    objENTUTM.IsUPStream = 0;
                    objENTUTM.IngredientsMasterDetail_Id = 0;
                    objENTUTM.Mode = "ADD";
                    if (objDALUTM.InsertUpdateDeleteIngredientUnitTypeDetail(objENTUTM))
                    {
                        MessageBox.Show("Unit Type Added Successfully.", "Unit Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Problem In Unit Type.", "Unit Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Unit Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

    }
}
