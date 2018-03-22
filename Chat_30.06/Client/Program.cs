using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static Socket _clientSocket = new Socket
        (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static string login;
        static void Main(string[] args)
        {
            //zaloguj();
            LoopConnect();
            SendLoop();
            Console.ReadLine();
        }

        private static void zaloguj()
        {
            login = Console.ReadLine().ToString();
        }

        private static void SendLoop()
        {
            while (true)
            {
                Console.Write("Enter the request: ");
                string req = Console.ReadLine();
                byte[] buffer = Encoding.ASCII.GetBytes(req);
                _clientSocket.Send(buffer);

                byte[] receiveBuf = new byte[1024];
                int rec = _clientSocket.Receive(receiveBuf);
                byte[] data = new byte[rec];
                Array.Copy(receiveBuf, data, rec);
                Console.WriteLine("received: " + Encoding.ASCII.GetString(data));
            }
        }

        private static void LoopConnect()
        {
            int attemps = 0;

            while (!_clientSocket.Connected)
            {
                try
                {
                    attemps++;
                    _clientSocket.Connect(IPAddress.Loopback, 100);
                }
                catch (SocketException)
                {
                    Console.Clear();
                    Console.WriteLine("Connection Attemps: " + attemps.ToString());
                }
            }
            Console.Clear();
            Console.WriteLine("Connected");

        }
    }
}


/*
static void Main(string[] args)
{

    //
    try
    {
        // Create a TcpClient.
        // Note, for this client to work you need to have a TcpServer 
        // connected to the same address as specified by the server, port
        // combination.
        Int32 port = 13000;
        TcpClient client = new TcpClient("127.0.0.1", port);
        while (true)
        {
            // Translate the passed "asdfjbskdfjbnsd" into ASCII and store it as a Byte array.
            string message = Console.ReadLine();
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();

            NetworkStream stream = client.GetStream();

            // Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length);

            Console.WriteLine("Sent: {0}", message);

            // Receive the TcpServer.response.

            // Buffer to store the response bytes.
            data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);
            stream.Close();
        }
        // Close everything.

        //client.Close();
    }
    catch (ArgumentNullException e)
    {
        Console.WriteLine("ArgumentNullException: {0}", e);
    }
    catch (SocketException e)
    {
        Console.WriteLine("SocketException: {0}", e);
    }

    Console.WriteLine("\n Press Enter to continue...");
    Console.Read();
    //

}
*/





//}