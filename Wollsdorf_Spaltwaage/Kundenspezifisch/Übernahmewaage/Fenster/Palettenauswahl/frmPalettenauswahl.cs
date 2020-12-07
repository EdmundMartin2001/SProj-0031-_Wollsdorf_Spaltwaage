using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Allgemein;
using Wollsdorf_Spaltwaage.Allgemein.ButtonBar;
using Wollsdorf_Spaltwaage.Allgemein.DIO_RS485;
using Wollsdorf_Spaltwaage.Allgemein.Forms;
using Wollsdorf_Spaltwaage.Allgemein.ScaleEngine;
using Wollsdorf_Spaltwaage.Allgemein.SQL;
using Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Controls;
using Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Data;
using Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Fenster.Wiegen;
using System.Threading;

namespace Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Fenster.Palettenauswahl
{
    internal partial class frmPalettenauswahl : Form
    {
        private ctrlPalette objSelPal;

        private cWiegung objWiegung;
        private cSMT_DIO objDIO;

        private readonly List<ctrlPalette> objPalettenUserControlList;
        private bool bLesemodus;
        private bool bStorno = false;
        private bool bAbschluss = false;

        public frmPalettenauswahl(cWiegung Wiegung, bool lesemodus)
        {                         
            InitializeComponent();

            this.objPalettenUserControlList = new List<ctrlPalette>();
            this.objPalettenUserControlList.Add(this.ctrlPalette1);
            this.objPalettenUserControlList.Add(this.ctrlPalette2);
            this.objPalettenUserControlList.Add(this.ctrlPalette3);
            this.objPalettenUserControlList.Add(this.ctrlPalette4);
            this.objPalettenUserControlList.Add(this.ctrlPalette5);
            this.objPalettenUserControlList.Add(this.ctrlPalette6);

            this.timer1.Interval = 4000;
            this.timer1.Enabled = false;
            this.objWiegung = Wiegung;
            this.bLesemodus = lesemodus;
            this.objSelPal = null;

            this.objDIO = new cSMT_DIO();
        }
        private void frmPalettenauswahl_Load(object sender, EventArgs e)
        {
            cFormStyle.FORM_LOAD(this, null);
            
            // Optischen Anzeige das die Digital IO aktiv sind
            this.dispTopLabelLeft.Text = this.objWiegung.objSettings.get_ArbeitsplatzName;

            this.Init_ButtonBar();
            this.InitPaletten();

            if (this.objWiegung.objSettings.bDioeingänge)
            {
                this.dispTopLabelLeft.Text += " <Digial IO = Ein>";
                cGlobalScale.objCIND890APIClient_DigitalIO.DiscreteIO.OnDIOInput += new MTTS.IND890.CE.CDiscreteIO.DIOInput(DiscreteIO_OnDIOInput);
            }
            else
            {
                this.dispTopLabelLeft.Text += " <Digial IO = Aus>";
            }

            this.timer2.Interval = 500;
            this.timer2.Enabled = true;
        }
        
        private void DiscreteIO_OnDIOInput(byte location, byte port, byte value)
        {
            int iDigitalIOEingang = 0;

            if (!this.objWiegung.objSettings.bDioeingänge)
            {
                return;
            }

            if (value < 1)
            {
                return;
            }

            // Location 1 = RelaisBox1 mit IO 1 bis 4
            // Location 2 = RelaisBox2 mit IO 5 bis 8
            if (location == 2)
            {
                iDigitalIOEingang = (4 + value);
            }
            else
            {
                iDigitalIOEingang = value;
            }

            MessageBox.Show(">>>>>>>>>>>> Test IO (" + location.ToString() +") ===>" + value.ToString() + " ===>" + iDigitalIOEingang.ToString());

            switch (iDigitalIOEingang)
            {
                case 1:
                    this.objSelPal = this.ctrlPalette1;
                    this.StarteWiegung(this);
                    break;
                case 2:
                    this.objSelPal = this.ctrlPalette2;
                    this.StarteWiegung(this);
                    break;
                case 3:
                    this.objSelPal = this.ctrlPalette3;
                    this.StarteWiegung(this);
                    break;
                case 4:
                    this.objSelPal = this.ctrlPalette4;
                    this.StarteWiegung(this);
                    break;
                case 5:
                    this.objSelPal = this.ctrlPalette5;
                    this.StarteWiegung(this);
                    break;
                case 6:
                    this.objSelPal = this.ctrlPalette6;
                    this.StarteWiegung(this);
                    break;
            }
        }
 
