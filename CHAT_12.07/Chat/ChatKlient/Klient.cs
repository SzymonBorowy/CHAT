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
using System.Collections;

namespace ChatKlient
{
    public partial class Klient : Form
    {
        private TcpClient client;
        private string addressIPServer = null; //= "192.168.1.164";
        private BinaryWriter write;
        private bool isActive = false;
        private ArrayList namesClients;
        private List<Button> listaButton = new List<Button>();


        private string[,] plansza = new string[3,3];

        public string wynik = null;
        public Klient()
        {
            
            InitializeComponent();
            addressIPServer = ConfigurationManager.AppSettings["ip"];
            buttonDisconnect.Enabled = false;
            namesClients = new ArrayList();

            this.tabPageGra.Parent = null; // hide    



        }

        #region obsluga

        delegate void OperacjeNaButtonCallBack(Button buton, string tekst, string operacja);
        private void OperacjeNaButton(Button buton, string tekst, string operacja)
        {
            if (buton.InvokeRequired)
            {
                OperacjeNaButtonCallBack f = new OperacjeNaButtonCallBack(OperacjeNaButton);
                this.Invoke(f, new object[] { buton, tekst, operacja});
            }
            else
            {
                switch(operacja)
                {
                    case "odblokuj":
                        buton.Enabled = true;
                        break;
                    case "zablokuj":
                        buton.Enabled = false;
                        break;
                    case "tekst":
                        buton.Text = tekst;
                        break;
                    default:
                        break;
                }
                
            }
        }

        delegate void OperacjeNaListaCallBack(ListBox lista, string tekst, string operacja);
        private void OperacjeNaLista(ListBox lista, string tekst, string operacja)
        {
            if (lista.InvokeRequired)
            {
                OperacjeNaListaCallBack f = new OperacjeNaListaCallBack(OperacjeNaLista);
                this.Invoke(f, new object[] { lista, tekst, operacja });
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
                        lista.Items.Remove(lista.SelectedIndex);
                        break;
                    default:
                        break;
                }

            }
        }

        delegate void OperacjeNaTextBoxCallBack(TextBox txt, string operacja);
        private void OperacjeNaTextBox(TextBox txt, string operacja)
        {
            if (txt.InvokeRequired)
            {
                OperacjeNaTextBoxCallBack f = new OperacjeNaTextBoxCallBack(OperacjeNaTextBox);
                this.Invoke(f, new object[] { txt, operacja });
            }
            else
            {
                switch (operacja)
                {
                    case "odblokuj":
                        txt.Enabled = true;
                        break;
                    case "zablokuj":
                        txt.Enabled = false;
                        break;
                    default:
                        break;
                }

            }
        }

        delegate void OperacjeNaFormCallBack(Form forma, string tekst, string operacja);
        private void OperacjeNaForm(Form forma, string tekst, string operacja)
        {
            if (forma.InvokeRequired)
            {
                OperacjeNaFormCallBack f = new OperacjeNaFormCallBack(OperacjeNaForm);
                this.Invoke(f, new object[] { forma, tekst, operacja });
            }
            else
            {
                switch (operacja)
                {
                    case "zb1":
                        break;
                    case "zablokuj":
                        
                        break;
                    default:
                        break;
                }

            }
        }

        delegate void OperacjeNaTabPageCallBack(TabPage tabpage, TabControl tabcontrol, string operacja);
        private void OperacjeNaTabPage(TabPage tabpage, TabControl tabcontrol, string operacja)
        {
            if (tabcontrol.InvokeRequired)
            {
                OperacjeNaTabPageCallBack f = new OperacjeNaTabPageCallBack(OperacjeNaTabPage);
                this.Invoke(f, new object[] { tabpage, tabcontrol, operacja });
            }
            else
            {
                switch (operacja)
                {
                    case "odblokuj":
                        tabpage.Parent = tabcontrol;
                        tabpage.Select();
                        break;
                    case "zablokuj":
                        //tabpage.Dispose();
                        tabpage.Parent = null; // hide 
                        break;
                    default:
                        break;
                }

            }
        }

