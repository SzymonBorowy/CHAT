namespace ChatServer
{
    partial class Serwer
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
            this.listBoxMessage = new System.Windows.Forms.ListBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.groupBoxAdmin = new System.Windows.Forms.GroupBox();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxServer = new System.Windows.Forms.ListBox();
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.comboBoxIpAddress = new System.Windows.Forms.ComboBox();
            this.backgroundWorkerMainLoop = new System.ComponentModel.BackgroundWorker();
            this.groupBoxAdmin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxMessage
            // 
            this.listBoxMessage.FormattingEnabled = true;
            this.listBoxMessage.Location = new System.Drawing.Point(229, 17);
            this.listBoxMessage.Name = "listBoxMessage";
            this.listBoxMessage.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxMessage.Size = new System.Drawing.Size(341, 381);
            this.listBoxMessage.TabIndex = 12;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(19, 401);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 13;
            this.buttonSend.Text = "Wyślij";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(100, 404);
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(470, 20);
            this.textBoxMessage.TabIndex = 15;
            this.textBoxMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxMessage_KeyDown);
            // 
            // groupBoxAdmin
            // 
            this.groupBoxAdmin.Controls.Add(this.buttonRemove);
            this.groupBoxAdmin.Controls.Add(this.listBoxUsers);
            this.groupBoxAdmin.Controls.Add(this.buttonStop);
            this.groupBoxAdmin.Controls.Add(this.buttonStart);
            this.groupBoxAdmin.Controls.Add(this.label3);
            this.groupBoxAdmin.Controls.Add(this.label2);
            this.groupBoxAdmin.Controls.Add(this.label1);
            this.groupBoxAdmin.Controls.Add(this.listBoxServer);
            this.groupBoxAdmin.Controls.Add(this.numericUpDownPort);
            this.groupBoxAdmin.Controls.Add(this.comboBoxIpAddress);
            this.groupBoxAdmin.Location = new System.Drawing.Point(10, 12);
            this.groupBoxAdmin.Name = "groupBoxAdmin";
            this.groupBoxAdmin.Size = new System.Drawing.Size(213, 386);
            this.groupBoxAdmin.TabIndex = 11;
            this.groupBoxAdmin.TabStop = false;
            this.groupBoxAdmin.Text = "Panel administracyjny";
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(9, 352);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 8;
            this.buttonRemove.Text = "Usuń";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.Location = new System.Drawing.Point(90, 215);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxUsers.Size = new System.Drawing.Size(117, 160);
            this.listBoxUsers.TabIndex = 7;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(132, 173);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 6;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(6, 173);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Lista użydkowników:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Adres IP:";
            // 
            // listBoxServer
            // 
            this.listBoxServer.FormattingEnabled = true;
            this.listBoxServer.Location = new System.Drawing.Point(6, 72);
            this.listBoxServer.Name = "listBoxServer";
            this.listBoxServer.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxServer.Size = new System.Drawing.Size(201, 95);
            this.listBoxServer.TabIndex = 2;
            // 
            // numericUpDownPort
            // 
            this.numericUpDownPort.Location = new System.Drawing.Point(107, 46);
            this.numericUpDownPort.Maximum = new decimal(new int[] {
            25001,
            0,
            0,
            0});
            this.numericUpDownPort.Name = "numericUpDownPort";
            this.numericUpDownPort.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownPort.TabIndex = 1;
            this.numericUpDownPort.Value = new decimal(new int[] {
            6969,
            0,
            0,
            0});
            // 
            // comboBoxIpAddress
            // 
            this.comboBoxIpAddress.FormattingEnabled = true;
            this.comboBoxIpAddress.Location = new System.Drawing.Point(107, 19);
            this.comboBoxIpAddress.Name = "comboBoxIpAddress";
            this.comboBoxIpAddress.Size = new System.Drawing.Size(100, 21);
            this.comboBoxIpAddress.TabIndex = 0;
            this.comboBoxIpAddress.Text = "192.168.1.164";
            // 
            // backgroundWorkerMainLoop
            // 
            this.backgroundWorkerMainLoop.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMainLoop_DoWork);
            // 
            // Serwer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 435);
            this.Controls.Add(this.listBoxMessage);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.groupBoxAdmin);
            this.Name = "Serwer";
            this.Text = "Serwer";
            this.groupBoxAdmin.ResumeLayout(false);
            this.groupBoxAdmin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxMessage;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.GroupBox groupBoxAdmin;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxServer;
        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.ComboBox comboBoxIpAddress;
        private System.ComponentModel.BackgroundWorker backgroundWorkerMainLoop;
    }
}

