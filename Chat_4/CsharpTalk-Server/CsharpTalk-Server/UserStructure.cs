using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace CsharpTalk_Server
{
    struct UserStruct
    {
        public UserSession Session;
        public TcpClient TcpClient;
        public NetworkStream Stream;
        public BinaryReader Reader;
        public BinaryWriter Writer;
        public string Login, Password;
    }
}
