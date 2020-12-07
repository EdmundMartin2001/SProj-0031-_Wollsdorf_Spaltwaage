using System;
using System.Diagnostics;
using System.Collections;
using System.Text.RegularExpressions;
using Wollsdorf_Spaltwaage.Allgemein.SQL;

namespace SMT_SQL_2V.Helper
{
    internal class cStringHelper
    {
        /// <summary>
        /// Schneidet einen String an der Max Len ab
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Truncate(string value, int maxLength)
        {
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        /// <summary>
        /// Verwendet RegEx um die Anzahl der zu suchenden Zeichen im String zu ermitteln
        /// zb.: if ( CountStrings(tb.Text, ",") > 0)....
        /// </summary>
        /// <param name="stringToCheck">Gesamter String</param>
        /// <param name="stringToSearchFor">Gesuchtes Zeichen</param>
        /// <returns></returns>
        public static int CountStrings(string stringToCheck, string stringToSearchFor)
        {
            MatchCollection coll = Regex.Matches(stringToCheck, stringToSearchFor);
            return coll.Count;
        }


        /// <summary>
        /// Entferne alle Vornullen, mit Hilfe des TrimStart Befehls
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        #region RemoveLeadingZeroesV2
        public static string RemoveLeadingZeroesV2(string input)
        {
            try
            {
                input = "00000000010213";
                return input.TrimStart('0');
            }
            catch (Exception ex)
            {
                SiAuto.LogException("RemoveLeadingZeroesV2", ex);
                return "ERROR";
            }
        }
        #endregion

        /// <summary>
        /// Entferne Vornullen aus einem String mithilfe des RegEX 
        /// Befehls
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        #region RemoveLeadingZeroesV1
        public static string RemoveLeadingZeroesV1(string input)
        {
            try
            {
                Regex reg1 = new Regex("\\.\\d*(?<1>0+)[^\\d]*$", RegexOptions.IgnoreCase);

                Match m = reg1.Match(input);
                if (m.Success)
                {
                    input = input.Replace(m.Groups["1"].Value, "");
                    Regex reg2 = new Regex("(?<1>\\.)[^\\d]*$", RegexOptions.IgnoreCase);
                    m = reg2.Match(input);

                    if (m.Success)
                    {
                        input = input.Replace(".", "");
                    }
                    Regex reg3 = new Regex("\\d", RegexOptions.IgnoreCase);
                    m = reg3.Match(input);
                    if (!m.Success)
                    {
                        input = "0" + input;

                    }

                }
                if (input.StartsWith("."))
                    input = "0" + input;

                return input;
            }
            catch (Exception ex)
            {
                SiAuto.LogException("RemoveTrailingZeroes", ex);
            }

            return input;
        }
        #endregion


        #region Word_Wrap - Teile String auf und füge einen Wortumbruch ein

        public static string Word_Wrap(string text, int maxLength) 
        { 

            text = text.Replace("\n", " "); 
            text = text.Replace("\r", " "); 
            text = text.Replace(".", ". "); 
            text = text.Replace(">", "> "); 
            text = text.Replace("\t", " "); 
            text = text.Replace(",", ", "); 
            text = text.Replace(";", "; "); 
            text = text.Replace("<br>", " "); 
            text = text.Replace("  ", " "); 


            string[] Words = text.Split(' '); 
            int currentLineLength = 0; 
            ArrayList Lines = new ArrayList(text.Length / maxLength); 
            string currentLine = ""; 
             
            bool InTag = false; 

            foreach (string currentWord in Words) 
            { 
                //ignore html 
                if (currentWord.Length > 0) 
                { 
                    if (currentWord.Substring(0,1) == "<") 
                        InTag = true; 


                    if (InTag) 
                    { 
                        //handle filenames inside html tags 
                        if (currentLine.EndsWith(".")) 
                        { 
                            currentLine += currentWord; 
                        } 
                        else 
                            currentLine += " " + currentWord; 

                        if (currentWord.IndexOf(">") > -1) 
                            InTag = false; 
                    } 
                    else 
                    { 
                        if (currentLineLength + currentWord.Length + 1 < maxLength) 
                        { 
                            
                            currentLine += " " + currentWord; 
                            currentLineLength += (currentWord.Length + 1); 
                        } 
                        else 
                        { 
                            Lines.Add(currentLine); 
                            currentLine = currentWord; 
                            currentLineLength = currentWord.Length; 
                        } 
                    } 
                } 
            } 


            if (currentLine != "") 
                Lines.Add(currentLine); 

            string[] textLinesStr = new string[Lines.Count]; 
            Lines.CopyTo(textLinesStr, 0); 

            string ret = "";

            for (int i=0; i < Lines.Count ; i++ ) 
            {
                ret += Lines[i] + "\r\n";
            }

            return ret; 
        }

        #endregion

        #region Calculate_Format
        /// <summary>
        /// Erstelle einen String für die double.ToString("####0.000") Konvertierung
        /// wobei Keine Nachkomma und das auffüllen der linken Seite berücksichtig wird
        /// </summary>
        /// <param name="iNachkomma"></param>
        /// <returns></returns>
        public static string Converter_Format(int iNachkomma)
        {
            return Converter_Format(iNachkomma, 0);
        }
        public static string Converter_Format(int iNachkomma, int iPadLeftLen)
        {
            string sFormat = "";

            if (iNachkomma < 1)
            {
                sFormat = "####0";
                if (iPadLeftLen > 0) { sFormat = sFormat.PadLeft(iPadLeftLen, '#'); }
            }
            else
            {
                sFormat = "####0.";
                sFormat = sFormat.PadRight(iNachkomma + sFormat.Length, '0');
                if (iPadLeftLen > 0) { sFormat = sFormat.PadLeft(iPadLeftLen, '#'); }
            }

            return sFormat;
        }
        #endregion

        /// <summary>
        /// prüfe mittels RegEx ob die E-Mail Adresse gültig ist
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        #region IsValidEmail
        public static bool IsValidEmail(string strIn)
       {
            try
            {
                return System.Text.RegularExpressions.Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"); 
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.Message );
                return false;
            }				
       }       
       #endregion
      
