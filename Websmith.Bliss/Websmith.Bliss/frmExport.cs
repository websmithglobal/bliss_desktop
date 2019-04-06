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
    public partial class frmExport : Form
    {
        public frmExport()
        {
            InitializeComponent();
        }

        private void ExportExcel()
        {
            try
            {
                ENT.OrderBook objENTOrder = new ENT.OrderBook();
                DAL.OrderBook objDALOrder = new DAL.OrderBook();
                objENTOrder.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                objENTOrder.OrderDateFrom = GlobalVariable.ChangeDate(dtpFromDate.Text);
                objENTOrder.OrderDateTo = GlobalVariable.ChangeDate(dtpToDate.Text);
                objENTOrder.Mode = "GetOrderMasterByEmpID";
                DataTable dtMaster = objDALOrder.GetDatatableForExportExcel(objENTOrder);

                if (dtMaster.Rows.Count <= 0)
                {
                    MessageBox.Show("Data Not Found.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                pb1.Minimum = 0;
                pb1.Maximum = dtMaster.Rows.Count - 1;
                pb1.Value = 0;

                string fileName = dtpFromDate.Text.Replace("/", "-") + "_TO_" + dtpToDate.Text.Replace("/", "-") + ".xlsx";
                string folderPath = Path.Combine(Application.StartupPath, "Export Excel");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                #region Custome Excel
                var document = new SLDocument();
                document.AddWorksheet("Orders");

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
                detailStyleMain.Font.Bold = true;
                //detailStyleMain.Fill.SetPatternForegroundColor(SLThemeColorIndexValues.Accent6Color, 0.70);
                //detailStyleMain.Font.FontColor = System.Drawing.Color.White;
                #endregion

                var row = 1;
                document.SelectWorksheet("Orders");
                for (int i = 1; i < dtMaster.Columns.Count; i++)
                {
                    document.SetCellValue(row, i, dtMaster.Columns[i].ToString().ToUpper());
                }

                row = 3;
                for (int i = 0; i < (dtMaster.Rows.Count); i++)
                {
                    document.SetRowStyle(row, detailStyleMain);
                    for (int j = 1; j < dtMaster.Columns.Count; j++)
                    {
                        if (dtMaster.Rows[i][j] != null)
                        {
                            document.SetCellValue(row, j, Convert.ToString(dtMaster.Rows[i][j]));
                        }
                        else
                        {
                            document.SetCellValue(row, j, "");
                        }
                    }
                    row++;

                    objENTOrder.OrderID = new Guid(Convert.ToString(dtMaster.Rows[i][0]));
                    objENTOrder.Mode = "GetOrderTransactionByOrderID";
                    DataTable dtDetail = objDALOrder.GetDatatableForExportExcel(objENTOrder);
                    for (int j = 0; j < dtDetail.Rows.Count; j++)
                    {
                        int col = 2;
                        if (i == 0)
                        {
                            for (int a = 0; a < dtDetail.Columns.Count; a++)
                            {
                                document.SetCellValue(2, col, dtDetail.Columns[a].ToString().ToUpper());
                                col++;
                            }
                        }
                        col = 2;
                        for (int n = 0; n < dtDetail.Columns.Count; n++)
                        {
                            if (dtDetail.Rows[j][n] != null)
                            {
                                document.SetCellValue(row, col, Convert.ToString(dtDetail.Rows[j][n]));
                            }
                            else
                            {
                                document.SetCellValue(row, col, "");
                            }
                            col++;
                        }
                        //document.SetRowStyle(row, detailStyle);
                        row++;
                    }
                    pb1.Value = i;
                    row++;
                }
                document.SetCellStyle(1, 1, 2, dtMaster.Columns.Count, headerStyle);
                document.AutoFitColumn(1, dtMaster.Columns.Count);
                document.FreezePanes(2, 1);
                document.SaveAs(Path.Combine(folderPath, fileName));
                #endregion
                if (MessageBox.Show("Data Exported Successfully. You Want To Open Exported File ?", "Export", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(Path.Combine(folderPath, fileName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalVariable.IsDate(dtpFromDate.Text))
                {
                    MessageBox.Show("From date is not valid.", "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpFromDate.Focus();
                    return;
                }
                if (!GlobalVariable.IsDate(dtpToDate.Text))
                {
                    MessageBox.Show("To date is not valid.", "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpToDate.Focus();
                    return;
                }
                ExportExcel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmExport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmExport_Load(object sender, EventArgs e)
        {
            dtpFromDate.Focus();
        }
    }
}
