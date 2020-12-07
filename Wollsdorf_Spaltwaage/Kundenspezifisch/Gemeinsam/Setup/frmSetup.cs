using System;
using System.Drawing;
using System.Windows.Forms;
using MTTS.IND890.CE;
using Wollsdorf.Spaltwaage;
using Wollsdorf_Spaltwaage.Allgemein.ButtonBar;
using Wollsdorf_Spaltwaage.Allgemein.DIO_RS485;
using Wollsdorf_Spaltwaage.Allgemein.Forms;
using Wollsdorf_Spaltwaage.Allgemein.ScaleEngine;
using Wollsdorf_Spaltwaage.Allgemein.SQL;
using Wollsdorf_Spaltwaage.Kundenspezifisch.Gemeinsam.Settings;
using Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Data;

namespace Wollsdorf_Spaltwaage.Kundenspezifisch.Gemeinsam.Setup
{
    internal partial class frmSetup : Form
    {
        private string sInputTag;
        private cWiegung objWiegung;

        public frmSetup(ref cWiegung Wiegung)
        {
            InitializeComponent();

            cGlobalScale.objCIND890APIClient.OnGetKeypadResult += new CIND890APIClient.delegateOnGetKeypadResult(objCIND890APIClient_OnGetKeypadResult);
            this.objWiegung = Wiegung;

            this.cmdDel_SalzWG_Daten.Visible = SETTINGS.IS_EntwicklungsPC();
            this.cmdDel_ÜbernWG_Daten.Visible = SETTINGS.IS_EntwicklungsPC();

        }

        ~frmSetup()
        {
            cGlobalScale.objCIND890APIClient.OnGetKeypadResult -= new CIND890APIClient.delegateOnGetKeypadResult(objCIND890APIClient_OnGetKeypadResult);
        }

