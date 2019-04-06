using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DAL = Websmith.DataLayer;
using ENT = Websmith.Entity;

namespace Websmith.Bliss
{
    public partial class frmModifiers : Form
    {
        DAL.ModifierCategoryDetail objDALMCD = new DAL.ModifierCategoryDetail();
        ENT.ModifierCategoryDetail objENTMCD = new ENT.ModifierCategoryDetail();
        List<ENT.ModifierCategoryDetail> lstENTMCD = new List<ENT.ModifierCategoryDetail>();
        
        DAL.OrderWiseModifier objDALOWM = new DAL.OrderWiseModifier();
        ENT.OrderWiseModifier objENTOWM = new ENT.OrderWiseModifier();
        List<ENT.OrderWiseModifier> lstENTOWM = new List<ENT.OrderWiseModifier>();
        
        public frmModifiers()
        {
            InitializeComponent();
        }

        public frmModifiers(string Order_ID, string Transaction_ID, string Product_ID)
        {
            InitializeComponent();
            txtOrderID.Text = Order_ID;
            txtProductID.Text = Product_ID;
            txtTransID.Text = Transaction_ID;
        }

        #region Category Penal

        private bool CheckDuplicateIngredients(string IngredientsID)
        {
            bool result = false;
            try
            {
                objENTOWM.OrderID = new Guid(txtOrderID.Text);
                objENTOWM.ProductID = new Guid(txtProductID.Text);
                objENTOWM.TransactionID = new Guid(txtTransID.Text);
                objENTOWM.IngredientsID = new Guid(IngredientsID);
                objENTOWM.Mode = "GetByOrderProductTransactionIngredientsID";
                lstENTOWM = objDALOWM.GetOrderWiseModifier(objENTOWM);
                if (lstENTOWM.Count > 0)
                    result = true;
                else
                    result = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        private void getModifiersCategory()
        {
            try
            {
                objENTMCD.Mode = "GetRecordByProductID";
                objENTMCD.ProductID = new Guid(txtProductID.Text.Trim());
                lstENTMCD = objDALMCD.getModifierCategoryDetail(objENTMCD);

                if (lstENTMCD.Count == 0)
                {
                    return;
                }

                int pnl_width = 0;
                int x = 0;
                int y = 8;
                int i = 0;
                int col = 0;
                double row = 0;

                if (pnlModiCategory.Width < 190)
                {
                    col = 1;
                    pnl_width = (pnlModiCategory.Width - 120) / 2;
                    if (lstENTMCD.Count < col)
                        row = lstENTMCD.Count == 0 ? 0 : 1;
                    else
                        row = lstENTMCD.Count;
                }

                if (lstENTMCD.Count > 5)
                { pnl_width = pnl_width / 2; }

                for (int h = 0; h < row; h++)
                {
                    x = pnl_width;
                    for (int v = 0; v < col; v++)
                    {
                        Button button = new Button();
                        button.Location = new Point(x, y);
                        button.Click += new EventHandler(CategoryButton_ClickEvent);
                        button.Name = lstENTMCD[i].ModifierCategoryID.ToString();
                        button.Tag = lstENTMCD[i].ModifierCategoryDetail_Id.ToString();
                        button.Text = lstENTMCD[i].ModifierCategoryName;
                        button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)))));
                        button.Size = new System.Drawing.Size(120, 70);
                        button.BackColor = Color.Green; 
                        button.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                        button.ForeColor = Color.White;
                        button.FlatStyle = FlatStyle.Flat;
                        pnlModiCategory.Controls.Add(button);
                        if (i == lstENTMCD.Count - 1)
                        {
                            break;
                        }
                        i++;
                    }
                    y = y + 78;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CategoryButton_ClickEvent(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                foreach (Control ctrl in pnlModiCategory.Controls)
                {
                    if (ctrl.GetType() == typeof(Button))
                    {
                        ctrl.BackColor = Color.Green;
                    }                    
                }
                if (button != null)
                {
                    for (int n = 0; n <= lstENTMCD.Count - 1; n++)
                    {
                        if (button.Tag.ToString() == lstENTMCD[n].ModifierCategoryDetail_Id.ToString())
                        {
                            if (lstENTMCD.Count != 0)
                            {
                                button.BackColor = Color.DarkOrange;
                                this.getModifierDetail(lstENTMCD[n].ModifierCategoryDetail_Id);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getModifierDetail(int ModifierCategoryDetailId)
        {
            try
            {
                DAL.ModifierDetail objDALMD = new DAL.ModifierDetail();
                ENT.ModifierDetail objENTMD = new ENT.ModifierDetail();
                List<ENT.ModifierDetail> lstENTMD = new List<ENT.ModifierDetail>();

                objENTMD.Mode = "GetRecordByMCDID";
                objENTMD.ModifierCategoryDetail_Id = ModifierCategoryDetailId;
                lstENTMD = objDALMD.getModifierDetail(objENTMD);

                pnlModifier.Controls.Clear();
                int pnl_width = 0;
                int x = 0;
                int y = 12;
                int i = 0;
                int col = 0;
                double row = 0;

                if (lstENTMD.Count == 0)
                {
                    return;
                }

                if (pnlModifier.Width > 499)
                {
                    col = 3;
                    pnl_width = (pnlModifier.Width - 492) / 2;
                    if (lstENTMD.Count <= col)
                        row = lstENTMD.Count == 0 ? 0 : 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstENTMD.Count) / col, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    col = 2;
                    pnl_width = (pnlModifier.Width - 326) / 2;
                    if (lstENTMD.Count <= col)
                        row = lstENTMD.Count == 0 ? 0 : 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstENTMD.Count) / col, 2, MidpointRounding.AwayFromZero);
                }

                if (lstENTMD.Count > 8)
                {
                    pnl_width = pnl_width - 10;
                }

                for (int h = 0; h < row; h++)
                {
                    x = pnl_width;
                    for (int v = 0; v < col; v++)
                    {
                        #region Generate Dynamic Button
                        Button button_plus = new Button();
                        button_plus.Location = new Point(125, 3);
                        button_plus.Size = new System.Drawing.Size(30, 30);
                        button_plus.Name = lstENTMD[i].IngredientsID.ToString();
                        button_plus.Tag = lstENTMD[i].ModifierCategoryDetail_Id.ToString();
                        button_plus.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                        button_plus.BackColor = Color.White;
                        button_plus.ForeColor = Color.Black;
                        button_plus.FlatStyle = FlatStyle.Flat;
                        button_plus.Text = "+";
                        button_plus.UseVisualStyleBackColor = false;
                        button_plus.Click += new EventHandler(ButtonPlus_ClickEvent);

                        Button button_minus = new Button();
                        button_minus.Location = new Point(3, 3);
                        button_minus.Size = new System.Drawing.Size(30, 30);
                        button_minus.Name = lstENTMD[i].IngredientsID.ToString();
                        button_minus.Tag = lstENTMD[i].ModifierCategoryDetail_Id.ToString();
                        button_minus.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                        button_minus.BackColor = Color.White;
                        button_minus.ForeColor = Color.Black;
                        button_minus.FlatStyle = FlatStyle.Flat;
                        button_minus.Text = "-";
                        button_minus.UseVisualStyleBackColor = false;
                        button_minus.Click += new EventHandler(ButtonMinus_ClickEvent);

                        Label lblItemName = new Label();
                        lblItemName.AutoSize = true;
                        lblItemName.Location = new System.Drawing.Point(3, 35);
                        lblItemName.Name = lstENTMD[i].ModifierCategoryDetail_Id.ToString(); //"lblOCTableName" + i;
                        lblItemName.Tag = lstENTMD[i].IngredientsID;
                        lblItemName.Text = lstENTMD[i].Name;
                        lblItemName.ForeColor = Color.White;
                        lblItemName.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                        lblItemName.Click += new EventHandler(PanelModifiersLabel_ClickEvent);

                        Label lblAmount = new Label();
                        lblAmount.AutoSize = true;
                        lblAmount.Location = new System.Drawing.Point(3, 57);
                        lblAmount.Name = lstENTMD[i].ModifierCategoryDetail_Id.ToString(); //"lblOCAmount" + i;
                        lblAmount.Tag = lstENTMD[i].IngredientsID;
                        lblAmount.Text = "Rs. " + lstENTMD[i].Price.ToString();
                        lblAmount.ForeColor = Color.White;
                        lblAmount.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                        lblAmount.Click += new EventHandler(PanelModifiersLabel_ClickEvent);

                        Panel pnlButton = new Panel();
                        pnlButton.Location = new Point(x, y);
                        pnlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)))));
                        pnlButton.Size = new System.Drawing.Size(160, 84);
                        if (lstENTMD[i].IsDefault)
                        {
                            pnlButton.BackColor = Color.DarkOrange;
                        }
                        else
                        {
                            pnlButton.BackColor = ChnagePanelModifiersColor(lstENTMD[i].IngredientsID.ToString()); //Green;
                        }
                        pnlButton.BorderStyle = BorderStyle.FixedSingle;
                        pnlButton.Tag = lstENTMD[i].IngredientsID;
                        pnlButton.Name = lstENTMD[i].ModifierCategoryDetail_Id.ToString();
                        pnlButton.TabIndex = i;
                        pnlButton.Controls.Add(lblItemName);
                        pnlButton.Controls.Add(button_plus);
                        pnlButton.Controls.Add(button_minus);
                        pnlButton.Controls.Add(lblAmount);
                        pnlButton.Click += new EventHandler(PanelModifiers_ClickEvent);

                        pnlModifier.Controls.Add(pnlButton);
                        #endregion

                        if (i == lstENTMD.Count - 1)
                        {
                            break;
                        }
                        i++;
                        x = x + 166;
                    }
                    y = y + 90;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Color ChnagePanelModifiersColor(string IngredientsID)
        {
            Color clr = new Color();
            try
            {
                objENTOWM.OrderID = new Guid(txtOrderID.Text);
                objENTOWM.ProductID = new Guid(txtProductID.Text);
                objENTOWM.TransactionID = new Guid(txtTransID.Text);
                objENTOWM.IngredientsID = new Guid(IngredientsID);
                objENTOWM.Mode = "GetByOrderProductTransactionIngredientsID";
                lstENTOWM = objDALOWM.GetOrderWiseModifier(objENTOWM);
                if (lstENTOWM.Count > 0)
                {
                    if (lstENTOWM[0].ModifierOption == "NO")
                        clr = Color.DarkRed;
                    else if (lstENTOWM[0].ModifierOption == "ONLY")
                        clr = Color.YellowGreen;
                    else if (lstENTOWM[0].ModifierOption == "SIDE")
                        clr = Color.Peru;
                    else
                        clr = Color.Green;
                }
                else
                {
                    return Color.Green;
                }
            }
            catch (Exception)
            {
                return Color.Blue;
            }
            return clr;
        }