        /// <summary>
        /// Ermittelt aus einem String im Folgenden Format die Daten
        /// Keyword={Daten,Daten,Daten}
        /// </summary>
        /// <param name="sIn">Der Umzuwandelnde String</param>
        /// <param name="sKeyWord">ZURÜCK: Das Enthaltene Keyword</param>
        /// <param name="sData">Die Auflistung aller Daten, kann auch Leer sein</param>
        /// <returns>FALSE wenn ein Problem Auftritt</returns>
        #region MultiParser
        public static bool MultiParser(string sIn, out string sKeyWord, out string[]sSplitData )
        {
            sKeyWord = "";
            sSplitData = null;
            
            int		iStartPos	= 0;
            int		iEndPos		= 0;
            string	sDummy		= "";
            
            #region Extrahiere das Keyword aus dem gesamtstring (also zb. Hotkey= von Hotkey={CONTROL,ALT,F6} )
            try
            {
                iStartPos = sIn.IndexOf("=");			
                if ( iStartPos <0 ) { return false; }
                sKeyWord = sIn.Substring( 0, iStartPos );
            }
            catch(Exception ex)
            {
                Trace.WriteLine ( "{cStringHelper.MultiParser} EBENE A" );
                Trace.WriteLine ( ex.Message );
                Debug.Assert (false, "Fehler im Multiparser  EBENE A");
                return false;
            }
            #endregion
            
            #region Extrahiere die Nutzdaten zwischen { und }
            try
            {
                iStartPos = sIn.IndexOf("{");
                if ( iStartPos <0 ) { return false; }
                iEndPos  = sIn.IndexOf("}", iStartPos);
                if ( (iEndPos <=0) || ( iEndPos < iStartPos) || (iStartPos==iEndPos) ) { return false;}			
                sDummy = sIn.Substring( iStartPos+1, iEndPos-iStartPos-1);
            }
            catch(Exception ex)
            {
                Trace.WriteLine ( "{cStringHelper.MultiParser}  EBENE B" );
                Trace.WriteLine ( ex.Message );
                Debug.Assert (false, "Fehler im Multiparser  EBENE B");
                return false;
            }				
            #endregion
            
            #region Trenne Elemente mit SPLIT auf
            try
            {			 
                sSplitData = sDummy.Split( ',' ); 
                if ( sSplitData.GetUpperBound(0) <0 ) { return false; }
            }
            catch(Exception ex)
            {
                Trace.WriteLine ( "{cStringHelper.MultiParser}  EBENE C" );
                Trace.WriteLine ( ex.Message );
                Debug.Assert (false, "Fehler im Multiparser  EBENE C");
                return false;
            }				
            #endregion

            return true;
        }	  
       #endregion
       
