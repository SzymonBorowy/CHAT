using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Security.Cryptography;

namespace CsharpTalk_ClientApp
{
    class Global
    {
        public static string ServerIP { get; set; }
        public static int ServerPort { get; set; }
        public static string Login { get; set; }
        public static string Password { get; set; }
        public static bool RememberCredentials { get; set; }
        public static bool IsLoggedIn { get; set; }
        public static TcpClient tcpClient { get; set; }
        public static NetworkStream tcpStream { get; set; }
        public static BinaryReader tcpReader { get; set; }
        public static BinaryWriter tcpWriter { get; set; }
        public static Timer Timer { get; set; }

        /// <summary>
        /// Zwraca MD5 podanego tekstu. W naszym programie metoda posluzy nam do szyfrowania hasla.
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string GetHash(string Text)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(Text));

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
