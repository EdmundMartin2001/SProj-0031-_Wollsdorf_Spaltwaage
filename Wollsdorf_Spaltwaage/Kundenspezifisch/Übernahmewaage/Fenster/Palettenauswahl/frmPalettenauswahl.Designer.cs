using Wollsdorf_Spaltwaage.Allgemein.ButtonBar;
using Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Controls;

namespace Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Fenster.Palettenauswahl
{
    partial class frmPalettenauswahl
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
            this.dispTopLabelLeft = new System.Windows.Forms.Label();
            this.dispTopLabelRight = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctrlButtonBar1 = new ctrlButtonBar();
            this.ctrlPalette1 = new ctrlPalette();
            this.ctrlPalette2 = new ctrlPalette();
            this.ctrlPalette3 = new ctrlPalette();
            this.ctrlPalette4 = new ctrlPalette();
            this.ctrlPalette5 = new ctrlPalette();
            this.ctrlPalette6 = new ctrlPalette();
            this.dispInfo = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer();
            this.dispErrorText = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
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
            this.pnlTop.Size = new System.Drawing.Size(1015, 19);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Location = new System.Drawing.Point(0, 217);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 4);
            // 
            // ctrlButtonBar1
            // 
            this.ctrlButtonBar1.BackColor = System.Drawing.Color.White;
            this.ctrlButtonBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlButtonBar1.Location = new System.Drawing.Point(0, 488);
            this.ctrlButtonBar1.Name = "ctrlButtonBar1";
            this.ctrlButtonBar1.Size = new System.Drawing.Size(1015, 87);
            this.ctrlButtonBar1.TabIndex = 39;
            // 
            // ctrlPalette1
            // 
            this.ctrlPalette1.Location = new System.Drawing.Point(35, 227);
            this.ctrlPalette1.Name = "ctrlPalette1";
            this.ctrlPalette1.Size = new System.Drawing.Size(150, 150);
            this.ctrlPalette1.TabIndex = 44;
            this.ctrlPalette1.Click += new System.EventHandler(this.ctrlPalette1_Click);
            // 
            // ctrlPalette2
            // 
            this.ctrlPalette2.Location = new System.Drawing.Point(193, 227);
            this.ctrlPalette2.Name = "ctrlPalette2";
            this.ctrlPalette2.Size = new System.Drawing.Size(150, 150);
            this.ctrlPalette2.TabIndex = 45;
            this.ctrlPalette2.Click += new System.EventHandler(this.ctrlPalette2_Click);
            // 
            // ctrlPalette3
            // 
            this.ctrlPalette3.Location = new System.Drawing.Point(351, 227);
            this.ctrlPalette3.Name = "ctrlPalette3";
            this.ctrlPalette3.Size = new System.Drawing.Size(150, 150);
            this.ctrlPalette3.TabIndex = 46;
            this.ctrlPalette3.Click += new System.EventHandler(this.ctrlPalette3_Click);
            // 
            // ctrlPalette4
            // 
            this.ctrlPalette4.Location = new System.Drawing.Point(509, 227);
            this.ctrlPalette4.Name = "ctrlPalette4";
            this.ctrlPalette4.Size = new System.Drawing.Size(150, 150);
            this.ctrlPalette4.TabIndex = 47;
            this.ctrlPalette4.Click += new System.EventHandler(this.ctrlPalette4_Click);
            // 
            // ctrlPalette5
            // 
            this.ctrlPalette5.Location = new System.Drawing.Point(667, 227);
            this.ctrlPalette5.Name = "ctrlPalette5";
            this.ctrlPalette5.Size = new System.Drawing.Size(150, 150);
            this.ctrlPalette5.TabIndex = 48;
            this.ctrlPalette5.Click += new System.EventHandler(this.ctrlPalette5_Click);
            // 
            // ctrlPalette6
            // 
            this.ctrlPalette6.Location = new System.Drawing.Point(825, 227);
            this.ctrlPalette6.Name = "ctrlPalette6";
            this.ctrlPalette6.Size = new System.Drawing.Size(150, 150);
            this.ctrlPalette6.TabIndex = 49;
            this.ctrlPalette6.Click += new System.EventHandler(this.ctrlPalette6_Click);
            // 
            // dispInfo
            // 
            this.dispInfo.BackColor = System.Drawing.Color.LightGray;
            this.dispInfo.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.dispInfo.Location = new System.Drawing.Point(35, 445);
            this.dispInfo.Name = "dispInfo";
            this.dispInfo.Size = new System.Drawing.Size(940, 30);
            this.dispInfo.Text = "Bereit...";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dispErrorText
            // 
            this.dispErrorText.BackColor = System.Drawing.Color.Red;
            this.dispErrorText.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.dispErrorText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dispErrorText.Location = new System.Drawing.Point(238, 360);
            this.dispErrorText.Name = "dispErrorText";
            this.dispErrorText.Size = new System.Drawing.Size(534, 85);
            this.dispErrorText.Text = "?";
            this.dispErrorText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.dispErrorText.Visible = false;            
            // 
            // frmPalettenauswahl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1015, 575);
            this.ControlBox = false;
            this.Controls.Add(this.dispErrorText);
            this.Controls.Add(this.dispInfo);
            this.Controls.Add(this.ctrlPalette6);
            this.Controls.Add(this.ctrlPalette5);
            this.Controls.Add(this.ctrlPalette4);
            this.Controls.Add(this.ctrlPalette3);
            this.Controls.Add(this.ctrlPalette2);
            this.Controls.Add(this.ctrlPalette1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ctrlButtonBar1);
            this.Controls.Add(this.pnlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPalettenauswahl";
            this.Text = "frmPalettenauswahl";
            this.Load += new System.EventHandler(this.frmPalettenauswahl_Load);
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label dispTopLabelLeft;
        private System.Windows.Forms.Label dispTopLabelRight;
        private ctrlButtonBar ctrlButtonBar1;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel panel1;
        private ctrlPalette ctrlPalette1;
        private ctrlPalette ctrlPalette2;
        private ctrlPalette ctrlPalette3;
        private ctrlPalette ctrlPalette4;
        private ctrlPalette ctrlPalette5;
        private ctrlPalette ctrlPalette6;
        private System.Windows.Forms.Label dispInfo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label dispErrorText;
        private System.Windows.Forms.Timer timer2;
    }
}