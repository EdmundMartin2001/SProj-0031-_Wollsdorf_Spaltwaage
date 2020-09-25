//namespace Wollsdorf.Spaltwaage
//{
//    using System;
//    using System.Collections.Generic;
//    using Allgemein;
//    using System.Text;
//    using Wollsdorf.Spaltwaage.Data;

//    internal class cDruck_AbschlussBericht
//    {
//        private cWiegung objWiegung;
//        private List<string> lstDruckerBuffer;
//        private int iCountLines;
//        private int iCountPage;
//        private int iMaxLinesPerPage = 66;
//        private char[] cEpson_FormFeed = { '\x0C' };
//        private int iHeaderPrintedPos = -1;

//        public cDruck_AbschlussBericht(ref cWiegung Wiegung) 
//        {
//            this.objWiegung = Wiegung;
//            this.lstDruckerBuffer = new List<string>() { };
//            this.iCountLines = 0;
//            this.iCountPage = 1;
//        }

//        ~cDruck_AbschlussBericht()
//        {

//        }

//        public bool drucke_Kopf() 
//        {
//            bool bRet = false;
//            try
//            {
//                //bRet = this.drucke_Kopf_Go();
//            }
//            catch (Exception ex)
//            {
//                SiAuto.LogException(ex);
//                throw;
//            }
//            return bRet;
//        }
//        //private bool drucke_Kopf_Go()
//        //{
//        //    bool bRet = false;
//        //    try
//        //    {
//        //        // Überschrift
//        //        this.PrintLine("Wollsdorf Leder Schmidt - Waagenprotokoll" + "\x0d\x0a");
//        //        this.PrintLine("-----------------------------------------" + "\x0d\x0a");
//        //        this.PrintLine("\x0d\x0a");
//        //        this.PrintLine("Lieferant:  " + this.objWiegung.iLieferantennummer.ToString() + "\x0d\x0a");
//        //        this.PrintLine("BestellNr:  " + this.objWiegung.lBestellnummer.ToString() + "\x0d\x0a");             
//        //        this.PrintLine("Datum:      " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "\x0d\x0a");
//        //        //this.PrintLine("Hakentara:  " + objWiegung.objKopfDaten.dHakenTara.ToString("###0.00") + " kg" + "\x0d\x0a");
//        //        this.PrintLine("\x0d\x0a");

//        //        int iNummer = 0;
//        //        string sDummy = "";

//        //        // ############################################################################################
//        //        // Klassenüberschrift (Liste alle gespeicherten Tierklassen mit den Bereichen auf)
//        //        #region Klassen Auflisten
//        //        string sTemp = "";
//        //        sTemp = "Nr|Bezeichnung       |Tier  | von (kg)| bis (kg)";
//        //        this.PrintLine(sTemp + "\x0d\x0a");
//        //        this.PrintLine(("".PadLeft(80, '-') + "\x0d\x0a"));
                

//        //        //foreach (cData_KlassenStamm_Item kl in this.objWiegung.objKlassenStammList)
//        //        //{
//        //        //    iNummer++;

//        //        //    sDummy = iNummer.ToString("#0").PadLeft(2, ' ') +
//        //        //        "|" +
//        //        //        kl.sBezeichnung.PadRight(18, ' ') + "|" +
//        //        //        ENUM_HELPER.Get_TierArten_Text(kl.TierArt).PadRight(6, ' ') + "|" +
//        //        //        kl.dGewicht_von.ToString("##0.00").PadLeft(9, ' ') + "|" +
//        //        //        kl.dGewicht_bis.ToString("##0.00").PadLeft(9, ' ') + "";

//        //        //    this.PrintLine(sDummy + "\x0d\x0a");
//        //        }

//        //        this.PrintLine("\x0d\x0a");

//        //        this.PrintLine(this.drucke_WiegeHeader(this.iCountPage));
//        //        this.PrintLine(("".PadLeft(80, '-') + "\x0d\x0a"));

//        //        this.PRINT_BUFFER();
//        //        #endregion

//        //        //#region Drucke
//        //        //iNummer = 0;
//        //        //foreach (string sSendTxt in this.lstDruckerBuffer)
//        //        //{
//        //        //    iNummer++;

