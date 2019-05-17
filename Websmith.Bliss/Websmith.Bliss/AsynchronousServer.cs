using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using BAL = Websmith.BusinessLayer;

namespace Websmith.Bliss
{
    class AsynchronousServer
    {
        public static int connectedCount = 0;

        public static int bufferSize = 500000000; //1024; //buffer size for messages
        public static int port = Properties.Settings.Default.Port;
        public static IPAddress ipAddress;

        public static bool runningServer = false;

        public static Label console;
        public static Panel consoleContainer;

        public static Panel list;

        public static Socket server;
        public static Dictionary<string, Client> clients;

        static byte[] bytes = new Byte[bufferSize];  //bytes buffer for messages - if applyed
        static string response = String.Empty;       //latest message - if applyed

        static Thread chatThread;
        static Thread listenThread;

        public static void StartListening() // start server and listen for clients
        {
            list.Controls.Clear();

            clients = new Dictionary<string, Client>();

            string brentClients = Properties.Settings.Default.brentClients; // get all older clients
            if (brentClients.Length > 1)
            {
                string[] split = brentClients.Split('$');
                Console.WriteLine("brent : " + brentClients);

                connectedCount = 0;

                Console.WriteLine("split: " + split.Length);

                for (int i = 0; i < split.Length; i++)// add all older clients to list
                {
                    Console.WriteLine("add client");
                    clients.Add(split[i], new Client(split[i], null, connectedCount));
                    connectedCount++;
                }
            }

            runningServer = true;

            Console.WriteLine("Running = true;");

            String localIp = GetLocalIPAddress();

            console.Text = "Connecting to " + localIp + ":" + port + "\n";
            Console.WriteLine("Console height: " + AsynchronousServer.console.Size.Height);

            IPAddress ipAddress = IPAddress.Parse(localIp);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            server = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);

            server.Bind(localEndPoint);
            server.Listen(10);

            console.Text += "Server started\n";

