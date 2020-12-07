namespace Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Data
{
    internal class cBeladungsDaten
    {
        public int iPalettenNr { get; private set; }

        public double dSettings_MaxGewicht { get; set; }
        public bool bSettings_IsActive { get; set; }
        public string sSettings_Bezeichnung { get; set; }
        public double dSettings_PalettenTara { get; set; }
        public string sSettings_ArtikelNr { get; set; }

        public int iWiegung_Gesamtanzahl { get; set; }
        public double dWiegung_Gesamtgewicht { get; set; }
        
        /// <summary>
        /// Wird für das Storno benötigt
        /// </summary>
        public double dWiegung_LastNetto { get; set; }

        public cBeladungsDaten(
            int PalettenNr, 
            double SettingsMaxPalGewicht, 
            bool bISActivePal, 
            string sBezeichnung,
            double dPalettenTara,
            string sArtikelNr)
        {
            this.iPalettenNr = PalettenNr;
            this.dSettings_MaxGewicht = SettingsMaxPalGewicht;
            this.bSettings_IsActive = bISActivePal;
            this.sSettings_Bezeichnung = sBezeichnung;
            this.dSettings_PalettenTara = dPalettenTara;
            this.sSettings_ArtikelNr = sArtikelNr;

            this.iWiegung_Gesamtanzahl = 0;
            this.dWiegung_Gesamtgewicht = 0;
            this.dWiegung_LastNetto = 0;
        }
    }
}
