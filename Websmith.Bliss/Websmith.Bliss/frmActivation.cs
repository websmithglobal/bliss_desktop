using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL = Websmith.DataLayer;
using ENT = Websmith.Entity;

namespace Websmith.Bliss
{
    public partial class frmActivation : Form
    {
        string BranchID = string.Empty;

        public frmActivation()
        {
            InitializeComponent();
        }

        public frmActivation(string strBranchID)
        {
            InitializeComponent();
            BranchID = strBranchID;
        }

        private void frmActivation_Load(object sender, EventArgs e)
        {
            txtSerial.Text = new DAL.SecurityManager().GetSerial().ToString();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            long key;
            if (!Int64.TryParse(txtKey.Text, out key))
            {
                MessageBox.Show("Invalid Activation Key.", "Activation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKey.Focus();
                return;
            }

            if (new DAL.SecurityManager().CheckKey(key))
            {
                ENT.BranchSettingDetail objENT = new ENT.BranchSettingDetail();
                objENT.DemoCode = DAL.SecurityManager.Base64Encode(DAL.SecurityManager.Base64Encode(key.ToString()));
                objENT.IsDemoVersion = false;
                objENT.BranchID = new Guid(BranchID);
                objENT.Mode = "UPDATE_ACTIVATION";
                if (new DAL.BranchSettingDetail().InsertUpdateDeleteVersionDetail(objENT))
                {
                    MessageBox.Show("Activation Was Successfull !!!", "Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Activation Was Unsuccessfull !!!","Activation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
