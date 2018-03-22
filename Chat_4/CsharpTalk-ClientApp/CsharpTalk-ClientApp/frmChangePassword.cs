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
    public partial class frmChangePassword : Form
    {
        string Login, OldPassword, NewPassword;
        public frmChangePassword(string Login, string OldPassword)
        {
            this.Login = Login;
            this.OldPassword = OldPassword;
            InitializeComponent();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            txtUserName.Text = Login;
            txtOldPassword.Text = OldPassword;
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() != "" && txtOldPassword.Text.Trim() != "" && txtNewPassword.Text.Trim() != "" && txtNewPassword.Text == txtRepeatNewPassword.Text)
                btnChange.Enabled = true;
            else
                btnChange.Enabled = false;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            new ChangePasswordDelegate(ChangePassword).BeginInvoke(txtUserName.Text, txtOldPassword.Text, txtNewPassword.Text, null, null);
        }

        delegate void ChangePasswordDelegate(string UserName, string OldPassword, string NewPassword);
        void ChangePassword(string UserName, string OldPassword, string NewPassword)
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
                Global.tcpWriter.Write("changepassword");
                Global.tcpWriter.Write(txtUserName.Text);
                Global.tcpWriter.Write(Global.GetHash(txtOldPassword.Text));
                Global.tcpWriter.Write(Global.GetHash(txtNewPassword.Text));
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
