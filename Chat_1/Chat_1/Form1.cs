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

namespace Chat_1
{
    public partial class Form1 : Form
    {
        Socket sck;
        EndPoint epLockal, epRemote;
        public Form1()
        {
            InitializeComponent();
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            uzydkownikIP.Text = wczytaj_lokalne_IP();
            klientIP.Text = wczytaj_lokalne_IP();
        }
        private string wczytaj_lokalne_IP()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            
            foreach(IPAddress ip in host.AddressList)
            {
                if(ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }

        private void start_Click(object sender, EventArgs e)
        {
            try
            {
                epLockal = new IPEndPoint(IPAddress.Parse(uzydkownikIP.Text), Convert.ToInt32(uzydkownikPort.Text));
                sck.Bind(epLockal);

                epRemote = new IPEndPoint(IPAddress.Parse(klientIP.Text), Convert.ToInt32(klientPort.Text));
                sck.Connect(epRemote);

                byte[] buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(zwroc_wiadomosc), buffer);

                start.Text = "Połączony";
                start.Enabled = false;
                wyslij.Enabled = true;
                wiadomosc.Focus();
                listBox1.Items.Add("Connected");

            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.ToString());
            }
        }

        private void wyslij_Click(object sender, EventArgs e)
        {
            try
            {
                ASCIIEncoding enc = new ASCIIEncoding();
                byte[] msg = new byte[1500];
                msg = enc.GetBytes(wiadomosc.Text);
                sck.Send(msg);

                listBox1.Items.Add("Ty: " + wiadomosc.Text);
                wiadomosc.Clear();
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.ToString());
            }
        }

        private void zwroc_wiadomosc(IAsyncResult aResult)
        {
            try
            {
                int rozmiar = sck.EndReceiveFrom(aResult, ref epRemote);
                if(rozmiar >0 )
                {
                    byte[] receivedData = new byte[1464];
                    receivedData = (byte[])aResult.AsyncState;

                    ASCIIEncoding eEncoding = new ASCIIEncoding();
                    string receivedMessage = eEncoding.GetString(receivedData);
                    listBox1.Items.Add("Klient: " + receivedMessage);
                }
                byte[] buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(zwroc_wiadomosc), buffer);
            }
            catch (Exception exp )
            {

                MessageBox.Show(exp.ToString());
            }
        }

    }
}
