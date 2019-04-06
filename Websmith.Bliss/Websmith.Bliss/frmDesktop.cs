using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Websmith.Bliss
{
    public partial class frmDesktop : Form
    {
        private int childFormNumber = 0;

        public frmDesktop()
        {
            InitializeComponent();
        }

        static void RestartApp(int pid, string applicationName)
        {
            Process process = null;
            try
            {
                process = Process.GetProcessById(pid);
                process.WaitForExit(1000);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Process.Start(applicationName, "");
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void oRDERBOOKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOrderBook frmOB = new frmOrderBook();
            //frmOB.MdiParent = this;
            frmOB.Show();
        }

        private void btnOrderBook_Click(object sender, EventArgs e)
        {
            frmOrderBook frmOB = new frmOrderBook();
            frmOB.MdiParent = this;
            frmOB.Show();
        }

        private void stAbout_Click(object sender, EventArgs e)
        {
            frmAbout frmABT = new frmAbout();
            frmABT.ShowDialog();
        }

        private void stGeneral_Click(object sender, EventArgs e)
        {
            frmGeneral frmGN = new frmGeneral();
            frmGN.ShowDialog();
        }

        private void stEmployee_Click(object sender, EventArgs e)
        {
            frmEmployee frmEMP = new frmEmployee();
            frmEMP.MdiParent = this;
            frmEMP.Show();
        }

        private void mmAddCustomer_Click(object sender, EventArgs e)
        {
            frmCustomerMaster frmCust = new frmCustomerMaster();
            frmCust.ShowDialog();
        }

        private void mmMergeTable_Click(object sender, EventArgs e)
        {
            frmMergeTable frmMT = new frmMergeTable();
            frmMT.ShowDialog();
        }

        private void frmDesktop_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = GlobalVariable.Dashboard+ " || " + GlobalVariable.BranchName + " || " + GlobalVariable.EmployeeName;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void stLogOut_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form childForm in MdiChildren)
                {
                    childForm.Close();
                }
                GlobalVariable.objLoginResponse = null;
                GlobalVariable.objEmployeeMasterList = null;
                RestartApp(Process.GetCurrentProcess().Id, "Websmith.Bliss.exe");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ssyncData_Click(object sender, EventArgs e)
        {
            frmSyncData frm = new frmSyncData();
            frm.ShowDialog();
        }

        private void displayCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
    }
}
