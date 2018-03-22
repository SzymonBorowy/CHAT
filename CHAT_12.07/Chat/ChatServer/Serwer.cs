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
using System.Collections;
using System.IO;
using System.Net;

namespace ChatServer
{
    public partial class Serwer : Form
    {
        private TcpListener server;
        private TcpClient client;
        private ArrayList clientsList;
        private ArrayList namesClients;
        private bool isServerActive;
        //private string IP;
        public Serwer()
        {
            InitializeComponent();
            clientsList = new ArrayList();
            namesClients = new ArrayList();
            isServerActive = false;
            //IP = null;
            for (int i = 1; i <= 5; i++)
            {
                clientsList.Add(i);
                namesClients.Add(i + ".1");
                listBoxUsers.Items.Add(i + " -> " + i + ".1");
            }
        }
        #region obsluga

        delegate void OperacjeNaButtonCallBack(Button buton, string operacja);
        private void OperacjeNaButton(Button buton, string operacja)
        {
            if (buton.InvokeRequired)
            {
                OperacjeNaButtonCallBack f = new OperacjeNaButtonCallBack(OperacjeNaButton);
                this.Invoke(f, new object[] { buton, operacja });
            }
            else
            {
                switch (operacja)
                {
                    case "odblokuj":
                        buton.Enabled = true;
                        break;
                    case "zablokuj":
                        buton.Enabled = false;
                        break;
                    default:
                        break;
                }
            }
        }

        delegate void OperacjeNaListaCallBack(ListBox lista, string tekst, int numer, string operacja);
        private void OperacjeNaLista(ListBox lista, string tekst, int numer, string operacja)
        {
            if (lista.InvokeRequired)
            {
                OperacjeNaListaCallBack f = new OperacjeNaListaCallBack(OperacjeNaLista);
                this.Invoke(f, new object[] { lista, tekst, numer, operacja });
            }
            else
            {
                switch (operacja)
                {
                    case "dodaj":
                        lista.Items.Add(tekst);
                        lista.TopIndex = lista.Items.Count - 1;
                        break;
                    case "wyczysc":
                        lista.Items.Clear();
                        break;
                    case "usun":
                        lista.Items.RemoveAt(numer);
                        break;
                    case "uzytkownicy":
                        string txt = null;
                        foreach (string us in lista.Items)
                        {
                            txt += us.Remove(us.IndexOf(" ")) + ";";
                        }
                        foreach (var name in namesClients)
                        {
                            txt += name + ";";
                        }
                        txt = txt.Remove(txt.Count() - 1);
                        SendUdpMessage("administrator:Lista_uzytkownikow:" + txt);
                        break;
                    default:
                        break;
                }
                lista.Refresh();
            }
        }
        
        #endregion
        private void AddText(string who, string message, ListBox lista)
        {
            OperacjeNaLista(lista, "[" + who + "]:  " + message, -1, "dodaj");
        }
        private void SendUdpMessage(string message)
        {
            foreach (string user in listBoxUsers.Items)
            {
                try
                {
                    using (UdpClient klientUDP = new UdpClient(user.Remove(user.IndexOf(" ")), 2500))
                    {
                        byte[] bufor = Encoding.UTF8.GetBytes(message);
                        klientUDP.Send(bufor, bufor.Length);
                    }
                }
                catch (Exception)
                { }
            }
        }

        private void SendUdpMessageToOneUser(string message, string user)
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

