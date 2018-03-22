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
using System.Collections;
using System.IO;

namespace ChatSerwer
{
    public partial class Form1 : Form
    {
        private TcpListener server;
        private TcpClient client;
        private ArrayList clientsList;
        private ArrayList namesClients;
        private bool isServerActive;
        public Form1()
        {
            InitializeComponent();
            clientsList = new ArrayList();
            namesClients = new ArrayList();
            isServerActive = false;
            webBrowserChat.Document.Write("<html><head><style>body,table { font - size: 10pt; font - family: Verdana; margin: 3px 3px 3px 3px; font - color: black; }</ style ></ head >< body width =\"" + (webBrowserChat.ClientSize.Width - 20).ToString() + "\">");
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
                this.webBrowserChat.Document.Window.ScrollTo(1, int.MaxValue);
            }
        }
        delegate void RemoveTextCallBack(int i);
        private void RemoveText(int i)
        {
            if (listBoxUsers.InvokeRequired)
            {
                RemoveTextCallBack f = new RemoveTextCallBack(RemoveText);
                this.Invoke(f, new object[] { i });
            }
            else
            {
                listBoxUsers.Items.RemoveAt(i);
            }
        }

        delegate void ZwrocCallBack();
        private void Zwroc()
        {
            if (listBoxUsers.InvokeRequired)
            {
                ZwrocCallBack f = new ZwrocCallBack(Zwroc);
                this.Invoke(f);
            }
            else
            {
                string lista = null;
                foreach (string us in listBoxUsers.Items)
                {
                    lista += us + ";";      
                }
                lista = lista.Remove(lista.Count() - 1);
                MessageBox.Show(lista+"\n"+ lista.Remove(lista.Count() - 1));
                SendUdpMessage("administrator:Lista_uzytkownikow:" + lista);
            }
        }
        #endregion

        private void AddText(string who, string message)
        {
            //SetTextHTML("<table><tr><td width=\"10%\"><b>[" + who + "]: </ b ></ td > ");
            SetText(listBoxMessage,"[" + who + "]:  " + message);
            //SetTextHTML("<td colspan=2>" + message + "</td></tr></table>");
            //SetScroll();
        }
        private void SendUdpMessage(string message)
        {
            foreach (string user in listBoxUsers.Items)
            {
                try
                {
                    using (UdpClient klientUDP = new UdpClient(user, 2500))
                    {
                        byte[] bufor = Encoding.UTF8.GetBytes(message);
                        klientUDP.Send(bufor, bufor.Length);
                    }
                }
                catch (Exception)
                { }
            }
               
        }

        private void backgroundWorkerMainLoop_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                server.Start();
                SetText(listBoxServer, "Serwer oczekuje na połączenia ...");
                while (true)
                {
                    client = server.AcceptTcpClient();
                    SetText(listBoxServer, "Klient podłączony");
                    NetworkStream ns = client.GetStream();
                    BinaryReader read = new BinaryReader(ns);
                    string data = read.ReadString();
                    string[] cmd = data.Split(new char[] { ':' });
                    if (cmd[1] == "HI")
                    {
                        BinaryWriter write = new BinaryWriter(ns);
                        if (namesClients.BinarySearch(cmd[0]) > -1)
                        {
                            write.Write("ERROR:Użytkownik o podanej nazwie już istnieje");
                        }
                        else
                        {
                            write.Write("HI");
                            BackgroundWorker clientThread = new BackgroundWorker();
                            clientThread.WorkerSupportsCancellation = true;
                            clientThread.DoWork += new DoWorkEventHandler(clientThread_DoWork);
                            namesClients.Add(cmd[0]);
                            //clientsList.Add(cmd[0]);
                            clientsList.Add(clientThread);
                            clientThread.RunWorkerAsync();
                            System.Threading.Thread.Sleep(100);
                            SendUdpMessage("administrator:SAY:Użytkownik " + cmd[0] +" dołączył do rozmowy");
                            //MessageBox.Show(listBoxUsers.Text);
                            //System.Threading.Thread.Sleep(1000);
                            Zwroc();
                            //SendUdpMessage("administrator:ADD:");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Klient nie dokonał autoryzacji");
                        isServerActive = false;
                        client.Close();
                    }
                }
            }
            catch
            {
                isServerActive = false;
                server.Stop();
                SetText(listBoxServer, "Połączenie przerwane");
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!isServerActive)
                try
                {
                    server = new TcpListener(IPAddress.Parse(comboBoxIpAddress.Text), (int)numericUpDownPort.Value);
                    //server = new TcpListener(IPAddress.Parse("127.0.0.1"), 6969);
                    backgroundWorkerMainLoop.RunWorkerAsync();
                    isServerActive = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd inicjacji serwera (" + ex.Message + ")");
                }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (isServerActive)
            {
                SendUdpMessage("administrator:SAY:Serwer zostanie wyłączony");
                if (client != null) client.Close();
                server.Stop();
                listBoxServer.Items.Add("Serwer wyłączony");
                listBoxUsers.Items.Clear();
                namesClients.Clear();
                clientsList.Clear();
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (textBoxMessage.Text != String.Empty && textBoxMessage.Text.Trim()!= String.Empty)
                {
                AddText("administrator", textBoxMessage.Text);
                if (isServerActive) SendUdpMessage("administrator:SAY:" + textBoxMessage.Text);
                textBoxMessage.Clear();
            }
        }

