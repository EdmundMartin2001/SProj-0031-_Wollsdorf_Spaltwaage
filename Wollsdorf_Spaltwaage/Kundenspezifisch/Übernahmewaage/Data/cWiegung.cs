using Wollsdorf_Spaltwaage.Kundenspezifisch.Gemeinsam.Settings;

namespace Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Data
{
    internal class cWiegung
    {
        public cData_Settings objSettings;
        public long lBestellnummer { get; set; }
        public int iLieferantennummer { get; set; }
        
        public cWiegung()
        {
            this.objSettings = new cData_Settings();
        }
    }
}
