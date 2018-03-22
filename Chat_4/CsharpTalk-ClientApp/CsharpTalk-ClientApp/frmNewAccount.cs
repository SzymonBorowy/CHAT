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
    public partial class frmNewAccount : Form
    {
        public frmNewAccount()
        {
            InitializeComponent();
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() != "" && txtPassword.Text.Trim() != "" && txtPassword.Text == txtRepeatPassword.Text)
                btnCreate.Enabled = true;
            else btnCreate.Enabled = false;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            new CreateDelegate(Create).BeginInvoke(txtUserName.Text, txtPassword.Text, null, null);
        }

        delegate void CreateDelegate(string UserName, string Password);
        void Create(string UserName, string Password)
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
                Global.tcpWriter.Write("register");
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

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            toolTip.ToolTipTitle = "Uwaga";
            toolTip.Show("Pod taką nazwą będą Cię widzieć inni użytkownicy czatu.", txtUserName);
        }
    }
}
