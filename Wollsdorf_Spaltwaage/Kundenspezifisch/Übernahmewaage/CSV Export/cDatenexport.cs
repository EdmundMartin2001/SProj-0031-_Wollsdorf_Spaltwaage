//namespace Wollsdorf.Spaltwaage
//{
//    using System;
//    using System.Linq;
//    using System.Collections.Generic;
//    using System.Text;
//    using Allgemein;
//    using System.Diagnostics;
//    using Wollsdorf.Spaltwaage.Data;

//    internal class cDatenexport
//    {
//        private System.IO.StreamWriter sw;
//        private string sKennzeichen;

//        private cWiegung objWiegung;        
                
//        public cDatenexport(            
//            ref cWiegung wg) 
//        {
//            this.objWiegung = wg;
//            this.sKennzeichen = "";
//        }

//        public bool Starte_ÜW_CSV_DatenExport(
//            string sKennzeichen, 
//            cData_Wiegung_Item EinzelWiegung)
//        {
//            bool bRet = false;

//            try
//            {
//                this.objWiegeItem = EinzelWiegung;
//                this.sKennzeichen = sKennzeichen;

//                if (this.OpenFile())
//                {
//                    int iStartNr = cGlobalNummerkreis.Nummernkreis1_GetNext();

//                    //bRet = drucke_Datenexport_go(iStartNr,this.objWiegung, this.objWiegeItem);

//                    if (bRet)
//                    {
//                        // Setze neuen Nummernkreis
//                        cGlobalNummerkreis.Nummernkreis1_Set(iStartNr);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                SiAuto.LogException("Starte_ÜW_CSV_DatenExport", ex );
//                throw;
//            }
//            finally
//            {
//                if (sw != null)
//                {
//                    sw.Close();
//                    sw = null;
//                }
//            }

//            return bRet;
//        }
//        public bool Starte_ÜW_CSV_Abschluss(
//           string sKennzeichen)
//        {
//            bool bRet = false;

//            try
//            {
//                this.sKennzeichen = sKennzeichen;

//                if (this.OpenFile())
//                {
//                    int iStartNr = cGlobalNummerkreis.Nummernkreis1_GetNext();

//                    bRet = drucke_LieferantenAbschluss_go(iStartNr,this.objWiegung);

//                    if (bRet)
//                    {
//                        // Setze neuen Nummernkreis
//                        cGlobalNummerkreis.Nummernkreis1_Set(iStartNr);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                SiAuto.LogException(ex);
//                throw;
//            }
//            finally
//            {
//                if (sw != null)
//                {
//                    sw.Close();
//                    sw = null;
//                }
//            }

//            return bRet;
//        }
//        //private bool drucke_Datenexport_go(int iIndexNr, cWiegung wg, cData_Wiegung_Item EinzelWiegung)
//        //{
//        //    bool bRet = true;
//        //    string Exp_OutStr = "";

