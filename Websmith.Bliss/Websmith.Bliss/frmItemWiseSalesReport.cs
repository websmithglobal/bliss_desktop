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
using System.Reflection;

namespace Websmith.Bliss
{
    public partial class frmItemWiseSalesReport : Form
    {
        public frmItemWiseSalesReport()
        {
            InitializeComponent();
        }

        private void FillComboProduct()
        {
            try
            {
                ENT.CategoryWiseProduct objENTIMD = new ENT.CategoryWiseProduct();
                List<ENT.CategoryWiseProduct> lstENTIMD = new List<ENT.CategoryWiseProduct>();
                BindingSource bs = new BindingSource();
                objENTIMD.Mode = "GetProductForLookupCombo";
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

        private void FillComboCategory()
        {
            try
            {
                ENT.CategoryMaster objENTCat = new ENT.CategoryMaster();
                List<ENT.CategoryMaster> lstENTCate = new List<ENT.CategoryMaster>();
                BindingSource bs1 = new BindingSource();
                objENTCat.Mode = "GetAllRecord";
                lstENTCate = new DAL.CategoryMaster().getCategoryMaster(objENTCat);
                bs1.DataSource = lstENTCate;
                cmbCategory.DataSource = bs1;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable listToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private void ExportExcel(List<ENT.Transaction> lstCategory)
        {
            try
            {
                DataTable dtMaster = listToDataTable(lstCategory);

                if (lstCategory.Count <= 0)
                {
                    MessageBox.Show("Data Not Found.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                pb1.Minimum = 0;
                pb1.Maximum = lstCategory.Count - 1;
                pb1.Value = 0;

                string fileName = "ItemWiseSale_" + dtpFromDate.Text.Replace("/", "-") + "_TO_" + dtpToDate.Text.Replace("/", "-") + ".xlsx";
                string folderPath = Path.Combine(Application.StartupPath, "Export Excel");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                #region Custome Excel
                var document = new SLDocument();
                document.AddWorksheet("ItemWiseSale");

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
                document.SelectWorksheet("ItemWiseSale");
                if (chkProduct.Checked)
                    document.SetCellValue(row, 1, "Item Wise Sale Report");
                else if (chkCategory.Checked)
                    document.SetCellValue(row, 1, "Category Wise Item Sales");
                else if (chkCategory.Checked && chkCategory.Checked)
                    document.SetCellValue(row, 1, "Category And Item Wise Sales");
                else
                    document.SetCellValue(row, 1, "Date Item Wise Sales");

                row += 1;
                document.SetCellValue(row, 1, "Date From: " + dtpFromDate.Text + " To: " + dtpToDate.Text);
                row += 1;

                int col = 1;
                PropertyInfo[] Props = typeof(ENT.Transaction).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    if (prop.Name == "ProductName" || prop.Name == "Quantity" || prop.Name == "TotalAmount")
                    {
                        document.SetCellValue(row, col, prop.Name.ToString().ToUpper());
                        col++;
                    }
                }

                decimal totqty = 0;
                decimal totamt = 0;
                row += 1;
                for (int i = 0; i < lstCategory.Count; i++)
                {
                    document.SetRowStyle(row, detailStyleMain);

                    if (!string.IsNullOrEmpty(lstCategory[i].ProductName))
                    { document.SetCellValue(row, 1, Convert.ToString(lstCategory[i].ProductName)); }
                    else { document.SetCellValue(row, 1, ""); }

                    if (!string.IsNullOrEmpty(Convert.ToString(lstCategory[i].Quantity)))
                    { document.SetCellValue(row, 2, Convert.ToString(lstCategory[i].Quantity)); }
                    else { document.SetCellValue(row, 2, ""); }

                    if (!string.IsNullOrEmpty(Convert.ToString(lstCategory[i].TotalAmount)))
                    { document.SetCellValue(row, 3, Convert.ToString(lstCategory[i].TotalAmount)); }
                    else { document.SetCellValue(row, 3, ""); }

                    totqty = totqty + lstCategory[i].Quantity;
                    totamt = totamt + lstCategory[i].TotalAmount;

                    row++;
                    pb1.Value = i;
                }

                row += 1;
                document.SetRowStyle(row, detailStyleTotal);
                document.SetCellValue(row, 1, "Grand Total");
                document.SetCellValue(row, 2, Convert.ToString(totqty));
                document.SetCellValue(row, 3, Convert.ToString(totamt));

                document.SetCellStyle(1, 1, 3, 3, headerStyle);
                document.AutoFitColumn(1, 3);
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
            try
            {

                ENT.Transaction objEntCat = new ENT.Transaction();
                objEntCat.Mode = "GetProductByCategoryForSalesReport";

                if (!GlobalVariable.IsDate(dtpFromDate.Text))
                {
                    MessageBox.Show("From date is not valid.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpFromDate.Focus();
                    return;
                }
                if (!GlobalVariable.IsDate(dtpToDate.Text))
                {
                    MessageBox.Show("To date is not valid.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpToDate.Focus();
                    return;
                }
                if (dtpFromDate.Value > dtpToDate.Value)
                {
                    MessageBox.Show("FromDate must be less than ToDate.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpToDate.Focus();
                    return;
                }
                objEntCat.OrderDateFrom = GlobalVariable.ChangeDate(dtpFromDate.Text);
                objEntCat.OrderDateTo = GlobalVariable.ChangeDate(dtpToDate.Text);

                if (chkProduct.Checked)
                {
                    if (cmbProduct.SelectedIndex < 0)
                    {
                        MessageBox.Show("Select valid product from list.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbProduct.Focus();
                        return;
                    }
                    objEntCat.ProductID = new Guid(cmbProduct.SelectedValue.ToString());
                }
                else
                { objEntCat.ProductID = new Guid("00000000-0000-0000-0000-000000000000"); }

                if (chkCategory.Checked)
                {
                    if (cmbCategory.SelectedIndex < 0)
                    {
                        MessageBox.Show("Select valid category from list.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbProduct.Focus();
                        return;
                    }
                    objEntCat.CategoryID = new Guid(cmbCategory.SelectedValue.ToString());
                }
                else
                { objEntCat.CategoryID = new Guid("00000000-0000-0000-0000-000000000000"); }

                List<ENT.Transaction> lstCategory = new List<ENT.Transaction>();
                lstCategory = new DAL.Transaction().getOrderTransaction(objEntCat);

                txtTotalAmount.Text = "0";
                txtTotalQty.Text = "0";
                dgvSales.Rows.Clear();
                for (int i = 0; i < lstCategory.Count; i++)
                {
                    dgvSales.Rows.Add();
                    dgvSales.Rows[i].Cells["colCategoryID"].Value = lstCategory[i].CategoryID;
                    dgvSales.Rows[i].Cells["colCategoryName"].Value = lstCategory[i].CategoryName;
                    dgvSales.Rows[i].Cells["colProductID"].Value = lstCategory[i].ProductID;
                    dgvSales.Rows[i].Cells["colProductName"].Value = lstCategory[i].ProductName;
                    dgvSales.Rows[i].Cells["colQuantity"].Value = lstCategory[i].Quantity;
                    dgvSales.Rows[i].Cells["colTotalAmount"].Value = lstCategory[i].TotalAmount;
                    txtTotalAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotalAmount.Text) + Convert.ToDecimal(lstCategory[i].TotalAmount));
                    txtTotalQty.Text = Convert.ToString(Convert.ToDecimal(txtTotalQty.Text) + Convert.ToDecimal(lstCategory[i].Quantity));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSalesReport_Load(object sender, EventArgs e)
        {
            FillComboProduct();
            FillComboCategory();
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
                ENT.Transaction objEntCat = new ENT.Transaction();
                objEntCat.Mode = "GetItemWiseSalesExport";

                if (!GlobalVariable.IsDate(dtpFromDate.Text))
                {
                    MessageBox.Show("From date is not valid.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpFromDate.Focus();
                    return;
                }
                if (!GlobalVariable.IsDate(dtpToDate.Text))
                {
                    MessageBox.Show("To date is not valid.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpToDate.Focus();
                    return;
                }
                if (dtpFromDate.Value > dtpToDate.Value)
                {
                    MessageBox.Show("FromDate must be less than ToDate.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpToDate.Focus();
                    return;
                }
                objEntCat.OrderDateFrom = GlobalVariable.ChangeDate(dtpFromDate.Text);
                objEntCat.OrderDateTo = GlobalVariable.ChangeDate(dtpToDate.Text);

                if (chkProduct.Checked)
                {
                    if (cmbProduct.SelectedIndex < 0)
                    {
                        MessageBox.Show("Select valid product from list.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbProduct.Focus();
                        return;
                    }
                    objEntCat.ProductID = new Guid(cmbProduct.SelectedValue.ToString());
                }
                else
                { objEntCat.ProductID = new Guid("00000000-0000-0000-0000-000000000000"); }

                if (chkCategory.Checked)
                {
                    if (cmbCategory.SelectedIndex < 0)
                    {
                        MessageBox.Show("Select valid category from list.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbProduct.Focus();
                        return;
                    }
                    objEntCat.CategoryID = new Guid(cmbCategory.SelectedValue.ToString());
                }
                else
                { objEntCat.CategoryID = new Guid("00000000-0000-0000-0000-000000000000"); }

                List<ENT.Transaction> lstCategory = new List<ENT.Transaction>();
                lstCategory = new DAL.Transaction().getOrderTransaction(objEntCat);

                ExportExcel(lstCategory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalVariable.IsDate(dtpFromDate.Text))
                {
                    MessageBox.Show("From date is not valid.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpFromDate.Focus();
                    return;
                }
                if (!GlobalVariable.IsDate(dtpToDate.Text))
                {
                    MessageBox.Show("To date is not valid.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpToDate.Focus();
                    return;
                }
                if (dtpFromDate.Value > dtpToDate.Value)
                {
                    MessageBox.Show("FromDate must be less than ToDate.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpToDate.Focus();
                    return;
                }
                string OrderDateFrom = dtpFromDate.Text;
                string OrderDateTo = dtpToDate.Text;
                string ProductID = "";
                if (chkProduct.Checked)
                {
                    if (cmbProduct.SelectedIndex < 0)
                    {
                        MessageBox.Show("Select valid product from list.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbProduct.Focus();
                        return;
                    }
                    ProductID = cmbProduct.SelectedValue.ToString();
                }
                else
                { ProductID = "00000000-0000-0000-0000-000000000000"; }

                string CategoryID = "";
                if (chkCategory.Checked)
                {
                    if (cmbCategory.SelectedIndex < 0)
                    {
                        MessageBox.Show("Select valid category from list.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbProduct.Focus();
                        return;
                    }
                    CategoryID = cmbCategory.SelectedValue.ToString();
                }
                else
                { CategoryID = "00000000-0000-0000-0000-000000000000"; }
                PrintSalesReport ItemWiseReport = new PrintSalesReport(OrderDateFrom, OrderDateTo, CategoryID, ProductID);
                ItemWiseReport.PrintItemWiseSalesReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Print", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
