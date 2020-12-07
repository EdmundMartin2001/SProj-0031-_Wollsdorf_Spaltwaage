using System;
using System.Windows.Forms;
using Wollsdorf_Spaltwaage.Allgemein.Forms;

namespace Allgemein.FormHelper
{
    /// <remarks>SMT Template</remarks>
    internal partial class frmTemplate : Form
    {
        public frmTemplate()
        {
            InitializeComponent();
        }

        private void frmTemplate_Load(object sender, EventArgs e)
        {
            cFormStyle.FORM_LOAD(this, this.pnlWaage);
        }
    }
}