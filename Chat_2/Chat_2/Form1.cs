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

namespace Chat_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SimpleTcpServer server;
        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13;//enter
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;

        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            listStatus.Invoke((MethodInvoker)delegate ()
            {
                string text = e.MessageString;
                // text = text.Remove(text.IndexOf("!"));
                listStatus.Items.Add(text);
                //e.ReplyLine(string.Format("You said: {0}", text));
                e.Reply("You said: {0}"+ text);
                //MessageBox.Show(text+" ŁIII");
            });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(txtHost.Text);
            server.Start(ip, Convert.ToInt32(txtPort.Text));
            listStatus.Items.Add("Server starting ...");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
                server.Stop();
        }
    }
}
