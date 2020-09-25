namespace Wollsdorf.Spaltwaage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWiegen));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.label1 = new System.Windows.Forms.Label();
            this.dispWaitText = new System.Windows.Forms.Label();
            this.dispErrorText = new System.Windows.Forms.Label();
            this.dispStatus = new System.Windows.Forms.Label();
            this.cmdCancel = new Allgemein.Controls.ctrlButton();
            this.cmdRetry = new Allgemein.Controls.ctrlButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 62);
            this.label1.Text = "Wiegemodus";
            // 
            // dispWaitText
            // 
            this.dispWaitText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.dispWaitText.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.dispWaitText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dispWaitText.Location = new System.Drawing.Point(14, 195);
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
            this.dispErrorText.Location = new System.Drawing.Point(13, 197);
            this.dispErrorText.Name = "dispErrorText";
            this.dispErrorText.Size = new System.Drawing.Size(534, 85);
            this.dispErrorText.Text = "?";
            this.dispErrorText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.dispErrorText.Visible = false;
            // 
            // dispStatus
            // 
            this.dispStatus.Font = new System.Drawing.Font("Arial", 22F, System.Drawing.FontStyle.Bold);
            this.dispStatus.Location = new System.Drawing.Point(13, 64);
            this.dispStatus.Name = "dispStatus";
            this.dispStatus.Size = new System.Drawing.Size(432, 75);
            this.dispStatus.Text = "Wiegung erfolgt ....";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Bild_Icon = ((System.Drawing.Icon)(resources.GetObject("cmdCancel.Bild_Icon")));
            this.cmdCancel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.cmdCancel.ForeColor = System.Drawing.Color.White;
            this.cmdCancel.Location = new System.Drawing.Point(14, 119);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(126, 73);
            this.cmdCancel.TabIndex = 8;
            this.cmdCancel.Text = "Abbruch";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdRetry
            // 
            this.cmdRetry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRetry.Bild_Icon = ((System.Drawing.Icon)(resources.GetObject("cmdRetry.Bild_Icon")));
            this.cmdRetry.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.cmdRetry.ForeColor = System.Drawing.Color.White;
            this.cmdRetry.Location = new System.Drawing.Point(146, 119);
            this.cmdRetry.Name = "cmdRetry";
            this.cmdRetry.Size = new System.Drawing.Size(126, 73);
            this.cmdRetry.TabIndex = 9;
            this.cmdRetry.Text = "Retry";
            this.cmdRetry.Click += new System.EventHandler(this.cmdRetry_Click);
            // 
            // frmWiegen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(837, 284);
            this.Controls.Add(this.cmdRetry);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.dispStatus);
            this.Controls.Add(this.dispErrorText);
            this.Controls.Add(this.dispWaitText);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWiegen";
            this.Load += new System.EventHandler(this.frmWiegen_Load);
            this.Activated += new System.EventHandler(this.frmWiegen_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label dispWaitText;
        private System.Windows.Forms.Label dispErrorText;
        private System.Windows.Forms.Label dispStatus;
        private Allgemein.Controls.ctrlButton cmdCancel;
        private Allgemein.Controls.ctrlButton cmdRetry;
    }
}