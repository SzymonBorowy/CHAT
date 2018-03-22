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

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listLog.Items.Add("Nasluchiwanie polaczenia TCP na porcie 3002");
            TcpListener tcp_listener = new TcpListener(IPAddress.Any, 3002);
            tcp_listener.Start(); //zacznij nasluchiwac polaczen
            listLog.Items.Add("Zaakceptowano klienta");
            TcpClient client = tcp_listener.AcceptTcpClient();
            listLog.Items.Add("Utworzono strumien do przesylania danych");
            NetworkStream ns = client.GetStream();

            byte[] buffer = new byte[client.ReceiveBufferSize];
            int data = ns.Read(buffer, 0, client.ReceiveBufferSize);

            string message = Encoding.Unicode.GetString(buffer, 0, client.ReceiveBufferSize);
            listLog.Items.Add("Message: " + message);
        }
    }
}
