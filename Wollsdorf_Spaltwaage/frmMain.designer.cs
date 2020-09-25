namespace Wollsdorf
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.dispClock = new System.Windows.Forms.Label();
            this.dispWelcomeMessage = new System.Windows.Forms.Label();
            this.picKundenLogo = new System.Windows.Forms.PictureBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.dispTopLabelRight = new System.Windows.Forms.Label();
            this.dispTopLabelLeft = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlLoadingMessage = new System.Windows.Forms.Panel();
            this.dispLoadMessage = new System.Windows.Forms.Label();
            this.cmdClose = new Allgemein.Controls.ctrlButton();
            this.cmdSetup = new Allgemein.Controls.ctrlButton();
            this.cmdReboot = new Allgemein.Controls.ctrlButton();
            this.cmdBaseSetup = new Allgemein.Controls.ctrlButton();
            this.ctrlButtonBar1 = new Allgemein.Controls.ctrlButtonBar();
            this.pnlTop.SuspendLayout();
            this.pnlLoadingMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // dispClock
            // 
            this.dispClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dispClock.Font = new System.Drawing.Font("Arial", 22F, System.Drawing.FontStyle.Bold);
            this.dispClock.Location = new System.Drawing.Point(725, 290);
            this.dispClock.Name = "dispClock";
            this.dispClock.Size = new System.Drawing.Size(299, 32);
            this.dispClock.Text = "11.11.1111 12:12";
            this.dispClock.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dispWelcomeMessage
            // 
            this.dispWelcomeMessage.Font = new System.Drawing.Font("Arial", 22F, System.Drawing.FontStyle.Bold);
            this.dispWelcomeMessage.Location = new System.Drawing.Point(12, 412);
            this.dispWelcomeMessage.Name = "dispWelcomeMessage";
            this.dispWelcomeMessage.Size = new System.Drawing.Size(625, 95);
            this.dispWelcomeMessage.Text = "Sie befinden sich im Hauptmenü. \r\nBitte wählen Sie eine Funktion aus.";
            // 
            // picKundenLogo
            // 
            this.picKundenLogo.Image = ((System.Drawing.Image)(resources.GetObject("picKundenLogo.Image")));
            this.picKundenLogo.Location = new System.Drawing.Point(4, 237);
            this.picKundenLogo.Name = "picKundenLogo";
            this.picKundenLogo.Size = new System.Drawing.Size(602, 162);
            this.picKundenLogo.Click += new System.EventHandler(this.picKundenLogo_Click);
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
            // timer1
            // 
            this.timer1.Interval = 800;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(829, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 38);
            this.button1.TabIndex = 16;
            this.button1.Text = "button1";
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Location = new System.Drawing.Point(0, 217);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 4);
            // 
            // pnlLoadingMessage
            // 
            this.pnlLoadingMessage.Controls.Add(this.dispLoadMessage);
            this.pnlLoadingMessage.Location = new System.Drawing.Point(3, 432);
            this.pnlLoadingMessage.Name = "pnlLoadingMessage";
            this.pnlLoadingMessage.Size = new System.Drawing.Size(761, 68);
            this.pnlLoadingMessage.Visible = false;
            // 
            // dispLoadMessage
            // 
            this.dispLoadMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dispLoadMessage.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.dispLoadMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dispLoadMessage.Location = new System.Drawing.Point(0, 0);
            this.dispLoadMessage.Name = "dispLoadMessage";
            this.dispLoadMessage.Size = new System.Drawing.Size(761, 68);
            this.dispLoadMessage.Text = "label2";
            this.dispLoadMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.dispLoadMessage.Visible = false;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.cmdClose.ForeColor = System.Drawing.Color.White;
            this.cmdClose.Bild_Icon = ((System.Drawing.Icon)(resources.GetObject("cmdClose.ImageIcon")));
            this.cmdClose.Location = new System.Drawing.Point(898, 420);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(126, 87);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "Beenden";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSetup
            // 
            this.cmdSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSetup.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.cmdSetup.ForeColor = System.Drawing.Color.White;
            this.cmdSetup.Bild_Icon = ((System.Drawing.Icon)(resources.GetObject("cmdSetup.ImageIcon")));
            this.cmdSetup.Location = new System.Drawing.Point(898, 332);
            this.cmdSetup.Name = "cmdSetup";
            this.cmdSetup.Size = new System.Drawing.Size(126, 87);
            this.cmdSetup.TabIndex = 5;
            this.cmdSetup.Text = "Setup";
            this.cmdSetup.Click += new System.EventHandler(this.cmdSetup_Click);
            // 
            // cmdReboot
            // 
            this.cmdReboot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdReboot.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.cmdReboot.ForeColor = System.Drawing.Color.White;
            this.cmdReboot.Bild_Icon = ((System.Drawing.Icon)(resources.GetObject("cmdReboot.ImageIcon")));
            this.cmdReboot.Location = new System.Drawing.Point(770, 332);
            this.cmdReboot.Name = "cmdReboot";
            this.cmdReboot.Size = new System.Drawing.Size(126, 87);
            this.cmdReboot.TabIndex = 7;
            this.cmdReboot.Text = "Neustart";
            this.cmdReboot.Click += new System.EventHandler(this.cmdReboot_Click);
            // 
            // cmdBaseSetup
            // 
            this.cmdBaseSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBaseSetup.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.cmdBaseSetup.ForeColor = System.Drawing.Color.White;
            this.cmdBaseSetup.Bild_Icon = ((System.Drawing.Icon)(resources.GetObject("cmdBaseSetup.ImageIcon")));
            this.cmdBaseSetup.Location = new System.Drawing.Point(770, 420);
            this.cmdBaseSetup.Name = "cmdBaseSetup";
            this.cmdBaseSetup.Size = new System.Drawing.Size(126, 87);
            this.cmdBaseSetup.TabIndex = 12;
            this.cmdBaseSetup.Text = "Start\r\noption";
            this.cmdBaseSetup.Click += new System.EventHandler(this.cmdStartoption_Click);
            // 
            // ctrlButtonBar1
            // 
            this.ctrlButtonBar1.BackColor = System.Drawing.Color.White;
            this.ctrlButtonBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlButtonBar1.Location = new System.Drawing.Point(0, 513);
            this.ctrlButtonBar1.Name = "ctrlButtonBar1";
            this.ctrlButtonBar1.Size = new System.Drawing.Size(1024, 87);
            this.ctrlButtonBar1.TabIndex = 12;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.picKundenLogo);
            this.Controls.Add(this.dispWelcomeMessage);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSetup);
            this.Controls.Add(this.cmdReboot);
            this.Controls.Add(this.dispClock);
            this.Controls.Add(this.cmdBaseSetup);
            this.Controls.Add(this.ctrlButtonBar1);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlLoadingMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Click += new System.EventHandler(this.frmMain_Click);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            this.pnlTop.ResumeLayout(false);
            this.pnlLoadingMessage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Allgemein.Controls.ctrlButtonBar ctrlButtonBar1;
        private System.Windows.Forms.Label dispWelcomeMessage;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label dispTopLabelRight;
        private System.Windows.Forms.Label dispTopLabelLeft;
        protected System.Windows.Forms.PictureBox picKundenLogo;
        private Allgemein.Controls.ctrlButton cmdSetup;
        private Allgemein.Controls.ctrlButton cmdClose;
        private Allgemein.Controls.ctrlButton cmdReboot;
        private System.Windows.Forms.Label dispClock;
        private System.Windows.Forms.Timer timer1;
        private Allgemein.Controls.ctrlButton cmdBaseSetup;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlLoadingMessage;
        private System.Windows.Forms.Label dispLoadMessage;
        

    }
}

