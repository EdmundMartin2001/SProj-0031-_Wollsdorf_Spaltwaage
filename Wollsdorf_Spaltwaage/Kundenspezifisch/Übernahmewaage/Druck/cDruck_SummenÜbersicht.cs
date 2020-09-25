namespace Wollsdorf.Spaltwaage
{
    using System;
    using System.Collections.Generic;
    using Allgemein;
    using System.Text;

    internal class cDruck_Summenliste
    {
        private cWiegung objWiegung;
        private List<string> lstDruckerBuffer;
        private int iCountLines;
        private int iCountPage;
        private int iMaxLinesPerPage = 66;
        private char[] cEpson_FormFeed = { '\x0C' };

        public cDruck_Summenliste(ref cWiegung Wiegung) 
        {
            this.objWiegung = Wiegung;
            this.lstDruckerBuffer = new List<string>() { };
            this.iCountLines = 0;
            this.iCountPage = 1;
        }

        ~cDruck_Summenliste()
        {

        }

        public bool drucke_Kopf() 
        {
            bool bRet = false;
            try
            {
                bRet = this.drucke_Kopf_Go();
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
                throw;
            }
            return bRet;
        }
        private bool drucke_Kopf_Go()
        {
            bool bRet = false;
            try
            {
                // Überschrift
                this.PrintLine("Wollsdorf Leder Schmidt - Summenprotokoll" + "\x0d\x0a");
                this.PrintLine("-----------------------------------------" + "\x0d\x0a");
                this.PrintLine("\x0d\x0a");
                this.PrintLine("Datum:      " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "\x0d\x0a");
                this.PrintLine("\x0d\x0a");

                int iNummer = 0;
                string sDummy = "";

                // Klassenüberschrift
                string sTemp = "";

                this.PrintLine(this.drucke_WiegeHeader(this.iCountPage));
                this.PrintLine(("".PadLeft(80, '-') + "\x0d\x0a"));

                #region Drucke
                iNummer = 0;
                foreach (string sSendTxt in this.lstDruckerBuffer)
                {
                    iNummer++;

                    if (iNummer >= 3)
                    {
                        System.Threading.Thread.Sleep(300);
                        iNummer = 0;
                    }

                    if (this.SendTORS232(sSendTxt))
                    {
                        // Seitenwechsel
                        this.iCountPage++;

                        cGlobalScale.objRS232_X5.SendString(new string(this.cEpson_FormFeed));
                        System.Threading.Thread.Sleep(300);
                        this.SendTORS232(this.drucke_WiegeHeader(this.iCountPage));
                        System.Threading.Thread.Sleep(300);
                        this.SendTORS232(("".PadLeft(80, '-') + "\x0d\x0a"));
                    }
                }
                #endregion

                this.lstDruckerBuffer.Clear();
                bRet = true;
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
                throw;
            }
            return bRet;
        }

        public bool drucke_Fußzeile()
        {
            bool bRet = false;
            try
            {
                bRet = this.drucke_Fußzeile_Go();
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
                throw;
            }
            return bRet;
        }
        private bool drucke_Fußzeile_Go()
        {
            bool bRet = false;
            try
            {               
                int iNummer = 0;
                
                this.PrintLine(("".PadLeft(80, '-') + "\x0d\x0a"));   
                this.PrintLine("*Listenende" + "\x0d\x0a");

                #region Drucke
                iNummer = 0;
                foreach (string sSendTxt in this.lstDruckerBuffer)
                {
                    iNummer++;

                    if (iNummer >= 3)
                    {
                        System.Threading.Thread.Sleep(300);
                        iNummer = 0;
                    }

                    if (this.SendTORS232(sSendTxt))
                    {
                        // Seitenwechsel
                        this.iCountPage++;

                        cGlobalScale.objRS232_X5.SendString(new string(this.cEpson_FormFeed));
                        System.Threading.Thread.Sleep(300);
                        this.SendTORS232(this.drucke_WiegeHeader(this.iCountPage));
                        System.Threading.Thread.Sleep(300);
                        this.SendTORS232(("".PadLeft(80, '-') + "\x0d\x0a"));
                    }
                }
                #endregion

                this.lstDruckerBuffer.Clear();
                bRet = true;
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
                throw;
            }
            return bRet;
        }

        //public bool drucke_ZwischenSum()
        //{
        //    bool bRet = false;
        //    try
        //    {
        //        bRet = this.drucke_ZwischenSum_Go();
        //    }
        //    catch (Exception ex)
        //    {
        //        SiAuto.LogException(ex);
        //        throw;
        //    }
        //    return bRet;
        //}
        //private bool drucke_ZwischenSum_Go()
        //{
        //    bool bRet = false;
        //    int iHeaderPrintedPos = -1;
        //    try
        //    {
        //        // Überschrift
        //        this.PrintLine("Summierung" + "\x0d\x0a");
        //        this.PrintLine("-----------------------------------------" + "\x0d\x0a");
        //        this.PrintLine("Anzahl Einzelwiegung: " + this.objWiegung.objWiegePositionsList.Count.ToString() + "\x0d\x0a");
        //        this.PrintLine("\x0d\x0a");
        //        this.PrintLine("Bulle ->Gesamt: " + this.FormatGewicht(objWiegung.objSUM.dGesamtSumme_Bullen) + "\x0d\x0a");
        //        this.PrintLine("Kuh   ->Gesamt: " + this.FormatGewicht(objWiegung.objSUM.dGesamtSumme_Kühe) + "\x0d\x0a");
        //        this.PrintLine("Rind  ->Gesamt: " + this.FormatGewicht(objWiegung.objSUM.dGesamtSumme_Rinder) + "\x0d\x0a");
        //        this.PrintLine("Ochse ->Gesamt: " + this.FormatGewicht(objWiegung.objSUM.dGesamtSumme_Ochsen) + "\x0d\x0a");
        //        this.PrintLine("\x0d\x0a");

        //        int iNummer = 0;
        //        string sDummy = "";

        //        // Klassenüberschrift
        //        string sTemp = "";
        //        sTemp = "Nr|Bezeichnung       |Tier  |Anzahl|  MW (kg)| Gew (kg)";
        //        this.PrintLine(sTemp + "\x0d\x0a");
        //        this.PrintLine(("".PadLeft(80, '-') + "\x0d\x0a"));

        //        // Markiere Druckzeile damit Header nicht vor dieser Zeile bei einem Seitenwechsel gedruckt wird
        //        iHeaderPrintedPos = this.lstDruckerBuffer.Count;

        //        foreach (cData_KlassenStamm_Item kl in this.objWiegung.objKlassenStammList)
        //        {
        //            iNummer++;

        //            sDummy = iNummer.ToString("#0").PadLeft(2, ' ') +
        //                "|" +
        //                kl.sBezeichnung.PadRight(18, ' ') + "|" +
        //                ENUM_HELPER.Get_TierArten_Text(kl.TierArt).PadRight(6, ' ') + "|" +
        //                kl.iSummierung_Anzahl.ToString().PadLeft(6, ' ') + "|" +
        //                this.FormatGewicht(kl.dSummierung_Gewicht) + "|" +
        //                this.FormatGewicht(kl.Get_Summierung_Mittelwert) + "";

        //            this.PrintLine(sDummy + "\x0d\x0a");
        //        }

        //        this.PrintLine("\x0d\x0a");

        //        iNummer = 0;
        //        foreach (string sSendTxt in this.lstDruckerBuffer)
        //        {
        //            iNummer++;

        //            if (iNummer >= 3)
        //            {
        //                System.Threading.Thread.Sleep(300);
        //                iNummer = 0;
        //            }

        //            if (this.SendTORS232(sSendTxt))
        //            {
        //                // Seitenwechsel
        //                this.iCountPage++;

        //                cGlobalScale.objRS232_X5.SendString(new string(this.cEpson_FormFeed));
        //                System.Threading.Thread.Sleep(300);
                        
        //                // Erst erneut Header drucken, wenn dieser schon vorher einmal gedruckt wurde
        //                if ( iNummer >= iHeaderPrintedPos)
        //                {
        //                    this.SendTORS232(this.drucke_ZwischenSumHeader(this.iCountPage));
        //                    System.Threading.Thread.Sleep(300);
        //                    this.SendTORS232(("".PadLeft(80, '-') + "\x0d\x0a"));
        //                }
        //            }
        //        }
        //        this.lstDruckerBuffer.Clear();

        //        bRet = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        SiAuto.LogException(ex);
        //        throw;
        //    }
        //    return bRet;
        //}

        private string drucke_ZwischenSumHeader(int iSeite)
        {
            string sTemp = "";

            sTemp = ("Seite / Page " + iSeite.ToString() + "\x0d\x0a");
            sTemp += ("Nr|Bezeichnung       |Tier  |Anzahl|  MW (kg)| Gew (kg)\x0d\x0a");

            return sTemp;
        }
        private string drucke_WiegeHeader(int iSeite) 
        {
            string stemp = "";

            stemp = ("Seite / Page " + iSeite.ToString() + "\x0d\x0a");
            stemp += ("Tier......|Klasse..............|Anzahl|Gewicht(kg)\x0d\x0a");
            
            return stemp;
        }

        public bool drucke_Position()
        {
            bool bRet = false;
            try
            {

            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
                throw;
            }
            return bRet;
        }
        private bool drucke_Position_go()
        {
            bool bRet = false;
            string sSendTxt = "";

            //sSendTxt = ENUM_HELPER.TierArt_EnumToDisplay(Position.objKLASSE.TierArt).PadRight(10, ' ') + "|" +
            //           this.FormatPadCut_MaxLen(Position.objKLASSE.sBezeichnung, 20) + "|" +
            //           Position.iGesamtAnzahl.ToString().PadRight(6, ' ') + "|" +
            //           this.FormatStornoNetto(Position.dSummeNetto, false) + "\x0d\x0a";

            // Einzeldruck einer Wiegeposition
            if (this.SendTORS232(sSendTxt))
            {
                // Seitenwechsel
                this.iCountPage++;

                cGlobalScale.objRS232_X5.SendString(new string(this.cEpson_FormFeed));
                System.Threading.Thread.Sleep(300);
                this.SendTORS232(this.drucke_WiegeHeader(this.iCountPage));
                System.Threading.Thread.Sleep(300);
                this.SendTORS232(("".PadLeft(80, '-') + "\x0d\x0a"));                
            }
            this.lstDruckerBuffer.Clear();
            System.Threading.Thread.Sleep(300);
            bRet = true;

            return bRet;
        }

        private string FormatStornoNetto(double dNetto, bool bStorno) 
        {
            if (bStorno)
            {
                dNetto = dNetto - dNetto - dNetto;
                dNetto.ToString("##0.00").PadLeft(9, ' ');
            }

            return dNetto.ToString("##0.00").PadLeft(9, ' '); 
        }
        private string FormatGewicht(double dNetto)
        {            
            return dNetto.ToString("###0.00").PadLeft(9, ' ');
        }
        private string FormatPadCut_MaxLen(string s, int iMaxLen)
        {
            if (s.Length > iMaxLen)
            {
                return s.Substring(0, iMaxLen);
            }
            else
            {
                return s.PadRight(iMaxLen, ' ');
            }

        }

      
        //private void MittelWertFormatHelper(eTierArt eKlasse, double dSum, double dMW, int iAnz)
        //{
        //    string sDummy = "";
        //    if( iAnz > 0)
        //    {
        //        sDummy = "kkkkkkkkkkkk".PadRight(13, ' ') + " " +
        //            ENUM_HELPER.Get_TierArten_Text(eKlasse).PadRight(6, ' ') +
        //            " MW:"+dMW.ToString("##0.00").PadLeft(7,' ')+" kg, STK:"+ iAnz.ToString().PadLeft(3,' ') +
        //            " ,Sum: "+dSum.ToString().PadLeft(7,' ')+" kg";
        //    }
		 
    //        /* Name */
    //        //strcat(PrintLine , 	" - " );
    //        StringFillUp(Druck_Klasse_Bezeichnung , 12, FORMAT_LEFT, ' ');
    //        strcpy(PrintLine , 	Druck_Klasse_Bezeichnung );

    //        ret = WriteString( TDF_DB_TRANSFER_ENUM, PrintLine , 0);
    //        Delay(100);

    //        /* Tierart */
    //        strcpy(PrintLine , 	" " );
    //        switch ( Druck_Klasse_Geschlecht )
    //        {
    //            case ART_BULLEN:	strcat(PrintLine , 	"BULLE "); 	break;		
    //            case ART_KUEHE:     strcat(PrintLine , 	"KUH   ");  break;		
    //            case ART_RINDER:    strcat(PrintLine , 	"RIND  "); 	break;		
    //            case ART_OCHSEN:	strcat(PrintLine , 	"OCHSE ");	break;		
    //            case ART_UNBEKANNT: strcat(PrintLine , 	"UNBEK. "); break;		
    //        }
    //        ret = WriteString( TDF_DB_TRANSFER_ENUM, PrintLine , 0);
    //        Delay(100);

    //        /* Mittelwert von */
    //        strcpy(DummyWeight, "" );
    //        strcpy(PrintLine , 	" MW: " );

    //        /* Mittelwert pro Klasse */
    //        dtoa (dMittelw , DummyWeight, 2 );
    //        StringFillUp(DummyWeight , 6, 1, ' ');
    //        strcat(PrintLine , 	DummyWeight );
    //        strcat(PrintLine , 	" kg," );

    //        ret = WriteString( TDF_DB_TRANSFER_ENUM, PrintLine , 0);
    //        Delay(100);

    //        /* Anzahl pro Klasse */
    //        strcpy(DummyWeight, "");
    //        strcpy(PrintLine , 	" STK: " );
    //        itoa ( Druck_Klasse_GewCount  , DummyWeight );
    //        StringFillUp(DummyWeight , 3, 1, ' ');
    //        strcat(PrintLine , 	DummyWeight);

    //        ret = WriteString( TDF_DB_TRANSFER_ENUM, PrintLine , 0);
    //        Delay(100);

    //        //Druck_Klasse_GewSumme = 99999;

    //        /* Summe pro Klasse */
    //        strcpy(DummyWeight, "");
    //        strcpy(PrintLine , 	" ,SUM:" );
    //        dtoa ( Druck_Klasse_GewSumme , DummyWeight, 2 );
    //        StringFillUp(DummyWeight , 8, 1, ' ');
    //        strcat(PrintLine , 	DummyWeight);
    //        strcat(PrintLine , 	" kg" );
    //        strcat(PrintLine ,  "\x0d\x0a");

    //        ret = WriteString( TDF_DB_TRANSFER_ENUM, PrintLine , 0);
    //        Delay(100);
    //    }
    //}while(TRUE);
        //}

        private void PrintLine(string DruckeText)
        {
            this.lstDruckerBuffer.Add(DruckeText);
        }

        public bool Starte_DruckerTest()
        {
            bool bRet = false;
            try
            {
                bRet = this.Starte_DruckerTest_Go();
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
                throw;
            }
            return bRet;
        }
        private bool Starte_DruckerTest_Go()
        {
            bool bRet = false;

            try
            {
                this.PrintLine("*************************************************************\x0d\x0a");
                this.PrintLine("Druckertest\x0d\x0a");
                this.PrintLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "\x0d\x0a");
                this.PrintLine("WOLLSDORF EPSON DRUCKER TEST\x0d\x0a");
                this.PrintLine("Test der Umlaute: Ö Ä Ü ö ä ü ß\x0d\x0a");
                this.PrintLine("*************************************************************\x0d\x0a");
                this.PrintLine("Test der Zeilenlänge:\x0d\x0a");
                this.PrintLine("123456789x123456789x123456789x123456789x123456789x123456789x123456789x123456789x\x0d\x0a");
                this.PrintLine("        10        20        30        40        50        60        70        80\x0d\x0a");
                this.PrintLine("*************************************************************\x0d\x0a");

                int iNummer = 0;
                this.PrintLine("\x0d\x0a");

                iNummer = 0;
                foreach (string sSendTxt in this.lstDruckerBuffer)
                {
                    iNummer++;

                    if (iNummer >= 3)
                    {
                        System.Threading.Thread.Sleep(300);
                        iNummer = 0;
                    }

                    this.SendTORS232(sSendTxt);
                }

                cGlobalScale.objRS232_X5.SendString(new string(this.cEpson_FormFeed));
                bRet = true;
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
                throw;
            }
            return bRet;
        }

        public bool Starte_SeitenLängenTest()
        {
            bool bRet = false;
            try
            {
                bRet = this.Starte_SeitenLängenTest_Go();
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
                throw;
            }
            return bRet;
        }
        private bool Starte_SeitenLängenTest_Go()
        {
            bool bRet = false;

            try
            {
                // Überschrift
                for (int i = 1; i <= 180; i++)
                {
                    this.PrintLine("Zeile " + i.ToString("000") +" Sonderzeichen: äöü ÄÖÜ ß \x0d\x0a");
                }

                int iNummer = 0;

                this.PrintLine("\x0d\x0a");

                iNummer = 0;
                foreach (string sSendTxt in this.lstDruckerBuffer)
                {
                    iNummer++;

                    if (iNummer >= 3)
                    {
                        System.Threading.Thread.Sleep(300);
                        iNummer = 0;
                    }

                    if(this.SendTORS232(sSendTxt))
                    {
                        //Seitenwechsel
                        cGlobalScale.objRS232_X5.SendString(new string (this.cEpson_FormFeed));
                                                
                        this.SendTORS232("************************* Neue Seite *************************\x0d\x0a");
                        this.SendTORS232("Dies sollte der Begin einer neuen Seite sein. Ist das nicht der Fall, dann wurde\x0d\x0a");
                        this.SendTORS232("die Anzahl Zeilen/Seite falsch eingestellt\x0d\x0a");
                        this.SendTORS232("Ändern Sie den Wert, Entladen das Papier und drucken nochmals\x0d\x0a");
                    }
                }

                cGlobalScale.objRS232_X5.SendString(new string(this.cEpson_FormFeed));                
                bRet = true;
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
                throw;
            }
            return bRet;
        }

        private bool SendTORS232(string SendTxt)
        {
            bool bSeitewechsel = false;

            try
            {
                if (SendTxt.IndexOf('\n') > 0)
                {
                    iCountLines++;
                }

                if (iCountLines >= iMaxLinesPerPage)
                {
                    bSeitewechsel = true;
                    iCountLines = 0;
                }

                byte[] encodingBytes = Encoding.GetEncoding(850).GetBytes(SendTxt);
                cGlobalScale.objRS232_X5.SendBytes(encodingBytes, encodingBytes.Length);
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
            }

            return bSeitewechsel;
        }
    }
}