//        //        //    if (iNummer >= 3)
//        //        //    {
//        //        //        System.Thr eading.Thread.Sleep(300);
//        //        //        iNummer = 0;
//        //        //    }

//        //        //    if (this.SendTORS232(sSendTxt))
//        //        //    {
//        //        //        // Seitenwechsel
//        //        //        this.iCountPage++;

//        //        //        cGlobalScale.objRS232_X5.SendString(new string(this.cEpson_FormFeed));
//        //        //        System.Thr eading.Thread.Sleep(300);
//        //        //        this.SendTORS232(this.drucke_WiegeHeader(this.iCountPage));
//        //        //        System.Thr eading.Thread.Sleep(300);
//        //        //        this.SendTORS232(("".PadLeft(80, '-') + "\x0d\x0a"));
//        //        //    }
//        //        //}
//        //        //#endregion
//        //        //this.lstDruckerBuffer.Clear();
//        //        bRet = true;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        SiAuto.LogException(ex);
//        //        throw;
//        //    }
//        //    return bRet;
//        //}

//        public bool drucke_ZwischenSum()
//        {
//            bool bRet = false;
//            try
//            {
//                //bRet = this.drucke_ZwischenSum_Go();
//            }
//            catch (Exception ex)
//            {
//                SiAuto.LogException(ex);
//                throw;
//            }
//            return bRet;
//        }
//        //private bool drucke_ZwischenSum_Go()
//        //{
//        //    bool bRet = false;
//        //    iHeaderPrintedPos = -1;

//        //    try
//        //    {
//        //        // Überschrift
//        //        this.PrintLine("Summierung" + "\x0d\x0a");
//        //        this.PrintLine("-----------------------------------------" + "\x0d\x0a");
//        //        this.PrintLine("Anzahl Einzelwiegung: " + this.objWiegung.objWiegePositionsList.Count.ToString() + " Stk\x0d\x0a");
//        //        this.PrintLine("\x0d\x0a");
//        //        this.PrintLine("Stiere ->Gesamt: " + this.FormatGewicht(objWiegung.objSUM.dGesamtSumme_Stiere) + " kg | " + objWiegung.objSUM.iMittelwert_Anzahl_Stiere.ToString() + " Stk\x0d\x0a");
//        //        this.PrintLine("Kuh    ->Gesamt: " + this.FormatGewicht(objWiegung.objSUM.dGesamtSumme_Kühe)   + " kg | " + objWiegung.objSUM.iMittelwert_Anzahl_Kühe.ToString() + " Stk\x0d\x0a");
//        //        this.PrintLine("Rind   ->Gesamt: " + this.FormatGewicht(objWiegung.objSUM.dGesamtSumme_Rinder) + " kg | " + objWiegung.objSUM.iMittelwert_Anzahl_Rinder.ToString() + " Stk\x0d\x0a");
//        //        this.PrintLine("Ochse  ->Gesamt: " + this.FormatGewicht(objWiegung.objSUM.dGesamtSumme_Ochsen) + " kg | " + objWiegung.objSUM.iMittelwert_Anzahl_Ochsen.ToString() + " Stk\x0d\x0a");
//        //        this.PrintLine("\x0d\x0a");

//        //        int iNummer = 0;
//        //        string sDummy = "";

//        //        // Klassenüberschrift
//        //        string sTemp = "";
//        //        sTemp = "Nr|Bezeichnung       |Tier  |Anzahl|  MW (kg)| Gew (kg)";
//        //        this.PrintLine(sTemp + "\x0d\x0a");
//        //        this.PrintLine(("".PadLeft(80, '-') + "\x0d\x0a"));

//        //        // Markiere Druckzeile damit Header nicht vor dieser Zeile bei einem Seitenwechsel gedruckt wird
//        //        this.iHeaderPrintedPos = this.lstDruckerBuffer.Count;

//        //        foreach (cData_KlassenStamm_Item kl in this.objWiegung.objKlassenStammList)
//        //        {
//        //            iNummer++;

