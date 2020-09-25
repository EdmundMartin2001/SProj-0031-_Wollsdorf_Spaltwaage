namespace Allgemein
{
    partial class frmYesNo
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
            this.dispMessage = new System.Windows.Forms.Label();
            this.cmdNein = new System.Windows.Forms.Button();
            this.cmdJa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dispMessage
            // 
            this.dispMessage.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.dispMessage.Location = new System.Drawing.Point(18, 12);
            this.dispMessage.Name = "dispMessage";
            this.dispMessage.Size = new System.Drawing.Size(604, 107);
            this.dispMessage.Text = "#Meldung\r\n2\r\n3\r\n4";
            // 
            // cmdNein
            // 
            this.cmdNein.Location = new System.Drawing.Point(481, 134);
            this.cmdNein.Name = "cmdNein";
            this.cmdNein.Size = new System.Drawing.Size(141, 55);
            this.cmdNein.TabIndex = 1;
            this.cmdNein.TabStop = false;
            this.cmdNein.Text = "NEIN";
            this.cmdNein.Click += new System.EventHandler(this.cmdNein_Click);
            // 
            // cmdJa
            // 
            this.cmdJa.Location = new System.Drawing.Point(317, 134);
            this.cmdJa.Name = "cmdJa";
            this.cmdJa.Size = new System.Drawing.Size(141, 55);
            this.cmdJa.TabIndex = 3;
            this.cmdJa.TabStop = false;
            this.cmdJa.Text = "JA";
            this.cmdJa.Click += new System.EventHandler(this.cmdJa_Click);
            // 
            // frmYesNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(638, 199);
            this.Controls.Add(this.cmdJa);
            this.Controls.Add(this.dispMessage);
            this.Controls.Add(this.cmdNein);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmYesNo";
            this.Text = "frmYesNo";
            this.Load += new System.EventHandler(this.frmYesNo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label dispMessage;
        private System.Windows.Forms.Button cmdNein;
        private System.Windows.Forms.Button cmdJa;
    }
}