        private cBeladungsDaten InitBeladungsDaten(int iPalNr)
        {
            cBeladungsDaten iCurrBel = new
                Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Data.cBeladungsDaten(
                /*Palettten Nr*/iPalNr,
                /*Max Gewicht*/ this.objWiegung.objSettings.dEinzelWiegungMaxGewicht,
                /*Pal Aktiv*/   true,
                /*Bezeichnung*/ this.objWiegung.objSettings.KlassenBezeichnungsListe[iPalNr],
                /*Palettentara*/ this.objWiegung.objSettings.dPalettenTara,
                /*ArtikelNr*/ this.objWiegung.objSettings.ArtikelNrListe[iPalNr]);

            if(! cWiegung_Handling.Load_Settings(ref iCurrBel) )
            {
                MessageBox.Show("Ladefehler Palettendaten");
            }

            return iCurrBel;
        }

        private void InitPaletten() 
        {
            this.ctrlPalette1.objBeladungsDaten = InitBeladungsDaten(0);
            this.ctrlPalette2.objBeladungsDaten = InitBeladungsDaten(1);
            this.ctrlPalette3.objBeladungsDaten = InitBeladungsDaten(2);
            this.ctrlPalette4.objBeladungsDaten = InitBeladungsDaten(3);
            this.ctrlPalette5.objBeladungsDaten = InitBeladungsDaten(4);
            this.ctrlPalette6.objBeladungsDaten = InitBeladungsDaten(5);

            foreach (ctrlPalette AktCtrl in this.objPalettenUserControlList)
            {
                AktCtrl.UpdateCtrl();
            }                        
        }
        private void Init_ButtonBar()
        {
            this.ctrlButtonBar1.DISP_PAGE(1);
            this.ctrlButtonBar1.SET_BUTTON_TEXT(1, "Abbruch", "§Close");
            this.ctrlButtonBar1.SET_BUTTON_TEXT(2, "Nullstellen", "§Nullstellen");
            this.ctrlButtonBar1.SET_BUTTON_TEXT(3, "Tarieren", "§Tarieren");
            this.ctrlButtonBar1.SET_BUTTON_TEXT(4, "", "§FREE4");
            this.ctrlButtonBar1.SET_BUTTON_TEXT(5, "Abschluss", "§ABSCHLUSS");
            this.ctrlButtonBar1.SET_BUTTON_TEXT(6, "Storno", "§STORNO");
            this.ctrlButtonBar1.SET_BUTTON_TEXT(7, "", "§FREE7");
            this.ctrlButtonBar1.SET_BUTTON_TEXT(8, "", "§FREE8");

            Icon myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_House;
            this.ctrlButtonBar1.Button_F1.Bild_Icon = myIcon;

            myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_Zero;
            this.ctrlButtonBar1.Button_F2.Bild_Icon = myIcon;

            myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_Tare;
            this.ctrlButtonBar1.Button_F3.Bild_Icon = myIcon;

            myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_Report_4;
            this.ctrlButtonBar1.Button_F5.Bild_Icon = myIcon;

            myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_Delete_2;
            this.ctrlButtonBar1.Button_F6.Bild_Icon = myIcon;

            this.ctrlButtonBar1.EventButtonClick += new ctrlButtonBar._EventButtonClick(ctrlButtonBar1_EventButtonClick);
        }
        private void ctrlButtonBar1_EventButtonClick(object sender, string fTaste, int iTastenCode, string fTag)
        {
            try
            {
                switch (fTag.ToUpper())
                {
                    case "§CLOSE":
                        this.Close();
                        break;
                    case "§NULLSTELLEN":
                        this.ShowStatus("Waage wird Nullgestellt!");
                        cGlobalScale.ZeroScale();
                        this.ShowStatus("Bereit...");
                        break;
                    case "§TARIEREN":
                        this.ShowStatus("Waage wird Tariert!");
                        cGlobalScale.TareScale();
                        this.ShowStatus("Bereit...");
                        break;
                    case "§STORNO":
                        this.ShowStatus("Bitte Palette für Storno auswählen!");
                        this.timer1.Enabled = true;
                        this.bStorno = true;
                        break;
                    case "§ABSCHLUSS":                        
                        this.ShowStatus("Bitte Palette für Abschluss auswählen!");
                        this.timer1.Enabled = true;
                        this.bAbschluss = true;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException("ctrlButtonBar1_EventButtonClick11", ex);
            }
        }

        private void ShowStatus(string sStatus) 
        {
            this.dispInfo.Text = sStatus;
            Application.DoEvents();

            System.Threading.Thread.Sleep(1000);
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
            }
            catch (Exception)
            {
            }
        }
 
