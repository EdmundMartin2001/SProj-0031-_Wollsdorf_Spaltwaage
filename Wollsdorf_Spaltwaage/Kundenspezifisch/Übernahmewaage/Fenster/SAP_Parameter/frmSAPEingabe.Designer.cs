namespace Wollsdorf.Spaltwaage
{
    partial class frmSAPEingabe
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
            this.tbBestellnummer = new System.Windows.Forms.TextBox();
            this.tbLieferantennummer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dispTopLabelLeft = new System.Windows.Forms.Label();
            this.dispTopLabelRight = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlLoadingMessage = new System.Windows.Forms.Panel();
            this.dispLoadMessage = new System.Windows.Forms.Label();
            this.dispErrorText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdsimu = new System.Windows.Forms.Button();
            this.ctrlDoubleNumPad1 = new Allgemein.Controls.ctrlDoubleNumPad();
            this.ctrlButtonBar1 = new Allgemein.Controls.ctrlButtonBar();
            this.pnlTop.SuspendLayout();
            this.pnlLoadingMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbBestellnummer
            // 
            this.tbBestellnummer.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular);
            this.tbBestellnummer.Location = new System.Drawing.Point(294, 285);
            this.tbBestellnummer.MaxLength = 10;
            this.tbBestellnummer.Name = "tbBestellnummer";
            this.tbBestellnummer.Size = new System.Drawing.Size(201, 37);
            this.tbBestellnummer.TabIndex = 13;
            this.tbBestellnummer.WordWrap = false;
            // 
            // tbLieferantennummer
            // 
            this.tbLieferantennummer.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular);
            this.tbLieferantennummer.Location = new System.Drawing.Point(294, 242);
            this.tbLieferantennummer.MaxLength = 3;
            this.tbLieferantennummer.Name = "tbLieferantennummer";
            this.tbLieferantennummer.Size = new System.Drawing.Size(201, 37);
            this.tbLieferantennummer.TabIndex = 3;
            this.tbLieferantennummer.WordWrap = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(15, 285);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 39);
            this.label1.Text = "Bestellnummer:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(15, 242);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(250, 39);
            this.label4.Text = "Lieferantennummer:";
            // 
            // dispTopLabelLeft
            // 
            this.dispTopLabelLeft.Location = new System.Drawing.Point(4, 0);
            this.dispTopLabelLeft.Name = "dispTopLabelLeft";
            this.dispTopLabelLeft.Size = new System.Drawing.Size(354, 16);
            this.dispTopLabelLeft.Text = "Test Applikation 1.0";
            // 
            // dispTopLabelRight
            // 
            this.dispTopLabelRight.BackColor = System.Drawing.Color.Gainsboro;
            this.dispTopLabelRight.Location = new System.Drawing.Point(795, 0);
            this.dispTopLabelRight.Name = "dispTopLabelRight";
            this.dispTopLabelRight.Size = new System.Drawing.Size(217, 16);
            this.dispTopLabelRight.Text = "Terminal ID 99";
            this.dispTopLabelRight.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlTop.Controls.Add(this.dispTopLabelRight);
            this.pnlTop.Controls.Add(this.dispTopLabelLeft);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1024, 19);
            // 
            // pnlLoadingMessage
            // 
            this.pnlLoadingMessage.Controls.Add(this.dispLoadMessage);
            this.pnlLoadingMessage.Location = new System.Drawing.Point(4, 439);
            this.pnlLoadingMessage.Name = "pnlLoadingMessage";
            this.pnlLoadingMessage.Size = new System.Drawing.Size(574, 68);
            // 
            // dispLoadMessage
            // 
            this.dispLoadMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dispLoadMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dispLoadMessage.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.dispLoadMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dispLoadMessage.Location = new System.Drawing.Point(0, 0);
            this.dispLoadMessage.Name = "dispLoadMessage";
            this.dispLoadMessage.Size = new System.Drawing.Size(574, 68);
            this.dispLoadMessage.Text = "label2";
            this.dispLoadMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.dispLoadMessage.Visible = false;
            // 
            // dispErrorText
            // 
            this.dispErrorText.BackColor = System.Drawing.Color.Red;
            this.dispErrorText.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.dispErrorText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dispErrorText.Location = new System.Drawing.Point(4, 430);
            this.dispErrorText.Name = "dispErrorText";
            this.dispErrorText.Size = new System.Drawing.Size(570, 64);
            this.dispErrorText.Text = "label2";
            this.dispErrorText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.dispErrorText.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Location = new System.Drawing.Point(0, 217);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 4);
            // 
            // cmdsimu
            // 
            this.cmdsimu.Location = new System.Drawing.Point(527, 245);
            this.cmdsimu.Name = "cmdsimu";
            this.cmdsimu.Size = new System.Drawing.Size(29, 18);
            this.cmdsimu.TabIndex = 37;
            this.cmdsimu.Text = "x";
            this.cmdsimu.Click += new System.EventHandler(this.cmdsimu_Click);
            // 
            // ctrlDoubleNumPad1
            // 
            this.ctrlDoubleNumPad1.Location = new System.Drawing.Point(590, 242);
            this.ctrlDoubleNumPad1.Name = "ctrlDoubleNumPad1";
            this.ctrlDoubleNumPad1.Size = new System.Drawing.Size(429, 253);
            this.ctrlDoubleNumPad1.TabIndex = 19;
            // 
            // ctrlButtonBar1
            // 
            this.ctrlButtonBar1.BackColor = System.Drawing.Color.White;
            this.ctrlButtonBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlButtonBar1.Location = new System.Drawing.Point(0, 513);
            this.ctrlButtonBar1.Name = "ctrlButtonBar1";
            this.ctrlButtonBar1.Size = new System.Drawing.Size(1024, 87);
            this.ctrlButtonBar1.TabIndex = 15;
            // 
            // frmSAPEingabe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this.ControlBox = false;
            this.Controls.Add(this.cmdsimu);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlLoadingMessage);
            this.Controls.Add(this.ctrlDoubleNumPad1);
            this.Controls.Add(this.tbBestellnummer);
            this.Controls.Add(this.tbLieferantennummer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.ctrlButtonBar1);
            this.Controls.Add(this.dispErrorText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSAPEingabe";
            this.Load += new System.EventHandler(this.frmSAPEingabe_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlLoadingMessage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label dispTopLabelLeft;
        private System.Windows.Forms.Label dispTopLabelRight;
        private System.Windows.Forms.Panel pnlTop;
        private Allgemein.Controls.ctrlButtonBar ctrlButtonBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLieferantennummer;
        private System.Windows.Forms.TextBox tbBestellnummer;
        private Allgemein.Controls.ctrlDoubleNumPad ctrlDoubleNumPad1;
        private System.Windows.Forms.Panel pnlLoadingMessage;
        private System.Windows.Forms.Label dispLoadMessage;
        private System.Windows.Forms.Label dispErrorText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdsimu;

    }
}