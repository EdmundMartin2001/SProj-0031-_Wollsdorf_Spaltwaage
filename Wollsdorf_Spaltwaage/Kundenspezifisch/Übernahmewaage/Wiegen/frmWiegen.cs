using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Allgemein;
using MTTS.IND890.CE;
using System.Threading;

namespace MAN_Fahrzeugwaage
{
    internal partial class frmWiegen : Form
    {        
        private cWiegung objWiegung;
            
        private int iAktiveAchse;
        private bool bWiegung1Erfolgt;
        private bool bWiegung2Erfolgt;

        public frmWiegen(ref cWiegung Wiegung)
        {
            InitializeComponent();

            this.bWiegung1Erfolgt = false;
            this.bWiegung2Erfolgt = false;

            this.iAktiveAchse = 1;
            this.objWiegung = Wiegung;
            this.UpdateGui();

            this.cmdsimuW1a.Visible = SETTINGS.IS_EntwicklungsPC();
            this.cmdSimuW2a.Visible = SETTINGS.IS_EntwicklungsPC();
            this.cmdsimuW1b.Visible = SETTINGS.IS_EntwicklungsPC();
            this.cmdSimuW2b.Visible = SETTINGS.IS_EntwicklungsPC();

            this.ctrlButtonBar1.EventButtonClick += new Allgemein.Controls.ctrlButtonBar._EventButtonClick(ctrlButtonBar1_EventButtonClick);
        }

        private void frmWiegen_Load(object sender, EventArgs e)
        {
            Allgemein.FormHelper.cFormStyle.FORM_LOAD(this, null);
            this.dispTopLabelLeft.Text = this.objWiegung.objSettings.sArbeitsplatzname;          
            this.Init_ButtonBar();
        }

