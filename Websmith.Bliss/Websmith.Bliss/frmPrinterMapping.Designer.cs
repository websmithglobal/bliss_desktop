namespace Websmith.Bliss
{
    partial class frmPrinterMapping
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.dgvOtherStation = new System.Windows.Forms.DataGridView();
            this.ODeviceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ODeviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OPrinterName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvYourStation = new System.Windows.Forms.DataGridView();
            this.YDeviceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YDeviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YPrinterName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSaveYourStation = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSaveOtherStation = new System.Windows.Forms.Button();
            this.tblMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtherStation)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYourStation)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 1;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.dgvOtherStation, 0, 3);
            this.tblMain.Controls.Add(this.panel2, 0, 2);
            this.tblMain.Controls.Add(this.dgvYourStation, 0, 1);
            this.tblMain.Controls.Add(this.panel1, 0, 0);
            this.tblMain.Controls.Add(this.tableLayoutPanel1, 0, 4);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 5;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 245F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblMain.Size = new System.Drawing.Size(449, 494);
            this.tblMain.TabIndex = 0;
            // 
            // dgvOtherStation
            // 
            this.dgvOtherStation.AllowUserToAddRows = false;
            this.dgvOtherStation.AllowUserToDeleteRows = false;
            this.dgvOtherStation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOtherStation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOtherStation.ColumnHeadersHeight = 30;
            this.dgvOtherStation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ODeviceID,
            this.ODeviceName,
            this.OPrinterName});
            this.dgvOtherStation.Location = new System.Drawing.Point(4, 190);
            this.dgvOtherStation.Margin = new System.Windows.Forms.Padding(4);
            this.dgvOtherStation.MultiSelect = false;
            this.dgvOtherStation.Name = "dgvOtherStation";
            this.dgvOtherStation.RowHeadersWidth = 30;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvOtherStation.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOtherStation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOtherStation.Size = new System.Drawing.Size(441, 237);
            this.dgvOtherStation.TabIndex = 28;
            // 
            // ODeviceID
            // 
            this.ODeviceID.HeaderText = "DeviceID";
            this.ODeviceID.Name = "ODeviceID";
            this.ODeviceID.Visible = false;
            // 
            // ODeviceName
            // 
            this.ODeviceName.HeaderText = "Station/Device Name";
            this.ODeviceName.Name = "ODeviceName";
            this.ODeviceName.ReadOnly = true;
            // 
            // OPrinterName
            // 
            this.OPrinterName.HeaderText = "Printer Name";
            this.OPrinterName.Name = "OPrinterName";
            this.OPrinterName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OPrinterName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(89)))), ((int)(((byte)(79)))));
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 154);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(443, 29);
            this.panel2.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(111, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Assign Printer To Other Station";
            // 
            // dgvYourStation
            // 
            this.dgvYourStation.AllowUserToAddRows = false;
            this.dgvYourStation.AllowUserToDeleteRows = false;
            this.dgvYourStation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvYourStation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvYourStation.ColumnHeadersHeight = 30;
            this.dgvYourStation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.YDeviceID,
            this.YDeviceName,
            this.YPrinterName});
            this.dgvYourStation.Location = new System.Drawing.Point(4, 39);
            this.dgvYourStation.Margin = new System.Windows.Forms.Padding(4);
            this.dgvYourStation.MultiSelect = false;
            this.dgvYourStation.Name = "dgvYourStation";
            this.dgvYourStation.RowHeadersWidth = 30;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvYourStation.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvYourStation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvYourStation.Size = new System.Drawing.Size(441, 108);
            this.dgvYourStation.TabIndex = 26;
            // 
            // YDeviceID
            // 
            this.YDeviceID.HeaderText = "DeviceID";
            this.YDeviceID.Name = "YDeviceID";
            this.YDeviceID.Visible = false;
            // 
            // YDeviceName
            // 
            this.YDeviceName.HeaderText = "Station/Device Name";
            this.YDeviceName.Name = "YDeviceName";
            this.YDeviceName.ReadOnly = true;
            // 
            // YPrinterName
            // 
            this.YPrinterName.HeaderText = "Printer Name";
            this.YPrinterName.Name = "YPrinterName";
            this.YPrinterName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.YPrinterName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(89)))), ((int)(((byte)(79)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(443, 29);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(114, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Assign Printer To Your Station";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.panel5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 434);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(443, 57);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnClose);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(297, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(143, 51);
            this.panel5.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(4, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(135, 47);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "&CANCEL";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnSaveYourStation);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(141, 51);
            this.panel4.TabIndex = 1;
            // 
            // btnSaveYourStation
            // 
            this.btnSaveYourStation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveYourStation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(89)))), ((int)(((byte)(79)))));
            this.btnSaveYourStation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveYourStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveYourStation.ForeColor = System.Drawing.Color.White;
            this.btnSaveYourStation.Location = new System.Drawing.Point(3, 2);
            this.btnSaveYourStation.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveYourStation.Name = "btnSaveYourStation";
            this.btnSaveYourStation.Size = new System.Drawing.Size(135, 47);
            this.btnSaveYourStation.TabIndex = 5;
            this.btnSaveYourStation.Text = "&SAVE YOUR STATION";
            this.btnSaveYourStation.UseVisualStyleBackColor = false;
            this.btnSaveYourStation.Click += new System.EventHandler(this.btnSaveYourStation_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnSaveOtherStation);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(150, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(141, 51);
            this.panel3.TabIndex = 0;
            // 
            // btnSaveOtherStation
            // 
            this.btnSaveOtherStation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveOtherStation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(89)))), ((int)(((byte)(79)))));
            this.btnSaveOtherStation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveOtherStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveOtherStation.ForeColor = System.Drawing.Color.White;
            this.btnSaveOtherStation.Location = new System.Drawing.Point(3, 2);
            this.btnSaveOtherStation.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveOtherStation.Name = "btnSaveOtherStation";
            this.btnSaveOtherStation.Size = new System.Drawing.Size(135, 47);
            this.btnSaveOtherStation.TabIndex = 6;
            this.btnSaveOtherStation.Text = "&SAVE OTHER STATION";
            this.btnSaveOtherStation.UseVisualStyleBackColor = false;
            this.btnSaveOtherStation.Click += new System.EventHandler(this.btnSaveOtherStation_Click);
            // 
            // frmPrinterMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(173)))), ((int)(((byte)(158)))));
            this.ClientSize = new System.Drawing.Size(449, 494);
            this.Controls.Add(this.tblMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrinterMapping";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Printer Mapping";
            this.Load += new System.EventHandler(this.frmPrinterMapping_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPrinterMapping_KeyDown);
            this.tblMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtherStation)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYourStation)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvYourStation;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvOtherStation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSaveYourStation;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSaveOtherStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODeviceID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODeviceName;
        private System.Windows.Forms.DataGridViewComboBoxColumn OPrinterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn YDeviceID;
        private System.Windows.Forms.DataGridViewTextBoxColumn YDeviceName;
        private System.Windows.Forms.DataGridViewComboBoxColumn YPrinterName;
    }
}