        private void Class2Gui() 
        {
            this.tbArbeitsplatz.Text = this.objWiegung.objSettings.sArbeitsplatzname;
            this.tbPasswort.Text = this.objWiegung.objSettings.sServicePasswort;
            this.cbDIOEingänge.Checked = this.objWiegung.objSettings.bDioeingänge;
            this.tbPath.Text = this.objWiegung.objSettings.CSVPath;
            this.tbZeilen.Text = this.objWiegung.objSettings.iZeilenProSeite.ToString();
            this.tbMinGew.Text = this.objWiegung.objSettings.dEinzelWiegungMinGewicht.ToString();
            this.tbMaxGew.Text = this.objWiegung.objSettings.dEinzelWiegungMaxGewicht.ToString();
            this.tbPalettentara.Text = this.objWiegung.objSettings.dPalettenTara.ToString();
            this.tbPalettengesamtGewicht.Text = this.objWiegung.objSettings.dPalettenMax.ToString();
            this.tbKlasse1.Text = this.objWiegung.objSettings.KlassenBezeichnungsListe[0];
            this.tbKlasse2.Text = this.objWiegung.objSettings.KlassenBezeichnungsListe[1];
            this.tbKlasse3.Text = this.objWiegung.objSettings.KlassenBezeichnungsListe[2];
            this.tbKlasse4.Text = this.objWiegung.objSettings.KlassenBezeichnungsListe[3];
            this.tbKlasse5.Text = this.objWiegung.objSettings.KlassenBezeichnungsListe[4];
            this.tbKlasse6.Text = this.objWiegung.objSettings.KlassenBezeichnungsListe[5];
            this.tbArtikelNr1.Text = this.objWiegung.objSettings.ArtikelNrListe[0];
            this.tbArtikelNr2.Text = this.objWiegung.objSettings.ArtikelNrListe[1];
            this.tbArtikelNr3.Text = this.objWiegung.objSettings.ArtikelNrListe[2];
            this.tbArtikelNr4.Text = this.objWiegung.objSettings.ArtikelNrListe[3];
            this.tbArtikelNr5.Text = this.objWiegung.objSettings.ArtikelNrListe[4];
            this.tbArtikelNr6.Text = this.objWiegung.objSettings.ArtikelNrListe[5];
            this.tbGruppenName1.Text = this.objWiegung.objSettings.sGruppenName1;
            this.tbGruppenName2.Text = this.objWiegung.objSettings.sGruppenName2;   
        }
        private void Gui2Class() 
        {
            this.objWiegung.objSettings.sArbeitsplatzname = this.tbArbeitsplatz.Text;
            this.objWiegung.objSettings.sServicePasswort = this.tbPasswort.Text;
            this.objWiegung.objSettings.bDioeingänge = this.cbDIOEingänge.Checked;
            this.objWiegung.objSettings.CSVPath = this.tbPath.Text;
            this.objWiegung.objSettings.dEinzelWiegungMaxGewicht = cGlobalHandling.TextboxToDouble(this.tbMaxGew);
            this.objWiegung.objSettings.dEinzelWiegungMinGewicht = cGlobalHandling.TextboxToDouble(this.tbMinGew);
            this.objWiegung.objSettings.dPalettenTara = cGlobalHandling.TextboxToDouble(this.tbPalettentara);
            this.objWiegung.objSettings.dPalettenMax = cGlobalHandling.TextboxToDouble(this.tbPalettengesamtGewicht);
            this.objWiegung.objSettings.KlassenBezeichnungsListe[0] = this.tbKlasse1.Text;
            this.objWiegung.objSettings.KlassenBezeichnungsListe[1] = this.tbKlasse2.Text;
            this.objWiegung.objSettings.KlassenBezeichnungsListe[2] = this.tbKlasse3.Text;
            this.objWiegung.objSettings.KlassenBezeichnungsListe[3] = this.tbKlasse4.Text;
            this.objWiegung.objSettings.KlassenBezeichnungsListe[4] = this.tbKlasse5.Text;
            this.objWiegung.objSettings.KlassenBezeichnungsListe[5] = this.tbKlasse6.Text;
            this.objWiegung.objSettings.ArtikelNrListe[0] = this.tbArtikelNr1.Text;
            this.objWiegung.objSettings.ArtikelNrListe[1] = this.tbArtikelNr2.Text;
            this.objWiegung.objSettings.ArtikelNrListe[2] = this.tbArtikelNr3.Text;
            this.objWiegung.objSettings.ArtikelNrListe[3] = this.tbArtikelNr4.Text;
            this.objWiegung.objSettings.ArtikelNrListe[4] = this.tbArtikelNr5.Text;
            this.objWiegung.objSettings.ArtikelNrListe[5] = this.tbArtikelNr6.Text;
            this.objWiegung.objSettings.sGruppenName1 = this.tbGruppenName1.Text;
            this.objWiegung.objSettings.sGruppenName2 = this.tbGruppenName2.Text;

            this.objWiegung.objSettings.iZeilenProSeite = cGlobalHandling.TextboxToInt(this.tbZeilen);                       
        }

        private void frmSetup_Load(object sender, EventArgs e)
        {
            cFormStyle.FORM_LOAD(this, null);
            this.dispTopLabelLeft.Text = this.objWiegung.objSettings.get_ArbeitsplatzName;
            this.Init_ButtonBar();
            this.Class2Gui(); 
        }
        private void Init_ButtonBar()
        {
            ctrlButtonBar1.DISP_PAGE(1);
            ctrlButtonBar1.SET_BUTTON_TEXT(1, "Zurück", "§Close");
            ctrlButtonBar1.SET_BUTTON_TEXT(2, "", "§Free1");
            ctrlButtonBar1.SET_BUTTON_TEXT(3, "", "§Free2");
            ctrlButtonBar1.SET_BUTTON_TEXT(4, "Speichern", "§Save");
            ctrlButtonBar1.SET_BUTTON_TEXT(5, "", "§Free3");
            ctrlButtonBar1.SET_BUTTON_TEXT(6, "DIO Test", "§DIOTest");
            ctrlButtonBar1.SET_BUTTON_TEXT(7, "Druckertest",       "§DruckerTestKurz");
            ctrlButtonBar1.SET_BUTTON_TEXT(8, "", "§Free8");

            Icon myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_House;
            ctrlButtonBar1.Button_F1.Bild_Icon = myIcon;

            myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_Ok;
            ctrlButtonBar1.Button_F4.Bild_Icon = myIcon;

            myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_Light_1;
            ctrlButtonBar1.Button_F6.Bild_Icon = myIcon;

            myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_Print;
            ctrlButtonBar1.Button_F7.Bild_Icon = myIcon;

            ctrlButtonBar1.EventButtonClick += new ctrlButtonBar._EventButtonClick(ctrlButtonBar1_EventButtonClick);        
        }

