using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Xml.Serialization;

namespace CsharpTalk_Server
{
    class Global
    {
        public static frmMain FormMain { get; set; }
        public static bool IsServerRunning { get; set; }
        public static TcpListener Server { get; set; }
        public static List<UserInfo> Users { get; set; }
        public static Hashtable ConnectedUsers { get; set; }
        public static FileInfo DatabaseFile { get { return new FileInfo("users.xml"); } }
    }

    public class UserInfo
    {
        [XmlElement("Login")]
        public string Login { get; set; }

        [XmlElement("Password")]
        public string Password { get; set; }

        public UserInfo(string Login, string Password)
        {
            this.Login = Login;
            this.Password = Password;
        }

        public UserInfo()
        { }
    }
}
