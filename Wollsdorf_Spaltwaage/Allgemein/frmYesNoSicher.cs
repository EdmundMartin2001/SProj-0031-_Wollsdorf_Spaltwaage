using System;
using System.Windows.Forms;
using Wollsdorf_Spaltwaage.Kundenspezifisch;

namespace Wollsdorf_Spaltwaage.Allgemein
{
    public partial class frmYesNoSicher : Form
    {
        public frmYesNoSicher(string sInfoText)
        {
            InitializeComponent();

            this.dispMessage.Text = sInfoText;
        }

        private void cmdNein_Click(object sender, EventArgs e)
        {
            this.cmdNein.Visible = false;
            Application.DoEvents();

            this.DialogResult = DialogResult.No;
            this.Close();
        }
        private void cmdJa_Click(object sender, EventArgs e)
        {
            this.cmdJa.Visible = false;
            Application.DoEvents();

            this.cmdJa2.Visible = true;
            this.cmdJa.Visible = false;
        }
        private void cmdJa2_Click(object sender, EventArgs e)
        {
            this.cmdJa2.Visible = false;
            Application.DoEvents();

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void frmYesNoSicher_Load(object sender, EventArgs e)
        {
            cGlobalHandling.CenterForm(this, 60);
        }      
    }
}