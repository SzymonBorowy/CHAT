using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Xml.Serialization;

namespace CsharpTalk_Server
{

    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Global.FormMain = this;
            LoadUsers();

            Global.ConnectedUsers = new Hashtable();
            Global.Server = new TcpListener(IPAddress.Any, 1000);
            Global.Server.Start(5);
            Global.IsServerRunning = true;
            Global.Server.BeginAcceptTcpClient(new AsyncCallback(OnAccept), null);
            Log("Rozpoczęto nasłuchiwanie na porcie " + Global.Server.Server.LocalEndPoint.ToString().Split(':')[1]);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Wczytuje z pliku XML liste zarejestrowanych uzytkownikow
        /// </summary>
        void LoadUsers()
        {
            if (!Global.DatabaseFile.Exists)
            {
                Global.Users = new List<UserInfo>();
            }
            else
            {
                try
                {
                    XmlSerializer xmlDeserializer = new XmlSerializer(typeof(List<UserInfo>), new XmlRootAttribute("Users"));
                    TextReader xmlReader = new StreamReader(Global.DatabaseFile.FullName);
                    Global.Users = (List<UserInfo>)xmlDeserializer.Deserialize(xmlReader);
                    xmlReader.Close();
                    LoadUsersList();
                }
                catch (Exception exc)
                {
                    Global.Users = new List<UserInfo>(); // mimo wszystko musimy stworzyc pusta liste uzytkownikow
                    MessageBox.Show("Wystąpił błąd podczas wczytywania listy użytkowników: " + exc.Message, "Ups...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Zapisuje liste zarejestrowanych uzytkownikow do pliku XML
        /// </summary>
        /// <returns></returns>
        bool SaveUsers()
        {
            try
            {
                TextWriter xmlWriter = new StreamWriter(Global.DatabaseFile.FullName);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<UserInfo>), new XmlRootAttribute("Users"));
                xmlSerializer.Serialize(xmlWriter, Global.Users);
                xmlWriter.Close();
                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Nie można zachować listy zarejestrowanych użytkowników: " + exc.Message, "Kurwa no...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (Global.DatabaseFile.Exists)
                    Global.DatabaseFile.Delete();
                return false;
            }
        }

        delegate void LoadUsersListDelegate();
        /// <summary>
        /// Wczytuje liste zarejestrowanych uzytkownikow do listboxa
        /// </summary>
        void LoadUsersList()
        {
            if (InvokeRequired)
            {
                Invoke(new LoadUsersListDelegate(LoadUsersList));
                return;
            }
            lstAllUsers.Items.Clear();
            foreach (UserInfo userInfo in Global.Users)
            {
                lstAllUsers.Items.Add(userInfo.Login);
            }
            groupBoxAllUsers.Text = "Użytkownicy (" + lstAllUsers.Items.Count + ")";
        }

        /// <summary>
        /// Metoda jest wywolywana kiedy ktos sie polaczyl z serwerem
        /// </summary>
        /// <param name="ar">W tym wypadku ma zawsze wartosc null.</param>
        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                TcpClient tcpClient = Global.Server.EndAcceptTcpClient(ar);
                Global.Server.BeginAcceptTcpClient(new AsyncCallback(OnAccept), null); // nasluchujemy wiecej przychodzacych polaczen
                NetworkStream networkStream = tcpClient.GetStream();
                BinaryReader binaryReader = new BinaryReader(networkStream);
                BinaryWriter binaryWriter = new BinaryWriter(networkStream);
                networkStream.ReadTimeout = 20000; // klient wysyla co 10sek pusta wiadomosc, ktora musi do nas dotrzec w ciagu kolejnych 10sek.

                Log("Połączono z " + tcpClient.Client.RemoteEndPoint.ToString());
                Log("Oczekiwanie na żądanie...");

                switch (binaryReader.ReadString()) // okreslamy czego chce od nas aplikacja kliencka
                {
                    case "register":
                        {
                            Log("Żądanie utworzenia nowego konta użytkownika, oczekiwanie na dane konta...");
                            string Login = binaryReader.ReadString();
                            string Password = binaryReader.ReadString();

                            string Result = "";
                            if (UserExists(Login))
                            {
                                Result = "Istnieje już użytkownik o nazwie '" + Login + "'.";
                            }
                            else
                            {
                                Global.Users.Add(new UserInfo(Login, Password)); // dodajemy uzytkownika do listy zarejestrowanych
                                LoadUsersList();
                                if (SaveUsers())
                                    Result = "Użytkownik '" + Login + "' został zarejestrowany.";
                                else
                                    Result = "(500) Wewnętrzny błąd serwera: Nie można było zachować listy użytkowników. Nowe konto może nie funkcjonować prawidłowo.";
                            }
                            Log(Result);

                            binaryWriter.Write(Result); // odsylamy do klienta wynik
                            binaryWriter.Close();
                            binaryReader.Close();
                            networkStream.Close();
                            tcpClient.Close();
                            break;
                        }
                    case "login":
                        {
                            Log("Żądanie zalogowania. Oczekiwanie na login i hasło...");
                            string UserLogin = binaryReader.ReadString();
                            string UserPassword = binaryReader.ReadString();

                            if (UserExists(UserLogin)) // sprawdzamy czy istnieje taki uzytkownik
                            {
                                if (Global.ConnectedUsers.ContainsKey(UserLogin)) // sprawdzamy czy uzytkownik jest juz zalogowany..
                                {
                                    binaryWriter.Write("Jesteś już zalogowany!");
                                    binaryWriter.Close();
                                    binaryReader.Close();
                                    networkStream.Close();
                                    tcpClient.Close();
                                    Log("Użytkownik '" + UserLogin + "' jest już zalogowany.");
                                }
                                else if (GetUserPassword(UserLogin) == UserPassword) // .. jesli nie to sprawdzamy czy uzytkownik podal wlasciwe haslo
                                {
                                    UserSession userSession = new UserSession(UserLogin);

                                    UserStruct userStruct = new UserStruct();
                                    userStruct.Session = userSession;
                                    userStruct.TcpClient = tcpClient;
                                    userStruct.Stream = networkStream;
                                    userStruct.Reader = binaryReader;
                                    userStruct.Writer = binaryWriter;
                                    userStruct.Login = UserLogin;
                                    userStruct.Password = UserPassword;

                                    Global.ConnectedUsers.Add(UserLogin, userStruct);
                                    AddToConnectedUsersList(UserLogin);
                                    userStruct.Session.RunUserSessionAsync();

                                    binaryWriter.Write("OK"); // informujemy klienta ze zostal poprawnie zalogowany
                                    Log("Zalogowano użytkownika '" + UserLogin + "'");
                                    SendConnectedUsersList();
                                    SendToAll("servermsg", UserLogin + " połączył się");
                                }
                                else // uzytkownik podal niewlasciwe haslo
                                {
                                    binaryWriter.Write("Nieprawidłowe hasło!");
                                    binaryWriter.Close();
                                    binaryReader.Close();
                                    networkStream.Close();
                                    tcpClient.Close();
                                    Log("Użytkownik '" + UserLogin + "' podał nieprawidłowe hasło");
                                }
                            }
                            else
                            {
                                binaryWriter.Write("Nie istnieje taki użytkownik!");
                                binaryWriter.Close();
                                binaryReader.Close();
                                networkStream.Close();
                                tcpClient.Close();
                                Log("Nie istnieje użytkownik '" + UserLogin + "'");
                            }

                            break;
                        }
                    case "delete":
                        {
                            Log("Żądanie usunięcia konta. Oczekiwanie na login i hasło...");
                            string UserLogin = binaryReader.ReadString();
                            string UserPassword = binaryReader.ReadString();

                            if (UserExists(UserLogin)) // sprawdzamy czy istnieje taki uzytkownik
                            {
                                if (UserPassword == GetUserPassword(UserLogin)) // sprawdzamy czy uzytkownik podal wlasciwe haslo
                                {
                                    string Result = "";
                                    for (int i = 0; i < Global.Users.Count; i++) // szukamy uzytkownika w bazie
                                    {
                                        if (Global.Users[i].Login == UserLogin)
                                        {
                                            Global.Users.RemoveAt(i);
                                            LoadUsersList();
                                            if (SaveUsers())
                                                Result = "Użytkownik '" + UserLogin + "' został usunięty.";
                                            else
                                                Result = "(500) Wewnętrzny błąd serwera: Nie można było zachować listy użytkowników. Konto mogło nie zostać usunięte.";
                                        }
                                    }
                                    Log(Result);

                                    binaryWriter.Write(Result); // odsylamy do klienta wynik
                                    binaryWriter.Close();
                                    binaryReader.Close();
                                    networkStream.Close();
                                    tcpClient.Close();
                                }
                                else // uzytkownik podal nieprawidlowe haslo
                                {
                                    binaryWriter.Write("Nieprawidłowe hasło!");
                                    binaryWriter.Close();
                                    binaryReader.Close();
                                    networkStream.Close();
                                    tcpClient.Close();
                                    Log("Użytkownik '" + UserLogin + "' podał nieprawidłowe hasło");
                                }
                            }
                            else // uzytkownik nie istnieje
                            {
                                binaryWriter.Write("Nie istnieje taki użytkownik!");
                                binaryWriter.Close();
                                binaryReader.Close();
                                networkStream.Close();
                                tcpClient.Close();
                                Log("Nie istnieje użytkownik '" + UserLogin + "'");
                            }

                            break;
                        }
                    case "changepassword":
                        {
                            Log("Żądanie zmiany hasła. Oczekiwanie na dane konta...");
                            string UserLogin = binaryReader.ReadString();
                            string UserPassword = binaryReader.ReadString();
                            string UserNewPassword = binaryReader.ReadString();

                            if (UserExists(UserLogin)) // sprawdzamy czy istnieje taki uzytkownik
                            {
                                if (UserPassword == GetUserPassword(UserLogin)) // sprawdzamy czy uzytkownik podal wlasciwe haslo
                                {
                                    string Result = "";
                                    for (int i = 0; i < Global.Users.Count; i++) // szukamy uzytkownika w bazie
                                    {
                                        if (Global.Users[i].Login == UserLogin)
                                        {
                                            Global.Users[i].Password = UserNewPassword;
                                            if (SaveUsers())
                                                Result = "Hasło użytkownika '" + UserLogin + "' zostało zmienione.";
                                            else
                                                Result = "(500) Wewnętrzny błąd serwera: Nie można było zachować listy użytkowników. Hasło mogło nie zostać zmienione.";
                                        }
                                    }
                                    Log(Result);

                                    binaryWriter.Write(Result); // odsylamy do klienta wynik
                                    binaryWriter.Close();
                                    binaryReader.Close();
                                    networkStream.Close();
                                    tcpClient.Close();
                                }
                                else // uzytkownik podal nieprawidlowe haslo
                                {
                                    binaryWriter.Write("Nieprawidłowe hasło!");
                                    binaryWriter.Close();
                                    binaryReader.Close();
                                    networkStream.Close();
                                    tcpClient.Close();
                                    Log("Użytkownik '" + UserLogin + "' podał nieprawidłowe hasło");
                                }
                            }
                            else // uzytkownik nie istnieje
                            {
                                binaryWriter.Write("Nie istnieje taki użytkownik!");
                                binaryWriter.Close();
                                binaryReader.Close();
                                networkStream.Close();
                                tcpClient.Close();
                                Log("Nie istnieje użytkownik '" + UserLogin + "'");
                            }

                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception exc)
            {
                Log(exc.Message);
            }
        }

        /// <summary>
        /// Metoda jest wywolywana kiedy odebrano wiadomosc od ktoregos z userow.
        /// </summary>
        /// <param name="User">Nazwa uzytkownika, ktory przeslal widaomosc.</param>
        /// <param name="Message">Tresc wiadomosci.</param>
        public void OnMessageReceived(string User, string Message)
        {
            Log(User + ": " + Message);
            SendToAll("msg", User, Message); // przekazujemy ta wiadomosc wszystkim aktualnie polaczonym userom
        }

        /// <summary>
        /// Metoda wysyla wiadomosc do wszystkich polaczonych uzytkownikow.
        /// </summary>
        /// <param name="Data">Stringi do wyslania :)</param>
        void SendToAll(params string[] Data)
        {
            foreach (UserStruct userStruct in Global.ConnectedUsers.Values)
            {
                userStruct.Writer.Write(Data.Count()); // najpierw wysylamy liczbe elementow w tablicy (aplikacja kliencka musi wiedziec ile stringow ma odebrac)
                for (int i = 0; i < Data.Count(); i++ )
                {
                    userStruct.Writer.Write(Data[i]);
                }
            }
        }

        /// <summary>
        /// Wysyla do wszystkich aktualna liste polaczonych uzytkownikow.
        /// </summary>
        void SendConnectedUsersList()
        {
            foreach (UserStruct userStruct in Global.ConnectedUsers.Values)
            {
                SendConnectedUsersList(userStruct.Login);
            }
        }

        /// <summary>
        /// Wysyla do wybranego uzytkownika liste wszystkich polaczonych obecnie userow.
        /// </summary>
        /// <param name="User">Nazwa uzytkownika do ktorego wyslac.</param>
        void SendConnectedUsersList(string User)
        {
            ((UserStruct)Global.ConnectedUsers[User]).Writer.Write(lstConnectedUsers.Items.Count + 1); // wysylamy liczbe okreslajaca ile stringow ma odebrac aplikacja kliencka (+1 poniewaz dochodzi poczatkowa komenda informujaca aplikacje kliencka co jej wysylamy)
            ((UserStruct)Global.ConnectedUsers[User]).Writer.Write("userlist"); // informujemy klienta co mu wysylamy..
            for (int i = 0; i < Global.ConnectedUsers.Count; i++) // ..i dalej wiadomo :)
            {
                ((UserStruct)Global.ConnectedUsers[User]).Writer.Write(lstConnectedUsers.Items[i].ToString());
            }
        }

        delegate void UserDisconnectDelegate(string User);
        public void UserDisconnect(string User)
        {
            if (InvokeRequired)
            {
                Invoke(new UserDisconnectDelegate(UserDisconnect), User);
                return;
            }
            try { ((UserStruct)Global.ConnectedUsers[User]).TcpClient.Close(); }
            catch { }
            try { ((UserStruct)Global.ConnectedUsers[User]).Stream.Close(); }
            catch { }
            try { ((UserStruct)Global.ConnectedUsers[User]).Reader.Close(); }
            catch { }
            try { ((UserStruct)Global.ConnectedUsers[User]).Writer.Close(); }
            catch { }
            Global.ConnectedUsers.Remove(User); // usuwamy uzytkownika z listy Hashtable polaczonych uzytkownikow
            lstConnectedUsers.Items.Remove(User); // usuwamy wpis z listy
            SendToAll("servermsg", User + " rozłączył się."); // wysylamy do wszystkich wiadomosc ze uzytkownik sie rozlaczyl
            SendConnectedUsersList();
            Log(User + " odłączył się.");
        }

        /// <summary>
        /// Sprawdza czy istnieje okreslony w parametrze uzytkownik.
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        bool UserExists(string Login)
        {
            foreach (UserInfo userInfo in Global.Users)
            {
                if (userInfo.Login == Login)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Zwraca hash hasla wybranego uzytkownika.
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        string GetUserPassword(string Login)
        {
            foreach (UserInfo user in Global.Users)
            {
                if (user.Login == Login)
                {
                    return user.Password;
                }
            }
            return null;
        }

        delegate void AddToConnectedUsersListDelegate(string ClientName);
        /// <summary>
        /// Dodaje uzytkownika do listy aktualnie polaczonych userow.
        /// </summary>
        /// <param name="UserName"></param>
        void AddToConnectedUsersList(string UserName)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AddToConnectedUsersListDelegate(AddToConnectedUsersList), UserName);
                return;
            }
            lstConnectedUsers.Items.Add(UserName);
            groupBoxConnectedUsers.Text = "Połączeni użytkownicy (" + lstConnectedUsers.Items.Count + ")";
        }

        string DeleteUser(string Login, string Password)
        {
            return "Użytkownik '" + Login + "' został usunięty.";
        }


        delegate void LogDelegate(string Text);
        public void Log(string Text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new LogDelegate(Log), Text);
                return;
            }
            txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss") + " " + Text + "\n");
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Na pewno usunąć użytkownika " + lstAllUsers.SelectedItem.ToString() + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                Global.Users.RemoveAt(lstAllUsers.SelectedIndex);
                LoadUsersList();
                SaveUsers();
            }
        }

        private void btnDisconnectSelected_Click(object sender, EventArgs e)
        {
            string user = lstConnectedUsers.SelectedItem.ToString();
            if (MessageBox.Show("Na pewno rozłączyć użytkownika '" + user + "'?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                ((UserStruct)Global.ConnectedUsers[user]).Stream.Close();
            }
        }
    }
}
