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

namespace Klient
{
    public partial class Klient : Form
    {
        private TcpClient klient = null;
        private bool czypolaczono = false;
        private BinaryReader r = null;
        private BinaryWriter w = null;
        
        public Klient()
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
            klient = new TcpClient();
            wyswietl(txtLog, "Próbuje siê po³¹czyæ\n");
            klient.Connect(IPAddress.Parse(txtIP.Text), int.Parse(txtPort.Text));
            wyswietl(txtLog, "Po³¹czenie nawi¹zane\n¯¹dam zezwolenia\n");
            NetworkStream stream = klient.GetStream();
            w = new BinaryWriter(stream);
            r = new BinaryReader(stream);
            w.Write(KomunikatyKlienta.Zadaj);
            if (r.ReadString() == KomunikatySerwera.OK)
            {
                wyswietl(txtLog,"Po³¹czono\n");
                czypolaczono = true;
                cmdWyslij.Enabled = true;
                Odbieranie.RunWorkerAsync();
            }
            else
            {
                wyswietl(txtLog, "Brak odpowiedzi\nRozlaczono\n");
                czypolaczono = false;
                if (klient != null) klient.Close();
                cmdWyslij.Enabled = false;
                cmdPolacz.Text = "Po³¹cz";
            }
        }

        private void Odbieranie_DoWork(object sender, DoWorkEventArgs e)
        {
            string tekst;
            while ((tekst = r.ReadString()) != KomunikatySerwera.Rozlacz)
            {
                wyswietl(txtOtrzymywane, "===== Rozmówca =====\n" + tekst + '\n');
            }
            cmdWyslij.Enabled = false;
            wyswietl(txtLog, "Rozlaczono\n");
            cmdPolacz.Text = "Po³¹cz";
            czypolaczono = false;
            if (klient != null) klient.Close();
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

        private void cmdPolacz_Click(object sender, EventArgs e)
        {
            if (cmdPolacz.Text == "Po³¹cz")
            {
                Polaczenie.RunWorkerAsync();
                cmdPolacz.Text = "Roz³¹cz";
            }
            else
            {
                if (czypolaczono)
                {
                    w.Write(KomunikatyKlienta.Rozlacz);
                    klient.Close();
                    czypolaczono = false;
                }
                cmdPolacz.Text = "Po³¹cz";
                cmdWyslij.Enabled = false;
                wyswietl(txtLog, "Rozlaczono\n");
            }
        }

        private void Klient_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (czypolaczono)
            {
                w.Write(KomunikatyKlienta.Rozlacz);
                klient.Close();
                czypolaczono = false;
            }
            Polaczenie.CancelAsync();
            Odbieranie.CancelAsync();
        }
    }
}