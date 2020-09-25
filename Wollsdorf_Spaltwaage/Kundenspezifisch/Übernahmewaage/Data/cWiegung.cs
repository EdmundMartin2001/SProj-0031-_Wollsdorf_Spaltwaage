using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wollsdorf.Spaltwaage
{
    internal class cWiegung
    {
        public Wollsdorf.Data.cData_Settings objSettings;
        public long lBestellnummer { get; set; }
        public int iLieferantennummer { get; set; }
        
        public cWiegung()
        {
            this.objSettings = new Wollsdorf.Data.cData_Settings();
        }
    }
}