        private void ctrlButtonBar1_EventButtonClick(object sender, string fTaste, int iTastenCode, string fTag)
        {
            try
            {
                switch (fTag.ToUpper())
                {
                    case "§CLOSE":
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        break;
                    case "§SAVE":
                        this.Save_Settings();
                        break;
                    case "§DIOTEST":
                        this.Starte_DIO_Test();
                        break;
                    case "§DRUCKERTESTKURZ":
                        this.Starte_Testdruck_Kurz();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException("ctrlButtonBar1_EventButtonClick4", ex);
            }
        }

        private void Starte_DIO_Test()
        {
            #region Phase 1
            try
            {
                CScaleWeight sw = cGlobalScale.GetDynamic();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DIO Test Phase 1");
            }
            #endregion

            #region Phase 2
            try
            {
                CWeight w = cGlobalScale.objCIND890APIClient.CurrentScale.DisplayWeight;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DIO Test Phase 2");
            }
            #endregion

            #region Phase 3
            try
            {
                CDiscreteIO oDiscreteIO = cGlobalScale.objCIND890APIClient_DigitalIO.DiscreteIO;

                if (oDiscreteIO == null)
                {
                    MessageBox.Show("DIO NULL", "DIO Test Phase 3>>");
                }

                //for (int iLocation = 0; iLocation < 10; iLocation++)
                //{

                //    #region Phase 4
                //    try
                //    {
                //        if (oDiscreteIO != null)
                //        {
                //            oDiscreteIO.WriteToDIO((byte)iLocation, 5, 0);
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message, iLocation.ToString() + ">>> DIO Test Phase 4");
                //    }
                //    #endregion
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DIO Test Phase 3");
            }
            #endregion

            try
            {
                var fdio = new frmDIOTest();
                fdio.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DIO Test Start");
            }
        }

        private void btnPwd_Click(object sender, EventArgs e)
        {
            ShowPasswortInput(((Control)sender).Name.ToUpper());
        }
        private void btnArbeitsplatz_Click(object sender, EventArgs e)
        {
            this.ShowArbeitsplatzInput(((Control)sender).Name.ToUpper());
        }
        private void ShowArbeitsplatzInput(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeAlphaNumericKeypad(this.tbArbeitsplatz.Text, "Arbeitsplatz:");
        }
        private void ShowKlasse1Input(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeAlphaNumericKeypad(this.tbKlasse1.Text, "Name der 1.Klasse:");
        }
        private void ShowKlasse2Input(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeAlphaNumericKeypad(this.tbKlasse2.Text, "Name der 2.Klasse:");
        }
        private void ShowKlasse3Input(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeAlphaNumericKeypad(this.tbKlasse3.Text, "Name der 3.Klasse:");
        }
        private void ShowKlasse4Input(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeAlphaNumericKeypad(this.tbKlasse4.Text, "Name der 4.Klasse:");
        }
        private void ShowKlasse5Input(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeAlphaNumericKeypad(this.tbKlasse5.Text, "Name der 5.Klasse:");
        }
        private void ShowKlasse6Input(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeAlphaNumericKeypad(this.tbKlasse6.Text, "Name der 6.Klasse:");
        }

        private void ShowGruppenName1Input(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeAlphaNumericKeypad(this.tbGruppenName1.Text, "Gruppenname eingeben:");
        }
        private void ShowGruppenName2Input(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeAlphaNumericKeypad(this.tbGruppenName2.Text, "Gruppenname eingeben:");
        }


