namespace ChatKlient
{
    partial class Klient
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
            this.components = new System.ComponentModel.Container();
            this.backgroundWorkerMainThread = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.listBoxMessage = new System.Windows.Forms.ListBox();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.textBoxNick = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGra = new System.Windows.Forms.TabPage();
            this.labelPrzeciwnik = new System.Windows.Forms.Label();
            this.labelRuch = new System.Windows.Forms.Label();
            this.labelZnak = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageGra.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorkerMainThread
            // 
            this.backgroundWorkerMainThread.WorkerSupportsCancellation = true;
            this.backgroundWorkerMainThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMainThread_DoWork);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.buttonStartGame);
            this.tabPage1.Controls.Add(this.listBoxMessage);
            this.tabPage1.Controls.Add(this.listBoxUsers);
            this.tabPage1.Controls.Add(this.textBoxMessage);
            this.tabPage1.Controls.Add(this.textBoxNick);
            this.tabPage1.Controls.Add(this.buttonSend);
            this.tabPage1.Controls.Add(this.buttonDisconnect);
            this.tabPage1.Controls.Add(this.buttonConnect);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(613, 342);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(479, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "label1";
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Location = new System.Drawing.Point(482, 297);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(75, 23);
            this.buttonStartGame.TabIndex = 24;
            this.buttonStartGame.Text = "Gra";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // listBoxMessage
            // 
            this.listBoxMessage.FormattingEnabled = true;
            this.listBoxMessage.Location = new System.Drawing.Point(16, 15);
            this.listBoxMessage.Name = "listBoxMessage";
            this.listBoxMessage.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxMessage.Size = new System.Drawing.Size(460, 251);
            this.listBoxMessage.TabIndex = 23;
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.Location = new System.Drawing.Point(482, 16);
            this.listBoxUsers.MultiColumn = true;
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxUsers.Size = new System.Drawing.Size(120, 251);
            this.listBoxUsers.TabIndex = 22;
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(16, 300);
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(379, 20);
            this.textBoxMessage.TabIndex = 21;
            this.textBoxMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxMessage_KeyDown);
            // 
            // textBoxNick
            // 
            this.textBoxNick.Location = new System.Drawing.Point(16, 272);
            this.textBoxNick.Name = "textBoxNick";
            this.textBoxNick.Size = new System.Drawing.Size(217, 20);
            this.textBoxNick.TabIndex = 18;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(401, 297);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 20;
            this.buttonSend.Text = "Wyślij";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(320, 272);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(75, 23);
            this.buttonDisconnect.TabIndex = 19;
            this.buttonDisconnect.Text = "Rozłącz";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(239, 272);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 17;
            this.buttonConnect.Text = "Połącz";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPageGra);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(621, 368);
            this.tabControl1.TabIndex = 17;
            // 
            // tabPageGra
            // 
            this.tabPageGra.Controls.Add(this.labelPrzeciwnik);
            this.tabPageGra.Controls.Add(this.labelRuch);
            this.tabPageGra.Controls.Add(this.labelZnak);
            this.tabPageGra.Controls.Add(this.button9);
            this.tabPageGra.Controls.Add(this.button8);
            this.tabPageGra.Controls.Add(this.button7);
            this.tabPageGra.Controls.Add(this.button6);
            this.tabPageGra.Controls.Add(this.button5);
            this.tabPageGra.Controls.Add(this.button4);
            this.tabPageGra.Controls.Add(this.button3);
            this.tabPageGra.Controls.Add(this.button2);
            this.tabPageGra.Controls.Add(this.button1);
            this.tabPageGra.Location = new System.Drawing.Point(4, 22);
            this.tabPageGra.Name = "tabPageGra";
            this.tabPageGra.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGra.Size = new System.Drawing.Size(613, 342);
            this.tabPageGra.TabIndex = 1;
            this.tabPageGra.Tag = "0,0";
            this.tabPageGra.Text = "Gra";
            this.tabPageGra.UseVisualStyleBackColor = true;
            // 
            // labelPrzeciwnik
            // 
            this.labelPrzeciwnik.AutoSize = true;
            this.labelPrzeciwnik.Location = new System.Drawing.Point(321, 49);
            this.labelPrzeciwnik.Name = "labelPrzeciwnik";
            this.labelPrzeciwnik.Size = new System.Drawing.Size(80, 13);
            this.labelPrzeciwnik.TabIndex = 20;
            this.labelPrzeciwnik.Text = "labelPrzeciwnik";
            // 
            // labelRuch
            // 
            this.labelRuch.AutoSize = true;
            this.labelRuch.Location = new System.Drawing.Point(321, 119);
            this.labelRuch.Name = "labelRuch";
            this.labelRuch.Size = new System.Drawing.Size(55, 13);
            this.labelRuch.TabIndex = 19;
            this.labelRuch.Text = "labelRuch";
            // 
            // labelZnak
            // 
            this.labelZnak.AutoSize = true;
            this.labelZnak.Location = new System.Drawing.Point(321, 189);
            this.labelZnak.Name = "labelZnak";
            this.labelZnak.Size = new System.Drawing.Size(54, 13);
            this.labelZnak.TabIndex = 18;
            this.labelZnak.Text = "labelZnak";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(187, 189);
            this.button9.Margin = new System.Windows.Forms.Padding(10);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(50, 50);
            this.button9.TabIndex = 17;
            this.button9.Tag = "2;2";
            this.button9.Text = " ";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(117, 189);
            this.button8.Margin = new System.Windows.Forms.Padding(10);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(50, 50);
            this.button8.TabIndex = 16;
            this.button8.Tag = "2;1";
            this.button8.Text = " ";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(47, 189);
            this.button7.Margin = new System.Windows.Forms.Padding(10);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(50, 50);
            this.button7.TabIndex = 15;
            this.button7.Tag = "2;0";
            this.button7.Text = " ";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(187, 119);
            this.button6.Margin = new System.Windows.Forms.Padding(10);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(50, 50);
            this.button6.TabIndex = 14;
            this.button6.Tag = "1;2";
            this.button6.Text = " ";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(117, 119);
            this.button5.Margin = new System.Windows.Forms.Padding(10);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(50, 50);
            this.button5.TabIndex = 13;
            this.button5.Tag = "1;1";
            this.button5.Text = " ";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(47, 119);
            this.button4.Margin = new System.Windows.Forms.Padding(10);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(50, 50);
            this.button4.TabIndex = 12;
            this.button4.Tag = "1;0";
            this.button4.Text = " ";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(187, 49);
            this.button3.Margin = new System.Windows.Forms.Padding(10);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 50);
            this.button3.TabIndex = 11;
            this.button3.Tag = "0;2";
            this.button3.Text = " ";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(117, 49);
            this.button2.Margin = new System.Windows.Forms.Padding(10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 50);
            this.button2.TabIndex = 10;
            this.button2.Tag = "0;1";
            this.button2.Text = " ";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 49);
            this.button1.Margin = new System.Windows.Forms.Padding(10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 9;
            this.button1.Tag = "0;0";
            this.button1.Text = " ";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Klient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 388);
            this.Controls.Add(this.tabControl1);
            this.Name = "Klient";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageGra.ResumeLayout(false);
            this.tabPageGra.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorkerMainThread;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.ListBox listBoxMessage;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.TextBox textBoxNick;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGra;
        public System.Windows.Forms.Button button9;
        public System.Windows.Forms.Button button8;
        public System.Windows.Forms.Button button7;
        public System.Windows.Forms.Button button6;
        public System.Windows.Forms.Button button5;
        public System.Windows.Forms.Button button4;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelZnak;
        private System.Windows.Forms.Label labelRuch;
        private System.Windows.Forms.Label labelPrzeciwnik;
    }
}