        delegate void OperacjeNaLabelCallBack(Label label, string tekst, string operacja);
        private void OperacjeNaLabel(Label label, string tekst, string operacja)
        {
            if (label.InvokeRequired)
            {
                OperacjeNaLabelCallBack f = new OperacjeNaLabelCallBack(OperacjeNaLabel);
                this.Invoke(f, new object[] { label, tekst, operacja });
            }
            else
            {
                switch (operacja)
                {
                    case "tekst":
                        label.Text = tekst;
                        break;
                    case "zablokuj":

                        break;
                    default:
                        break;
                }

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

        private void AddText(string who, string message, ListBox lista)
        {
            OperacjeNaLista(lista, "[" + who + "]:  " + message, "dodaj");
        }

        private void backgroundWorkerMainThread_DoWork(object sender, DoWorkEventArgs e)
        {
            UdpClient client = new UdpClient(2500);
            IPEndPoint addressIP = new IPEndPoint(IPAddress.Parse(addressIPServer), 0);
            string message = "";
            while (!backgroundWorkerMainThread.CancellationPending)
            {
                Byte[] bufor = client.Receive(ref addressIP);
                string data = Encoding.UTF8.GetString(bufor);
                string[] cmd = data.Split(new char[] { ':' });
                if (cmd.Length > 2)
                {
                    message = cmd[2];
                    for (int i = 3; i < cmd.Length; i++)
                        message += ":" + cmd[i];
                }

                if (cmd[1] == "BYE")
                {
                    AddText("system", "klient odłączony", listBoxMessage);
                    client.Close();
                    OperacjeNaButton(buttonConnect, null, "odblokuj");
                    OperacjeNaButton(buttonDisconnect, null, "zablokuj");
                    OperacjeNaTextBox(textBoxNick, "odblokuj");
                    OperacjeNaTextBox(textBoxMessage, "zablokuj");
                    OperacjeNaLista(listBoxUsers, null, "wyczysc");
                    return;
                }
                else if (cmd[1] == "SAY")
                {
                    AddText(cmd[0], message, listBoxMessage);
                }
                else if (cmd[1] == "Lista_uzytkownikow")
                {
                    namesClients.Clear();
                    ArrayList tab1 = new ArrayList();
                    ArrayList tab2 = new ArrayList();
                    OperacjeNaLista(listBoxUsers, null, "wyczysc");
                    int index = 0;
                    foreach (string us in cmd[2].Split(new char[] { ';' }))
                    {
                        if(index<=(cmd[2].Split(new char[] { ';' }).Count()/2-1))
                        {
                            tab1.Add(us);
                            //OperacjeNaLista(listBoxUsers, us, "dodaj");
                        }
                        else
                        {
                            tab2.Add(us);
                            //namesClients.Add(us);
                        }
                        index++;
                    }
                    for(int i=0;i< cmd[2].Split(new char[] { ';' }).Count() / 2;i++)
                    {
                        OperacjeNaLista(listBoxUsers, tab1[i]+" -> "+tab2[i], "dodaj");
                    }
                        
                }
                else if (cmd[1] == "start_game")
                {
                    AddText(cmd[0], message, listBoxMessage);
                    string[] tab = message.Split(new char[] { ';' });
                    StartGame(tab[1], tab[2], tab[3]);
                }
                else if (cmd[1] == "gra")
                {
                    OperacjeNaLabel(labelRuch, "Twój ruch", "tekst");
                    //AddText(cmd[0], message, listBoxMessage);
                    string[] tab1 = message.Split(new char[] { '|' });
                    foreach(Button but in listaButton)
                    {
                        if(tab1[1] == but.Name)
                        {
                            OperacjeNaButton(but, null, "zablokuj");
                            OperacjeNaButton(but, tab1[0], "tekst");
                            string[] pozycja = but.Tag.ToString().Split(new char[] { ';' });
                            plansza[Convert.ToInt32(pozycja[0]), Convert.ToInt32(pozycja[1])] = but.Text;
                            break;
                        }
                    }
                    sprawdzCzyWygrana(labelZnak.Text);
                   
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
                   // textBoxNick.ReadOnly = true;
                    NetworkStream ns = client.GetStream();
                    write = new BinaryWriter(ns);
                    write.Write(textBoxNick.Text + ":HI:" + "pusty");
                    BinaryReader read = new BinaryReader(ns);
                    string answer = read.ReadString();
                    if (answer == "HI")
                    {
                        backgroundWorkerMainThread.RunWorkerAsync();
                        isActive = true;
                        OperacjeNaButton(buttonConnect, null, "zablokuj");
                        OperacjeNaButton(buttonDisconnect, null, "odblokuj");
                        OperacjeNaTextBox(textBoxNick, "zablokuj");
                        OperacjeNaTextBox(textBoxMessage, "odblokuj");
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
            {
                if(listBoxUsers.SelectedIndex == -1)
                {
                    write.Write(textBoxNick.Text + ":SAY:" + textBoxMessage.Text);
                }
                else
                {
                    string listaWybranychOsob = null;
                    foreach (string os in listBoxUsers.SelectedItems)
                    {
                        //MessageBox.Show(os.Remove(os.IndexOf(" ")));
                        listaWybranychOsob += os.Remove(os.IndexOf(" ")) + ";";
                        OperacjeNaLista(listBoxMessage, "[" + textBoxNick.Text + "] to [" + os + "] send: " + textBoxMessage.Text, "dodaj");
                    }
                    //MessageBox.Show(listaWybranychOsob);
                    write.Write(textBoxNick.Text + ":wyslij_do_wybranych_osob:" + listaWybranychOsob + " " + textBoxMessage.Text);
                }
            }
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
                backgroundWorkerMainThread.CancelAsync();
                if (client != null)
                    client.Close();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            Close();
            OperacjeNaButton(buttonConnect, null, "odblokuj");
            OperacjeNaButton(buttonDisconnect, null, "zablokuj");
        }

        

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedItems.Count == 1)
            {
                if(listBoxUsers.SelectedItem.ToString().Remove(0, listBoxUsers.SelectedItem.ToString().IndexOf(" ")+3).TrimStart().TrimEnd() !=  textBoxNick.Text.TrimStart().TrimEnd())
                {
                    write.Write(textBoxNick.Text + ":gra_start:" + listBoxUsers.SelectedItem.ToString().Remove(listBoxUsers.SelectedItem.ToString().IndexOf(" ")));
                    
                }
                else
                {
                    MessageBox.Show("Nie możesz grać sam ze sobą !");
                }
            }
            else
            {
                MessageBox.Show("Wybierz jednego uzytkownika");
            }
        }
        private void StartGame(string przeciwnik, string znak, string ruch)
        {
            OperacjeNaButton(buttonStartGame, null, "zablokuj");
            znak = znak.TrimStart();
            if (ruch == "1") OperacjeNaLabel(labelRuch, "Twój ruch", "tekst");
            else  OperacjeNaLabel(labelRuch, "Ruch przeciwnika", "tekst");
            OperacjeNaTabPage(tabPageGra, tabControl1, "odblokuj");
            listaButton.Add(button1);
            listaButton.Add(button2);
            listaButton.Add(button3);
            listaButton.Add(button4);
            listaButton.Add(button5);
            listaButton.Add(button6);
            listaButton.Add(button7);
            listaButton.Add(button8);
            listaButton.Add(button9);
            foreach(Button but in listaButton)
            {
                but.Click += Button_Click;
                OperacjeNaButton(but, "", "tekst");
                OperacjeNaButton(but, null, "odblokuj");
            }
            OperacjeNaLabel(labelZnak, znak, "tekst");
            OperacjeNaLabel(labelPrzeciwnik, przeciwnik, "tekst");
            for(int i =0;i<3;i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.plansza[i,j] = " ";
                }
            }
                
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if(labelRuch.Text == "Twój ruch")
            {
                var button = (Button)sender;
                string[] pozycja = button.Tag.ToString().Split(new char[] { ';' });
                wyslijbuttona(labelZnak.Text, button.Name);
                OperacjeNaButton(button, labelZnak.Text, "tekst");
                OperacjeNaButton(button, null, "zablokuj");
                plansza[Convert.ToInt32(pozycja[0]),Convert.ToInt32(pozycja[1])] = button.Text;
                sprawdzCzyWygrana(labelZnak.Text);
            }
            else MessageBox.Show("RUCH PRZECIWNIKA !!!");

        }

        private void sprawdzCzyWygrana(string twojZnak)
        {
            string znak = null;
            //poziomo
            for (int i = 0; i < 3; i++)
            {
                if(plansza[i,0]+plansza[i,1]+plansza[i,2]=="XXX")
                {
                    znak = "X";
                }
                else if (plansza[i, 0] + plansza[i, 1] + plansza[i, 2] == "OOO")
                {
                    znak = "O";
                }
            }
            //pionowo
            for (int i = 0; i < 3; i++)
            {
                if (plansza[0, i] + plansza[1, i] + plansza[2, i] == "XXX")
                {
                    znak = "X";
                }
                else if (plansza[0, i] + plansza[1, i] + plansza[2, i] == "OOO")
                {
                    znak = "O";
                }
            }

            //skośnie
            if (plansza[0, 0] + plansza[1, 1] + plansza[2, 2] == "XXX")
            {
                znak = "X";
            }
            else if (plansza[0, 0] + plansza[1, 1] + plansza[2, 2] == "OOO")
            {
                znak = "O";
            }
            else if(plansza[0, 2] + plansza[1, 1] + plansza[2, 0] == "XXX")
            {
                znak = "X";
            }
            else if (plansza[0, 2] + plansza[1, 1] + plansza[2, 0] == "OOO")
            {
                znak = "O";
            }
            //remis
            string tekst = null;
            foreach(var i in plansza)
            {
                tekst += i.Trim();
            }
            if (twojZnak == znak) { MessageBox.Show("Wygrałeś", "Wynik"); zamknijPage(tabPageGra); }
            else if (tekst.Length == 9) { MessageBox.Show("Remis", "Wynik"); zamknijPage(tabPageGra); }
            else if ((twojZnak == "X" && znak == "O") || (twojZnak == "O" && znak == "X")) { MessageBox.Show("Przegrałeś", "Wynik"); zamknijPage(tabPageGra); }

        }

        private void zamknijPage(TabPage tabPage)
        {
            OperacjeNaTabPage(tabPage, tabControl1, "zablokuj");
            OperacjeNaButton(buttonStartGame, null, "odblokuj");
            foreach (Button but in listaButton)
            {
                but.Click -= Button_Click;
            }
            listaButton.Clear();
            write.Write(textBoxNick.Text + ":gra_koniec:" + labelPrzeciwnik.Text);
        }

        public void wyslijbuttona(string znak,string nazwa)
        {
            write.Write(textBoxNick.Text + ":gra:" + labelPrzeciwnik.Text + ";" + znak +"|"+nazwa);
            OperacjeNaLabel(labelRuch, "Ruch przecwinika", "tekst");
        }

    }
}
/*do zrobienia 
 +* uruchamiając grą trzeba wystać znaki 
 +* po stronie servera musi odbywać się dzielenie znaków 
 -/+* możliwość odpalenia kilku gier z różnymi użytkownikami 
 -* nazwa gry powinna zwierać to pomiędzy kim jest rozgrywana 
 +* większy opis w trakcie gry 
 +* sprawdzanie kto wygrał
 +* zamykanie tabpage po wyświetleniu komunikatu o wygranej
 +* przebudownie switcha gry 
 +* dodanie remisu
 +* zastanowieni się czy to nie powinno byc po stronie serwera raczej nie 
 +* wyświetlanie komunikatu o wygranej 
 */