        //ArtikelNr Input
        private void ShowArtikelNr1Input(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeNumericKeypad(CIND890APIClient.enumKeypadType.NUMERIC, this.tbArtikelNr1.Text, "Artikelnummer der 1.Klasse:");
        }
        private void ShowArtikelNr2Input(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeNumericKeypad(CIND890APIClient.enumKeypadType.NUMERIC, this.tbArtikelNr2.Text, "Artikelnummer der 2.Klasse:");
        }
        private void ShowArtikelNr3Input(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeNumericKeypad(CIND890APIClient.enumKeypadType.NUMERIC, this.tbArtikelNr3.Text, "Artikelnummer der 3.Klasse:");
        }
        private void ShowArtikelNr4Input(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeNumericKeypad(CIND890APIClient.enumKeypadType.NUMERIC, this.tbArtikelNr4.Text, "Artikelnummer der 4.Klasse:");
        }
        private void ShowArtikelNr5Input(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeNumericKeypad(CIND890APIClient.enumKeypadType.NUMERIC, this.tbArtikelNr5.Text, "Artikelnummer der 5.Klasse:");
        }
        private void ShowArtikelNr6Input(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeNumericKeypad(CIND890APIClient.enumKeypadType.NUMERIC, this.tbArtikelNr6.Text, "Artikelnummer der 6.Klasse:");
        }

        private void ShowPalettengesamtGewichtInput(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeNumericKeypad(CIND890APIClient.enumKeypadType.DECIMAL, this.tbPalettengesamtGewicht.Text, "Palettengesamtgewicht eingeben:");
        }

        private void ShowPathInput(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeAlphaNumericKeypad(this.tbPath.Text, "Pfad Dateiexport:");
        }
        private void ShowMinGewInput(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeNumericKeypad(MTTS.IND890.CE.CIND890APIClient.enumKeypadType.DECIMAL, this.tbMinGew.Text, "Gewicht eingeben:");
        }
        private void ShowMaxGewInput(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeNumericKeypad(MTTS.IND890.CE.CIND890APIClient.enumKeypadType.DECIMAL, this.tbMaxGew.Text, "Gewicht eingeben:");
        }
        private void ShowPalettenTaraInput(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeNumericKeypad(MTTS.IND890.CE.CIND890APIClient.enumKeypadType.DECIMAL, this.tbPalettentara.Text, "Tara eingeben:");
        }
        private void ShowZeilenProSeiteInput(string Tag) 
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeNumericKeypad(MTTS.IND890.CE.CIND890APIClient.enumKeypadType.NUMERIC, this.tbZeilen.Text, "Anzahl Zeilen eingeben:");
        }

        private void ShowPasswortInput(string Tag)
        {
            this.sInputTag = Tag;
            cGlobalScale.objCIND890APIClient.InvokeAlphaNumericKeypad(tbPasswort.Text, "Passwort eingeben:", /*Passwchar*/ true);
        }