//        //            sDummy = iNummer.ToString("#0").PadLeft(2, ' ') +
//        //                "|" +
//        //                kl.sBezeichnung.PadRight(18, ' ') + "|" +
//        //                ENUM_HELPER.Get_TierArten_Text(kl.TierArt).PadRight(6, ' ') + "|" +
//        //                kl.iSummierung_Anzahl.ToString().PadLeft(6, ' ') + "|" +
//        //                this.FormatGewicht(kl.dSummierung_Gewicht) + "|" +
//        //                this.FormatGewicht(kl.Get_Summierung_Mittelwert) + "";

//        //            this.PrintLine(sDummy + "\x0d\x0a");
//        //        }

//        //        this.PrintLine("\x0d\x0a");

//        //        iNummer = 0;
//        //        foreach (string sSendTxt in this.lstDruckerBuffer)
//        //        {
//        //            iNummer++;

//        //            if (iNummer >= 3)
//        //            {
//        //                this.DRUCK_PAUSE();
//        //                iNummer = 0;
//        //            }

//        //            if (this.SendTORS232(sSendTxt))
//        //            {
//        //                // Seitenwechsel
//        //                this.iCountPage++;

//        //                cGlobalScale.objRS232_X5.SendString(new string(this.cEpson_FormFeed));
//        //                this.DRUCK_PAUSE();
                        
//        //                // Erst erneut Header drucken, wenn dieser schon vorher einmal gedruckt wurde
//        //                if ( iNummer >= iHeaderPrintedPos)
//        //                {
//        //                    this.SendTORS232(this.drucke_ZwischenSumHeader(this.iCountPage));
//        //                    this.DRUCK_PAUSE();
//        //                    this.SendTORS232(("".PadLeft(80, '-') + "\x0d\x0a"));
//        //                }
//        //            }
//        //        }
//        //        this.lstDruckerBuffer.Clear();

//        //        bRet = true;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        SiAuto.LogException(ex);
//        //        throw;
//        //    }
//        //    return bRet;
//        //}

//        private string drucke_ZwischenSumHeader(int iSeite)
//        {
//            string sTemp = "";

//            sTemp = ("Seite / Page " + iSeite.ToString() + "\x0d\x0a");
//            sTemp += ("Nr|Bezeichnung       |Tier  |Anzahl|  MW (kg)| Gew (kg)\x0d\x0a");

//            return sTemp;
//        }
//        private string drucke_WiegeHeader(int iSeite) 
//        {
//            string stemp = "";

//            stemp = ("Seite / Page " + iSeite.ToString() + "\x0d\x0a");
//            stemp += ("Nr|S|W|Tier  |Netto (kg)\x0d\x0a");
            
//            return stemp;
//        }

//        public bool drucke_WiegePosition(
//            ref cData_Wiegung_Item EinzelWiegung)
//        {
//            bool bRet = false;
//            try
//            {
//                //bRet = this.drucke_WiegePosition_go(ref EinzelWiegung);
//            }
//            catch (Exception ex)
//            {
//                SiAuto.LogException(ex);
//                throw;
//            }
//            return bRet;
//        }
//        //private bool drucke_WiegePosition_go(
//        //    ref cData_Wiegung_Item EinzelWiegung)
//        //{
//        //    bool bRet = false;
//        //    string sSendTxt = "";

//        //    sSendTxt = EinzelWiegung.iFortlaufendeNummer.ToString().PadLeft(2, ' ') + "|" +
//        //                (EinzelWiegung.bISStorno ? "S" : " ") + "|" +
//        //                (EinzelWiegung.bISWasser ? "W" : " ") + "|" +
//        //                ENUM_HELPER.TierArt_EnumToDisplay(EinzelWiegung.objTierArt.eTierArt).ToString().PadRight(6, ' ') + "|" +
//        //                this.FormatStornoNetto(EinzelWiegung.dNetto, EinzelWiegung.bISStorno) + "\x0d\x0a";

//        //    // Einzeldruck einer Wiegeposition
//        //    if (this.SendTORS232(sSendTxt))
//        //    {
//        //        // Seitenwechsel
//        //        this.iCountPage++;