        private void PanelModifiers_ClickEvent(object sender, EventArgs e)
        {
            try
            {
                Panel pnlButton = sender as Panel;
                if (rdoNo.Checked == false && rdoOnly.Checked == false && rdoSide.Checked == false && rdoRemove.Checked == false)
                {
                    MessageBox.Show("Please select valid option for modifier. Like No, Only, Side.", "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
               
                if (rdoRemove.Checked == false)
                {
                    if (CheckDuplicateIngredients(pnlButton.Tag.ToString()))
                    {
                        MessageBox.Show("Modifier already added for this product.", "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (InsertModifiers(pnlButton.Tag.ToString(), Convert.ToInt32(pnlButton.Name)))
                    {
                        if (rdoNo.Checked)
                            pnlButton.BackColor = ChnagePanelModifiersColor(pnlButton.Tag.ToString());
                        else if (rdoOnly.Checked)
                            pnlButton.BackColor = ChnagePanelModifiersColor(pnlButton.Tag.ToString());
                        else if (rdoSide.Checked)
                            pnlButton.BackColor = ChnagePanelModifiersColor(pnlButton.Tag.ToString());
                        else
                            pnlButton.BackColor = ChnagePanelModifiersColor(pnlButton.Tag.ToString());
                    }
                }
                else 
                {
                    if (DeleteModifiers(pnlButton.Tag.ToString()))
                    {
                        if (rdoNo.Checked)
                            pnlButton.BackColor = ChnagePanelModifiersColor(pnlButton.Tag.ToString());
                        else if (rdoOnly.Checked)
                            pnlButton.BackColor = ChnagePanelModifiersColor(pnlButton.Tag.ToString());
                        else if (rdoSide.Checked)
                            pnlButton.BackColor = ChnagePanelModifiersColor(pnlButton.Tag.ToString());
                        else
                            pnlButton.BackColor = ChnagePanelModifiersColor(pnlButton.Tag.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PanelModifiersLabel_ClickEvent(object sender, EventArgs e)
        {
            try
            {
                Label pnlLabel = sender as Label;
                if (rdoNo.Checked == false && rdoOnly.Checked == false && rdoSide.Checked == false && rdoRemove.Checked == false)
                {
                    MessageBox.Show("Please select valid option for modifier. Like No, Only, Side.", "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
              
                if (rdoRemove.Checked == false)
                {
                    if (CheckDuplicateIngredients(pnlLabel.Tag.ToString()))
                    {
                        MessageBox.Show("Modifier already added for this product.");
                        return;
                    }
                    if (InsertModifiers(pnlLabel.Tag.ToString(), Convert.ToInt32(pnlLabel.Name)))
                    {
                        if (rdoNo.Checked)
                            pnlLabel.Parent.BackColor = ChnagePanelModifiersColor(pnlLabel.Tag.ToString());
                        else if (rdoOnly.Checked)
                            pnlLabel.Parent.BackColor = ChnagePanelModifiersColor(pnlLabel.Tag.ToString());
                        else if (rdoSide.Checked)
                            pnlLabel.Parent.BackColor = ChnagePanelModifiersColor(pnlLabel.Tag.ToString());
                        else
                            pnlLabel.Parent.BackColor = ChnagePanelModifiersColor(pnlLabel.Tag.ToString());
                    }
                }
                else
                {
                    if (DeleteModifiers(pnlLabel.Tag.ToString()))
                    {
                        if (rdoNo.Checked)
                            pnlLabel.Parent.BackColor = ChnagePanelModifiersColor(pnlLabel.Tag.ToString());
                        else if (rdoOnly.Checked)
                            pnlLabel.Parent.BackColor = ChnagePanelModifiersColor(pnlLabel.Tag.ToString());
                        else if (rdoSide.Checked)
                            pnlLabel.Parent.BackColor = ChnagePanelModifiersColor(pnlLabel.Tag.ToString());
                        else
                            pnlLabel.Parent.BackColor = ChnagePanelModifiersColor(pnlLabel.Tag.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonPlus_ClickEvent(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                objENTOWM.OrderID = new Guid(txtOrderID.Text);
                objENTOWM.ProductID = new Guid(txtProductID.Text);
                objENTOWM.TransactionID = new Guid(txtTransID.Text);
                objENTOWM.IngredientsID = new Guid(button.Name.ToString());
                objENTOWM.Mode = "GetByOrderProductTransactionIngredientsID";
                lstENTOWM = objDALOWM.GetOrderWiseModifier(objENTOWM);
                if (lstENTOWM.Count>0)
                {
                    objENTOWM.OrderID = new Guid(txtOrderID.Text);
                    objENTOWM.ProductID = new Guid(txtProductID.Text);
                    objENTOWM.TransactionID = new Guid(txtTransID.Text);
                    objENTOWM.IngredientsID = new Guid(button.Name.ToString());
                    objENTOWM.Quantity = lstENTOWM[0].Quantity + 1;
                    objENTOWM.Total = Convert.ToDecimal(objENTOWM.Quantity) * Convert.ToDecimal(lstENTOWM[0].Price);
                    objENTOWM.Mode = "UPDATE_QTY";
                    objDALOWM.InsertUpdateDeleteOrderWiseModifier(objENTOWM);
                    getOrderWiseModifiers();
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonMinus_ClickEvent(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                objENTOWM.OrderID = new Guid(txtOrderID.Text);
                objENTOWM.ProductID = new Guid(txtProductID.Text);
                objENTOWM.TransactionID = new Guid(txtTransID.Text);
                objENTOWM.IngredientsID = new Guid(button.Name.ToString());
                objENTOWM.Mode = "GetByOrderProductTransactionIngredientsID";
                lstENTOWM = objDALOWM.GetOrderWiseModifier(objENTOWM);
                if (lstENTOWM.Count > 0)
                {
                    objENTOWM.OrderID = new Guid(txtOrderID.Text);
                    objENTOWM.ProductID = new Guid(txtProductID.Text);
                    objENTOWM.TransactionID = new Guid(txtTransID.Text);
                    objENTOWM.IngredientsID = new Guid(button.Name.ToString());
                    if (lstENTOWM[0].Quantity > 0)
                    {
                        objENTOWM.Quantity = lstENTOWM[0].Quantity - 1;
                        objENTOWM.Total = Convert.ToDecimal(objENTOWM.Quantity) * Convert.ToDecimal(lstENTOWM[0].Price);
                    }
                    else
                    {
                        objENTOWM.Quantity = 0;
                        objENTOWM.Total = 0;
                    }
                    objENTOWM.Mode = "UPDATE_QTY";
                    objDALOWM.InsertUpdateDeleteOrderWiseModifier(objENTOWM);
                    getOrderWiseModifiers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool InsertModifiers(string IngredientsID, int ModifierCategoryDetail_Id)
        {
            bool Result = false;
            try
            {
                DAL.ModifierDetail objDALMD = new DAL.ModifierDetail();
                ENT.ModifierDetail objENTMD = new ENT.ModifierDetail();
                List<ENT.ModifierDetail> lstENTMD = new List<ENT.ModifierDetail>();

                objENTMD.Mode = "GetModifierForProduct";
                objENTMD.IngredientsID = new Guid(IngredientsID);
                objENTMD.ModifierCategoryDetail_Id = ModifierCategoryDetail_Id;
                lstENTMD = objDALMD.getModifierDetail(objENTMD);
                if (lstENTMD.Count > 0)
                {
                    int qty = 1;
                    string strOption = "";
                    if (rdoNo.Checked)
                        strOption = rdoNo.Text.Trim();
                    else if (rdoOnly.Checked)
                        strOption = rdoOnly.Text.Trim();
                    else if (rdoSide.Checked)
                        strOption = rdoSide.Text.Trim();

                    objENTOWM.ModifierID = Guid.NewGuid();
                    objENTOWM.OrderID = new Guid(txtOrderID.Text);
                    objENTOWM.ProductID = new Guid(txtProductID.Text);
                    objENTOWM.TransactionID = new Guid(txtTransID.Text);
                    objENTOWM.IngredientsID = new Guid(lstENTMD[0].IngredientsID.ToString());
                    objENTOWM.Quantity = qty;
                    //objENTOWM.Quantity = lstENTMD[0].Qty;
                    objENTOWM.Price = lstENTMD[0].Price;
                    objENTOWM.Total = Convert.ToDecimal(qty) * Convert.ToDecimal(lstENTMD[0].Price);
                    objENTOWM.ModifierOption = strOption;
                    objENTOWM.RUserID = new Guid(GlobalVariable.BranchID);
                    objENTOWM.RUserType = GlobalVariable.RUserType;
                 
                    objENTOWM.Mode = "ADD";
                    Result = objDALOWM.InsertUpdateDeleteOrderWiseModifier(objENTOWM);
                    if (Result)
                    {
                        getOrderWiseModifiers();
                    }
                }
            }
            catch (Exception ex)
            {
                Result = false;
                MessageBox.Show(ex.Message.ToString(), "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Result;
        }

        private bool DeleteModifiers(string IngredientsID)
        {
            bool Result = false;
            try
            {
                objENTOWM.OrderID = new Guid(txtOrderID.Text);
                objENTOWM.ProductID = new Guid(txtProductID.Text);
                objENTOWM.TransactionID = new Guid(txtTransID.Text);
                objENTOWM.IngredientsID = new Guid(IngredientsID);
                objENTOWM.Mode = "GetByOrderProductTransactionIngredientsID";
                lstENTOWM = objDALOWM.GetOrderWiseModifier(objENTOWM);

                if (lstENTOWM.Count > 0)
                {
                    objENTOWM.ModifierID = lstENTOWM[0].ModifierID;
                    objENTOWM.Mode = "DELETE";
                    Result = objDALOWM.InsertUpdateDeleteOrderWiseModifier(objENTOWM);
                    if (Result)
                    {
                        getOrderWiseModifiers();
                    }
                }
            }
            catch (Exception ex)
            {
                Result = false;
                MessageBox.Show(ex.Message.ToString(), "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Result;
        }

        private void getOrderWiseModifiers()
        {
            try
            {
                listView.Items.Clear();
                txtTotalAmount.Text = "0.00";
                objENTOWM.OrderID = new Guid(txtOrderID.Text);
                objENTOWM.ProductID = new Guid(txtProductID.Text);
                objENTOWM.TransactionID = new Guid(txtTransID.Text);
                objENTOWM.Mode = "GetRecordByOrderAndProductID";
                lstENTOWM = objDALOWM.GetOrderWiseModifier(objENTOWM);
                for (int i = 0; i < lstENTOWM.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = lstENTOWM[i].Name;
                    item.SubItems.Add(lstENTOWM[i].Quantity.ToString());
                    item.SubItems.Add(lstENTOWM[i].Price.ToString());
                    item.SubItems.Add(lstENTOWM[i].ModifierOption.ToString());
                    item.SubItems.Add(lstENTOWM[i].IngredientsID.ToString());
                    item.SubItems.Add(lstENTOWM[i].ModifierID.ToString());
                    item.SubItems.Add(lstENTOWM[i].Total.ToString());
                    listView.Items.Add(item);
                    txtTotalAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotalAmount.Text) + Convert.ToDecimal(lstENTOWM[i].Total));
                }
                GlobalVariable.decModifierAmount = txtTotalAmount.Text.Trim() == string.Empty ? 0 : Convert.ToDecimal(txtTotalAmount.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void frmModifiers_Load(object sender, EventArgs e)
        {
            try
            {
                this.getModifiersCategory();
                this.getOrderWiseModifiers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count > 0)
                {
                    //string s1 = listView.SelectedItems[0].SubItems[0].Text;
                    //string s2 = listView.SelectedItems[0].SubItems[1].Text;
                    //string s3 = listView.SelectedItems[0].SubItems[2].Text;
                    //string s4 = listView.SelectedItems[0].SubItems[3].Text;
                    //string s5 = listView.SelectedItems[0].SubItems[4].Text;
                    //MessageBox.Show(s1 + "-" + s2 + "-" + s3 + "" + s4 + "-" + s5);
                }
                else
                {
                    MessageBox.Show("Please select an item before assigning a value.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmModifiers_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
