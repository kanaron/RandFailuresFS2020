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
        ISimCon oSimCon;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            oSimCon = new Simcon();
            oSimCon.Connect(this.Handle);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            oSimCon.updateData();
            oSimCon.setValue();
        }
    }
}