        void objCIND890APIClient_OnGetKeypadResult(byte returnCode, string value)
        {
            if (returnCode != 1)
            {
                return;
            }

            if (string.IsNullOrEmpty(sInputTag))
            {
                return;
            }

            switch (this.sInputTag.ToUpper())
            {
                case "CMDPASSWORT":
                    this.SetText(this.tbPasswort, value);
                    break;
                case "CMDARBEITSPLATZ":
                    this.SetText(this.tbArbeitsplatz, value);
                    break;
                case "CMDMINGEW":
                    this.SetText(this.tbMinGew, value);
                    break;
                case "CMDMAXGEW":
                    this.SetText(this.tbMaxGew, value);
                    break;
                case "CMDPALETTENTARA":
                    this.SetText(this.tbPalettentara, value);
                    break;
                case "CMDPALETTENGESAMTGEWICHT":
                    this.SetText(this.tbPalettengesamtGewicht, value);
                    break;
                case "CMDZEILEN":
                    this.SetText(this.tbZeilen, value);
                    break;
                case "CMDPATH":
                    this.SetText(this.tbZeilen, value);
                    break;
                case "CMDKLASSE1":
                    this.SetText(this.tbKlasse1, value);
                    break;
                case "CMDKLASSE2":
                    this.SetText(this.tbKlasse2, value);
                    break;
                case "CMDKLASSE3":
                    this.SetText(this.tbKlasse3, value);
                    break;
                case "CMDKLASSE4":
                    this.SetText(this.tbKlasse4, value);
                    break;
                case "CMDKLASSE5":
                    this.SetText(this.tbKlasse5, value);
                    break;
                case "CMDKLASSE6":
                    this.SetText(this.tbKlasse6, value);
                    break;

                case "CMDGRUPPENNAME1":
                    this.SetText(this.tbGruppenName1, value);
                    break;
                case "CMDGRUPPENNAME2":
                    this.SetText(this.tbGruppenName2, value);
                    break;

                case "CMDARTIKELNR1":
                    this.SetText(this.tbArtikelNr1, value);
                    break;
                case "CMDARTIKELNR2":
                    this.SetText(this.tbArtikelNr2, value);
                    break;
                case "CMDARTIKELNR3":
                    this.SetText(this.tbArtikelNr3, value);
                    break;
                case "CMDARTIKELNR4":
                    this.SetText(this.tbArtikelNr4, value);
                    break;
                case "CMDARTIKELNR5":
                    this.SetText(this.tbArtikelNr5, value);
                    break;
                case "CMDARTIKELNR6":
                    this.SetText(this.tbArtikelNr6, value);
                    break;

                default:
                    MessageBox.Show("Ungültiger Button in KeyPadResult" + this.sInputTag);
                    break;
            }
        }

