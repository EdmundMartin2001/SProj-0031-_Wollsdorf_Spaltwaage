namespace Allgemein
{
    using System;
    using System.Windows.Forms;
    
    internal partial class frmInfoDlg : Form
    {
        public frmInfoDlg(string sInfoText)
        {
            InitializeComponent();

            this.dispMessage.Text = sInfoText;
        }

        private void frmInfoDlg_Load(object sender, EventArgs e)
        {
            cGlobalHandling.CenterForm(this, 40);
        }
        private void frmInfoDlg_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Close();
        }
        private void cmdWeiter_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}