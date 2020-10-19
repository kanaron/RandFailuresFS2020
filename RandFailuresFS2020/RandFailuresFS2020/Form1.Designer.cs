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
            this.fLeftFlap = new System.Windows.Forms.NumericUpDown();
            this.fRightFlap = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.fLeftFlap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fRightFlap)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(109, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // fLeftFlap
            // 
            this.fLeftFlap.Location = new System.Drawing.Point(59, 50);
            this.fLeftFlap.Name = "fLeftFlap";
            this.fLeftFlap.Size = new System.Drawing.Size(42, 20);
            this.fLeftFlap.TabIndex = 1;
            this.fLeftFlap.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            // 
            // fRightFlap
            // 
            this.fRightFlap.Location = new System.Drawing.Point(59, 76);
            this.fRightFlap.Name = "fRightFlap";
            this.fRightFlap.Size = new System.Drawing.Size(42, 20);
            this.fRightFlap.TabIndex = 2;
            this.fRightFlap.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 215);
            this.Controls.Add(this.fRightFlap);
            this.Controls.Add(this.fLeftFlap);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.fLeftFlap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fRightFlap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.NumericUpDown fLeftFlap;
        private System.Windows.Forms.NumericUpDown fRightFlap;
    }
}