            listenThread = new Thread(() => //thread to listen for clients
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    if (runningServer)
                    {
                        try
                        {
                            Socket client = server.Accept();
                            bool contained = true;

                            contained = clients.ContainsKey(client.RemoteEndPoint.ToString().Split(':')[0]); //check if client is already in list

                            if (contained) //if client already in list, set new socket, and status to connected
                            {
                                clients[client.RemoteEndPoint.ToString().Split(':')[0]].client = client;
                                clients[client.RemoteEndPoint.ToString().Split(':')[0]].instanceClient();
                                try
                                {
                                    ServerSetControlPropertyThreadSafe(AsynchronousServer.console, "Text", AsynchronousServer.console.Text + "Client reconnected: " + client.RemoteEndPoint.ToString() + "  (" + clients[client.RemoteEndPoint.ToString().Split(':')[0]].index + ")" + "\n");
                                }
                                catch (Exception)
                                {
                                }
                            }
                            else //if client not already in list set it as new client
                            {
                                clients.Add(client.RemoteEndPoint.ToString().Split(':')[0], new Client(client.RemoteEndPoint.ToString().Split(':')[0], client, connectedCount));
                                connectedCount++;
                                StoreData();
                            }
                        }
                        catch (System.Net.Sockets.SocketException)
                        {
                            runningServer = false;
                        }
                    }
                    else
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }
            });

            listenThread.Start();

        }

        //function to store clients for window exit/reload
        public static void StoreData()
        {
            string storeData = "";

            foreach (KeyValuePair<string, Client> client in clients)
            {
                storeData += client.Key + "$"; //separate all ip adressed by '$'
            }

            if (storeData.Length > 0)
            {
                storeData = storeData.Substring(0, storeData.Length - 1);
            }
            Console.WriteLine("Store data : " + storeData);
            Properties.Settings.Default.brentClients = storeData;
            Properties.Settings.Default.Save();
        }

        //send data to all clients, except for case when data is coming from another client, and "without" variable is index of this client. When "without" variable is -1 means is from server to all clients.
        public static void Send(String data, int without)
        {

            if (without == -1)
            {
                ServerSetControlPropertyThreadSafe(console, "Text", console.Text + "Server: " + data + "\n");
                data += "\n";
            }
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            foreach (KeyValuePair<string, Client> client in clients)
            {
                if (client.Value.index != without) //when "without" != -1, means that "without" is index of client that sent this message
                {
                    client.Value.Send(byteData);
                }
            }
        }

        public static void close() //disconnect all clients and server socket
        {
            if (runningServer)
            {
                try
                {
                    foreach (KeyValuePair<string, Client> client in clients)
                    {
                        client.Value.close();
                    }
                    if (runningServer)
                    {
                        server.Close();
                        runningServer = false;
                        ServerSetControlPropertyThreadSafe(console, "Text", console.Text + "Server closed\n");
                    }
                }
                catch (Exception e)
                {
                    ServerSetControlPropertyThreadSafe(console, "Text", console.Text + "Error trying to close server: " + e.ToString() + "\n");
                }
            }

        }

        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        //helper methods to set form text from another thread
        private delegate void ServerSetControlPropertyThreadSafeDelegate(System.Windows.Forms.Control control, string propertyName, object propertyValue);

        public static void ServerSetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new ServerSetControlPropertyThreadSafeDelegate
                (ServerSetControlPropertyThreadSafe),
                new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(propertyName,BindingFlags.SetProperty,null,control,new object[] { propertyValue });
            }

            //get new console height and set scroll to bottom, using helper class to do this, because this is usually called from another thread (listenThreadClass)
            TestFormCotrolHelper.ControlInvike(AsynchronousServer.consoleContainer, () => AsynchronousServer.consoleContainer.VerticalScroll.Maximum = AsynchronousServer.console.Size.Height + 20);
            TestFormCotrolHelper.ControlInvike(AsynchronousServer.consoleContainer, () => AsynchronousServer.consoleContainer.VerticalScroll.Value = AsynchronousServer.consoleContainer.VerticalScroll.Maximum);
        }

    }

    //client class for instance in array
    class Client
    {
        public bool connected = false;
        public int index; //index in server's list
        public Socket client;
        public string key;

        private String response = String.Empty; //latest message from server
        private byte[] bytes = new byte[AsynchronousServer.bufferSize]; //bytes buffer to read message

        static Thread chatThread; //thread to listen from messages from client

        Panel itemPanel;
        Label itemIndex;
        Label itemAdress;
        PictureBox itemStatus;
        Button itemDelete;

        public Client(string key, Socket client, int count)
        {
            this.key = key;
            this.client = client;
            this.index = count;
            FillList();
            if (client != null)
            { //if client is created from server.accept() result, then start listen thread, otherwise means client is created without socket connection, from list of all connected/disconnected clients
                AsynchronousServer.ServerSetControlPropertyThreadSafe(AsynchronousServer.console, "Text", AsynchronousServer.console.Text + "New client connected: " + client.RemoteEndPoint.ToString() + "  (" + index + ")" + "\n");
                instanceClient();
            }
        }

        public void instanceClient()
        {
            this.connected = true;
            itemStatus.ImageLocation = "green.bmp"; //set connected image in clients list

            new BAL.DeviceMaster().ChangeStatus(key, 2); // change device status to connected

            chatThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (connected)
                {
                    try
                    {
                        int receivedBytes = 0;
                        try
                        {
                            receivedBytes = client.Receive(bytes);
                        }
                        catch (Exception){ }
                       
                        //get message from connected client in bytes buffer
                        if (receivedBytes == 0) //if not bytes received means client lost connection
                        {
                            //AsynchronousServer.ServerSetControlPropertyThreadSafe(AsynchronousServer.console, "Text", AsynchronousServer.console.Text + "Client " + index + " (" + key + "): Lost connection" + "  (" + index + ")" + "\n");
                            ////client.Shutdown(SocketShutdown.Both);
                            //client.Close();
                            //connected = false;
                            //itemStatus.ImageLocation = "red.bmp";//set disconnected image in clients list
                            //new BAL.DeviceMaster().ChangeStatus(key, 1); // change device status to disconnected
                        }
                        else //put buffer bytes in string
                        {
                            response = Encoding.ASCII.GetString(bytes, 0, receivedBytes);
                            var result = new ClientServerDataParsing().GetResponseJson(response);
                            AsynchronousServer.ServerSetControlPropertyThreadSafe(AsynchronousServer.console, "Text", AsynchronousServer.console.Text + "Client " + index + " (" + key + "): " + response);
                            Console.WriteLine("Console height: " + AsynchronousServer.console.Size.Height);
                            if (result.Item1)
                            {
                                AsynchronousServer.Send(result.Item2, index);
                            }
                            else
                            {
                                //AsynchronousServer.Send(response, index);
                            }
                        }
                    }
                    catch (System.Net.Sockets.SocketException)
                    {
                        connected = false;
                        connected = false;
                        itemStatus.ImageLocation = "red.bmp";//set disconnected image in clients list
                        new BAL.DeviceMaster().ChangeStatus(key, 1); // change device status to disconnected
                    }
                    Thread.Sleep(500);
                }
            });

            chatThread.Start();
        }

        public void Send(byte[] byteData) //send data to client
        {
            if (connected)
            {
                client.Send(byteData);
            }
        }

        public void FillList() //render current client in all clients list
        {
            itemPanel = new System.Windows.Forms.Panel();
            itemIndex = new System.Windows.Forms.Label();
            itemAdress = new System.Windows.Forms.Label();
            itemStatus = new System.Windows.Forms.PictureBox();
            itemDelete = new System.Windows.Forms.Button();

            itemPanel.SuspendLayout();

            itemPanel.ResumeLayout(false);
            itemPanel.PerformLayout();

            Console.WriteLine("FillList : " + AsynchronousServer.clients.Count + "   key: " + this.key);

            itemPanel.Location = new System.Drawing.Point(0, 25 * AsynchronousServer.clients.Count + 5);
            itemPanel.Size = new System.Drawing.Size(250, 25);

            itemIndex.Location = new System.Drawing.Point(0, 0);
            itemIndex.Size = new System.Drawing.Size(25, 25);
            itemIndex.Text = index.ToString();
            itemIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // itemIndex.Click += new System.EventHandler(this.label3_Click);

            itemStatus.Location = new System.Drawing.Point(25, 0);
            itemStatus.Size = new System.Drawing.Size(25, 25);
            itemStatus.ImageLocation = "red.bmp";
            itemStatus.BackColor = System.Drawing.Color.Gray;

            itemAdress.Location = new System.Drawing.Point(50, 0);
            itemAdress.Size = new System.Drawing.Size(125, 25);
            itemAdress.Text = this.key;
            itemAdress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            itemDelete.Location = new System.Drawing.Point(175);
            itemDelete.Size = new System.Drawing.Size(75, 25);
            itemDelete.Text = "DELETE";
            itemDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            itemDelete.Click += new System.EventHandler(deleteClick); //set function to "DELETE" click
            itemDelete.Visible = false;  // delete button visible false

            itemPanel.Controls.Add(itemIndex);
            itemPanel.Controls.Add(itemStatus);
            itemPanel.Controls.Add(itemAdress);
            itemPanel.Controls.Add(itemDelete);

            //add item using helper method, because it is usually called from another thread
            TestFormCotrolHelper.ControlInvike(AsynchronousServer.list, () => AsynchronousServer.list.Controls.Add(itemPanel));
        }

        public void close() //disconnect client
        {
            if (connected)
            {                
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                this.connected = false;
                itemStatus.ImageLocation = "red.bmp"; //set disconnected image to list item
                new BAL.DeviceMaster().ChangeStatus(key, 1); // change device status to connected
            }
        }

        private void deleteClick(object sender, EventArgs e) // remove client from list, this function is triggered from "DELETE" button
        {
            if (connected)
            {
                connected = false;
                AsynchronousServer.ServerSetControlPropertyThreadSafe(AsynchronousServer.console, "Text", AsynchronousServer.console.Text + "Client " + index + " (" + key + "): Lost connection" + "  (" + index + ")" + "\n");
                AsynchronousServer.ServerSetControlPropertyThreadSafe(AsynchronousServer.console, "Text", AsynchronousServer.console.Text + "Client " + index + " (" + key + "): Deleted" + "  (" + index + ")" + "\n");
                AsynchronousServer.clients.Remove(this.key);
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                TestFormCotrolHelper.ControlInvike(AsynchronousServer.list, () => AsynchronousServer.list.Controls.Remove(itemPanel));
            }
            else
            {
                AsynchronousServer.clients.Remove(this.key);
                TestFormCotrolHelper.ControlInvike(AsynchronousServer.list, () => AsynchronousServer.list.Controls.Remove(itemPanel));
                AsynchronousServer.ServerSetControlPropertyThreadSafe(AsynchronousServer.console, "Text", AsynchronousServer.console.Text + "Client " + index + " (" + key + "): Deleted" + "  (" + index + ")" + "\n");
            }
            AsynchronousServer.StoreData();

            Console.WriteLine("My Index: " + this.index);
            foreach (KeyValuePair<string, Client> item in AsynchronousServer.clients) //move all items below this with 25 units upper, for not leaving blank space in list
            {
                Console.WriteLine("item: " + item.Value.index);
                if (item.Value.index > this.index)
                {
                    item.Value.itemPanel.Location = new System.Drawing.Point(0, item.Value.itemPanel.Location.Y - 25);
                }
            }
        }
    }
}

