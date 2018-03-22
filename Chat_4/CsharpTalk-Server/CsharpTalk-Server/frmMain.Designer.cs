namespace CsharpTalk_Server
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
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.groupBoxConnectedUsers = new System.Windows.Forms.GroupBox();
            this.lstConnectedUsers = new System.Windows.Forms.ListBox();
            this.btnDisconnectSelected = new System.Windows.Forms.Button();
            this.groupBoxAllUsers = new System.Windows.Forms.GroupBox();
            this.lstAllUsers = new System.Windows.Forms.ListBox();
            this.btnDeleteSelected = new System.Windows.Forms.Button();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.groupBoxConnectedUsers.SuspendLayout();
            this.groupBoxAllUsers.SuspendLayout();
            this.groupBoxLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.White;
            this.txtLog.Location = new System.Drawing.Point(9, 21);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(511, 376);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "";
            this.txtLog.WordWrap = false;
            // 
            // groupBoxConnectedUsers
            // 
            this.groupBoxConnectedUsers.Controls.Add(this.lstConnectedUsers);
            this.groupBoxConnectedUsers.Controls.Add(this.btnDisconnectSelected);
            this.groupBoxConnectedUsers.Location = new System.Drawing.Point(12, 12);
            this.groupBoxConnectedUsers.Name = "groupBoxConnectedUsers";
            this.groupBoxConnectedUsers.Size = new System.Drawing.Size(260, 211);
            this.groupBoxConnectedUsers.TabIndex = 3;
            this.groupBoxConnectedUsers.TabStop = false;
            this.groupBoxConnectedUsers.Text = "Połączeni użytkownicy (0)";
            // 
            // lstConnectedUsers
            // 
            this.lstConnectedUsers.FormattingEnabled = true;
            this.lstConnectedUsers.Location = new System.Drawing.Point(6, 21);
            this.lstConnectedUsers.Name = "lstConnectedUsers";
            this.lstConnectedUsers.Size = new System.Drawing.Size(248, 160);
            this.lstConnectedUsers.TabIndex = 7;
            // 
            // btnDisconnectSelected
            // 
            this.btnDisconnectSelected.Location = new System.Drawing.Point(6, 183);
            this.btnDisconnectSelected.Name = "btnDisconnectSelected";
            this.btnDisconnectSelected.Size = new System.Drawing.Size(248, 23);
            this.btnDisconnectSelected.TabIndex = 7;
            this.btnDisconnectSelected.Text = "Odłącz zaznaczonego";
            this.btnDisconnectSelected.UseVisualStyleBackColor = true;
            this.btnDisconnectSelected.Click += new System.EventHandler(this.btnDisconnectSelected_Click);
            // 
            // groupBoxAllUsers
            // 
            this.groupBoxAllUsers.Controls.Add(this.lstAllUsers);
            this.groupBoxAllUsers.Controls.Add(this.btnDeleteSelected);
            this.groupBoxAllUsers.Location = new System.Drawing.Point(278, 12);
            this.groupBoxAllUsers.Name = "groupBoxAllUsers";
            this.groupBoxAllUsers.Size = new System.Drawing.Size(260, 211);
            this.groupBoxAllUsers.TabIndex = 5;
            this.groupBoxAllUsers.TabStop = false;
            this.groupBoxAllUsers.Text = "Użytkownicy (0)";
            // 
            // lstAllUsers
            // 
            this.lstAllUsers.FormattingEnabled = true;
            this.lstAllUsers.Location = new System.Drawing.Point(6, 21);
            this.lstAllUsers.Name = "lstAllUsers";
            this.lstAllUsers.Size = new System.Drawing.Size(248, 160);
            this.lstAllUsers.TabIndex = 8;
            // 
            // btnDeleteSelected
            // 
            this.btnDeleteSelected.Location = new System.Drawing.Point(6, 183);
            this.btnDeleteSelected.Name = "btnDeleteSelected";
            this.btnDeleteSelected.Size = new System.Drawing.Size(248, 23);
            this.btnDeleteSelected.TabIndex = 6;
            this.btnDeleteSelected.Text = "Usuń zaznaczonego";
            this.btnDeleteSelected.UseVisualStyleBackColor = true;
            this.btnDeleteSelected.Click += new System.EventHandler(this.btnDeleteSelected_Click);
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Controls.Add(this.txtLog);
            this.groupBoxLog.Location = new System.Drawing.Point(12, 229);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Size = new System.Drawing.Size(526, 403);
            this.groupBoxLog.TabIndex = 6;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "Log";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 641);
            this.Controls.Add(this.groupBoxLog);
            this.Controls.Add(this.groupBoxAllUsers);
            this.Controls.Add(this.groupBoxConnectedUsers);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSharp-Talk Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBoxConnectedUsers.ResumeLayout(false);
            this.groupBoxAllUsers.ResumeLayout(false);
            this.groupBoxLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.GroupBox groupBoxConnectedUsers;
        private System.Windows.Forms.GroupBox groupBoxAllUsers;
        private System.Windows.Forms.GroupBox groupBoxLog;
        private System.Windows.Forms.Button btnDisconnectSelected;
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.Windows.Forms.ListBox lstConnectedUsers;
        private System.Windows.Forms.ListBox lstAllUsers;
    }
}

