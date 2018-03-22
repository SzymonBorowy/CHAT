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
using System.IO;
using System.Configuration;

namespace ChatKlient
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private string addressIPServer = null; //= "192.168.1.164";
        private BinaryWriter write;
        private bool isActive = false;
        public Form1()
        {
            InitializeComponent();
            webBrowserChat.Document.Write("<html><head><style>body,table { font-size: 10pt; font - family: Verdana; margin: 3px 3px 3px 3px; font - color: black; }</style></head><body width =\"" + (webBrowserChat.ClientSize.Width - 20).ToString() + "\">");
            addressIPServer = ConfigurationManager.AppSettings["ip"];
        }

        #region obsluga
        delegate void SetTextCallBack(ListBox lista, string tekst);
        private void SetText(ListBox lista, string tekst)
        {
            if (lista.InvokeRequired)
            {
                SetTextCallBack f = new SetTextCallBack(SetText);
                this.Invoke(f, new object[] { lista, tekst });
            }
            else
            {
                lista.Items.Add(tekst);
                lista.SelectedIndex = lista.Items.Count - 1;
            }
            
        }
        delegate void SetTextHTMLCallBack(string tekst);
        private void SetTextHTML(string tekst)
        {
            if (webBrowserChat.InvokeRequired)
            {
                SetTextHTMLCallBack f = new SetTextHTMLCallBack(SetTextHTML);
                this.Invoke(f, new object[] { tekst });
            }
            else
            {
                this.webBrowserChat.Document.Write(tekst);
            }
        }
        delegate void SetScrollCallBack();
        private void SetScroll()
        {
            if (webBrowserChat.InvokeRequired)
            {
                SetScrollCallBack f = new SetScrollCallBack(SetScroll);
                this.Invoke(f);
            }
            else
            {
                //this.webBrowserChat.Document.Window.ScrollTo(1, int.MaxValue);
                webBrowserChat.Document.Body.All[webBrowserChat.Document.Body.All.Count - 1].ScrollIntoView(false);
            }
        }

        delegate void OdblokujButtonCallBack(Button but);
        private void OdblokujButton(Button but)
        {
            if (webBrowserChat.InvokeRequired)
            {
                OdblokujButtonCallBack f = new OdblokujButtonCallBack(OdblokujButton);
                this.Invoke(f, new object[] { but });
            }
            else
            {
                but.Enabled = true;
            }
        }
        //delegate void RemoveTextCallBack(int i);
        //private void RemoveText(int i)
        //{
        //    if (listBoxUsers.InvokeRequired)
        //    {
        //        RemoveTextCallBack f = new RemoveTextCallBack(RemoveText);
        //        this.Invoke(f, new object[] { i });
        //    }
        //    else
        //    {
        //        listBoxUsers.Items.RemoveAt(i);
        //    }
        //}
        #endregion

        private void AddText(string who, string message)
        {
            SetTextHTML("<table><tr><td width=\"10%\"><b>[" + who + "]: </ b ></ td > ");

            SetTextHTML("<td colspan=2>" + message + "</td></tr></table>");
            SetScroll();
        }

        private void backgroundWorkerMainThread_DoWork(object sender, DoWorkEventArgs e)
        {
            UdpClient client = new UdpClient(2500);
            IPEndPoint addressIP = new IPEndPoint(IPAddress.Parse(addressIPServer), 0);
            //MessageBox.Show(IPAddress.Parse(addressIPServer).ToString());
            string message = "";
            while (!backgroundWorkerMainThread.CancellationPending)
            {
                Byte[] bufor = client.Receive(ref addressIP);
                string data = Encoding.UTF8.GetString(bufor);
                string[] cmd = data.Split(new char[] { ':' });
                if (cmd[1] == "BYE")
                {
                    AddText("system", "klient odłączony");
                    client.Close();
                    OdblokujButton(buttonConnect);
                    return;
                }
                if (cmd.Length > 2)
                {
                    message = cmd[2];
                    for (int i = 3; i < cmd.Length; i++)
                        message += ":" + cmd[i];
                }
                if(cmd[1] == "SAY")
                {
                    AddText(cmd[0], message);
                }
                else if (cmd[1] == "Lista_uzytkownikow")
                {
                    //listBoxUsers.Items.Clear();
                    foreach (string us in cmd[2].Split(new char[] { ';' }))
                        SetText(listBoxUsers, us);

                    MessageBox.Show(cmd[0]+cmd[1]+cmd[2]);
                }
                else
                {
                    MessageBox.Show("blad");
                }
                
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxNick.Text != String.Empty)
                {
                    client = new TcpClient(addressIPServer, Convert.ToInt32(ConfigurationManager.AppSettings["port"]));
                    textBoxNick.ReadOnly = true;
                    NetworkStream ns = client.GetStream();
                    write = new BinaryWriter(ns);
                    write.Write(textBoxNick.Text + ":HI:" + "pusty");
                    BinaryReader read = new BinaryReader(ns);
                    string answer = read.ReadString();
                    if (answer == "HI")
                    {
                        backgroundWorkerMainThread.RunWorkerAsync();
                        isActive = true;
                        buttonConnect.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Serwer odmawia nawiązania połączenia");
                        buttonConnect.Enabled = true;
                        client.Close();
                    }
                }
                else
                    MessageBox.Show("Wpisz swój nick");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie można nawiązać połączenia " + ex.Message);
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (isActive && textBoxMessage.Text != String.Empty)
                write.Write(textBoxNick.Text + ":SAY:" + textBoxMessage.Text);
            textBoxMessage.Text = String.Empty;
        }

        private void textBoxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonSend_Click(sender, null);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (write != null)
            {
                try
                {
                    write.Write(textBoxNick.Text + ":BYE:" + "pusty");
                    write.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Błąd");
                }
            }
            if (backgroundWorkerMainThread.IsBusy)
                //backgroundWorkerMainThread.CancelAsync();
            if (client != null)
                client.Close();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

