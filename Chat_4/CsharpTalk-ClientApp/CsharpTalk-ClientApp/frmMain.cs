using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace CsharpTalk_ClientApp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        ConfigurationService cs = new ConfigurationService("settings.ini");

        private void frmMain_Load(object sender, EventArgs e)
        {
            Global.IsLoggedIn = false;
            txtLogin.Text = cs.GetStringValue("account", "login", "");
            txtPassword.Text = cs.GetStringValue("account", "password", "");
            checkBoxRemember.Checked = cs.GetBooleanValue("account", "remember", false);
            txtServerIP.Text = cs.GetStringValue("server", "ip", "127.0.0.1");
            txtServerPort.Text = cs.GetStringValue("server", "port", "1000");
            Global.ServerIP = txtServerIP.Text;
            Global.ServerPort = Convert.ToInt32(txtServerPort.Text);
            wbConversation.DocumentText += "<html><body style='font-family: Segoe UI; font-size: 9pt; margin-top: -5px; margin-left: -5px; margin-right: -5px'>";
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Global.IsLoggedIn)
                Disconnect();
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Enter)
            {
                base.OnKeyDown(e);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                if (Global.IsLoggedIn)
                    SendMessage();
                else
                    MessageBox.Show("Zaloguj się aby wysłać wiadomość...");
            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (txtLogin.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                btnLogIn_LogOut.Enabled = true;
            }
            else
            {
                btnLogIn_LogOut.Enabled = false;
            }
        }

        private void btnNewAccount_Click(object sender, EventArgs e)
        {
            new frmNewAccount().ShowDialog();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            new frmChangePassword(txtLogin.Text, txtPassword.Text).ShowDialog();
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            new frmDeleteAccount(txtLogin.Text, txtPassword.Text).ShowDialog();
        }

        string CurrentTime
        {
            get { return DateTime.Now.ToString("HH:mm:ss"); }
        }

        private void btnLogIn_LogOut_Click(object sender, EventArgs e)
        {
            if (btnLogIn_LogOut.Text == "Zaloguj")
            {
                Global.RememberCredentials = checkBoxRemember.Checked;
                cs.WriteValue("account", "remember", Global.RememberCredentials);

                Global.Login = txtLogin.Text;
                cs.WriteValue("account", "login", Global.RememberCredentials ? Global.Login : "");

                Global.Password = txtPassword.Text;
                cs.WriteValue("account", "password", Global.RememberCredentials ? Global.Password : "");

                Global.ServerIP = txtServerIP.Text;
                Global.ServerPort = Convert.ToInt32(txtServerPort.Text);

                txtLogin.Enabled = false;
                txtPassword.Enabled = false;
                checkBoxRemember.Enabled = false;
                btnLogIn_LogOut.Enabled = false;
                new LogInDelegate(LogIn).BeginInvoke(txtLogin.Text, txtPassword.Text, null, null); //  rozpoczynamy logowanie w nowym watku
            }
            else
            {
                Disconnect();
                txtLogin.Enabled = true;
                txtPassword.Enabled = true;
                checkBoxRemember.Enabled = true;
            }
        }

        delegate void LogInDelegate(string Login, string Password);
        void LogIn(string Login, string Password)
        {
            try
            {
                SetLoggingInStatus("Łączenie...");
                Global.tcpClient = new TcpClient();
                Global.tcpClient.Connect(Global.ServerIP, Global.ServerPort);
                Global.tcpStream = Global.tcpClient.GetStream();
                Global.tcpReader = new BinaryReader(Global.tcpStream);
                Global.tcpWriter = new BinaryWriter(Global.tcpStream);
                SetLoggingInStatus("Wysyłanie żądania...");

                Global.tcpWriter.Write("login"); // informujemy serwer ze chcemy sie zalogowac..
                Global.tcpWriter.Write(Global.Login); // .. a nastepnie wysylamy mu login oraz MD5 hasla
                Global.tcpWriter.Write(Global.GetHash(Global.Password));

                SetLoggingInStatus("Odpowiedź serwera...");
                string response = Global.tcpReader.ReadString();
                if (response == "OK") // to znaczy ze wszystko poszlo OK :)
                    LoginOK();
                else
                    LoginFailed(response); // w kazdym innym przypadku oznacza to ze cos poszlo nie tak.. (odpowiedz od serwera zawiera tresc bledu, wystarczy to wyswietlic)
            }
            catch (Exception exc)
            {
                LoginFailed(exc.Message);
            }
        }

        delegate void LoginOKDelegate();
        /// <summary>
        /// Metoda wywolywana jest kiedy poprawnie zalogowano na serwer.
        /// </summary>
        void LoginOK()
        {
            if (InvokeRequired)
            {
                Invoke(new LoginOKDelegate(LoginOK));
                return;
            }
            Global.IsLoggedIn = true;
            lstConnectedUsers.Enabled = true;
            btnLogIn_LogOut.Enabled = true;
            btnLogIn_LogOut.Text = "Wyloguj";
            lblStatus.Text = "Zalogowany";
            // blokujemy mozliwosc zmiany hasla, usuniecia konta i utworzenia nowego konta (dostepne tylko kiedy jestesmy niezalogowani)
            btnChangePassword.Enabled = false;
            btnDeleteAccount.Enabled = false;
            btnNewAccount.Enabled = false;

            Global.Timer = new System.Threading.Timer(Timer_Elapsed, null, 10000, 10000); // tworzymy nowy timer, ktory co 10sek bedzie wysylal do serwera pusta wiadomosc w celu utrzymania polaczenia. Jesli serwer po ustalonym czasie nie odbierze od nas zadnej wiadomosci, uzna polaczenie za martwe i nas rozlaczy.
            new Thread(new ThreadStart(Listen)).Start(); // w nowym watku zaczynamy nasluchiwac wiadomosci od serwera
        }

        delegate void LoginFailedDelegate(string Error);
        /// <summary>
        /// Metoda wywolywana jest kiedy wystapil blad podczas logowania.
        /// </summary>
        /// <param name="Error"></param>
        void LoginFailed(string Error)
        {
            if (InvokeRequired)
            {
                Invoke(new LoginFailedDelegate(LoginFailed), Error);
                return;
            }
            Disconnect();
            txtLogin.Enabled = true;
            txtPassword.Enabled = true;
            checkBoxRemember.Enabled = true;
            MessageBox.Show(Error, "Ups...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        delegate void SetLoggingInStatusDelegate(string Status);
        void SetLoggingInStatus(string Status)
        {
            if (InvokeRequired)
            {
                Invoke(new SetLoggingInStatusDelegate(SetLoggingInStatus), Status);
                return;
            }
            lblStatus.Text = Status;
        }
        
        /// <summary>
        /// Nasluchuje wiadomosci od serwera.
        /// </summary>
        void Listen()
        {
            string[] IncomingData;
            while (true)
            {
                try
                {
                    IncomingData = new string[Global.tcpReader.ReadInt32()]; // odbieramy liczbe okreslajaca ile stringow musimy odebrac, i tworzymy tablice o takim rozmiarze
                    for (int i = 0; i < IncomingData.Length; i++)  // odbieramy po kolei wszystkie stringi
                    {
                        IncomingData[i] = Global.tcpReader.ReadString(); 
                    }

                    switch (IncomingData[0]) // pierwszy element w tablicy okresla jaki typ wiadomosci wyslal serwer
                    {
                        case "msg": // jeden z uzytkownikow wyslal wiadomosc
                            {
                                MessageFromUserReceived(IncomingData[1], IncomingData[2]); // nastepne DWA elementy w tablicy to kolejno: nazwa uzytkownika przesylajacego wiadomosc, tresc tej wiadomosci
                                break;
                            }
                        case "userlist": // serwer wyslal nam aktualna liste polaczonych uzytkownikow
                            {
                                UpdateConnectedUsersList(IncomingData.ToList()); // musimy zamienic tablice na liste (nie wiem dlaczego, ale kiedy przekazujemy w parametrze tablice stringow, metoda Invoke daje wyjatek "Niezgodnosc liczby parametrow")
                                break;
                            }
                        case "servermsg": // wiadomosc od serwera (np o tym ze ktos sie polaczyl lub odlaczyl)
                            {
                                MessageFromServerReceived(IncomingData[1]);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                catch (Exception exc)
                {
                    Disconnect(); // kiedy wystapil blad rozlaczamy sie (nie jestem pewien czy to dobre rozwiazanie....)
                    break;
                }
            }
        }

        delegate void UpdateConnectedUsersListDelegate(List<string> Users);
        /// <summary>
        /// Wypelnia listboxa lista polaczonych uzytkownikow.
        /// </summary>
        /// <param name="Users"></param>
        void UpdateConnectedUsersList(List<string> Users)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateConnectedUsersListDelegate(UpdateConnectedUsersList), Users); // jesli zamiast listy jest tablica stringow, wyrzucany jest tutaj wyjatek (???)
                return;
            }
            lstConnectedUsers.Items.Clear(); // najpierw czyscimy stara liste..
            for (int i = 1; i < Users.Count; i++) // ..a nastepnie wypelniamy ja nowymi danymi, pomijajac pierwszy element na liscie, poniewaz jest to typ wiadomosci wyslany przez serwer czyli w tym wypadku "userlist"
            {
                lstConnectedUsers.Items.Add(Users[i]);
            }
        }

        delegate void MessageFromServerReceivedDelegate(string Message);
        /// <summary>
        /// Metoda jest wywolywana kiedy odebrano wiadomosc od samego serwera, a nie od zwyklego uzytkownika
        /// </summary>
        /// <param name="Message"></param>
        void MessageFromServerReceived(string Message)
        {
            if (InvokeRequired)
            {
                Invoke(new MessageFromServerReceivedDelegate(MessageFromServerReceived), Message);
                return;
            }
            wbConversation.DocumentText += "<div style='font-style: italic; background-color: #CEE3F6'>" + CurrentTime + ": " + Message.Replace("\n", "<br>") + "</div>"; 
        }

        delegate void MessageFromUserReceivedDelegate(string User, string Message);
        /// <summary>
        /// Metoda wywolywana jest kiedy jeden z uzytkownikow wyslal wiadomosc
        /// </summary>
        /// <param name="User">Nazwa uzytkownika ktory wyslal wiadomosc</param>
        /// <param name="Message">Tresc wiadomosci</param>
        void MessageFromUserReceived(string User, string Message)
        {
            if (InvokeRequired)
            {
                Invoke(new MessageFromUserReceivedDelegate(MessageFromUserReceived), User, Message);
                return;
            }
            wbConversation.DocumentText += "<div style='border-bottom: 1px dashed black'>" + CurrentTime + " <b>" + User + "</b>: " + Message.Replace("\n", "<br>") + "</div>";
        }

        delegate void OnDisconnectDelegate();
        void Disconnect()
        {
            if (InvokeRequired)
            {
                Invoke(new OnDisconnectDelegate(Disconnect));
                return;
            }
            try { Global.tcpClient.Close(); }
            catch { }
            try { Global.tcpStream.Close(); }
            catch { }
            try { Global.tcpReader.Close(); }
            catch { }
            try { Global.tcpWriter.Close(); }
            catch { }
            try { Global.Timer.Dispose(); }
            catch { }
            Global.IsLoggedIn = false;
            lstConnectedUsers.Items.Clear();
            lstConnectedUsers.Enabled = false;
            btnLogIn_LogOut.Enabled = true;
            btnLogIn_LogOut.Text = "Zaloguj";
            lblStatus.Text = "Niezalogowany";
            // odblokowujemy mozliwosci zmiany hasla itd.
            btnChangePassword.Enabled = true;
            btnDeleteAccount.Enabled = true;
            btnNewAccount.Enabled = true;
        }

        /// <summary>
        /// Metoda wywolywana przez timer co 10sek, wysyla pusta wiadomosc do serwera w celu utrzymania polaczenia. Jesli zadna wiadomosc nie dotrze do serwera przez ustalony przez niego okres czasu, serwer uznaje nasze polaczenie za martwe i nas odlacza.
        /// </summary>
        /// <param name="state">Parametr musi tu byc, choc nie jest nam potrzebny i w tym wypadku ma zawsze wartosc null.</param>
        void Timer_Elapsed(object state)
        {
            Global.tcpWriter.Write("");
        }

        /// <summary>
        /// Wysyla nasza wiadomosc z textboxa do serwera. UWAGA: nie wyswietlamy sobie od razu tej wiadomosci w webbrowserze, dopiero serwer ja nam odesle tak jak innym uzytkownikom (mamy wtedy pewnosc ze nasza wiadomosc dotarla do serwera)
        /// </summary>
        void SendMessage()
        {
            Global.tcpWriter.Write(txtMessage.Text);
            txtMessage.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cs.WriteValue("server", "ip", Global.ServerIP);
            cs.WriteValue("server", "port", Global.ServerPort);
        }
    }
}
