namespace MAN_Fahrzeugwaage
{
    partial class frmWiegen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.lbAchse = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dispWaitText = new System.Windows.Forms.Label();
            this.dispErrorText = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.dispTopLabelRight = new System.Windows.Forms.Label();
            this.dispTopLabelLeft = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dispStatus = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdsimuW1b = new System.Windows.Forms.Button();
            this.cmdsimuW1a = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblA1Rechts = new System.Windows.Forms.Label();
            this.lblA1Links = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmdSimuW2b = new System.Windows.Forms.Button();
            this.cmdSimuW2a = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblA2Rechts = new System.Windows.Forms.Label();
            this.lblA2Links = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.ctrlButtonBar1 = new Allgemein.Controls.ctrlButtonBar();
            this.pnlTop.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbAchse
            // 
            this.lbAchse.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold);
            this.lbAchse.Location = new System.Drawing.Point(35, 220);
            this.lbAchse.Name = "lbAchse";
            this.lbAchse.Size = new System.Drawing.Size(253, 62);
            this.lbAchse.Text = "Achse 1";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(243, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 62);
            this.label1.Text = "Wiegen";
            // 
            // dispWaitText
            // 
            this.dispWaitText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.dispWaitText.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.dispWaitText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dispWaitText.Location = new System.Drawing.Point(27, 423);
            this.dispWaitText.Name = "dispWaitText";
            this.dispWaitText.Size = new System.Drawing.Size(534, 87);
            this.dispWaitText.Text = "Bitte warten Sie ...";
            this.dispWaitText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.dispWaitText.Visible = false;
            // 
            // dispErrorText
            // 
            this.dispErrorText.BackColor = System.Drawing.Color.Red;
            this.dispErrorText.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.dispErrorText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dispErrorText.Location = new System.Drawing.Point(26, 425);
            this.dispErrorText.Name = "dispErrorText";
            this.dispErrorText.Size = new System.Drawing.Size(534, 85);
            this.dispErrorText.Text = "?";
            this.dispErrorText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.dispErrorText.Visible = false;
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
            // dispTopLabelRight
            // 
            this.dispTopLabelRight.BackColor = System.Drawing.Color.Gainsboro;
            this.dispTopLabelRight.Location = new System.Drawing.Point(795, 0);
            this.dispTopLabelRight.Name = "dispTopLabelRight";
            this.dispTopLabelRight.Size = new System.Drawing.Size(217, 16);
            this.dispTopLabelRight.Text = "Terminal ID 99";
            this.dispTopLabelRight.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dispTopLabelRight.Visible = false;
            // 
            // dispTopLabelLeft
            // 
            this.dispTopLabelLeft.Location = new System.Drawing.Point(4, 0);
            this.dispTopLabelLeft.Name = "dispTopLabelLeft";
            this.dispTopLabelLeft.Size = new System.Drawing.Size(354, 16);
            this.dispTopLabelLeft.Text = "Test Applikation 1.0";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 214);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 3);
            // 
            // dispStatus
            // 
            this.dispStatus.Font = new System.Drawing.Font("Arial", 22F, System.Drawing.FontStyle.Bold);
            this.dispStatus.Location = new System.Drawing.Point(35, 282);
            this.dispStatus.Name = "dispStatus";
            this.dispStatus.Size = new System.Drawing.Size(432, 95);
            this.dispStatus.Text = "Bitte für die Wiegung der ersten Achse vorbereiten";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.cmdsimuW1b);
            this.panel2.Controls.Add(this.cmdsimuW1a);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.lblA1Rechts);
            this.panel2.Controls.Add(this.lblA1Links);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(590, 251);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(205, 241);
            // 
            // cmdsimuW1b
            // 
            this.cmdsimuW1b.Location = new System.Drawing.Point(76, 218);
            this.cmdsimuW1b.Name = "cmdsimuW1b";
            this.cmdsimuW1b.Size = new System.Drawing.Size(70, 20);
            this.cmdsimuW1b.TabIndex = 17;
            this.cmdsimuW1b.Text = "Simu W1";
            this.cmdsimuW1b.Visible = false;
            this.cmdsimuW1b.Click += new System.EventHandler(this.cmdsimuW1b_Click);
            // 
            // cmdsimuW1a
            // 
            this.cmdsimuW1a.Location = new System.Drawing.Point(3, 218);
            this.cmdsimuW1a.Name = "cmdsimuW1a";
            this.cmdsimuW1a.Size = new System.Drawing.Size(70, 20);
            this.cmdsimuW1a.TabIndex = 8;
            this.cmdsimuW1a.Text = "Simu W1";
            this.cmdsimuW1a.Visible = false;
            this.cmdsimuW1a.Click += new System.EventHandler(this.cmdsimuW1_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(164, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 28);
            this.label6.Text = "kg";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(164, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 28);
            this.label5.Text = "kg";
            // 
            // lblA1Rechts
            // 
            this.lblA1Rechts.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblA1Rechts.Location = new System.Drawing.Point(35, 181);
            this.lblA1Rechts.Name = "lblA1Rechts";
            this.lblA1Rechts.Size = new System.Drawing.Size(123, 28);
            this.lblA1Rechts.Text = "-";
            this.lblA1Rechts.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblA1Links
            // 
            this.lblA1Links.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblA1Links.Location = new System.Drawing.Point(35, 93);
            this.lblA1Links.Name = "lblA1Links";
            this.lblA1Links.Size = new System.Drawing.Size(123, 28);
            this.lblA1Links.Text = "-";
            this.lblA1Links.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(3, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 28);
            this.label4.Text = "Links";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(3, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 28);
            this.label3.Text = "Rechts";
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(0, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(207, 2);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 30);
            this.label2.Text = "1. Achse";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.cmdSimuW2b);
            this.panel4.Controls.Add(this.cmdSimuW2a);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.lblA2Rechts);
            this.panel4.Controls.Add(this.lblA2Links);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Location = new System.Drawing.Point(803, 251);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(205, 241);
            // 
            // cmdSimuW2b
            // 
            this.cmdSimuW2b.Location = new System.Drawing.Point(74, 218);
            this.cmdSimuW2b.Name = "cmdSimuW2b";
            this.cmdSimuW2b.Size = new System.Drawing.Size(65, 20);
            this.cmdSimuW2b.TabIndex = 18;
            this.cmdSimuW2b.Text = "Simu W2";
            this.cmdSimuW2b.Visible = false;
            this.cmdSimuW2b.Click += new System.EventHandler(this.cmdSimuW2b_Click);
            // 
            // cmdSimuW2a
            // 
            this.cmdSimuW2a.Location = new System.Drawing.Point(3, 218);
            this.cmdSimuW2a.Name = "cmdSimuW2a";
            this.cmdSimuW2a.Size = new System.Drawing.Size(65, 20);
            this.cmdSimuW2a.TabIndex = 9;
            this.cmdSimuW2a.Text = "Simu W2";
            this.cmdSimuW2a.Visible = false;
            this.cmdSimuW2a.Click += new System.EventHandler(this.cmdSimuW2_Click);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label11.Location = new System.Drawing.Point(164, 184);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 28);
            this.label11.Text = "kg";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label10.Location = new System.Drawing.Point(164, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 28);
            this.label10.Text = "kg";
            // 
            // lblA2Rechts
            // 
            this.lblA2Rechts.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblA2Rechts.Location = new System.Drawing.Point(35, 181);
            this.lblA2Rechts.Name = "lblA2Rechts";
            this.lblA2Rechts.Size = new System.Drawing.Size(123, 28);
            this.lblA2Rechts.Text = "-";
            this.lblA2Rechts.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblA2Links
            // 
            this.lblA2Links.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblA2Links.Location = new System.Drawing.Point(35, 93);
            this.lblA2Links.Name = "lblA2Links";
            this.lblA2Links.Size = new System.Drawing.Size(123, 28);
            this.lblA2Links.Text = "-";
            this.lblA2Links.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(3, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 28);
            this.label7.Text = "Links";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label8.Location = new System.Drawing.Point(3, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 28);
            this.label8.Text = "Rechts";
            // 
            // panel5
            // 
            this.panel5.Location = new System.Drawing.Point(0, 41);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(207, 2);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.label9.Location = new System.Drawing.Point(3, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 30);
            this.label9.Text = "2. Achse";
            // 
            // ctrlButtonBar1
            // 
            this.ctrlButtonBar1.BackColor = System.Drawing.Color.LemonChiffon;
            this.ctrlButtonBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlButtonBar1.Location = new System.Drawing.Point(0, 513);
            this.ctrlButtonBar1.Name = "ctrlButtonBar1";
            this.ctrlButtonBar1.Size = new System.Drawing.Size(1024, 87);
            this.ctrlButtonBar1.TabIndex = 13;
            // 
            // frmWiegen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this.ControlBox = false;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dispStatus);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ctrlButtonBar1);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.dispErrorText);
            this.Controls.Add(this.dispWaitText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbAchse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWiegen";
            this.Load += new System.EventHandler(this.frmWiegen_Load);
            this.pnlTop.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbAchse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label dispWaitText;
        private System.Windows.Forms.Label dispErrorText;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label dispTopLabelRight;
        private System.Windows.Forms.Label dispTopLabelLeft;
        private Allgemein.Controls.ctrlButtonBar ctrlButtonBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label dispStatus;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblA1Rechts;
        private System.Windows.Forms.Label lblA1Links;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblA2Rechts;
        private System.Windows.Forms.Label lblA2Links;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button cmdsimuW1a;
        private System.Windows.Forms.Button cmdSimuW2a;
        private System.Windows.Forms.Button cmdsimuW1b;
        private System.Windows.Forms.Button cmdSimuW2b;
    }
}