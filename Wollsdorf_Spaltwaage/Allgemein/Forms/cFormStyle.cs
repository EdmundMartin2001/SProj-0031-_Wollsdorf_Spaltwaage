using System.Drawing;
using System.Windows.Forms;

namespace Wollsdorf_Spaltwaage.Allgemein.Forms
{
    internal class cFormStyle
    {
        public static void FORM_LOAD(System.Windows.Forms.Form F, Panel ScalePanel)
        {
            F.FormBorderStyle = FormBorderStyle.None;
            F.AutoScroll = false;
            F.Text = "";
            F.MinimizeBox = false;
            F.MaximizeBox = false;
            F.ControlBox = false;
            F.WindowState = FormWindowState.Maximized;

            if (ScalePanel != null)
            {
                ScalePanel.BackColor = Color.White;
            }
        }
    }
}
