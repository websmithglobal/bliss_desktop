using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.Bliss
{
    public partial class frmAddMenu : Form
    {
        ENT.CategoryMaster objENTCategoryMaster = new ENT.CategoryMaster();
        List<ENT.CategoryMaster> lstCategoryMaster = new List<ENT.CategoryMaster>();
        DAL.CategoryMaster objDALCategoryMaster = new DAL.CategoryMaster();
        List<ENT.CategoryAddress> lstCatAddr = new List<ENT.CategoryAddress>();
        int itemCount;
        int increaseLength;
        bool IsRefreshMenuPanel = true;
        Panel pnlMainMenu;
        Panel pnlAddressBar;

        public frmAddMenu()
        {
            InitializeComponent();
        }

        #region Breadcrumb Panel

        private void drawAddressBar(string pnlName)
        {
            try
            {
                itemCount = 0;

                lstCatAddr = new List<ENT.CategoryAddress>();
                pnlAddressBar = new Panel();
                pnlAddressBar.AutoScroll = true;
                pnlAddressBar.Location = new System.Drawing.Point(6, 6);  // pnlMainMenu.Height + 12 // 510
                pnlAddressBar.Size = new System.Drawing.Size(tpMenu.Width - 15, 62);  // w-490 // h-470
                pnlAddressBar.BorderStyle = BorderStyle.FixedSingle;
                pnlAddressBar.Name = pnlName;
                pnlAddressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)))));
                this.tpMenu.Controls.Add(pnlAddressBar);
                lstCatAddr.Add(new ENT.CategoryAddress() { CategoryID = "00000000-0000-0000-0000-000000000000", CategoryName = "Home", Index = itemCount });
                addAddressbutton();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Add Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addAddressbutton()
        {
            try
            {
                increaseLength = 6;
                Button button = new Button();
                pnlAddressBar.Controls.Remove(button);
                for (int i = 0; i < lstCatAddr.Count; i++)
                {
                    button.Location = new Point(increaseLength, 6);
                    if (i == 0)
                    {
                        button.Click += new EventHandler(btnHome_Click);
                        button.ImageAlign = ContentAlignment.MiddleLeft;
                        button.ImageKey = "home.png";
                        button.ImageList = this.imageList1;
                        button.BackColor = Color.Green;
                    }
                    else
                    {
                        button.Click += new EventHandler(btnAddressBar_Click);
                        button.ImageAlign = ContentAlignment.MiddleRight;
                        button.ImageKey = "right.png";
                        button.ImageList = this.imageList1;
                        button.BackColor = Color.FromArgb(9, 89, 79); //Color.Teal; //DarkOrange;
                    }
                    button.Name = "btn" + i;
                    button.TabIndex = i;
                    button.Font = new Font("Microsoft Sans Serif", 11);
                    button.Tag = lstCatAddr[i].CategoryID;
                    button.Text = lstCatAddr[i].CategoryName;
                    button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)))));
                    button.Size = new System.Drawing.Size(120, 50);

                    button.ForeColor = Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                    button.Font = new Font(button.Font, FontStyle.Bold);

                    pnlAddressBar.Controls.Add(button);
                    increaseLength += 126;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Add Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveAddressbutton(int Index)
        {
            try
            {
                for (int i = lstCatAddr.Count - 1; i > 0; i--)
                {
                    if (Index <= i)
                    {
                        increaseLength -= 126;
                        lstCatAddr.RemoveAt(i);
                        pnlAddressBar.Controls.RemoveAt(i);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Add Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddressBar_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                ENT.CategoryMaster objENTCat = new ENT.CategoryMaster();
                List<ENT.CategoryMaster> lstCat = new List<ENT.CategoryMaster>();
                DAL.CategoryMaster objDALCat = new DAL.CategoryMaster();

                objENTCat.Mode = "GetRecordByCategoryID";
                objENTCat.CategoryID = new Guid(button.Tag.ToString());
                lstCat = objDALCat.getCategoryMaster(objENTCat);
                if (lstCat.Count > 0)
                {
                    RemoveAddressbutton(button.TabIndex);
                    this.getCategory(lstCat[0].ParentID.ToString());
                }
                else
                {
                    MessageBox.Show("No More Parent Category Found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Add Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnHome = sender as Button;
                this.RemoveAddressbutton(btnHome.TabIndex);
                this.getCategory(btnHome.Tag.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Add Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Main Menu

        private void drawPanelMenu(string pnlName)
        {
            try
            {
                pnlMainMenu = new Panel();
                pnlMainMenu.AutoScroll = true;
                pnlMainMenu.Location = new System.Drawing.Point(6, pnlAddressBar.Height + 12);
                pnlMainMenu.Size = new System.Drawing.Size(tpMenu.Width - 15, tpMenu.Height - 82);  // w-490 // h-470
                pnlMainMenu.BorderStyle = BorderStyle.FixedSingle;
                pnlMainMenu.Name = pnlName;
                pnlMainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)))));
                this.tpMenu.Controls.Add(pnlMainMenu);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Add Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getCategory(string ParentID)
        {
            try
            {
                tpMenu.Controls.Remove(pnlMainMenu);
                this.drawPanelMenu("pnlMainMenu");

                objENTCategoryMaster.Mode = "GetAddCategoryProduct";
                objENTCategoryMaster.ParentID = new Guid(ParentID);
                lstCategoryMaster = objDALCategoryMaster.getDisplayCategoryButton(objENTCategoryMaster);
                //lstCategoryMaster = objDALCategoryMaster.getDisplayCategoryButton(objENTCategoryMaster).OrderBy(ord => ord.CategoryName).ToList();
                int pnl_width = 0;
                int x = 0;
                int y = 12;
                int i = 0;
                int col = 0;
                double row = 0;

                if (pnlMainMenu.Width > 525)
                {
                    col = 4;
                    pnl_width = (pnlMainMenu.Width - 498) / 2;
                    if (lstCategoryMaster.Count <= col)
                        row = 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstCategoryMaster.Count) / col, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    col = 3;
                    pnl_width = (pnlMainMenu.Width - 372) / 2;
                    if (lstCategoryMaster.Count <= col)
                        row = 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstCategoryMaster.Count) / col, 2, MidpointRounding.AwayFromZero);
                }

                for (int h = 0; h < row; h++)
                {
                    x = pnl_width;
                    for (int v = 0; v < col; v++)
                    {
                        if (lstCategoryMaster[i].IsCategory == 4 && ParentID == "00000000-0000-0000-0000-000000000000")
                        { continue; }

                        Button button = new Button();
                        button.Location = new Point(x, y);
                        button.Size = new System.Drawing.Size(120, 100);
                        button.Text = lstCategoryMaster[i].CategoryName;
                        button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)))));
                        
                        if (lstCategoryMaster[i].IsCategory == 0)
                        {
                            button.BackColor = Color.Green; // Color for Product
                            button.Click += new EventHandler(ButtonClickOneEvent);
                            button.Tag = lstCategoryMaster[i].CategoryID;
                        }
                        else if (lstCategoryMaster[i].IsCategory == 3)
                        {
                            button.BackColor = Color.Black; // Color for Add Category
                            button.Click += new EventHandler(AddCatClickOneEvent);
                            button.Tag = ParentID;
                        }
                        else if (lstCategoryMaster[i].IsCategory == 4)
                        {
                            button.BackColor = Color.Black; // Color for Add Product
                            button.Click += new EventHandler(AddProductClickOneEvent);
                            button.Tag = ParentID;
                        }
                        else 
                        {
                            button.BackColor = Color.FromArgb(58, 173, 158);  // Color for Category
                            button.Click += new EventHandler(ButtonClickOneEvent);
                            button.Tag = lstCategoryMaster[i].CategoryID;
                        }

                        button.Font = new Font("Microsoft Sans Serif", 11);
                        button.ForeColor = Color.White;
                        button.FlatStyle = FlatStyle.Flat;
                        button.Font = new Font(button.Font, FontStyle.Bold);
                        pnlMainMenu.Controls.Add(button);
                        if (i == lstCategoryMaster.Count - 1)
                        {
                            break;
                        }
                        i++;
                        x = x + 126;
                    }
                    y = y + 106;
                }
                itemCount += 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Add Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonClickOneEvent(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button != null)
                {
                    for (int n = 0; n <= lstCategoryMaster.Count - 1; n++)
                    {
                        if (button.Tag.ToString() == lstCategoryMaster[n].CategoryID.ToString())
                        {
                            if (lstCategoryMaster[n].IsCategory != 0)
                            {
                                lstCatAddr.Add(new ENT.CategoryAddress() { CategoryID = lstCategoryMaster[n].CategoryID.ToString(), CategoryName = lstCategoryMaster[n].CategoryName.ToString(), Index = itemCount });
                                this.getCategory(lstCategoryMaster[n].CategoryID.ToString());
                                addAddressbutton();
                            }
                            else
                            {
                                //Guid ProdId = new Guid(button.Tag.ToString());
                                //this.AddCartItem(ProdId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Add Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddCatClickOneEvent(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button != null)
                {
                    frmAddCategory frmAC = new frmAddCategory(button.Tag.ToString());
                    frmAC.ShowDialog();
                    getCategory(button.Tag.ToString());
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Add Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddProductClickOneEvent(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button != null)
                {
                    frmAddProduct frmAP = new frmAddProduct(button.Tag.ToString());
                    frmAP.ShowDialog();
                    getCategory(button.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Add Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void frmAddMenu_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsRefreshMenuPanel)
                {
                    tpMenu.Controls.Remove(pnlAddressBar);
                    this.drawAddressBar("pnlAddressBar");
                    this.getCategory("00000000-0000-0000-0000-000000000000");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Add Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmAddMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