//        //        cGlobalScale.objRS232_X5.SendString(new string(this.cEpson_FormFeed));
//        //        this.DRUCK_PAUSE();
//        //        this.SendTORS232(this.drucke_WiegeHeader(this.iCountPage));
//        //        this.DRUCK_PAUSE();
//        //        this.SendTORS232(("".PadLeft(80, '-') + "\x0d\x0a"));                
//        //    }
//        //    this.lstDruckerBuffer.Clear();
//        //    this.DRUCK_PAUSE();
//        //    bRet = true;

//        //    return bRet;
//        //}

//        public bool drucke_Fusszeile()
//        {
//            bool bRet = false;
//            try
//            {
//                bRet = this.drucke_Fusszeile_Go();
//            }
//            catch (Exception ex)
//            {
//                SiAuto.LogException(ex);
//                throw;
//            }
//            return bRet;
//        }
//        private bool drucke_Fusszeile_Go()
//        {
//            bool bRet = false;
//            try
//            {
//                // Überschrift
//                this.PrintLine("-----------------------------------------" + "\x0d\x0a");
//                this.PrintLine("\x0d\x0a");
//                this.PrintLine("Lieferant:  " + this.objWiegung.iLieferantennummer.ToString() + "\x0d\x0a");
//                this.PrintLine("BestellNr:  " + this.objWiegung.lBestellnummer.ToString() + "\x0d\x0a");
//                this.PrintLine("Datum:      " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "\x0d\x0a");
//                //this.PrintLine("Hakentara:  " + objWiegung.dHakenTara.ToString("###0.00") + " kg" + "\x0d\x0a");
//                this.PrintLine("\x0d\x0a");

//                // Drucke alles im Buffer aus und lösche diesen
//                this.PRINT_BUFFER();
 
//                bRet = true;
//            }
//            catch (Exception ex)
//            {
//                SiAuto.LogException(ex);
//                throw;
//            }
//            return bRet;
//        }

//        private string FormatStornoNetto(double dNetto, bool bStorno) 
//        {
//            if (bStorno)
//            {
//                dNetto = dNetto - dNetto - dNetto;
//                dNetto.ToString("##0.00").PadLeft(9, ' ');
//            }

//            return dNetto.ToString("##0.00").PadLeft(9, ' '); 
//        }
//        private string FormatGewicht(double dNetto)
//        {            
//            return dNetto.ToString("###0.00").PadLeft(9, ' ');
//        }

//        //public bool drucke_Zwischensumme(ref Wollsdorf.cWiegung wg) 
//        //{
//        //    bool bRet = false;
//        //    try
//        //    {
//        //        bRet = this.drucke_Zwischensumme_go(ref wg);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        throw;
//        //    }
//        //    return bRet;
//        //}
//        //private bool drucke_Zwischensumme_go(ref Wollsdorf.cWiegung wg) 
//        //{
//        //    bool bRet = false;
//        //    try
//        //    {
//        //        this.PrintLine("\x0d\x0a---------------------------------------");
//        //        //strcat(PrintLine, "\x0d\x0a");
//        //        //strcat(PrintLine, "---------------------------------------");
//        //        //strcat(PrintLine, "\x0d\x0a");
//        //        this.PrintLine("\x0d\x0aLieferant: "+this.objWiegung.objWiegung.sLieferantennummer.ToString()+", Datum: "+
//        //            DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss"));
//        //        ///* Lieferantendaten */
//        //        //strcpy(PrintLine, "\x0d\x0a");
//        //        //strcat(PrintLine, "Lieferant: ");
//        //        //ltoa(LieferantenNummer, Dummy1);
//        //        //strcat(PrintLine, Dummy1);
//        //        //strcat(PrintLine, ", ");
//        //        //ret = WriteString(TDF_DB_TRANSFER_ENUM, PrintLine, 0);
//        //        //Delay(100);
//        //        //strcpy(PrintLine, "Datum: ");
//        //        //strcat(PrintLine, DateStrActual);
//        //        //strcat(PrintLine, " ");
//        //        //strcat(PrintLine, TimeStrActual);
//        //        //strcat(PrintLine, "\x0d\x0a");
//        //        //ret = WriteString(TDF_DB_TRANSFER_ENUM, PrintLine, 0);
//        //        //Delay(100);
//        //        this.PrintLine("Anzahl Einzelwiegungen: "+objWiegung.objWiegePosition.Count.ToString()+"\x0d\x0a");
//        //        ///* Anzahl Wägungen */
//        //        //strcpy(PrintLine, "Anzahl Einzelwiegungen: ");
//        //        //ltoa(LaufendeNummer, Dummy1);
//        //        //strcat(PrintLine, Dummy1);
//        //        //strcat(PrintLine, "\x0d\x0a");
//        //        //strcat(PrintLine, "\x0d\x0a");
//        //        //ret = WriteString(TDF_DB_TRANSFER_ENUM, PrintLine, 0);
//        //        //Delay(100);	

