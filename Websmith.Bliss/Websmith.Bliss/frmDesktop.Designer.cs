namespace Websmith.Bliss
{
    partial class frmDesktop
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mAINMENUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mmOrderBook = new System.Windows.Forms.ToolStripMenuItem();
            this.mmAddCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.mmMergeTable = new System.Windows.Forms.ToolStripMenuItem();
            this.sETTINGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.stGeneral = new System.Windows.Forms.ToolStripMenuItem();
            this.stReports = new System.Windows.Forms.ToolStripMenuItem();
            this.rptSales = new System.Windows.Forms.ToolStripMenuItem();
            this.rptSalesSalesPeriod = new System.Windows.Forms.ToolStripMenuItem();
            this.rptSalesCreditCard = new System.Windows.Forms.ToolStripMenuItem();
            this.rptSalesGiftCard = new System.Windows.Forms.ToolStripMenuItem();
            this.rptSalesByEmployee = new System.Windows.Forms.ToolStripMenuItem();
            this.rptSalesByMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.customerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.employeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.stEmployee = new System.Windows.Forms.ToolStripMenuItem();
            this.stDataBackUp = new System.Windows.Forms.ToolStripMenuItem();
            this.stCloudServices = new System.Windows.Forms.ToolStripMenuItem();
            this.stTillManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.stLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.ssyncData = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mAINMENUToolStripMenuItem,
            this.sETTINGToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip.Size = new System.Drawing.Size(1008, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // mAINMENUToolStripMenuItem
            // 
            this.mAINMENUToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmOrderBook,
            this.mmAddCustomer,
            this.mmMergeTable});
            this.mAINMENUToolStripMenuItem.Name = "mAINMENUToolStripMenuItem";
            this.mAINMENUToolStripMenuItem.Size = new System.Drawing.Size(80, 19);
            this.mAINMENUToolStripMenuItem.Text = "Main Menu";
            // 
            // mmOrderBook
            // 
            this.mmOrderBook.Name = "mmOrderBook";
            this.mmOrderBook.Size = new System.Drawing.Size(152, 22);
            this.mmOrderBook.Text = "Order Book";
            this.mmOrderBook.Click += new System.EventHandler(this.oRDERBOOKToolStripMenuItem_Click);
            // 
            // mmAddCustomer
            // 
            this.mmAddCustomer.Name = "mmAddCustomer";
            this.mmAddCustomer.Size = new System.Drawing.Size(152, 22);
            this.mmAddCustomer.Text = "Add Customer";
            this.mmAddCustomer.Click += new System.EventHandler(this.mmAddCustomer_Click);
            // 
            // mmMergeTable
            // 
            this.mmMergeTable.Name = "mmMergeTable";
            this.mmMergeTable.Size = new System.Drawing.Size(152, 22);
            this.mmMergeTable.Text = "Merge Table";
            this.mmMergeTable.Click += new System.EventHandler(this.mmMergeTable_Click);
            // 
            // sETTINGToolStripMenuItem
            // 
            this.sETTINGToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stAbout,
            this.stGeneral,
            this.stReports,
            this.stMenu,
            this.stEmployee,
            this.stDataBackUp,
            this.stCloudServices,
            this.stTillManagement,
            this.stLogOut,
            this.ssyncData});
            this.sETTINGToolStripMenuItem.Name = "sETTINGToolStripMenuItem";
            this.sETTINGToolStripMenuItem.Size = new System.Drawing.Size(61, 19);
            this.sETTINGToolStripMenuItem.Text = "Settings";
            // 
            // stAbout
            // 
            this.stAbout.Name = "stAbout";
            this.stAbout.Size = new System.Drawing.Size(164, 22);
            this.stAbout.Text = "About";
            this.stAbout.Click += new System.EventHandler(this.stAbout_Click);
            // 
            // stGeneral
            // 
            this.stGeneral.Name = "stGeneral";
            this.stGeneral.Size = new System.Drawing.Size(164, 22);
            this.stGeneral.Text = "General";
            this.stGeneral.Click += new System.EventHandler(this.stGeneral_Click);
            // 
            // stReports
            // 
            this.stReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rptSales,
            this.customerToolStripMenuItem,
            this.employeeToolStripMenuItem,
            this.inventoryToolStripMenuItem});
            this.stReports.Name = "stReports";
            this.stReports.Size = new System.Drawing.Size(164, 22);
            this.stReports.Text = "Reports";
            // 
            // rptSales
            // 
            this.rptSales.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rptSalesSalesPeriod,
            this.rptSalesCreditCard,
            this.rptSalesGiftCard,
            this.rptSalesByEmployee,
            this.rptSalesByMenu});
            this.rptSales.Name = "rptSales";
            this.rptSales.Size = new System.Drawing.Size(126, 22);
            this.rptSales.Text = "Sales";
            // 
            // rptSalesSalesPeriod
            // 
            this.rptSalesSalesPeriod.Name = "rptSalesSalesPeriod";
            this.rptSalesSalesPeriod.Size = new System.Drawing.Size(190, 22);
            this.rptSalesSalesPeriod.Text = "Sales Period";
            
            // 
            // rptSalesCreditCard
            // 
            this.rptSalesCreditCard.Name = "rptSalesCreditCard";
            this.rptSalesCreditCard.Size = new System.Drawing.Size(190, 22);
            this.rptSalesCreditCard.Text = "Credit Card";
            // 
            // rptSalesGiftCard
            // 
            this.rptSalesGiftCard.Name = "rptSalesGiftCard";
            this.rptSalesGiftCard.Size = new System.Drawing.Size(190, 22);
            this.rptSalesGiftCard.Text = "Gift Card Transactions";
            // 
            // rptSalesByEmployee
            // 
            this.rptSalesByEmployee.Name = "rptSalesByEmployee";
            this.rptSalesByEmployee.Size = new System.Drawing.Size(190, 22);
            this.rptSalesByEmployee.Text = "By Employee";
            // 
            // rptSalesByMenu
            // 
            this.rptSalesByMenu.Name = "rptSalesByMenu";
            this.rptSalesByMenu.Size = new System.Drawing.Size(190, 22);
            this.rptSalesByMenu.Text = "By Menu";
            // 
            // customerToolStripMenuItem
            // 
            this.customerToolStripMenuItem.Name = "customerToolStripMenuItem";
            this.customerToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.customerToolStripMenuItem.Text = "Customer";
            // 
            // employeeToolStripMenuItem
            // 
            this.employeeToolStripMenuItem.Name = "employeeToolStripMenuItem";
            this.employeeToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.employeeToolStripMenuItem.Text = "Employee";
            // 
            // inventoryToolStripMenuItem
            // 
            this.inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
            this.inventoryToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.inventoryToolStripMenuItem.Text = "Inventory";
            // 
            // stMenu
            // 
            this.stMenu.Name = "stMenu";
            this.stMenu.Size = new System.Drawing.Size(164, 22);
            this.stMenu.Text = "Menu";
            // 
            // stEmployee
            // 
            this.stEmployee.Name = "stEmployee";
            this.stEmployee.Size = new System.Drawing.Size(164, 22);
            this.stEmployee.Text = "Employee";
            this.stEmployee.Click += new System.EventHandler(this.stEmployee_Click);
            // 
            // stDataBackUp
            // 
            this.stDataBackUp.Name = "stDataBackUp";
            this.stDataBackUp.Size = new System.Drawing.Size(164, 22);
            this.stDataBackUp.Text = "Data Back Up";
            // 
            // stCloudServices
            // 
            this.stCloudServices.Name = "stCloudServices";
            this.stCloudServices.Size = new System.Drawing.Size(164, 22);
            this.stCloudServices.Text = "Cloud Services";
            // 
            // stTillManagement
            // 
            this.stTillManagement.Name = "stTillManagement";
            this.stTillManagement.Size = new System.Drawing.Size(164, 22);
            this.stTillManagement.Text = "Till Management";
            // 
            // stLogOut
            // 
            this.stLogOut.Name = "stLogOut";
            this.stLogOut.Size = new System.Drawing.Size(164, 22);
            this.stLogOut.Text = "Log Out";
            this.stLogOut.Click += new System.EventHandler(this.stLogOut_Click);
            // 
            // ssyncData
            // 
            this.ssyncData.Name = "ssyncData";
            this.ssyncData.Size = new System.Drawing.Size(164, 22);
            this.ssyncData.Text = "Sync Data";
            this.ssyncData.Click += new System.EventHandler(this.ssyncData_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 617);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // toolStrip
            // 
            this.toolStrip.Location = new System.Drawing.Point(0, 25);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip.Size = new System.Drawing.Size(1008, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // frmDesktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 639);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmDesktop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DASHBOARD";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDesktop_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem mAINMENUToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mmOrderBook;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripMenuItem sETTINGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stAbout;
        private System.Windows.Forms.ToolStripMenuItem stGeneral;
        private System.Windows.Forms.ToolStripMenuItem stReports;
        private System.Windows.Forms.ToolStripMenuItem stMenu;
        private System.Windows.Forms.ToolStripMenuItem stEmployee;
        private System.Windows.Forms.ToolStripMenuItem stDataBackUp;
        private System.Windows.Forms.ToolStripMenuItem stCloudServices;
        private System.Windows.Forms.ToolStripMenuItem stTillManagement;
        private System.Windows.Forms.ToolStripMenuItem stLogOut;
        private System.Windows.Forms.ToolStripMenuItem rptSales;
        private System.Windows.Forms.ToolStripMenuItem rptSalesSalesPeriod;
        private System.Windows.Forms.ToolStripMenuItem rptSalesCreditCard;
        private System.Windows.Forms.ToolStripMenuItem rptSalesGiftCard;
        private System.Windows.Forms.ToolStripMenuItem rptSalesByEmployee;
        private System.Windows.Forms.ToolStripMenuItem rptSalesByMenu;
        private System.Windows.Forms.ToolStripMenuItem customerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem employeeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mmAddCustomer;
        private System.Windows.Forms.ToolStripMenuItem mmMergeTable;
        private System.Windows.Forms.ToolStripMenuItem ssyncData;
    }
}



