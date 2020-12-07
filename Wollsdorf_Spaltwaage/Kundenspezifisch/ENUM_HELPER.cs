using System;
using Wollsdorf_Spaltwaage.Allgemein.SQL;
using Wollsdorf_Spaltwaage.Kundenspezifisch.Gemeinsam.Settings;

namespace Wollsdorf_Spaltwaage.Kundenspezifisch
{
    internal class ENUM_HELPER
    {
        public static cData_Settings.eArbeitsplatztyp Arbeitsplatztyp_StringToEnum(string sWert)
        {
            cData_Settings.eArbeitsplatztyp eResult = cData_Settings.eArbeitsplatztyp.none;

            try
            {
                // YourEnum foo = (YourEnum) Enum.Parse(typeof(YourEnum), yourString);
                eResult = (cData_Settings.eArbeitsplatztyp)Enum.Parse(typeof(cData_Settings.eArbeitsplatztyp), sWert, true);
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
            }

            return eResult;
        }
        public static int Arbeitsplatztyp_EnumToInt(cData_Settings.eArbeitsplatztyp eWert)
        {
            int iReturn = 0;

            try
            {
                iReturn=(int)eWert;
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
            }

            return iReturn;
        }        
    }
}
