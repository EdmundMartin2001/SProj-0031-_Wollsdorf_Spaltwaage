using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SMT_SQL_2V.DB.Private;
using Wollsdorf;
using Allgemein;
using Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Data;

namespace Wollsdorf.Spaltwaage
{
    internal class cWiegung_Handling
    {
        public static bool Save_Palette(Controls.ctrlPalette PalettenUserControl)
        {
            bool bRet = false;
            cDB_SQL_CE qry = null;

            try
            {
                qry = new SMT_SQL_2V.DB.Private.cDB_SQL_CE(SMT_SQL_2V.DB.cDB_Settings.CE_ConnectionString);
                qry.ADD("Wiegung_Sum_Kg", PalettenUserControl.objBeladungsDaten.dWiegung_Gesamtgewicht);
                qry.ADD("Wiegung_Sum_Stk", PalettenUserControl.objBeladungsDaten.iWiegung_Gesamtanzahl);
                qry.UPDATE_THROW("SMT_WIEGUNG", "Wiegung_PalNr", PalettenUserControl.objBeladungsDaten.iPalettenNr, "Save_Palette", 99);
            }
            catch (Exception ex)
            {
                SiAuto.LogException("cWiegung_Handling.Save_Palette", ex);
                throw;
            }
            finally
            {
                if (qry != null)
                {
                    qry.FREE();
                }
            }

            return bRet;
        }

        public static bool Load_Settings(ref cBeladungsDaten BeladungsDaten)
        {
            cDB_SQL_CE qry = null;
            bool bRet = false;

            try
            {
                qry = new SMT_SQL_2V.DB.Private.cDB_SQL_CE(SMT_SQL_2V.DB.cDB_Settings.CE_ConnectionString);

                if (qry.OPEN("SELECT * from [SMT_WIEGUNG] Where Wiegung_PalNr = '" + BeladungsDaten.iPalettenNr.ToString() + "'"))
                {
                    BeladungsDaten.dWiegung_Gesamtgewicht = qry.getF("Wiegung_Sum_Kg");
                    BeladungsDaten.iWiegung_Gesamtanzahl = qry.getI("Wiegung_Sum_Stk");
                    bRet = true;
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException("cWiegung_Handling.Load_Settings", ex);
                throw;
            }
            finally
            {
                if (qry != null)
                {
                    qry.FREE();
                }
            }

            return bRet;
        }      

        private static void CreateKey(string sName)
        {
            cDB_SQL_CE qry = null;

            try
            {
                qry = new SMT_SQL_2V.DB.Private.cDB_SQL_CE(SMT_SQL_2V.DB.cDB_Settings.CE_ConnectionString);
                qry.ADD("Reihenfolge", 0);
                qry.ADD("Fieldname", sName);
                qry.ADD("Value_Type", "TX");
                qry.ADD("Field_Value", "0");
                qry.INSERT_THROW("SMT_WIEGUNG", false, "", 1);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (qry != null)
                {
                    qry.FREE();
                }
            }
        }
        private static bool SetUpToBool(string sValue)
        {
            if (sValue.Equals("1") ||
                 sValue.ToUpper().Equals("TRUE") ||
                sValue.ToUpper().Equals("Y"))
            {
                return true;
            }

            return false;
        }
        private static double SetUpToDouble(string sValue)
        {
            System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CurrentCulture;
            System.Globalization.NumberFormatInfo nf = ci.NumberFormat;

            return Convert.ToDouble(sValue.Replace(",", nf.NumberDecimalSeparator).Replace(".", nf.NumberDecimalSeparator));
        }
        private static int SetUpToInt(string sValue)
        {
            return Convert.ToInt32(sValue);
        }      
    }
}