//        //        //DRUCKE EINE SUMME PRO TIERART also BULLEN, KUH, RIND, OCHSE an wenn Wägungen erfolgt sind
//        //        double dDummyWeight = 0;
//        //        foreach (Wollsdorf.Data.cData_KlassenStamm_Item kl in this.objWiegung.objKlassenStammList)
//        //        {
//        //            switch (kl.TierArt)
//        //            {
//        //                case eTierArt.kühe:
//        //                    dDummyWeight = wg.objSUM.dGesamtSumme_Kühe;
//        //                    break;
//        //                case eTierArt.ochsen:
//        //                    dDummyWeight = wg.objSUM.dGesamtSumme_Ochsen;
//        //                    break;
//        //                case eTierArt.rinder:
//        //                    dDummyWeight = wg.objSUM.dGesamtSumme_Rinder;
//        //                    break;
//        //                case eTierArt.stiere:
//        //                    dDummyWeight = wg.objSUM.dGesamtSumme_Bullen;
//        //                    break;
//        //                default:
//        //                    dDummyWeight = 0;
//        //                    break;
//        //            }

//        //            this.PrintLine(
//        //                ENUM_HELPER.Get_TierArten_Text(kl.TierArt).PadRight(6, ' ') +
//        //                "->Gesamt: " + dDummyWeight.ToString("##0.00")+" kg");
//        //        }
//        //        this.PrintLine("\x0d\x0a");
//        //        //for (i = 1; i < 5; i++)
//        //        //    if (Zwischen_MittelwertCnt[i] > 0) /* Summe Geschlecht : BULLEN */
//        //        //    {
//        //        //        strcpy(DummyWeight, "");
//        //        //        dtoa(Zwischen_GesamtSumme[i], DummyWeight, 2);
//        //        //        StringFillUp(DummyWeight, 10, 1, ' ');
//        //        //        switch (i)
//        //        //        {
//        //        //            case ART_BULLEN: strcat(PrintLine, "BULLE "); break;
//        //        //            case ART_KUEHE: strcat(PrintLine, "KUH   "); break;
//        //        //            case ART_RINDER: strcat(PrintLine, "RIND  "); break;
//        //        //            case ART_OCHSEN: strcat(PrintLine, "OCHSE "); break;
//        //        //            case ART_UNBEKANNT: strcat(PrintLine, "UNBEK."); break;
//        //        //        }
//        //        //        strcat(PrintLine, "->Gesamt: ");
//        //        //        strcat(PrintLine, DummyWeight);
//        //        //        strcat(PrintLine, " kg \x0d\x0a");
//        //        //}
//        //        int iNummer = 0; 
//        //        string sDummy = "";
//        //        foreach (Wollsdorf.Data.cData_KlassenStamm_Item kl in this.objWiegung.objKlassenStammList)
//        //        {
//        //            switch (kl.TierArt)
//        //            {
//        //                case eTierArt.kühe:
//        //                    if (wg.objSUM.dMittelwert_Kühe > 0)
//        //                    {
//        //                        this.MittelWertFormatHelper(kl.TierArt, wg.objSUM.dGesamtSumme_Kühe, wg.objSUM.dMittelwert_Kühe, wg.objSUM.iMittelwert_Anzahl_Kühe);
//        //                    }
//        //                    break;
//        //                case eTierArt.ochsen:
//        //                    if (wg.objSUM.dMittelwert_Ochsen > 0)
//        //                    {
//        //                        this.MittelWertFormatHelper(kl.TierArt, wg.objSUM.dGesamtSumme_Ochsen, wg.objSUM.dMittelwert_Ochsen, wg.objSUM.iMittelwert_Anzahl_Ochsen);
//        //                    }
//        //                    break;
//        //                case eTierArt.stiere:
//        //                    if (wg.objSUM.dGesamtSumme_Bullen > 0)
//        //                    {
//        //                        this.MittelWertFormatHelper(kl.TierArt, wg.objSUM.dGesamtSumme_Bullen, wg.objSUM.dMittelwert_Bullen, wg.objSUM.iMittelwert_Anzahl_Bullen);
//        //                    }
//        //                    break;
//        //                case eTierArt.rinder:
//        //                    if (wg.objSUM.dMittelwert_Rinder > 0)
//        //                    {
//        //                        this.MittelWertFormatHelper(kl.TierArt, wg.objSUM.dGesamtSumme_Rinder, wg.objSUM.dMittelwert_Rinder, wg.objSUM.iMittelwert_Anzahl_Rinder);
//        //                    }
//        //                    break;


