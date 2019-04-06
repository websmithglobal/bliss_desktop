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
    public partial class frmTillPayOut : Form
    {
        ENT.TillPayOut objENTPayOut;
        //List<ENT.TillPayOut> lstENTPayOut;
        DAL.TillPayOut objDALPayOut = new DAL.TillPayOut();
        string strMode = "";

        public frmTillPayOut()
        {
            InitializeComponent();
        }

        public frmTillPayOut(string TillID)
        {
            InitializeComponent();
            txtTillID.Text = TillID;
        }

        public void ClearData()
        {
            strMode = "ADD";
            txtReason.Text = "";
            txtPayOutID.Text = Guid.NewGuid().ToString();
            txtAmount.Text = "";
            txtAmount.Focus();
        }

        private void EnterAmount(string code)
        {
            if (txtAmount.Text.Length < 15)
            {
                txtAmount.Text += code.Trim();
            }
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            this.EnterAmount(btn.Text.Trim());
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
            txtAmount.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTillPayOut_Load(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAmount.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Pay In amount saved successfully.", "Till Pay In", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAmount.Focus();
                    return;
                }

                objENTPayOut = new Entity.TillPayOut();
                objENTPayOut.PayOutID = new Guid(txtPayOutID.Text);
                objENTPayOut.TillID = new Guid(txtTillID.Text);
                objENTPayOut.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                objENTPayOut.Amount = Convert.ToDecimal(txtAmount.Text);
                objENTPayOut.Reason = txtReason.Text.Trim();
                objENTPayOut.EntryDateTime = GlobalVariable.ChangeDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                objENTPayOut.Mode = strMode;
                if (objDALPayOut.InsertUpdateDeleteTillPayOut(objENTPayOut))
                {
                    MessageBox.Show("Pay In amount saved successfully.", "Till Pay In", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Pay In", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmTillPayOut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
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
    }
}
