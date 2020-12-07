namespace Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Controls
{
    partial class ctrlPalette
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dispPalettenBez = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dispGewicht = new System.Windows.Forms.Label();
            this.dispAnzahl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.Text = "Anz:";
            // 
            // dispPalettenBez
            // 
            this.dispPalettenBez.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.dispPalettenBez.Location = new System.Drawing.Point(8, 7);
            this.dispPalettenBez.Name = "dispPalettenBez";
            this.dispPalettenBez.Size = new System.Drawing.Size(139, 84);
            this.dispPalettenBez.Text = "III. Sort. 7/9kg  (III.Inters 7/9kg)";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 20);
            this.label2.Text = "Gew:";
            // 
            // dispGewicht
            // 
            this.dispGewicht.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.dispGewicht.Location = new System.Drawing.Point(40, 119);
            this.dispGewicht.Name = "dispGewicht";
            this.dispGewicht.Size = new System.Drawing.Size(77, 32);
            this.dispGewicht.Text = "12345";
            this.dispGewicht.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dispAnzahl
            // 
            this.dispAnzahl.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.dispAnzahl.Location = new System.Drawing.Point(40, 88);
            this.dispAnzahl.Name = "dispAnzahl";
            this.dispAnzahl.Size = new System.Drawing.Size(77, 38);
            this.dispAnzahl.Text = "0";
            this.dispAnzahl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(120, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 20);
            this.label3.Text = "kg";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(120, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 20);
            this.label4.Text = "Stk";
            // 
            // ctrlPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dispGewicht);
            this.Controls.Add(this.dispAnzahl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dispPalettenBez);
            this.Controls.Add(this.label1);
            this.Name = "ctrlPalette";
            this.Click += new System.EventHandler(this.ctrlPalette_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label dispPalettenBez;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label dispGewicht;
        private System.Windows.Forms.Label dispAnzahl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

    }
}
