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
        static int bufferSize = 500000000; //1024; //max size in bytes of single income message

        public static bool connected = false; //client status

        public static int port;
        public static IPAddress ipAddress;

        public static Panel consoleContainer;
        public static Label console;

        public static Socket client;

        private static String response = String.Empty;      //latest message from server
        private static byte[] bytes = new byte[bufferSize]; //buffer to read brent server message

        static Thread listenThread;   //thread to listen for messages asynchronous

        public static bool keepConnection = false;

        public static void StartClient()
        {
            try
            {
                //set text to console using local helper class to set text from another thread than the one it was created from
                ClientSetControlPropertyThreadSafe(console, "Text", console.Text + "Try connecting to " + ipAddress + ":" + port + "...\n");

                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                //create thread object that listens for messages from server while it is connected
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
                        ClientSetControlPropertyThreadSafe(console, "Text", console.Text + "Unable to connect\n");
                        if (keepConnection)
                            StartClient();
                        return;
                    }

                    ClientSetControlPropertyThreadSafe(console, "Text", console.Text + "Connected\n");

                    while (client.Connected && connected)
                    {
                        try
                        {
                            int receivedBytes = client.Receive(bytes); //place brent response from server in bytes buffer
                            if (receivedBytes == 0)//means server connection is lost
                            {
                                if (connected)
                                {
                                    ClientSetControlPropertyThreadSafe(console, "Text", console.Text + "Lost server connection 1 \n");
                                }
                                connected = false;
                                if (keepConnection)
                                    StartClient();
                            }
                            else//read response from server
                            {
                                response = Encoding.ASCII.GetString(bytes, 0, receivedBytes);
                                ClientSetControlPropertyThreadSafe(console, "Text", console.Text + "Server: " + response);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            if (connected)
                            {
                                ClientSetControlPropertyThreadSafe(console, "Text", console.Text + "Lost server connection 3 \n");
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
                ClientSetControlPropertyThreadSafe(console, "Text", console.Text + "Error connecting check ip/port\n");
                if (keepConnection)
                    StartClient();
            }
        }

        //send message to server
        public static void Send(String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data + "\n");

            int bytesSent = client.Send(byteData);
            ClientSetControlPropertyThreadSafe(console, "Text", console.Text + "Client: " + data + "\n");
        }

        //disconnect client from server
        public static void close()
        {
            connected = false;
            ClientSetControlPropertyThreadSafe(console, "Text", console.Text + "Disconnected from server\n");
            if (client != null)
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
        }

        //helper methods to set form text from another thread
        private delegate void ClientSetControlPropertyThreadSafeDelegate(System.Windows.Forms.Control control,string propertyName,object propertyValue);

        public static void ClientSetControlPropertyThreadSafe(Control control,string propertyName,object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new ClientSetControlPropertyThreadSafeDelegate
                (ClientSetControlPropertyThreadSafe),
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

            //get new console height and set scroll to bottom, using helper class to do this, because this is usually called from another thread (listenThreadClass)
            TestFormCotrolHelper.ControlInvike(consoleContainer, () => consoleContainer.VerticalScroll.Maximum = console.Size.Height + 20);
            TestFormCotrolHelper.ControlInvike(consoleContainer, () => consoleContainer.VerticalScroll.Value = consoleContainer.VerticalScroll.Maximum);
        }
    }
}
