namespace Wollsdorf.Data
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;

    internal class cData_Settings
    {
        public enum eArbeitsplatztyp
        {
            none = 0,
            Übernahmewaage = 1,
            Salzwaage = 2
        }

        public string sArbeitsplatzname { get; set; }

        public string sServicePasswort { get; set; }
        public eArbeitsplatztyp Arbeitsplatztyp { get; set; }
        public bool bDioausgänge { get; set; }
        public bool bDioeingänge { get; set; }
        private string sCSVPath;
        public int iZeilenProSeite { get; set; }
        public bool bServiceMode { get; set; }
        public double dPalettenTara { get; set; }
        
        public double dEinzelWiegungMinGewicht { get; set; }
        public double dEinzelWiegungMaxGewicht { get; set; }

        public double dPalettenMax { get; set; }

        public string sGruppenName1 { get; set; }
        public string sGruppenName2 { get; set; }
        public string[] KlassenBezeichnungsListe = new string[6];
        public string[] ArtikelNrListe = new string[6];

        public cData_Settings() 
        {
            this.resetAll();
        }

        public void resetAll() 
        {

            this.KlassenBezeichnungsListe[0] = "STIERE 6/8 KG (bulls 6/8kg)";
            this.KlassenBezeichnungsListe[1] = "STIERE 9/10 KG (bulls 9/10kg)";
            this.KlassenBezeichnungsListe[2] = "III. Sort.7/9kg  (III.Inters 7/9kg)";
            this.KlassenBezeichnungsListe[3] = "Reserve";
            this.KlassenBezeichnungsListe[4] = "Reserve";
            this.KlassenBezeichnungsListe[5] = "Reserve";
            this.ArtikelNrListe[0] = "10300002";
            this.ArtikelNrListe[1] = "10300001";
            this.ArtikelNrListe[2] = "10300005";
            this.ArtikelNrListe[3] = "";
            this.ArtikelNrListe[4] = "";
            this.ArtikelNrListe[5] = "";
            this.sGruppenName1 = "WB-SPALTCROUPONS";
            this.sGruppenName2 = "(WB-Split Double Butts)";
            this.sArbeitsplatzname = "";
            this.sServicePasswort = "";
            this.Arbeitsplatztyp = eArbeitsplatztyp.none;
            this.dEinzelWiegungMinGewicht = 0;
            this.dEinzelWiegungMaxGewicht = 500;
            this.dPalettenTara = 0;
            this.dPalettenMax = 3000;
            this.bDioausgänge = false;
            this.bDioeingänge = false;
            this.sCSVPath = "";
            this.iZeilenProSeite = 0;
            this.bServiceMode = false;
        }

        public string get_ArbeitsplatzName
        {
            get
            {
                return this.sArbeitsplatzname + " - " +
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            }
        }

        public string Set_ArbeitsplatzTyp
        {
            set
            {
                try
                {
                    this.Arbeitsplatztyp = ENUM_HELPER.Arbeitsplatztyp_StringToEnum(value);
                }
                catch (Exception ex)
                {
                    Allgemein.SiAuto.LogException(ex);
                }
            }
        }

        public string CSVPath
        {
            get
            {
                if (bServiceMode)
                {
                    return @"\\HP1\C\TEMP\Wollsdorf";
                }
                else
                {
                    return sCSVPath;
                }
            }
            set
            {
                sCSVPath = value;
            }            
        }
    }
}
