using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Windows.Forms;
using Wollsdorf.Spaltwaage;
using Wollsdorf_Spaltwaage.Allgemein.ButtonBar;
using Wollsdorf_Spaltwaage.Allgemein.Forms;
using Wollsdorf_Spaltwaage.Allgemein.SQL;
using Wollsdorf_Spaltwaage.Kundenspezifisch.Gemeinsam.Settings;
using Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Data;

namespace Wollsdorf_Spaltwaage.Kundenspezifisch.Gemeinsam.Servicefunktionen
{
    internal partial class frmServiceFunktionen : Form
    {
        private cWiegung objWiegung;

        public frmServiceFunktionen(ref cWiegung Wiegung)
        {
            InitializeComponent();
            this.objWiegung = Wiegung;
        }

        private void frmServiceFunktionen_Load(object sender, EventArgs e)
        {
            cFormStyle.FORM_LOAD(this, null);
            this.dispTopLabelLeft.Text = this.objWiegung.objSettings.get_ArbeitsplatzName;
            this.Init_ButtonBar();
        }
        private void Init_ButtonBar()
        {
            ctrlButtonBar1.DISP_PAGE(1);
            ctrlButtonBar1.SET_BUTTON_TEXT(1, "Zurück", "§Close");
            ctrlButtonBar1.SET_BUTTON_TEXT(2, "", "§Free1");
            ctrlButtonBar1.SET_BUTTON_TEXT(3, "", "§Free2");
            ctrlButtonBar1.SET_BUTTON_TEXT(4, "Weiter", "§Save");
            ctrlButtonBar1.SET_BUTTON_TEXT(5, "", "§Free3");
            ctrlButtonBar1.SET_BUTTON_TEXT(6, "", "§Free4");
            ctrlButtonBar1.SET_BUTTON_TEXT(7, "", "§Free5");
            ctrlButtonBar1.SET_BUTTON_TEXT(8, "", "§Free6");

            Icon myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_House;
            ctrlButtonBar1.Button_F1.Bild_Icon = myIcon;

            myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_Ok;
            ctrlButtonBar1.Button_F4.Bild_Icon = myIcon;

            ctrlButtonBar1.EventButtonClick += new ctrlButtonBar._EventButtonClick(ctrlButtonBar1_EventButtonClick);

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
                    case "§SAVE":
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException("ctrlButtonBar1_EventButtonClick3", ex);
            }
        }

