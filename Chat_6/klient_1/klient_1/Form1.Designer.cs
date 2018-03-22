namespace klient_1
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
            this.btn_wyslij = new System.Windows.Forms.Button();
            this.txtWiadomosc = new System.Windows.Forms.TextBox();
            this.listLog = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btn_wyslij
            // 
            this.btn_wyslij.Location = new System.Drawing.Point(197, 38);
            this.btn_wyslij.Name = "btn_wyslij";
            this.btn_wyslij.Size = new System.Drawing.Size(75, 23);
            this.btn_wyslij.TabIndex = 0;
            this.btn_wyslij.Text = "Wyślij";
            this.btn_wyslij.UseVisualStyleBackColor = true;
            this.btn_wyslij.Click += new System.EventHandler(this.btn_wyslij_Click);
            // 
            // txtWiadomosc
            // 
            this.txtWiadomosc.Location = new System.Drawing.Point(12, 12);
            this.txtWiadomosc.Name = "txtWiadomosc";
            this.txtWiadomosc.Size = new System.Drawing.Size(260, 20);
            this.txtWiadomosc.TabIndex = 1;
            // 
            // listLog
            // 
            this.listLog.FormattingEnabled = true;
            this.listLog.Location = new System.Drawing.Point(12, 77);
            this.listLog.Name = "listLog";
            this.listLog.Size = new System.Drawing.Size(260, 95);
            this.listLog.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.listLog);
            this.Controls.Add(this.txtWiadomosc);
            this.Controls.Add(this.btn_wyslij);
            this.Name = "Form1";
            this.Text = "Klient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_wyslij;
        private System.Windows.Forms.TextBox txtWiadomosc;
        private System.Windows.Forms.ListBox listLog;
    }
}

