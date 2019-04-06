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
    public partial class frmOutwardView : Form
    {
        public frmOutwardView()
        {
            InitializeComponent();
        }

        private List<ENT.OutwardMaster> SearchOrder()
        {
            List<ENT.OutwardMaster> lstENTInward = new List<ENT.OutwardMaster>();
            try
            {
                ENT.OutwardMaster objENTOutward = new ENT.OutwardMaster();
                objENTOutward.DateFrom = GlobalVariable.ChangeDate(dtpFromDate.Text);
                objENTOutward.DateTo = GlobalVariable.ChangeDate(dtpToDate.Text);
                objENTOutward.Mode = "GetByDate";
                lstENTInward = new DAL.OutwardMaster().GetOutwardMaster(objENTOutward);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lstENTInward;
        }

        private void FillGrid()
        {
            try
            {
                List<ENT.OutwardMaster> lstENTOrder = this.SearchOrder();
                txtTotal.Text = "0";
                dgvSales.Rows.Clear();
                for (int i = 0; i < lstENTOrder.Count; i++)
                {
                    dgvSales.Rows.Add();
                    dgvSales.Rows[i].Cells["colOutwardID"].Value = lstENTOrder[i].OutwardID;
                    dgvSales.Rows[i].Cells["colInvoiceNo"].Value = lstENTOrder[i].InvoiceNo;
                    dgvSales.Rows[i].Cells["colInvoiceDate"].Value = lstENTOrder[i].InvoiceDate;
                    dgvSales.Rows[i].Cells["colEmpID"].Value = lstENTOrder[i].EmpID;
                    dgvSales.Rows[i].Cells["colEmpName"].Value = lstENTOrder[i].EmpName;
                    dgvSales.Rows[i].Cells["colMobileNo"].Value = lstENTOrder[i].Mobile;
                    dgvSales.Rows[i].Cells["colFinalTotal"].Value = lstENTOrder[i].FinalTotal;
                    dgvSales.Rows[i].Cells["colRemark"].Value = lstENTOrder[i].Remark;
                    txtTotal.Text = Convert.ToString(Convert.ToDecimal(txtTotal.Text) + lstENTOrder[i].FinalTotal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportExcel()
        {
            try
            {
                ENT.OutwardMaster objENTOW = new ENT.OutwardMaster();
                DAL.OutwardMaster objDALOW = new DAL.OutwardMaster();
                objENTOW.DateFrom = GlobalVariable.ChangeDate(dtpFromDate.Text);
                objENTOW.DateTo = GlobalVariable.ChangeDate(dtpToDate.Text);
                objENTOW.Mode = "GetByDateForExport";
                DataTable dtMaster = objDALOW.GetDatatableForExportExcel(objENTOW);

                if (dtMaster.Rows.Count <= 0)
                {
                    MessageBox.Show("Data Not Found.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string fileName = "OUTWARD_" + dtpFromDate.Text.Replace("/", "-") + "_TO_" + dtpToDate.Text.Replace("/", "-") + ".xlsx";
                string folderPath = Path.Combine(Application.StartupPath, "Export Excel");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                #region Custome Excel
                var document = new SLDocument();
                document.AddWorksheet("Inward");

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

                int colCount = dtMaster.Columns.Count;
                int rowHeader = 1;
                var row = 1;
                document.SelectWorksheet("Inward");
                for (int i = 1; i < dtMaster.Columns.Count; i++)
                {
                    document.SetCellValue(row, i, dtMaster.Columns[i].ToString().ToUpper());
                }

                row = chkPrintDetail.Checked ? 3 : 2;
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

                    if (chkPrintDetail.Checked)
                    {
                        rowHeader = 2;
                        objENTOW.OutwardID = new Guid(Convert.ToString(dtMaster.Rows[i]["OutwardID"]));
                        objENTOW.Mode = "GetByIDForExport";
                        DataTable dtDetail = objDALOW.GetDatatableForExportExcel(objENTOW);
                        colCount = dtDetail.Columns.Count > dtMaster.Columns.Count ? dtDetail.Columns.Count : dtMaster.Columns.Count;
                        for (int j = 0; j < dtDetail.Rows.Count; j++)
                        {
                            int col = 2;
                            if (i == 0)
                            {
                                for (int a = 1; a < dtDetail.Columns.Count; a++)
                                {
                                    document.SetCellValue(2, col, dtDetail.Columns[a].ToString().ToUpper());
                                    col++;
                                }
                            }
                            col = 2;
                            for (int n = 1; n < dtDetail.Columns.Count; n++)
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
                            row++;
                        }
                        row++;
                    }
                }
                document.SetCellStyle(1, 1, rowHeader, colCount + 1, headerStyle);
                document.AutoFitColumn(1, colCount + 1);
                document.FreezePanes(rowHeader, 1);
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

        private void btnShow_Click(object sender, EventArgs e)
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
                FillGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSalesReport_Load(object sender, EventArgs e)
        {
            FillGrid();
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
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                frmOutward frm = new frmOutward(Guid.NewGuid().ToString(), "ADD");
                frm.ShowDialog();
                FillGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSales.RowCount > 0)
                {
                    string OutwardID = dgvSales.Rows[dgvSales.CurrentRow.Index].Cells["colOutwardID"].Value.ToString();
                    frmOutward frm = new frmOutward(OutwardID, "UPDATE");
                    frm.ShowDialog();
                    FillGrid();
                }
                else
                {
                    MessageBox.Show("Record Not Found.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSales.RowCount > 0)
                {
                    if (MessageBox.Show("Are you sure ? You want to delete selected invoice.", "Conformation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        bool resultM = false, resultD = false;
                        string OutwardID = dgvSales.Rows[dgvSales.CurrentRow.Index].Cells["colOutwardID"].Value.ToString();

                        ENT.OutwardDetail objENTOD = new ENT.OutwardDetail();
                        objENTOD.OutwardIDF = new Guid(OutwardID);
                        objENTOD.Mode = "DELETE";
                        if (new DAL.OutwardDetail().InsertUpdateDeleteOutwardDetail(objENTOD))
                        {
                            resultD = true;
                            ENT.OutwardMaster objENT = new ENT.OutwardMaster();
                            objENT.OutwardID = new Guid(OutwardID);
                            objENT.Mode = "DELETE";
                            if (new DAL.OutwardMaster().InsertUpdateDeleteOutwardMaster(objENT))
                            { resultM = true; }
                        }
                        if (resultM && resultD)
                        {
                            MessageBox.Show("Invoice Deleted Successfully.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillGrid();
                        }
                        else
                        {
                            MessageBox.Show("Invoice Not Deleted Successfully.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillGrid();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Record Not Found.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }
    }
}
