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
    /// <summary>
    /// this class created for print any types of receipt
    /// </summary>
    public class PrintReceipt
    {
        #region All Variable

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

        #endregion

        public PrintReceipt() { }

        /// <summary>
        /// constructor will set passed order id to local variable of order id
        /// </summary>
        /// <param name="strOrderID"></param>
        public PrintReceipt(string strOrderID)
        {
            OrderID = strOrderID;
        }

        /// <summary>
        /// constructor will set passed order id and print status to local variable of order id and print status
        /// </summary>
        /// <param name="strOrderID"></param>
        /// <param name="prtStatus"></param>
        public PrintReceipt(string strOrderID, int prtStatus)
        {
            OrderID = strOrderID;
            PrintStatus = prtStatus;
        }

        /// <summary>
        /// Get top one printer for print and return printer name.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// call this function when order payment done.
        /// it will also check this print is duplicate or not.
        /// if duplicate print aloowed from general setting then print otherwise give the message "Duplicate Print Not Allowed"
        /// after print success it change the order print status to duplicate.
        /// </summary>
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
                        // this is check first time print
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
                        // this is check every time print
                        if (PrintStatus > Convert.ToInt32(GlobalVariable.PrintStatus.NotPrinted))
                            IsDuplicatePrint = lstSetting[0].DuplicatePrint;
                        else
                            IsDuplicatePrint = false;
                    }
                }

                // create object of PrintDialog, PrintDocument, PrinterSettings, PaperSize, Font
                // then set it all to default value for print size.
                PrintDialog pd = new PrintDialog();
                pdoc = new PrintDocument();
                PrinterSettings ps = new PrinterSettings();
                Font font = new Font("Verdana", 15);
                PaperSize psize = new PaperSize("Custom", 100, 200);
                pd.Document = pdoc;
                pd.Document.DefaultPageSettings.PaperSize = psize;
                pdoc.DefaultPageSettings.PaperSize.Height = 820;
                pdoc.DefaultPageSettings.PaperSize.Width = 520;
                pdoc.PrinterSettings.PrinterName = GetDefaultPrinterForYourStation();  //this function return default printer
                pdoc.PrintPage += new PrintPageEventHandler(pdoc_OrderReceipt);  // dynamic event for print receipt
                pdoc.Print();

                // this code is used to change status of print like is first time or duplicate.
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

        /// <summary>
        /// this is a pdoc_OrderReceipt event which is a created from PrintOrderReceipt function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                
                int startX = 5; // this is start printing after 5px from left (startX means Horizontal)
                int startY = 5; // this is start printing after 5px from top (startY means Vertical)
                int Offset = 10; // this will add offset value for new line to startY (startY+Offset)

                if (OrderID == "")
                {
                    return;
                }

                // get GeneralSetting setting from database
                lstGeneralSetting = GlobalVariable.GetGeneralSetting();
                lstENTOrder = new List<ENT.OrderBook>();
                ENT.OrderBook objENTOrder = new ENT.OrderBook();
                DAL.OrderBook objDALOrder = new DAL.OrderBook();
                objENTOrder.Mode = "GetPrintReceiptByOrderID";
                objENTOrder.OrderID = new Guid(OrderID);
                lstENTOrder = objDALOrder.getOrder(objENTOrder);

                // if order item found then below code is execute
                if (lstENTOrder.Count > 0)
                {
                    GetBranchMasterSetting();

                    // Slogan print on header
                    if (!string.IsNullOrEmpty(lstGeneralSetting[0].PrintHeader))
                    {
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 15.0F);
                        graphics = DrawStringCenter(graphics, lstGeneralSetting[0].PrintHeader, fontDetail, rect, Color.Black);
                        Offset = Offset + 15;
                    }

                    // if this is a duplicate print then print on KOT "Duplicate"
                    if (IsDuplicatePrint)
                    {
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, "Duplicate", new Font("Verdana", 14), rect, Color.Black);
                        Offset = Offset + 25;
                    }

                    // this code will print branch name on KOT
                    rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                    graphics = DrawStringCenter(graphics, BranchName, fontHeading, rect, Color.Black);
                    Offset = Offset + 25;

                    // this code will print branch address on KOT
                    rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                    graphics = DrawStringCenter(graphics, BranchAddress, fontCommon, rect, Color.Black);
                    Offset = Offset + 15;

                    // this code will print branch mobile number on KOT if not empty
                    if (BranchMobileNo.Trim() != string.Empty)
                    {
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, "Phone No.: " + BranchMobileNo, fontCommon, rect, Color.Black);
                        Offset = Offset + 15;
                    }

                    // this code will print branch gst number on KOT if not empty
                    if (BranchGSTIN.Trim() != string.Empty)
                    {
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, "GSTIN: " + BranchGSTIN, fontCommon, rect, Color.Black);
                        Offset = Offset + 15;
                    }

                    // this code will print order date time on KOT if KOTDateTime is true
                    if (lstGeneralSetting[0].KOTDateTime)
                    {
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, lstENTOrder[0].OrderDate, fontCommon, rect, Color.Black);
                        Offset = Offset + 15;
                    }

                    // it will draw horizontal line
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;

                    // this code will print cutomer name and address on KOT if CustomerNameOnKOT is true
                    if (lstGeneralSetting[0].CustomerNameOnKOT)
                    {
                        graphics.DrawString("Customer Name: " + lstENTOrder[0].Name, fontCommon, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;

                        graphics.DrawString("Address: " + lstENTOrder[0].Address, fontCommon, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;
                    }

                    // it will print mobile no on KOT
                    graphics.DrawString("Mobile No.: " + lstENTOrder[0].MobileNo, fontCommon, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;

                    // it will draw horizontal line
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    // it will print order no on KOT
                    rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                    graphics = DrawStringCenter(graphics, lstENTOrder[0].OrderNo, fontCommon, rect, Color.Black);
                    Offset = Offset + 10;

                    // it will draw horizontal line
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    // this code will print Order type on KOT if KOTOrderType is true
                    if (lstGeneralSetting[0].KOTOrderType)
                    {
                        rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                        if (lstENTOrder[0].DeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.DineIn))  // if order type is dine in then print table name
                            graphics = DrawStringCenter(graphics, lstENTOrder[0].TableName, fontHeading, rect, Color.Black);
                        else
                            graphics = DrawStringCenter(graphics, lstENTOrder[0].DeliveryTypeName, fontHeading, rect, Color.Black); // if order type is not dine in then only print delivery type
                        Offset = Offset + 20;

                        // it will draw horizontal line
                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;
                    }

                    // print label of qty, item, etc.
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

                    // it will draw horizontal line
                    Offset = Offset + 10;
                    startX = 5;
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    // this code will get order items using order id from order transaction table
                    List<ENT.Transaction> lstENTTran = new List<ENT.Transaction>();
                    ENT.Transaction objENTTran = new ENT.Transaction();
                    DAL.Transaction objDALTran = new DAL.Transaction();
                    objENTTran.Mode = "GetRecordByOrderID";
                    objENTTran.OrderID = new Guid(OrderID);
                    lstENTTran = objDALTran.getOrderTransaction(objENTTran);

                    for (int i = 0; i < lstENTTran.Count; i++)
                    {
                        // this will print order item quantiy
                        startX = 5;
                        rect = new RectangleF(startX, startY + Offset, 25.0F, 12.0F);
                        graphics = DrawStringLeft(graphics, lstENTTran[i].Quantity.ToString(), fontDetail, rect, Color.Black);

                        // this will print order item name
                        startX = startX + 25;
                        rect = new RectangleF(startX, startY + Offset, 140.0F, 12.0F);
                        graphics = DrawStringLeft(graphics, lstENTTran[i].ProductName.ToString(), fontDetail, rect, Color.Black);

                        // this will print order item rate
                        startX = startX + 140;
                        rect = new RectangleF(startX, startY + Offset, 50.0F, 12.0F);
                        graphics = DrawStringRight(graphics, lstENTTran[i].Rate.ToString(), fontDetail, rect, Color.Black);

                        // this will print order item total (qty*rate)
                        startX = startX + 50;
                        rect = new RectangleF(startX, startY + Offset, 60.0F, 12.0F);
                        graphics = DrawStringRight(graphics, lstENTTran[i].TotalAmount.ToString(), fontDetail, rect, Color.Black);

                        Offset = Offset + 15;

                        // calculate final total of order amount
                        CalcTotal(lstENTTran[i].Quantity, lstENTTran[i].Rate, lstENTOrder[0].DiscountType, lstENTOrder[0].DiscountPer, lstENTOrder[0].Discount, lstENTOrder[0].TaxPercent1, lstENTOrder[0].TaxPercent2, lstENTOrder[0].ExtraCharge, lstENTOrder[0].TipGratuity, lstENTOrder[0].DeliveryCharge);
                    }

                    // it will draw horizontal line
                    Offset = Offset - 5;
                    startX = 5;
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;

                    // this will print order sub total
                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString("Sub Total : ", fontCommon, brush, rect);
                    graphics.DrawString(SRSubTotal.ToString(), fontCommon, brush, rect, formatRightToLeft);
                    Offset = Offset + 15;

                    // this will print order extra charge if greater than 0
                    if (SRExtraCharge > 0)
                    {
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Extra Charge (+) : ", fontCommon, brush, rect);
                        graphics.DrawString(SRExtraCharge.ToString(), fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;
                    }

                    // this will print Discount Percentage if greater than 0
                    if (lstENTOrder[0].DiscountPer > 0)
                    {
                        if (SRDisc > 0)
                        {
                            // this will print Discount amount
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
                            // this will print Discount amount
                            rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                            graphics.DrawString("Discount (Rs.) (-) : ", fontCommon, brush, rect);
                            graphics.DrawString(SRDisc.ToString(), fontCommon, brush, rect, formatRightToLeft);
                            Offset = Offset + 15;
                        }
                    }

                    // this will print tax if greater than 0
                    if (SRSGSTAmount > 0)
                    {
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString(lstENTOrder[0].TaxLabel1.ToString() + " (" + lstENTOrder[0].TaxPercent1.ToString() + "%) (+) :  ", fontCommon, brush, rect);
                        graphics.DrawString(SRSGSTAmount.ToString(), fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;
                    }

                    // this will print tax if greater than 0
                    if (SRCGSTAmount > 0)
                    {
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString(lstENTOrder[0].TaxLabel2.ToString() + " (" + lstENTOrder[0].TaxPercent2.ToString() + "%) (+) :  ", fontCommon, brush, rect);
                        graphics.DrawString(SRCGSTAmount.ToString(), fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;
                    }

                    // this will print tip if greater than 0
                    if (SRTipTotal > 0)
                    {
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Tip (+) : ", fontCommon, brush, rect);
                        graphics.DrawString(SRTipTotal.ToString(), fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;
                    }

                    // this will print Delivery Charge if greater than 0
                    if (SRDeliveryChargeTotal > 0)
                    {
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics.DrawString("Delivery Charge (+) : ", fontCommon, brush, rect);
                        graphics.DrawString(SRDeliveryChargeTotal.ToString(), fontCommon, brush, rect, formatRightToLeft);
                        Offset = Offset + 15;
                    }

                    // this will print RoundingTotal if RoundingTotal=true 
                    if (lstGeneralSetting[0].RoundingTotal)
                    {
                        decimal roundOff = Math.Round(SRNetAmount, 0, MidpointRounding.AwayFromZero) - SRNetAmount;

                        // this will print Round Off amount
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 15.0F);
                        graphics = DrawStringLeft(graphics, "Round Off : ", fontCommon, rect, Color.Black);
                        graphics = DrawStringRight(graphics, roundOff.ToString(), fontCommon, rect, Color.Black);
                        Offset = Offset + 10;

                        // it will draw horizontal line
                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 10;

                        // this will print Total Payable Amount
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics = DrawStringRight(graphics, "Total Payable Amount: " + Math.Round(SRNetAmount, 0, MidpointRounding.AwayFromZero).ToString(), new Font("Verdana", 9, FontStyle.Bold), rect, Color.Black);
                        Offset = Offset + 10;
                    }
                    else
                    {
                        // it will draw horizontal line
                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 10;

                        // this will print Total Payable Amount
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics = DrawStringRight(graphics, "Total Payable Amount: " + SRNetAmount.ToString(), new Font("Verdana", 9, FontStyle.Bold), rect, Color.Black);
                        Offset = Offset + 10;
                    }

                    // it will draw horizontal line
                    graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;

                    // it will print system name
                    if (lstGeneralSetting[0].KOTServerName)
                    {
                        // it will draw System Name
                        rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                        graphics = DrawStringCenter(graphics, "Server : " + GlobalVariable.EmployeeName, fontDetail, rect, Color.Black);
                        Offset = Offset + 10;

                        // it will draw horizontal line
                        graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 10;
                    }

                    // it will print footer slogan
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

        /// <summary>
        ///  calculate order total on KOT
        ///  all the param will pass related to calulation
        ///  this function call from pdoc_OrderReceipt event
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="rate"></param>
        /// <param name="discType"></param>
        /// <param name="discPer"></param>
        /// <param name="discAmt"></param>
        /// <param name="taxSGSTPer"></param>
        /// <param name="taxCGSTPer"></param>
        /// <param name="extraCharge"></param>
        /// <param name="tip"></param>
        /// <param name="deliveryCharge"></param>
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

        /// <summary>
        /// this function is used to print till summary calculation.
        /// this function will call from till form
        /// </summary>
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
                pdoc.PrinterSettings.PrinterName = GetDefaultPrinterForYourStation(); // return default printer
                pdoc.PrintPage += new PrintPageEventHandler(pdoc_TillSummary); // PrintDocument event
                pdoc.Print();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Till Summary", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// this is a pdoc_TillSummary event which is a created from PrintTillSummary function
        /// in this event code is same functionality of printing style like pdoc_OrderReceipt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pdoc_TillSummary(object sender, PrintPageEventArgs e)
        {
            try
            {
                // create all object used in this code
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

                // this code is used to get till summery for print
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
                    Offset = Offset + 15;

                    rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                    graphics.DrawString("End Date & Time : "+ Convert.ToString(lstENTTill[0].EndDateTime), fontCommon, brush, rect);
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

        /// <summary>
        /// this function is used to print on KDS 
        /// </summary>
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

                    // check printer is online or offline means on or off
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

        /// <summary>
        /// this is a pdoc_SendToKDS event which is a created from PrintSendToKDS function
        /// in this event code is same functionality of printing style like pdoc_OrderReceipt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// this function used to print font in center of KOT print
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        /// <param name="f"></param>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Graphics DrawStringCenter(Graphics g, string s, Font f,RectangleF r, Color c)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            g.DrawString(s, f, new SolidBrush(c), r, stringFormat);
            return g;
        }

        /// <summary>
        /// /// this function used to print font in right side of KOT print
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        /// <param name="f"></param>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Graphics DrawStringRight(Graphics g, string s, Font f, RectangleF r, Color c)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Far;
            g.DrawString(s, f, new SolidBrush(c), r, stringFormat);
            return g;
        }

        /// <summary>
        /// /// this function used to print font in left side of KOT print
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        /// <param name="f"></param>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Graphics DrawStringLeft(Graphics g, string s, Font f, RectangleF r, Color c)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            g.DrawString(s, f, new SolidBrush(c), r, stringFormat);
            return g;
        }

        #endregion
        
        /// <summary>
        /// this function is used to get default setting for print on KOT
        /// like branch name, address, mobile no, gstno, etc.
        /// </summary>
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
        
        /// <summary>
        /// this function will help us to chnage print status of order.
        /// </summary>
        /// <param name="ordID"></param>
        /// <param name="PrintStatus"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// this function will return true or false .
        /// if printer is online then true else false.
        /// </summary>
        /// <param name="_PrinterName"></param>
        /// <returns></returns>
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
