using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Allgemein;
using Wollsdorf_Spaltwaage.Kundenspezifisch.Übernahmewaage.Data;

namespace Wollsdorf.Spaltwaage.Controls
{
    internal partial class ctrlPalette : UserControl
    {
        public cBeladungsDaten objBeladungsDaten;        
        private bool bSelektiert;
        private bool bLesemodus;        

        public ctrlPalette()
        {
            InitializeComponent();
            
            this.bSelektiert = false;
            this.objBeladungsDaten = null;
            this.bLesemodus = false;
            this.SetCtrlColor();
        }

        public void SetLesemodus(bool lesemodus) 
        {
            this.bLesemodus = lesemodus;
        }

        public bool IsSelektiert() 
        {
            return this.bSelektiert;
        }

        private void SetCtrlColor ()
        {
            this.BackColor = this.bSelektiert ? SETTINGS.colMerkmalSelektiert : SETTINGS.colButtonUnSel;
        }

        public void UpdateCtrl() 
        {
            //this.objPalettenItem.UpdateAll(anz, gew, active, bez);

            this.Visible = this.objBeladungsDaten.bSettings_IsActive;
            this.dispAnzahl.Text = this.objBeladungsDaten.iWiegung_Gesamtanzahl.ToString();
            this.dispGewicht.Text = this.objBeladungsDaten.dWiegung_Gesamtgewicht.ToString("#####0.0");
            this.dispPalettenBez.Text = this.objBeladungsDaten.sSettings_Bezeichnung;           
        }
        public void ResetButton()
        {
            this.bSelektiert = false;
            this.BackColor = SETTINGS.colButtonUnSel;
        }

        private void ctrlPalette_Click(object sender, EventArgs e)
        {
            if(!this.bLesemodus)
	        {
                this.bSelektiert = !this.bSelektiert;
                this.SetCtrlColor();
            }
        }
    }
}
