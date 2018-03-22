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

namespace Serwer_1
{
    public partial class Form1 : Form
    {
        TcpListener tcp_listener;
        TcpClient client;
        public Form1()
        {
            InitializeComponent();
            tcp_listener = new TcpListener(IPAddress.Any, 3002);
        }

        private void start_Click(object sender, EventArgs e)
        {
            tcp_listener.Start(); //zacznij nasluchiwac polaczen
            listLog.Items.Add("Nasluchiwanie polaczenia TCP na porcie 3002");
            listLog.Items.Add("Szukanie klientow");
            listLog.Refresh();
            client = tcp_listener.AcceptTcpClient();
            listLog.Items.Add("Zaakceptowano klienta");
        }
        private void wczytaj_wiadomosc()
        {
            listLog.Items.Add("Utworzono strumien do przesylania danych");
            NetworkStream ns = client.GetStream();

            byte[] buffer = new byte[client.ReceiveBufferSize];
            int data = ns.Read(buffer, 0, client.ReceiveBufferSize);

            string message = Encoding.Unicode.GetString(buffer, 0, client.ReceiveBufferSize);
            listLog.Items.Add("Message: " + message);
        }
    }
}
