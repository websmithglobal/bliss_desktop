namespace Websmith.Bliss
{
    partial class frmModifiers
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
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTransID = new System.Windows.Forms.TextBox();
            this.txtOrderID = new System.Windows.Forms.TextBox();
            this.txtProductID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlModiCategory = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlModifier = new System.Windows.Forms.Panel();
            this.pnlOption = new System.Windows.Forms.Panel();
            this.rdoRemove = new System.Windows.Forms.RadioButton();
            this.rdoSide = new System.Windows.Forms.RadioButton();
            this.rdoOnly = new System.Windows.Forms.RadioButton();
            this.rdoNo = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colQty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOpt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIngredientsID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colModifierID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tblMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlOption.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 2;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.9834F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.01659F));
            this.tblMain.Controls.Add(this.panel1, 0, 0);
            this.tblMain.Controls.Add(this.pnlModiCategory, 0, 1);
            this.tblMain.Controls.Add(this.tableLayoutPanel1, 1, 1);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 2;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 443F));
            this.tblMain.Size = new System.Drawing.Size(964, 506);
            this.tblMain.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.tblMain.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.txtTransID);
            this.panel1.Controls.Add(this.txtOrderID);
            this.panel1.Controls.Add(this.txtProductID);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(958, 35);
            this.panel1.TabIndex = 0;
            // 
            // txtTransID
            // 
            this.txtTransID.Location = new System.Drawing.Point(63, 7);
            this.txtTransID.Name = "txtTransID";
            this.txtTransID.Size = new System.Drawing.Size(133, 22);
            this.txtTransID.TabIndex = 3;
            this.txtTransID.Visible = false;
            // 
            // txtOrderID
            // 
            this.txtOrderID.Location = new System.Drawing.Point(34, 7);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.Size = new System.Drawing.Size(133, 22);
            this.txtOrderID.TabIndex = 2;
            this.txtOrderID.Visible = false;
            // 
            // txtProductID
            // 
            this.txtProductID.Location = new System.Drawing.Point(9, 7);
            this.txtProductID.Name = "txtProductID";
            this.txtProductID.Size = new System.Drawing.Size(133, 22);
            this.txtProductID.TabIndex = 1;
            this.txtProductID.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(432, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modifiers";
            // 
            // pnlModiCategory
            // 
            this.pnlModiCategory.AutoScroll = true;
            this.pnlModiCategory.BackColor = System.Drawing.Color.Black;
            this.pnlModiCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlModiCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlModiCategory.Location = new System.Drawing.Point(3, 44);
            this.pnlModiCategory.Name = "pnlModiCategory";
            this.pnlModiCategory.Size = new System.Drawing.Size(177, 459);
            this.pnlModiCategory.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 384F));
            this.tableLayoutPanel1.Controls.Add(this.pnlModifier, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlOption, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(186, 44);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.368192F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.63181F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(775, 459);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // pnlModifier
            // 
            this.pnlModifier.AutoScroll = true;
            this.pnlModifier.BackColor = System.Drawing.Color.Khaki;
            this.pnlModifier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlModifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlModifier.Location = new System.Drawing.Point(3, 45);
            this.pnlModifier.Name = "pnlModifier";
            this.pnlModifier.Size = new System.Drawing.Size(385, 411);
            this.pnlModifier.TabIndex = 0;
            // 
            // pnlOption
            // 
            this.pnlOption.BackColor = System.Drawing.Color.Khaki;
            this.pnlOption.Controls.Add(this.rdoRemove);
            this.pnlOption.Controls.Add(this.rdoSide);
            this.pnlOption.Controls.Add(this.rdoOnly);
            this.pnlOption.Controls.Add(this.rdoNo);
            this.pnlOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOption.Location = new System.Drawing.Point(3, 3);
            this.pnlOption.Name = "pnlOption";
            this.pnlOption.Size = new System.Drawing.Size(385, 36);
            this.pnlOption.TabIndex = 1;
            // 
            // rdoRemove
            // 
            this.rdoRemove.AutoSize = true;
            this.rdoRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoRemove.Location = new System.Drawing.Point(277, 8);
            this.rdoRemove.Name = "rdoRemove";
            this.rdoRemove.Size = new System.Drawing.Size(90, 20);
            this.rdoRemove.TabIndex = 3;
            this.rdoRemove.Text = "REMOVE";
            this.rdoRemove.UseVisualStyleBackColor = true;
            // 
            // rdoSide
            // 
            this.rdoSide.AutoSize = true;
            this.rdoSide.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoSide.Location = new System.Drawing.Point(188, 8);
            this.rdoSide.Name = "rdoSide";
            this.rdoSide.Size = new System.Drawing.Size(61, 20);
            this.rdoSide.TabIndex = 2;
            this.rdoSide.Text = "SIDE";
            this.rdoSide.UseVisualStyleBackColor = true;
            // 
            // rdoOnly
            // 
            this.rdoOnly.AutoSize = true;
            this.rdoOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoOnly.Location = new System.Drawing.Point(94, 8);
            this.rdoOnly.Name = "rdoOnly";
            this.rdoOnly.Size = new System.Drawing.Size(66, 20);
            this.rdoOnly.TabIndex = 1;
            this.rdoOnly.Text = "ONLY";
            this.rdoOnly.UseVisualStyleBackColor = true;
            // 
            // rdoNo
            // 
            this.rdoNo.AutoSize = true;
            this.rdoNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoNo.Location = new System.Drawing.Point(18, 8);
            this.rdoNo.Name = "rdoNo";
            this.rdoNo.Size = new System.Drawing.Size(48, 20);
            this.rdoNo.TabIndex = 0;
            this.rdoNo.Text = "NO";
            this.rdoNo.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Khaki;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(394, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(378, 36);
            this.panel2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(134, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Selected Items";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtTotalAmount);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.listView);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(394, 45);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(378, 411);
            this.panel3.TabIndex = 3;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalAmount.BackColor = System.Drawing.Color.Khaki;
            this.txtTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Location = new System.Drawing.Point(280, 385);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(95, 23);
            this.txtTotalAmount.TabIndex = 28;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(163, 388);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 17);
            this.label7.TabIndex = 27;
            this.label7.Text = "Total Amount :";
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colQty,
            this.colPrice,
            this.colOpt,
            this.colIngredientsID,
            this.colModifierID,
            this.colTotal});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(1, 0);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(375, 384);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 155;
            // 
            // colQty
            // 
            this.colQty.Text = "Qty.";
            this.colQty.Width = 50;
            // 
            // colPrice
            // 
            this.colPrice.Text = "Price";
            this.colPrice.Width = 50;
            // 
            // colOpt
            // 
            this.colOpt.Text = "Option";
            // 
            // colIngredientsID
            // 
            this.colIngredientsID.Text = "ID";
            this.colIngredientsID.Width = 0;
            // 
            // colModifierID
            // 
            this.colModifierID.Text = "ModifierID";
            this.colModifierID.Width = 0;
            // 
            // colTotal
            // 
            this.colTotal.Text = "Total";
            // 
            // frmModifiers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 506);
            this.Controls.Add(this.tblMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModifiers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modifiers";
            this.Load += new System.EventHandler(this.frmModifiers_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmModifiers_KeyDown);
            this.tblMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlOption.ResumeLayout(false);
            this.pnlOption.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlModiCategory;
        private System.Windows.Forms.TextBox txtOrderID;
        private System.Windows.Forms.TextBox txtProductID;
        private System.Windows.Forms.TextBox txtTransID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlModifier;
        private System.Windows.Forms.Panel pnlOption;
        private System.Windows.Forms.RadioButton rdoSide;
        private System.Windows.Forms.RadioButton rdoOnly;
        private System.Windows.Forms.RadioButton rdoNo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.ColumnHeader colOpt;
        private System.Windows.Forms.ColumnHeader colIngredientsID;
        private System.Windows.Forms.ColumnHeader colPrice;
        private System.Windows.Forms.ColumnHeader colModifierID;
        private System.Windows.Forms.RadioButton rdoRemove;
        private System.Windows.Forms.ColumnHeader colTotal;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label label7;
    }
}