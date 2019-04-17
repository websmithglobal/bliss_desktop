using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Collections;

namespace Websmith.Bliss
{
    class AsynchronousServer
    {
        public static int bufferSize = 1024;
        public static int port = Properties.Settings.Default.Port;
        public static IPAddress ipAddress;

        public static bool runningServer = false;

        public static Label console = new Label();

        public static Socket server;
        public static ArrayList clients;

        static byte[] bytes = new Byte[bufferSize];
        static string response = String.Empty;

        static Thread chatThread;
        static Thread listenThread;

        public static void StartListening()
        {
        
            runningServer = true;
            clients = new ArrayList();

            Console.WriteLine("Running = true;");

            String localIp = GetLocalIPAddress();
            console.Text = "Connecting to " + localIp + ":" + port + "\n";
            IPAddress ipAddress = IPAddress.Parse(localIp);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(localEndPoint);
            server.Listen(10);

            console.Text += "Server started\n";

            listenThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    if (runningServer)
                    {
                        try
                        {
                            Socket client = server.Accept();
                            clients.Add(new Client(client, clients.Count));
                        }catch(SocketException e)
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

        public static void Send(String data, int without)
        {

            if (without == -1)
            {
                frmSocketServer.SetControlPropertyThreadSafe(console, "Text", console.Text + "Server: " + data + "\n");
                data += "<EOF>\n";
            }
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            foreach(Client client in clients)
            {
                if (client.index != without)
                {
                    client.Send(byteData);
                }
            }

        }

        public static void close()
        {
         
            if (runningServer)
            {
                try
                {
                    foreach (Client client in clients)
                    {
                        client.close();
                    }
                    clients.Clear();
                    if (runningServer)
                    {
                        server.Close();
                        runningServer = false;
                        frmSocketServer.SetControlPropertyThreadSafe(console, "Text", console.Text + "Server closed\n");
                    }      
                }
                catch (Exception e)
                {
                    frmSocketServer.SetControlPropertyThreadSafe(console, "Text", console.Text + "Error trying to close server: " + e.ToString() + "\n");
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

    }

    class Client
    {
        public bool connected = false;
        public int index;
        public  Socket client;

        private String response = String.Empty;
        private byte[] bytes = new byte[AsynchronousServer.bufferSize];

        static Thread chatThread;

        public Client(Socket client, int count)
        {

            this.client = client;
            this.index = count;
            this.connected = true;

            frmSocketServer.SetControlPropertyThreadSafe(AsynchronousServer.console, "Text", AsynchronousServer.console.Text + "Client connected: " + client.RemoteEndPoint.ToString() + "\n");

            chatThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (connected)
                {
                    try
                    {
                        int receivedBytes = client.Receive(bytes);
                        if (receivedBytes == 0)
                        {
                            frmSocketServer.SetControlPropertyThreadSafe(AsynchronousServer.console, "Text", AsynchronousServer.console.Text + "E1 Client " + index + " (" + client.RemoteEndPoint.ToString() + "): Lost connection" + "\n");
                            client.Shutdown(SocketShutdown.Both);
                            client.Close();
                            connected = false;
                            AsynchronousServer.clients.Remove(this);
                        }
                        else
                        {
                            response = Encoding.ASCII.GetString(bytes, 0, receivedBytes);
                            frmSocketServer.SetControlPropertyThreadSafe(AsynchronousServer.console, "Text", AsynchronousServer.console.Text + "Client " + index + " (" + client.RemoteEndPoint.ToString()  + "): " + response);
                            AsynchronousServer.Send(response, index);
                        }
                    }
                    catch (System.Net.Sockets.SocketException e)
                    {
                        connected = false;
                        AsynchronousServer.clients.Remove(index);
                    } 
                    Thread.Sleep(500);
                }
            });

            chatThread.Start();

        }

        public void Send(byte[] byteData)
        {
            client.Send(byteData);
        }

        public void close()
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            this.connected = false;
        }
    }

}

