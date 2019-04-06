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
    public partial class frmTillSummary : Form
    {
        ENT.TillManage objENTTill;
        DAL.TillManage objDALTill = new DAL.TillManage();
        List<ENT.TillManage> lstENTTill = new List<ENT.TillManage>();

        public frmTillSummary()
        {
            InitializeComponent();
        }

        public frmTillSummary(string strTillId)
        {
            InitializeComponent();
            txtTillId.Text = strTillId;
        }

        private void GetTillSummary()
        {
            try
            {
                objENTTill = new ENT.TillManage();
                objENTTill.TillID = new Guid(txtTillId.Text);
                objENTTill.Mode = "GetByID";
                lstENTTill = objDALTill.getTillManage(objENTTill);
                if (lstENTTill.Count > 0)
                {
                    lblStartDate.Text = "Start Date : " + lstENTTill[0].StartDateTime;
                    lblEndDate.Text = "End Date : " + lstENTTill[0].EndDateTime;
                    txtTotalStartCash.Text = Convert.ToString(lstENTTill[0].StartCash);
                    txtPayIn.Text = Convert.ToString(lstENTTill[0].PayIn);
                    txtPayOut.Text = Convert.ToString(lstENTTill[0].PayOut);
                    txtCash.Text = Convert.ToString(lstENTTill[0].Cash);
                    txtExpectedCash.Text = Convert.ToString(lstENTTill[0].ExpectedCash);
                    txtEndingCash.Text = Convert.ToString(lstENTTill[0].EndCash);
                    txtDifference.Text = Convert.ToString(lstENTTill[0].Difference);
                }
                else
                {
                    lblStartDate.Text = "Start Date : ";
                    lblEndDate.Text = "End Date : ";
                    MessageBox.Show("Till is not set on selected date.", "Till Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Till Summary", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmTillSummary_Load(object sender, EventArgs e)
        {
            GetTillSummary();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrintSummary_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure? You want to print summary.", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                PrintReceipt.TillID = txtTillId.Text.Trim();
                PrintReceipt pr = new Bliss.PrintReceipt();
                pr.PrintTillSummary();
            }                
        }

        private void frmTillSummary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
