namespace CsharpTalk_ClientApp
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wbConversation = new System.Windows.Forms.WebBrowser();
            this.groupBoxConnectedUsers = new System.Windows.Forms.GroupBox();
            this.lstConnectedUsers = new System.Windows.Forms.ListBox();
            this.groupBoxAccount = new System.Windows.Forms.GroupBox();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.btnDeleteAccount = new System.Windows.Forms.Button();
            this.checkBoxRemember = new System.Windows.Forms.CheckBox();
            this.btnNewAccount = new System.Windows.Forms.Button();
            this.btnLogIn_LogOut = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxConversation = new System.Windows.Forms.GroupBox();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.groupBoxServer = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxConnectedUsers.SuspendLayout();
            this.groupBoxAccount.SuspendLayout();
            this.groupBoxConversation.SuspendLayout();
            this.groupBoxServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // wbConversation
            // 
            this.wbConversation.Location = new System.Drawing.Point(7, 21);
            this.wbConversation.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbConversation.Name = "wbConversation";
            this.wbConversation.Size = new System.Drawing.Size(418, 343);
            this.wbConversation.TabIndex = 0;
            // 
            // groupBoxConnectedUsers
            // 
            this.groupBoxConnectedUsers.Controls.Add(this.lstConnectedUsers);
            this.groupBoxConnectedUsers.Location = new System.Drawing.Point(442, 5);
            this.groupBoxConnectedUsers.Name = "groupBoxConnectedUsers";
            this.groupBoxConnectedUsers.Size = new System.Drawing.Size(180, 148);
            this.groupBoxConnectedUsers.TabIndex = 1;
            this.groupBoxConnectedUsers.TabStop = false;
            this.groupBoxConnectedUsers.Text = "Podłączone osoby";
            // 
            // lstConnectedUsers
            // 
            this.lstConnectedUsers.Enabled = false;
            this.lstConnectedUsers.FormattingEnabled = true;
            this.lstConnectedUsers.Location = new System.Drawing.Point(6, 21);
            this.lstConnectedUsers.Name = "lstConnectedUsers";
            this.lstConnectedUsers.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstConnectedUsers.Size = new System.Drawing.Size(168, 121);
            this.lstConnectedUsers.TabIndex = 2;
            // 
            // groupBoxAccount
            // 
            this.groupBoxAccount.Controls.Add(this.btnChangePassword);
            this.groupBoxAccount.Controls.Add(this.btnDeleteAccount);
            this.groupBoxAccount.Controls.Add(this.checkBoxRemember);
            this.groupBoxAccount.Controls.Add(this.btnNewAccount);
            this.groupBoxAccount.Controls.Add(this.btnLogIn_LogOut);
            this.groupBoxAccount.Controls.Add(this.label3);
            this.groupBoxAccount.Controls.Add(this.txtPassword);
            this.groupBoxAccount.Controls.Add(this.label2);
            this.groupBoxAccount.Controls.Add(this.txtLogin);
            this.groupBoxAccount.Controls.Add(this.lblStatus);
            this.groupBoxAccount.Controls.Add(this.label1);
            this.groupBoxAccount.Location = new System.Drawing.Point(442, 159);
            this.groupBoxAccount.Name = "groupBoxAccount";
            this.groupBoxAccount.Size = new System.Drawing.Size(180, 178);
            this.groupBoxAccount.TabIndex = 2;
            this.groupBoxAccount.TabStop = false;
            this.groupBoxAccount.Text = "Twoje konto";
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Location = new System.Drawing.Point(89, 148);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(84, 23);
            this.btnChangePassword.TabIndex = 9;
            this.btnChangePassword.Text = "Zmień hasło";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // btnDeleteAccount
            // 
            this.btnDeleteAccount.Location = new System.Drawing.Point(6, 148);
            this.btnDeleteAccount.Name = "btnDeleteAccount";
            this.btnDeleteAccount.Size = new System.Drawing.Size(77, 23);
            this.btnDeleteAccount.TabIndex = 8;
            this.btnDeleteAccount.Text = "Usuń konto";
            this.btnDeleteAccount.UseVisualStyleBackColor = true;
            this.btnDeleteAccount.Click += new System.EventHandler(this.btnDeleteAccount_Click);
            // 
            // checkBoxRemember
            // 
            this.checkBoxRemember.AutoSize = true;
            this.checkBoxRemember.Location = new System.Drawing.Point(9, 96);
            this.checkBoxRemember.Name = "checkBoxRemember";
            this.checkBoxRemember.Size = new System.Drawing.Size(138, 17);
            this.checkBoxRemember.TabIndex = 5;
            this.checkBoxRemember.Text = "Zapamiętaj login i hasło";
            this.checkBoxRemember.UseVisualStyleBackColor = true;
            // 
            // btnNewAccount
            // 
            this.btnNewAccount.Location = new System.Drawing.Point(75, 119);
            this.btnNewAccount.Name = "btnNewAccount";
            this.btnNewAccount.Size = new System.Drawing.Size(98, 23);
            this.btnNewAccount.TabIndex = 7;
            this.btnNewAccount.Text = "Nowe konto...";
            this.btnNewAccount.UseVisualStyleBackColor = true;
            this.btnNewAccount.Click += new System.EventHandler(this.btnNewAccount_Click);
            // 
            // btnLogIn_LogOut
            // 
            this.btnLogIn_LogOut.Enabled = false;
            this.btnLogIn_LogOut.Location = new System.Drawing.Point(6, 119);
            this.btnLogIn_LogOut.Name = "btnLogIn_LogOut";
            this.btnLogIn_LogOut.Size = new System.Drawing.Size(63, 23);
            this.btnLogIn_LogOut.TabIndex = 6;
            this.btnLogIn_LogOut.Text = "Zaloguj";
            this.btnLogIn_LogOut.UseVisualStyleBackColor = true;
            this.btnLogIn_LogOut.Click += new System.EventHandler(this.btnLogIn_LogOut_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Hasło:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(51, 68);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(123, 22);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Login:";
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(51, 40);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(123, 22);
            this.txtLogin.TabIndex = 3;
            this.txtLogin.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(48, 18);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(86, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Niezalogowany";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status:";
            // 
            // groupBoxConversation
            // 
            this.groupBoxConversation.Controls.Add(this.txtMessage);
            this.groupBoxConversation.Controls.Add(this.wbConversation);
            this.groupBoxConversation.Location = new System.Drawing.Point(5, 5);
            this.groupBoxConversation.Name = "groupBoxConversation";
            this.groupBoxConversation.Size = new System.Drawing.Size(431, 440);
            this.groupBoxConversation.TabIndex = 4;
            this.groupBoxConversation.TabStop = false;
            this.groupBoxConversation.Text = "Rozmowa";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(7, 365);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(418, 69);
            this.txtMessage.TabIndex = 1;
            this.txtMessage.Text = "";
            this.txtMessage.WordWrap = false;
            this.txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessage_KeyDown);
            // 
            // groupBoxServer
            // 
            this.groupBoxServer.Controls.Add(this.btnSave);
            this.groupBoxServer.Controls.Add(this.txtServerPort);
            this.groupBoxServer.Controls.Add(this.label5);
            this.groupBoxServer.Controls.Add(this.txtServerIP);
            this.groupBoxServer.Controls.Add(this.label4);
            this.groupBoxServer.Location = new System.Drawing.Point(442, 343);
            this.groupBoxServer.Name = "groupBoxServer";
            this.groupBoxServer.Size = new System.Drawing.Size(180, 102);
            this.groupBoxServer.TabIndex = 5;
            this.groupBoxServer.TabStop = false;
            this.groupBoxServer.Text = "Serwer";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(50, 71);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Zapisz";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(40, 43);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(134, 22);
            this.txtServerPort.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Port:";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(40, 15);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(133, 22);
            this.txtServerIP.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "IP:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 452);
            this.Controls.Add(this.groupBoxServer);
            this.Controls.Add(this.groupBoxConversation);
            this.Controls.Add(this.groupBoxAccount);
            this.Controls.Add(this.groupBoxConnectedUsers);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSharp-Talk";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBoxConnectedUsers.ResumeLayout(false);
            this.groupBoxAccount.ResumeLayout(false);
            this.groupBoxAccount.PerformLayout();
            this.groupBoxConversation.ResumeLayout(false);
            this.groupBoxServer.ResumeLayout(false);
            this.groupBoxServer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbConversation;
        private System.Windows.Forms.GroupBox groupBoxConnectedUsers;
        private System.Windows.Forms.ListBox lstConnectedUsers;
        private System.Windows.Forms.GroupBox groupBoxAccount;
        private System.Windows.Forms.Button btnNewAccount;
        private System.Windows.Forms.Button btnLogIn_LogOut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxRemember;
        private System.Windows.Forms.GroupBox groupBoxConversation;
        private System.Windows.Forms.RichTextBox txtMessage;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnDeleteAccount;
        private System.Windows.Forms.GroupBox groupBoxServer;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label label4;
    }
}

