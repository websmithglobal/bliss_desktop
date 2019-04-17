using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;

namespace Websmith.Bliss
{
    
    public class AsynchronousClient
    {
        static int bufferSize = 1024;

        public static bool connected = false;

        public static int port;
        public static IPAddress ipAddress;
        public static Label console;

        public static Socket client;

        private static String response = String.Empty;
        private static byte[] bytes = new byte[bufferSize];

        static Thread listenThread;

        public static bool keepConnection = false;

        public static void StartClient()
        {
            try
            {
                SetControlPropertyThreadSafe(console, "Text", console.Text + "Try connecting to " + ipAddress + ":" + port + "...\n");

                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                listenThread = new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    try
                    {
                        client.Connect(remoteEP);
                        connected = true;
                    }
                    catch (System.Net.Sockets.SocketException e)
                    {
                        Console.WriteLine(e.ToString());
                        SetControlPropertyThreadSafe(console, "Text", console.Text + "Unable to connect\n");
                        if (keepConnection)
                            StartClient();
                        return;
                    }
          
                    SetControlPropertyThreadSafe(console, "Text", console.Text + "Connected\n");
                    while (client.Connected && connected)
                    {
                        try
                        {
                            int receivedBytes = client.Receive(bytes);
                            if (receivedBytes == 0)
                            {
                                if (connected)
                                {
                                    SetControlPropertyThreadSafe(console, "Text", console.Text + "Lost server connection 1 \n");
                                }
                                connected = false;
                                if (keepConnection)
                                    StartClient();
                            }
                            else
                            {
                                response = Encoding.ASCII.GetString(bytes, 0, receivedBytes);
                                SetControlPropertyThreadSafe(console, "Text", console.Text + "Server: " + response);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            if (connected)
                            {
                                SetControlPropertyThreadSafe(console, "Text", console.Text + "Lost server connection 3 \n");
                            }
                            connected = false;
                            if (keepConnection)
                                StartClient();
                        }
                    }


                });
                listenThread.Start();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                connected = false;
                Console.WriteLine(e.ToString());
                SetControlPropertyThreadSafe(console, "Text", console.Text + "Error connecting check ip/port\n");
                if (keepConnection)
                    StartClient();
            }
        }

        public static void Send(String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data + "\n");// + "<EOF>\n"

            int bytesSent = client.Send(byteData);
            SetControlPropertyThreadSafe(console, "Text", console.Text + "Client: " + data + "\n");
        }

        public static void close()
        {
            connected = false;
            console.Text += "Disconnected from server\n";
            if (client != null) { 
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
        }

        private delegate void SetControlPropertyThreadSafeDelegate(System.Windows.Forms.Control control,string propertyName,object propertyValue);

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate
                (SetControlPropertyThreadSafe),
                new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(
                    propertyName,
                    BindingFlags.SetProperty,
                    null,
                    control,
                    new object[] { propertyValue });
            }
        }

    }
}
