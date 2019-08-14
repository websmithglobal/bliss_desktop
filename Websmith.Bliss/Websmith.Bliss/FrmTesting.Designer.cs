namespace Websmith.Bliss
{
    partial class FrmTesting
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.txtFees = new System.Windows.Forms.TextBox();
            this.txtItemTotal = new System.Windows.Forms.TextBox();
            this.txtTax1 = new System.Windows.Forms.TextBox();
            this.txtTax2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtTax2Amount = new System.Windows.Forms.TextBox();
            this.txtTax1Amount = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Amount";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Free";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "TAX 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "TAX 2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Item Total";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(92, 17);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(100, 20);
            this.txtTotalAmount.TabIndex = 0;
            // 
            // txtFees
            // 
            this.txtFees.Location = new System.Drawing.Point(92, 43);
            this.txtFees.Name = "txtFees";
            this.txtFees.Size = new System.Drawing.Size(100, 20);
            this.txtFees.TabIndex = 1;
            // 
            // txtItemTotal
            // 
            this.txtItemTotal.Location = new System.Drawing.Point(92, 69);
            this.txtItemTotal.Name = "txtItemTotal";
            this.txtItemTotal.Size = new System.Drawing.Size(100, 20);
            this.txtItemTotal.TabIndex = 2;
            // 
            // txtTax1
            // 
            this.txtTax1.Location = new System.Drawing.Point(92, 95);
            this.txtTax1.Name = "txtTax1";
            this.txtTax1.Size = new System.Drawing.Size(26, 20);
            this.txtTax1.TabIndex = 3;
            this.txtTax1.Text = "9";
            // 
            // txtTax2
            // 
            this.txtTax2.Location = new System.Drawing.Point(92, 121);
            this.txtTax2.Name = "txtTax2";
            this.txtTax2.Size = new System.Drawing.Size(26, 20);
            this.txtTax2.TabIndex = 4;
            this.txtTax2.Text = "9";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(92, 147);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "CALC";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTax2Amount
            // 
            this.txtTax2Amount.Location = new System.Drawing.Point(124, 121);
            this.txtTax2Amount.Name = "txtTax2Amount";
            this.txtTax2Amount.Size = new System.Drawing.Size(68, 20);
            this.txtTax2Amount.TabIndex = 7;
            // 
            // txtTax1Amount
            // 
            this.txtTax1Amount.Location = new System.Drawing.Point(124, 95);
            this.txtTax1Amount.Name = "txtTax1Amount";
            this.txtTax1Amount.Size = new System.Drawing.Size(68, 20);
            this.txtTax1Amount.TabIndex = 6;
            // 
            // FrmTesting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(209, 183);
            this.Controls.Add(this.txtTax2Amount);
            this.Controls.Add(this.txtTax1Amount);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtTax2);
            this.Controls.Add(this.txtTax1);
            this.Controls.Add(this.txtItemTotal);
            this.Controls.Add(this.txtFees);
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmTesting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmTesting";
            this.Load += new System.EventHandler(this.FrmTesting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.TextBox txtFees;
        private System.Windows.Forms.TextBox txtItemTotal;
        private System.Windows.Forms.TextBox txtTax1;
        private System.Windows.Forms.TextBox txtTax2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtTax2Amount;
        private System.Windows.Forms.TextBox txtTax1Amount;
    }
}