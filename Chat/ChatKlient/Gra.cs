using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatKlient
{
    public partial class Gra : Form
    {
        private Klient kl;
        public Gra( Klient klient)
        {
            InitializeComponent();
            button1.Click += Button_Click;
            button2.Click += Button_Click;
            button3.Click += Button_Click;
            button4.Click += Button_Click;
            button5.Click += Button_Click;
            button6.Click += Button_Click;
            button7.Click += Button_Click;
            button8.Click += Button_Click;
            button9.Click += Button_Click;
            kl = klient;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            label1.Text = button.TabIndex.ToString();
            //kl.wyslijbuttona(button.TabIndex.ToString());

        }

    }
}
