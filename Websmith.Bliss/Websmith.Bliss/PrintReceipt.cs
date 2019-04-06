using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;
using System.Management;

namespace Websmith.Bliss
{
    public class PrintReceipt
    {
        PrintDocument pdoc = null;
        string OrderID = "";
        string BranchAddress = "";
        string BranchName = "";
        string BranchMobileNo = "";
        string BranchGSTIN = "";
        public static string TillID = "";
        int PrintStatus;
        bool IsDuplicatePrint = false;
        List<ENT.GeneralSetting> lstGeneralSetting;
        List<ENT.ViewKOTRoutingPrint> lstENTPrint;
        List<ENT.OrderBook> lstENTOrder;

        public PrintReceipt() { }

        public PrintReceipt(string strOrderID)
        {
            OrderID = strOrderID;
        }

        public PrintReceipt(string strOrderID, int prtStatus)
        {
            OrderID = strOrderID;
            PrintStatus = prtStatus;
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
                    DefaultPrinter = Properties.Settings.Default.PrinterPath.ToString();  // "EPSON TM-m30 Receipt";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Print", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return DefaultPrinter;
        }

        #region Order Receipt Print
        decimal SRSubTotal = 0;
        decimal SRDisc = 0;
        decimal SRSGSTAmount = 0;
        decimal SRCGSTAmount = 0;
        decimal SRTotalTax = 0;
        decimal SRExtraCharge = 0;
        decimal SRTipTotal = 0;
        decimal SRDeliveryChargeTotal = 0;
        decimal SRNetAmount = 0;
        
