//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Wollsdorf.Spaltwaage.Data
//{
//    internal class cPalettenData
//    {
//        //public int iPalettenNr { get; private set; }
//        //public int iGesamtanzahl { get; set; }
//        //public double dGesamtgewicht { get; set; }
//        public double dLastNetto { get; set; }
//        //public double dMaxGewicht { get; set; }
//        //public bool bIsActive { get; set; }
//        //public string sBezeichnung { get; set; }
//        //public double dTara { get; set; }
//        public List<cData_Wiegung_Item> WiegeitemListe;

//        public cPalettenData()
//        {
//            //this.iGesamtanzahl = 0;
//            //this.dGesamtgewicht = 0;
//            //this.dLastNetto = 0;
//            //this.dMaxGewicht = 5000;
//            //this.bIsActive = false;
//            //this.sBezeichnung = "";
//            //this.WiegeitemListe = new List<cData_Wiegung_Item>();
//        }

//        public void UpdateAll(int anz, double gew, bool active, string bez)
//        {
//            this.iGesamtanzahl = anz;
//            this.dGesamtgewicht = gew;
//            this.bIsActive = active;
//            this.sBezeichnung = bez;
//        }

//        public void SetPalNr(int iPalNr) 
//        {
//            this.iPalettenNr = iPalNr;
//        }

//        public void AddWiegeItem(double gew, DateTime zeitpunkt)
//        {
//            this.WiegeitemListe.Add(new cData_Wiegung_Item());
//        }

//        public void RemoveLastWiegeItem() 
//        {
//            this.WiegeitemListe.Remove(this.WiegeitemListe.Last());
//        }

//        public bool IsVisible() 
//        {
//            return bIsActive;
//        }
//    }
//}