        #region Invoke required
        delegate void SetTextDelegate(Control senderControl, string strText);
        private void SetText(Control sender, string str)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    SetTextDelegate SetTextEvent = new SetTextDelegate(SetText);
                    this.Invoke(SetTextEvent, new object[] { sender, str });
                }
                else
                {
                    sender.Text = str;
                }
            }
            catch { }
        }
        #endregion Invoke required

        private void cmdPalettenTara_Click(object sender, EventArgs e)
        {
            ShowPalettenTaraInput(((Control)sender).Name.ToUpper());
        }

        private void cmdMinGew_Click(object sender, EventArgs e)
        {
            ShowMinGewInput(((Control)sender).Name.ToUpper());
        }
        private void cmdPath_Click(object sender, EventArgs e)
        {            
            ShowPathInput(((Control)sender).Name.ToUpper());
        }
        private void Save_Settings()
        {
            this.Gui2Class();

            cData_Settings_Handling.Save_Settings(this.objWiegung.objSettings);
            this.DialogResult = DialogResult.OK;
        }

        private void Starte_Testdruck_Kurz()
        {
            DialogResult dialogResult = MessageBox.Show(
               "Dieser Test druckt eine Seiten mit einigen Zeilen. Wirlich starten ?",
               "Epson Drucker Testdruck", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                cDruck_DruckerTest prn = new cDruck_DruckerTest();
                bool bRet = prn.Starte_DruckerTest();

                MessageBox.Show(
                    "Der Ausdruck wurde mit folgenden Ergebnis gesendet: " + bRet.ToString(), "Druck beendet");
            }
        }

        private void cmdZeilen_Click(object sender, EventArgs e)
        {
            ShowZeilenProSeiteInput(((Control)sender).Name.ToUpper());
        }

        private void cmdOpenPath_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", this.tbPath.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: \r\n" + ex.Message, "Dateifehler 11");
                throw;
            }
        }

        private void cmdTest_Click(object sender, EventArgs e)
        {
            try
            {
                string sPath = this.tbPath.Text.Trim() + @"\IND890Test.txt";

                System.IO.StreamWriter sw = new
                        System.IO.StreamWriter(sPath, false /*Append*/ , System.Text.Encoding.GetEncoding("windows-1252"));
                sw.WriteLine("Hallo " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));

                sw.Close();

                MessageBox.Show("Datei IND890Test.txt wurde erzeugt", "IND890 Testdatei");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: \r\n" + ex.Message, "IND890 Dateifehler");
                throw;
            }
        }

        private void cmdDel_SalzWG_Daten_Click(object sender, EventArgs e)
        {
             DialogResult dialogResult = MessageBox.Show(
                "Wirlich löschen ?",
                "Service Mode", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
             if (dialogResult == DialogResult.Yes)
             {
                cDB_SQL_CE qry = null;
                try
                {
                    string sSQL = "DELETE FROM [SMT_SALZ] ";
                    qry = new cDB_SQL_CE(cDB_Settings.CE_ConnectionString);
                    qry.Exec(sSQL);                               
                }
                catch (Exception ex)
                {
                    SiAuto.LogException(ex);
                    throw;
                }
                finally
                {
                    if (qry != null)
                    {
                        qry.FREE();
                    }
                }
            }
        }

        private void cmdDel_ÜbernWG_Daten_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
               "Wirlich löschen ?",
               "Service Mode", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                cDB_SQL_CE qry = null;
                try
                {
                    string sSQL = "DELETE FROM [SMT_WAAGE] ";
                    qry = new cDB_SQL_CE(cDB_Settings.CE_ConnectionString);
                    qry.Exec(sSQL);
                }
                catch (Exception ex)
                {
                    SiAuto.LogException(ex);
                    throw;
                }
                finally
                {
                    if (qry != null)
                    {
                        qry.FREE();
                    }
                }
            }
        }

        private void cmdMaxGew_Click(object sender, EventArgs e)
        {
            ShowMaxGewInput(((Control)sender).Name.ToUpper());
        }

        private void cmdKlasse1_Click(object sender, EventArgs e)
        {
            ShowKlasse1Input(((Control)sender).Name.ToUpper());
        }

        private void cmdKlasse2_Click(object sender, EventArgs e)
        {
            ShowKlasse2Input(((Control)sender).Name.ToUpper());
        }

        private void cmdKlasse3_Click(object sender, EventArgs e)
        {
            ShowKlasse3Input(((Control)sender).Name.ToUpper());
        }

        private void cmdKlasse4_Click(object sender, EventArgs e)
        {
            ShowKlasse4Input(((Control)sender).Name.ToUpper());
        }

        private void cmdKlasse5_Click(object sender, EventArgs e)
        {
            ShowKlasse5Input(((Control)sender).Name.ToUpper());
        }

        private void cmdKlasse6_Click(object sender, EventArgs e)
        {
            ShowKlasse6Input(((Control)sender).Name.ToUpper());
        }


        //ArtikelNr Eingabe
        private void cmdArtikelNr1_Click(object sender, EventArgs e)
        {
            ShowArtikelNr1Input(((Control)sender).Name.ToUpper());
        }

        private void cmdArtikelNr2_Click(object sender, EventArgs e)
        {
            ShowArtikelNr2Input(((Control)sender).Name.ToUpper());
        }

        private void cmdArtikelNr3_Click(object sender, EventArgs e)
        {
            ShowArtikelNr3Input(((Control)sender).Name.ToUpper());
        }

        private void cmdArtikelNr4_Click(object sender, EventArgs e)
        {
            ShowArtikelNr4Input(((Control)sender).Name.ToUpper());
        }

        private void cmdArtikelNr5_Click(object sender, EventArgs e)
        {
            ShowArtikelNr5Input(((Control)sender).Name.ToUpper());
        }

        private void cmdArtikelNr6_Click(object sender, EventArgs e)
        {
            ShowArtikelNr6Input(((Control)sender).Name.ToUpper());
        }

        private void cmdPalettengesamtGewicht_Click(object sender, EventArgs e)
        {
            ShowPalettengesamtGewichtInput(((Control)sender).Name.ToUpper());
        }

        private void cmdGruppenName1_Click(object sender, EventArgs e)
        {
            ShowGruppenName1Input(((Control)sender).Name.ToUpper());
        }

        private void cmdGruppenName2_Click(object sender, EventArgs e)
        {
            ShowGruppenName2Input(((Control)sender).Name.ToUpper());
        }
    }
}