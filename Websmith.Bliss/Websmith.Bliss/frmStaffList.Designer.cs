namespace Websmith.Bliss
{
    partial class frmStaffList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tpStaffList = new System.Windows.Forms.TabPage();
            this.dgvEmployee = new System.Windows.Forms.DataGridView();
            this.EmpID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpMobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpRoleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpSalaryTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpIsDisplayInKDS = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.EmpTotalHourInADay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpJoinDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpGenderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpSalaryAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpAttendance = new System.Windows.Forms.TabPage();
            this.tpPayroll = new System.Windows.Forms.TabPage();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.tabMain.SuspendLayout();
            this.tpStaffList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tpStaffList);
            this.tabMain.Controls.Add(this.tpAttendance);
            this.tabMain.Controls.Add(this.tpPayroll);
            this.tabMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Margin = new System.Windows.Forms.Padding(4);
            this.tabMain.Multiline = true;
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Drawing.Point(10, 12);
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(984, 511);
            this.tabMain.TabIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tpStaffList
            // 
            this.tpStaffList.Controls.Add(this.dgvEmployee);
            this.tpStaffList.Location = new System.Drawing.Point(4, 47);
            this.tpStaffList.Margin = new System.Windows.Forms.Padding(4);
            this.tpStaffList.Name = "tpStaffList";
            this.tpStaffList.Padding = new System.Windows.Forms.Padding(4);
            this.tpStaffList.Size = new System.Drawing.Size(976, 460);
            this.tpStaffList.TabIndex = 0;
            this.tpStaffList.Text = "STAFF LIST";
            this.tpStaffList.UseVisualStyleBackColor = true;
            // 
            // dgvEmployee
            // 
            this.dgvEmployee.AllowUserToAddRows = false;
            this.dgvEmployee.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dgvEmployee.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEmployee.BackgroundColor = System.Drawing.Color.White;
            this.dgvEmployee.ColumnHeadersHeight = 45;
            this.dgvEmployee.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmpID,
            this.EmpCode,
            this.EmpName,
            this.EmpMobile,
            this.EmpRoleName,
            this.EmpSalaryTypeName,
            this.EmpIsDisplayInKDS,
            this.EmpTotalHourInADay,
            this.EmpJoinDate,
            this.EmpEmail,
            this.EmpGenderName,
            this.EmpSalaryAmt,
            this.EmpAddress});
            this.dgvEmployee.Location = new System.Drawing.Point(4, 4);
            this.dgvEmployee.MultiSelect = false;
            this.dgvEmployee.Name = "dgvEmployee";
            this.dgvEmployee.ReadOnly = true;
            this.dgvEmployee.RowHeadersWidth = 30;
            this.dgvEmployee.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvEmployee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployee.Size = new System.Drawing.Size(968, 452);
            this.dgvEmployee.TabIndex = 17;
            // 
            // EmpID
            // 
            this.EmpID.HeaderText = "Emp ID";
            this.EmpID.Name = "EmpID";
            this.EmpID.ReadOnly = true;
            this.EmpID.Visible = false;
            // 
            // EmpCode
            // 
            this.EmpCode.HeaderText = "Employee Code";
            this.EmpCode.Name = "EmpCode";
            this.EmpCode.ReadOnly = true;
            // 
            // EmpName
            // 
            this.EmpName.HeaderText = "Employee Name";
            this.EmpName.Name = "EmpName";
            this.EmpName.ReadOnly = true;
            this.EmpName.Width = 140;
            // 
            // EmpMobile
            // 
            this.EmpMobile.HeaderText = "Mobile";
            this.EmpMobile.Name = "EmpMobile";
            this.EmpMobile.ReadOnly = true;
            this.EmpMobile.Width = 105;
            // 
            // EmpRoleName
            // 
            this.EmpRoleName.HeaderText = "RoleName";
            this.EmpRoleName.Name = "EmpRoleName";
            this.EmpRoleName.ReadOnly = true;
            this.EmpRoleName.Width = 120;
            // 
            // EmpSalaryTypeName
            // 
            this.EmpSalaryTypeName.HeaderText = "Salary Type";
            this.EmpSalaryTypeName.Name = "EmpSalaryTypeName";
            this.EmpSalaryTypeName.ReadOnly = true;
            // 
            // EmpIsDisplayInKDS
            // 
            this.EmpIsDisplayInKDS.HeaderText = "Is Display In KDS";
            this.EmpIsDisplayInKDS.Name = "EmpIsDisplayInKDS";
            this.EmpIsDisplayInKDS.ReadOnly = true;
            this.EmpIsDisplayInKDS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EmpIsDisplayInKDS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // EmpTotalHourInADay
            // 
            this.EmpTotalHourInADay.HeaderText = "Total Hour In A Day";
            this.EmpTotalHourInADay.Name = "EmpTotalHourInADay";
            this.EmpTotalHourInADay.ReadOnly = true;
            // 
            // EmpJoinDate
            // 
            this.EmpJoinDate.HeaderText = "Join Date";
            this.EmpJoinDate.Name = "EmpJoinDate";
            this.EmpJoinDate.ReadOnly = true;
            // 
            // EmpEmail
            // 
            this.EmpEmail.HeaderText = "Email";
            this.EmpEmail.Name = "EmpEmail";
            this.EmpEmail.ReadOnly = true;
            this.EmpEmail.Width = 200;
            // 
            // EmpGenderName
            // 
            this.EmpGenderName.HeaderText = "Gender";
            this.EmpGenderName.Name = "EmpGenderName";
            this.EmpGenderName.ReadOnly = true;
            // 
            // EmpSalaryAmt
            // 
            this.EmpSalaryAmt.HeaderText = "Salary Amount";
            this.EmpSalaryAmt.Name = "EmpSalaryAmt";
            this.EmpSalaryAmt.ReadOnly = true;
            // 
            // EmpAddress
            // 
            this.EmpAddress.HeaderText = "Address";
            this.EmpAddress.Name = "EmpAddress";
            this.EmpAddress.ReadOnly = true;
            // 
            // tpAttendance
            // 
            this.tpAttendance.Location = new System.Drawing.Point(4, 47);
            this.tpAttendance.Margin = new System.Windows.Forms.Padding(4);
            this.tpAttendance.Name = "tpAttendance";
            this.tpAttendance.Padding = new System.Windows.Forms.Padding(4);
            this.tpAttendance.Size = new System.Drawing.Size(976, 460);
            this.tpAttendance.TabIndex = 1;
            this.tpAttendance.Text = "ATTENDANCE";
            this.tpAttendance.UseVisualStyleBackColor = true;
            // 
            // tpPayroll
            // 
            this.tpPayroll.Location = new System.Drawing.Point(4, 47);
            this.tpPayroll.Margin = new System.Windows.Forms.Padding(4);
            this.tpPayroll.Name = "tpPayroll";
            this.tpPayroll.Padding = new System.Windows.Forms.Padding(4);
            this.tpPayroll.Size = new System.Drawing.Size(976, 460);
            this.tpPayroll.TabIndex = 2;
            this.tpPayroll.Text = "PAYROLL";
            this.tpPayroll.UseVisualStyleBackColor = true;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Location = new System.Drawing.Point(421, 12);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(265, 20);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Search By Name Or Mobile No. :";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(692, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(211, 26);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(89)))), ((int)(((byte)(79)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(909, 10);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(68, 26);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmStaffList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(173)))), ((int)(((byte)(158)))));
            this.ClientSize = new System.Drawing.Size(984, 511);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.tabMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStaffList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Staff List";
            this.Load += new System.EventHandler(this.frmStaffList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmStaffList_KeyDown);
            this.tabMain.ResumeLayout(false);
            this.tpStaffList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tpStaffList;
        private System.Windows.Forms.TabPage tpAttendance;
        private System.Windows.Forms.TabPage tpPayroll;
        private System.Windows.Forms.DataGridView dgvEmployee;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpMobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpRoleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpSalaryTypeName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EmpIsDisplayInKDS;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpTotalHourInADay;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpJoinDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpGenderName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpSalaryAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpAddress;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnClear;
    }
}