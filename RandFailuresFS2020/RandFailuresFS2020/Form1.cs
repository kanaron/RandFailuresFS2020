using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandFailuresFS2020
{
    public partial class Form1 : Form
    {
        ISimCon oSimCon = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(GetAll(this, typeof(NumericUpDown)).Count().ToString());
            oSimCon = new Simcon(this);
            oSimCon.Connect();
        }

        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == 0x402)
            {
                if (oSimCon.GetSimConnect() != null)
                {
                    oSimCon.GetSimConnect().ReceiveMessage();
                }
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            oSimCon.SetValue();
        }

        private void btnFailList_Click(object sender, EventArgs e)
        {
            List<SimVar> failList = oSimCon.getFailList();

            foreach (SimVar s in failList)
            {
                richTextBox1.Text += s.sName + " " + s.whenFail + " " + s.failureHeight + " " + s.failureTime + "\n";
            }
        }
    }
}
