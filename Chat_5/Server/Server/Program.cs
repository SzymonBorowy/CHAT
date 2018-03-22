using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Nasluchiwanie polaczenia TCP na porcie 3002");
            TcpListener tcp_listener = new TcpListener(IPAddress.Any, 3002);
            tcp_listener.Start(); //zacznij nasluchiwac polaczen
            
            TcpClient client = tcp_listener.AcceptTcpClient();
            Console.WriteLine("Zaakceptowano klienta");
            Console.WriteLine("Utworzono strumien do przesylania danych");
            NetworkStream ns = client.GetStream();

            byte[] buffer = new byte[client.ReceiveBufferSize];
            int data = ns.Read(buffer, 0, client.ReceiveBufferSize);

            while (!tcp_listener.Pending())
            {
                string message = Encoding.Unicode.GetString(buffer, 0, client.ReceiveBufferSize);
                Console.WriteLine("Message: " + message);
            }
            Console.ReadKey();
        }

        
    }
}
