using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Allgemein;

namespace Wollsdorf.Spaltwaage
{
    internal class cDruck_DruckerTest
    {
        private List<string> lstDruckerBuffer;
        private int iCountLines;
        private int iCountPage;
        private int iMaxLinesPerPage = 66;
        private char[] cEpson_FormFeed = { '\x0C' };
        private int iHeaderPrintedPos = -1;

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
