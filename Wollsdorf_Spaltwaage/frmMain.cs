namespace Wollsdorf
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using Allgemein;
    using Wollsdorf.Spaltwaage;

    public partial class frmMain : Form
    {
        private Wollsdorf.Spaltwaage.cWiegung objWiegung;
        
        public frmMain()
        {
            InitializeComponent();

            this.objWiegung = new Wollsdorf.Spaltwaage.cWiegung();
            this.objWiegung.objSettings = new Wollsdorf.Data.cData_Settings();
        
            this.pnlLoadingMessage.Visible = false;
            this.dispLoadMessage.Visible = false;
            this.timer1.Interval = 800;
            this.timer1.Enabled = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Allgemein.FormHelper.cFormStyle.FORM_LOAD(this, null);

            cData_Settings_Handling.Load_Settings(this.objWiegung.objSettings);

            this.dispTopLabelLeft.Text = this.objWiegung.objSettings.get_ArbeitsplatzName;

            // Visuelle Anzeige das der Servicemode ein ist
            this.RefreshServiceModeGui();
            
            this.Init_ButtonBar();

            ctrlButtonBar1.EventButtonClick += new Allgemein.Controls.ctrlButtonBar._EventButtonClick(ctrlButtonBar1_EventButtonClick);

            try
            {
                if (!cGlobalScale.STARTE_SCALE())
                {
                    this.ctrlButtonBar1.Button_F4.Enabled = false;
                    MessageBox.Show("Fehler keine Waage", "Starte Waage");
                }
                else
                {
                    cGlobalScale.Show_Scale();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                this.ctrlButtonBar1.Button_F4.Enabled = false;
                MessageBox.Show(ex.Message, "Starte ScaleEngine");
                throw;
            }
        }
        private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cGlobalScale.BEENDE_SCALE();
        }
        private void frmMain_Click(object sender, EventArgs e)
        {
            this.SMT_ServiceMode();
        }
        private void cmdClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (cGlobalHandling.Frage_Passwort(
                     this.objWiegung.objSettings.get_ArbeitsplatzName,
                    /*HideScaleWindow*/ false,
                     this.objWiegung.objSettings.sServicePasswort,
                    /*Terminal ID*/ "",
                    /*ShowServiceMode*/ false) == DialogResult.OK)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
            }
            finally
            {
            }
        }
        private void cmdSetup_Click(object sender, EventArgs e)
        {
            DialogResult dlgRes;

            try
            {
                dlgRes = cGlobalHandling.Frage_Passwort(
                    this.objWiegung.objSettings.get_ArbeitsplatzName,
                    /*ShoHideScale*/ false,
                    this.objWiegung.objSettings.sServicePasswort,
                    /*TerminalID*/ "",
                    /*ShowServiceMode*/ true);

                if (dlgRes == DialogResult.Ignore)
                {
                    // Invertiere den Status Service Mode
                    cData_Settings_Handling.SetSMTServiceMode(this.objWiegung.objSettings);

                    // Visuelle Anzeige das der Servicemode ein ist
                    this.RefreshServiceModeGui();

                    this.Init_ButtonBar();
                }
                else if (dlgRes == DialogResult.OK)
                {
                    frmSetup f = new frmSetup(ref this.objWiegung);
                    dlgRes = f.ShowDialog();
                    f.Dispose();
                    f = null;

                    if (dlgRes == DialogResult.OK)
                    {
                        cData_Settings_Handling.Load_Settings(this.objWiegung.objSettings);
                        this.dispTopLabelLeft.Text = this.objWiegung.objSettings.get_ArbeitsplatzName;

                        this.RefreshServiceModeGui();

                        this.Init_ButtonBar();
                    }
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
            }
        }
        private void cmdReboot_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = cGlobalHandling.MessageBoxYesNoSicher(
                "Sind Sie sicher das sie das Gerät neustarten wollen?", "Neustart");

            if (dialogResult == DialogResult.Yes)
            {
                cGlobalScale.objCIND890APIClient.Terminal.ApplicationMode = MTTS.IND890.CE.CTerminal.enumDisplayMode.FULL_SCREEN;
                cGlobalScale.BEENDE_SCALE();

                cGlobalHandling.rebootDevice();
            }
        }
        private void cmdStartoption_Click(object sender, EventArgs e)
        {
            try
            {
                if (cGlobalHandling.Frage_Passwort(
                     this.objWiegung.objSettings.get_ArbeitsplatzName,
                    /*HideScaleWindow*/ false,
                     this.objWiegung.objSettings.sServicePasswort,
                    /*Terminal ID*/ "",
                    /*ShowServiceMode*/ false) == DialogResult.OK)
                {
                    frmStartOptionen frmSO = new frmStartOptionen();
                    DialogResult dlgRes = frmSO.ShowDialog();
                    frmSO.Dispose();
                    frmSO = null;

                    if (dlgRes == DialogResult.OK)
                    {
                        cData_Settings_Handling.Load_Settings(this.objWiegung.objSettings);
                        this.dispTopLabelLeft.Text = this.objWiegung.objSettings.get_ArbeitsplatzName;
                    }
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
            }
            finally
            {
            }
        }
        private void Init_ButtonBar()
        {
            this.ctrlButtonBar1.DISP_PAGE(1);
            this.ctrlButtonBar1.SET_BUTTON_TEXT(1, "", "§Free1");
            this.ctrlButtonBar1.SET_BUTTON_TEXT(2, "Nullstellen", "§Nullstellen");
            this.ctrlButtonBar1.SET_BUTTON_TEXT(3, "Tarieren", "§Tarieren");
            this.ctrlButtonBar1.SET_BUTTON_TEXT(4, "Start", "§START");
            this.ctrlButtonBar1.SET_BUTTON_TEXT(5, "", "§Free5");
            this.ctrlButtonBar1.SET_BUTTON_TEXT(6, "", "§Free6");

            Icon myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_Zero;
            this.ctrlButtonBar1.Button_F2.Bild_Icon = myIcon;

            myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_Tare;
            this.ctrlButtonBar1.Button_F3.Bild_Icon = myIcon;

            myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_ArrowRight;
            this.ctrlButtonBar1.Button_F4.Bild_Icon = myIcon;
            
            //if(objWiegung.objSettings.Arbeitsplatztyp == Wollsdorf.Data.cData_Settings.eArbeitsplatztyp.Salzwaage ||
            //   SETTINGS.IS_EntwicklungsPC())
            //{
            //    this.ctrlButtonBar1.SET_BUTTON_TEXT(5, "Start\nSalzwaage", "§StartSW");

            //    myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_ArrowRight;
            //    this.ctrlButtonBar1.Button_F5.Bild_Icon = myIcon;  //Starte Salzwaare
            //}

            //if (objWiegung.objSettings.Arbeitsplatztyp == Wollsdorf.Data.cData_Settings.eArbeitsplatztyp.Übernahmewaage ||
            //   SETTINGS.IS_EntwicklungsPC())
            //{
            //    this.ctrlButtonBar1.SET_BUTTON_TEXT(4, "Start\nÜbernahme", "§StartÜW");
            //    this.ctrlButtonBar1.SET_BUTTON_TEXT(7, "Klassen", "§Klassen");                
            //    this.ctrlButtonBar1.SET_BUTTON_TEXT(8, "Lieferanten\nabschluss", "§Lieferantenabschluss");

            //    myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_ArrowRight;
            //    this.ctrlButtonBar1.Button_F4.Bild_Icon = myIcon; // Starte Übernahmewaage

            //    myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_Report_4;
            //    this.ctrlButtonBar1.Button_F7.Bild_Icon = myIcon; // Klasseneditor

            //    myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_Report_4;
            //    this.ctrlButtonBar1.Button_F8.Bild_Icon = myIcon; // Lieferantenabschluss
            //}
        }
        private void ctrlButtonBar1_EventButtonClick(object sender, string fTaste, int iTastenCode, string fTag)
        {
            try
            {
                switch (fTag.ToUpper())
                {
                    case "§CLOSE": 
                        
                    case "§NULLSTELLEN":
                        cGlobalScale.ZeroScale();
                        break;
                    case "§TARIEREN":
                        cGlobalScale.TareScale();
                        break;
                    //case "§STARTÜW":
                    //    this.Starte_Ablauf();
                    //    cGlobalScale.Show_Scale();
                    //    break;
                    //case "§STARTSW":

                    //    cGlobalScale.Show_Scale();
                    //    break;
                    case "§START":
                        frmSAPEingabe frmSAPEingabe = new frmSAPEingabe(ref objWiegung);
                        frmSAPEingabe.ShowDialog(); 
                        break;
                    //case "§PALETTENAUSWAHLREADONLY":
                    //    frmPalettenauswahl frmPalette1 = new frmPalettenauswahl(objWiegung, true);
                    //    frmPalette1.ShowDialog();
                    //    break;
                    //case "§PALETTENAUSWAHL":
                    //    frmPalettenauswahl frmPalette2 = new frmPalettenauswahl(objWiegung, false);
                    //    frmPalette2.ShowDialog();
                    //    break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException("ctrlButtonBar1_EventButtonClick2", ex);
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

        private void SMT_ServiceMode()
        {
            try
            {
                if (cGlobalHandling.Frage_Passwort(
                     this.objWiegung.objSettings.get_ArbeitsplatzName,
                    /*HideScaleWindow*/ false,
                     this.objWiegung.objSettings.sServicePasswort,
                    /*Terminal ID*/ "",
                    /*ShowServiceMode*/ false) == DialogResult.OK)
                {
                    frmServiceFunktionen f = new frmServiceFunktionen(ref this.objWiegung);
                    f.ShowDialog();
                    f.Dispose();
                    f = null;
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
            }
        }

        public void Starte_Ablauf()
        {

        }    
        private void picKundenLogo_Click(object sender, EventArgs e)
        {
            if (SETTINGS.IS_EntwicklungsPC())
            {
                this.Close();
            }
        }    

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.dispClock.Text = DateTime.Now.ToString("ddd, dd.MM.yyyy HH:mm");
        }
        private void RefreshServiceModeGui()
        {
            SETTINGS.bISEntwicklungsTerminal = this.objWiegung.objSettings.bServiceMode;

            if (SETTINGS.IS_EntwicklungsPC())
            {
                this.dispWelcomeMessage.ForeColor = Color.Red;
                this.dispWelcomeMessage.Text = "Service Mode !!!";
            }
            else
            {
                this.dispWelcomeMessage.ForeColor = Color.Black;
                this.dispWelcomeMessage.Text = "Sie befinden sich im Hauptmenü. \n" +
                                               "Bitte wählen Sie eine Funktion aus.";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!cGlobalScale.objCIND890APIClient.Terminal.WeightWindowStatus)
            {
                
                cGlobalScale.objCIND890APIClient.Terminal.HideWeightWindow = true;
            }

            cGlobalScale.objCIND890APIClient.Terminal.Top = 19;
            cGlobalScale.objCIND890APIClient.Terminal.Left = 669;
            cGlobalScale.objCIND890APIClient.Terminal.Height = 600;
            cGlobalScale.objCIND890APIClient.Terminal.Width = 200;
            cGlobalScale.objCIND890APIClient.Terminal.ApplicationMode = MTTS.IND890.CE.CTerminal.enumDisplayMode.WEIGHT_WINDOW;
        }
    }
}