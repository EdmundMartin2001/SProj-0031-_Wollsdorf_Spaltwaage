using System;
using Wollsdorf_Spaltwaage.Allgemein.SQL;

namespace Allgemein
{
    internal class cGlobalNummerkreis
    {
        public static int Nummernkreis1_GetNext()
        {
            string sSQL = "SELECT Field_Value FROM [SMT_SETTINGS] WHERE " +
                          "FieldName = 'Nummernkreis1'";

            cDB_SQL_CE qry = null;

            int iRet = 0;
            try
            {
                qry = new cDB_SQL_CE(cDB_Settings.CE_ConnectionString);

                if (qry.OPEN(sSQL))
                {
                    iRet = Convert.ToInt32(qry.getS("Field_Value"));
                    iRet++;
                }
                else
                {
                    iRet = 1;
                }

                if (iRet > 32000)
                {
                    iRet = 1;
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
            }
            finally
            {
                if (qry != null)
                {
                    qry.FREE();
                }
            }

            return iRet;
        }
        public static int Nummernkreis1_GetCurrent()
        {
            string sSQL = "SELECT Field_Value FROM [SMT_SETTINGS] WHERE " +
                          "FieldName = 'Nummernkreis1'";

            cDB_SQL_CE qry = null;

            int iRet = 0;
            try
            {
                qry = new cDB_SQL_CE(cDB_Settings.CE_ConnectionString);

                if (qry.OPEN(sSQL))
                {
                    iRet = Convert.ToInt32(qry.getS("Field_Value"));
                }
                else
                {
                    iRet = 1;
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
            }
            finally
            {
                if (qry != null)
                {
                    qry.FREE();
                }
            }

            return iRet;
        }
        public static void Nummernkreis1_Set(int FortNr)
        {
            cDB_SQL_CE qry = null;
            string sSQL = "";

            try
            {
                sSQL = "UPDATE [SMT_SETTINGS] SET Field_Value = '" + FortNr.ToString() + "' Where FieldName = 'Nummernkreis1'";
                qry = new cDB_SQL_CE(cDB_Settings.CE_ConnectionString);
                
                qry.Exec(sSQL);
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
            }
            finally
            {
                if (qry != null)
                {
                    qry.FREE();
                }
            }
        }
        public static void Nummernkreis1_SetNext()
        {
            try
            {
                // Aktuellen Zähler lesen
                int iNext = Nummernkreis1_GetNext();

                Nummernkreis1_Set(iNext);
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
            }
        }
    }
}
