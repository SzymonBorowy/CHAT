using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Sockets;
using System.Net;

namespace Klient
{
    public partial class Form1 : Form
    {
        
        TcpClient client = new TcpClient("127.0.0.1", 3002);
       
        public Form1()
        {
            InitializeComponent();
        }

        private void wyslij_Click(object sender, EventArgs e)
        {
            listLog.Items.Add("Tworzenie obiektu do obslugi strumienia");
            NetworkStream ns = client.GetStream();
            listLog.Items.Add("Podaj wiadomosc:");
            string ch = Console.ReadLine();
            listLog.Items.Add("Kodowanie wiadomosci");
            byte[] message = Encoding.Unicode.GetBytes(ch);
            listLog.Items.Add("Wysylanie wiadomosci do serwera");
            ns.Write(message, 0, message.Length);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listLog.Items.Add("Utworzenie obiektu TcpClient na porcie 3002");
        }
    }
}
