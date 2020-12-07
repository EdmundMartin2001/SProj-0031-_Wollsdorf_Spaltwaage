namespace Wollsdorf_Spaltwaage.Allgemein
{
    partial class frmYesNoSicher
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
            this.cmdJa = new System.Windows.Forms.Button();
            this.dispMessage = new System.Windows.Forms.Label();
            this.cmdNein = new System.Windows.Forms.Button();
            this.cmdJa2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdJa
            // 
            this.cmdJa.Location = new System.Drawing.Point(316, 133);
            this.cmdJa.Name = "cmdJa";
            this.cmdJa.Size = new System.Drawing.Size(141, 55);
            this.cmdJa.TabIndex = 6;
            this.cmdJa.TabStop = false;
            this.cmdJa.Text = "JA";
            this.cmdJa.Click += new System.EventHandler(this.cmdJa_Click);
            // 
            // dispMessage
            // 
            this.dispMessage.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.dispMessage.Location = new System.Drawing.Point(17, 11);
            this.dispMessage.Name = "dispMessage";
            this.dispMessage.Size = new System.Drawing.Size(604, 107);
            this.dispMessage.Text = "#Meldung\r\n2\r\n3\r\n4";
            // 
            // cmdNein
            // 
            this.cmdNein.Location = new System.Drawing.Point(480, 133);
            this.cmdNein.Name = "cmdNein";
            this.cmdNein.Size = new System.Drawing.Size(141, 55);
            this.cmdNein.TabIndex = 5;
            this.cmdNein.TabStop = false;
            this.cmdNein.Text = "NEIN";
            this.cmdNein.Click += new System.EventHandler(this.cmdNein_Click);
            // 
            // cmdJa2
            // 
            this.cmdJa2.Location = new System.Drawing.Point(17, 133);
            this.cmdJa2.Name = "cmdJa2";
            this.cmdJa2.Size = new System.Drawing.Size(141, 55);
            this.cmdJa2.TabIndex = 8;
            this.cmdJa2.TabStop = false;
            this.cmdJa2.Text = "Ganz Sicher ?";
            this.cmdJa2.Visible = false;
            this.cmdJa2.Click += new System.EventHandler(this.cmdJa2_Click);
            // 
            // frmYesNoSicher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(638, 199);
            this.Controls.Add(this.cmdJa2);
            this.Controls.Add(this.cmdJa);
            this.Controls.Add(this.dispMessage);
            this.Controls.Add(this.cmdNein);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmYesNoSicher";
            this.Text = "frmYesNoSicher";
            this.Load += new System.EventHandler(this.frmYesNoSicher_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdJa;
        private System.Windows.Forms.Label dispMessage;
        private System.Windows.Forms.Button cmdNein;
        private System.Windows.Forms.Button cmdJa2;
    }
}