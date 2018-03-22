using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

namespace klient_1
{
    public partial class Form1 : Form
    {
        TcpClient client;
        NetworkStream ns;
        public Form1()
        {
            InitializeComponent();
            listLog.Items.Add("Utworzenie obiektu TcpClient na porcie 3002");
            TcpClient client = new TcpClient("127.0.0.1", 3002);
            listLog.Items.Add("Tworzenie obiektu do obslugi strumienia");
            NetworkStream ns = client.GetStream();
        }

        private void btn_wyslij_Click(object sender, EventArgs e)
        {
            listLog.Items.Add("Podaj wiadomosc:");
            string wiadomosc = txtWiadomosc.Text;
            listLog.Items.Add("Kodowanie wiadomosci");
            byte[] message = Encoding.Unicode.GetBytes(wiadomosc);
            listLog.Items.Add("Wysylanie wiadomosci do serwera");
            ns.Write(message, 0, message.Length);
            txtWiadomosc.Clear();
            //listLog.Items.Add("Usuwanie obiektu TcpClient i przerywanie polaczenia TCP");
            //client.Close();
            //Console.ReadKey();
        }
    }
}
