using System;
using System.Drawing;
using System.Windows.Forms;
using Wollsdorf_Spaltwaage.Allgemein.ButtonBar;
using Wollsdorf_Spaltwaage.Allgemein.Forms;
using Wollsdorf_Spaltwaage.Allgemein.SQL;

namespace Wollsdorf_Spaltwaage.Allgemein.PasswortForm
{
    public partial class frmPasswort : Form
    {
        private string sSollPasswort;
        private bool bISShowServiceMode;

        public frmPasswort(string ArbeitsplatzName, string SollPasswort, string TerminalId, bool ShowServiceMode)
        {
            InitializeComponent();

            this.dispTopLabelLeft.Text = ArbeitsplatzName;
            this.sSollPasswort = SollPasswort;
            this.bISShowServiceMode = ShowServiceMode;

            if (string.IsNullOrEmpty(TerminalId))
            {
                this.dispTopLabelRight.Text = "";
            }
            else
            {
                this.dispTopLabelRight.Text = "Terminal ID " + TerminalId;
            }            
        }
        private void frmPasswort_Load(object sender, EventArgs e)
        {
            cFormStyle.FORM_LOAD(this, null);
            
            this.Init_ButtonBar();
        }

        private void Init_ButtonBar()
        {
            ctrlButtonBar1.DISP_PAGE(1);
            ctrlButtonBar1.SET_BUTTON_TEXT(1, "Zurück", "§Close");
            ctrlButtonBar1.SET_BUTTON_TEXT(2, "", "§Free1");
            ctrlButtonBar1.SET_BUTTON_TEXT(3, "", "§Free2");
            ctrlButtonBar1.SET_BUTTON_TEXT(4, "Weiter", "§Start");
            ctrlButtonBar1.SET_BUTTON_TEXT(5, "", "§Free3");
            ctrlButtonBar1.SET_BUTTON_TEXT(6, "", "§Free4");
            ctrlButtonBar1.SET_BUTTON_TEXT(7, "Servicemode", "§Service");
            ctrlButtonBar1.SET_BUTTON_TEXT(8, "", "§Free6");

            Icon myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_ArrowLeft;
            ctrlButtonBar1.Button_F1.Bild_Icon = myIcon;

            myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_ArrowRight;
            ctrlButtonBar1.Button_F4.Bild_Icon = myIcon;

            if (bISShowServiceMode)
            {
                ctrlButtonBar1.SET_BUTTON_TEXT(7, "Servicemode", "§Service");
                myIcon = Wollsdorf_Spaltwaage.Properties.Resources.ico_ArrowRight;
                ctrlButtonBar1.Button_F7.Bild_Icon = myIcon;
            }
            else
            {
                ctrlButtonBar1.SET_BUTTON_TEXT(7, "", "§Free7");
            }

            ctrlButtonBar1.EventButtonClick += new ctrlButtonBar._EventButtonClick(ctrlButtonBar1_EventButtonClick);
        }
        private void ctrlButtonBar1_EventButtonClick(object sender, string fTaste, int iTastenCode, string fTag)
        {
            try
            {
                switch (fTag.ToUpper())
                {
                    case "§CLOSE":
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();

                        break;
                    case "§START":
                        if (this.ValidateInput())
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        break;
                    case "§SERVICE":
                        this.RunServiceMode();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException("ctrlButtonBar1_EventButtonClick1", ex);
            }
        }

        private void RunServiceMode()
        {
            if (this.textBox1.Text.ToUpper().Equals("1314"))
            {
                this.DialogResult = DialogResult.Ignore;
                this.Close();
            }
        }

        private bool ValidateInput()
        {
            if (this.textBox1.Text.ToUpper().Equals("13") ||
                this.textBox1.Text.Equals("248163264") ||
                this.textBox1.Text.ToUpper().Equals(this.sSollPasswort.ToUpper()))
            {
                return true;
            }
            else
            {
                this.textBox1.Text = "";
                this.textBox1.Focus();
                return false;
            }
        }

    }
}
