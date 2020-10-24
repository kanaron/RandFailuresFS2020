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
            tcFailures.TabPages.Remove(tpEvents);

            foreach (Control c in GetAll(this, typeof(NumericUpDown)))
            {
                if (c is NumericUpDown)
                    if (c.Name.Contains("All"))
                        ((NumericUpDown)c).Value = nruAll.Value;
            }

            oSimCon = new Simcon(this);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Connect")
            {
                if (checkMinMax())
                {
                    statusLabel.Text = oSimCon.Connect();

                    if (statusLabel.Text == "SimConnect connected")
                    {
                        btnConnect.Text = "Disconnect";
                        connectToolStripMenuItem.Text = "Disconnect";
                        btnStart.Enabled = true;
                        StartToolStripMenuItem.Enabled = false;
                    }
                }
            }
            else
            {
                statusLabel.Text = oSimCon.Disconnect();
                btnConnect.Text = "Connect";
                connectToolStripMenuItem.Text = "Connect";
                btnStart.Enabled = false;
                StartToolStripMenuItem.Enabled = false;
                btnStop.Enabled = false;
                stopToolStripMenuItem.Enabled = false;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (checkMinMax())
            {
                oSimCon.Disconnect();
                oSimCon = null;
                oSimCon = new Simcon(this);
                oSimCon.Connect();
                oSimCon.setMinAlt((int)nruMinAlt.Value);
                oSimCon.setMaxAlt((int)nruMaxAlt.Value);
                oSimCon.setMinTime((int)nruMinTime.Value);
                oSimCon.setMaxTime((int)nruMaxTime.Value);
                oSimCon.setMaxNoFails((int)nruNoFails.Value);
                if (!cbInstant.Checked && !cbTaxi.Checked && !cbTime.Checked && !cbAlt.Checked)
                {
                    MessageBox.Show("At least one checkbox in \"When fail can occur\" have to be checked", "Error", MessageBoxButtons.OK);
                }
                else
                {
                    oSimCon.setWhenFail(cbInstant.Checked, cbTaxi.Checked, cbTime.Checked, cbAlt.Checked);
                    oSimCon.prepareFailures();

                    btnStop.Enabled = true;
                    stopToolStripMenuItem.Enabled = true;
                    btnStart.Enabled = false;
                    StartToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            oSimCon.stopTimers();
            btnStop.Enabled = false;
            stopToolStripMenuItem.Enabled = false;
            btnStart.Enabled = true;
            StartToolStripMenuItem.Enabled = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oSimCon.Disconnect();
            Application.Exit();
        }

        private void btnFailList_Click(object sender, EventArgs e)
        {
            List<SimVar> failList = oSimCon.getFailList();

            richTextBox1.Clear();

            string altTime = "";

            foreach (SimVar s in failList)
            {
                if (s.whenFail == WHEN_FAIL.ALT)
                {
                    altTime = "at " + s.failureHeight.ToString() + " ft";
                }
                else if (s.whenFail == WHEN_FAIL.TIME)
                {
                    altTime = "after " + s.failureTime.ToString() + " seconds";
                }

                richTextBox1.Text += "Name: " + s.sName + " when will fail: " + s.whenFail + " " + altTime + "\n";
            }
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
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

        #region setOptions
        private void nruMinAlt_ValueChanged(object sender, EventArgs e)
        {
            if (checkMinMax())
                oSimCon.setMinAlt((int)nruMinAlt.Value);
        }

        private void nruMinTime_ValueChanged(object sender, EventArgs e)
        {
            if (checkMinMax())
                oSimCon.setMinTime((int)nruMinTime.Value);
        }

        private void nruMaxAlt_ValueChanged(object sender, EventArgs e)
        {
            if (checkMinMax())
                oSimCon.setMaxAlt((int)nruMaxAlt.Value);
        }

        private void nruMaxTime_ValueChanged(object sender, EventArgs e)
        {
            if (checkMinMax())
                oSimCon.setMaxTime((int)nruMaxTime.Value);
        }

        bool checkMinMax()
        {
            if ((nruMaxAlt.Value < nruMinAlt.Value) || (nruMaxTime.Value < nruMinTime.Value))
            {
                MessageBox.Show("Min value can not be greater than max value", "Error", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void nruNoFails_ValueChanged(object sender, EventArgs e)
        {
            oSimCon.setMaxNoFails((int)nruNoFails.Value);
        }

        private void GitLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GitLink.LinkVisited = true;

            System.Diagnostics.Process.Start("https://github.com/kanaron/RandFailuresFS2020");
        }
        #endregion

        #region setAllBox
        private void nruAllPanel_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control c in tpPanel.Controls)
            {
                if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = nruAllPanel.Value;
            }
        }

        private void nruE1All_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control c in GetAll(gbEngine1, typeof(NumericUpDown)))
            {
                if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = nruE1All.Value;
            }
        }

        private void nruE2All_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control c in GetAll(gbEngine2, typeof(NumericUpDown)))
            {
                if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = nruE2All.Value;
            }
        }

        private void nruE3All_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control c in GetAll(gbEngine3, typeof(NumericUpDown)))
            {
                if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = nruE3All.Value;
            }
        }

        private void nruE4All_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control c in GetAll(gbEngine4, typeof(NumericUpDown)))
            {
                if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = nruE4All.Value;
            }
        }

        private void nruEnginesAll_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control c in tpEngine1.Controls)
            {
                if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = nruEnginesAll.Value;
            }
        }

        private void nruAvionicsAll_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control c in GetAll(gbAvionics, typeof(NumericUpDown)))
            {
                if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = nruAvionicsAll.Value;
            }
        }

        private void nruGearAll_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control c in GetAll(gbGear, typeof(NumericUpDown)))
            {
                if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = nruGearAll.Value;
            }
        }

        private void nruFuelAll_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control c in GetAll(gbFuel, typeof(NumericUpDown)))
            {
                if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = nruFuelAll.Value;
            }
        }

        private void nruBrakesAll_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control c in GetAll(gbBrakes, typeof(NumericUpDown)))
            {
                if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = nruBrakesAll.Value;
            }
        }

        private void nruSystemAll_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control c in tpAvionics.Controls)
            {
                if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = nruSystemAll.Value;
            }
        }

        private void nruPanelAll_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control c in GetAll(gbPanel, typeof(NumericUpDown)))
            {
                if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = nruPanelAll.Value;
            }
        }

        private void nruOtherAll_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control c in GetAll(gbOther, typeof(NumericUpDown)))
            {
                if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = nruOtherAll.Value;
            }
        }

        private void nruAll_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control c in GetAll(this, typeof(NumericUpDown)))
            {
                if (c is NumericUpDown)
                    if (c.Name.Contains("All"))
                        ((NumericUpDown)c).Value = nruAll.Value;
            }
        }
        #endregion
    }
}
