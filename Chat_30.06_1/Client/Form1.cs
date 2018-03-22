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

namespace Client
{
    public partial class Form1 : Form
    {
        TcpClient client = null;
        public Form1()
        {
            InitializeComponent();
            Int32 port = 13000;
            client = new TcpClient("127.0.0.1", port);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                

                // Translate the passed "asdfjbskdfjbnsd" into ASCII and store it as a Byte array.
                string message = txtMessage.Text;
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                //Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
               // Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                
            }
            catch (ArgumentNullException ex)
            {
                //Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException ex)
            {
                //Console.WriteLine("SocketException: {0}", e);
            }

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Int32 port = 13000;
            client = new TcpClient("127.0.0.1", port);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            client.Close();

        }
    }
}
