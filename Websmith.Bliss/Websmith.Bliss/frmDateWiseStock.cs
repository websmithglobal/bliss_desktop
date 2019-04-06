using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;
using System.Linq;
using System.IO;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System.Data;

namespace Websmith.Bliss
{
    public partial class frmDateWiseStock : Form
    {
        public frmDateWiseStock()
        {
            InitializeComponent();
        }

        private List<ENT.OrderBook> SearchOrder()
        {
            List<ENT.OrderBook> lstENTOrder = new List<ENT.OrderBook>();
            try
            {
                ENT.OrderBook objENTOrder = new ENT.OrderBook();
                DAL.OrderBook objDALOrder = new DAL.OrderBook();
                objENTOrder.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                objENTOrder.OrderDateFrom = GlobalVariable.ChangeDate(dtpFromDate.Text);
                objENTOrder.OrderDateTo = GlobalVariable.ChangeDate(dtpToDate.Text);
                objENTOrder.Mode = "GetByEmployeeID";
                lstENTOrder = objDALOrder.getSalesReport(objENTOrder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lstENTOrder;
        }

        private void FillComboProduct()
        {
            try
            {
                ENT.CategoryWiseProduct objENTIMD = new ENT.CategoryWiseProduct();
                List<ENT.CategoryWiseProduct> lstENTIMD = new List<ENT.CategoryWiseProduct>();
                BindingSource bs = new BindingSource();
                objENTIMD.Mode = "GetProductForLookupCombo"; //GetProductForComboIsSelling
                lstENTIMD = new DAL.CategoryWiseProduct().getCategoryWiseProduct(objENTIMD);
                bs.DataSource = lstENTIMD;
                cmbProduct.DataSource = bs;
                cmbProduct.DisplayMember = "ProductName";
                cmbProduct.ValueMember = "ProductID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable getStock()
        {
            DataTable dt =new DataTable();
            try
            {
                string sqlQuery = "";
                string sqlQueryProduct = "";
                string where = "";
                string sqlQueryMain = "SELECT * From ( ";

                if (checkBox1.Checked)
                {
                    sqlQueryProduct = " UNION ALL SELECT 0 As Sort, ProductID, 'Opening As On " + dtpFromDate.Value.ToString("dd/MM/yyyy") + "' AS ProductName, SUM(Qty) AS StkQty FROM ( ";
                    sqlQueryProduct += " SELECT  ProductID, ProductName + '-IN' AS ProductName, TotQty AS Qty FROM ViewInward ";
                    sqlQueryProduct += " Where ProductID = '" + Convert.ToString(cmbProduct.SelectedValue) + "' And InvoiceDate < '" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "' ";
                    sqlQueryProduct += " UNION ALL ";
                    sqlQueryProduct += " SELECT ProductID, ProductName + '-OUT' AS ProductName, - Qty AS Qty FROM ViewOutward ";
                    sqlQueryProduct += " Where ProductID = '" + Convert.ToString(cmbProduct.SelectedValue) + "' And InvoiceDate < '" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "' ";
                    sqlQueryProduct += " UNION ALL ";
                    sqlQueryProduct += " SELECT ProductID, ProductName + '-SALE' AS ProductName, - Quantity AS Qty FROM ViewOrder ";
                    sqlQueryProduct += " Where  ProductID = '" + Convert.ToString(cmbProduct.SelectedValue) + "' And DATEADD(DAY, DATEDIFF(DAY, 0, OrderDate), 0) < '" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "' And IsDrink=1 ";
                    sqlQueryProduct += " ) AS Opening Group By ProductID ";
                    txtTotal.Visible = true;
                    lblStock.Visible = true;
                }
                else
                {
                    txtTotal.Visible = false;
                    lblStock.Visible = false;
                }

                if (checkBox1.Checked)
                {
                    where = " And ProductID = '" + Convert.ToString(cmbProduct.SelectedValue) + "' ";
                    sqlQuery = "SELECT 1 As Sort, Stock.ProductID, ProductName, Qty AS StkQty From ( ";
                    sqlQuery += " SELECT ProductID, ProductName + '-IN' AS ProductName, TotQty AS Qty FROM ViewInward ";
                    sqlQuery += " Where InvoiceDate Between '" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "' And '" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "' " + where + " ";
                    sqlQuery += " UNION ALL ";
                    sqlQuery += " SELECT ProductID, ProductName + '-OUT' AS ProductName, - Qty AS Qty FROM ViewOutward ";
                    sqlQuery += " Where InvoiceDate Between '" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "' And '" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "' " + where + " ";
                    sqlQuery += " UNION ALL ";
                    sqlQuery += " SELECT ProductID, ProductName + '-SALE' AS ProductName, - Quantity AS Qty FROM ViewOrder ";
                    sqlQuery += " Where DATEADD(DAY, DATEDIFF(DAY, 0, OrderDate), 0) Between '" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "' And '" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "' " + where + " And IsDrink=1 ";
                    sqlQuery += " ) AS Stock ";
                    //sqlQuery += " Group By Stock.ProductID,CategoryWiseProduct.ProductName ";
                }
                else
                {
                    where = "";
                    sqlQuery = "SELECT 1 As Sort, Stock.ProductID, ProductName, SUM(Qty) AS StkQty From ( ";
                    sqlQuery += " SELECT ProductID, ProductName, TotQty AS Qty FROM ViewInward ";
                    sqlQuery += " Where InvoiceDate Between '" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "' And '" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "' " + where + " ";
                    sqlQuery += " UNION ALL ";
                    sqlQuery += " SELECT ProductID, ProductName, - Qty AS Qty FROM ViewOutward ";
                    sqlQuery += " Where InvoiceDate Between '" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "' And '" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "' " + where + " ";
                    sqlQuery += " UNION ALL ";
                    sqlQuery += " SELECT ProductID, ProductName, - Quantity AS Qty FROM ViewOrder ";
                    sqlQuery += " Where DATEADD(DAY, DATEDIFF(DAY, 0, OrderDate), 0) Between '" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "' And '" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "' And IsDrink=1 " + where + " ";
                    sqlQuery += " ) AS Stock ";
                    sqlQuery += " Group By Stock.ProductID,ProductName ";
                }

                sqlQueryMain = sqlQueryMain + sqlQuery + sqlQueryProduct;
                sqlQueryMain += " ) As StockMaster order by Sort";

                dt = new DAL.OrderBook().getDateWiseStock(sqlQueryMain);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        private void ExportExcel()
        {
            try
            {
                DataTable dtMaster = getStock();

                if (dtMaster.Columns.Count > 0)
                {
                    dtMaster.Columns.Remove("Sort");
                    dtMaster.Columns.Remove("ProductID");
                }

                if (dtMaster.Rows.Count <= 0)
                {
                    MessageBox.Show("Data Not Found.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                pb1.Minimum = 0;
                pb1.Maximum = dtMaster.Rows.Count - 1;
                pb1.Value = 0;

                string fileName = "STOCK_" + dtpFromDate.Text.Replace("/", "-") + "_TO_" + dtpToDate.Text.Replace("/", "-") + ".xlsx";
                string folderPath = Path.Combine(Application.StartupPath, "Export Excel");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                #region Custome Excel
                var document = new SLDocument();
                document.AddWorksheet("Stock");

                #region Formating Excel

                SLStyle headerStyle = document.CreateStyle();
                headerStyle.SetFont("Calibri", 12);
                headerStyle.SetFontBold(true);
                headerStyle.SetWrapText(true);
                headerStyle.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
                headerStyle.SetVerticalAlignment(VerticalAlignmentValues.Center);
                headerStyle.Fill.SetPatternType(PatternValues.Solid);
                headerStyle.Fill.SetPatternForegroundColor(SLThemeColorIndexValues.Accent1Color, 0.35);
                headerStyle.Font.FontColor = System.Drawing.Color.White;

                SLStyle subHeaderStyle = document.CreateStyle();
                subHeaderStyle.SetFont("Calibri", 12);
                subHeaderStyle.SetFontBold(true);
                subHeaderStyle.SetWrapText(true);
                subHeaderStyle.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
                subHeaderStyle.SetVerticalAlignment(VerticalAlignmentValues.Center);
                subHeaderStyle.Fill.SetPatternType(PatternValues.Solid);
                subHeaderStyle.Fill.SetPatternForegroundColor(SLThemeColorIndexValues.Light1Color, 0.35);
                subHeaderStyle.Font.FontColor = System.Drawing.Color.White;

                SLStyle detailStyle = document.CreateStyle();
                detailStyle.SetFont("Calibri", 11);
                detailStyle.SetWrapText(true);          // This appears to reset the Horizontal alignment to the default.
                detailStyle.SetVerticalAlignment(VerticalAlignmentValues.Center);
                //detailStyle.SetTopBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);

                SLStyle detailStyleMain = document.CreateStyle();
                detailStyleMain.SetFont("Calibri", 11);
                detailStyleMain.SetWrapText(true);          // This appears to reset the Horizontal alignment to the default.
                detailStyleMain.SetVerticalAlignment(VerticalAlignmentValues.Center);
                detailStyleMain.Font.Bold = false;

                SLStyle detailStyleTotal = document.CreateStyle();
                detailStyleTotal.SetFont("Calibri", 11);
                detailStyleTotal.SetWrapText(true);          // This appears to reset the Horizontal alignment to the default.
                detailStyleTotal.SetVerticalAlignment(VerticalAlignmentValues.Center);
                detailStyleTotal.Font.Bold = true;
                //detailStyleMain.Fill.SetPatternForegroundColor(SLThemeColorIndexValues.Accent6Color, 0.70);
                //detailStyleMain.Font.FontColor = System.Drawing.Color.White;
                #endregion

                var row = 1;
                document.SelectWorksheet("Stock");
                if(checkBox1.Checked)
                    document.SetCellValue(row, 1, "Date & Product Wise Stock");
                else
                    document.SetCellValue(row, 1, "Date Wise Stock");

                row += 1;
                document.SetCellValue(row, 1, "Date From: " + dtpFromDate.Text + " TO: " + dtpToDate.Text);
                row += 1;
                for (int i = 0; i < dtMaster.Columns.Count; i++)
                {
                    if(!dtMaster.Columns[i].ToString().Contains("ProductID") || !dtMaster.Columns[i].ToString().Contains("Sort"))
                        document.SetCellValue(row, i+1, dtMaster.Columns[i].ToString().ToUpper());
                }

                row += 1;
                for (int i = 0; i < (dtMaster.Rows.Count); i++)
                {
                    document.SetRowStyle(row, detailStyleMain);
                    for (int j = 0; j < dtMaster.Columns.Count; j++)
                    {
                        if (dtMaster.Rows[i][j] != null)
                        {
                            document.SetCellValue(row, j+1, Convert.ToString(dtMaster.Rows[i][j]));
                        }
                        else
                        {
                            document.SetCellValue(row, j+1, "");
                        }
                    }
                    row++;

                    pb1.Value = i;
                }

                if (checkBox1.Checked)
                {
                    row += 1;
                    document.SetRowStyle(row, detailStyleTotal);
                    document.SetCellValue(row, dtMaster.Columns.Count - 1, "Total Stock");
                    document.SetCellValue(row, dtMaster.Columns.Count, Convert.ToString(txtTotal.Text));
                }
               
                document.SetCellStyle(1, 1, 3, dtMaster.Columns.Count, headerStyle);
                document.AutoFitColumn(1, dtMaster.Columns.Count);
                document.FreezePanes(3, 1);
                document.SaveAs(Path.Combine(folderPath, fileName));
                #endregion

                if (MessageBox.Show("Data Exported Successfully. You Want To Open Exported File ?", "Export", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(Path.Combine(folderPath, fileName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.IsDate(dtpFromDate.Text))
            {
                MessageBox.Show("From date is not valid.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFromDate.Focus();
                return;
            }
            if (!GlobalVariable.IsDate(dtpToDate.Text))
            {
                MessageBox.Show("To date is not valid.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpToDate.Focus();
                return;
            }
            if (dtpFromDate.Value > dtpToDate.Value)
            {
                MessageBox.Show("FromDate must be less than ToDate.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpToDate.Focus();
                return;
            }

            if (checkBox1.Checked)
            {
                if (cmbProduct.SelectedIndex < 0)
                {
                    MessageBox.Show("Select valid product from list.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbProduct.Focus();
                    return;
                }
            }
            
            DataTable dtStock = getStock();
            txtTotal.Text = "0";
            dgvSales.Rows.Clear();
            for (int i = 0; i < dtStock.Rows.Count; i++)
            {
                dgvSales.Rows.Add();
                dgvSales.Rows[i].Cells["colProductID"].Value = dtStock.Rows[i]["ProductID"].ToString();
                dgvSales.Rows[i].Cells["colProductName"].Value = dtStock.Rows[i]["ProductName"].ToString();
                dgvSales.Rows[i].Cells["colStock"].Value = dtStock.Rows[i]["StkQty"].ToString();
                txtTotal.Text = Convert.ToString(Convert.ToDecimal(txtTotal.Text) + Convert.ToDecimal(dtStock.Rows[i]["StkQty"].ToString()));
            }            
        }

        private void frmSalesReport_Load(object sender, EventArgs e)
        {
            txtTotal.Visible = false;
            lblStock.Visible = false;
            FillComboProduct();
        }

        private void frmSalesReport_KeyDown(object sender, KeyEventArgs e)
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

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalVariable.IsDate(dtpFromDate.Text))
                {
                    MessageBox.Show("From date is not valid.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpFromDate.Focus();
                    return;
                }
                if (!GlobalVariable.IsDate(dtpToDate.Text))
                {
                    MessageBox.Show("To date is not valid.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpToDate.Focus();
                    return;
                }
                ExportExcel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
