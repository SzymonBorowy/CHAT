using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace client
{
    public partial class Form1 : Form
    {

        private static Socket _clientSocket = new Socket
        (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        //NetworkStream serverStream;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string req = "hello world";
            byte[] buffer = Encoding.ASCII.GetBytes(req);
            _clientSocket.Send(buffer);

            byte[] receiveBuf = new byte[1024];
            int rec = _clientSocket.Receive(receiveBuf);
            byte[] data = new byte[rec];
            Array.Copy(receiveBuf, data, rec);
            msg(Encoding.ASCII.GetString(data));

            /*
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Message from Client$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[10025];
            serverStream.Read(inStream, 0, 10000);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            msg("Data from Server : " + returndata);
            */

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            msg("Client Started");
            _clientSocket.Connect("127.0.0.1", 100);
            label1.Text = "Client Socket Program - Server Connected ...";
        }
        public void msg(string mesg)
        {
            textBox1.Text = textBox1.Text + Environment.NewLine + " >> " + mesg;
        }
    }
}
