namespace RandFailuresFS2020
{
    partial class Form1
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.tcFailures = new System.Windows.Forms.TabControl();
            this.tpAvionics = new System.Windows.Forms.TabPage();
            this.tpElectrical = new System.Windows.Forms.TabPage();
            this.tpEngine1 = new System.Windows.Forms.TabPage();
            this.tpEngine2 = new System.Windows.Forms.TabPage();
            this.tpEngine3 = new System.Windows.Forms.TabPage();
            this.tpEngine4 = new System.Windows.Forms.TabPage();
            this.tpFuel = new System.Windows.Forms.TabPage();
            this.fLeftFlap = new System.Windows.Forms.NumericUpDown();
            this.fRightFlap = new System.Windows.Forms.NumericUpDown();
            this.tpBrakes = new System.Windows.Forms.TabPage();
            this.fE1Leak = new System.Windows.Forms.NumericUpDown();
            this.fE1Turbo = new System.Windows.Forms.NumericUpDown();
            this.fE1Fire = new System.Windows.Forms.NumericUpDown();
            this.fE2Fire = new System.Windows.Forms.NumericUpDown();
            this.fE2Turbo = new System.Windows.Forms.NumericUpDown();
            this.fE2Leak = new System.Windows.Forms.NumericUpDown();
            this.fE3Fire = new System.Windows.Forms.NumericUpDown();
            this.fE3Turbo = new System.Windows.Forms.NumericUpDown();
            this.fE3Leak = new System.Windows.Forms.NumericUpDown();
            this.fE4Fire = new System.Windows.Forms.NumericUpDown();
            this.fE4Turbo = new System.Windows.Forms.NumericUpDown();
            this.fE4Leak = new System.Windows.Forms.NumericUpDown();
            this.fFuelLeft = new System.Windows.Forms.NumericUpDown();
            this.fFuelRight = new System.Windows.Forms.NumericUpDown();
            this.fFuelCenter = new System.Windows.Forms.NumericUpDown();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnFailList = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tcFailures.SuspendLayout();
            this.tpAvionics.SuspendLayout();
            this.tpEngine1.SuspendLayout();
            this.tpEngine2.SuspendLayout();
            this.tpEngine3.SuspendLayout();
            this.tpEngine4.SuspendLayout();
            this.tpFuel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fLeftFlap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fRightFlap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE1Leak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE1Turbo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE1Fire)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE2Fire)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE2Turbo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE2Leak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE3Fire)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE3Turbo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE3Leak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE4Fire)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE4Turbo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE4Leak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fFuelLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fFuelRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fFuelCenter)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tcFailures
            // 
            this.tcFailures.Controls.Add(this.tabPage1);
            this.tcFailures.Controls.Add(this.tpAvionics);
            this.tcFailures.Controls.Add(this.tpBrakes);
            this.tcFailures.Controls.Add(this.tpElectrical);
            this.tcFailures.Controls.Add(this.tpFuel);
            this.tcFailures.Controls.Add(this.tpEngine1);
            this.tcFailures.Controls.Add(this.tpEngine2);
            this.tcFailures.Controls.Add(this.tpEngine3);
            this.tcFailures.Controls.Add(this.tpEngine4);
            this.tcFailures.Location = new System.Drawing.Point(12, 41);
            this.tcFailures.Name = "tcFailures";
            this.tcFailures.SelectedIndex = 0;
            this.tcFailures.Size = new System.Drawing.Size(470, 295);
            this.tcFailures.TabIndex = 1;
            // 
            // tpAvionics
            // 
            this.tpAvionics.Controls.Add(this.fRightFlap);
            this.tpAvionics.Controls.Add(this.fLeftFlap);
            this.tpAvionics.Location = new System.Drawing.Point(4, 22);
            this.tpAvionics.Name = "tpAvionics";
            this.tpAvionics.Padding = new System.Windows.Forms.Padding(3);
            this.tpAvionics.Size = new System.Drawing.Size(462, 178);
            this.tpAvionics.TabIndex = 0;
            this.tpAvionics.Text = "Avionics";
            this.tpAvionics.UseVisualStyleBackColor = true;
            // 
            // tpElectrical
            // 
            this.tpElectrical.Location = new System.Drawing.Point(4, 22);
            this.tpElectrical.Name = "tpElectrical";
            this.tpElectrical.Padding = new System.Windows.Forms.Padding(3);
            this.tpElectrical.Size = new System.Drawing.Size(462, 178);
            this.tpElectrical.TabIndex = 1;
            this.tpElectrical.Text = "Electrical";
            this.tpElectrical.UseVisualStyleBackColor = true;
            // 
            // tpEngine1
            // 
            this.tpEngine1.Controls.Add(this.fE1Fire);
            this.tpEngine1.Controls.Add(this.fE1Turbo);
            this.tpEngine1.Controls.Add(this.fE1Leak);
            this.tpEngine1.Location = new System.Drawing.Point(4, 22);
            this.tpEngine1.Name = "tpEngine1";
            this.tpEngine1.Size = new System.Drawing.Size(462, 178);
            this.tpEngine1.TabIndex = 2;
            this.tpEngine1.Text = "Engine1";
            this.tpEngine1.UseVisualStyleBackColor = true;
            // 
            // tpEngine2
            // 
            this.tpEngine2.Controls.Add(this.fE2Fire);
            this.tpEngine2.Controls.Add(this.fE2Turbo);
            this.tpEngine2.Controls.Add(this.fE2Leak);
            this.tpEngine2.Location = new System.Drawing.Point(4, 22);
            this.tpEngine2.Name = "tpEngine2";
            this.tpEngine2.Size = new System.Drawing.Size(462, 178);
            this.tpEngine2.TabIndex = 3;
            this.tpEngine2.Text = "Engine2";
            this.tpEngine2.UseVisualStyleBackColor = true;
            // 
            // tpEngine3
            // 
            this.tpEngine3.Controls.Add(this.fE3Fire);
            this.tpEngine3.Controls.Add(this.fE3Turbo);
            this.tpEngine3.Controls.Add(this.fE3Leak);
            this.tpEngine3.Location = new System.Drawing.Point(4, 22);
            this.tpEngine3.Name = "tpEngine3";
            this.tpEngine3.Size = new System.Drawing.Size(462, 178);
            this.tpEngine3.TabIndex = 4;
            this.tpEngine3.Text = "Engine3";
            this.tpEngine3.UseVisualStyleBackColor = true;
            // 
            // tpEngine4
            // 
            this.tpEngine4.Controls.Add(this.fE4Fire);
            this.tpEngine4.Controls.Add(this.fE4Turbo);
            this.tpEngine4.Controls.Add(this.fE4Leak);
            this.tpEngine4.Location = new System.Drawing.Point(4, 22);
            this.tpEngine4.Name = "tpEngine4";
            this.tpEngine4.Size = new System.Drawing.Size(462, 234);
            this.tpEngine4.TabIndex = 5;
            this.tpEngine4.Text = "Engine4";
            this.tpEngine4.UseVisualStyleBackColor = true;
            // 
            // tpFuel
            // 
            this.tpFuel.Controls.Add(this.fFuelLeft);
            this.tpFuel.Controls.Add(this.fFuelRight);
            this.tpFuel.Controls.Add(this.fFuelCenter);
            this.tpFuel.Location = new System.Drawing.Point(4, 22);
            this.tpFuel.Name = "tpFuel";
            this.tpFuel.Size = new System.Drawing.Size(462, 178);
            this.tpFuel.TabIndex = 6;
            this.tpFuel.Text = "Fuel";
            this.tpFuel.UseVisualStyleBackColor = true;
            // 
            // fLeftFlap
            // 
            this.fLeftFlap.Location = new System.Drawing.Point(40, 21);
            this.fLeftFlap.Name = "fLeftFlap";
            this.fLeftFlap.Size = new System.Drawing.Size(40, 20);
            this.fLeftFlap.TabIndex = 0;
            this.fLeftFlap.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // fRightFlap
            // 
            this.fRightFlap.Location = new System.Drawing.Point(40, 47);
            this.fRightFlap.Name = "fRightFlap";
            this.fRightFlap.Size = new System.Drawing.Size(40, 20);
            this.fRightFlap.TabIndex = 1;
            this.fRightFlap.Value = new decimal(new int[] {
            99,
            0,
            0,
            0});
            // 
            // tpBrakes
            // 
            this.tpBrakes.Location = new System.Drawing.Point(4, 22);
            this.tpBrakes.Name = "tpBrakes";
            this.tpBrakes.Size = new System.Drawing.Size(462, 178);
            this.tpBrakes.TabIndex = 7;
            this.tpBrakes.Text = "Brakes";
            this.tpBrakes.UseVisualStyleBackColor = true;
            // 
            // fE1Leak
            // 
            this.fE1Leak.Location = new System.Drawing.Point(67, 19);
            this.fE1Leak.Name = "fE1Leak";
            this.fE1Leak.Size = new System.Drawing.Size(40, 20);
            this.fE1Leak.TabIndex = 1;
            this.fE1Leak.Value = new decimal(new int[] {
            95,
            0,
            0,
            0});
            // 
            // fE1Turbo
            // 
            this.fE1Turbo.Location = new System.Drawing.Point(67, 45);
            this.fE1Turbo.Name = "fE1Turbo";
            this.fE1Turbo.Size = new System.Drawing.Size(40, 20);
            this.fE1Turbo.TabIndex = 2;
            this.fE1Turbo.Value = new decimal(new int[] {
            94,
            0,
            0,
            0});
            // 
            // fE1Fire
            // 
            this.fE1Fire.Location = new System.Drawing.Point(67, 71);
            this.fE1Fire.Name = "fE1Fire";
            this.fE1Fire.Size = new System.Drawing.Size(40, 20);
            this.fE1Fire.TabIndex = 3;
            this.fE1Fire.Value = new decimal(new int[] {
            93,
            0,
            0,
            0});
            // 
            // fE2Fire
            // 
            this.fE2Fire.Location = new System.Drawing.Point(68, 71);
            this.fE2Fire.Name = "fE2Fire";
            this.fE2Fire.Size = new System.Drawing.Size(40, 20);
            this.fE2Fire.TabIndex = 6;
            this.fE2Fire.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // fE2Turbo
            // 
            this.fE2Turbo.Location = new System.Drawing.Point(68, 45);
            this.fE2Turbo.Name = "fE2Turbo";
            this.fE2Turbo.Size = new System.Drawing.Size(40, 20);
            this.fE2Turbo.TabIndex = 5;
            this.fE2Turbo.Value = new decimal(new int[] {
            91,
            0,
            0,
            0});
            // 
            // fE2Leak
            // 
            this.fE2Leak.Location = new System.Drawing.Point(68, 19);
            this.fE2Leak.Name = "fE2Leak";
            this.fE2Leak.Size = new System.Drawing.Size(40, 20);
            this.fE2Leak.TabIndex = 4;
            this.fE2Leak.Value = new decimal(new int[] {
            92,
            0,
            0,
            0});
            // 
            // fE3Fire
            // 
            this.fE3Fire.Location = new System.Drawing.Point(58, 71);
            this.fE3Fire.Name = "fE3Fire";
            this.fE3Fire.Size = new System.Drawing.Size(40, 20);
            this.fE3Fire.TabIndex = 6;
            this.fE3Fire.Value = new decimal(new int[] {
            87,
            0,
            0,
            0});
            // 
            // fE3Turbo
            // 
            this.fE3Turbo.Location = new System.Drawing.Point(58, 45);
            this.fE3Turbo.Name = "fE3Turbo";
            this.fE3Turbo.Size = new System.Drawing.Size(40, 20);
            this.fE3Turbo.TabIndex = 5;
            this.fE3Turbo.Value = new decimal(new int[] {
            88,
            0,
            0,
            0});
            // 
            // fE3Leak
            // 
            this.fE3Leak.Location = new System.Drawing.Point(58, 19);
            this.fE3Leak.Name = "fE3Leak";
            this.fE3Leak.Size = new System.Drawing.Size(40, 20);
            this.fE3Leak.TabIndex = 4;
            this.fE3Leak.Value = new decimal(new int[] {
            89,
            0,
            0,
            0});
            // 
            // fE4Fire
            // 
            this.fE4Fire.Location = new System.Drawing.Point(71, 70);
            this.fE4Fire.Name = "fE4Fire";
            this.fE4Fire.Size = new System.Drawing.Size(40, 20);
            this.fE4Fire.TabIndex = 6;
            this.fE4Fire.Value = new decimal(new int[] {
            84,
            0,
            0,
            0});
            // 
            // fE4Turbo
            // 
            this.fE4Turbo.Location = new System.Drawing.Point(71, 44);
            this.fE4Turbo.Name = "fE4Turbo";
            this.fE4Turbo.Size = new System.Drawing.Size(40, 20);
            this.fE4Turbo.TabIndex = 5;
            this.fE4Turbo.Value = new decimal(new int[] {
            85,
            0,
            0,
            0});
            // 
            // fE4Leak
            // 
            this.fE4Leak.Location = new System.Drawing.Point(71, 18);
            this.fE4Leak.Name = "fE4Leak";
            this.fE4Leak.Size = new System.Drawing.Size(40, 20);
            this.fE4Leak.TabIndex = 4;
            this.fE4Leak.Value = new decimal(new int[] {
            86,
            0,
            0,
            0});
            // 
            // fFuelLeft
            // 
            this.fFuelLeft.Location = new System.Drawing.Point(62, 69);
            this.fFuelLeft.Name = "fFuelLeft";
            this.fFuelLeft.Size = new System.Drawing.Size(40, 20);
            this.fFuelLeft.TabIndex = 6;
            this.fFuelLeft.Value = new decimal(new int[] {
            96,
            0,
            0,
            0});
            // 
            // fFuelRight
            // 
            this.fFuelRight.Location = new System.Drawing.Point(62, 43);
            this.fFuelRight.Name = "fFuelRight";
            this.fFuelRight.Size = new System.Drawing.Size(40, 20);
            this.fFuelRight.TabIndex = 5;
            this.fFuelRight.Value = new decimal(new int[] {
            97,
            0,
            0,
            0});
            // 
            // fFuelCenter
            // 
            this.fFuelCenter.Location = new System.Drawing.Point(62, 17);
            this.fFuelCenter.Name = "fFuelCenter";
            this.fFuelCenter.Size = new System.Drawing.Size(40, 20);
            this.fFuelCenter.TabIndex = 4;
            this.fFuelCenter.Value = new decimal(new int[] {
            98,
            0,
            0,
            0});
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Controls.Add(this.btnFailList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(462, 269);
            this.tabPage1.TabIndex = 8;
            this.tabPage1.Text = "Test";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnFailList
            // 
            this.btnFailList.Location = new System.Drawing.Point(369, 14);
            this.btnFailList.Name = "btnFailList";
            this.btnFailList.Size = new System.Drawing.Size(75, 23);
            this.btnFailList.TabIndex = 0;
            this.btnFailList.Text = "Take fail list";
            this.btnFailList.UseVisualStyleBackColor = true;
            this.btnFailList.Click += new System.EventHandler(this.btnFailList_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(360, 263);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 348);
            this.Controls.Add(this.tcFailures);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tcFailures.ResumeLayout(false);
            this.tpAvionics.ResumeLayout(false);
            this.tpEngine1.ResumeLayout(false);
            this.tpEngine2.ResumeLayout(false);
            this.tpEngine3.ResumeLayout(false);
            this.tpEngine4.ResumeLayout(false);
            this.tpFuel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fLeftFlap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fRightFlap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE1Leak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE1Turbo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE1Fire)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE2Fire)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE2Turbo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE2Leak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE3Fire)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE3Turbo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE3Leak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE4Fire)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE4Turbo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fE4Leak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fFuelLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fFuelRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fFuelCenter)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TabControl tcFailures;
        private System.Windows.Forms.TabPage tpAvionics;
        private System.Windows.Forms.NumericUpDown fLeftFlap;
        private System.Windows.Forms.TabPage tpElectrical;
        private System.Windows.Forms.TabPage tpFuel;
        private System.Windows.Forms.TabPage tpEngine1;
        private System.Windows.Forms.TabPage tpEngine2;
        private System.Windows.Forms.TabPage tpEngine3;
        private System.Windows.Forms.TabPage tpEngine4;
        private System.Windows.Forms.NumericUpDown fRightFlap;
        private System.Windows.Forms.TabPage tpBrakes;
        private System.Windows.Forms.NumericUpDown fE1Fire;
        private System.Windows.Forms.NumericUpDown fE1Turbo;
        private System.Windows.Forms.NumericUpDown fE1Leak;
        private System.Windows.Forms.NumericUpDown fE2Fire;
        private System.Windows.Forms.NumericUpDown fE2Turbo;
        private System.Windows.Forms.NumericUpDown fE2Leak;
        private System.Windows.Forms.NumericUpDown fE3Fire;
        private System.Windows.Forms.NumericUpDown fE3Turbo;
        private System.Windows.Forms.NumericUpDown fE3Leak;
        private System.Windows.Forms.NumericUpDown fE4Fire;
        private System.Windows.Forms.NumericUpDown fE4Turbo;
        private System.Windows.Forms.NumericUpDown fE4Leak;
        private System.Windows.Forms.NumericUpDown fFuelLeft;
        private System.Windows.Forms.NumericUpDown fFuelRight;
        private System.Windows.Forms.NumericUpDown fFuelCenter;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnFailList;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

