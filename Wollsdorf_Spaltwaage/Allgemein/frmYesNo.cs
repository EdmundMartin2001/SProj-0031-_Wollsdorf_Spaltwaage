using System;
using System.Windows.Forms;
using Wollsdorf_Spaltwaage.Kundenspezifisch;

namespace Wollsdorf_Spaltwaage.Allgemein
{
    internal partial class frmYesNo : Form
    {
        public frmYesNo(string sInfoText)
        {
            InitializeComponent();

            this.dispMessage.Text = sInfoText;
        }

        private void frmYesNo_Load(object sender, EventArgs e)
        {
            cGlobalHandling.CenterForm(this, 40);
        }
        private void cmdJa_Click(object sender, EventArgs e)
        {
            this.cmdJa.Visible = false;
            Application.DoEvents();

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
        private void cmdNein_Click(object sender, EventArgs e)
        {
            this.cmdNein.Visible = false;
            Application.DoEvents();

            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}