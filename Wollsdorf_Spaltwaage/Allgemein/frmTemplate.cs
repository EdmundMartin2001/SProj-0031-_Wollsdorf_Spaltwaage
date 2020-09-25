using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            Allgemein.FormHelper.cFormStyle.FORM_LOAD(this, this.pnlWaage);
        }
    }
}