       /// <summary>
       /// Überprüfe ob die Angabe Numerisch ist
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        #region IsNumeric
        public static bool IsNumeric(string input)
       {
            return System.Text.RegularExpressions.Regex.IsMatch(input, "^\\d+$");		  
       }   	   
       #endregion
      
        #region Suche Erste Zahl

        /// <summary>
        /// Durchlaufe den Angegebenen String uns liefere die Position des ersten Numerischen
        /// Werts zurück.
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        #region SucheErsteZahl
        public static int SucheErsteZahl(string inStr)
        {
            return SucheErsteZahl(inStr , 0);
        }
        #endregion

        /// <summary>
        /// Durchlaufe den Angegebenen String uns liefere die Position des ersten Numerischen
        /// Werts zurück. Komma, Punkt oder Minus werden ebenfalls als Zahl gewertet
        /// </summary>
        /// <param name="inStr"></param>
        /// <param name="iStartOffset"></param>
        /// <returns></returns>
        #region SucheErsteZahl - mit Startposition
        public static int SucheErsteZahl(string inStr, int iStartOffset)
        {
            int i = -1;
            int sPos = -1;

            try
            {

                /// ZAHL START: Suche die Startposition der ersten Zahl
                for (i = iStartOffset /*Starte bei dem !+ */ ; i < inStr.Length; i++)
                {
                    //Trace.WriteLine(inStr[i] + "<>" + Convert.ToInt16(inStr[i]));

                    if ((Convert.ToInt16(inStr[i]) >= 48 && Convert.ToInt16(inStr[i]) <= 57)
                            || inStr[i] == '.' || inStr[i] == ',' || inStr[i] == '-' ) { sPos = i; break; }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine ("Exception in 'SucheErsteZahl'");
                Trace.WriteLine (ex.Message );
                return -1;
            }
            
            if ( sPos < 0 ) { return -1; }
            return sPos;
        }
        #endregion

        #endregion

        /// <summary>
        /// Suche erste Position die keine Zahl ist.
        /// Minus, Punkt oder Komma werden auch als Zahl gewertet
        /// </summary>
        /// <param name="inStr"></param>
        /// <param name="iStartOffset"></param>
        /// <returns></returns>
        #region SucheErsteNichtZahl
        public static int SucheErsteNichtZahl(string inStr, int iStartOffset)
        {
            int i = -1;
            int ePos = -1;

            /// ZAHL ENDE: Suche die Erste nicht Zahl
            for ( i=iStartOffset; i < inStr.Length +1 ; i++)
            {
                //Trace.WriteLine ( inStr[i]  + "<>" + Convert.ToInt16(inStr[i]) );
                if ( i >= inStr.Length ) { ePos = inStr.Length; break; } // Zahlen gehen bis ans Ende  
                if ( (Convert.ToInt16(inStr[i]) < 48  ||  Convert.ToInt16(inStr[i]) > 57) && 
                        inStr[i] != '.' && inStr[i] != ',' && inStr[i] != '-' ) { ePos=i; break; }
            } 
            
            if ( ePos <0 ) { return -1; }
            //if ( (ePos == 0 && sPos == 0) || (ePos <= sPos) ) { return -1; } 

            return ePos;
        }
        #endregion

        /// <summary>
        /// Suche Einheit im String durch 1.Suche der Position mit den Zahlen,
        /// und das Ende der Zahlen. Danach wird das erste Zeichen das kein Space
        /// ist ermittelt. Dann noch am Ende das CRLF weggeschnitten
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        #region SucheEinheit
        public static string SucheEinheit(string inStr)
        {
            int i = -1;
            int ePos = -1;
            string sTmp = "";

            int iSucheStart = SucheErsteZahl(inStr);
            int iSucheErsteNichtZahhl = SucheErsteNichtZahl(inStr, iSucheStart);
            
            /// Suche Leerzeichen im String am Ende der Zahl
            for ( i=iSucheErsteNichtZahhl; i < inStr.Length +1 ; i++)
            {
                Trace.WriteLine ( inStr[i]  + "<>" + Convert.ToInt16(inStr[i]) );
                if ( i >= inStr.Length ) { ePos = inStr.Length; break; } // Zahlen gehen bis ans Ende  
                if ( (Convert.ToInt16(inStr[i]) != 32)) { ePos=i; break; }
            } 
            
            if ( ePos <0 ) { return ""; }

            if (ePos < inStr.Length && ePos > 0)
            {
                sTmp = inStr.Substring(ePos, (inStr.Length - ePos) );
                sTmp  = sTmp .Replace("\r\n", "").Trim();
            }

            return sTmp;
        }
        #endregion

       /// <summary>
       /// Wandle den String in einen Integer um. Es kann dabei Wahlweise eine Fehlermeldung ausgegeben werden
       /// oder ein Default Wert gesetzt werden.
       /// </summary>
       /// <param name="sInput">Der zu Konvertierende Parameter</param>
       /// <param name="sHinweisFieldName">Für eine Fehlermeldung zb. der Name des Feldes</param>
       /// <param name="bShowError">Soll ein Fehler angezeigt werden</param>
       /// <param name="iDefault">Ergebniswert bei Fehler, gleich ob Fehler gezeigt wird oder nicht</param>
       /// <returns></returns>
       public static int Get_Safe_Int_Convert(string sInput, string sHinweisFieldName, bool bShowError, int iDefault)
       {
            try
            {
                return Convert.ToInt32(sInput);
            }
            catch(Exception ex)
            {
                Trace.WriteLine ("{Get_Safe_Int_Convert} >" + ex.Message );
             
                if ( bShowError )
                {
                    System.Windows.Forms.MessageBox.Show ("Fehler bei der Konvertierung für das Feld [" + sHinweisFieldName + "]\r\n\r\n" + ex.Message ,
                                                                      "Convert String to Ganzzahl");
                }			 
                return iDefault;
          }
       }
        
        /// <summary>
       /// Wandle den String in eine long um. Es kann dabei Wahlweise eine Fehlermeldung ausgegeben werden
       /// oder ein Default Wert gesetzt werden.
       /// </summary>
       /// <param name="sInput">Der zu Konvertierende Parameter</param>
       /// <param name="sHinweisFieldName">Für eine Fehlermeldung zb. der Name des Feldes</param>
       /// <param name="bShowError">Soll ein Fehler angezeigt werden</param>
       /// <param name="iDefault">Ergebniswert bei Fehler, gleich ob Fehler gezeigt wird oder nicht</param>
       /// <returns></returns>
       public static long Get_Safe_Long_Convert(string sInput, string sHinweisFieldName, bool bShowError, long lDefault)
       {
            try
            {
                return Convert.ToInt32(sInput);
            }
            catch(Exception ex)
            {
                Trace.WriteLine ("{Get_Safe_Int_Convert} >" + ex.Message );
             
                if ( bShowError )
                {
                    System.Windows.Forms.MessageBox.Show ("Fehler bei der Konvertierung für das Feld [" + sHinweisFieldName + "]\r\n\r\n" + ex.Message ,
                                                                      "Convert String to Ganzzahl");
                }			 
                return lDefault;
          }
       }

public static double ConvertToDouble(string sIn)
{
    try
    {
        if (sIn == null)
        {
            return 0;
        }

        System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CurrentCulture;
        System.Globalization.NumberFormatInfo nf = ci.NumberFormat;

        return Convert.ToDouble(sIn.Replace(",", nf.NumberDecimalSeparator).Replace(".", nf.NumberDecimalSeparator));
    }
    catch (Exception ex)
    {
        SiAuto.LogException( "{ConvertToDouble} Exception" , ex);
        SiAuto.LogString("Aktueller Wert", sIn);
        return 0;
    }
}

        /// <summary>
        /// Die Funktion prüft ob der angeforderte String länger als der Original String ist
        /// und liefert in diesem Fall nur die verfügbaren Zeichen zurück
        /// </summary>
        /// <param name="sIn">Datenstring</param>
        /// <param name="iStart">Start Position der Substring Funktion</param>
        /// <param name="iLen">Länge der Werte ab Start Position</param>
        /// <param name="sFunktionsName">Name der Funktion für die Errormessage</param>
        /// <returns>Rückmeldung</returns>
        #region Get_Safe_SubString
         public static string Get_Safe_SubString(string sIn, int iStart, int iLen, string sFunktionsName)
        {
            try
            {
                // Wenn die Angeforderte Länge ab Startposition nicht verfügbar ist
                // wird nur soviel wie da ist geliefert
                if ( sIn.Length < ( iStart + iLen ) )
                {
                    return sIn.Substring(iStart);
                }
                else
                {
                    return sIn.Substring(iStart, iLen);
                }
            }
            catch ( Exception ex )
            {
                SiAuto.LogException("Get_Safe_SubString", ex);
                return "ERROR";
            }
        }
        #endregion


        /// <summary>
        /// Liefere den Substring wie angefordert zurück, auch ein PadR erfolgt 
        /// Wir die Angeforderte Länge überschritten, so wird der Original String für das Padding herangezogen
        /// </summary>
        /// <param name="sIn">Arbeitsstring</param>
        /// <param name="iStart">Startposition für das Substring</param>
        /// <param name="iLen">Länge für das Substring</param>
        /// <param name="iPadRLen">Nach dem Schneiden auf diese Anzahl Padden</param>
        /// <param name="sFunktionsName">Name der Funktion für die Errormessage</param>
        /// <returns></returns>
        #region Get_Safe_SubString_and_PadRight
        public static string Get_Safe_SubString_and_PadRight(string sIn, int iStart, int iLen, int iPadRLen, string sFunktionsName)
        {
            try
            {
                string sDummy = "";

                //Fehler 1 Abfangen, Startpos höher als Stringlänge
                //Fehler 2 Abfangen, Länge geht sich nicht aus 
                if ( (iStart > sIn.Length) ||
                     (iStart + iLen) > sIn.Length)
                {
                    if ((iStart + iLen) > sIn.Length)
                    {
                        sDummy = sIn.Substring(iStart);
                    }
                    else
                    {
                        sDummy = sIn;
                    }
                }
                else
                {
                    sDummy = sIn.Substring(iStart, iLen);
                }

                sDummy = sDummy.PadRight(iPadRLen);

                return sDummy;
            }
            catch (Exception ex)
            {
                SiAuto.LogException("Get_Safe_SubString_and_PadRight", ex);
                return "SUBSTRERROR";
            }
        }
        #endregion
        
          }


}
