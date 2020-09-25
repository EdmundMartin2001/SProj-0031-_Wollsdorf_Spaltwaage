namespace Wollsdorf
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using SMT_SQL_2V.DB.Private;
    using Allgemein;

    internal class cData_Settings_Handling
    {
        public static bool Save_Settings(Data.cData_Settings Data_Settings)
        {
            bool bRet = false;
            cDB_SQL_CE qry = null;

            try
            {
                qry = new SMT_SQL_2V.DB.Private.cDB_SQL_CE(SMT_SQL_2V.DB.cDB_Settings.CE_ConnectionString);

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + ENUM_HELPER.Arbeitsplatztyp_EnumToInt(Data_Settings.Arbeitsplatztyp).ToString() + "' " +
                           "Where FieldName = 'Arbeitsplatztyp'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.bDioausgänge.ToString() + "' " +
                           "Where FieldName = 'Dioausgänge'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.bDioeingänge.ToString() + "' " +
                           "Where FieldName = 'Dioeingänge'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.dEinzelWiegungMinGewicht.ToString() + "' " +
                           "Where FieldName = 'PaletteMinGew'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.dEinzelWiegungMaxGewicht.ToString() + "' " +
                           "Where FieldName = 'PaletteMaxGew'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.dPalettenTara.ToString() + "' " +
                           "Where FieldName = 'PaletteTara'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.dPalettenMax.ToString() + "' " +
                           "Where FieldName = 'PalettenMax'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.sArbeitsplatzname.ToString() + "' " +
                           "Where FieldName = 'Arbeitsplatzname'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.CSVPath.ToString() + "' " +
                           "Where FieldName = 'CSVPath'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.sServicePasswort.ToString() + "' " +
                           "Where FieldName = 'ServicePasswort'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.iZeilenProSeite.ToString() + "' " +
                           "Where FieldName = 'ZeilenProA4Seite'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.ArtikelNrListe[0].ToString() + "' " +
                           "Where FieldName = 'ArtikelNr1'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.ArtikelNrListe[1].ToString() + "' " +
                           "Where FieldName = 'ArtikelNr2'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.ArtikelNrListe[2].ToString() + "' " +
                           "Where FieldName = 'ArtikelNr3'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.ArtikelNrListe[3].ToString() + "' " +
                           "Where FieldName = 'ArtikelNr4'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.ArtikelNrListe[4].ToString() + "' " +
                           "Where FieldName = 'ArtikelNr5'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.ArtikelNrListe[5].ToString() + "' " +
                           "Where FieldName = 'ArtikelNr6'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.KlassenBezeichnungsListe[0].ToString() + "' " +
                           "Where FieldName = 'PalettenName1'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.KlassenBezeichnungsListe[1].ToString() + "' " +
                           "Where FieldName = 'PalettenName2'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.KlassenBezeichnungsListe[2].ToString() + "' " +
                           "Where FieldName = 'PalettenName3'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.KlassenBezeichnungsListe[3].ToString() + "' " +
                           "Where FieldName = 'PalettenName4'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.KlassenBezeichnungsListe[4].ToString() + "' " +
                           "Where FieldName = 'PalettenName5'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.KlassenBezeichnungsListe[5].ToString() + "' " +
                           "Where FieldName = 'PalettenName6'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.sGruppenName1.ToString() + "' " +
                           "Where FieldName = 'GruppenName1'");

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + Data_Settings.sGruppenName2.ToString() + "' " +
                           "Where FieldName = 'GruppenName2'");

            }
            catch (Exception ex)
            {
                SiAuto.LogException("cData_Settings_Handling.Save_Settings", ex);
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

        public static bool Load_Settings(Data.cData_Settings Data_Settings)
        {
            bool bRet = false;
            cDB_SQL_CE qry = null;

            try
            {
                qry = new SMT_SQL_2V.DB.Private.cDB_SQL_CE(SMT_SQL_2V.DB.cDB_Settings.CE_ConnectionString);

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'Arbeitsplatztyp'"))
                {
                    Data_Settings.Set_ArbeitsplatzTyp = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'Dioausgänge'"))
                {
                    Data_Settings.bDioausgänge = SetUpToBool(qry.getS("Field_Value"));
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'Dioeingänge'"))
                {
                    Data_Settings.bDioeingänge = SetUpToBool(qry.getS("Field_Value"));
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'PalettenMinGew'"))
                {
                    Data_Settings.dEinzelWiegungMinGewicht = SetUpToDouble(qry.getS("Field_Value"));
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'PalettenMaxGew'"))
                {
                    Data_Settings.dEinzelWiegungMaxGewicht = SetUpToDouble(qry.getS("Field_Value"));
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'PalettenTara'"))
                {
                    Data_Settings.dPalettenTara = SetUpToDouble(qry.getS("Field_Value"));
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'PalettenMax'"))
                {
                    Data_Settings.dPalettenMax = SetUpToDouble(qry.getS("Field_Value"));
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'Arbeitsplatzname'"))
                {
                    Data_Settings.sArbeitsplatzname = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'CSVPath'"))
                {
                    Data_Settings.CSVPath = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value FROM [SMT_SETTINGS] WHERE FieldName = 'ServicePasswort'"))
                {
                    Data_Settings.sServicePasswort = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value FROM [SMT_SETTINGS] WHERE FieldName = 'ZeilenProA4Seite'"))
                {
                    Data_Settings.iZeilenProSeite = SetUpToInt(qry.getS("Field_Value"));
                }

                if (qry.OPEN("SELECT Field_Value FROM [SMT_SETTINGS] WHERE FieldName = 'ServiceMode'"))
                {
                    Data_Settings.bServiceMode = SetUpToBool(qry.getS("Field_Value"));
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'ArtikelNr1'"))
                {
                    Data_Settings.ArtikelNrListe[0] = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'ArtikelNr2'"))
                {
                    Data_Settings.ArtikelNrListe[1] = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'ArtikelNr3'"))
                {
                    Data_Settings.ArtikelNrListe[2] = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'ArtikelNr4'"))
                {
                    Data_Settings.ArtikelNrListe[3] = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'ArtikelNr5'"))
                {
                    Data_Settings.ArtikelNrListe[4] = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'ArtikelNr6'"))
                {
                    Data_Settings.ArtikelNrListe[5] = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'PalettenName1'"))
                {
                    Data_Settings.KlassenBezeichnungsListe[0] = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'PalettenName2'"))
                {
                    Data_Settings.KlassenBezeichnungsListe[1] = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'PalettenName3'"))
                {
                    Data_Settings.KlassenBezeichnungsListe[2] = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'PalettenName4'"))
                {
                    Data_Settings.KlassenBezeichnungsListe[3] = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'PalettenName5'"))
                {
                    Data_Settings.KlassenBezeichnungsListe[4] = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'PalettenName6'"))
                {
                    Data_Settings.KlassenBezeichnungsListe[5] = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'GruppenName1'"))
                {
                    Data_Settings.sGruppenName1 = qry.getS("Field_Value");
                }

                if (qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'GruppenName2'"))
                {
                    Data_Settings.sGruppenName2 = qry.getS("Field_Value");
                }

            }
            catch (Exception ex)
            {
                SiAuto.LogException("cData_Settings_Handling", ex);
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

        /// <summary>
        /// Erstinitialisierung im Servicemode
        /// </summary>
        public static void Settings_Init()
        {            
            cDB_SQL_CE qry = null;

            try
            {
                //qry = new SMT_SQL_2V.DB.Private.cDB_SQL(SMT_SQL_2V.DB.cDB_Settings.CE_ConnectionString);

                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'Arbeitsplatztyp'") < 1)
                {
                    CreateKey("Arbeitsplatztyp");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'Dioausgänge'") < 1)
                {
                    CreateKey("Dioausgänge");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'Dioeingänge'") < 1)
                {
                    CreateKey("Dioeingänge");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'MinGewSalzwaage'") < 1)
                {
                    CreateKey("MinGewSalzwaage");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'Arbeitsplatzname'") < 1)
                {
                    CreateKey("Arbeitsplatzname");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'CSVPath'") < 1)
                {
                    CreateKey("CSVPath");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'ZeilenProA4Seite'") < 1)
                {
                    CreateKey("ZeilenProA4Seite");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'PalettenMax'") < 1)
                {
                    CreateKey("PalettenMax");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'GruppenName1'") < 1)
                {
                    CreateKey("GruppenName1");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'GruppenName2'") < 1)
                {
                    CreateKey("GruppenName2");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'ArtikelNr1'") < 1)
                {
                    CreateKey("ArtikelNr1");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'ArtikelNr2'") < 1)
                {
                    CreateKey("ArtikelNr2");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'ArtikelNr3'") < 1)
                {
                    CreateKey("ArtikelNr3");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'ArtikelNr4'") < 1)
                {
                    CreateKey("ArtikelNr4");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'ArtikelNr5'") < 1)
                {
                    CreateKey("ArtikelNr5");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'ArtikelNr6'") < 1)
                {
                    CreateKey("ArtikelNr6");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'PalettenName1'") < 1)
                {
                    CreateKey("PalettenName1");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'PalettenName2'") < 1)
                {
                    CreateKey("PalettenName2");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'PalettenName3'") < 1)
                {
                    CreateKey("PalettenName3");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'PalettenName4'") < 1)
                {
                    CreateKey("PalettenName4");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'PalettenName5'") < 1)
                {
                    CreateKey("PalettenName5");
                }
                if (cDB_SQL_CE.getInteger("SELECT count(*) from [SMT_SETTINGS] Where FieldName = 'PalettenName6'") < 1)
                {
                    CreateKey("PalettenName6");
                }
                
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

        private static void CreateKey(string sName)
        {
               cDB_SQL_CE qry = null;

               try
               {
                   qry = new SMT_SQL_2V.DB.Private.cDB_SQL_CE(SMT_SQL_2V.DB.cDB_Settings.CE_ConnectionString);
                   qry.ADD("Reihenfolge", 0);
                   qry.ADD("Fieldname",sName );
                   qry.ADD("Value_Type", "TX" );
                   qry.ADD("Field_Value", "0");
                   qry.INSERT_THROW("SMT_SETTINGS", false, "",1);
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
        public static void SetSMTServiceMode(Data.cData_Settings Data_Settings)
        {
            cDB_SQL_CE qry = null;
            bool bServiceMode = false;

            try
            {
                qry = new SMT_SQL_2V.DB.Private.cDB_SQL_CE(SMT_SQL_2V.DB.cDB_Settings.CE_ConnectionString);

                if (!qry.OPEN("SELECT Field_Value from [SMT_SETTINGS] Where FieldName = 'ServiceMode'"))
                {
                    CreateKey("ServiceMode");
                }
                else
                {
                    bServiceMode = SetUpToBool(qry.getS("Field_Value"));
                }

                bServiceMode = !bServiceMode;
                Data_Settings.bServiceMode = bServiceMode;

                qry.Exec("UPDATE [SMT_SETTINGS] SET Field_Value = '" + (bServiceMode ? "1" : "0") + "' " +
                         "Where FieldName = 'ServiceMode'");

                cGlobalHandling.MessageBox("Servicemode = " + (bServiceMode ? "EIN" : "AUS"), "Servicemode");
            }
            catch (Exception ex)
            {
                SiAuto.LogException("SetSMTServiceMode", ex);
                throw;
            }
            finally
            {
                if (qry != null)
                {
                    qry.FREE();
                }
            }
        }
    }

}
