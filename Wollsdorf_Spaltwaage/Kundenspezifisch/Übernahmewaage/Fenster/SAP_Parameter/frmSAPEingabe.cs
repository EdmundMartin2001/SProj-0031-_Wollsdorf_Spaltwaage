using System;
using System.Drawing;
using System.Windows.Forms;
using Wollsdorf_Spaltwaage.Allgemein.ButtonBar;
using Wollsdorf_Spaltwaage.Allgemein.Forms;
using Wollsdorf_Spaltwaage.Allgemein.ScaleEngine;
using Wollsdorf_Spaltwaage.Allgemein.SQL;
using Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Data;
using Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Fenster.Palettenauswahl;

namespace Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Fenster.SAP_Parameter
{
    internal partial class frmSAPEingabe : Form
    {
        private cWiegung objWiegung;
        
        public frmSAPEingabe(ref cWiegung Wiegung)
        {
            InitializeComponent();

            this.objWiegung = Wiegung;

            // Loading Info Panel
            this.InfoMessageDialog(/*Show*/ false, "");
        }

        public void Set_WorkObject(ref cWiegung Wiegung)
        {
            this.objWiegung = Wiegung;
        }

        private void frmSAPEingabe_Load(object sender, EventArgs e)
        {
            cFormStyle.FORM_LOAD(this, null);

            this.dispTopLabelLeft.Text = this.objWiegung.objSettings.get_ArbeitsplatzName;
            this.Init_ButtonBar();
            this.Class2Gui();

            this.tbLieferantennummer.Focus();
            this.tbLieferantennummer.SelectAll();
        }

