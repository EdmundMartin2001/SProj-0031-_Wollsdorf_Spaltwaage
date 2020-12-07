using Wollsdorf_Spaltwaage.Allgemein.Button;
using Wollsdorf_Spaltwaage.Allgemein.Touch_Numeric;

namespace Wollsdorf_Spaltwaage.Kundenspezifisch.Gemeinsam.Taravorgabe
{
    partial class frmTaravorgabe
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
            this.tbTara = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlDoubleNumPad1 = new ctrlDoubleNumPad();
            this.cmdCancel = new ctrlButton();
            this.cmdOK = new ctrlButton();
            this.dispWaitText = new System.Windows.Forms.Label();
            this.dispErrorText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbTara
            // 
            this.tbTara.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular);
            this.tbTara.ForeColor = System.Drawing.Color.Black;
            this.tbTara.Location = new System.Drawing.Point(158, 138);
            this.tbTara.MaxLength = 10;
            this.tbTara.Name = "tbTara";
            this.tbTara.Size = new System.Drawing.Size(173, 39);
            this.tbTara.TabIndex = 14;
            this.tbTara.WordWrap = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(158, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 37);
            this.label4.Text = "Tara:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(337, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 37);
            this.label1.Text = "kg";
            // 
            // ctrlDoubleNumPad1
            // 
            this.ctrlDoubleNumPad1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ctrlDoubleNumPad1.Location = new System.Drawing.Point(535, 41);
            this.ctrlDoubleNumPad1.Name = "ctrlDoubleNumPad1";
            this.ctrlDoubleNumPad1.Size = new System.Drawing.Size(429, 253);
            this.ctrlDoubleNumPad1.TabIndex = 21;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(158, 183);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(77, 44);
            this.cmdCancel.TabIndex = 20;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(254, 183);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(77, 44);
            this.cmdOK.TabIndex = 19;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // dispWaitText
            // 
            this.dispWaitText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.dispWaitText.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.dispWaitText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dispWaitText.Location = new System.Drawing.Point(40, 192);
            this.dispWaitText.Name = "dispWaitText";
            this.dispWaitText.Size = new System.Drawing.Size(489, 49);
            this.dispWaitText.Text = "Bitte warten Sie ...";
            this.dispWaitText.Visible = false;
            // 
            // dispErrorText
            // 
            this.dispErrorText.BackColor = System.Drawing.Color.Red;
            this.dispErrorText.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.dispErrorText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dispErrorText.Location = new System.Drawing.Point(40, 216);
            this.dispErrorText.Name = "dispErrorText";
            this.dispErrorText.Size = new System.Drawing.Size(489, 78);
            this.dispErrorText.Text = "Tara Fehler !\r\nPrüfen Sie die Eingabe";
            this.dispErrorText.Visible = false;
            // 
            // frmTaravorgabe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1024, 329);
            this.ControlBox = false;
            this.Controls.Add(this.dispWaitText);
            this.Controls.Add(this.dispErrorText);
            this.Controls.Add(this.ctrlDoubleNumPad1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTara);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTaravorgabe";
            this.Text = "d";
            this.Load += new System.EventHandler(this.frmTaravorgabe_Load);
            this.Activated += new System.EventHandler(this.frmTaravorgabe_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbTara;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private ctrlButton cmdOK;
        private ctrlButton cmdCancel;
        private ctrlDoubleNumPad ctrlDoubleNumPad1;
        private System.Windows.Forms.Label dispWaitText;
        private System.Windows.Forms.Label dispErrorText;
    }
}