//        //            }

//        //            //sDummy = "Nr. " +
//        //            //    iNummer.ToString("#0").PadLeft(2, ' ') +
//        //            //    " - " +
//        //            //    kl.sBezeichnung.PadRight(18, ' ') +
//        //            //    cGlobalHandling.Get_Tierklassen_Text(kl.TierKlasse).PadRight(6, ' ') + " von: " +
//        //            //    kl.dGewicht_von.ToString("##0.00").PadLeft(6, ' ') + " kg bis: " +
//        //            //    kl.dGewicht_bis.ToString("##0.00").PadLeft(6, ' ') + " kg";

//        //            this.PrintLine(sDummy);
//        //        }

//        //        this.PrintLine("\x0d\x0a");

//        //        iNummer = 0;
//        //        foreach (string sSendTxt in this.lstDruckerBuffer)
//        //        {
//        //            iNummer++;

//        //            if (iNummer >= 3)
//        //            {
//        //                System.Thr eading.Thread.Sleep(300);
//        //                iNummer = 0;
//        //            }

//        //            cGlobalScale.objRS232_X5.SendString(sSendTxt);
//        //        }

//        //        bRet = true;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        SiAuto.LogException(ex);
//        //        throw;
//        //    }
//        //    return bRet;
//        //}

//        //private void MittelWertFormatHelper(eTierArt eKlasse, double dSum, double dMW, int iAnz)
//        //{
//        //    string sDummy = "";
//        //    if( iAnz > 0)
//        //    {
//        //        sDummy = "kkkkkkkkkkkk".PadRight(13, ' ') + " " +
//        //            ENUM_HELPER.Get_TierArten_Text(eKlasse).PadRight(6, ' ') +
//        //            " MW:"+dMW.ToString("##0.00").PadLeft(7,' ')+" kg, STK:"+ iAnz.ToString().PadLeft(3,' ') +
//        //            " ,Sum: "+dSum.ToString().PadLeft(7,' ')+" kg";
//        //    }
		 
//    //        /* Name */
//    //        //strcat(PrintLine , 	" - " );
//    //        StringFillUp(Druck_Klasse_Bezeichnung , 12, FORMAT_LEFT, ' ');
//    //        strcpy(PrintLine , 	Druck_Klasse_Bezeichnung );

//    //        ret = WriteString( TDF_DB_TRANSFER_ENUM, PrintLine , 0);
//    //        Delay(100);

//    //        /* Tierart */
//    //        strcpy(PrintLine , 	" " );
//    //        switch ( Druck_Klasse_Geschlecht )
//    //        {
//    //            case ART_BULLEN:	strcat(PrintLine , 	"BULLE "); 	break;		
//    //            case ART_KUEHE:     strcat(PrintLine , 	"KUH   ");  break;		
//    //            case ART_RINDER:    strcat(PrintLine , 	"RIND  "); 	break;		
//    //            case ART_OCHSEN:	strcat(PrintLine , 	"OCHSE ");	break;		
//    //            case ART_UNBEKANNT: strcat(PrintLine , 	"UNBEK. "); break;		
//    //        }
//    //        ret = WriteString( TDF_DB_TRANSFER_ENUM, PrintLine , 0);
//    //        Delay(100);

