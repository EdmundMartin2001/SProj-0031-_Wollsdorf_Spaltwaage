namespace Wollsdorf
{
    partial class frmServiceFunktionen
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.dispTopLabelRight = new System.Windows.Forms.Label();
            this.dispTopLabelLeft = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdSalzwaageTestdruck = new System.Windows.Forms.Button();
            this.cmdDruckeTestSeite = new System.Windows.Forms.Button();
            this.cmdInitSettings = new System.Windows.Forms.Button();
            this.cmdTextPrinter_Abschluss = new System.Windows.Forms.Button();
            this.cmdTextPrinter_Kopf = new System.Windows.Forms.Button();
            this.cmdCSVcreate = new System.Windows.Forms.Button();
            this.cmdCSVTest = new System.Windows.Forms.Button();
            this.cmdSQLreaderIND = new System.Windows.Forms.Button();
            this.cmdSQLinserterIND = new System.Windows.Forms.Button();
            this.tbSQLergebnisbox = new System.Windows.Forms.TextBox();
            this.cmdSQLinserter = new System.Windows.Forms.Button();
            this.cmdSQLreader = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ctrlButtonBar1 = new Allgemein.Controls.ctrlButtonBar();
            this.cmdKill_SMT_Waage = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
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
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Location = new System.Drawing.Point(0, 217);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 4);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 47);
            this.button1.TabIndex = 30;
            this.button1.Text = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdDruckeTestSeite
            // 
            this.cmdDruckeTestSeite.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.cmdDruckeTestSeite.Location = new System.Drawing.Point(171, 35);
            this.cmdDruckeTestSeite.Name = "cmdDruckeTestSeite";
            this.cmdDruckeTestSeite.Size = new System.Drawing.Size(156, 46);
            this.cmdDruckeTestSeite.TabIndex = 28;
            this.cmdDruckeTestSeite.Text = "Druck Testseite";
            this.cmdDruckeTestSeite.Click += new System.EventHandler(this.cmdDruckeTestSeite_Click);
            // 
            // cmdInitSettings
            // 
            this.cmdInitSettings.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.cmdInitSettings.Location = new System.Drawing.Point(356, 96);
            this.cmdInitSettings.Name = "cmdInitSettings";
            this.cmdInitSettings.Size = new System.Drawing.Size(126, 65);
            this.cmdInitSettings.TabIndex = 27;
            this.cmdInitSettings.Text = "Initialisiere Settings";
            this.cmdInitSettings.Click += new System.EventHandler(this.cmdInitSettings_Click);
            // 
            // cmdTextPrinter_Abschluss
            // 
            this.cmdTextPrinter_Abschluss.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.cmdTextPrinter_Abschluss.Location = new System.Drawing.Point(171, 96);
            this.cmdTextPrinter_Abschluss.Name = "cmdTextPrinter_Abschluss";
            this.cmdTextPrinter_Abschluss.Size = new System.Drawing.Size(156, 46);
            this.cmdTextPrinter_Abschluss.TabIndex = 24;
            this.cmdTextPrinter_Abschluss.Text = "Druck Abschluss";
            this.cmdTextPrinter_Abschluss.Click += new System.EventHandler(this.cmdTextPrinter_Abschluss_Click);
            // 
            // cmdTextPrinter_Kopf
            // 
            this.cmdTextPrinter_Kopf.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.cmdTextPrinter_Kopf.Location = new System.Drawing.Point(9, 35);
            this.cmdTextPrinter_Kopf.Name = "cmdTextPrinter_Kopf";
            this.cmdTextPrinter_Kopf.Size = new System.Drawing.Size(156, 46);
            this.cmdTextPrinter_Kopf.TabIndex = 22;
            this.cmdTextPrinter_Kopf.Text = "Druck Kopf";
            this.cmdTextPrinter_Kopf.Click += new System.EventHandler(this.cmdTextPrinter_Kopf_Click);
            // 
            // cmdCSVcreate
            // 
            this.cmdCSVcreate.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.cmdCSVcreate.Location = new System.Drawing.Point(821, 44);
            this.cmdCSVcreate.Name = "cmdCSVcreate";
            this.cmdCSVcreate.Size = new System.Drawing.Size(154, 64);
            this.cmdCSVcreate.TabIndex = 27;
            this.cmdCSVcreate.Text = "CSV erstellen Test";
            this.cmdCSVcreate.Click += new System.EventHandler(this.cmdCSVcreate_Click);
            // 
            // cmdCSVTest
            // 
            this.cmdCSVTest.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.cmdCSVTest.Location = new System.Drawing.Point(722, 44);
            this.cmdCSVTest.Name = "cmdCSVTest";
            this.cmdCSVTest.Size = new System.Drawing.Size(66, 64);
            this.cmdCSVTest.TabIndex = 26;
            this.cmdCSVTest.Text = "Test";
            this.cmdCSVTest.Click += new System.EventHandler(this.cmdCSVTest_Click);
            // 
            // cmdSQLreaderIND
            // 
            this.cmdSQLreaderIND.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.cmdSQLreaderIND.Location = new System.Drawing.Point(517, 44);
            this.cmdSQLreaderIND.Name = "cmdSQLreaderIND";
            this.cmdSQLreaderIND.Size = new System.Drawing.Size(147, 46);
            this.cmdSQLreaderIND.TabIndex = 25;
            this.cmdSQLreaderIND.Text = "SQL CE Auslesen";
            this.cmdSQLreaderIND.Click += new System.EventHandler(this.cmdSQLreaderIND_Click);
            // 
            // cmdSQLinserterIND
            // 
            this.cmdSQLinserterIND.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.cmdSQLinserterIND.Location = new System.Drawing.Point(364, 44);
            this.cmdSQLinserterIND.Name = "cmdSQLinserterIND";
            this.cmdSQLinserterIND.Size = new System.Drawing.Size(147, 46);
            this.cmdSQLinserterIND.TabIndex = 24;
            this.cmdSQLinserterIND.Text = "SQL CE Einfügen";
            this.cmdSQLinserterIND.Click += new System.EventHandler(this.cmdSQLinserterIND_Click);
            // 
            // tbSQLergebnisbox
            // 
            this.tbSQLergebnisbox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.tbSQLergebnisbox.Location = new System.Drawing.Point(8, 92);
            this.tbSQLergebnisbox.Name = "tbSQLergebnisbox";
            this.tbSQLergebnisbox.Size = new System.Drawing.Size(656, 26);
            this.tbSQLergebnisbox.TabIndex = 23;
            // 
            // cmdSQLinserter
            // 
            this.cmdSQLinserter.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.cmdSQLinserter.Location = new System.Drawing.Point(158, 44);
            this.cmdSQLinserter.Name = "cmdSQLinserter";
            this.cmdSQLinserter.Size = new System.Drawing.Size(147, 46);
            this.cmdSQLinserter.TabIndex = 22;
            this.cmdSQLinserter.Text = "Einfügen";
            this.cmdSQLinserter.Click += new System.EventHandler(this.cmdSQLinserter_Click);
            // 
            // cmdSQLreader
            // 
            this.cmdSQLreader.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.cmdSQLreader.Location = new System.Drawing.Point(28, 44);
            this.cmdSQLreader.Name = "cmdSQLreader";
            this.cmdSQLreader.Size = new System.Drawing.Size(124, 46);
            this.cmdSQLreader.TabIndex = 21;
            this.cmdSQLreader.Text = "Auslesen";
            this.cmdSQLreader.Click += new System.EventHandler(this.cmdSQLreader_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.tabControl1.Location = new System.Drawing.Point(0, 227);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1024, 286);
            this.tabControl1.TabIndex = 52;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.cmdKill_SMT_Waage);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.cmdInitSettings);
            this.tabPage1.Controls.Add(this.cmdSalzwaageTestdruck);
            this.tabPage1.Controls.Add(this.cmdTextPrinter_Kopf);
            this.tabPage1.Controls.Add(this.cmdDruckeTestSeite);
            this.tabPage1.Controls.Add(this.cmdTextPrinter_Abschluss);
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1016, 244);
            this.tabPage1.Text = "Seite 1";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.cmdCSVcreate);
            this.tabPage2.Controls.Add(this.cmdCSVTest);
            this.tabPage2.Controls.Add(this.cmdSQLreader);
            this.tabPage2.Controls.Add(this.cmdSQLreaderIND);
            this.tabPage2.Controls.Add(this.cmdSQLinserter);
            this.tabPage2.Controls.Add(this.cmdSQLinserterIND);
            this.tabPage2.Controls.Add(this.tbSQLergebnisbox);
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1016, 244);
            this.tabPage2.Text = "Seite 2";
            // 
            // ctrlButtonBar1
            // 
            this.ctrlButtonBar1.BackColor = System.Drawing.Color.White;
            this.ctrlButtonBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlButtonBar1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.ctrlButtonBar1.Location = new System.Drawing.Point(0, 513);
            this.ctrlButtonBar1.Name = "ctrlButtonBar1";
            this.ctrlButtonBar1.Size = new System.Drawing.Size(1024, 87);
            this.ctrlButtonBar1.TabIndex = 24;
            // 
            // cmdKill_SMT_Waage
            // 
            this.cmdKill_SMT_Waage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.cmdKill_SMT_Waage.Location = new System.Drawing.Point(546, 35);
            this.cmdKill_SMT_Waage.Name = "cmdKill_SMT_Waage";
            this.cmdKill_SMT_Waage.Size = new System.Drawing.Size(256, 46);
            this.cmdKill_SMT_Waage.TabIndex = 31;
            this.cmdKill_SMT_Waage.Text = "Löschen SMT_WAAGE";
            this.cmdKill_SMT_Waage.Click += new System.EventHandler(this.cmdKill_SMT_Waage_Click);
            // 
            // frmServiceFunktionen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.ctrlButtonBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmServiceFunktionen";
            this.Text = "frmServiceFunktionen";
            this.Load += new System.EventHandler(this.frmServiceFunktionen_Load);
            this.pnlTop.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label dispTopLabelRight;
        private System.Windows.Forms.Label dispTopLabelLeft;
        private Allgemein.Controls.ctrlButtonBar ctrlButtonBar1;
        private System.Windows.Forms.Button cmdSQLreaderIND;
        private System.Windows.Forms.Button cmdSQLinserterIND;
        private System.Windows.Forms.TextBox tbSQLergebnisbox;
        private System.Windows.Forms.Button cmdSQLinserter;
        private System.Windows.Forms.Button cmdSQLreader;
        private System.Windows.Forms.Button cmdTextPrinter_Abschluss;
        private System.Windows.Forms.Button cmdTextPrinter_Kopf;
        private System.Windows.Forms.Button cmdCSVTest;
        private System.Windows.Forms.Button cmdCSVcreate;
        private System.Windows.Forms.Button cmdInitSettings;
        private System.Windows.Forms.Button cmdDruckeTestSeite;
        private System.Windows.Forms.Button cmdSalzwaageTestdruck;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdKill_SMT_Waage;
    }
}