//        //    try
//        //    {
//        //            Exp_OutStr = String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19}",
//        //            /*00 fortlaufende Nummer*/  iIndexNr.ToString(),
//        //            /*01 Kennzeichen*/          "E",
//        //            /*02 Wasser*/         
//        //            /*03 Lieferantennummer*/    wg.iLieferantennummer.ToString(),
//        //            /*04 Bestellnummer*/        wg.lBestellnummer.ToString(),
//        //            ///*05 Hakentara*/            wg.dHakenTara.ToString("###.00"),
//        //            /*06 Netto*/                EinzelWiegung.dNetto.ToString("###.00"),
//        //            /*07 Tierart*/              
//        //            /*08 Schnitte*/             ENUM_HELPER.Get_Schnitte_Text(EinzelWiegung.objMerkmalStatus.Schnitte),
//        //            /*09 Löcher*/               ENUM_HELPER.Get_Löcher_Text(EinzelWiegung.objMerkmalStatus.Löcher),
//        //            /*10 Fäulnis*/              ENUM_HELPER.Get_Fäulnis_Text(EinzelWiegung.objMerkmalStatus.Fäulnis),
//        //            /*11 SW*/                   ENUM_HELPER.Get_SW_Text(EinzelWiegung.objMerkmalStatus.SW),
//        //            /*12 Dung*/                 ENUM_HELPER.Get_Dung_Text(EinzelWiegung.objMerkmalStatus.Dung),
//        //            /*13 Eis*/                  ENUM_HELPER.Get_Eis_Text(EinzelWiegung.objMerkmalStatus.Eis),
//        //            /*14 Überlagert*/           ENUM_HELPER.Get_Überlagert_Text(EinzelWiegung.objMerkmalStatus.Überlagert),
//        //            /*15 Haarlässig*/           ENUM_HELPER.Get_Haarlässig_Text(EinzelWiegung.objMerkmalStatus.Haarlässig),
//        //            /*16 Fett*/                 ENUM_HELPER.Get_Fett_Text(EinzelWiegung.objMerkmalStatus.Fett), 
//        //            /*17 Nass*/                 ENUM_HELPER.Get_Nass_Text(EinzelWiegung.objMerkmalStatus.Nass),
//        //            /*18 Lebenschäden*/         ENUM_HELPER.Get_Lebendschäden_Text(EinzelWiegung.objMerkmalStatus.Lebendschäden),
//        //            /*19 Zeitstempel*/          DateTime.Now.ToString("dd.MM.yyy HH:mm:ss")
//        //            );
//        //    }
//        //    catch (Exception ex)
//        //    {            
//        //        bRet = false;
//        //        return bRet;
//        //    }
//        //    sw.WriteLine(Exp_OutStr);
//        //    return bRet;
//        //}
//        private bool drucke_LieferantenAbschluss_go(int iIndexNr, cWiegung wg)
//        {
//            bool bRet = true;
//            string Exp_OutStr = "";

//            try
//            {
//                Exp_OutStr = String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19}",
//                    /*00 fortlaufende Nummer*/  iIndexNr.ToString(),
//                    /*01 Kennzeichen*/          "A",
//                    /*02 Wasser*/               "",
//                    /*03 Lieferantennummer*/    wg.iLieferantennummer.ToString(),
//                    /*04 Bestellnummer*/        wg.lBestellnummer.ToString(),
//                    ///*05 Hakentara*/            wg.objKopfDaten.dStutzAbfälle.ToString("###.00"),
//                    ///*06 Netto*/                wg.objKopfDaten.dTemperatur.ToString("###.0"),
//                    /*07 Tierart*/              "",
//                    /*08 Schnitte*/             "",
//                    /*09 Löcher*/               "",
//                    /*10 Fäulnis*/              "",
//                    /*11 SW*/                   "",
//                    /*12 Dung*/                 "",
//                    /*13 Eis*/                  "",
//                    /*14 Überlagert*/           "",
//                    /*15 Haarlässig*/           "",
//                    /*16 Fett*/                 "",
//                    /*17 Nass*/                 "",
//                    /*18 Lebenschäden*/         "",
//                    /*19 Zeitstempel*/          DateTime.Now.ToString("dd.MM.yyy HH:mm:ss")
//                );
//            }
//            catch (Exception ex)
//            {
//                bRet = false;
//                return bRet;
//            }
//            sw.WriteLine(Exp_OutStr);
//            return bRet;
//        }

//        private bool OpenFile() 
//        {
//            bool bret = false;

//            string fullFileName =
//                this.objWiegung.objSettings.CSVPath + @"\Frischhaut.csv";

//            try
//            {
//                sw = new System.IO.StreamWriter(
//                    fullFileName, true /*Append*/ , System.Text.Encoding.GetEncoding("windows-1252"));
//                bret = true;    
//            }
//            catch (Exception ex)
//            {
//                SiAuto.LogException("OpenFile_CSV", ex);
//            }

//            return bret;
//        }
//    }
//}
