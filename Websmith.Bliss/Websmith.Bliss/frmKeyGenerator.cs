using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Websmith.Bliss
{
    public partial class frmKeyGenerator : Form
    {
        public frmKeyGenerator()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenrateKey_Click(object sender, EventArgs e)
        {
            try
            {
                long serialno;
                if (!Int64.TryParse(txtSerial.Text, out serialno))
                {
                    MessageBox.Show("Invalid Serail Number.", "Activation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSerial.Focus();
                    return;
                }
                txtKey.Text = new Websmith.DataLayer.SecurityManager().GenerateKey(serialno).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Activation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
