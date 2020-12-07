using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Allgemein;
using Wollsdorf_Spaltwaage.Allgemein;
using Wollsdorf_Spaltwaage.Allgemein.PasswortForm;
using Wollsdorf_Spaltwaage.Allgemein.ScaleEngine;
using Wollsdorf_Spaltwaage.Allgemein.SQL;

namespace Wollsdorf_Spaltwaage.Kundenspezifisch
{
    internal class cGlobalHandling
    {

        [DllImport("coredll.dll", SetLastError = true)]
        static extern int SetSystemPowerState(string psState, int StateFlags, int Options);
        [DllImport("Coredll")]
        internal static extern IntPtr FindWindow(String lpClassName, String lpWindowName);
        [DllImport("coredll.dll", SetLastError = true)]
        static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        const int POWER_FORCE = 4096;
        const int POWER_STATE_RESET = 0x00800000;
        public const int SW_SHOW = 5;
        public const int SW_HIDE = 0;

        public static void CenterForm(Form theForm, int TopOffset)
        {
            theForm.Location = new System.Drawing.Point(
                Screen.PrimaryScreen.WorkingArea.Width / 2 - theForm.Width / 2,
                (Screen.PrimaryScreen.WorkingArea.Height / 2 - theForm.Height / 2) + TopOffset);
        }
        public static void MessageBox(string MessageText, string Caption)
        {
            global::Allgemein.frmInfoDlg f = new frmInfoDlg(MessageText);
            f.Text = Caption;
            f.ShowDialog();
        }
        public static DialogResult MessageBoxYesNo(string MessageText, string Caption)
        {
            frmYesNo f = new frmYesNo(MessageText);
            f.Text = Caption;
            return f.ShowDialog();
        }
        public static DialogResult MessageBoxYesNoSicher(string MessageText, string Caption)
        {
            frmYesNoSicher f = new frmYesNoSicher(MessageText);
            f.Text = Caption;
            return f.ShowDialog();
        }
        public static void rebootDevice()
        {
            SetSystemPowerState(null, POWER_STATE_RESET, POWER_FORCE);
        }
        public static void HideShowScaleWindow(bool bShow)
        {
            IntPtr iptrTB = FindWindow(null, "IND890-10 Wiegeapplikation");

            if (iptrTB == IntPtr.Zero )
            {
                iptrTB = FindWindow(null, "IND890-10 Weighing Application");

                if (iptrTB == IntPtr.Zero) 
                {
                    MessageBox("Window = null","");
                    return;
                }
            }

            if (!bShow)
            {
                //It's minimized
                ShowWindow(iptrTB, SW_HIDE);
                //  SetForegroundWindow(iptrTB);
            }
            else
            {
                ShowWindow(iptrTB, SW_SHOW);
            }
        }

        /// <summary>
        /// Aus Performace Gründen sollte das Scale Fenster nur ausgeblendet werden,
        /// Wenn es nicht danach wieder benötigt wird
        /// Damit soll ein dauerndes unnötiges Ein und Ausblenden verhindert werden.
        /// </summary>
        /// <param name="ArbeitsplatzName"></param>
        /// <param name="bHideScaleWindow"></param>
        /// <returns></returns>
        public static DialogResult Frage_Passwort(
            string ArbeitsplatzName,
            bool bHideScaleWindow,
            string SollPasswort,
            string TerminalID,
            bool ShowServiceMode)
        {
            if (bHideScaleWindow)
            {
                cGlobalScale.Hide_Scale();
            }

            frmPasswort f = new frmPasswort(ArbeitsplatzName, SollPasswort, TerminalID, ShowServiceMode);
            DialogResult dlgRes = f.ShowDialog();
            f.Dispose();
            f = null;

            if (bHideScaleWindow)
            {
                cGlobalScale.Show_Scale();
            }

            return dlgRes;
        }
        public static bool IsBetween(int val, int low, int high)
        {
            return val >= low && val <= high;
        }
        public static bool IsBetween(double val, double low, double high)
        {
            return val >= low && val <= high;
        }
        public static bool IsNumeric(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, "^\\d+$");
        }
        public static double TextboxToDouble(TextBox tb)
        {
            double dResult = -1;
            
            try
            {
                System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CurrentCulture;
                System.Globalization.NumberFormatInfo nf = ci.NumberFormat;

                dResult =Convert.ToDouble(tb.Text.Replace(",", nf.NumberDecimalSeparator).Replace(".", nf.NumberDecimalSeparator));
                //dResult = Convert.ToDouble(tb.Text);
            }
            catch (Exception) { }

            return dResult;
        }
        public static int TextboxToInt(TextBox tb)
        {
            int iResult = -1;

            try
            {
                iResult = Convert.ToInt32(tb.Text.Trim());
            }
            catch (Exception) { }

            return iResult;
        }
        public static long TextboxToLong(TextBox tb)
        {
            long lResult = -1;

            try
            {
                lResult =  Convert.ToInt64(tb.Text.Trim());
            }
            catch (Exception) { }

            return lResult;
        }
  
        private static void SendTORS232(string SendTxt)
        {
            try
            {
                Trace.Write(SendTxt);

                byte[] encodingBytes = System.Text.Encoding.Default.GetBytes(SendTxt);
                cGlobalScale.objRS232_X5.SendBytes(encodingBytes, encodingBytes.Length);
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
            }
        }

        public static void Drucke_Daten(string sSendTxt)
        {
            string sPrintBuf = "";
            int iChar = 0;

            try
            {
                for (int iCopies = 1; iCopies <= 1/*this.iAnzahlDruckjobs*/; iCopies++)
                {
                    iChar = 0;
                    sPrintBuf = "";


                    foreach (char c in sSendTxt)
                    {
                        iChar++;
                        sPrintBuf += c;

                        if (iChar > 50)
                        {
                            SendTORS232(sPrintBuf);
                            sPrintBuf = "";
                            iChar = 0;
                        }
                    }

                    if (sPrintBuf.Length > 0)
                    {
                        // Ausdruck wird durchgeführt
                        SendTORS232(sPrintBuf);
                    }
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
            }
        }
    
    }
}
