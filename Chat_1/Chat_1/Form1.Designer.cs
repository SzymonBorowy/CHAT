namespace Chat_1
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Wymagana metoda obsługi projektanta — nie należy modyfikować 
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.start = new System.Windows.Forms.Button();
            this.wyslij = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.uzydkownikIP = new System.Windows.Forms.TextBox();
            this.uzydkownikPort = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.klientIP = new System.Windows.Forms.TextBox();
            this.klientPort = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.wiadomosc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(506, 41);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 39);
            this.start.TabIndex = 0;
            this.start.Text = "Start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // wyslij
            // 
            this.wyslij.Location = new System.Drawing.Point(506, 242);
            this.wyslij.Name = "wyslij";
            this.wyslij.Size = new System.Drawing.Size(75, 23);
            this.wyslij.TabIndex = 1;
            this.wyslij.Text = "Wyślij";
            this.wyslij.UseVisualStyleBackColor = true;
            this.wyslij.Click += new System.EventHandler(this.wyslij_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.uzydkownikIP);
            this.groupBox1.Controls.Add(this.uzydkownikPort);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Użytkownik ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "PORT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "IP";
            // 
            // uzydkownikIP
            // 
            this.uzydkownikIP.Location = new System.Drawing.Point(60, 19);
            this.uzydkownikIP.Name = "uzydkownikIP";
            this.uzydkownikIP.Size = new System.Drawing.Size(118, 20);
            this.uzydkownikIP.TabIndex = 1;
            // 
            // uzydkownikPort
            // 
            this.uzydkownikPort.Location = new System.Drawing.Point(60, 61);
            this.uzydkownikPort.Name = "uzydkownikPort";
            this.uzydkownikPort.Size = new System.Drawing.Size(118, 20);
            this.uzydkownikPort.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.klientIP);
            this.groupBox2.Controls.Add(this.klientPort);
            this.groupBox2.Location = new System.Drawing.Point(308, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(192, 100);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Klient";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "IP";
            // 
            // klientIP
            // 
            this.klientIP.Location = new System.Drawing.Point(60, 19);
            this.klientIP.Name = "klientIP";
            this.klientIP.Size = new System.Drawing.Size(118, 20);
            this.klientIP.TabIndex = 3;
            // 
            // klientPort
            // 
            this.klientPort.Location = new System.Drawing.Point(60, 64);
            this.klientPort.Name = "klientPort";
            this.klientPort.Size = new System.Drawing.Size(118, 20);
            this.klientPort.TabIndex = 4;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 118);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(488, 121);
            this.listBox1.TabIndex = 0;
            // 
            // wiadomosc
            // 
            this.wiadomosc.Location = new System.Drawing.Point(12, 245);
            this.wiadomosc.Name = "wiadomosc";
            this.wiadomosc.Size = new System.Drawing.Size(488, 20);
            this.wiadomosc.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "PORT";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 283);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.wiadomosc);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.wyslij);
            this.Controls.Add(this.start);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button wyslij;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox uzydkownikIP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox uzydkownikPort;
        private System.Windows.Forms.TextBox klientIP;
        private System.Windows.Forms.TextBox klientPort;
        private System.Windows.Forms.TextBox wiadomosc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}