        private void textBoxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) buttonSend_Click(this, null);
        }

        void clientThread_DoWork(object sender, DoWorkEventArgs e)
        {
            IPEndPoint IP = (IPEndPoint)client.Client.RemoteEndPoint;
            SetText(listBoxUsers, IP.Address.ToString());
            SetText(listBoxServer, "Klient [" + IP.Address.ToString() + "] uwieżytelniony");
            
            NetworkStream ns = client.GetStream();
            BinaryReader read = new BinaryReader(ns);
            string[] cmd = null;
            BackgroundWorker bw = (BackgroundWorker)sender;
            try
            {
                while ((cmd = read.ReadString().Split(new char[] { ':' }))[1] != "BYE" && bw.CancellationPending == false)
                {
                    string message = null;
                    if (cmd.Length > 2)
                    {
                        message = cmd[2];
                        for (int i = 3; i < cmd.Length; i++)
                            message += ":" + cmd[i];
                    }
                    switch (cmd[1])
                    {
                        case "SAY":
                            AddText(cmd[0], message);
                            SendUdpMessage(cmd[0] + ":" + cmd[1] + ":" + message);
                            break;
                    }
                }
                SetText(listBoxServer, "Użytkownik [" + cmd[0] + "] opuścił serwer");
                for (int i = 0; i < listBoxUsers.Items.Count; i++)
                    if (IP.Address.ToString() == listBoxUsers.Items[i].ToString())
                    {
                        RemoveText(i);
                        namesClients.RemoveAt(i);
                        clientsList.RemoveAt(i);
                    }
                SendUdpMessage("administrator:SAY:Użytkownik " + cmd[0] + " opuścił rozmowę");
               
                read.Close();
                ns.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            int index = listBoxUsers.SelectedIndex;
            using (UdpClient clientUdp = new UdpClient(listBoxUsers.Items[index].ToString(), 2500))
            {
                byte[] buff = Encoding.UTF8.GetBytes("administrator:SAY:Zostałeś odłączony");
               
                clientUdp.Send(buff, buff.Length);
                byte[] bufor2 = Encoding.UTF8.GetBytes("administrator:BYE:pusty");
                clientUdp.Send(bufor2, bufor2.Length);
            }
            listBoxServer.Items.Add("Klient [" + listBoxUsers.Items[index].ToString() + "] rozłączony");
            ((BackgroundWorker)clientsList[index]).CancelAsync();
            SendUdpMessage("administrator:SAY:Użytkownik " + listBoxUsers.Items[index].ToString() + " został odłączony");
            listBoxUsers.Items.RemoveAt(index);
            clientsList.RemoveAt(index);
            namesClients.RemoveAt(index);
        }
    }
}

