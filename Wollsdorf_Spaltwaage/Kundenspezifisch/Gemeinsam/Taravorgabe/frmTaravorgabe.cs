using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Allgemein;

namespace Wollsdorf.Kundenspezifisch.Gemeinsam.Taravorgabe
{
    public partial class frmTaravorgabe : Form
    {
        public frmTaravorgabe()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmTaravorgabe_Load(object sender, EventArgs e)
        {
            this.Top = 218;
        }

        private bool Starte_Taravorgabe()
        {
            bool bRet = false;

            double dTaravorgabe = cGlobalHandling.TextboxToDouble(this.tbTara);

            if (!this.ValidateInput(dTaravorgabe))
            {
                return false;
            }
            else
            {
                bRet = this.Taravorgabe(dTaravorgabe);
            }

            return bRet;
        }

        private void ZeigeFehlerMeldung(string sText)
        {
            try
            {
                this.dispErrorText.Text = sText;
                this.dispErrorText.Visible = true;
                Application.DoEvents();

                System.Threading.Thread.Sleep(1000);
                this.dispErrorText.Visible = false;
            }
            catch (Exception)
            {
            }
        }

        private bool ValidateInput(double dTaraGewicht)
        {
            bool bRet = false;

            if (dTaraGewicht <= 0)
            {
                this.ZeigeFehlerMeldung("Eingabe ungültig");
                bRet = false;
            }
            else
            {
                bRet = true;
            }

            return bRet;
        }
        private bool Taravorgabe(double dTaraGewicht)
        {
            bool bRet = false;

            try
            {
                this.dispWaitText.Visible = true;
                Application.DoEvents();

                MTTS.IND890.CE.CScale.enumTareResult eRes =
                    MTTS.IND890.CE.CScale.enumTareResult.TARE_FAILED;

                // Tara an die Waage senden
                eRes = cGlobalScale.setTare(dTaraGewicht);

                if (eRes != MTTS.IND890.CE.CScale.enumTareResult.TARE_SUCCESS)
                {
                    this.dispWaitText.Visible = false;
                    this.ZeigeFehlerMeldung("Tara Fehler\nDie Waage meldet eine ungültige Tara");
                    bRet = false;
                }
                else
                {
                    bRet = true;
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
            }
            finally
            {
                this.dispWaitText.Visible = false;
                Application.DoEvents();
            }

            return bRet;
        }

        private void frmTaravorgabe_Activated(object sender, EventArgs e)
        {
            this.tbTara.Focus();
            this.tbTara.SelectAll();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            bool bret = Starte_Taravorgabe();
            if (bret)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.tbTara.Focus();
                this.tbTara.SelectAll();
            }
        }

    }
}