        private void Init_ButtonBar()
        {
            ctrlButtonBar1.DISP_PAGE(1);
            ctrlButtonBar1.SET_BUTTON_TEXT(1, "", "§Free1");
            ctrlButtonBar1.SET_BUTTON_TEXT(2, "", "§Free2");
            ctrlButtonBar1.SET_BUTTON_TEXT(3, "", "§Free3");
            ctrlButtonBar1.SET_BUTTON_TEXT(6, "", "§Free6");
            ctrlButtonBar1.SET_BUTTON_TEXT(7, "", "§Free7");
            ctrlButtonBar1.SET_BUTTON_TEXT(8, "Beenden ", "§CLOSE");

            Icon myIcon;

            if(this.iAktiveAchse == 1)
            {
                ctrlButtonBar1.SET_BUTTON_TEXT(4, "Wiegung 1", "§W1");
                ctrlButtonBar1.SET_BUTTON_TEXT(5, "", "§Free4");
                    
                myIcon = Properties.Resources.ico_Arrow_9;
                ctrlButtonBar1.Button_F4.ImageIcon = myIcon;

                ctrlButtonBar1.Button_F5.ImageIcon = null;
            }
            else
            {
                ctrlButtonBar1.SET_BUTTON_TEXT(4, "", "§Free4");
                ctrlButtonBar1.SET_BUTTON_TEXT(5, "Wiegung 2", "§W2");

                ctrlButtonBar1.Button_F4.ImageIcon = null;

                myIcon = Properties.Resources.ico_Arrow_9;
                ctrlButtonBar1.Button_F5.ImageIcon = myIcon;
            }

            myIcon = Properties.Resources.ico_Door;
            ctrlButtonBar1.Button_F8.ImageIcon = myIcon;

            
        }
        private void ctrlButtonBar1_EventButtonClick(object sender, string fTaste, int iTastenCode, string fTag)
        {
            try
            {
                switch (fTag.ToUpper())
                {
                    case "§W1":
                        if (!this.bWiegung1Erfolgt)
                        {
                            this.bWiegung1Erfolgt = true;
                            this.UpdateGui();
                            this.starteWiegeablauf(-1, -1);
                            
                            this.UpdateGui();
                            this.Init_ButtonBar();
                        }
                        break;
                    case "§W2":
                        if (!this.bWiegung2Erfolgt)
                        {
                            this.bWiegung2Erfolgt = true;
                            this.starteWiegeablauf(-1, -1);
                        }
                        break;
                    case "§CLOSE":
                        Allgemein.cGlobalHandling.TransferRS232(ref objWiegung, 0, false, false);
                        this.DialogResult = DialogResult.Abort;
                        this.Close();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException("ctrlButtonBar1_EventButtonClick", ex);
            }
        }


        private void starteWiegeablauf(double dSimulationL, double dSimulationR)
        {
            this.dispErrorText.Visible = false;
            this.Achse1Farbe(0);
            this.Achse2Farbe(0);
            bool bWiegungOk = this.Starte_Gewichtsübernahme(
                dSimulationL /*Ab Null wird der Wert als Simulationswert verwendet*/,
                dSimulationR /*Ab Null wird der Wert als Simulationswert verwendet*/);

            // Wenn Wiegung nicht okay, dann erneutes Wiegen ermöglichen 
            // Rücksetzen der Wiegesperre
            if (!bWiegungOk)
            {
                if (this.iAktiveAchse == 1)
                {
                    this.bWiegung1Erfolgt = false;
                }
                else if (this.iAktiveAchse == 2)
                {
                    this.bWiegung2Erfolgt = false;
                }

                return;
            }

            if (bWiegungOk)
            {
                if (((this.objWiegung.objWiegeItem_W1.dNetto_Links + this.objWiegung.objWiegeItem_W1.dNetto_Rechts) > 0) && 
                    ((this.objWiegung.objWiegeItem_W2.dNetto_Links + this.objWiegung.objWiegeItem_W2.dNetto_Rechts) > 0))
                {
                    this.Achse1Farbe(1);
                    this.Achse2Farbe(1);
                    DialogResult mB = this.messageBoxDoppelAbfrage();
                    if (mB == DialogResult.Yes)
                    {                   
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }                
            }
        }

        private DialogResult messageBoxDoppelAbfrage()
        {
            DialogResult mB = DialogResult.Abort;
            do
            {
                Allgemein.cGlobalHandling.TransferRS232(ref objWiegung, 2, this.bWiegung1Erfolgt, this.bWiegung2Erfolgt);
                mB = cGlobalHandling.MessageBoxYesNoSicher("Ist der Ausdruck in Ordnung ?", "Ausdruck okay");
                //if (Starte_DatenExport())
                //{
                //    Salzwaage.cSalzwaage_Handling.Delete_All_SalzRecords(this.objSalzWiegung.lBestellnummer);
                //    this.Close();
                //}

            } while (mB != DialogResult.Yes);
            return mB;
        }

        private void ZeigeMeldung(string sMeldung)
        {
            try
            {
                if (!sMeldung.Equals(""))
                {
                    this.dispWaitText.Text = sMeldung;
                    this.dispWaitText.Visible = true;
                    this.dispWaitText.BringToFront();
                }
                else
                {
                    this.dispWaitText.Visible = false;
                }
                Application.DoEvents();
            }
            catch (Exception)
            {
            }
        }
        //private void ZeigeFehlerMeldung(string sFehlerText)
        //{
        //    try
        //    {
        //        this.dispErrorText.Text = sFehlerText;
        //        this.dispErrorText.Visible = true;
        //        this.dispErrorText.BringToFront();
        //        Application.DoEvents();

        //        System.Threading.Thread.Sleep(1000);
        //        this.dispErrorText.Visible = false;
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        private void ZeigeFehlerMeldung(string sFehlerText)
        {
            try
            {
                this.dispErrorText.Text = sFehlerText;
                this.dispErrorText.Visible = true;
                this.dispErrorText.BringToFront();
                Application.DoEvents();

                System.Threading.Thread.Sleep(1000);
                //this.dispErrorText.Visible = false;
            }
            catch (Exception)
            {
            }
        }

        private void UpdateGui()
        {
            if ( this.iAktiveAchse == 1)
            {
                this.lbAchse.Text = "Achse 1";
                this.dispWaitText.Text = "";
            }
            else
            {
                this.lbAchse.Text = "Achse 2";
                this.dispStatus.Text = "Bitte für die Wiegung der zweiten Achse vorbereiten";
                this.dispWaitText.Text = "";
            }

            if ((objWiegung.objWiegeItem_W1.dNetto_Links > 0) && (objWiegung.objWiegeItem_W1.dNetto_Rechts > 0))
            {
                this.lblA1Links.Text = objWiegung.objWiegeItem_W1.dNetto_Links.ToString();
                this.lblA1Rechts.Text = objWiegung.objWiegeItem_W1.dNetto_Rechts.ToString();
            }
            if ((objWiegung.objWiegeItem_W2.dNetto_Links > 0) && (objWiegung.objWiegeItem_W2.dNetto_Rechts > 0))
            {
                this.lblA2Links.Text = objWiegung.objWiegeItem_W2.dNetto_Links.ToString();
                this.lblA2Rechts.Text = objWiegung.objWiegeItem_W2.dNetto_Rechts.ToString();
            }
        }

        /// <summary>
        /// Wenn SimuWert >=0 dann wird Simuwert verwendet
        /// </summary>
        /// <param name="dSimuWert"></param>
        /// <returns></returns>
        private bool Starte_Gewichtsübernahme(double dSimuWertLinks, double dSimuWertRechts)
        {
            bool bRet = true;
            
            try
            {      
                if (this.iAktiveAchse == 1)
                {
                    this.ZeigeMeldung("Bitte warten, Lese Gewicht Erstwiegung");
                }
                else if (this.iAktiveAchse == 2 && 
                    ((objWiegung.objWiegeItem_W1.dNetto_Links + objWiegung.objWiegeItem_W1.dNetto_Rechts) > 0))
                {
                    this.ZeigeMeldung("Bitte warten, Lese Gewicht Zweitwiegung");
                }
                else
                {
                    this.ZeigeMeldung("");

                    this.dispWaitText.Visible = false;
                    this.ZeigeFehlerMeldung("Fehler\nKein gültiges Gewicht");
                    bRet = false;
                }

                CScaleWeight wLinks = null;
                CScaleWeight wRechts = null;

                // Lese Gewicht von Waage Links und Rechts mit Fehlerhandling und Anzeige
                if (!this.Lese_Gewicht(ref wLinks, ref wRechts))
                {
                    bRet = false;
                }
                else
                {
                    // Hier muss die Simulation rein
                    if ((dSimuWertLinks >= 0) && (iAktiveAchse == 1))
                    {
                        wLinks.NetWeight = new CWeight(dSimuWertLinks, 0, "kg");
                        wRechts.NetWeight = new CWeight(dSimuWertRechts, 0, "kg");
                    }
                    else if ((dSimuWertLinks >= 0) && (iAktiveAchse == 2))
                    {
                        wLinks.NetWeight = new CWeight(dSimuWertLinks, 0, "kg");
                        wRechts.NetWeight = new CWeight(dSimuWertRechts, 0, "kg");
                    }
                    // Ende Simulation

                    #region Prüfe Plausibilität der Gewichtsübernahme
                    if ((wLinks.NetWeight.Weight + wRechts.NetWeight.Weight) > 5000)
                    {
                        this.dispWaitText.Visible = false;
                        if(this.iAktiveAchse == 1)
                        {
                            this.Achse1Farbe(2);
                        }
                        else
                        {
                            this.Achse2Farbe(2);
                        }
                        this.ZeigeFehlerMeldung("Fehler\nÜber Maximalgewicht");
                        bRet = false;
                    }

                    if (this.iAktiveAchse == 2)
                    {
                        if (((this.objWiegung.objWiegeItem_W1.dNetto_Links + this.objWiegung.objWiegeItem_W1.dNetto_Rechts) + 
                            (wLinks.NetWeight.Weight + wRechts.NetWeight.Weight)) > 9000)
                        {
                            this.dispWaitText.Visible = false;
                            this.Achse1Farbe(2);
                            this.Achse2Farbe(2);
                            this.ZeigeFehlerMeldung("Gesamtgewicht zu hoch (max. 9000)");
                            bRet = false;
                        }
                    }
                    #endregion 

                    // Wenn Wiegung positiv und Plausibilitäten geprüft
                    //if ( bRet)
                    //{
                        Data.cData_Wiegung_Item iCurr = null;

                        if (this.iAktiveAchse == 1)
                        {
                            iCurr = this.objWiegung.objWiegeItem_W1;
                        }
                        else
                        {
                            iCurr = this.objWiegung.objWiegeItem_W2;
                        }
                        
                        
                        iCurr.dBrutto_Links = wLinks.GrossWeight.Weight;
                        iCurr.dNetto_Links = wLinks.NetWeight.Weight;
                        iCurr.dTara_Links = wLinks.TareWeight.Weight;

                        iCurr.dBrutto_Rechts = wRechts.GrossWeight.Weight;
                        iCurr.dNetto_Rechts = wRechts.NetWeight.Weight;
                        iCurr.dTara_Rechts = wRechts.TareWeight.Weight;
                        
                        iCurr.dtWiegung = DateTime.Now;
                        iCurr.sEinheit= wLinks.NetWeight.Unit;

                        if (this.iAktiveAchse == 1)
                        {
                            Allgemein.cGlobalHandling.TransferRS232(ref objWiegung, 1, this.bWiegung1Erfolgt, this.bWiegung2Erfolgt);
                        }
                    //}
                }

                if (bRet && this.iAktiveAchse == 1)
                {
                    this.Achse1Farbe(1);
                    this.iAktiveAchse = 2;
                }

                this.UpdateGui();
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
            }
            finally
            {
                // Hide Message Panel
                this.ZeigeMeldung("");

                this.dispWaitText.Visible = false;
                Application.DoEvents();
            }

            return bRet;
        }

        private bool Lese_Gewicht(ref CScaleWeight sLinks, ref CScaleWeight sRechts)
        {
            bool bRet = false;
            sRechts = null;
            sLinks = null;
            
            try
            {
                sLinks = cGlobalScale.GetStabile(/*Linke oder Rechte Waage*/ 0);
                
                if (sLinks == null)
                {
                    // Hide Message Panel
                    this.ZeigeMeldung("");

                    this.dispWaitText.Visible = false;
                    this.ZeigeFehlerMeldung("Fehler\nKein gültiges Gewicht");
                    bRet = false;
                }
                else 
                {
                    sRechts = cGlobalScale.GetStabile(/*Linke oder Rechte Waage*/ 1);

                    if (sRechts == null)
                    {
                        // Hide Message Panel
                        this.ZeigeMeldung("");

                        this.dispWaitText.Visible = false;
                        this.ZeigeFehlerMeldung("Fehler\nKein gültiges Gewicht");
                        bRet = false;
                    }
                    else
                    {
                        bRet = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException("Wiegen", ex);
            }


            return bRet;
        }

        private void Achse1Farbe(int iStatus) 
        {
            if (iStatus == 0)
            {
                this.lblA1Links.ForeColor = Color.Black;
                this.lblA1Rechts.ForeColor = Color.Black;
                this.lblA1Links.BackColor = Color.WhiteSmoke;
                this.lblA1Rechts.BackColor = Color.WhiteSmoke;
            }
            else if (iStatus == 1)
            {
                this.lblA1Links.ForeColor = Color.Black;
                this.lblA1Rechts.ForeColor = Color.Black;
                this.lblA1Links.BackColor = Color.LightGreen;
                this.lblA1Rechts.BackColor = Color.LightGreen;
            }
            else 
            {
                this.lblA1Links.ForeColor = Color.White;
                this.lblA1Rechts.ForeColor = Color.White;
                this.lblA1Links.BackColor = Color.Red;
                this.lblA1Rechts.BackColor = Color.Red;
            }
        }

        private void Achse2Farbe(int iStatus)
        {
            if (iStatus == 0)
            {
                this.lblA2Links.ForeColor = Color.Black;
                this.lblA2Rechts.ForeColor = Color.Black;
                this.lblA2Links.BackColor = Color.WhiteSmoke;
                this.lblA2Rechts.BackColor = Color.WhiteSmoke;
            }
            else if (iStatus == 1)
            {
                this.lblA2Links.ForeColor = Color.Black;
                this.lblA2Rechts.ForeColor = Color.Black;
                this.lblA2Links.BackColor = Color.LightGreen;
                this.lblA2Rechts.BackColor = Color.LightGreen;
            }
            else
            {
                this.lblA2Links.ForeColor = Color.White;
                this.lblA2Rechts.ForeColor = Color.White;
                this.lblA2Links.BackColor = Color.Red;
                this.lblA2Rechts.BackColor = Color.Red;
            }
        }

        private void cmdsimuW1_Click(object sender, EventArgs e)
        {
            if (!this.bWiegung1Erfolgt)
            {
                this.bWiegung1Erfolgt = true;
                this.UpdateGui();

                /*Ab Null wird der Wert als Simulationswert verwendet*/
                this.starteWiegeablauf(1970, 2030);

                this.UpdateGui();
                this.Init_ButtonBar();
            }
        }

        private void cmdSimuW2_Click(object sender, EventArgs e)
        {
            if (!this.bWiegung2Erfolgt)
            {
                this.bWiegung2Erfolgt = true;
                this.starteWiegeablauf(2470, 2530);
            }
        }

        private void cmdsimuW1b_Click(object sender, EventArgs e)
        {
            if (!this.bWiegung1Erfolgt)
            {
                this.bWiegung1Erfolgt = true;
                this.UpdateGui();

                /*Ab Null wird der Wert als Simulationswert verwendet*/
                this.starteWiegeablauf(3200, 2800);

                this.UpdateGui();
                this.Init_ButtonBar();
            }            
        }

        private void cmdSimuW2b_Click(object sender, EventArgs e)
        {
            if (!this.bWiegung2Erfolgt)
            {
                this.bWiegung2Erfolgt = true;
                this.starteWiegeablauf(3200, 2800);
            }        
        }   
    }
}