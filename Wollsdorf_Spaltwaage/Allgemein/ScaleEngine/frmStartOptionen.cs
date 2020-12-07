using System;
using System.Drawing;
using System.Windows.Forms;
using MTTS.IND890.CE;
using Wollsdorf_Spaltwaage.Kundenspezifisch;

namespace Wollsdorf_Spaltwaage.Allgemein.ScaleEngine
{
    internal partial class frmStartOptionen : Form
    {
        private CTerminal.enumDisplayMode eDisplayMode;

        public frmStartOptionen()
        {
            InitializeComponent();
        }

        private void frmStartOptionen_Load(object sender, EventArgs e)
        {
            this.eDisplayMode = cGlobalScale.objCIND890APIClient.Terminal.ApplicationMode;

            cGlobalHandling.CenterForm(this, 70);

            this.Class2Gui();
        }
        private void cmdReboot_Click(object sender, EventArgs e)
        {
            this.SetAppMode();

            DialogResult dialogResult = 
                MessageBox.Show("Sind Sie sicher das sie das Gerät neustarten wollen?", "Neustart", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);

            if (dialogResult == DialogResult.Yes)
            {                
                cGlobalScale.BEENDE_SCALE();
                cGlobalHandling.rebootDevice();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.SetAppMode();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void cmdLegacy_Click(object sender, EventArgs e)
        {
            eDisplayMode = CTerminal.enumDisplayMode.LEGACY;
            Class2Gui();
        }
        private void cmdAsk_Click(object sender, EventArgs e)
        {
            eDisplayMode = CTerminal.enumDisplayMode.ALWAYS_ASK;
            Class2Gui();
        }

        private void Class2Gui() 
        {
            cmdAsk.StartColor = Color.Gray;
            cmdLegacy.StartColor = Color.Gray;

            switch (eDisplayMode)
            {
                case CTerminal.enumDisplayMode.ALWAYS_ASK:
                    cmdAsk.StartColor = Color.Red;
                    break;
                case CTerminal.enumDisplayMode.LEGACY:
                    cmdLegacy.StartColor = Color.Red;
                    break;
                default:
                    break;
            }
        }

        private void SetAppMode()
        {
            cGlobalScale.objCIND890APIClient.Terminal.ApplicationMode = this.eDisplayMode;
        }

        private void cmdPacOption_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sind Sie sicher das sie das Gerät im Setupmodus starten wollen?", "Base Setup", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                cGlobalScale.objCIND890APIClient.Terminal.LoadSetupScreen();
            }
        }
    }
}