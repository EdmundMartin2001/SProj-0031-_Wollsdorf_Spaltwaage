namespace Wollsdorf.Spaltwaage
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using Allgemein;

    internal class cDruck_SeitenLangeTest
    {
        private List<string> lstDruckerBuffer;
        private int iCountLines;
        private int iCountPage;
        private int iMaxLinesPerPage = 66;
        private char[] cEpson_FormFeed = { '\x0C' };

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
                    this.PrintLine("Zeile " + i.ToString("000") + " Sonderzeichen: äöü ÄÖÜ ß \x0d\x0a");
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

                    if (this.SendTORS232(sSendTxt))
                    {
                        //Seitenwechsel
                        cGlobalScale.objRS232_X5.SendString(new string(this.cEpson_FormFeed));

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

        private void PrintLine(string DruckeText)
        {
            this.lstDruckerBuffer.Add(DruckeText);
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
