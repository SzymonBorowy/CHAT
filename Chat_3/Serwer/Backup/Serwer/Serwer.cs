using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using komunikaty;

namespace Serwer
{
    public partial class frmSerwer : Form
    {
        private TcpListener listener = null;
        private TcpClient klient = null;
        private bool czypolaczono = false;
        private BinaryReader r = null;
        private BinaryWriter w = null;
        
        public frmSerwer()
        {
            InitializeComponent();
        }

        public void wyswietl(RichTextBox o, string tekst)
        {
            o.Focus();
            o.AppendText(tekst);
            o.ScrollToCaret();
            txtWysylane.Focus();
        }

        private void Polaczenie_DoWork(object sender, DoWorkEventArgs e)
        {
            wyswietl(txtLog, "Czekam na po³aczenie\n");
            listener = new TcpListener(int.Parse(txtPort.Text));
            listener.Start();
            while (!listener.Pending())
            {
                if (this.Polaczenie.CancellationPending)
                {
                    if (klient != null) klient.Close();
                    listener.Stop();
                    czypolaczono = false;
                    cmdSluchaj.Text = "Czekaj na po³¹czenie";
                    return;
                }
            }
            klient = listener.AcceptTcpClient();
            wyswietl(txtLog, "Za¿¹dano po³¹czenia\n");
            NetworkStream stream = klient.GetStream();
            w = new BinaryWriter(stream);
            r = new BinaryReader(stream);
            if (r.ReadString() == KomunikatyKlienta.Zadaj)
            {
                w.Write(KomunikatySerwera.OK);
                wyswietl(txtLog, "Po³¹czono\n");
                czypolaczono = true;
                cmdWyslij.Enabled = true;
                Odbieranie.RunWorkerAsync();
            }
            else
            {
                wyswietl(txtLog, "Klient odrzucony\nRozlaczono\n");
                if (klient != null) klient.Close();
                listener.Stop();
                czypolaczono = false;
            }
        }

        private void Odbieranie_DoWork(object sender, DoWorkEventArgs e)
        {
            string tekst;
            while ((tekst = r.ReadString()) != KomunikatyKlienta.Rozlacz)
                wyswietl(txtOtrzymywane, "===== Rozmówca =====\n" + tekst + '\n');
            wyswietl(txtLog, "Rozlaczono\n");
            czypolaczono = false;
            klient.Close();
            listener.Stop();
            cmdSluchaj.Text = "Czekaj na po³¹czenie";
        }

        private void cmdWyslij_Click(object sender, EventArgs e)
        {
            string tekst = txtWysylane.Text;
            if (tekst == "") { txtWysylane.Focus(); return; }
            if (tekst[tekst.Length - 1] == '\n')
                tekst = tekst.TrimEnd('\n');
            w.Write(tekst);
            wyswietl(txtOtrzymywane, "===== Ja =====\n" + tekst + '\n');
            txtWysylane.Text = "";
        }

        private void txtWysylane_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmdWyslij.Enabled && e.KeyChar == (char)13) cmdWyslij_Click(sender, e);
        }

        private void cmdSluchaj_Click(object sender, EventArgs e)
        {
            if (cmdSluchaj.Text == "Czekaj na po³¹czenie")
            {
                Polaczenie.RunWorkerAsync();
                cmdSluchaj.Text = "Roz³¹cz";
            }
            else
            {
                if (czypolaczono)
                {
                    w.Write(KomunikatySerwera.Rozlacz);
                    listener.Stop();
                    if (klient != null) klient.Close();
                    czypolaczono = false;
                }
                wyswietl(txtLog, "Rozlaczono\n");
                cmdWyslij.Enabled = false;
                cmdSluchaj.Text = "Czekaj na po³¹czenie";
                cmdWyslij.Enabled = false;
                Polaczenie.CancelAsync();
                Odbieranie.CancelAsync();
            }
        }

        private void frmSerwer_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (czypolaczono)
            {
                w.Write(KomunikatySerwera.Rozlacz);
                listener.Stop();
                if (klient != null) klient.Close();
                czypolaczono = false;
            }
            Polaczenie.CancelAsync();
            Odbieranie.CancelAsync();
        }
    }
}