        private void backgroundWorkerMainLoop_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                server.Start();
                OperacjeNaLista(listBoxServer, "Serwer oczekuje na połączenia ...", -1, "dodaj");
                while (true)
                {
                    client = server.AcceptTcpClient();
                    OperacjeNaLista(listBoxServer, "Klient podłączony", -1, "dodaj");
                    NetworkStream ns = client.GetStream();
                    BinaryReader read = new BinaryReader(ns);
                    string data = read.ReadString();
                    string[] cmd = data.Split(new char[] { ':' });
                    if (cmd[1] == "HI")
                    {
                        BinaryWriter write = new BinaryWriter(ns);
                        if (namesClients.IndexOf(cmd[0]) > -1)
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
                            clientsList.Add(clientThread);
                            clientThread.RunWorkerAsync();
                            System.Threading.Thread.Sleep(1000);
                            SendUdpMessage("administrator:SAY:Użytkownik " + cmd[0] + " dołączył do rozmowy");
                            System.Threading.Thread.Sleep(1000);
                            OperacjeNaLista(listBoxUsers, null, -1, "uzytkownicy");
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
            catch (Exception ex)
            {
                isServerActive = false;
                server.Stop();
                OperacjeNaLista(listBoxServer, "Połączenie przerwane", -1, "dodaj");
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!isServerActive)
                try
                {
                    server = new TcpListener(IPAddress.Parse(comboBoxIpAddress.Text), (int)numericUpDownPort.Value);
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
                OperacjeNaLista(listBoxServer, "Serwer wyłączony", -1, "dodaj");
                OperacjeNaLista(listBoxUsers, null, -1, "wyczysc");
                namesClients.Clear();
                clientsList.Clear();
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (textBoxMessage.Text != String.Empty && textBoxMessage.Text.Trim() != String.Empty)
            {
                if(listBoxUsers.SelectedIndex ==-1)
                {
                    AddText("administrator", textBoxMessage.Text, listBoxMessage);
                    if (isServerActive) SendUdpMessage("administrator:SAY:" + textBoxMessage.Text);
                }
                else
                {
                    foreach (string os in listBoxUsers.SelectedItems)
                    {
                        AddText("administrator", " to " + os.Remove(os.IndexOf(" ")) + ": " + textBoxMessage.Text, listBoxMessage);
                        if (isServerActive) SendUdpMessageToOneUser("administrator:SAY:to you:" + textBoxMessage.Text, os.Remove(os.IndexOf(" ")));
                    }
                }
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
            OperacjeNaLista(listBoxUsers, IP.Address.ToString() +" -> "+ namesClients[namesClients.Count-1], -1, "dodaj");
            OperacjeNaLista(listBoxServer, "Klient [" + IP.Address.ToString() + "] uwieżytelniony", -1, "dodaj");
            List<string> listaUzytkownikowZajetych = new List<string>();
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
                            AddText(cmd[0], message, listBoxMessage);
                            SendUdpMessage(cmd[0] + ":" + cmd[1] + ":" + message);
                            break;
                        case "wyslij_do_wybranych_osob":
                            string[] osoby = message.Split(new char[] { ';' });
                            for (int i = 0; i < osoby.Count()-1; i++)
                            {
                                AddText(cmd[0], " to [" + osoby[i] + "]: " + osoby[osoby.Count()-1], listBoxMessage);
                                SendUdpMessageToOneUser(cmd[0] + ":SAY:to you:" + osoby[osoby.Count() - 1], osoby[i]);
                                listaUzytkownikowZajetych.Add(osoby[i]);
                            }
                            break;
                        case "gra_start":
                            Random rand = new Random();
                            if(listaUzytkownikowZajetych.Contains(message) == false && listaUzytkownikowZajetych.Contains(IP.Address.ToString()) == false )
                            {
                                listaUzytkownikowZajetych.Add(message);
                                listaUzytkownikowZajetych.Add(IP.Address.ToString());
                                int wynik = rand.Next() % 2;
                                if (wynik == 0)
                                {//ty+przeciwnik+znak+ruch
                                    SendUdpMessageToOneUser(cmd[0] + ":start_game:" + message + ";" + IP.Address.ToString() + ";X;1", message);
                                    SendUdpMessageToOneUser(cmd[0] + ":start_game:" + IP.Address.ToString() + ";" + message + ";O;0", IP.Address.ToString());
                                    AddText("server.gra", IP.Address.ToString() + " [O] vs. " + message + " [X]", listBoxMessage);
                                }

                                else
                                {
                                    SendUdpMessageToOneUser(cmd[0] + ":start_game:" + message + ";" + IP.Address.ToString() + ";O;0", message);
                                    SendUdpMessageToOneUser(cmd[0] + ":start_game:" + IP.Address.ToString() + ";" + message + ";X;1", IP.Address.ToString());
                                    AddText("server.gra", IP.Address.ToString() + " [X] vs. " + message + " [O]", listBoxMessage);
                                }
                            }
                            else
                            {
                                SendUdpMessageToOneUser(cmd[0] + ":SAY:" + "uzytkownik "+ message + " zajety ", IP.Address.ToString());
                            }
                            
                            break;
                        case "gra":
                            string[] wiadomosc = message.Split(new char[] { ';' });
                            SendUdpMessageToOneUser(cmd[0] + ":gra:"+wiadomosc[1], wiadomosc[0]);
                            AddText("server.gra", cmd[0] + ":gra:" + wiadomosc[1]+" to: "+ wiadomosc[0], listBoxMessage);
                            break;
                        case "gra_koniec":
                            AddText("server.gra", "KONIEC GRY :" + IP.Address.ToString() + " [X] vs. " + message + " [O]", listBoxMessage);
                            listaUzytkownikowZajetych.Remove(IP.Address.ToString());
                            listaUzytkownikowZajetych.Remove(message);
                            break;
                    }
                }
                for (int i = 0; i < listBoxUsers.Items.Count; i++)
                    if (IP.Address.ToString() == listBoxUsers.Items[i].ToString().Remove(listBoxUsers.Items[i].ToString().IndexOf(" ")))
                    {
                        OperacjeNaLista(listBoxServer, "Użytkownik [" + IP.Address.ToString()+" -> "+ cmd[0] + "] opuścił serwer", -1, "dodaj");
                        SendUdpMessage("administrator:SAY:Użytkownik " + IP.Address.ToString() + " -> " + cmd[0] + " opuścił rozmowę");
                        OperacjeNaLista(listBoxUsers, null, i, "usun");
                        namesClients.RemoveAt(i);
                        clientsList.RemoveAt(i);
                        ((BackgroundWorker)clientsList[i]).CancelAsync();

                        OperacjeNaLista(listBoxUsers, null, -1, "uzytkownicy");
                    }
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
            while(listBoxUsers.SelectedIndex >-1)
            {
                try
                {
                    int index = listBoxUsers.SelectedIndex;
                    string ip = listBoxUsers.Items[index].ToString();
                    using (UdpClient clientUdp = new UdpClient(ip.Remove(ip.IndexOf(" ")), 2500))
                    {
                        byte[] buff = Encoding.UTF8.GetBytes("administrator:SAY:Zostałeś odłączony");

                        clientUdp.Send(buff, buff.Length);
                        byte[] bufor2 = Encoding.UTF8.GetBytes("administrator:BYE:pusty");
                        clientUdp.Send(bufor2, bufor2.Length);
                    }
                    OperacjeNaLista(listBoxServer, "Klient [" + listBoxUsers.Items[index].ToString() + "] rozłączony", -1, "dodaj");
                    ((BackgroundWorker)clientsList[index]).CancelAsync();
                    SendUdpMessage("administrator:SAY:Użytkownik " + listBoxUsers.Items[index].ToString() + " został odłączony");
                    OperacjeNaLista(listBoxUsers, null, index, "usun");
                    clientsList.RemoveAt(index);
                    namesClients.RemoveAt(index);
                    OperacjeNaLista(listBoxUsers, null, -1, "uzytkownicy");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //listBoxUsers.SetSelected(listBoxUsers.SelectedIndex, false);//sprawdzić dla dwóch klientów !!! 
            }
           
            
        }

    }
}
