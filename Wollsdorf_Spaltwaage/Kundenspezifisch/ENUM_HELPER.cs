namespace Wollsdorf
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using Wollsdorf.Spaltwaage;

    internal class ENUM_HELPER
    {
        public static Data.cData_Settings.eArbeitsplatztyp Arbeitsplatztyp_StringToEnum(string sWert)
        {
            Data.cData_Settings.eArbeitsplatztyp eResult = Data.cData_Settings.eArbeitsplatztyp.none;

            try
            {
                // YourEnum foo = (YourEnum) Enum.Parse(typeof(YourEnum), yourString);
                eResult = (Data.cData_Settings.eArbeitsplatztyp)Enum.Parse(typeof(Data.cData_Settings.eArbeitsplatztyp), sWert, true);
            }
            catch (Exception ex)
            {
                Allgemein.SiAuto.LogException(ex);
            }

            return eResult;
        }
        public static int Arbeitsplatztyp_EnumToInt(Data.cData_Settings.eArbeitsplatztyp eWert)
        {
            int iReturn = 0;

            try
            {
                iReturn=(int)eWert;
            }
            catch (Exception ex)
            {
                Allgemein.SiAuto.LogException(ex);
            }

            return iReturn;
        }        
    }
}
