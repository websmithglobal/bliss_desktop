﻿using System;

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Websmith.Bliss
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.clientConsole = new System.Windows.Forms.Label();
            this.disconnect1 = new System.Windows.Forms.Button();
            this.send1 = new System.Windows.Forms.Button();
            this.input1 = new System.Windows.Forms.TextBox();
            this.start1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.serverConsole = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.disconnect2 = new System.Windows.Forms.Button();
            this.send2 = new System.Windows.Forms.Button();
            this.input2 = new System.Windows.Forms.TextBox();
            this.start2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(-2, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(328, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "TCP CLIENT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(327, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(328, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "TCP SERVER";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.disconnect1);
            this.panel1.Controls.Add(this.send1);
            this.panel1.Controls.Add(this.input1);
            this.panel1.Controls.Add(this.start1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(-2, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(657, 363);
            this.panel1.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(350, 45);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(149, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Clear Console";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BackColor = System.Drawing.Color.LightGray;
            this.panel3.Controls.Add(this.clientConsole);
            this.panel3.Location = new System.Drawing.Point(7, 74);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(647, 250);
            this.panel3.TabIndex = 7;
            // 
            // clientConsole
            // 
            this.clientConsole.AutoSize = true;
            this.clientConsole.Location = new System.Drawing.Point(0, 0);
            this.clientConsole.Name = "clientConsole";
            this.clientConsole.Size = new System.Drawing.Size(0, 13);
            this.clientConsole.TabIndex = 0;
            // 
            // disconnect1
            // 
            this.disconnect1.Location = new System.Drawing.Point(505, 45);
            this.disconnect1.Name = "disconnect1";
            this.disconnect1.Size = new System.Drawing.Size(149, 23);
            this.disconnect1.TabIndex = 6;
            this.disconnect1.Text = "Disconnect";
            this.disconnect1.UseVisualStyleBackColor = true;
            this.disconnect1.Click += new System.EventHandler(this.disconnect1_Click);
            // 
            // send1
            // 
            this.send1.Location = new System.Drawing.Point(505, 340);
            this.send1.Name = "send1";
            this.send1.Size = new System.Drawing.Size(149, 23);
            this.send1.TabIndex = 5;
            this.send1.Text = "Send";
            this.send1.UseVisualStyleBackColor = true;
            this.send1.Click += new System.EventHandler(this.send1_Click);
            // 
            // input1
            // 
            this.input1.Location = new System.Drawing.Point(6, 342);
            this.input1.Name = "input1";
            this.input1.Size = new System.Drawing.Size(493, 20);
            this.input1.TabIndex = 4;
            // 
            // start1
            // 
            this.start1.Location = new System.Drawing.Point(505, 19);
            this.start1.Name = "start1";
            this.start1.Size = new System.Drawing.Size(149, 23);
            this.start1.TabIndex = 2;
            this.start1.Text = "Connect";
            this.start1.UseVisualStyleBackColor = true;
            this.start1.Click += new System.EventHandler(this.connect1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connect To:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(75, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(424, 20);
            this.textBox1.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.AutoScroll = true;
            this.panel6.BackColor = System.Drawing.Color.LightGray;
            this.panel6.Controls.Add(this.serverConsole);
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(350, 250);
            this.panel6.TabIndex = 7;
            // 
            // serverConsole
            // 
            this.serverConsole.AutoSize = true;
            this.serverConsole.Location = new System.Drawing.Point(0, 0);
            this.serverConsole.Name = "serverConsole";
            this.serverConsole.Size = new System.Drawing.Size(0, 13);
            this.serverConsole.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.disconnect2);
            this.panel2.Controls.Add(this.send2);
            this.panel2.Controls.Add(this.input2);
            this.panel2.Controls.Add(this.start2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Location = new System.Drawing.Point(-2, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(657, 363);
            this.panel2.TabIndex = 3;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(352, 52);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(147, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Clear console";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Location = new System.Drawing.Point(6, 81);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(646, 253);
            this.panel4.TabIndex = 7;
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.Location = new System.Drawing.Point(360, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(280, 253);
            this.panel5.TabIndex = 1;
            // 
            // disconnect2
            // 
            this.disconnect2.Location = new System.Drawing.Point(505, 51);
            this.disconnect2.Name = "disconnect2";
            this.disconnect2.Size = new System.Drawing.Size(148, 23);
            this.disconnect2.TabIndex = 6;
            this.disconnect2.Text = "Disconnect";
            this.disconnect2.UseVisualStyleBackColor = true;
            this.disconnect2.Click += new System.EventHandler(this.disconnect2_Click);
            // 
            // send2
            // 
            this.send2.Location = new System.Drawing.Point(505, 340);
            this.send2.Name = "send2";
            this.send2.Size = new System.Drawing.Size(148, 23);
            this.send2.TabIndex = 5;
            this.send2.Text = "Send";
            this.send2.UseVisualStyleBackColor = true;
            this.send2.Click += new System.EventHandler(this.send2_Click);
            // 
            // input2
            // 
            this.input2.Location = new System.Drawing.Point(4, 340);
            this.input2.Name = "input2";
            this.input2.Size = new System.Drawing.Size(495, 20);
            this.input2.TabIndex = 4;
            // 
            // start2
            // 
            this.start2.Location = new System.Drawing.Point(505, 21);
            this.start2.Name = "start2";
            this.start2.Size = new System.Drawing.Size(149, 23);
            this.start2.TabIndex = 2;
            this.start2.Text = "Start Server";
            this.start2.UseVisualStyleBackColor = true;
            this.start2.Click += new System.EventHandler(this.connect2);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(75, 21);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(424, 20);
            this.textBox2.TabIndex = 0;
            this.textBox2.Text = "9091";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 404);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "TCP server/client v1.5";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button start1;

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button start2;

        private System.Windows.Forms.Button disconnect1;
        private System.Windows.Forms.Button send1;
        private System.Windows.Forms.TextBox input1;
        private System.Windows.Forms.Button send2;
        private System.Windows.Forms.TextBox input2;
        private System.Windows.Forms.Button disconnect2;
        private System.Windows.Forms.Panel panel3; // client console container
        private System.Windows.Forms.Panel panel6; // server console container
        private System.Windows.Forms.Label clientConsole;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label serverConsole;
        private Panel panel5;

        ScrollBar vScrollBar1;
        private Button button3;
        private Button button4;
        private Timer timer1;
    }

}

