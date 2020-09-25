namespace Allgemein
{
    partial class frmInfoDlg
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
            this.cmdWeiter = new System.Windows.Forms.Button();
            this.dispMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdWeiter
            // 
            this.cmdWeiter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdWeiter.Location = new System.Drawing.Point(706, 180);
            this.cmdWeiter.Name = "cmdWeiter";
            this.cmdWeiter.Size = new System.Drawing.Size(141, 55);
            this.cmdWeiter.TabIndex = 0;
            this.cmdWeiter.TabStop = false;
            this.cmdWeiter.Text = "Weiter";
            this.cmdWeiter.Click += new System.EventHandler(this.cmdWeiter_Click);
            // 
            // dispMessage
            // 
            this.dispMessage.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dispMessage.Location = new System.Drawing.Point(16, 12);
            this.dispMessage.Name = "dispMessage";
            this.dispMessage.Size = new System.Drawing.Size(831, 165);
            this.dispMessage.Text = "#Meldung";
            // 
            // frmInfoDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(865, 250);
            this.Controls.Add(this.dispMessage);
            this.Controls.Add(this.cmdWeiter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInfoDlg";
            this.Text = "Systemmeldung";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmInfoDlg_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmInfoDlg_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdWeiter;
        private System.Windows.Forms.Label dispMessage;
    }
}