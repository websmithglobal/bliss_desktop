namespace Websmith.Bliss
{
    partial class frmSocketServer
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
            this.Start_Button = new System.Windows.Forms.Button();
            this.Send_Button = new System.Windows.Forms.Button();
            this.Close_Button = new System.Windows.Forms.Button();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbMessageSend = new System.Windows.Forms.TextBox();
            this.Disconnect = new System.Windows.Forms.Button();
            this.serverConsole = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.clientConsole = new System.Windows.Forms.Label();
            this.btnSendClient = new System.Windows.Forms.Button();
            this.tbClientSend = new System.Windows.Forms.TextBox();
            this.tbIpPort = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnClient = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Start_Button
            // 
            this.Start_Button.Location = new System.Drawing.Point(77, 21);
            this.Start_Button.Name = "Start_Button";
            this.Start_Button.Size = new System.Drawing.Size(95, 42);
            this.Start_Button.TabIndex = 0;
            this.Start_Button.Text = "Start Server";
            this.Start_Button.UseVisualStyleBackColor = true;
            this.Start_Button.Click += new System.EventHandler(this.Start_Button_Click);
            // 
            // Send_Button
            // 
            this.Send_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Send_Button.Location = new System.Drawing.Point(302, 353);
            this.Send_Button.Name = "Send_Button";
            this.Send_Button.Size = new System.Drawing.Size(75, 29);
            this.Send_Button.TabIndex = 2;
            this.Send_Button.Text = "Send";
            this.Send_Button.UseVisualStyleBackColor = true;
            this.Send_Button.Click += new System.EventHandler(this.Send_Button_Click);
            // 
            // Close_Button
            // 
            this.Close_Button.Location = new System.Drawing.Point(302, 21);
            this.Close_Button.Name = "Close_Button";
            this.Close_Button.Size = new System.Drawing.Size(75, 42);
            this.Close_Button.TabIndex = 3;
            this.Close_Button.Text = "Close";
            this.Close_Button.UseVisualStyleBackColor = true;
            // 
            // tbPort
            // 
            this.tbPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPort.Location = new System.Drawing.Point(10, 25);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(54, 29);
            this.tbPort.TabIndex = 4;
            this.tbPort.Text = "9091";
            // 
            // tbMessageSend
            // 
            this.tbMessageSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessageSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.tbMessageSend.Location = new System.Drawing.Point(12, 353);
            this.tbMessageSend.Name = "tbMessageSend";
            this.tbMessageSend.Size = new System.Drawing.Size(284, 29);
            this.tbMessageSend.TabIndex = 5;
            // 
            // Disconnect
            // 
            this.Disconnect.Location = new System.Drawing.Point(178, 21);
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.Size = new System.Drawing.Size(118, 42);
            this.Disconnect.TabIndex = 6;
            this.Disconnect.Text = "Disconnect";
            this.Disconnect.UseVisualStyleBackColor = true;
            this.Disconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // serverConsole
            // 
            this.serverConsole.AutoSize = true;
            this.serverConsole.Location = new System.Drawing.Point(3, 0);
            this.serverConsole.Name = "serverConsole";
            this.serverConsole.Size = new System.Drawing.Size(0, 16);
            this.serverConsole.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.serverConsole);
            this.panel1.Location = new System.Drawing.Point(12, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 278);
            this.panel1.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Start_Button);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.Send_Button);
            this.groupBox1.Controls.Add(this.Disconnect);
            this.groupBox1.Controls.Add(this.Close_Button);
            this.groupBox1.Controls.Add(this.tbMessageSend);
            this.groupBox1.Controls.Add(this.tbPort);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 394);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.btnSendClient);
            this.groupBox2.Controls.Add(this.tbClientSend);
            this.groupBox2.Controls.Add(this.tbIpPort);
            this.groupBox2.Controls.Add(this.btnDisconnect);
            this.groupBox2.Controls.Add(this.btnClient);
            this.groupBox2.Location = new System.Drawing.Point(408, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(420, 394);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Client";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.clientConsole);
            this.panel2.Location = new System.Drawing.Point(6, 69);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(402, 278);
            this.panel2.TabIndex = 15;
            // 
            // clientConsole
            // 
            this.clientConsole.AutoSize = true;
            this.clientConsole.Location = new System.Drawing.Point(3, 0);
            this.clientConsole.Name = "clientConsole";
            this.clientConsole.Size = new System.Drawing.Size(0, 16);
            this.clientConsole.TabIndex = 7;
            // 
            // btnSendClient
            // 
            this.btnSendClient.Location = new System.Drawing.Point(333, 353);
            this.btnSendClient.Name = "btnSendClient";
            this.btnSendClient.Size = new System.Drawing.Size(75, 29);
            this.btnSendClient.TabIndex = 13;
            this.btnSendClient.Text = "Send";
            this.btnSendClient.UseVisualStyleBackColor = true;
            this.btnSendClient.Click += new System.EventHandler(this.btnSendClient_Click);
            // 
            // tbClientSend
            // 
            this.tbClientSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.tbClientSend.Location = new System.Drawing.Point(6, 353);
            this.tbClientSend.Multiline = true;
            this.tbClientSend.Name = "tbClientSend";
            this.tbClientSend.Size = new System.Drawing.Size(321, 29);
            this.tbClientSend.TabIndex = 14;
            // 
            // tbIpPort
            // 
            this.tbIpPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIpPort.Location = new System.Drawing.Point(6, 25);
            this.tbIpPort.Name = "tbIpPort";
            this.tbIpPort.Size = new System.Drawing.Size(177, 29);
            this.tbIpPort.TabIndex = 12;
            this.tbIpPort.Text = "192.168.0.129:9092";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(290, 21);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(118, 42);
            this.btnDisconnect.TabIndex = 10;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnClient
            // 
            this.btnClient.Location = new System.Drawing.Point(189, 21);
            this.btnClient.Name = "btnClient";
            this.btnClient.Size = new System.Drawing.Size(95, 42);
            this.btnClient.TabIndex = 9;
            this.btnClient.Text = "Start Client";
            this.btnClient.UseVisualStyleBackColor = true;
            this.btnClient.Click += new System.EventHandler(this.btnClient_Click);
            // 
            // frmSocketServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 417);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSocketServer";
            this.Text = "Socket Server";
            this.Load += new System.EventHandler(this.frmSocketServer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Start_Button;
        private System.Windows.Forms.Button Send_Button;
        private System.Windows.Forms.Button Close_Button;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbMessageSend;
        private System.Windows.Forms.Button Disconnect;
        private System.Windows.Forms.Label serverConsole;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnClient;
        private System.Windows.Forms.TextBox tbIpPort;
        private System.Windows.Forms.Button btnSendClient;
        private System.Windows.Forms.TextBox tbClientSend;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label clientConsole;
    }
}