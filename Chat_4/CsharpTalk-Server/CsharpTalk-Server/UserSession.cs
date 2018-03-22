using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CsharpTalk_Server
{
    class UserSession
    {
        string Login = string.Empty;
        public UserSession(string Login)
        {
            this.Login = Login;
        }

        /// <summary>
        /// Rozpoczyna w nowym watku nasluchiwanie wiadomosci od okreslonego uzytkownika.
        /// </summary>
        public void RunUserSessionAsync()
        {
            new Thread(new ThreadStart(runUserSessionAsyc)).Start();
        }

        private void runUserSessionAsyc()
        {
            string IncomingMessage = "";
            while (true)
            {
                try
                {
                    IncomingMessage = ((UserStruct)Global.ConnectedUsers[Login]).Reader.ReadString();
                    switch (IncomingMessage)
                    {
                        case "": // pusta wiadomosc wyslana automatycznie w celu utrzymania polaczenia
                            {
                                break;
                            }
                        default:
                            {
                                Global.FormMain.OnMessageReceived(Login, IncomingMessage);
                                break;
                            }
                    }
                }
                catch
                {
                    Global.FormMain.UserDisconnect(Login); // rozlaczamy uzyszkodnika
                    break; // przerywany wykonywanie nieskonczonej petli
                }
            }
        }
    }
}
