using Wollsdorf_Spaltwaage.Allgemein.Button;

namespace Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Controls
{
    partial class ctrlListenScroll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlListenScroll));
            this.cmdScrollDown = new ctrlButton();
            this.cmdBildDown = new ctrlButton();
            this.cmdBildUp = new ctrlButton();
            this.cmdScrollUp = new ctrlButton();
            this.SuspendLayout();
            // 
            // cmdScrollDown
            // 
            this.cmdScrollDown.BackColor = System.Drawing.Color.White;
            this.cmdScrollDown.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.cmdScrollDown.ForeColor = System.Drawing.Color.Gray;
            this.cmdScrollDown.Location = new System.Drawing.Point(17, 229);
            this.cmdScrollDown.Name = "cmdScrollDown";
            this.cmdScrollDown.Size = new System.Drawing.Size(100, 100);
            this.cmdScrollDown.StartColor = System.Drawing.Color.Empty;
            this.cmdScrollDown.TabIndex = 11;
            this.cmdScrollDown.Click += new System.EventHandler(this.cmdScrollDown_Click);
            // 
            // cmdBildDown
            // 
            this.cmdBildDown.BackColor = System.Drawing.Color.White;
            this.cmdBildDown.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.cmdBildDown.ForeColor = System.Drawing.Color.Gray;
            this.cmdBildDown.Location = new System.Drawing.Point(17, 335);
            this.cmdBildDown.Name = "cmdBildDown";
            this.cmdBildDown.Size = new System.Drawing.Size(100, 100);
            this.cmdBildDown.StartColor = System.Drawing.Color.Empty;
            this.cmdBildDown.TabIndex = 10;
            this.cmdBildDown.Click += new System.EventHandler(this.cmdBildDown_Click);
            // 
            // cmdBildUp
            // 
            this.cmdBildUp.BackColor = System.Drawing.Color.White;
            this.cmdBildUp.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.cmdBildUp.ForeColor = System.Drawing.Color.Gray;
            this.cmdBildUp.Location = new System.Drawing.Point(17, 17);
            this.cmdBildUp.Name = "cmdBildUp";
            this.cmdBildUp.Size = new System.Drawing.Size(100, 100);
            this.cmdBildUp.StartColor = System.Drawing.Color.Empty;
            this.cmdBildUp.TabIndex = 9;
            this.cmdBildUp.Click += new System.EventHandler(this.cmdBildUp_Click);
            // 
            // cmdScrollUp
            // 
            this.cmdScrollUp.BackColor = System.Drawing.Color.White;
            this.cmdScrollUp.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.cmdScrollUp.ForeColor = System.Drawing.Color.Gray;
            this.cmdScrollUp.Location = new System.Drawing.Point(17, 123);
            this.cmdScrollUp.Name = "cmdScrollUp";
            this.cmdScrollUp.Size = new System.Drawing.Size(100, 100);
            this.cmdScrollUp.StartColor = System.Drawing.Color.Empty;
            this.cmdScrollUp.TabIndex = 8;
            this.cmdScrollUp.Click += new System.EventHandler(this.cmdScrollUp_Click);
            // 
            // ctrlListenScroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.cmdScrollDown);
            this.Controls.Add(this.cmdBildDown);
            this.Controls.Add(this.cmdBildUp);
            this.Controls.Add(this.cmdScrollUp);
            this.ForeColor = System.Drawing.Color.Gray;
            this.Name = "ctrlListenScroll";
            this.Size = new System.Drawing.Size(146, 465);
            this.Click += new System.EventHandler(this.ctrlListenScroll_Click);
            this.Resize += new System.EventHandler(this.ctrlListenScroll_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlButton cmdScrollDown;
        private ctrlButton cmdBildDown;
        private ctrlButton cmdBildUp;
        private ctrlButton cmdScrollUp;
    }
}
