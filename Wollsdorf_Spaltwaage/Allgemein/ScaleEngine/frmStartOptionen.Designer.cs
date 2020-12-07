using Wollsdorf_Spaltwaage.Allgemein.Button;

namespace Wollsdorf_Spaltwaage.Allgemein.ScaleEngine
{
    partial class frmStartOptionen
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
            this.cmdLegacy = new ctrlButton();
            this.cmdAsk = new ctrlButton();
            this.cmdCancel = new ctrlButton();
            this.cmdOK = new ctrlButton();
            this.cmdReboot = new ctrlButton();
            this.cmdPacOption = new ctrlButton();
            this.SuspendLayout();
            // 
            // cmdLegacy
            // 
            this.cmdLegacy.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.cmdLegacy.ForeColor = System.Drawing.Color.White;
            this.cmdLegacy.Location = new System.Drawing.Point(222, 21);
            this.cmdLegacy.Name = "cmdLegacy";
            this.cmdLegacy.Size = new System.Drawing.Size(180, 169);
            this.cmdLegacy.TabIndex = 0;
            this.cmdLegacy.Text = "Applikation\r\nautomatisch \r\nstarten";
            this.cmdLegacy.Click += new System.EventHandler(this.cmdLegacy_Click);
            // 
            // cmdAsk
            // 
            this.cmdAsk.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.cmdAsk.ForeColor = System.Drawing.Color.White;
            this.cmdAsk.Location = new System.Drawing.Point(17, 21);
            this.cmdAsk.Name = "cmdAsk";
            this.cmdAsk.Size = new System.Drawing.Size(180, 169);
            this.cmdAsk.TabIndex = 1;
            this.cmdAsk.Text = "Immer\r\nnachfragen";
            this.cmdAsk.Click += new System.EventHandler(this.cmdAsk_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.cmdCancel.ForeColor = System.Drawing.Color.White;
            this.cmdCancel.Location = new System.Drawing.Point(423, 211);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(180, 38);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Abbruch";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.cmdOK.ForeColor = System.Drawing.Color.White;
            this.cmdOK.Location = new System.Drawing.Point(222, 211);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(180, 38);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "Ok";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdReboot
            // 
            this.cmdReboot.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.cmdReboot.ForeColor = System.Drawing.Color.White;
            this.cmdReboot.Location = new System.Drawing.Point(17, 211);
            this.cmdReboot.Name = "cmdReboot";
            this.cmdReboot.Size = new System.Drawing.Size(180, 38);
            this.cmdReboot.TabIndex = 4;
            this.cmdReboot.Text = "Neustart";
            this.cmdReboot.Click += new System.EventHandler(this.cmdReboot_Click);
            // 
            // cmdPacOption
            // 
            this.cmdPacOption.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.cmdPacOption.ForeColor = System.Drawing.Color.White;
            this.cmdPacOption.Location = new System.Drawing.Point(423, 21);
            this.cmdPacOption.Name = "cmdPacOption";
            this.cmdPacOption.Size = new System.Drawing.Size(180, 169);
            this.cmdPacOption.TabIndex = 6;
            this.cmdPacOption.Text = "Pac Optionen";
            this.cmdPacOption.Click += new System.EventHandler(this.cmdPacOption_Click_1);
            // 
            // frmStartOptionen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(616, 275);
            this.ControlBox = false;
            this.Controls.Add(this.cmdPacOption);
            this.Controls.Add(this.cmdReboot);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAsk);
            this.Controls.Add(this.cmdLegacy);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStartOptionen";
            this.Text = "Startoptionen";
            this.Load += new System.EventHandler(this.frmStartOptionen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private global::Wollsdorf_Spaltwaage.Allgemein.Button.ctrlButton cmdLegacy;
        private global::Wollsdorf_Spaltwaage.Allgemein.Button.ctrlButton cmdAsk;
        private global::Wollsdorf_Spaltwaage.Allgemein.Button.ctrlButton cmdCancel;
        private global::Wollsdorf_Spaltwaage.Allgemein.Button.ctrlButton cmdOK;
        private global::Wollsdorf_Spaltwaage.Allgemein.Button.ctrlButton cmdReboot;
        private ctrlButton cmdPacOption;
    }
}