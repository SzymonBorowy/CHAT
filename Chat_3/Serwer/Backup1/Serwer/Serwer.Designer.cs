namespace Serwer
{
    partial class frmSerwer
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
            this.cmdSluchaj = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOtrzymywane = new System.Windows.Forms.RichTextBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdWyslij = new System.Windows.Forms.Button();
            this.txtWysylane = new System.Windows.Forms.RichTextBox();
            this.Polaczenie = new System.ComponentModel.BackgroundWorker();
            this.Odbieranie = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // cmdSluchaj
            // 
            this.cmdSluchaj.Location = new System.Drawing.Point(257, 35);
            this.cmdSluchaj.Name = "cmdSluchaj";
            this.cmdSluchaj.Size = new System.Drawing.Size(92, 38);
            this.cmdSluchaj.TabIndex = 19;
            this.cmdSluchaj.Text = "Czekaj na po³¹czenie";
            this.cmdSluchaj.UseVisualStyleBackColor = true;
            this.cmdSluchaj.Click += new System.EventHandler(this.cmdSluchaj_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(305, 9);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(44, 20);
            this.txtPort.TabIndex = 18;
            this.txtPort.Text = "8000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(254, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Nr portu";
            // 
            // txtOtrzymywane
            // 
            this.txtOtrzymywane.BackColor = System.Drawing.SystemColors.Window;
            this.txtOtrzymywane.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtOtrzymywane.Location = new System.Drawing.Point(12, 12);
            this.txtOtrzymywane.Name = "txtOtrzymywane";
            this.txtOtrzymywane.ReadOnly = true;
            this.txtOtrzymywane.Size = new System.Drawing.Size(239, 119);
            this.txtOtrzymywane.TabIndex = 16;
            this.txtOtrzymywane.Text = "";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.White;
            this.txtLog.ForeColor = System.Drawing.Color.Black;
            this.txtLog.Location = new System.Drawing.Point(15, 219);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(239, 30);
            this.txtLog.TabIndex = 15;
            this.txtLog.TabStop = false;
            this.txtLog.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Log";
            // 
            // cmdWyslij
            // 
            this.cmdWyslij.Enabled = false;
            this.cmdWyslij.Location = new System.Drawing.Point(257, 137);
            this.cmdWyslij.Name = "cmdWyslij";
            this.cmdWyslij.Size = new System.Drawing.Size(42, 23);
            this.cmdWyslij.TabIndex = 13;
            this.cmdWyslij.Text = "Wyslij";
            this.cmdWyslij.UseVisualStyleBackColor = true;
            this.cmdWyslij.Click += new System.EventHandler(this.cmdWyslij_Click);
            // 
            // txtWysylane
            // 
            this.txtWysylane.BackColor = System.Drawing.Color.White;
            this.txtWysylane.ForeColor = System.Drawing.Color.Black;
            this.txtWysylane.Location = new System.Drawing.Point(12, 137);
            this.txtWysylane.Name = "txtWysylane";
            this.txtWysylane.Size = new System.Drawing.Size(239, 63);
            this.txtWysylane.TabIndex = 12;
            this.txtWysylane.Text = "";
            this.txtWysylane.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWysylane_KeyPress);
            // 
            // Polaczenie
            // 
            this.Polaczenie.WorkerSupportsCancellation = true;
            this.Polaczenie.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Polaczenie_DoWork);
            // 
            // Odbieranie
            // 
            this.Odbieranie.WorkerSupportsCancellation = true;
            this.Odbieranie.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Odbieranie_DoWork);
            // 
            // frmSerwer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(356, 259);
            this.Controls.Add(this.cmdSluchaj);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOtrzymywane);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdWyslij);
            this.Controls.Add(this.txtWysylane);
            this.MaximizeBox = false;
            this.Name = "frmSerwer";
            this.Text = "Serwer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSerwer_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdSluchaj;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox txtOtrzymywane;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdWyslij;
        private System.Windows.Forms.RichTextBox txtWysylane;
        private System.ComponentModel.BackgroundWorker Polaczenie;
        private System.ComponentModel.BackgroundWorker Odbieranie;

    }
}