        private void Wiegung_Storno(ref ctrlPalette SelControl) 
        {
            this.ShowStatus("Wiegung wird storniert!");

            if (SelControl.objBeladungsDaten.dWiegung_LastNetto > 0)
            {
                DialogResult mB = cGlobalHandling.MessageBoxYesNoSicher("Sind Sie sicher?", "Storno");

                if (mB == DialogResult.Yes)
                {
                    SelControl.objBeladungsDaten.iWiegung_Gesamtanzahl--;
                    SelControl.objBeladungsDaten.dWiegung_Gesamtgewicht -= SelControl.objBeladungsDaten.dWiegung_LastNetto;                    
                    SelControl.objBeladungsDaten.dWiegung_LastNetto = 0;
                }
            }

            SelControl.UpdateCtrl();
            SelControl.ResetButton();

            this.ShowStatus("Bereit...");
        }
        private void Wiegung_Abschluss(ref ctrlPalette SelControl) 
        {
            this.ShowStatus("Palette wird abgeschlossen!");

            DialogResult mB = cGlobalHandling.MessageBoxYesNoSicher("Sind Sie sicher?", "Abschluss");
            
            if (mB == DialogResult.Yes)
            {
                if (this.StarteDrucken(ref SelControl))
                {
                    SelControl.objBeladungsDaten.dWiegung_Gesamtgewicht = 0;
                    SelControl.objBeladungsDaten.iWiegung_Gesamtanzahl = 0;
                    SelControl.objBeladungsDaten.dWiegung_LastNetto = 0;

                    cWiegung_Handling.Save_Palette(SelControl);
                }
            }

            SelControl.UpdateCtrl();
            SelControl.ResetButton();
        }

