using System;
using System.Threading;
using System.Windows.Forms;
using MTTS.IND890.CE;
using Wollsdorf_Spaltwaage.Allgemein.ScaleEngine;
using Wollsdorf_Spaltwaage.Allgemein.SQL;
using Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Controls;
using Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Data;

namespace Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Fenster.Wiegen
{
    internal partial class frmWiegen : Form
    {
        private bool bISActiveted;
        private cWiegung objWiegung;
        private ctrlPalette objSelAktivUserControl;
            
        public frmWiegen(
            ref cWiegung Wiegung, 
            ref ctrlPalette SelControl)
        {
            InitializeComponent();

            this.bISActiveted = false;
            this.objWiegung = Wiegung;
            this.objSelAktivUserControl = SelControl; // Enhält das Usercontrol samt Datenklasse

            this.cmdCancel.Visible = false;
            this.cmdRetry.Visible = false;
            this.cmdSimu1.Visible = SETTINGS.IS_EntwicklungsPC();
        }

        private void frmWiegen_Load(object sender, EventArgs e)
        {
            cGlobalHandling.CenterForm(this, 40);            
        }
        private void frmWiegen_Activated(object sender, EventArgs e)
        {
            if (!this.bISActiveted)
            {
                this.bISActiveted = true;
                
                this.starteWiegeablauf(-1);
                Thread.Sleep(1000);
            }
        }
        
   
        private void starteWiegeablauf(double dSimulationL)
        {
            this.dispErrorText.Visible = false;

            this.Starte_Gewichtsübernahme(
                dSimulationL /*Ab Null wird der Wert als Simulationswert verwendet*/);
        }

        private DialogResult messageBoxDoppelAbfrage()
        {
            DialogResult mB = DialogResult.Abort;
            do
            {
                //Allgemein.cGlobalHandling.TransferRS232(ref objWiegung, 2, this.bWiegung1Erfolgt, this.bWiegung2Erfolgt);
                mB = cGlobalHandling.MessageBoxYesNoSicher("Ist der Ausdruck in Ordnung ?", "Ausdruck okay");
                //if (Starte_DatenExport())
                //{
                //    Salzwaage.cSalzwaage_Handling.Delete_All_SalzRecords(this.objSalzWiegung.lBestellnummer);
                //    this.Close();
                //}

            } while (mB != DialogResult.Yes);
            return mB;
        }
        private void cmdsimuW1_Click(object sender, EventArgs e)
        {
            /*Ab Null wird der Wert als Simulationswert verwendet*/
            this.starteWiegeablauf(400);
        }
        private void cmdsimuW1b_Click(object sender, EventArgs e)
        {
            /*Ab Null wird der Wert als Simulationswert verwendet*/
            this.starteWiegeablauf(400);
        }
        private void cmdRetry_Click(object sender, EventArgs e)
        {
            this.starteWiegeablauf(-1);
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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

        /// <summary>
        /// Wenn SimuWert >=0 dann wird Simuwert verwendet
        /// </summary>
        /// <param name="dSimuWert"></param>
        /// <returns></returns>
        private void Starte_Gewichtsübernahme(double dSimuWert)
        {
            try
            {      
                this.ZeigeMeldung("Bitte warten, Lese Gewicht");
                 
                CScaleWeight scaleWeight = null;

                // Lese Gewicht von Waage mit Fehlerhandling
                if (!this.Lese_Gewicht(ref scaleWeight, dSimuWert))
                {
                    this.cmdCancel.Visible = true;
                    this.cmdRetry.Visible = true;
                }
                else
                {
                    // Positives Gewicht. Summe zu der Palette hinzufügen
                    objSelAktivUserControl.objBeladungsDaten.dWiegung_Gesamtgewicht += scaleWeight.NetWeight.Weight;
                    objSelAktivUserControl.objBeladungsDaten.iWiegung_Gesamtanzahl ++;

                    // Die letzte Wiegung für das Storno puffern um gegebenenfalls minus rechnen zu können
                    objSelAktivUserControl.objBeladungsDaten.dWiegung_LastNetto = scaleWeight.NetWeight.Weight;

                    //iCurr.dtWiegezeitpunkt = DateTime.Now;
                    //iCurr.dBrutto = scaleWeight.GrossWeight.Weight;
                    //iCurr.dTara = scaleWeight.TareWeight.Weight;
                    //iCurr.dNetto = scaleWeight.NetWeight.Weight; 

                    DialogResult = DialogResult.OK;
                    cWiegung_Handling.Save_Palette(objSelAktivUserControl);
                    this.Close();
                }
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
        }
        private bool Lese_Gewicht(ref CScaleWeight scaleWeight, double dSimuWert)
        {
            bool bRet = false;
            scaleWeight = null;
            
            try
            {
                scaleWeight = cGlobalScale.GetDynamic();
                
                if (scaleWeight == null)
                {
                    // Hide Message Panel
                    this.ZeigeMeldung("");
                     
                    this.dispWaitText.Visible = false;
                    this.ZeigeFehlerMeldung("Fehler\nKein gültiges Gewicht!");
                    bRet = false;
                }
                else
                {
                    // Hier muss die Simulation rein
                    if (dSimuWert >= 0)
                    {
                        scaleWeight.NetWeight = new CWeight(dSimuWert, 0, "kg");
                    }
                    // Ende Simulation
                }


                if (scaleWeight.NetWeight.Weight > this.objWiegung.objSettings.dEinzelWiegungMaxGewicht)
                {
                    // Hide Message Panel
                    this.ZeigeMeldung("");

                    this.dispWaitText.Visible = false;
                    this.ZeigeFehlerMeldung("Gewicht über Maximalgewicht!");
                    bRet = false;
                }
                else if (scaleWeight.NetWeight.Weight < this.objWiegung.objSettings.dEinzelWiegungMinGewicht)
                {
                    // Hide Message Panel
                    this.ZeigeMeldung("");

                    this.dispWaitText.Visible = false;
                    this.ZeigeFehlerMeldung("Gewicht unter Minimalgewicht!");
                    bRet = false;
                }
                else
                {
                    bRet = true;
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException("Wiegen", ex);
            }


            return bRet;
        }       
    }
}