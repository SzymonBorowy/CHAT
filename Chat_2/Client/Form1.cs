using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SimpleTCP;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SimpleTcpClient client;
        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            client = new SimpleTcpClient().Connect("127.0.0.1", 8910);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;

        }
        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            //txtStatus.Invoke((MethodInvoker)delegate ()
            //{
                MessageBox.Show(e.MessageString.ToString());
                string text = e.MessageString;
                txtStatus.Text += text;
           // });
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            client.WriteLineAndGetReply(txtMessage.Text+"\n", TimeSpan.FromSeconds(3));
            txtMessage.Clear();
            //client.WriteLine(txtMessage.Text);
        }
    }
}
