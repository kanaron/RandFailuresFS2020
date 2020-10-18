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
            oSimCon = new Simcon(this.Handle);
            oSimCon.Connect();
        }

        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == 0x402)
            {
                if (oSimCon.GetSimConnect() != null)
                {
                    oSimCon.GetSimConnect().ReceiveMessage();
                    //simconnect.ReceiveMessage();
                }
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }
    }
}
