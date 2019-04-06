using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.Bliss
{
    public class PrintSalesReport
    {
        PrintDocument pdoc = null;
        string BranchAddress = string.Empty;
        string BranchName = string.Empty;
        string BranchMobileNo = string.Empty;
        string BranchGSTIN = string.Empty;
        string KOTFooter = string.Empty;
        string FromDate = string.Empty;
        string ToDate = string.Empty;
        string CategoryID = string.Empty;
        string ProductID = string.Empty;

        public PrintSalesReport()
        {

        }

        public PrintSalesReport(string _FromDate, string _ToDate)
        {
            FromDate = _FromDate;
            ToDate = _ToDate;
        }

        public PrintSalesReport(string _FromDate, string _ToDate, string _CategoryID, string _ProductID)
        {
            FromDate = _FromDate;
            ToDate = _ToDate;
            CategoryID = _CategoryID;
            ProductID = _ProductID;
        }

        #region Common Function

        private void GetBranchMasterSetting()
        {
            try
            {
                ENT.BranchMasterSetting objENTBMS = new ENT.BranchMasterSetting();
                List<ENT.BranchMasterSetting> lstENTBMS = new List<ENT.BranchMasterSetting>();
                DAL.BranchMasterSetting objDALBMS = new DAL.BranchMasterSetting();

                objENTBMS.Mode = "GetAllRecord";
                lstENTBMS = objDALBMS.getBranchMasterSetting(objENTBMS);
                if (lstENTBMS.Count > 0)
                {
                    BranchName = lstENTBMS[0].BranchName;
                    BranchAddress = lstENTBMS[0].Address + ", " + lstENTBMS[0].SubAreaStreet + " " + lstENTBMS[0].PinCode;
                    BranchMobileNo = lstENTBMS[0].MobileNo;
                    BranchGSTIN = lstENTBMS[0].TinGSTNo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetDefaultPrinterForYourStation()
        {
            string DefaultPrinter = "";
            try
            {
                DAL.PrinterMapping objDALYour = new DAL.PrinterMapping();
                ENT.PrinterMapping objENTYour = new ENT.PrinterMapping();
                List<ENT.PrinterMapping> lstENTYour = new List<ENT.PrinterMapping>();

                objENTYour.Mode = "GetTopOne";
                objENTYour.DeviceTypeID = Convert.ToInt32(ENT.DeviceMaster.DeviceTypeName.POS);
                objENTYour.PartID = 1;
                lstENTYour = objDALYour.GetPrinterMapping(objENTYour);
                if (lstENTYour.Count > 0)
                {
                    DefaultPrinter = lstENTYour[0].DeviceIP;
                }
                else
                {
                    DefaultPrinter = Properties.Settings.Default.PrinterPath.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return DefaultPrinter;
        }

        #endregion

        #region Item Wise Sales Report
        List<ENT.Transaction> lstENTTrPrint;
        decimal SRQtyTot = 0;
        decimal SRAmtTot = 0;
        decimal SRQuantityTot = 0;
        decimal SRAmountTot = 0;

        public void PrintItemWiseSalesReportOld()
        {
            try
            {
                PrintDialog pd = new PrintDialog();
                pdoc = new PrintDocument();
                PrinterSettings ps = new PrinterSettings();
                Font font = new Font("Verdana", 15);
                PaperSize psize = new PaperSize("Custom", 100, 200);
                pd.Document = pdoc;
                pd.Document.DefaultPageSettings.PaperSize = psize;
                pdoc.DefaultPageSettings.PaperSize.Height = 820;
                pdoc.DefaultPageSettings.PaperSize.Width = 520;
                pdoc.PrinterSettings.PrinterName = GetDefaultPrinterForYourStation();  //"EPSON TM-m30 Receipt5";
                pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintItemWiseSalesReportOld);
                pdoc.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void pdoc_PrintItemWiseSalesReportOld(object sender, PrintPageEventArgs e)
        {
            try
            {
                Graphics graphics = e.Graphics;
                String underLine = "-------------------------------------------";
                SolidBrush brush = new SolidBrush(Color.Black);
                Font fontLine = new Font("Verdana", 10);
                Font fontCommon = new Font("Verdana", 8);
                Font fontDetail = new Font("Verdana", 7);
                Font fontSmallHeding = new Font("Verdana", 9, FontStyle.Bold);
                Font fontDetailHead = new Font("Verdana", 7, FontStyle.Bold);
                Font fontHeading = new Font("Verdana", 14, FontStyle.Bold);
                StringFormat formatRightToLeft = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                RectangleF rect;

                int startX = 5;
                int startY = 10;
                int Offset = 10;

                decimal SubTotQuantity = 0;
                decimal NetTotQuantity = 0;
                decimal SubTotAmount = 0;
                decimal NetTotAmount = 0;

                GetBranchMasterSetting();
                rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                graphics = DrawStringCenter(graphics, BranchName, fontHeading, rect, Color.Black);
                Offset = Offset + 25;

                rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                graphics = DrawStringCenter(graphics, DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss tt"), fontCommon, rect, Color.Black);
                Offset = Offset + 10;

                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                rect = new RectangleF(startX, startY + Offset, 270.0F, 20.0F);
                graphics = DrawStringCenter(graphics, "Item Wise Sales Report", fontSmallHeding, rect, Color.Black);
                Offset = Offset + 15;

                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                graphics.DrawString("From Date : " + FromDate + " To :" + ToDate, fontCommon, brush, rect);
                Offset = Offset + 15;

                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                rect = new RectangleF(startX, startY + Offset, 150.0F, 15.0F);
                graphics = DrawStringLeft(graphics, "Item", fontDetailHead, rect, Color.Black);

                startX = startX + 150;
                rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                graphics = DrawStringCenter(graphics, "Qty.", fontDetailHead, rect, Color.Black);

                startX = startX + 50;
                rect = new RectangleF(startX, startY + Offset, 70.0F, 15.0F);
                graphics = DrawStringCenter(graphics, "Total", fontDetailHead, rect, Color.Black);

                Offset = Offset + 15;
                startX = 5;
                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;

                DAL.Transaction objDALTR = new DAL.Transaction();
                List<ENT.Transaction> lstENTTR = new List<ENT.Transaction>();
                ENT.Transaction objENTTR = new ENT.Transaction();
                objENTTR.CategoryID = new Guid(CategoryID);
                objENTTR.ProductID = new Guid(ProductID);
                objENTTR.OrderDateFrom = GlobalVariable.ChangeDate(FromDate);
                objENTTR.OrderDateTo = GlobalVariable.ChangeDate(ToDate);
                objENTTR.Mode = "GetCategoryForSalesReport";
                lstENTTR = objDALTR.getOrderTransaction(objENTTR);

                for (int n = 0; n < lstENTTR.Count; n++)
                {
                    SubTotQuantity = 0;
                    SubTotAmount = 0;

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString(lstENTTR[n].CategoryName.Trim(), fontDetailHead, brush, rect);
                    Offset = Offset + 10;

                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    List<ENT.Transaction> lstENTProd = new List<ENT.Transaction>();
                    objENTTR.CategoryID = lstENTTR[n].CategoryID;
                    objENTTR.OrderDateFrom = GlobalVariable.ChangeDate(FromDate);
                    objENTTR.OrderDateTo = GlobalVariable.ChangeDate(ToDate);
                    objENTTR.Mode = "GetProductByCategoryForSalesReport";
                    lstENTProd = objDALTR.getOrderTransaction(objENTTR);

                    for (int i = 0; i < lstENTProd.Count; i++)
                    {
                        startX = 5;
                        rect = new RectangleF(startX, startY + Offset, 150.0F, 15.0F);
                        graphics = DrawStringLeft(graphics, lstENTProd[i].ProductName, fontDetail, rect, Color.Black);

                        startX = startX + 150;
                        rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                        graphics = DrawStringRight(graphics, lstENTProd[i].Quantity.ToString(), fontDetail, rect, Color.Black);

                        startX = startX + 50;
                        rect = new RectangleF(startX, startY + Offset, 70.0F, 15.0F);
                        graphics = DrawStringRight(graphics, lstENTProd[i].TotalAmount.ToString(), fontDetail, rect, Color.Black);

                        Offset = Offset + 15;
                        SubTotQuantity = SubTotQuantity + lstENTProd[i].Quantity;
                        SubTotAmount = SubTotAmount + lstENTProd[i].TotalAmount;
                    }

                    NetTotAmount = NetTotAmount + SubTotAmount;
                    NetTotQuantity = NetTotQuantity + SubTotQuantity;

                    startX = 150;
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;

                    rect = new RectangleF(startX, startY + Offset, 50.0F, 25.0F);
                    graphics.DrawString(SubTotQuantity.ToString(), fontDetailHead, brush, rect, formatRightToLeft);
                    startX = startX + 50;

                    rect = new RectangleF(startX, startY + Offset, 70.0F, 25.0F);
                    graphics.DrawString(SubTotAmount.ToString(), fontDetailHead, brush, rect, formatRightToLeft);
                    Offset = Offset + 15;

                    startX = 150;
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;

                    startX = 5;
                }

                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                rect = new RectangleF(startX, startY + Offset, 150.0F, 25.0F);
                graphics.DrawString("Grand Total : ", fontDetailHead, brush, rect);
                startX = startX + 150;

                rect = new RectangleF(startX, startY + Offset, 50.0F, 25.0F);
                graphics.DrawString(NetTotQuantity.ToString(), fontDetailHead, brush, rect, formatRightToLeft);
                startX = startX + 50;

                rect = new RectangleF(startX, startY + Offset, 70.0F, 25.0F);
                graphics.DrawString(NetTotAmount.ToString(), fontDetailHead, brush, rect, formatRightToLeft);
                startX = startX + 150;

                Offset = Offset + 15;
                startX = 5;
                
                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PrintItemWiseSalesReport()
        {
            try
            {
                IsFirstPage = true;
                IsLastPage = false;
                cnt = 0;
                pagecnt = 0;
                pageno = 0;
                SRFoodTotal = 0;
                SRSubTotal = 0;
                SRTotalDisc = 0;
                SRTotalSGSTAmount = 0;
                SRTotalCGSTAmount = 0;
                SRTipTotal = 0;
                SRDeliveryChargeTotal = 0;
                SRNetAmount = 0;

                DAL.Transaction objDALTR = new DAL.Transaction();
                List<ENT.Transaction> lstENTTR = new List<ENT.Transaction>();
                ENT.Transaction objENTTR = new ENT.Transaction();
                objENTTR.CategoryID = new Guid(CategoryID);
                objENTTR.ProductID = new Guid(ProductID);
                objENTTR.OrderDateFrom = GlobalVariable.ChangeDate(FromDate);
                objENTTR.OrderDateTo = GlobalVariable.ChangeDate(ToDate);
                objENTTR.Mode = "GetPrintItemWiseSalesReport";
                lstENTTR = objDALTR.getOrderTransaction(objENTTR);

                if (lstENTTR.Count <= 0)
                {
                    MessageBox.Show("Record Not Found.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                double page = 0;
                double record = 50;

                if (lstENTTR.Count <= 50)
                    page = 1;
                else
                    page = Math.Round(Convert.ToDouble(lstENTTR.Count) / record, 2, MidpointRounding.AwayFromZero);

                if (Convert.ToInt32(page) < page)
                { pagecnt = Convert.ToInt32(page) + 1; }
                else
                { pagecnt = Convert.ToInt32(page); }

                for (int i = 0; i < page; i++)
                {
                    pageno = i + 1;
                    if (i != 0) { IsFirstPage = false; }

                    if (i == Convert.ToInt32(page) || page == 1) { IsLastPage = true; }

                    lstENTTrPrint = new List<ENT.Transaction>();

                    for (int j = 0; j < record; j++)
                    {
                        #region CREATE LIST
                        lstENTTrPrint.Add(new ENT.Transaction()
                        {
                            CategoryID = lstENTTR[cnt].CategoryID,
                            CategoryName = lstENTTR[cnt].CategoryName,
                            EmployeeID = lstENTTR[cnt].EmployeeID,
                            IsSendToKitchen = lstENTTR[cnt].IsSendToKitchen,
                            Mode = (cnt + 1).ToString(),
                            OrderDateFrom = lstENTTR[cnt].OrderDateFrom,
                            OrderDateTo = lstENTTR[cnt].OrderDateTo,
                            OrderID = lstENTTR[cnt].OrderID,
                            ProductID = lstENTTR[cnt].ProductID,
                            RUserID = lstENTTR[cnt].RUserID,
                            RUserType = lstENTTR[cnt].RUserType,
                            EndDate = lstENTTR[cnt].EndDate,
                            IsUPStream = lstENTTR[cnt].IsUPStream,
                            ProductName = lstENTTR[cnt].ProductName,
                            Quantity= lstENTTR[cnt].Quantity,
                            Rate = lstENTTR[cnt].Rate,
                            Sort = lstENTTR[cnt].Sort,
                            SpecialRequest = lstENTTR[cnt].SpecialRequest,
                            StartDate = lstENTTR[cnt].StartDate,
                            TotalAmount = lstENTTR[cnt].TotalAmount,
                            TransactionID= lstENTTR[cnt].TransactionID
                        });
                        #endregion

                        cnt++;
                        if (lstENTTR.Count == cnt) { break; }
                    }

                    PrintDialog pd = new PrintDialog();
                    pdoc = new PrintDocument();
                    PrinterSettings ps = new PrinterSettings();
                    Font font = new Font("Verdana", 15);
                    PaperSize psize = new PaperSize("Custom", 100, 820);
                    pd.Document = pdoc;
                    pd.Document.DefaultPageSettings.PaperSize = psize;
                    pdoc.DefaultPageSettings.PaperSize.Height = 820;
                    pdoc.DefaultPageSettings.PaperSize.Width = 120;
                    pdoc.PrinterSettings.PrinterName = GetDefaultPrinterForYourStation();  //"EPSON TM-m30 Receipt5";
                    pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintItemWiseSalesReport);
                    pdoc.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void pdoc_PrintItemWiseSalesReport(object sender, PrintPageEventArgs e)
        {
            try
            {
                Graphics graphics = e.Graphics;
                String underLine = "-------------------------------------------";
                SolidBrush brush = new SolidBrush(Color.Black);
                Font fontLine = new Font("Verdana", 10);
                Font fontCommon = new Font("Verdana", 8);
                Font fontDetail = new Font("Verdana", 7);
                Font fontSmallHeding = new Font("Verdana", 9, FontStyle.Bold);
                Font fontDetailHead = new Font("Verdana", 7, FontStyle.Bold);
                Font fontHeading = new Font("Verdana", 14, FontStyle.Bold);
                StringFormat formatRightToLeft = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                RectangleF rect;

                int startX = 5;
                int startY = 5;
                int Offset = 10;

                if (lstENTTrPrint.Count > 0)
                {
                    if (IsFirstPage)
                    {
                        GetBranchMasterSetting();
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, BranchName, fontHeading, rect, Color.Black);
                        Offset = Offset + 25;

                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss tt"), fontCommon, rect, Color.Black);
                        Offset = Offset + 10;

                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;

                        rect = new RectangleF(startX, startY + Offset, 270.0F, 20.0F);
                        graphics = DrawStringCenter(graphics, "Category Wise Item Sales Report", fontSmallHeding, rect, Color.Black);
                        Offset = Offset + 15;

                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;

                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("From Date : " + FromDate + " To :" + ToDate, fontCommon, brush, rect);
                        Offset = Offset + 15;
                    }

                    if (IsFirstPage)
                    { graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset); }
                    else
                    { graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY); }
                    Offset = Offset + 15;

                    rect = new RectangleF(startX, startY + Offset, 150.0F, 15.0F);
                    graphics = DrawStringLeft(graphics, "Item", fontDetailHead, rect, Color.Black);

                    startX = startX + 150;
                    rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "Qty.", fontDetailHead, rect, Color.Black);

                    startX = startX + 50;
                    rect = new RectangleF(startX, startY + Offset, 75.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "Total", fontDetailHead, rect, Color.Black);

                    Offset = Offset + 15;
                    startX = 5;
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    for (int i = 0; i < lstENTTrPrint.Count; i++)
                    {
                        startX = 5;

                        if (i == 0)
                        {
                            rect = new RectangleF(startX, startY + Offset, 270.0F, 20.0F);
                            graphics = DrawStringLeft(graphics, lstENTTrPrint[i].CategoryName, fontDetailHead, rect, Color.Black);
                            Offset = Offset + 20;
                        }
                        else
                        {
                            if (lstENTTrPrint[i].CategoryName != lstENTTrPrint[i - 1].CategoryName)
                            {
                              
                                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                                Offset = Offset + 10;

                                rect = new RectangleF(startX, startY + Offset, 150.0F, 15.0F);
                                graphics = DrawStringLeft(graphics, "Category Wise Total :", fontDetailHead, rect, Color.Black);

                                startX = startX + 150;
                                rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                                graphics = DrawStringRight(graphics, SRQtyTot.ToString(), fontDetailHead, rect, Color.Black);

                                startX = startX + 50;
                                rect = new RectangleF(startX, startY + Offset, 75.0F, 15.0F);
                                graphics = DrawStringRight(graphics, SRAmtTot.ToString(), fontDetailHead, rect, Color.Black);

                                startX = 5;
                                Offset = Offset + 10;
                                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                                Offset = Offset + 15;

                                SRAmtTot = 0;
                                SRQtyTot = 0;
                            }
                            if (lstENTTrPrint[i].CategoryName != lstENTTrPrint[i - 1].CategoryName)
                            {
                                rect = new RectangleF(startX, startY + Offset, 270.0F, 20.0F);
                                graphics = DrawStringLeft(graphics, lstENTTrPrint[i].CategoryName, fontDetailHead, rect, Color.Black);
                                Offset = Offset + 20;
                            }
                        }

                        startX = 5;
                        rect = new RectangleF(startX, startY + Offset, 150.0F, 15.0F);
                        graphics = DrawStringLeft(graphics, lstENTTrPrint[i].ProductName, fontDetail, rect, Color.Black);

                        startX = startX + 150;
                        rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                        graphics = DrawStringRight(graphics, lstENTTrPrint[i].Quantity.ToString(), fontDetail, rect, Color.Black);

                        startX = startX + 50;
                        rect = new RectangleF(startX, startY + Offset, 75.0F, 15.0F);
                        graphics = DrawStringRight(graphics, lstENTTrPrint[i].TotalAmount.ToString(), fontDetail, rect, Color.Black);
                        Offset = Offset + 15;

                        SRQtyTot = SRQtyTot + lstENTTrPrint[i].Quantity;
                        SRAmtTot = SRAmtTot + lstENTTrPrint[i].TotalAmount;
                        SRQuantityTot = SRQuantityTot + lstENTTrPrint[i].Quantity;
                        SRAmountTot = SRAmountTot + lstENTTrPrint[i].TotalAmount;
                    }

                    startX = 5;

                    if (IsLastPage)
                    {
                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;

                        rect = new RectangleF(startX, startY + Offset, 150.0F, 15.0F);
                        graphics = DrawStringLeft(graphics, "Category Wise Total :", fontDetailHead, rect, Color.Black);

                        startX = startX + 150;
                        rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                        graphics = DrawStringRight(graphics, SRQtyTot.ToString(), fontDetailHead, rect, Color.Black);

                        startX = startX + 50;
                        rect = new RectangleF(startX, startY + Offset, 75.0F, 15.0F);
                        graphics = DrawStringRight(graphics, SRAmtTot.ToString(), fontDetailHead, rect, Color.Black);

                        startX = 5;
                        Offset = Offset + 10;
                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;
                                                
                        rect = new RectangleF(startX, startY + Offset, 150.0F, 15.0F);
                        graphics = DrawStringLeft(graphics, "Grand Total :", fontDetailHead, rect, Color.Black);

                        startX = startX + 150;
                        rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                        graphics = DrawStringRight(graphics, SRQuantityTot.ToString(), fontDetailHead, rect, Color.Black);

                        startX = startX + 50;
                        rect = new RectangleF(startX, startY + Offset, 75.0F, 15.0F);
                        graphics = DrawStringRight(graphics, SRAmountTot.ToString(), fontDetailHead, rect, Color.Black);
                        Offset = Offset + 15;
                    }

                    startX = 5;
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString(DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss tt"), fontDetail, brush, rect);
                    graphics.DrawString("Page " + pageno.ToString() + " of " + pagecnt.ToString(), fontDetail, brush, rect, formatRightToLeft);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Sales Invoice Report

        List<ENT.OrderBook> lstENTPrint;
        bool IsFirstPage = true;
        bool IsLastPage = false;
        int cnt = 0;
        int pagecnt = 0;
        int pageno = 0;
        
        decimal SRFoodTotal = 0;
        decimal SRSubTotal = 0;
        decimal SRTotalDisc = 0;
        decimal SRTotalSGSTAmount = 0;
        decimal SRTotalCGSTAmount = 0;
        decimal SRTipTotal = 0;
        decimal SRDeliveryChargeTotal = 0;
        decimal SRNetAmount = 0;

        public void PrintSalesInvoiceReportTest()
        {
            try
            {
                IsFirstPage = true;
                IsLastPage = false;
                cnt = 0;
                pagecnt = 0;
                pageno = 0;
                SRFoodTotal = 0;
                SRSubTotal = 0;
                SRTotalDisc = 0;
                SRTotalSGSTAmount = 0;
                SRTotalCGSTAmount = 0;
                SRTipTotal = 0;
                SRDeliveryChargeTotal = 0;
                SRNetAmount = 0;

                DAL.OrderBook objDALOrder = new DAL.OrderBook();
                List<ENT.OrderBook> lstENTOrder = new List<ENT.OrderBook>();
                ENT.OrderBook objENTOrder = new ENT.OrderBook();
                objENTOrder.OrderDateFrom = GlobalVariable.ChangeDate(FromDate);
                objENTOrder.OrderDateTo = GlobalVariable.ChangeDate(ToDate);
                objENTOrder.Mode = "GetPrintSalesReport";
                lstENTOrder = objDALOrder.getOrder(objENTOrder);

                if (lstENTOrder.Count <= 0)
                {
                    MessageBox.Show("Record Not Found.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                double page = 0;
                double record = 50;

                if (lstENTOrder.Count <= 50)
                    page = 1;
                else
                    page = Math.Round(Convert.ToDouble(lstENTOrder.Count) / record, 2, MidpointRounding.AwayFromZero);

                if (Convert.ToInt32(page) < page)
                { pagecnt = Convert.ToInt32(page) + 1; }
                else
                { pagecnt = Convert.ToInt32(page); }


                for (int i = 0; i < page; i++)
                {
                    pageno = i + 1;
                    if (i != 0) { IsFirstPage = false; }

                    if (i == Convert.ToInt32(page) || page == 1) { IsLastPage = true; }

                    lstENTPrint = new List<ENT.OrderBook>();

                    for (int j = 0; j < record; j++)
                    {
                        #region CREATE LIST
                        lstENTPrint.Add(new ENT.OrderBook()
                        {
                            Address = lstENTOrder[cnt].Address,
                            CategoryID = lstENTOrder[cnt].CategoryID,
                            CGSTAmount = lstENTOrder[cnt].CGSTAmount,
                            CustomerID = lstENTOrder[cnt].CustomerID,
                            DeliveryCharge = lstENTOrder[cnt].DeliveryCharge,
                            DeliveryType = lstENTOrder[cnt].DeliveryType,
                            DeliveryTypeName = lstENTOrder[cnt].DeliveryTypeName,
                            Discount = lstENTOrder[cnt].Discount,
                            DiscountID = lstENTOrder[cnt].DiscountID,
                            DiscountPer = lstENTOrder[cnt].DiscountPer,
                            DiscountRemark = lstENTOrder[cnt].DiscountRemark,
                            DiscountType = lstENTOrder[cnt].DiscountType,
                            EmployeeID = lstENTOrder[cnt].EmployeeID,
                            EndTime = lstENTOrder[cnt].EndTime,
                            ExtraCharge = lstENTOrder[cnt].ExtraCharge,
                            HeaderStatus = lstENTOrder[cnt].HeaderStatus,
                            IsPrint = lstENTOrder[cnt].IsPrint,
                            IsSendToKitchen = lstENTOrder[cnt].IsSendToKitchen,
                            MobileNo = lstENTOrder[cnt].MobileNo,
                            Mode = (cnt + 1).ToString(),
                            Name = lstENTOrder[cnt].Name,
                            OrderActions = lstENTOrder[cnt].OrderActions,
                            OrderActionsName = lstENTOrder[cnt].OrderActionsName,
                            OrderDate = lstENTOrder[cnt].OrderDate,
                            OrderDateFrom = lstENTOrder[cnt].OrderDateFrom,
                            OrderDateTo = lstENTOrder[cnt].OrderDateTo,
                            OrderID = lstENTOrder[cnt].OrderID,
                            OrderNo = lstENTOrder[cnt].OrderNo,
                            OrderSpecialRequest = lstENTOrder[cnt].OrderSpecialRequest,
                            OrderStatus = lstENTOrder[cnt].OrderStatus,
                            PayableAmount = lstENTOrder[cnt].PayableAmount,
                            PaymentMethod = lstENTOrder[cnt].PaymentMethod,
                            PaymentMethodText = lstENTOrder[cnt].PaymentMethodText,
                            ProductID = lstENTOrder[cnt].ProductID,
                            RUserID = lstENTOrder[cnt].RUserID,
                            RUserType = lstENTOrder[cnt].RUserType,
                            SearchKey = lstENTOrder[cnt].SearchKey,
                            SGSTAmount = lstENTOrder[cnt].SGSTAmount,
                            StartTime = lstENTOrder[cnt].StartTime,
                            SubTotal = lstENTOrder[cnt].SubTotal,
                            TableID = lstENTOrder[cnt].TableID,
                            TableName = lstENTOrder[cnt].TableName,
                            TableStatusID = lstENTOrder[cnt].TableStatusID,
                            TaxLabel1 = lstENTOrder[cnt].TaxLabel1,
                            TaxLabel2 = lstENTOrder[cnt].TaxLabel2,
                            TaxPercent1 = lstENTOrder[cnt].TaxPercent1,
                            TaxPercent2 = lstENTOrder[cnt].TaxPercent2,
                            TipGratuity = lstENTOrder[cnt].TipGratuity,
                            TokenNo = lstENTOrder[cnt].TokenNo,
                            TotalTax = lstENTOrder[cnt].TotalTax
                        });
                        #endregion

                        cnt++;
                        if (lstENTOrder.Count == cnt) { break; }
                    }

                    PrintDialog pd = new PrintDialog();
                    pdoc = new PrintDocument();
                    PrinterSettings ps = new PrinterSettings();
                    Font font = new Font("Verdana", 15);
                    PaperSize psize = new PaperSize("Custom", 100, 820);
                    pd.Document = pdoc;
                    pd.Document.DefaultPageSettings.PaperSize = psize;
                    pdoc.DefaultPageSettings.PaperSize.Height = 820;
                    pdoc.DefaultPageSettings.PaperSize.Width = 120;
                    pdoc.PrinterSettings.PrinterName = GetDefaultPrinterForYourStation();  //"EPSON TM-m30 Receipt5";
                    pdoc.PrintPage += new PrintPageEventHandler(pdoc_SalesSummaryTest);
                    pdoc.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void pdoc_SalesSummaryTest(object sender, PrintPageEventArgs e)
        {
            try
            {
                Graphics graphics = e.Graphics;
                String underLine = "-------------------------------------------";
                SolidBrush brush = new SolidBrush(Color.Black);
                Font fontLine = new Font("Verdana", 10);
                Font fontCommon = new Font("Verdana", 8);
                Font fontDetail = new Font("Verdana", 7);
                Font fontSmallHeding = new Font("Verdana", 9, FontStyle.Bold);
                Font fontDetailHead = new Font("Verdana", 7, FontStyle.Bold);
                Font fontHeading = new Font("Verdana", 14, FontStyle.Bold);
                StringFormat formatRightToLeft = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                RectangleF rect;

                int startX = 5;
                int startY = 5;
                int Offset = 10;

                if (lstENTPrint.Count > 0)
                {
                    if (IsFirstPage)
                    {
                        GetBranchMasterSetting();
                        rect = new RectangleF(startX, startY, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, BranchName, fontHeading, rect, Color.Black);
                        Offset = Offset + 25;

                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss tt"), fontCommon, rect, Color.Black);
                        Offset = Offset + 10;

                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;

                        rect = new RectangleF(startX, startY + Offset, 270.0F, 20.0F);
                        graphics = DrawStringCenter(graphics, "Bill Wise Sales Report", fontSmallHeding, rect, Color.Black);
                        Offset = Offset + 15;

                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;

                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("From Date : " + FromDate + " To :" + ToDate, fontCommon, brush, rect);
                        Offset = Offset + 15;
                    }

                    if (IsFirstPage)
                    { graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset); }
                    else
                    { graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY); }
                    Offset = Offset + 15;

                    rect = new RectangleF(startX, startY + Offset, 30.0F, 15.0F);
                    graphics = DrawStringLeft(graphics, "Bill", fontDetailHead, rect, Color.Black);

                    startX = startX + 30;
                    rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "Tbl Wt", fontDetailHead, rect, Color.Black);

                    startX = startX + 50;
                    rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "Each", fontDetailHead, rect, Color.Black);

                    startX = startX + 50;
                    rect = new RectangleF(startX, startY + Offset, 40.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "Disc.", fontDetailHead, rect, Color.Black);

                    startX = startX + 40;
                    rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "Total", fontDetailHead, rect, Color.Black);

                    startX = startX + 50;
                    rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "Remark", fontDetailHead, rect, Color.Black);

                    Offset = Offset + 15;
                    startX = 5;
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    for (int i = 0; i < lstENTPrint.Count; i++)
                    {
                        startX = 5;
                        rect = new RectangleF(startX, startY + Offset, 30.0F, 15.0F);
                        graphics = DrawStringLeft(graphics, lstENTPrint[i].Mode, fontDetail, rect, Color.Black);

                        startX = startX + 30;
                        rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                        graphics = DrawStringLeft(graphics, lstENTPrint[i].TableName, fontDetail, rect, Color.Black);

                        startX = startX + 50;
                        rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                        graphics = DrawStringRight(graphics, lstENTPrint[i].SubTotal.ToString(), fontDetail, rect, Color.Black);

                        startX = startX + 50;
                        rect = new RectangleF(startX, startY + Offset, 40.0F, 15.0F);
                        graphics = DrawStringRight(graphics, lstENTPrint[i].Discount.ToString(), fontDetail, rect, Color.Black);

                        startX = startX + 40;
                        rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                        graphics = DrawStringRight(graphics, lstENTPrint[i].PayableAmount.ToString(), fontDetail, rect, Color.Black);

                        startX = startX + 50;
                        rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                        graphics = DrawStringRight(graphics, lstENTPrint[i].PaymentMethodText, fontDetail, rect, Color.Black);
                        Offset = Offset + 15;

                        SRSubTotal = SRSubTotal + lstENTPrint[i].SubTotal;
                        SRTotalDisc = SRTotalDisc + lstENTPrint[i].Discount;
                        SRTotalSGSTAmount = SRTotalSGSTAmount + lstENTPrint[i].SGSTAmount;
                        SRTotalCGSTAmount = SRTotalCGSTAmount + lstENTPrint[i].CGSTAmount;
                        SRTipTotal = SRTipTotal + lstENTPrint[i].TipGratuity;
                        SRDeliveryChargeTotal = SRDeliveryChargeTotal + lstENTPrint[i].DeliveryCharge;
                        SRNetAmount = SRNetAmount + lstENTPrint[i].PayableAmount;
                    }

                    startX = 5;

                    if (IsLastPage)
                    {
                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;

                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Each Total Amount : ", fontCommon, brush, rect);
                        graphics.DrawString(SRSubTotal.ToString(), fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;

                        if (SRTotalSGSTAmount <= 0)
                        {
                            rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                            graphics.DrawString("SGST : ", fontCommon, brush, rect);
                            graphics.DrawString(SRTotalSGSTAmount.ToString(), fontCommon, brush, rect, formatRightToLeft);
                            Offset = Offset + 15;
                        }

                        if (SRTotalCGSTAmount <= 0)
                        {
                            rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                            graphics.DrawString("CGST : ", fontCommon, brush, rect);
                            graphics.DrawString(SRTotalCGSTAmount.ToString(), fontCommon, brush, rect, formatRightToLeft);
                            Offset = Offset + 15;
                        }

                        if (SRTipTotal <= 0)
                        {
                            rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                            graphics.DrawString("Tip (+) : ", fontCommon, brush, rect);
                            graphics.DrawString(SRTipTotal.ToString(), fontCommon, brush, rect, formatRightToLeft);
                            Offset = Offset + 15;
                        }

                        if (SRDeliveryChargeTotal <= 0)
                        {
                            rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                            graphics.DrawString("Delivery Charge (+) : ", fontCommon, brush, rect);
                            graphics.DrawString(SRDeliveryChargeTotal.ToString(), fontCommon, brush, rect, formatRightToLeft);
                            Offset = Offset + 15;
                        }

                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Discount Amount (-) : ", fontCommon, brush, rect);
                        graphics.DrawString(SRTotalDisc.ToString(), fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;

                        decimal roundOffMinus = 0;
                        decimal roundOffPlus = 0;
                        roundOffMinus = SRNetAmount - Math.Round(SRNetAmount, 0, MidpointRounding.AwayFromZero);
                        roundOffPlus = SRNetAmount - Math.Round(SRNetAmount, 0, MidpointRounding.AwayFromZero);

                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Rounding Amt.(+) : ", fontCommon, brush, rect);
                        graphics.DrawString(roundOffPlus > 0 ? roundOffPlus.ToString() : "0", fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;

                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Rounding Amt.(-) : ", fontCommon, brush, rect);
                        graphics.DrawString(roundOffMinus < 0 ? roundOffMinus.ToString() : "0", fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;

                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 10;

                        SRNetAmount = roundOffPlus > 0 ? SRNetAmount - roundOffPlus : roundOffMinus < 0 ? SRNetAmount - roundOffMinus : SRNetAmount;

                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Net Amount : ", fontSmallHeding, brush, rect);
                        graphics.DrawString(SRNetAmount.ToString(), fontSmallHeding, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;

                    }

                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString(DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss tt"), fontDetail, brush, rect);
                    graphics.DrawString("Page " + pageno.ToString() + " of " + pagecnt.ToString(), fontDetail, brush, rect, formatRightToLeft);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Date Wise Sales Summary

        public void PrintDateWiseSalesInvoiceReport()
        {
            try
            {
                IsFirstPage = true;
                IsLastPage = false;
                cnt = 0;
                pagecnt = 0;
                pageno = 0;
                SRFoodTotal = 0;
                SRSubTotal = 0;
                SRTotalDisc = 0;
                SRTotalSGSTAmount = 0;
                SRTotalCGSTAmount = 0;
                SRTipTotal = 0;
                SRDeliveryChargeTotal = 0;
                SRNetAmount = 0;

                DAL.OrderBook objDALOrder = new DAL.OrderBook();
                List<ENT.OrderBook> lstENTOrder = new List<ENT.OrderBook>();
                ENT.OrderBook objENTOrder = new ENT.OrderBook();
                objENTOrder.OrderDateFrom = GlobalVariable.ChangeDate(FromDate);
                objENTOrder.OrderDateTo = GlobalVariable.ChangeDate(ToDate);
                objENTOrder.Mode = "GetPrintSalesReport";
                lstENTOrder = objDALOrder.getOrder(objENTOrder);

                if (lstENTOrder.Count <= 0)
                {
                    MessageBox.Show("Record Not Found.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                double page = 0;
                double record = 50;

                if (lstENTOrder.Count <= 50)
                    page = 1;
                else
                    page = Math.Round(Convert.ToDouble(lstENTOrder.Count) / record, 2, MidpointRounding.AwayFromZero);

                if (Convert.ToInt32(page) < page)
                { pagecnt = Convert.ToInt32(page) + 1; }
                else
                { pagecnt = Convert.ToInt32(page); }

                for (int i = 0; i < page; i++)
                {
                    pageno = i + 1;
                    if (i != 0) { IsFirstPage = false; }

                    if (i == Convert.ToInt32(page) || page == 1) { IsLastPage = true; }

                    lstENTPrint = new List<ENT.OrderBook>();

                    for (int j = 0; j < record; j++)
                    {
                        #region CREATE LIST
                        lstENTPrint.Add(new ENT.OrderBook()
                        {
                            Address = lstENTOrder[cnt].Address,
                            CategoryID = lstENTOrder[cnt].CategoryID,
                            CGSTAmount = lstENTOrder[cnt].CGSTAmount,
                            CustomerID = lstENTOrder[cnt].CustomerID,
                            DeliveryCharge = lstENTOrder[cnt].DeliveryCharge,
                            DeliveryType = lstENTOrder[cnt].DeliveryType,
                            DeliveryTypeName = lstENTOrder[cnt].DeliveryTypeName,
                            Discount = lstENTOrder[cnt].Discount,
                            DiscountID = lstENTOrder[cnt].DiscountID,
                            DiscountPer = lstENTOrder[cnt].DiscountPer,
                            DiscountRemark = lstENTOrder[cnt].DiscountRemark,
                            DiscountType = lstENTOrder[cnt].DiscountType,
                            EmployeeID = lstENTOrder[cnt].EmployeeID,
                            EndTime = lstENTOrder[cnt].EndTime,
                            ExtraCharge = lstENTOrder[cnt].ExtraCharge,
                            HeaderStatus = lstENTOrder[cnt].HeaderStatus,
                            IsPrint = lstENTOrder[cnt].IsPrint,
                            IsSendToKitchen = lstENTOrder[cnt].IsSendToKitchen,
                            MobileNo = lstENTOrder[cnt].MobileNo,
                            Mode = (cnt + 1).ToString(),
                            Name = lstENTOrder[cnt].Name,
                            OrderActions = lstENTOrder[cnt].OrderActions,
                            OrderActionsName = lstENTOrder[cnt].OrderActionsName,
                            OrderDate = lstENTOrder[cnt].OrderDate,
                            OrderDateFrom = lstENTOrder[cnt].OrderDateFrom,
                            OrderDateTo = lstENTOrder[cnt].OrderDateTo,
                            OrderID = lstENTOrder[cnt].OrderID,
                            OrderNo = lstENTOrder[cnt].OrderNo,
                            OrderSpecialRequest = lstENTOrder[cnt].OrderSpecialRequest,
                            OrderStatus = lstENTOrder[cnt].OrderStatus,
                            PayableAmount = lstENTOrder[cnt].PayableAmount,
                            PaymentMethod = lstENTOrder[cnt].PaymentMethod,
                            PaymentMethodText = lstENTOrder[cnt].PaymentMethodText,
                            ProductID = lstENTOrder[cnt].ProductID,
                            RUserID = lstENTOrder[cnt].RUserID,
                            RUserType = lstENTOrder[cnt].RUserType,
                            SearchKey = lstENTOrder[cnt].SearchKey,
                            SGSTAmount = lstENTOrder[cnt].SGSTAmount,
                            StartTime = lstENTOrder[cnt].StartTime,
                            SubTotal = lstENTOrder[cnt].SubTotal,
                            TableID = lstENTOrder[cnt].TableID,
                            TableName = lstENTOrder[cnt].TableName,
                            TableStatusID = lstENTOrder[cnt].TableStatusID,
                            TaxLabel1 = lstENTOrder[cnt].TaxLabel1,
                            TaxLabel2 = lstENTOrder[cnt].TaxLabel2,
                            TaxPercent1 = lstENTOrder[cnt].TaxPercent1,
                            TaxPercent2 = lstENTOrder[cnt].TaxPercent2,
                            TipGratuity = lstENTOrder[cnt].TipGratuity,
                            TokenNo = lstENTOrder[cnt].TokenNo,
                            TotalTax = lstENTOrder[cnt].TotalTax
                        });
                        #endregion

                        cnt++;
                        if (lstENTOrder.Count == cnt) { break; }
                    }

                    PrintDialog pd = new PrintDialog();
                    pdoc = new PrintDocument();
                    PrinterSettings ps = new PrinterSettings();
                    Font font = new Font("Verdana", 15);
                    PaperSize psize = new PaperSize("Custom", 100, 820);
                    pd.Document = pdoc;
                    pd.Document.DefaultPageSettings.PaperSize = psize;
                    pdoc.DefaultPageSettings.PaperSize.Height = 820;
                    pdoc.DefaultPageSettings.PaperSize.Width = 120;
                    pdoc.PrinterSettings.PrinterName = GetDefaultPrinterForYourStation();  //"EPSON TM-m30 Receipt5";
                    pdoc.PrintPage += new PrintPageEventHandler(pdoc_DateWiseSalesInvoiceReport);
                    pdoc.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void pdoc_DateWiseSalesInvoiceReport(object sender, PrintPageEventArgs e)
        {
            try
            {
                Graphics graphics = e.Graphics;
                String underLine = "-------------------------------------------";
                SolidBrush brush = new SolidBrush(Color.Black);
                Font fontLine = new Font("Verdana", 10);
                Font fontCommon = new Font("Verdana", 8);
                Font fontDetail = new Font("Verdana", 7);
                Font fontSmallHeding = new Font("Verdana", 9, FontStyle.Bold);
                Font fontDetailHead = new Font("Verdana", 7, FontStyle.Bold);
                Font fontHeading = new Font("Verdana", 14, FontStyle.Bold);
                StringFormat formatRightToLeft = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                RectangleF rect;

                int startX = 5;
                int startY = 5;
                int Offset = 10;

                if (lstENTPrint.Count > 0)
                {
                    if (IsFirstPage)
                    {
                        GetBranchMasterSetting();
                        rect = new RectangleF(startX, startY, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, BranchName, fontHeading, rect, Color.Black);
                        Offset = Offset + 25;

                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss tt"), fontCommon, rect, Color.Black);
                        Offset = Offset + 10;

                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;

                        rect = new RectangleF(startX, startY + Offset, 270.0F, 20.0F);
                        graphics = DrawStringCenter(graphics, "Bill Wise Sales Report", fontSmallHeding, rect, Color.Black);
                        Offset = Offset + 15;

                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;

                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("From Date : " + FromDate + " To :" + ToDate, fontCommon, brush, rect);
                        Offset = Offset + 15;
                    }

                    if (IsFirstPage)
                    { graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset); }
                    else
                    { graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY); }
                    Offset = Offset + 15;

                    rect = new RectangleF(startX, startY + Offset, 30.0F, 15.0F);
                    graphics = DrawStringLeft(graphics, "Bill", fontDetailHead, rect, Color.Black);

                    startX = startX + 30;
                    rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "Tbl Wt", fontDetailHead, rect, Color.Black);

                    startX = startX + 50;
                    rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "Each", fontDetailHead, rect, Color.Black);

                    startX = startX + 50;
                    rect = new RectangleF(startX, startY + Offset, 40.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "Disc.", fontDetailHead, rect, Color.Black);

                    startX = startX + 40;
                    rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "Total", fontDetailHead, rect, Color.Black);

                    startX = startX + 50;
                    rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "Remark", fontDetailHead, rect, Color.Black);

                    Offset = Offset + 15;
                    startX = 5;
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;
                    
                    for (int i = 0; i < lstENTPrint.Count; i++)
                    {
                        startX = 5;

                        if (i == 0)
                        {
                            rect = new RectangleF(startX, startY + Offset, 270.0F, 20.0F);
                            graphics = DrawStringLeft(graphics, lstENTPrint[i].OrderDate.Substring(0, 10), fontDetailHead, rect, Color.Black);
                            Offset = Offset + 20;
                        }
                        else
                        {
                            if (lstENTPrint[i].OrderDate.Substring(0, 10) != lstENTPrint[i - 1].OrderDate.Substring(0, 10))
                            {
                                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                                Offset = Offset + 10;

                                rect = new RectangleF(startX, startY + Offset, 270.0F, 15.0F);
                                graphics = DrawStringLeft(graphics, "Date Wise Each Total : ", fontDetailHead, rect, Color.Black);
                                graphics = DrawStringRight(graphics, SRFoodTotal.ToString(), fontDetailHead, rect, Color.Black);
                                Offset = Offset + 15;

                                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                                Offset = Offset + 10;

                                SRFoodTotal = 0;
                            }
                            if (lstENTPrint[i].OrderDate.Substring(0, 10) != lstENTPrint[i - 1].OrderDate.Substring(0, 10))
                            {
                                rect = new RectangleF(startX, startY + Offset, 270.0F, 20.0F);
                                graphics = DrawStringLeft(graphics, lstENTPrint[i].OrderDate.Substring(0, 10), fontDetailHead, rect, Color.Black);
                                Offset = Offset + 20;
                            }
                        }

                        rect = new RectangleF(startX, startY + Offset, 30.0F, 15.0F);
                        graphics = DrawStringLeft(graphics, lstENTPrint[i].Mode, fontDetail, rect, Color.Black);

                        startX = startX + 30;
                        rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                        graphics = DrawStringLeft(graphics, lstENTPrint[i].TableName, fontDetail, rect, Color.Black);

                        startX = startX + 50;
                        rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                        graphics = DrawStringRight(graphics, lstENTPrint[i].SubTotal.ToString(), fontDetail, rect, Color.Black);

                        startX = startX + 50;
                        rect = new RectangleF(startX, startY + Offset, 40.0F, 15.0F);
                        graphics = DrawStringRight(graphics, lstENTPrint[i].Discount.ToString(), fontDetail, rect, Color.Black);

                        startX = startX + 40;
                        rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                        graphics = DrawStringRight(graphics, lstENTPrint[i].PayableAmount.ToString(), fontDetail, rect, Color.Black);

                        startX = startX + 50;
                        rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                        graphics = DrawStringRight(graphics, lstENTPrint[i].PaymentMethodText, fontDetail, rect, Color.Black);
                        Offset = Offset + 15;

                        SRFoodTotal = SRFoodTotal + lstENTPrint[i].SubTotal;
                        SRSubTotal = SRSubTotal + lstENTPrint[i].SubTotal;
                        SRTotalDisc = SRTotalDisc + lstENTPrint[i].Discount;
                        SRTotalSGSTAmount = SRTotalSGSTAmount + lstENTPrint[i].SGSTAmount;
                        SRTotalCGSTAmount = SRTotalCGSTAmount + lstENTPrint[i].CGSTAmount;
                        SRTipTotal = SRTipTotal + lstENTPrint[i].TipGratuity;
                        SRDeliveryChargeTotal = SRDeliveryChargeTotal + lstENTPrint[i].DeliveryCharge;
                        SRNetAmount = SRNetAmount + lstENTPrint[i].PayableAmount;
                    }

                    startX = 5;

                    if (IsLastPage)
                    {
                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;

                        rect = new RectangleF(startX, startY + Offset, 270.0F, 15.0F);
                        graphics = DrawStringLeft(graphics, "Date Wise Each Total : ", fontDetailHead, rect, Color.Black);
                        graphics = DrawStringRight(graphics, SRFoodTotal.ToString(), fontDetailHead, rect, Color.Black);
                        Offset = Offset + 15;
                        
                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;

                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Each Total Amount : ", fontCommon, brush, rect);
                        graphics.DrawString(SRSubTotal.ToString(), fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;

                        if (SRTotalSGSTAmount <= 0)
                        {
                            rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                            graphics.DrawString("SGST(+) : ", fontCommon, brush, rect);
                            graphics.DrawString(SRTotalSGSTAmount.ToString(), fontCommon, brush, rect, formatRightToLeft);
                            Offset = Offset + 15;
                        }

                        if (SRTotalCGSTAmount <= 0)
                        {
                            rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                            graphics.DrawString("CGST(+) : ", fontCommon, brush, rect);
                            graphics.DrawString(SRTotalCGSTAmount.ToString(), fontCommon, brush, rect, formatRightToLeft);
                            Offset = Offset + 15;
                        }

                        if (SRTipTotal <= 0)
                        {
                            rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                            graphics.DrawString("Tip(+) : ", fontCommon, brush, rect);
                            graphics.DrawString(SRTipTotal.ToString(), fontCommon, brush, rect, formatRightToLeft);
                            Offset = Offset + 15;
                        }

                        if (SRDeliveryChargeTotal <= 0)
                        {
                            rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                            graphics.DrawString("Delivery Charge(+) : ", fontCommon, brush, rect);
                            graphics.DrawString(SRDeliveryChargeTotal.ToString(), fontCommon, brush, rect, formatRightToLeft);
                            Offset = Offset + 15;
                        }

                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Discount Amount(-) : ", fontCommon, brush, rect);
                        graphics.DrawString(SRTotalDisc.ToString(), fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;

                        decimal roundOffMinus = 0;
                        decimal roundOffPlus = 0;
                        roundOffMinus = SRNetAmount - Math.Round(SRNetAmount, 0, MidpointRounding.AwayFromZero);
                        roundOffPlus = SRNetAmount - Math.Round(SRNetAmount, 0, MidpointRounding.AwayFromZero);

                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Rounding Amt.(+) : ", fontCommon, brush, rect);
                        graphics.DrawString(roundOffPlus > 0 ? roundOffPlus.ToString() : "0", fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;

                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Rounding Amt.(-) : ", fontCommon, brush, rect);
                        graphics.DrawString(roundOffMinus < 0 ? roundOffMinus.ToString() : "0", fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;

                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 10;

                        SRNetAmount = roundOffPlus > 0 ? SRNetAmount - roundOffPlus : roundOffMinus < 0 ? SRNetAmount - roundOffMinus : SRNetAmount;

                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Net Amount : ", fontSmallHeding, brush, rect);
                        graphics.DrawString(SRNetAmount.ToString(), fontSmallHeding, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;

                    }

                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString(DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss tt"), fontDetail, brush, rect);
                    graphics.DrawString("Page " + pageno.ToString() + " of " + pagecnt.ToString(), fontDetail, brush, rect, formatRightToLeft);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
        
        #region Common Graphics Function

        public static Graphics DrawStringCenter(Graphics g, string s, Font f, RectangleF r, Color c)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            //stringFormat.LineAlignment = StringAlignment.Center; // Not necessary here
            g.DrawString(s, f, new SolidBrush(c), r, stringFormat);
            return g;
        }

        public static Graphics DrawStringRight(Graphics g, string s, Font f, RectangleF r, Color c)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Far;
            //stringFormat.LineAlignment = StringAlignment.Center; // Not necessary here
            g.DrawString(s, f, new SolidBrush(c), r, stringFormat);
            return g;
        }

        public static Graphics DrawStringLeft(Graphics g, string s, Font f, RectangleF r, Color c)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            //stringFormat.LineAlignment = StringAlignment.Center; // Not necessary here
            g.DrawString(s, f, new SolidBrush(c), r, stringFormat);
            return g;
        }

        #endregion
    }
}
