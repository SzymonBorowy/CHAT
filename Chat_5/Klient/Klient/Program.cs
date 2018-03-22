using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;

namespace Klient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Utworzenie obiektu TcpClient na porcie 3002");
            TcpClient client = new TcpClient("127.0.0.1", 3002);
            Console.WriteLine("Tworzenie obiektu do obslugi strumienia");
            NetworkStream ns = client.GetStream();
            while(true)
            {
                Console.WriteLine("Podaj wiadomosc:");
                string ch = Console.ReadLine();
                Console.WriteLine("Kodowanie wiadomosci");
                byte[] message = Encoding.Unicode.GetBytes(ch);
                Console.WriteLine("Wysylanie wiadomosci do serwera");
                ns.Write(message, 0, message.Length);
            }
                   
            
            Console.WriteLine("Usuwanie obiektu TcpClient i przerywanie polaczenia TCP");
            //client.Close();
            Console.ReadKey();
        }
    }
}