        private void cmdSQLreader_Click(object sender, EventArgs e)
        {

            System.Data.SqlClient.SqlConnection myConnection = new System.Data.SqlClient.SqlConnection(cDB_Settings.ConnectionString_EXTERN);
            try
            {
                myConnection.Open();
                try
                {
                    System.Data.SqlClient.SqlDataReader myReader = null;
                    System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand("select Mitarbeiter_Name from SMT_Mitarbeiter where Mitarbeiter_ID='GT'",
                                                             myConnection);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        tbSQLergebnisbox.Text = myReader["Mitarbeiter_Name"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("Lese Fehler");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Connection Fehler");
            }
            finally
            {
                if (myConnection.State != ConnectionState.Closed)
                {
                    myConnection.Close();
                    myConnection = null;
                }
            }

        }
        private void cmdSQLinserter_Click(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlConnection myConnection = new 
            System.Data.SqlClient.SqlConnection(cDB_Settings.ConnectionString_EXTERN);

            try
            {
                myConnection.Open();
                try
                {
                    System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand("INSERT INTO SMT_TEST (Test_Text, Test_Nummer) " +
                                     "Values ('" + addTimestamp("Test") + "'," + addTimestamp("123.45") + ")", myConnection);
                    tbSQLergebnisbox.Text = addTimestamp("Test") + "," + addTimestamp("123.45");
                    myCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("Schreib Fehler");
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Fehler: \r\n" + ex.Message + "\r\nFehlercode: " + ex.Number.ToString() , "Datenbankfehler 13");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Fehler: \r\n" + ex.Message, "Datenbankfehler 14");
            }
            finally
            {
                if (myConnection.State != ConnectionState.Closed)
                {
                    myConnection.Close();
                    myConnection = null;
                }
            }
        }

        private string addTimestamp(string text)
        {
            text += DateTime.Now.ToString("ddMMyyyyHHmmss");
            return text;
        }

        private void cmdSQLreaderIND_Click(object sender, EventArgs e)
        {
            string sConn = string.Format("DataSource={0}", cDB_Settings.CE_ConnectionString);

            System.Data.SqlServerCe.SqlCeConnection myConnection =
                new System.Data.SqlServerCe.SqlCeConnection(sConn);
            try
            {
                myConnection.Open();
                try
                {
                    SqlCeDataReader myReader = null;
                    SqlCeCommand myCommand = new SqlCeCommand("select TOP(1) Test_Text from SMT_Test order by aic_SMT_TEST DESC",
                                                             myConnection);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        tbSQLergebnisbox.Text = myReader["Test_Text"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (myConnection.State != ConnectionState.Closed)
                {
                    myConnection.Close();
                    myConnection = null;
                }
            }

        }

        private void cmdSQLinserterIND_Click(object sender, EventArgs e)
        {
            string sConn = string.Format("DataSource={0}", cDB_Settings.CE_ConnectionString);

            System.Data.SqlServerCe.SqlCeConnection myConnection = new
                System.Data.SqlServerCe.SqlCeConnection(sConn);

            try
            {
                myConnection.Open();
                try
                {


                    SqlCeCommand myCommand = new SqlCeCommand("INSERT INTO SMT_TEST (Test_Datum,Test_Text, Test_Nummer) " +
                                     "Values (" + cDB_SQL_CE.DATE_TIME_TO_DB(DateTime.Now) + ",'" + addTimestamp("Test") + "'," + addTimestamp("123.45") + ")", myConnection);
                    tbSQLergebnisbox.Text = cDB_SQL_CE.DATE_TIME_TO_DB(DateTime.Now) + "," + addTimestamp("Test") + "," + addTimestamp("123.45");
                    myCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (myConnection.State != ConnectionState.Closed)
                {
                    myConnection.Close();
                    myConnection = null;
                }
            }
        }

        private void cmdTextPrinter_Kopf_Click(object sender, EventArgs e)
        {
            //this.Start_Refresh_Testdaten_Übernahmewaage();
            
            //bool bret = this.objPrn.drucke_Kopf();

            //foreach (cData_Wiegung_Item wi in this.objWiegung.objWiegePositionsList)
            //{
            //    cData_Wiegung_Item wTmp = wi;
            //    bret = objPrn.drucke_WiegePosition(ref wTmp);
            //}

            //bret = objPrn.drucke_ZwischenSum();

            //MessageBox.Show("Ergebnis = " + bret.ToString());
        }

        /*private void cmdTextPrinter_Wiegezeile_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            Wollsdorf.Data.cData_Wiegung_Item WiegePosition = new Wollsdorf.Data.cData_Wiegung_Item();
            WiegePosition.dNetto = 107.10;
            WiegePosition.dtWiegung = DateTime.Now;
            WiegePosition.iFortlaufendeNummer = 1;
            WiegePosition.iWiegeKlasse = 2;
            WiegePosition.objTierArt.eTierArt = eTierArt.rinder;

            bool bret = false;
            for (int i = 1; i < 50; i++)
            {
                WiegePosition.iFortlaufendeNummer = i;
                WiegePosition.dNetto = rand.Next(10, 500);
                WiegePosition.iWiegeKlasse = rand.Next(1, 3);

                bret = objPrn.drucke_WiegePosition(ref WiegePosition);

                if (!bret)
                {
                    break;
                }

            }

            MessageBox.Show("Ergebnis = " + bret.ToString());
        }
        */
        private void cmdTextPrinter_Abschluss_Click(object sender, EventArgs e)
        {
            
        }
        /*private void cmdTextPrinter_Storno_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            Wollsdorf.Data.cData_Wiegung_Item Wiegung = new Wollsdorf.Data.cData_Wiegung_Item();
            Wiegung.bISStorno = true;
            Wiegung.dNetto = 10.10;
            Wiegung.dtWiegung = DateTime.Now;
            Wiegung.iFortlaufendeNummer = 1;
            Wiegung.iWiegeKlasse = 2;
            Wiegung.objTierArt.eTierArt = eTierArt.rinder;

            bool bret = false;
            bret = objPrn.drucke_WiegePosition(ref Wiegung);
            MessageBox.Show("Ergebnis = " + bret.ToString());
        }
        */
        /*private void cmdTextPrinter_Zwischensumme_Click(object sender, EventArgs e)
        {
            this.Start_Refresh_Testdaten_Übernahmewaage();

            bool bret = false;
            bret = objPrn.drucke_ZwischenSum();
            MessageBox.Show("Ergebnis = " + bret.ToString());           
        }
         */
        private void cmdCSVTest_Click(object sender, EventArgs e)
        {
            try
            {
                string sPath = @"\\HP5\temp\Musterdaten.txt";
                System.IO.StreamWriter sw = new
                        System.IO.StreamWriter(sPath, false /*Append*/ , System.Text.Encoding.GetEncoding("windows-1252"));
                sw.WriteLine("Hallo " + DateTime.Now.ToString("dd.mm.yyyy ss:mm:hh"));

                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: \r\n" + ex.Message, "Dateifehler 11");
                throw;
            }
        }
        private void cmdCSVcreate_Click(object sender, EventArgs e)
        {
           // cWiegung wg = new cWiegung();

           // cData_Wiegung_Item wi = new cData_Wiegung_Item();
           // wi.dNetto = 123.45;
           // //wi.objTierArt.eTierArt = eTierArt.ochsen;
           // //wi.objMerkmalStatus.Schnitte = eMerkmalStufe.Stufe2;
           // //wi.objMerkmalStatus.Löcher = eMerkmalStufe.Stufe1;
           // //wi.objMerkmalStatus.Fäulnis = eMerkmalStufe.none;
           // //wi.objMerkmalStatus.SW = eMerkmalStufe.Stufe1;
           // //wi.objMerkmalStatus.Dung = eMerkmalStufe.Stufe1;
           // //wi.objMerkmalStatus.Eis = eMerkmalStufe.Stufe2;
           // //wi.objMerkmalStatus.Überlagert = eMerkmalStufe.none;
           // //wi.objMerkmalStatus.Haarlässig = eMerkmalStufe.Stufe1;
           // //wi.objMerkmalStatus.Fett = eMerkmalStufe.none;
           // //wi.objMerkmalStatus.Nass = eMerkmalStufe.none;
           // //wi.objMerkmalStatus.Lebendschäden = eMerkmalStufe.Stufe1;
            

           //cDatenexport dx = new cDatenexport(ref wg);
           //dx.Starte_ÜW_CSV_DatenExport("x", wi);
        }

        private void cmdIO_Input1_Click(object sender, EventArgs e)
        {
           
        }
        private void cmdInitSettings_Click(object sender, EventArgs e)
        {
            cmdInitSettings.Enabled = false;
            cmdInitSettings.Text = "*";
            cData_Settings_Handling.Settings_Init();
            MessageBox.Show("Fertig");
        }

        private void Start_Refresh_Testdaten_Übernahmewaage()
        {
            this.objWiegung.iLieferantennummer = 1313;
            this.objWiegung.lBestellnummer = 1234;

            //this.objWiegung.objWiegePositionsList.Clear();
            //this.objWiegung.objWiegePositionsList = this.Übernahmewaage_GeneriereTestDaten();
        }

       

        private void cmdDruckeTestSeite_Click(object sender, EventArgs e)
        {
            cDruck_SeitenLangeTest prn = new cDruck_SeitenLangeTest();
            bool bret = prn.Starte_SeitenLängenTest();
            MessageBox.Show("Ergebnis = " + bret.ToString());
        }

        
        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = global::Allgemein.cGlobalNummerkreis.Nummernkreis1_GetCurrent();

            i = global::Allgemein.cGlobalNummerkreis.Nummernkreis1_GetNext();

            global::Allgemein.cGlobalNummerkreis.Nummernkreis1_SetNext();

            i = global::Allgemein.cGlobalNummerkreis.Nummernkreis1_GetCurrent();
        }

        private void cmdKill_SMT_Waage_Click(object sender, EventArgs e)
        {
            cDB_SQL_CE qry = null;

            try
            {
                    string sSQL = "Delete from [SMT_WAAGE]";

                    try
                    {
                        qry = new cDB_SQL_CE(cDB_Settings.CE_ConnectionString);
                        qry.Exec(sSQL);
                    }
                    catch (Exception ex)
                    {
                        SiAuto.LogException(ex);
                    }
                    finally
                    {
                        if (qry != null)
                        {
                            qry.FREE();
                        }
                    }
            }
            catch (Exception ex)
            {
                SiAuto.LogException("SummenGruppeLöschen", ex);
                throw;
            }
        }       
    }
}