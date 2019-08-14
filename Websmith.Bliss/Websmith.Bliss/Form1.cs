using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Drawing.Printing;
using System.Drawing;

namespace Websmith.Bliss
{
    public partial class Form1 : Form
    {
        Socket clientSocket;
        bool clientMode = true;
        int attempts = 0;
        AsynchronousClient me;

        public Form1()
        {
            //initialize all graphic components
            InitializeComponent();
        }

        //open client panel
        private void button1_Click(object sender, EventArgs e)
        {
            clientMode = true;
            this.Controls.Remove(panel2); //close server panel
            this.Controls.Add(panel1);    //add client panel
            AsynchronousClient.console = this.clientConsole; //link console form for static usage
            AsynchronousClient.consoleContainer = this.panel3; //link parent of this form for scroll to bottom static usage
        }

        //open server panel
        private void button2_Click(object sender, EventArgs e)
        {
            clientMode = false;
            this.Controls.Remove(panel1);
            this.Controls.Add(panel2);
            AsynchronousServer.console = this.serverConsole;   //link forms for static usage
            AsynchronousServer.consoleContainer = this.panel6;
            AsynchronousServer.list = this.panel5;   //link list of all connected clients

            if (!AsynchronousServer.runningServer)
            {
                this.serverConsole.Text = "";
                AsynchronousServer.StartListening();   //autostarts server
            }
        }

        //connect client if not already
        private void connect1(object sender, EventArgs e)
        {
            if (AsynchronousClient.connected)
            {
                showAlert("You are already connected");
            }
            else
            {
                String data = this.textBox1.Text;
                String[] split = data.Split(':'); //read ip:port combination from input
                if(split.Length == 2)
                {
                    this.clientMode = true;
                    try
                    {
                        //start client and connect to server with given ip and port
                        AsynchronousClient.keepConnection = true;
                        AsynchronousClient.ipAddress = IPAddress.Parse(split[0]);
                        AsynchronousClient.port = Int32.Parse(split[1]);
                        AsynchronousClient.console = this.clientConsole;
                        AsynchronousClient.consoleContainer = this.panel3;
                        AsynchronousClient.StartClient();
                    }catch(System.FormatException e2)
                    {
                        showAlert("Invalid ip/port");
                    }
                }
                else
                {
                    showAlert("Bad adress, only allowed ip:port combination");
                }
            }
        }

        //connect server if not already
        private void connect2(object sender, EventArgs e)
        {
            //timer1.Start();
            if (AsynchronousServer.runningServer)
            {
                showAlert("Server already running");
            }
            else
            {
                String data = this.textBox2.Text;
                if (data.Length > 0)
                {  
                    //get data from input box, and start server
                    this.clientMode = false;
                    AsynchronousServer.port = Int32.Parse(data);
                    AsynchronousServer.StartListening();
                }
                else
                {
                    showAlert("Bad adress, only allowed ip:port combination");
                }
            }
        }

        //send message from client panel
        private void send1_Click(object sender, EventArgs e)
        {

            if (AsynchronousClient.client == null || !AsynchronousClient.connected)
            {
                showAlert("Client is not connected to server");
            }
            else
            {
                String data = this.input1.Text;
                AsynchronousClient.Send(data);
                send1.Focus();
                //this.input1.Text = "";
            }
        }

        //send message from server panel
        private void send2_Click(object sender, EventArgs e)
        {
            if (AsynchronousServer.clients.Count == 0)
            {
                showAlert("No client is connected to server");
            }
            else {
                String data = this.input2.Text;
                AsynchronousServer.Send(data, -1);
                send2.Focus();
                //this.input2.Text = "";
            }
        }

        //disconnect client if connected
        private void disconnect1_Click(object sender, EventArgs e)
        {
            if (AsynchronousClient.connected) {
                AsynchronousClient.keepConnection = false;
                AsynchronousClient.close();
            }
            else
            {
                if (AsynchronousClient.keepConnection)
                {
                    AsynchronousClient.keepConnection = false;
                }
                else
                {
                    showAlert("You are not connected to any server");
                }
            }
        }

        //disconnect all clients from server, and close server if opened
        private void disconnect2_Click(object sender, EventArgs e)
        {
            if (AsynchronousServer.runningServer)
            {
                AsynchronousServer.close();
            }
            else
            {
                showAlert("You are not connected to any server");
            }
        }

        //open popup with message
        private void showAlert(String message, String caption = "Alert")
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, caption, buttons);
        }
        
        //clear client console
        private void button3_Click(object sender, EventArgs e)
        {
            AsynchronousClient.ClientSetControlPropertyThreadSafe(this.clientConsole, "Text", "");
        }

        //clear server console
        private void button4_Click(object sender, EventArgs e)
        {
            AsynchronousServer.ServerSetControlPropertyThreadSafe(this.serverConsole, "Text", "");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ////PrintDocument pdoc = null;
            ////PrintDialog pd = new PrintDialog();
            ////pdoc = new PrintDocument();
            ////PrinterSettings ps = new PrinterSettings();
            ////Font font = new Font("Verdana", 15);
            ////PaperSize psize = new PaperSize("Custom", 100, 200);
            ////pd.Document = pdoc;
            ////pd.Document.DefaultPageSettings.PaperSize = psize;
            ////pdoc.DefaultPageSettings.PaperSize.Height = 820;
            ////pdoc.DefaultPageSettings.PaperSize.Width = 520;
            ////pdoc.PrinterSettings.PrinterName = "EPSON TM-m30 Receipt";  //this function return default printer
            ////pdoc.PrintPage += new PrintPageEventHandler(pdoc_OrderReceipt);  // dynamic event for print receipt
            ////pdoc.Print();

            //attempts = 0;
            //timer1.Stop();
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

                int startX = 5; // this is start printing after 5px from left (startX means Horizontal)
                int startY = 5; // this is start printing after 5px from top (startY means Vertical)
                int Offset = 10; // this will add offset value for new line to startY (startY+Offset)
                graphics.DrawString(underLine, fontLine, new SolidBrush(Color.Black), startX, startY + Offset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Print", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //attempts++;
            //if (AsynchronousServer.clients.Count == 0)
            //{
            //    showAlert("No client is connected to server");
            //}
            //else
            //{
            //    String data = "Sent message from server. Count : " + attempts;
            //    AsynchronousServer.Send(data, -1);
            //    //send2.Focus();
            //    //this.input2.Text = "";
            //}
        }
    }


    //helper class to modify form object property from another thread than the one from wich form was created
    class TestFormCotrolHelper
    {
        delegate void UniversalVoidDelegate();

        /// <summary>
        /// Call form controll action from different thread
        /// </summary>
        public static void ControlInvike(Control control, Action function)
        {
            if (control.IsDisposed || control.Disposing)
                return;

            if (control.InvokeRequired)
            {
                control.Invoke(new UniversalVoidDelegate(() => ControlInvike(control, function)));
                return;
            }
            function();
        }
    }

}