        private void Init_ButtonBar()
        {
            this.ctrlButtonBar1.DISP_PAGE(1);
            
            if (SETTINGS.IS_EntwicklungsPC())
            {
                this.ctrlButtonBar1.SET_BUTTON_TEXT(1, "Testdaten", "§TestDaten");
            }
            else
            {
                this.ctrlButtonBar1.SET_BUTTON_TEXT(1, "", "§Free1");
            }
            
            this.ctrlButtonBar1.SET_BUTTON_TEXT(2, "", "§Free2");            
            this.ctrlButtonBar1.SET_BUTTON_TEXT(3, "Zurück", "§Zurück");
            this.ctrlButtonBar1.SET_BUTTON_TEXT(4, "Weiter", "§Weiter");

            if (SETTINGS.IS_EntwicklungsPC())
            {
                this.ctrlButtonBar1.SET_BUTTON_TEXT(5, "Simudaten", "§Simu1");
            }
            else
            {
                this.ctrlButtonBar1.SET_BUTTON_TEXT(5, "", "§Free5");
            }

            this.ctrlButtonBar1.SET_BUTTON_TEXT(6, "", "§Free6");
            this.ctrlButtonBar1.SET_BUTTON_TEXT(7, "", "§Free7");
            this.ctrlButtonBar1.SET_BUTTON_TEXT(8, "", "§Free8");

            Icon myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_House;
            this.ctrlButtonBar1.Button_F3.Bild_Icon = myIcon;

            myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_ArrowRight;
            this.ctrlButtonBar1.Button_F4.Bild_Icon = myIcon;

            this.ctrlButtonBar1.EventButtonClick += new ctrlButtonBar._EventButtonClick(ctrlButtonBar1_EventButtonClick);
        }
        private void ctrlButtonBar1_EventButtonClick(object sender, string fTaste, int iTastenCode, string fTag)
        {
            try
            {
                switch (fTag.ToUpper())
                {
                    case "§SIMU1":
                        this.SimulationsWerte();
                        break;
                    case "§TESTDATEN":
                        this.ErstelleTestDaten();                        
                        break;
                    case "§ZURÜCK":
                        this.DialogResult = DialogResult.Cancel;
                        this.Close(); 
                        break;
                    case "§WEITER":
                        if (this.Starte_Taravorgabe())
                        {
                            this.Gui2Class();
                            this.Hide();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        frmPalettenauswahl frmPalette2 = new frmPalettenauswahl(objWiegung, false);
                        frmPalette2.ShowDialog();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException("ctrlButtonBar1_EventButtonClick14", ex);
            }
        }
        private void InfoMessageDialog(bool bShow, string sText)
        {
            this.dispLoadMessage.Text = sText;
            this.dispLoadMessage.Visible = bShow;
            this.pnlLoadingMessage.Visible = bShow;

            if (bShow)
            {
                this.pnlLoadingMessage.BringToFront();
            }

            Application.DoEvents();
        }
        private void ZeigeFehlerMeldung(string sText)
        {
            try
            {
                this.dispErrorText.Text = sText;
                this.dispErrorText.Visible = true;
                this.dispErrorText.BringToFront();
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
                
                bRet = false;
            }
            else
            {
                bRet = true;
            }

            return bRet;
        }
        private void ErstelleTestDaten()
        {
            // Bestell und Lieferantendaten 
            // müssen auch für die Simulation passen
            if (!this.Starte_Taravorgabe())
            {
                return;                
            }

            this.Gui2Class();

            // Loading Info Panel
            this.InfoMessageDialog(/*Show*/ false, "");
        }
 
        private void SimulationsWerte()
        {
            this.tbBestellnummer.Text = "4512467893";
            this.tbLieferantennummer.Text = "123";
        }
        private bool Starte_Taravorgabe()
        {
            bool bRet = false;

            if ((this.tbBestellnummer.Text.Equals("")) ||
                (!cGlobalHandling.IsNumeric(this.tbBestellnummer.Text.Trim())))
            {
                this.ZeigeFehlerMeldung("Bestellnummer ungültig");
                return false;
            }
            if ( (!this.tbBestellnummer.Text.StartsWith("45") ))
            {
                this.ZeigeFehlerMeldung("Bestellnummer muss mit 45 beginnen");
                return false;
            }
            if (this.tbBestellnummer.Text.Length !=10)
            {
                this.ZeigeFehlerMeldung("Die Länge der Bestellnummer ist falsch");
                return false;
            }
            if ((this.tbLieferantennummer.Text.Equals("")) ||
                 (!cGlobalHandling.IsNumeric(this.tbLieferantennummer.Text.Trim())))
            {
                this.ZeigeFehlerMeldung("Lieferantennummer ungültig");
                return false;
            }
            return bRet;
        }
        private bool Taravorgabe(double dTaraGewicht)
        {
            bool bRet = false;

            try
            {
                // Loading Info Panel
                this.InfoMessageDialog(/*Show*/ true, "Tara wird gesetzt");

                MTTS.IND890.CE.CScale.enumTareResult eRes =
                    MTTS.IND890.CE.CScale.enumTareResult.TARE_FAILED;

                // Tara an die Waage senden
                eRes = cGlobalScale.setTare(dTaraGewicht);

                if (eRes != MTTS.IND890.CE.CScale.enumTareResult.TARE_SUCCESS)
                {

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
                // Loading Info Panel
                this.InfoMessageDialog(/*Show*/ false, "");
            }

            return bRet;
        }

        private void Gui2Class()
        {
            this.objWiegung.lBestellnummer = cGlobalHandling.TextboxToLong(this.tbBestellnummer);
            this.objWiegung.iLieferantennummer = cGlobalHandling.TextboxToInt(this.tbLieferantennummer);
            
        }
        private void Class2Gui()
        {
            if (this.objWiegung.lBestellnummer > 0)
            {
                this.tbBestellnummer.Text = this.objWiegung.lBestellnummer.ToString();
            }
            else
            {
                this.tbBestellnummer.Text = "";
            }

            if (this.objWiegung.iLieferantennummer > 0)
            {
                this.tbLieferantennummer.Text = this.objWiegung.iLieferantennummer.ToString();
            }
            else
            {
                this.tbLieferantennummer.Text = "";
            }

        }
        
        private void cmdsimu_Click(object sender, EventArgs e)
        {
            this.SimulationsWerte();
        }
    }
}