        public void PrintOrderReceipt()
        {
            try
            {
                SRSubTotal = 0;
                SRDisc = 0;
                SRSGSTAmount = 0;
                SRCGSTAmount = 0;
                SRTotalTax = 0;
                SRExtraCharge = 0;
                SRTipTotal = 0;
                SRDeliveryChargeTotal = 0;
                SRNetAmount = 0;

                List<ENT.GeneralSetting> lstSetting = GlobalVariable.GetGeneralSetting();
                if (lstSetting.Count > 0)
                {
                    if (!lstSetting[0].DuplicatePrint)
                    {
                        if (PrintStatus == Convert.ToInt32(GlobalVariable.PrintStatus.NotPrinted))
                            IsDuplicatePrint = false;
                        else
                        {
                            MessageBox.Show("Duplicate Print Not Allowed.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        if (PrintStatus > Convert.ToInt32(GlobalVariable.PrintStatus.NotPrinted))
                            IsDuplicatePrint = lstSetting[0].DuplicatePrint;
                        else
                            IsDuplicatePrint = false;
                    }
                }

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
                pdoc.PrintPage += new PrintPageEventHandler(pdoc_OrderReceipt);
                pdoc.Print();

                if(PrintStatus == Convert.ToInt32(GlobalVariable.PrintStatus.NotPrinted))
                    ChangePrintStatus(OrderID,Convert.ToInt32(GlobalVariable.PrintStatus.Printed));
                else if (PrintStatus == Convert.ToInt32(GlobalVariable.PrintStatus.Printed))
                    ChangePrintStatus(OrderID, Convert.ToInt32(GlobalVariable.PrintStatus.DuplicatePrinted));
                else
                    ChangePrintStatus(OrderID, Convert.ToInt32(GlobalVariable.PrintStatus.DuplicatePrinted));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Receipt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        void pdoc_OrderReceipt(object sender, PrintPageEventArgs e)
        {
            try
            {
                Graphics graphics = e.Graphics;
                String underLine = "-------------------------------------------";
                SolidBrush brush = new SolidBrush(Color.Black);
                Font fontLine = new Font("Verdana", 10);
                Font fontCommon = new Font("Verdana", 8);
                Font fontDetail = new Font("Verdana", 7);
                Font fontDetailHead = new Font("Verdana", 7, FontStyle.Bold);
                Font fontHeading = new Font("Verdana", 14, FontStyle.Bold);
                StringFormat formatRightToLeft = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                RectangleF rect;
                
                int startX = 5;
                int startY = 5;
                int Offset = 10;

                if (OrderID == "")
                {
                    return;
                }

                lstGeneralSetting = GlobalVariable.GetGeneralSetting();
                lstENTOrder = new List<ENT.OrderBook>();
                ENT.OrderBook objENTOrder = new ENT.OrderBook();
                DAL.OrderBook objDALOrder = new DAL.OrderBook();
                objENTOrder.Mode = "GetPrintReceiptByOrderID";
                objENTOrder.OrderID = new Guid(OrderID);
                lstENTOrder = objDALOrder.getOrder(objENTOrder);
                if (lstENTOrder.Count > 0)
                {
                    GetBranchMasterSetting();

                    if (!string.IsNullOrEmpty(lstGeneralSetting[0].PrintHeader))
                    {
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 15.0F);
                        graphics = DrawStringCenter(graphics, lstGeneralSetting[0].PrintHeader, fontDetail, rect, Color.Black);
                        Offset = Offset + 15;
                    }

                    if (IsDuplicatePrint)
                    {
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, "Duplicate", new Font("Verdana", 14), rect, Color.Black);
                        Offset = Offset + 25;
                    }

                    rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                    graphics = DrawStringCenter(graphics, BranchName, fontHeading, rect, Color.Black);
                    Offset = Offset + 25;

                    rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                    graphics = DrawStringCenter(graphics, BranchAddress, fontCommon, rect, Color.Black);
                    Offset = Offset + 15;

                    if(BranchMobileNo.Trim() != string.Empty)
                    {
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, "Phone No.: " + BranchMobileNo, fontCommon, rect, Color.Black);
                        Offset = Offset + 15;
                    }
                    
                    if (BranchGSTIN.Trim() != string.Empty)
                    {
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, "GSTIN: " + BranchGSTIN, fontCommon, rect, Color.Black);
                        Offset = Offset + 15;
                    }

                    if (lstGeneralSetting[0].KOTDateTime)
                    {
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, lstENTOrder[0].OrderDate, fontCommon, rect, Color.Black);
                        Offset = Offset + 15;
                    }

                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;

                    if (lstGeneralSetting[0].CustomerNameOnKOT)
                    {
                        graphics.DrawString("Customer Name: " + lstENTOrder[0].Name, fontCommon, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;

                        graphics.DrawString("Address: " + lstENTOrder[0].Address, fontCommon, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;
                    }

                    graphics.DrawString("Mobile No.: " + lstENTOrder[0].MobileNo, fontCommon, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;
                    
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                    graphics = DrawStringCenter(graphics, lstENTOrder[0].OrderNo, fontCommon, rect, Color.Black);
                    Offset = Offset + 10;

                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    if (lstGeneralSetting[0].KOTOrderType)
                    {
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        if (lstENTOrder[0].DeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.DineIn))
                            graphics = DrawStringCenter(graphics, lstENTOrder[0].TableName, fontHeading, rect, Color.Black);
                        else
                            graphics = DrawStringCenter(graphics, lstENTOrder[0].DeliveryTypeName, fontHeading, rect, Color.Black);
                        Offset = Offset + 20;

                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;
                    }

                    rect = new RectangleF(startX, startY + Offset, 30.0F, 15.0F);
                    graphics = DrawStringLeft(graphics, "QTY", fontDetailHead, rect, Color.Black);

                    startX = startX + 25;
                    rect = new RectangleF(startX, startY + Offset, 140.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "ITEM", fontDetailHead, rect, Color.Black);

                    startX = startX + 140;
                    rect = new RectangleF(startX, startY + Offset, 50.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "EACH", fontDetailHead, rect, Color.Black);

                    startX = startX + 50;
                    rect = new RectangleF(startX, startY + Offset, 60.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "TOTAL", fontDetailHead, rect, Color.Black);

                    Offset = Offset + 10;
                    startX = 5;
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    List<ENT.Transaction> lstENTTran = new List<ENT.Transaction>();
                    ENT.Transaction objENTTran = new ENT.Transaction();
                    DAL.Transaction objDALTran = new DAL.Transaction();
                    objENTTran.Mode = "GetRecordByOrderID";
                    objENTTran.OrderID = new Guid(OrderID);
                    lstENTTran = objDALTran.getOrderTransaction(objENTTran);

                    for (int i = 0; i < lstENTTran.Count; i++)
                    {
                        startX = 5;
                        rect = new RectangleF(startX, startY + Offset, 25.0F, 12.0F);
                        graphics = DrawStringLeft(graphics, lstENTTran[i].Quantity.ToString(), fontDetail, rect, Color.Black);

                        startX = startX + 25;
                        rect = new RectangleF(startX, startY + Offset, 140.0F, 12.0F);
                        graphics = DrawStringLeft(graphics, lstENTTran[i].ProductName.ToString(), fontDetail, rect, Color.Black);

                        startX = startX + 140;
                        rect = new RectangleF(startX, startY + Offset, 50.0F, 12.0F);
                        graphics = DrawStringRight(graphics, lstENTTran[i].Rate.ToString(), fontDetail, rect, Color.Black);

                        startX = startX + 50;
                        rect = new RectangleF(startX, startY + Offset, 60.0F, 12.0F);
                        graphics = DrawStringRight(graphics, lstENTTran[i].TotalAmount.ToString(), fontDetail, rect, Color.Black);

                        Offset = Offset + 15;

                        CalcTotal(lstENTTran[i].Quantity, lstENTTran[i].Rate, lstENTOrder[0].DiscountType, lstENTOrder[0].DiscountPer, lstENTOrder[0].Discount, lstENTOrder[0].TaxPercent1, lstENTOrder[0].TaxPercent2, lstENTOrder[0].ExtraCharge, lstENTOrder[0].TipGratuity, lstENTOrder[0].DeliveryCharge);
                    }

                    Offset = Offset - 5;
                    startX = 5;
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString("Sub Total : ", fontCommon, brush, rect);
                    graphics.DrawString(SRSubTotal.ToString(), fontCommon, brush, rect, formatRightToLeft);
                    Offset = Offset + 15;
                    
                    if (SRExtraCharge > 0)
                    {
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Extra Charge (+) : ", fontCommon, brush, rect);
                        graphics.DrawString(SRExtraCharge.ToString(), fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;
                    }

                    if (lstENTOrder[0].DiscountPer > 0)
                    {
                        if (SRDisc > 0)
                        {
                            rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                            graphics.DrawString("Discount (" + lstENTOrder[0].DiscountPer.ToString() + "%) (-) : ", fontCommon, brush, rect);
                            graphics.DrawString(SRDisc.ToString(), fontCommon, brush, rect, formatRightToLeft);
                            Offset = Offset + 15;
                        }
                    }
                    else
                    {
                        if (SRDisc > 0)
                        {
                            rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                            graphics.DrawString("Discount (Rs.) (-) : ", fontCommon, brush, rect);
                            graphics.DrawString(SRDisc.ToString(), fontCommon, brush, rect, formatRightToLeft);
                            Offset = Offset + 15;
                        }
                    }

                    if (SRSGSTAmount > 0)
                    {
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString(lstENTOrder[0].TaxLabel1.ToString() + " (" + lstENTOrder[0].TaxPercent1.ToString() + "%) (+) :  ", fontCommon, brush, rect);
                        graphics.DrawString(SRSGSTAmount.ToString(), fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;
                    }

                    if (SRCGSTAmount > 0)
                    {
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString(lstENTOrder[0].TaxLabel2.ToString() + " (" + lstENTOrder[0].TaxPercent2.ToString() + "%) (+) :  ", fontCommon, brush, rect);
                        graphics.DrawString(SRCGSTAmount.ToString(), fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;
                    }

                    if (SRTipTotal > 0)
                    {
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Tip (+) : ", fontCommon, brush, rect);
                        graphics.DrawString(SRTipTotal.ToString(), fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;
                    }

                    if (SRDeliveryChargeTotal > 0)
                    {
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Delivery Charge (+) : ", fontCommon, brush, rect);
                        graphics.DrawString(SRDeliveryChargeTotal.ToString(), fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;
                    }

                    if (lstGeneralSetting[0].RoundingTotal)
                    {
                        decimal roundOff = Math.Round(SRNetAmount, 0, MidpointRounding.AwayFromZero) - SRNetAmount;

                        rect = new RectangleF(startX, startY + Offset, 275.0F, 15.0F);
                        graphics = DrawStringLeft(graphics, "Round Off : ", fontCommon, rect, Color.Black);
                        graphics = DrawStringRight(graphics, roundOff.ToString(), fontCommon, rect, Color.Black);
                        Offset = Offset + 10;
                        
                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 10;
                        
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics = DrawStringRight(graphics, "Total Payable Amount: " + Math.Round(SRNetAmount, 0, MidpointRounding.AwayFromZero).ToString(), new Font("Verdana", 9, FontStyle.Bold), rect, Color.Black);
                        Offset = Offset + 10;
                    }
                    else
                    {
                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 10;

                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics = DrawStringRight(graphics, "Total Payable Amount: " + SRNetAmount.ToString(), new Font("Verdana", 9, FontStyle.Bold), rect, Color.Black);
                        Offset = Offset + 10;
                    }
                    
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;
                    
                    if (lstGeneralSetting[0].KOTServerName)
                    {
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, "Server : " + GlobalVariable.EmployeeName, fontDetail, rect, Color.Black);
                        Offset = Offset + 10;

                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 10;
                    }

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics = DrawStringCenter(graphics, lstGeneralSetting[0].PrintFooter, fontDetail, rect, Color.Black);
                    Offset = Offset + 10;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Print", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcTotal(int qty, decimal rate, int discType, decimal discPer, decimal discAmt, decimal taxSGSTPer, decimal taxCGSTPer, decimal extraCharge, decimal tip, decimal deliveryCharge)
        {
            try
            {
                decimal discTotal = 0;
                SRSubTotal = SRSubTotal + (qty * rate);

                decimal subTotal = Convert.ToDecimal(SRSubTotal) + extraCharge;
                if (discType == 2)
                    discTotal = Math.Round((subTotal * discPer) / 100, 2, MidpointRounding.AwayFromZero);
                else if (discType == 1)
                    discTotal = discAmt;
                else
                    discTotal = 0;

                decimal SubTotalDiscount = subTotal - discTotal;

                SRDisc = discTotal;
                SRExtraCharge = extraCharge;
                SRTipTotal = tip;
                SRDeliveryChargeTotal = deliveryCharge;

                SRCGSTAmount = Math.Round((Convert.ToDecimal(SubTotalDiscount) * taxCGSTPer) / 100, 2, MidpointRounding.AwayFromZero);
                SRSGSTAmount = Math.Round((Convert.ToDecimal(SubTotalDiscount) * taxSGSTPer) / 100, 2, MidpointRounding.AwayFromZero);
                SRTotalTax = Math.Round(SRSGSTAmount + SRCGSTAmount, 2, MidpointRounding.AwayFromZero);
                SRNetAmount = (SubTotalDiscount + SRTotalTax + SRTipTotal + SRDeliveryChargeTotal);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Till Print

        public void PrintTillSummary()
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
                pdoc.PrintPage += new PrintPageEventHandler(pdoc_TillSummary);
                pdoc.Print();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Till Summary", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void pdoc_TillSummary(object sender, PrintPageEventArgs e)
        {
            try
            {
                Graphics graphics = e.Graphics;
                String underLine = "-------------------------------------------";
                SolidBrush brush = new SolidBrush(Color.Black);
                Font fontLine = new Font("Verdana", 10);
                Font fontCommon = new Font("Verdana", 8);
                Font fontDetail = new Font("Verdana", 7);
                Font fontDetailHead = new Font("Verdana", 7, FontStyle.Bold);
                Font fontHeading = new Font("Verdana", 14, FontStyle.Bold);
                StringFormat formatRightToLeft = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                RectangleF rect;
                
                int startX = 5;
                int startY = 10;
                int Offset = 10;

                if (TillID == "")
                {
                    return;
                }

                DAL.TillManage objDALTill = new DAL.TillManage();
                ENT.TillManage objENTTill = new ENT.TillManage();
                List<ENT.TillManage> lstENTTill = new List<ENT.TillManage>();
                objENTTill.TillID = new Guid(TillID);
                objENTTill.Mode = "GetByID";
                lstENTTill = objDALTill.getTillManage(objENTTill);
                if (lstENTTill.Count > 0)
                {
                    GetBranchMasterSetting();
                    rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                    graphics = DrawStringCenter(graphics, BranchName, fontHeading, rect, Color.Black);
                    Offset = Offset + 25;

                    rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                    graphics = DrawStringCenter(graphics, BranchAddress, fontCommon, rect, Color.Black);
                    Offset = Offset + 15;

                    rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                    graphics = DrawStringCenter(graphics, "Till Summary", fontHeading, rect, Color.Black);
                    Offset = Offset + 25;

                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString("Start Date & Time : "+ Convert.ToString(lstENTTill[0].StartDateTime), fontCommon, brush, rect);
                    //graphics.DrawString(Convert.ToString(lstENTTill[0].StartDateTime), fontCommon, brush, rect);
                    Offset = Offset + 15;

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString("End Date & Time : "+ Convert.ToString(lstENTTill[0].EndDateTime), fontCommon, brush, rect);
                    //graphics.DrawString(Convert.ToString(lstENTTill[0].EndDateTime), fontCommon, brush, rect, formatRightToLeft);
                    Offset = Offset + 10;

                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString("Start Cash : ", fontCommon, brush, rect);
                    graphics.DrawString(lstENTTill[0].StartCash.ToString(), fontCommon, brush, rect, formatRightToLeft);
                    Offset = Offset + 15;

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString("Pay In : ", fontCommon, brush, rect);
                    graphics.DrawString(lstENTTill[0].PayIn.ToString(), fontCommon, brush, rect, formatRightToLeft);
                    Offset = Offset + 15;

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString("Pay Out : ", fontCommon, brush, rect);
                    graphics.DrawString(lstENTTill[0].PayOut.ToString(), fontCommon, brush, rect, formatRightToLeft);
                    Offset = Offset + 15;

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString("Cash : ", fontCommon, brush, rect);
                    graphics.DrawString(lstENTTill[0].Cash.ToString(), fontCommon, brush, rect, formatRightToLeft);
                    Offset = Offset + 15;

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString("Expected Cash In Till : ", fontCommon, brush, rect);
                    graphics.DrawString(lstENTTill[0].ExpectedCash.ToString(), fontCommon, brush, rect, formatRightToLeft);
                    Offset = Offset + 15;

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString("Ending Cash Total : ", fontCommon, brush, rect);
                    graphics.DrawString(lstENTTill[0].EndCash.ToString(), fontCommon, brush, rect, formatRightToLeft);
                    Offset = Offset + 10;

                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString("Difference : ", fontCommon, brush, rect);
                    graphics.DrawString(lstENTTill[0].Difference.ToString(), fontCommon, brush, rect, formatRightToLeft);
                    Offset = Offset + 10;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Till Summary", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Send TO KDS

        public void PrintSendToKDS()
        {
            try
            {
                if (string.IsNullOrEmpty(OrderID))
                {
                    return;
                }

                ENT.ViewKOTRoutingPrint objENT = new ENT.ViewKOTRoutingPrint();
                objENT.OrderID = new Guid(OrderID);
                objENT.OrderActions = Convert.ToInt32(GlobalVariable.OrderActions.Pay);
                objENT.OrderStatus = Convert.ToInt32(GlobalVariable.OrderStatus.OPEN);
                objENT.Mode = "GetPrinterGroupBy";
                List<ENT.ViewKOTRoutingPrint> lstENT = DAL.ViewKOTRoutingPrint.GetKOTRoutingPrint(objENT);

                if (lstENT.Count == 0)
                {
                    MessageBox.Show("Maximum KOT count is reached. Please change KOT count in genearal setting.", "Send To KDS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                for (int i = 0; i < lstENT.Count; i++)
                {
                    ENT.ViewKOTRoutingPrint objENTPrint = new ENT.ViewKOTRoutingPrint();
                    objENTPrint.OrderID = lstENT[i].OrderID;
                    objENTPrint.EmployeeID = lstENT[i].EmployeeID;
                    objENTPrint.DeviceID = lstENT[i].DeviceID;
                    objENTPrint.PrinterID = lstENT[i].PrinterID;
                    objENTPrint.OrderActions = Convert.ToInt32(GlobalVariable.OrderActions.Pay);
                    objENTPrint.OrderStatus = Convert.ToInt32(GlobalVariable.OrderStatus.OPEN);
                    objENTPrint.Mode = "GetByOrderIDAndPrinterID";
                    lstENTPrint = DAL.ViewKOTRoutingPrint.GetKOTRoutingPrint(objENTPrint);

                    if (!CheckPrinterStatus(lstENTPrint[i].PrinterIP))
                    {
                        MessageBox.Show("Your Plug-N-Play printer is offline OR not connected.", "Send To KDS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    PrintDialog pd = new PrintDialog();
                    pdoc = new PrintDocument();
                    PrinterSettings ps = new PrinterSettings();
                    Font font = new Font("Verdana", 15);
                    PaperSize psize = new PaperSize("Custom", 100, 200);
                    pd.Document = pdoc;
                    pd.Document.DefaultPageSettings.PaperSize = psize;
                    pdoc.DefaultPageSettings.PaperSize.Height = 820;
                    pdoc.DefaultPageSettings.PaperSize.Width = 520;
                    pdoc.PrinterSettings.PrinterName = lstENTPrint[i].PrinterIP;
                    pdoc.PrintPage += new PrintPageEventHandler(pdoc_SendToKDS);
                    pdoc.Print();

                    new DAL.OrderBook().UpdateKOTPrintStatus(lstENTOrder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Send To KDS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void pdoc_SendToKDS(object sender, PrintPageEventArgs e)
        {
            try
            {
                lstENTOrder = new List<Entity.OrderBook>();
                lstGeneralSetting = GlobalVariable.GetGeneralSetting();
                Graphics graphics = e.Graphics;
                String underLine = "-------------------------------------------";
                SolidBrush brush = new SolidBrush(Color.Black);
                Font fontHeading = new Font("Verdana", 14, FontStyle.Bold);
                Font fontLine = new Font("Verdana", 10);
                Font fontDetailHead = new Font("Verdana", 7, FontStyle.Bold);
                Font fontDetail = new Font("Verdana", 7);
                Font fontCommon = new Font("Verdana", 8);

                StringFormat formatRightToLeft = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                RectangleF rect;

                int startX = 5;
                int startY = 10;
                int Offset = 10;


                if (lstENTPrint.Count > 0)
                {
                    if (lstENTPrint[0].IsSendToKitchen == Convert.ToInt32(GlobalVariable.SendToKDSPrintStatus.NotPrinted))
                    {
                        // This part is for new or first print of send to kds
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, "------New Order------", new Font("Verdana", 14), rect, Color.Black);
                        Offset = Offset + 25;
                    }
                    else if (lstENTPrint[0].IsSendToKitchen == Convert.ToInt32(GlobalVariable.SendToKDSPrintStatus.Printed))
                    {
                        // This part is for all next print of send to kds
                        string strHeader = "";
                        if (lstENTPrint[0].HeaderStatus == Convert.ToInt32(GlobalVariable.KOTPrintHeaderStatus.NewOrder))
                        {
                            strHeader = "------New Order------";
                        }
                        else if (lstENTPrint[0].HeaderStatus == Convert.ToInt32(GlobalVariable.KOTPrintHeaderStatus.RunningOrder))
                        {
                            strHeader = "------Running Order------";
                        }
                        else if (lstENTPrint[0].HeaderStatus == Convert.ToInt32(GlobalVariable.KOTPrintHeaderStatus.ResendItem))
                        {
                            strHeader = "------Resend Item------";
                        }
                        else if (lstENTPrint[0].HeaderStatus == Convert.ToInt32(GlobalVariable.KOTPrintHeaderStatus.CancelItem))
                        {
                            strHeader = "------Cancel Item------";
                        }
                        else if (lstENTPrint[0].HeaderStatus == Convert.ToInt32(GlobalVariable.KOTPrintHeaderStatus.CancelOrder))
                        {
                            strHeader = "------Cancel Order------";
                        }
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, strHeader, new Font("Verdana", 14), rect, Color.Black);
                        Offset = Offset + 25;
                    }

                    int KOTCount = lstENTPrint[0].KOTCount + 1;

                    rect = new RectangleF(startX, startY + Offset, 270.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, lstENTPrint[0].EmpCode + "/" + lstENTPrint[0].TokenNo + "/" + KOTCount.ToString(), fontCommon, rect, Color.Black);
                    Offset = Offset + 15;

                    // KOT DateTime Print if condition is true
                    if (lstGeneralSetting[0].KOTDateTime)
                    {
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 15.0F);
                        graphics = DrawStringCenter(graphics, DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"), fontCommon, rect, Color.Black);
                        Offset = Offset + 15;
                    }

                    // Customer Name Print On KOT if condition is true
                    if (lstGeneralSetting[0].CustomerNameOnKOT)
                    {
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 15.0F);
                        graphics = DrawStringCenter(graphics, lstENTPrint[0].Name, fontCommon, rect, Color.Black);
                        Offset = Offset + 15;
                    }

                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;

                    // KOT Order Type Print if condition is true
                    if (lstGeneralSetting[0].KOTOrderType)
                    {
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        if (lstENTPrint[0].DeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.DineIn))
                            graphics = DrawStringCenter(graphics, lstENTPrint[0].TableName, fontHeading, rect, Color.Black);
                        else
                            graphics = DrawStringCenter(graphics, lstENTPrint[0].DeliveryTypeName, fontHeading, rect, Color.Black);
                        Offset = Offset + 15;

                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;
                    }

                    rect = new RectangleF(startX, startY + Offset, 60.0F, 15.0F);
                    graphics = DrawStringLeft(graphics, "COURSE", fontDetailHead, rect, Color.Black);

                    startX = startX + 60;
                    rect = new RectangleF(startX, startY + Offset, 30.0F, 15.0F);
                    graphics = DrawStringLeft(graphics, "QTY", fontDetailHead, rect, Color.Black);

                    startX = startX + 30;
                    rect = new RectangleF(startX, startY + Offset, 180.0F, 15.0F);
                    graphics = DrawStringCenter(graphics, "ITEM NAME", fontDetailHead, rect, Color.Black);
                    Offset = Offset + 15;

                    startX = 5;
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;

                    for (int i = 0; i < lstENTPrint.Count; i++)
                    {
                        startX = 5;
                        rect = new RectangleF(startX, startY + Offset, 60.0F, 12.0F);
                        graphics = DrawStringLeft(graphics, string.Empty, fontDetail, rect, Color.Black);

                        startX = startX + 60;
                        rect = new RectangleF(startX, startY + Offset, 30.0F, 12.0F);
                        graphics = DrawStringLeft(graphics, lstENTPrint[i].Quantity.ToString(), fontDetail, rect, Color.Black);

                        startX = startX + 30;
                        rect = new RectangleF(startX, startY + Offset, 180.0F, 12.0F);
                        graphics = DrawStringLeft(graphics, lstENTPrint[i].ProductName.ToString(), fontDetail, rect, Color.Black);

                        Offset = Offset + 15;

                        lstENTOrder.Add(new ENT.OrderBook()
                        {
                            OrderID = lstENTPrint[i].OrderID,
                            ProductID = lstENTPrint[i].ProductID,
                            CategoryID = lstENTPrint[i].CategoryID,
                            IsSendToKitchen = Convert.ToInt32(GlobalVariable.SendToKDSPrintStatus.Printed),
                            HeaderStatus = Convert.ToInt32(GlobalVariable.KOTPrintHeaderStatus.RunningOrder),
                            Mode = "UPDATE_KDS_PRINT_STATUS"
                        });
                    }
                    Offset = Offset - 5;

                    startX = 5;
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    if (lstGeneralSetting[0].KOTServerName)
                    {
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Server Name: ", fontCommon, brush, rect);
                        graphics.DrawString(lstENTPrint[0].EmpName.ToString(), fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 10;

                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 10;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Send To KDS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Common Drawing Function

        public static Graphics DrawStringCenter(Graphics g, string s, Font f,RectangleF r, Color c)
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
                    BranchGSTIN= lstENTBMS[0].TinGSTNo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public static bool ChangePrintStatus(string ordID, int PrintStatus)
        {
            bool result = false;
            try
            {
                ENT.OrderBook objEntOrder = new ENT.OrderBook();
                DAL.OrderBook objDalOrder = new DAL.OrderBook();
                objEntOrder.OrderID = new Guid(ordID);
                objEntOrder.IsPrint = PrintStatus;
                objEntOrder.Mode = "UPDATE_PRINT_STATUS";
                result = objDalOrder.InsertUpdateDeleteOrder(objEntOrder);
            }
            catch (Exception)
            {
                result = false;
                MessageBox.Show("Problem In Printer Status Update.");
            }
            return result;
        }
        
        public bool CheckPrinterStatus(string _PrinterName)
        {
            bool Result = false;
            try
            {
                // Set management scope
                ManagementScope scope = new ManagementScope(@"\root\cimv2");
                scope.Connect();

                // Select Printers from WMI Object Collections
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

                string printerName = "";
                foreach (ManagementObject printer in searcher.Get())
                {
                    printerName = printer["Name"].ToString().ToLower();
                    string myString = printer["PrinterStatus"].ToString();
                    Int32 dStatus = Convert.ToInt32(myString);
                    if (printerName.Equals(_PrinterName.ToLower()))   //@"hp deskjet 930c"
                    {
                        //Console.WriteLine("Printer = " + printer["Name"]);
                        if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
                        {
                            // printer is offline by user
                            Result = false;
                            //Console.WriteLine("Your Plug-N-Play printer is not connected.");
                        }
                        else
                        {
                            // printer is not offline
                            Result = true;
                            //Console.WriteLine("Your Plug-N-Play printer is connected.");
                        }
                    }
                }
            }
            catch (Exception)
            {
                Result = false;
            }
            return Result;
        }
    }
}
