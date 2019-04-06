namespace Websmith.Bliss
{
    partial class frmGeneral
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
            this.grpPayConfig = new System.Windows.Forms.GroupBox();
            this.grpPrintOption = new System.Windows.Forms.GroupBox();
            this.chkDuplicatePrint = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chkCustNameOnKOT = new System.Windows.Forms.CheckBox();
            this.chkKDSWithoutPrinter = new System.Windows.Forms.CheckBox();
            this.chkRunningOrderOnKOT = new System.Windows.Forms.CheckBox();
            this.chkPrintOnPayment = new System.Windows.Forms.CheckBox();
            this.chkDisplayCardNo = new System.Windows.Forms.CheckBox();
            this.chkRoundingTotal = new System.Windows.Forms.CheckBox();
            this.chkKDSWithoutDisplay = new System.Windows.Forms.CheckBox();
            this.chkKOTOrderType = new System.Windows.Forms.CheckBox();
            this.chkKOTDateTime = new System.Windows.Forms.CheckBox();
            this.chkKOTServerName = new System.Windows.Forms.CheckBox();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.cmbFontSize = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbKOTCount = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFooter = new System.Windows.Forms.TextBox();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.grpDatetimeOption = new System.Windows.Forms.GroupBox();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.grpLanguage = new System.Windows.Forms.GroupBox();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.grpTill = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtTillCur9 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtTillCur8 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtTillCur7 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtTillCur6 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTillCur5 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTillCur4 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTillCur3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTillCur2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTillCur1 = new System.Windows.Forms.TextBox();
            this.grpTaxOnDine = new System.Windows.Forms.GroupBox();
            this.chkPartyEvent = new System.Windows.Forms.CheckBox();
            this.chkQueue = new System.Windows.Forms.CheckBox();
            this.chkOrderAhead = new System.Windows.Forms.CheckBox();
            this.chkDelivery = new System.Windows.Forms.CheckBox();
            this.chkTakeOut = new System.Windows.Forms.CheckBox();
            this.chkDineIn = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPer2 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtTaxLabel2 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPer1 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtTaxLabel1 = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpPrintOption.SuspendLayout();
            this.grpDatetimeOption.SuspendLayout();
            this.grpLanguage.SuspendLayout();
            this.grpTill.SuspendLayout();
            this.grpTaxOnDine.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPayConfig
            // 
            this.grpPayConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPayConfig.ForeColor = System.Drawing.Color.White;
            this.grpPayConfig.Location = new System.Drawing.Point(14, 12);
            this.grpPayConfig.Name = "grpPayConfig";
            this.grpPayConfig.Size = new System.Drawing.Size(684, 68);
            this.grpPayConfig.TabIndex = 0;
            this.grpPayConfig.TabStop = false;
            this.grpPayConfig.Text = "Payment Gateway Configuration";
            // 
            // grpPrintOption
            // 
            this.grpPrintOption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPrintOption.Controls.Add(this.chkDuplicatePrint);
            this.grpPrintOption.Controls.Add(this.label14);
            this.grpPrintOption.Controls.Add(this.chkCustNameOnKOT);
            this.grpPrintOption.Controls.Add(this.chkKDSWithoutPrinter);
            this.grpPrintOption.Controls.Add(this.chkRunningOrderOnKOT);
            this.grpPrintOption.Controls.Add(this.chkPrintOnPayment);
            this.grpPrintOption.Controls.Add(this.chkDisplayCardNo);
            this.grpPrintOption.Controls.Add(this.chkRoundingTotal);
            this.grpPrintOption.Controls.Add(this.chkKDSWithoutDisplay);
            this.grpPrintOption.Controls.Add(this.chkKOTOrderType);
            this.grpPrintOption.Controls.Add(this.chkKOTDateTime);
            this.grpPrintOption.Controls.Add(this.chkKOTServerName);
            this.grpPrintOption.Controls.Add(this.txtPrefix);
            this.grpPrintOption.Controls.Add(this.cmbFontSize);
            this.grpPrintOption.Controls.Add(this.label6);
            this.grpPrintOption.Controls.Add(this.label5);
            this.grpPrintOption.Controls.Add(this.cmbKOTCount);
            this.grpPrintOption.Controls.Add(this.label4);
            this.grpPrintOption.Controls.Add(this.label2);
            this.grpPrintOption.Controls.Add(this.label1);
            this.grpPrintOption.Controls.Add(this.txtFooter);
            this.grpPrintOption.Controls.Add(this.txtHeader);
            this.grpPrintOption.ForeColor = System.Drawing.Color.White;
            this.grpPrintOption.Location = new System.Drawing.Point(14, 86);
            this.grpPrintOption.Name = "grpPrintOption";
            this.grpPrintOption.Size = new System.Drawing.Size(684, 225);
            this.grpPrintOption.TabIndex = 1;
            this.grpPrintOption.TabStop = false;
            this.grpPrintOption.Text = "Print Option";
            // 
            // chkDuplicatePrint
            // 
            this.chkDuplicatePrint.AutoSize = true;
            this.chkDuplicatePrint.Location = new System.Drawing.Point(9, 88);
            this.chkDuplicatePrint.Name = "chkDuplicatePrint";
            this.chkDuplicatePrint.Size = new System.Drawing.Size(122, 22);
            this.chkDuplicatePrint.TabIndex = 4;
            this.chkDuplicatePrint.Text = "Duplicate Print";
            this.chkDuplicatePrint.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 116);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(673, 18);
            this.label14.TabIndex = 22;
            this.label14.Text = "---------------------------------------------------------------------------------" +
    "----------------------------------------------------";
            // 
            // chkCustNameOnKOT
            // 
            this.chkCustNameOnKOT.AutoSize = true;
            this.chkCustNameOnKOT.Location = new System.Drawing.Point(9, 195);
            this.chkCustNameOnKOT.Name = "chkCustNameOnKOT";
            this.chkCustNameOnKOT.Size = new System.Drawing.Size(196, 22);
            this.chkCustNameOnKOT.TabIndex = 13;
            this.chkCustNameOnKOT.Text = "Customer Name On KOT";
            this.chkCustNameOnKOT.UseVisualStyleBackColor = true;
            // 
            // chkKDSWithoutPrinter
            // 
            this.chkKDSWithoutPrinter.AutoSize = true;
            this.chkKDSWithoutPrinter.Location = new System.Drawing.Point(463, 195);
            this.chkKDSWithoutPrinter.Name = "chkKDSWithoutPrinter";
            this.chkKDSWithoutPrinter.Size = new System.Drawing.Size(160, 22);
            this.chkKDSWithoutPrinter.TabIndex = 15;
            this.chkKDSWithoutPrinter.Text = "KDS Without Printer";
            this.chkKDSWithoutPrinter.UseVisualStyleBackColor = true;
            // 
            // chkRunningOrderOnKOT
            // 
            this.chkRunningOrderOnKOT.AutoSize = true;
            this.chkRunningOrderOnKOT.Location = new System.Drawing.Point(223, 195);
            this.chkRunningOrderOnKOT.Name = "chkRunningOrderOnKOT";
            this.chkRunningOrderOnKOT.Size = new System.Drawing.Size(234, 22);
            this.chkRunningOrderOnKOT.TabIndex = 14;
            this.chkRunningOrderOnKOT.Text = "Running Order Display On KOT";
            this.chkRunningOrderOnKOT.UseVisualStyleBackColor = true;
            // 
            // chkPrintOnPayment
            // 
            this.chkPrintOnPayment.AutoSize = true;
            this.chkPrintOnPayment.Location = new System.Drawing.Point(153, 88);
            this.chkPrintOnPayment.Name = "chkPrintOnPayment";
            this.chkPrintOnPayment.Size = new System.Drawing.Size(183, 22);
            this.chkPrintOnPayment.TabIndex = 5;
            this.chkPrintOnPayment.Text = "Print On Payment Done";
            this.chkPrintOnPayment.UseVisualStyleBackColor = true;
            // 
            // chkDisplayCardNo
            // 
            this.chkDisplayCardNo.AutoSize = true;
            this.chkDisplayCardNo.Location = new System.Drawing.Point(463, 167);
            this.chkDisplayCardNo.Name = "chkDisplayCardNo";
            this.chkDisplayCardNo.Size = new System.Drawing.Size(139, 22);
            this.chkDisplayCardNo.TabIndex = 12;
            this.chkDisplayCardNo.Text = "Display Card No.";
            this.chkDisplayCardNo.UseVisualStyleBackColor = true;
            // 
            // chkRoundingTotal
            // 
            this.chkRoundingTotal.AutoSize = true;
            this.chkRoundingTotal.Location = new System.Drawing.Point(223, 167);
            this.chkRoundingTotal.Name = "chkRoundingTotal";
            this.chkRoundingTotal.Size = new System.Drawing.Size(127, 22);
            this.chkRoundingTotal.TabIndex = 11;
            this.chkRoundingTotal.Text = "Rounding Total";
            this.chkRoundingTotal.UseVisualStyleBackColor = true;
            // 
            // chkKDSWithoutDisplay
            // 
            this.chkKDSWithoutDisplay.AutoSize = true;
            this.chkKDSWithoutDisplay.Location = new System.Drawing.Point(9, 167);
            this.chkKDSWithoutDisplay.Name = "chkKDSWithoutDisplay";
            this.chkKDSWithoutDisplay.Size = new System.Drawing.Size(165, 22);
            this.chkKDSWithoutDisplay.TabIndex = 10;
            this.chkKDSWithoutDisplay.Text = "KDS Without Display";
            this.chkKDSWithoutDisplay.UseVisualStyleBackColor = true;
            // 
            // chkKOTOrderType
            // 
            this.chkKOTOrderType.AutoSize = true;
            this.chkKOTOrderType.Location = new System.Drawing.Point(463, 139);
            this.chkKOTOrderType.Name = "chkKOTOrderType";
            this.chkKOTOrderType.Size = new System.Drawing.Size(136, 22);
            this.chkKOTOrderType.TabIndex = 9;
            this.chkKOTOrderType.Text = "KOT Order Type";
            this.chkKOTOrderType.UseVisualStyleBackColor = true;
            // 
            // chkKOTDateTime
            // 
            this.chkKOTDateTime.AutoSize = true;
            this.chkKOTDateTime.Location = new System.Drawing.Point(223, 139);
            this.chkKOTDateTime.Name = "chkKOTDateTime";
            this.chkKOTDateTime.Size = new System.Drawing.Size(130, 22);
            this.chkKOTDateTime.TabIndex = 8;
            this.chkKOTDateTime.Text = "KOT Date Time";
            this.chkKOTDateTime.UseVisualStyleBackColor = true;
            // 
            // chkKOTServerName
            // 
            this.chkKOTServerName.AutoSize = true;
            this.chkKOTServerName.Location = new System.Drawing.Point(9, 139);
            this.chkKOTServerName.Name = "chkKOTServerName";
            this.chkKOTServerName.Size = new System.Drawing.Size(149, 22);
            this.chkKOTServerName.TabIndex = 7;
            this.chkKOTServerName.Text = "KOT Server Name";
            this.chkKOTServerName.UseVisualStyleBackColor = true;
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(72, 54);
            this.txtPrefix.MaxLength = 5;
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(264, 24);
            this.txtPrefix.TabIndex = 2;
            // 
            // cmbFontSize
            // 
            this.cmbFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFontSize.FormattingEnabled = true;
            this.cmbFontSize.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cmbFontSize.Location = new System.Drawing.Point(463, 86);
            this.cmbFontSize.Name = "cmbFontSize";
            this.cmbFontSize.Size = new System.Drawing.Size(210, 26);
            this.cmbFontSize.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(347, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 18);
            this.label6.TabIndex = 9;
            this.label6.Text = "KOT Font Size:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 18);
            this.label5.TabIndex = 8;
            this.label5.Text = "Prefix:";
            // 
            // cmbKOTCount
            // 
            this.cmbKOTCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKOTCount.FormattingEnabled = true;
            this.cmbKOTCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmbKOTCount.Location = new System.Drawing.Point(463, 54);
            this.cmbKOTCount.Name = "cmbKOTCount";
            this.cmbKOTCount.Size = new System.Drawing.Size(210, 26);
            this.cmbKOTCount.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(347, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "KOT Count:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(347, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Footer:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Header:";
            // 
            // txtFooter
            // 
            this.txtFooter.Location = new System.Drawing.Point(409, 24);
            this.txtFooter.Name = "txtFooter";
            this.txtFooter.Size = new System.Drawing.Size(264, 24);
            this.txtFooter.TabIndex = 1;
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(72, 24);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(264, 24);
            this.txtHeader.TabIndex = 0;
            // 
            // grpDatetimeOption
            // 
            this.grpDatetimeOption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatetimeOption.Controls.Add(this.cmbFormat);
            this.grpDatetimeOption.Controls.Add(this.label7);
            this.grpDatetimeOption.ForeColor = System.Drawing.Color.White;
            this.grpDatetimeOption.Location = new System.Drawing.Point(14, 317);
            this.grpDatetimeOption.Name = "grpDatetimeOption";
            this.grpDatetimeOption.Size = new System.Drawing.Size(336, 68);
            this.grpDatetimeOption.TabIndex = 2;
            this.grpDatetimeOption.TabStop = false;
            this.grpDatetimeOption.Text = "Date Time Option";
            // 
            // cmbFormat
            // 
            this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Items.AddRange(new object[] {
            "dd/MM/yyyy",
            "dd/MMM/yyyy",
            "dd/MMMM/yyyy",
            "MM/dd/yyyy",
            "MMM/dd/yyyy",
            "MMMM/dd/yyyy"});
            this.cmbFormat.Location = new System.Drawing.Point(124, 25);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(202, 26);
            this.cmbFormat.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 18);
            this.label7.TabIndex = 6;
            this.label7.Text = "Select Format:";
            // 
            // grpLanguage
            // 
            this.grpLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLanguage.Controls.Add(this.cmbLanguage);
            this.grpLanguage.Controls.Add(this.label8);
            this.grpLanguage.ForeColor = System.Drawing.Color.White;
            this.grpLanguage.Location = new System.Drawing.Point(362, 317);
            this.grpLanguage.Name = "grpLanguage";
            this.grpLanguage.Size = new System.Drawing.Size(336, 68);
            this.grpLanguage.TabIndex = 3;
            this.grpLanguage.TabStop = false;
            this.grpLanguage.Text = "Language";
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Items.AddRange(new object[] {
            "English"});
            this.cmbLanguage.Location = new System.Drawing.Point(138, 25);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(188, 26);
            this.cmbLanguage.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 18);
            this.label8.TabIndex = 6;
            this.label8.Text = "Select Language:";
            // 
            // grpTill
            // 
            this.grpTill.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTill.Controls.Add(this.label19);
            this.grpTill.Controls.Add(this.txtTillCur9);
            this.grpTill.Controls.Add(this.label20);
            this.grpTill.Controls.Add(this.txtTillCur8);
            this.grpTill.Controls.Add(this.label21);
            this.grpTill.Controls.Add(this.txtTillCur7);
            this.grpTill.Controls.Add(this.label22);
            this.grpTill.Controls.Add(this.txtTillCur6);
            this.grpTill.Controls.Add(this.label13);
            this.grpTill.Controls.Add(this.txtTillCur5);
            this.grpTill.Controls.Add(this.label12);
            this.grpTill.Controls.Add(this.txtTillCur4);
            this.grpTill.Controls.Add(this.label11);
            this.grpTill.Controls.Add(this.txtTillCur3);
            this.grpTill.Controls.Add(this.label10);
            this.grpTill.Controls.Add(this.txtTillCur2);
            this.grpTill.Controls.Add(this.label9);
            this.grpTill.Controls.Add(this.txtTillCur1);
            this.grpTill.ForeColor = System.Drawing.Color.White;
            this.grpTill.Location = new System.Drawing.Point(14, 391);
            this.grpTill.Name = "grpTill";
            this.grpTill.Size = new System.Drawing.Size(684, 83);
            this.grpTill.TabIndex = 4;
            this.grpTill.TabStop = false;
            this.grpTill.Text = "Till";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(409, 52);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(66, 18);
            this.label19.TabIndex = 21;
            this.label19.Text = "Till Cur9:";
            // 
            // txtTillCur9
            // 
            this.txtTillCur9.Location = new System.Drawing.Point(481, 49);
            this.txtTillCur9.MaxLength = 4;
            this.txtTillCur9.Name = "txtTillCur9";
            this.txtTillCur9.Size = new System.Drawing.Size(50, 24);
            this.txtTillCur9.TabIndex = 8;
            this.txtTillCur9.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPer2_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(281, 52);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(66, 18);
            this.label20.TabIndex = 20;
            this.label20.Text = "Till Cur8:";
            // 
            // txtTillCur8
            // 
            this.txtTillCur8.Location = new System.Drawing.Point(353, 49);
            this.txtTillCur8.MaxLength = 4;
            this.txtTillCur8.Name = "txtTillCur8";
            this.txtTillCur8.Size = new System.Drawing.Size(50, 24);
            this.txtTillCur8.TabIndex = 7;
            this.txtTillCur8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPer2_KeyPress);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(153, 52);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(66, 18);
            this.label21.TabIndex = 19;
            this.label21.Text = "Till Cur7:";
            // 
            // txtTillCur7
            // 
            this.txtTillCur7.Location = new System.Drawing.Point(225, 49);
            this.txtTillCur7.MaxLength = 4;
            this.txtTillCur7.Name = "txtTillCur7";
            this.txtTillCur7.Size = new System.Drawing.Size(50, 24);
            this.txtTillCur7.TabIndex = 6;
            this.txtTillCur7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPer2_KeyPress);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(25, 52);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(66, 18);
            this.label22.TabIndex = 18;
            this.label22.Text = "Till Cur6:";
            // 
            // txtTillCur6
            // 
            this.txtTillCur6.Location = new System.Drawing.Point(97, 49);
            this.txtTillCur6.MaxLength = 4;
            this.txtTillCur6.Name = "txtTillCur6";
            this.txtTillCur6.Size = new System.Drawing.Size(50, 24);
            this.txtTillCur6.TabIndex = 5;
            this.txtTillCur6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPer2_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(537, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 18);
            this.label13.TabIndex = 12;
            this.label13.Text = "Till Cur5:";
            // 
            // txtTillCur5
            // 
            this.txtTillCur5.Location = new System.Drawing.Point(617, 19);
            this.txtTillCur5.MaxLength = 4;
            this.txtTillCur5.Name = "txtTillCur5";
            this.txtTillCur5.Size = new System.Drawing.Size(50, 24);
            this.txtTillCur5.TabIndex = 4;
            this.txtTillCur5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPer2_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(409, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 18);
            this.label12.TabIndex = 10;
            this.label12.Text = "Till Cur4:";
            // 
            // txtTillCur4
            // 
            this.txtTillCur4.Location = new System.Drawing.Point(481, 19);
            this.txtTillCur4.MaxLength = 4;
            this.txtTillCur4.Name = "txtTillCur4";
            this.txtTillCur4.Size = new System.Drawing.Size(50, 24);
            this.txtTillCur4.TabIndex = 3;
            this.txtTillCur4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPer2_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(281, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 18);
            this.label11.TabIndex = 8;
            this.label11.Text = "Till Cur3:";
            // 
            // txtTillCur3
            // 
            this.txtTillCur3.Location = new System.Drawing.Point(353, 19);
            this.txtTillCur3.MaxLength = 4;
            this.txtTillCur3.Name = "txtTillCur3";
            this.txtTillCur3.Size = new System.Drawing.Size(50, 24);
            this.txtTillCur3.TabIndex = 2;
            this.txtTillCur3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPer2_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(153, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 18);
            this.label10.TabIndex = 6;
            this.label10.Text = "Till Cur2:";
            // 
            // txtTillCur2
            // 
            this.txtTillCur2.Location = new System.Drawing.Point(225, 19);
            this.txtTillCur2.MaxLength = 4;
            this.txtTillCur2.Name = "txtTillCur2";
            this.txtTillCur2.Size = new System.Drawing.Size(50, 24);
            this.txtTillCur2.TabIndex = 1;
            this.txtTillCur2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPer2_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 18);
            this.label9.TabIndex = 4;
            this.label9.Text = "Till Cur1:";
            // 
            // txtTillCur1
            // 
            this.txtTillCur1.Location = new System.Drawing.Point(97, 19);
            this.txtTillCur1.MaxLength = 4;
            this.txtTillCur1.Name = "txtTillCur1";
            this.txtTillCur1.Size = new System.Drawing.Size(50, 24);
            this.txtTillCur1.TabIndex = 0;
            this.txtTillCur1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPer2_KeyPress);
            // 
            // grpTaxOnDine
            // 
            this.grpTaxOnDine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTaxOnDine.Controls.Add(this.chkPartyEvent);
            this.grpTaxOnDine.Controls.Add(this.chkQueue);
            this.grpTaxOnDine.Controls.Add(this.chkOrderAhead);
            this.grpTaxOnDine.Controls.Add(this.chkDelivery);
            this.grpTaxOnDine.Controls.Add(this.chkTakeOut);
            this.grpTaxOnDine.Controls.Add(this.chkDineIn);
            this.grpTaxOnDine.ForeColor = System.Drawing.Color.White;
            this.grpTaxOnDine.Location = new System.Drawing.Point(14, 480);
            this.grpTaxOnDine.Name = "grpTaxOnDine";
            this.grpTaxOnDine.Size = new System.Drawing.Size(684, 78);
            this.grpTaxOnDine.TabIndex = 5;
            this.grpTaxOnDine.TabStop = false;
            this.grpTaxOnDine.Text = "Tax On Dine Option";
            // 
            // chkPartyEvent
            // 
            this.chkPartyEvent.AutoSize = true;
            this.chkPartyEvent.Location = new System.Drawing.Point(481, 47);
            this.chkPartyEvent.Name = "chkPartyEvent";
            this.chkPartyEvent.Size = new System.Drawing.Size(136, 22);
            this.chkPartyEvent.TabIndex = 5;
            this.chkPartyEvent.Text = "PARTY / EVENT";
            this.chkPartyEvent.UseVisualStyleBackColor = true;
            // 
            // chkQueue
            // 
            this.chkQueue.AutoSize = true;
            this.chkQueue.Location = new System.Drawing.Point(284, 47);
            this.chkQueue.Name = "chkQueue";
            this.chkQueue.Size = new System.Drawing.Size(81, 22);
            this.chkQueue.TabIndex = 4;
            this.chkQueue.Text = "QUEUE";
            this.chkQueue.UseVisualStyleBackColor = true;
            // 
            // chkOrderAhead
            // 
            this.chkOrderAhead.AutoSize = true;
            this.chkOrderAhead.Location = new System.Drawing.Point(28, 47);
            this.chkOrderAhead.Name = "chkOrderAhead";
            this.chkOrderAhead.Size = new System.Drawing.Size(136, 22);
            this.chkOrderAhead.TabIndex = 3;
            this.chkOrderAhead.Text = "ORDER AHEAD";
            this.chkOrderAhead.UseVisualStyleBackColor = true;
            // 
            // chkDelivery
            // 
            this.chkDelivery.AutoSize = true;
            this.chkDelivery.Location = new System.Drawing.Point(481, 19);
            this.chkDelivery.Name = "chkDelivery";
            this.chkDelivery.Size = new System.Drawing.Size(98, 22);
            this.chkDelivery.TabIndex = 2;
            this.chkDelivery.Text = "DELIVERY";
            this.chkDelivery.UseVisualStyleBackColor = true;
            // 
            // chkTakeOut
            // 
            this.chkTakeOut.AutoSize = true;
            this.chkTakeOut.Location = new System.Drawing.Point(284, 19);
            this.chkTakeOut.Name = "chkTakeOut";
            this.chkTakeOut.Size = new System.Drawing.Size(101, 22);
            this.chkTakeOut.TabIndex = 1;
            this.chkTakeOut.Text = "TAKE OUT";
            this.chkTakeOut.UseVisualStyleBackColor = true;
            // 
            // chkDineIn
            // 
            this.chkDineIn.AutoSize = true;
            this.chkDineIn.Location = new System.Drawing.Point(28, 19);
            this.chkDineIn.Name = "chkDineIn";
            this.chkDineIn.Size = new System.Drawing.Size(81, 22);
            this.chkDineIn.TabIndex = 0;
            this.chkDineIn.Text = "DINE-IN";
            this.chkDineIn.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(477, 631);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 36);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "&SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtPer2);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtTaxLabel2);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtPer1);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txtTaxLabel1);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(14, 564);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(684, 55);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Order Tax Setting";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(555, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 18);
            this.label15.TabIndex = 10;
            this.label15.Text = "Per (%):";
            // 
            // txtPer2
            // 
            this.txtPer2.Location = new System.Drawing.Point(623, 19);
            this.txtPer2.MaxLength = 5;
            this.txtPer2.Name = "txtPer2";
            this.txtPer2.Size = new System.Drawing.Size(50, 24);
            this.txtPer2.TabIndex = 3;
            this.txtPer2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPer2_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(347, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 18);
            this.label16.TabIndex = 8;
            this.label16.Text = "Tax Label-2:";
            // 
            // txtTaxLabel2
            // 
            this.txtTaxLabel2.Location = new System.Drawing.Point(441, 19);
            this.txtTaxLabel2.Name = "txtTaxLabel2";
            this.txtTaxLabel2.Size = new System.Drawing.Size(108, 24);
            this.txtTaxLabel2.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(221, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(62, 18);
            this.label17.TabIndex = 6;
            this.label17.Text = "Per (%):";
            // 
            // txtPer1
            // 
            this.txtPer1.Location = new System.Drawing.Point(289, 19);
            this.txtPer1.MaxLength = 5;
            this.txtPer1.Name = "txtPer1";
            this.txtPer1.Size = new System.Drawing.Size(50, 24);
            this.txtPer1.TabIndex = 1;
            this.txtPer1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPer1_KeyPress);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(88, 18);
            this.label18.TabIndex = 4;
            this.label18.Text = "Tax Label-1:";
            // 
            // txtTaxLabel1
            // 
            this.txtTaxLabel1.Location = new System.Drawing.Point(100, 19);
            this.txtTaxLabel1.Name = "txtTaxLabel1";
            this.txtTaxLabel1.Size = new System.Drawing.Size(117, 24);
            this.txtTaxLabel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Teal;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(595, 631);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 36);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "&CLOSE";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(710, 677);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpTaxOnDine);
            this.Controls.Add(this.grpTill);
            this.Controls.Add(this.grpLanguage);
            this.Controls.Add(this.grpDatetimeOption);
            this.Controls.Add(this.grpPrintOption);
            this.Controls.Add(this.grpPayConfig);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGeneral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "General Setting";
            this.Load += new System.EventHandler(this.frmGeneral_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGeneral_KeyDown);
            this.grpPrintOption.ResumeLayout(false);
            this.grpPrintOption.PerformLayout();
            this.grpDatetimeOption.ResumeLayout(false);
            this.grpDatetimeOption.PerformLayout();
            this.grpLanguage.ResumeLayout(false);
            this.grpLanguage.PerformLayout();
            this.grpTill.ResumeLayout(false);
            this.grpTill.PerformLayout();
            this.grpTaxOnDine.ResumeLayout(false);
            this.grpTaxOnDine.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPayConfig;
        private System.Windows.Forms.GroupBox grpPrintOption;
        private System.Windows.Forms.TextBox txtFooter;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbKOTCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbFontSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.CheckBox chkKOTOrderType;
        private System.Windows.Forms.CheckBox chkKOTDateTime;
        private System.Windows.Forms.CheckBox chkKOTServerName;
        private System.Windows.Forms.CheckBox chkCustNameOnKOT;
        private System.Windows.Forms.CheckBox chkKDSWithoutPrinter;
        private System.Windows.Forms.CheckBox chkRunningOrderOnKOT;
        private System.Windows.Forms.CheckBox chkPrintOnPayment;
        private System.Windows.Forms.CheckBox chkDisplayCardNo;
        private System.Windows.Forms.CheckBox chkRoundingTotal;
        private System.Windows.Forms.CheckBox chkKDSWithoutDisplay;
        private System.Windows.Forms.GroupBox grpDatetimeOption;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox grpLanguage;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox grpTill;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtTillCur5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTillCur4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTillCur3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTillCur2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTillCur1;
        private System.Windows.Forms.GroupBox grpTaxOnDine;
        private System.Windows.Forms.CheckBox chkPartyEvent;
        private System.Windows.Forms.CheckBox chkQueue;
        private System.Windows.Forms.CheckBox chkOrderAhead;
        private System.Windows.Forms.CheckBox chkDelivery;
        private System.Windows.Forms.CheckBox chkTakeOut;
        private System.Windows.Forms.CheckBox chkDineIn;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkDuplicatePrint;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtPer2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtTaxLabel2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtPer1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtTaxLabel1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtTillCur9;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtTillCur8;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtTillCur7;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtTillCur6;
        private System.Windows.Forms.Button btnClose;
    }
}