using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.Common
{
    public class PrintReceipt
    {
        PrintDocument pdoc = null;
        string OrderID = "";
        string BranchAddress = "";
        string BranchName = "";

        public PrintReceipt() { }

        public PrintReceipt(string strOrderID) { OrderID = strOrderID; }

        public void print()
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
            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
            pdoc.Print();

            //DialogResult result = pd.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    PrintPreviewDialog pp = new PrintPreviewDialog();
            //    pp.Document = pdoc;
            //    result = pp.ShowDialog();
            //    if (result == DialogResult.OK)
            //    {
            //        pdoc.Print();
            //    }
            //}
        }

        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
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

            if (OrderID == "")
            {
                return;
            }

            List<ENT.OrderBook> lstENTOrder = new List<ENT.OrderBook>();
            ENT.OrderBook objENTOrder = new ENT.OrderBook();
            DAL.OrderBook objDALOrder = new DAL.OrderBook();
            objENTOrder.Mode = "GetPrintReceiptByOrderID";
            objENTOrder.OrderID = new Guid(OrderID);
            lstENTOrder = objDALOrder.getOrder(objENTOrder);
            if (lstENTOrder.Count > 0)
            {
                GetBranchMasterSetting();
                rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                graphics = DrawStringCenter(graphics, BranchName, fontHeading, rect, Color.Black);
                Offset = Offset + 25;

                rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                graphics = DrawStringCenter(graphics, BranchAddress, fontCommon, rect, Color.Black);
                Offset = Offset + 15;

                rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                graphics = DrawStringCenter(graphics, lstENTOrder[0].OrderDate, fontCommon, rect, Color.Black);
                Offset = Offset + 15;

                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                graphics.DrawString("Customer Name: " + lstENTOrder[0].Name, fontCommon, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                graphics.DrawString("Address: " + lstENTOrder[0].Address, fontCommon, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                graphics.DrawString("Mobile No.: " + lstENTOrder[0].MobileNo, fontCommon, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                graphics.DrawString("Server Name: " + GlobalVariable.EmployeeName, fontCommon, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                graphics.DrawString("Order Token: " + GlobalVariable.EmployeeCode + "/" + lstENTOrder[0].TokenNo.ToString(), new Font("Verdana", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;

                rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                graphics = DrawStringCenter(graphics, lstENTOrder[0].OrderNo, fontCommon, rect, Color.Black);
                Offset = Offset + 10;

                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;

                rect = new RectangleF(startX, startY + Offset, 270.0F, 25.0F);
                if (lstENTOrder[0].DeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.DineIn))
                    graphics = DrawStringCenter(graphics, lstENTOrder[0].TableName, fontHeading, rect, Color.Black);
                else
                    graphics = DrawStringCenter(graphics, lstENTOrder[0].DeliveryTypeName, fontHeading, rect, Color.Black);
                Offset = Offset + 20;

                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

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
                }
                Offset = Offset - 5;
                startX = 5;
                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                graphics.DrawString("Sub Total : ", fontCommon, brush, rect);
                graphics.DrawString(lstENTOrder[0].SubTotal.ToString(), fontCommon, brush, rect, formatRightToLeft);
                Offset = Offset + 15;

                rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                graphics.DrawString("Discount (" + lstENTOrder[0].DiscountPer.ToString() + "%) : ", fontCommon, brush, rect);
                graphics.DrawString(lstENTOrder[0].Discount.ToString(), fontCommon, brush, rect, formatRightToLeft);
                Offset = Offset + 15;

                rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                graphics.DrawString(lstENTOrder[0].TaxLabel1.ToString() + " (" + lstENTOrder[0].TaxPercent1.ToString() + "%) :  ", fontCommon, brush, rect);
                graphics.DrawString(lstENTOrder[0].SGSTAmount.ToString(), fontCommon, brush, rect, formatRightToLeft);
                Offset = Offset + 15;

                rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                graphics.DrawString(lstENTOrder[0].TaxLabel2.ToString() + " (" + lstENTOrder[0].TaxPercent2.ToString() + "%) :  ", fontCommon, brush, rect);
                graphics.DrawString(lstENTOrder[0].CGSTAmount.ToString(), fontCommon, brush, rect, formatRightToLeft);
                Offset = Offset + 15;

                rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                graphics.DrawString("Tip : ", fontCommon, brush, rect);
                graphics.DrawString(lstENTOrder[0].TipGratuity.ToString(), fontCommon, brush, rect, formatRightToLeft);
                Offset = Offset + 15;

                rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                graphics.DrawString("Extra Charge : ", fontCommon, brush, rect);
                graphics.DrawString(lstENTOrder[0].ExtraCharge.ToString(), fontCommon, brush, rect, formatRightToLeft);
                Offset = Offset + 10;

                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;

                rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                graphics = DrawStringRight(graphics, "Total Payable Amount: " + lstENTOrder[0].PayableAmount.ToString(), new Font("Verdana", 9, FontStyle.Bold), rect, Color.Black);
                Offset = Offset + 10;

                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;

                rect = new RectangleF(startX, startY + Offset, 275.0F, 25.0F);
                graphics = DrawStringCenter(graphics, "Thank You For Visit.", fontDetail, rect, Color.Black);
            }
        }

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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