//    //        /* Mittelwert von */
//    //        strcpy(DummyWeight, "" );
//    //        strcpy(PrintLine , 	" MW: " );

//    //        /* Mittelwert pro Klasse */
//    //        dtoa (dMittelw , DummyWeight, 2 );
//    //        StringFillUp(DummyWeight , 6, 1, ' ');
//    //        strcat(PrintLine , 	DummyWeight );
//    //        strcat(PrintLine , 	" kg," );

//    //        ret = WriteString( TDF_DB_TRANSFER_ENUM, PrintLine , 0);
//    //        Delay(100);

//    //        /* Anzahl pro Klasse */
//    //        strcpy(DummyWeight, "");
//    //        strcpy(PrintLine , 	" STK: " );
//    //        itoa ( Druck_Klasse_GewCount  , DummyWeight );
//    //        StringFillUp(DummyWeight , 3, 1, ' ');
//    //        strcat(PrintLine , 	DummyWeight);

//    //        ret = WriteString( TDF_DB_TRANSFER_ENUM, PrintLine , 0);
//    //        Delay(100);

//    //        //Druck_Klasse_GewSumme = 99999;

//    //        /* Summe pro Klasse */
//    //        strcpy(DummyWeight, "");
//    //        strcpy(PrintLine , 	" ,SUM:" );
//    //        dtoa ( Druck_Klasse_GewSumme , DummyWeight, 2 );
//    //        StringFillUp(DummyWeight , 8, 1, ' ');
//    //        strcat(PrintLine , 	DummyWeight);
//    //        strcat(PrintLine , 	" kg" );
//    //        strcat(PrintLine ,  "\x0d\x0a");

//    //        ret = WriteString( TDF_DB_TRANSFER_ENUM, PrintLine , 0);
//    //        Delay(100);
//    //    }
//    //}while(TRUE);
        
//        private void PrintLine(string DruckeText)
//        {
//            this.lstDruckerBuffer.Add(DruckeText);
//        }
//        private bool SendTORS232(string SendTxt)
//        {
//            bool bSeitewechsel = false;

//            try
//            {
//                if (SendTxt.IndexOf('\n') > 0)
//                {
//                    iCountLines++;
//                }

//                if (iCountLines >= iMaxLinesPerPage)
//                {
//                    bSeitewechsel = true;
//                    iCountLines = 0;
//                }

//                byte[] encodingBytes = Encoding.GetEncoding(850).GetBytes(SendTxt);
//                cGlobalScale.objRS232_X5.SendBytes(encodingBytes, encodingBytes.Length);
//            }
//            catch (Exception ex)
//            {
//                SiAuto.LogException(ex);
//            }

//            return bSeitewechsel;
//        }
//        private void PRINT_BUFFER()
//        {
//            int iNummer = 0;

//            try
//            {
//                foreach (string sSendTxt in this.lstDruckerBuffer)
//                {
//                    iNummer++;

//                    if (iNummer >= 3)
//                    {
//                        this.DRUCK_PAUSE();
//                        iNummer = 0;
//                    }

//                    if (this.SendTORS232(sSendTxt))
//                    {
//                        // Seitenwechsel
//                        this.iCountPage++;

//                        cGlobalScale.objRS232_X5.SendString(new string(this.cEpson_FormFeed));
//                        this.DRUCK_PAUSE();

//                        // Erst erneut Header drucken, wenn dieser schon vorher einmal gedruckt wurde
//                        if (iNummer >= this.iHeaderPrintedPos)
//                        {
//                            this.SendTORS232(this.drucke_ZwischenSumHeader(this.iCountPage));
//                            this.DRUCK_PAUSE();
//                            this.SendTORS232(("".PadLeft(80, '-') + "\x0d\x0a"));
//                        }
//                    }
//                }
//                this.lstDruckerBuffer.Clear();
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//        }
//        private void DRUCK_PAUSE()
//        {
//            try
//            {
//                if (SMT_SQL_2V.SMT_Standards.AppStandards.ISEntwicklungsPC)
//                {
//                }
//                else
//                {
//                    System.Threading.Thread.Sleep(300);
//                }
//            }
//            catch (Exception)
//            {
                
//                throw;
//            }
//        }
//    }
//}