        private bool StarteDrucken(ref ctrlPalette PalettenUserControl) 
        {
            bool bRet = false;

            do
            {
                this.ShowStatus("Starte Druckvorgang!");
                
                try
                {
                    // Enthält R:Wollsdor.ZPL aus dem Z-Designer
                    // Ein geändertes Layout muss mit EXPORT TO PRINTER in eine Datei exportiert werden.
                    // Danach den gesamten Layout Text in den Resourcefile einfügen.
                    string sZPL_Label = Wollsdorf_Spaltwaage.Properties.Resources.sZPL_Label;
                    cGlobalHandling.Drucke_Daten(sZPL_Label);
                }
                catch (Exception ex)
                {
                    SiAuto.LogException("StarteDrucken ZPL_Label", ex);
                }

                try
                {                    
                    string sZPL = Wollsdorf_Spaltwaage.Properties.Resources.sZPL_Data;

                    

                    sZPL = sZPL.Replace("@ARTNR@", PalettenUserControl.objBeladungsDaten.sSettings_ArtikelNr);
                    sZPL = sZPL.Replace("@ARTBEZ@",  PalettenUserControl.objBeladungsDaten.sSettings_Bezeichnung);
                    sZPL = sZPL.Replace("@PALNR@", cGlobalNummerkreis.Nummernkreis1_GetCurrent().ToString()); //SOLL Fortlaufend sein
                    sZPL = sZPL.Replace("@GRUPPENN1@", this.objWiegung.objSettings.sGruppenName1);
                    sZPL = sZPL.Replace("@GRUPPENN2@", this.objWiegung.objSettings.sGruppenName2);
                    sZPL = sZPL.Replace("@STK@", PalettenUserControl.objBeladungsDaten.iWiegung_Gesamtanzahl.ToString());
                    sZPL = sZPL.Replace("@GEW@", PalettenUserControl.objBeladungsDaten.dWiegung_Gesamtgewicht.ToString("####0.0") + " kg"); //LÄNGE Stimmt im Zebra?
                    sZPL = sZPL.Replace("@DATE@", DateTime.Now.ToString("dd.MM.yyyy HH:mm") );

                    cGlobalHandling.Drucke_Daten(sZPL);
                }
                catch (Exception ex)
                {
                    SiAuto.LogException("StarteDrucken ZPL_Data", ex);
                }

                DialogResult mB = cGlobalHandling.MessageBoxYesNoSicher("Ist der Ausdruck in Ordnung ?", "Ausdruck okay");

                if (mB == DialogResult.Yes)
                {
                    cGlobalNummerkreis.Nummernkreis1_SetNext();
                    bRet = true;
                    break;
                }

            } while (true);


            return bRet;
        }
        private void StarteWiegung(object sender)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new WaitCallback(StarteWiegung), this);
            }
            else
            {
                this.StarteWiegung_go();
            }
        }

        private void StarteWiegung_go()
        {            
            if (this.bAbschluss == true)
            {
                if (this.objSelPal.objBeladungsDaten.dWiegung_Gesamtgewicht > 0)
                {
                    this.Wiegung_Abschluss(ref this.objSelPal);                    
                }
                this.bAbschluss = false;
                this.objSelPal.ResetButton();
            }
            else if (this.bStorno == true)
            {
                this.Wiegung_Storno(ref this.objSelPal);
                this.bStorno = false;
            }
            else
            {
                #region Wiegung durchführen
                this.ShowStatus("Starte Wiegung!");
                frmWiegen fWiegen = new frmWiegen(ref objWiegung, ref this.objSelPal);
                fWiegen.ShowDialog();

                if (fWiegen.DialogResult == DialogResult.OK)
                {
                    this.objSelPal.UpdateCtrl();

                    // Wenn die Maximalbeladung einer Palette erreicht ist, dann wird ein Etikett ausgedruckt
                    // und die Summe gelöscht
                    if (this.objSelPal.objBeladungsDaten.dWiegung_Gesamtgewicht >= this.objWiegung.objSettings.dPalettenMax)
                    {
                        this.StarteDrucken(ref this.objSelPal);
                    }

                    this.objSelPal.ResetButton(); // Farbe der Buttons aufheben
                }
                #endregion
            }

            this.objSelPal.ResetButton();
            this.ShowStatus("Bereit...");
        }

        private void ctrlPalette1_Click(object sender, EventArgs e)
        {
            this.objSelPal = this.ctrlPalette1;
            this.StarteWiegung(this);           
        }
        private void ctrlPalette2_Click(object sender, EventArgs e)
        {
            this.objSelPal = this.ctrlPalette2;
            this.StarteWiegung(this);
        }
        private void ctrlPalette3_Click(object sender, EventArgs e)
        {
            this.objSelPal = this.ctrlPalette3;
            this.StarteWiegung(this);
        }
        private void ctrlPalette4_Click(object sender, EventArgs e)
        {
            this.objSelPal = this.ctrlPalette4;
            this.StarteWiegung(this);
        }
        private void ctrlPalette5_Click(object sender, EventArgs e)
        {
            this.objSelPal = this.ctrlPalette5;
            this.StarteWiegung(this);
        }
        private void ctrlPalette6_Click(object sender, EventArgs e)
        {
            this.objSelPal = this.ctrlPalette6;
            this.StarteWiegung(this);           
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.ShowStatus("Bereit...");

            this.timer1.Enabled = false;
            this.bStorno = false;
            this.bAbschluss = false;


        }
    }
}