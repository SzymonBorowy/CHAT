using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace CsharpTalk_ClientApp
{
    public partial class frmDeleteAccount : Form
    {
        string Login, Password;
        public frmDeleteAccount(string Login, string Password)
        {
            this.Login = Login;
            this.Password = Password;
            InitializeComponent();
        }

        private void frmDeleteAccount_Load(object sender, EventArgs e)
        {
            txtPassword.Text = Password;
            txtUserName.Text = Login;
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() != "" && txtUserName.Text.Trim() != "")
                btnDelete.Enabled = true;
            else btnDelete.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            new DeleteDelegate(Delete).BeginInvoke(txtUserName.Text, txtPassword.Text, null, null);
        }

        delegate void DeleteDelegate(string UserName, string Password);
        void Delete(string UserName, string Password)
        {
            try
            {
                SetStatus("Łączenie...");
                Global.tcpClient = new TcpClient();
                Global.tcpClient.Connect(Global.ServerIP, Global.ServerPort);
                Global.tcpStream = Global.tcpClient.GetStream();
                Global.tcpReader = new BinaryReader(Global.tcpStream);
                Global.tcpWriter = new BinaryWriter(Global.tcpStream);
                SetStatus("Wysyłanie żądania...");
                Global.tcpWriter.Write("delete");
                Global.tcpWriter.Write(txtUserName.Text);
                Global.tcpWriter.Write(Global.GetHash(txtPassword.Text));
                SetStatus("Oczekiwanie na odpowiedź serwera...");
                string response = Global.tcpReader.ReadString();
                SetStatus("");
                MessageBox.Show(response);
                CloseWindow();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                CloseWindow();
            }
            finally
            {
                try
                {
                    Global.tcpClient.Close();
                    Global.tcpReader.Close();
                    Global.tcpWriter.Close();
                    Global.tcpStream.Close();
                }
                catch { }
            }
        }

        delegate void CloseWindowDelegate();
        void CloseWindow()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CloseWindowDelegate(CloseWindow));
                return;
            }
            this.Close();
        }

        delegate void SetStatusDelegate(string Status);
        void SetStatus(string Status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetStatusDelegate(SetStatus), Status);
                return;
            }
            lblStatus.Text = Status;
        }
    }
}
