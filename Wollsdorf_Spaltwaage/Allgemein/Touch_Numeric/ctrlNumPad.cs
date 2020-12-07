using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Wollsdorf_Spaltwaage.Allgemein.SQL;

namespace Wollsdorf_Spaltwaage.Allgemein.Touch_Numeric
{
    public partial class ctrlNumPad : UserControl
    {
        [DllImport("coredll.dll")]
        internal static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public ctrlNumPad()
        {
            InitializeComponent();
        }

        private void PressKey(char vk_key)
        {
            byte VK_KEY = 0;
            try
            {
                VK_KEY = Convert.ToByte(vk_key.ToString().ToUpper()[0]);
            }
            catch (OverflowException) { }
            bool shift = false;

            if (Char.IsUpper(vk_key))
            {
                shift = true;
            }

            switch (vk_key)
            {
                case ';':
                    VK_KEY = 0xBA;
                    break;
                case ':':
                    VK_KEY = 0xBA;
                    shift = true;
                    break;
                case '=':
                    VK_KEY = 0xBB;
                    break;
                case '+':
                    VK_KEY = 0xBB;
                    shift = true;
                    break;
                case ',':
                    VK_KEY = 0xBC;
                    break;
                case '<':
                    VK_KEY = 0xBC;
                    shift = true;
                    break;
                case '-':
                    VK_KEY = 0xBD;
                    break;
                case '_':
                    VK_KEY = 0xBD;
                    shift = true;
                    break;
                case '.':
                    VK_KEY = 0xBE;
                    break;
                case '>':
                    VK_KEY = 0xBE;
                    shift = true;
                    break;
                case '/':
                    VK_KEY = 0xBF;
                    break;
                case '?':
                    VK_KEY = 0xBF;
                    shift = true;
                    break;
                case '`':
                    VK_KEY = 0xC0;
                    break;
                case '~':
                    VK_KEY = 0xC0;
                    shift = true;
                    break;
                case '[':
                    VK_KEY = 0xDB;
                    break;
                case '{':
                    VK_KEY = 0xDB;
                    shift = true;
                    break;
                case ']':
                    VK_KEY = 0xDD;
                    break;
                case '}':
                    VK_KEY = 0xDD;
                    shift = true;
                    break;
                case '\\':
                    VK_KEY = 0xDC;
                    break;
                case '|':
                    VK_KEY = 0xDC;
                    shift = true;
                    break;
                case '\'':
                    VK_KEY = 0xDE;
                    break;
                case '"':
                    VK_KEY = 0xDE;
                    shift = true;
                    break;
                case '!':
                    VK_KEY = Convert.ToByte('1');
                    shift = true;
                    break;
                case '@':
                    VK_KEY = Convert.ToByte('2');
                    shift = true;
                    break;
                case '#':
                    VK_KEY = Convert.ToByte('3');
                    shift = true;
                    break;
                case '$':
                    VK_KEY = Convert.ToByte('4');
                    shift = true;
                    break;
                case '%':
                    VK_KEY = Convert.ToByte('5');
                    shift = true;
                    break;
                case '^':
                    VK_KEY = Convert.ToByte('6');
                    shift = true;
                    break;
                case '&':
                    VK_KEY = Convert.ToByte('7');
                    shift = true;
                    break;
                case '*':
                    VK_KEY = Convert.ToByte('8');
                    shift = true;
                    break;
                case '(':
                    VK_KEY = Convert.ToByte('9');
                    shift = true;
                    break;
                case ')':
                    VK_KEY = Convert.ToByte('0');
                    shift = true;
                    break;

                case '¡':
                    VK_KEY = Convert.ToByte(' ');
                    break;
                case '€':
                    VK_KEY = Convert.ToByte(' ');
                    break;
                case '£':
                    VK_KEY = Convert.ToByte(' ');
                    break;
                case '¥':
                    VK_KEY = Convert.ToByte(' ');
                    break;
                case '¿':
                    VK_KEY = Convert.ToByte(' ');
                    break;

                case ('\t'):
                    VK_KEY = 0x09;
                    break;
            }


            const int KEYEVENTF_KEYUP = 0x2;
            const int KEYEVENTF_KEYDOWN = 0x0;
            const byte VK_SHIFT = 0xA0;

            if (shift) keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYDOWN, 0);//press Shift Key

            keybd_event(VK_KEY, 0, KEYEVENTF_KEYDOWN, 0);//press Key
            keybd_event(VK_KEY, 0, KEYEVENTF_KEYUP, 0);//release Key

            if (shift) keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, 0);//press Shift Key
        }

        private void Reset_TextBox()
        {
            try
            {
                Control c = FindFocusedControl(this.Parent);

                if (c != null)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        ((TextBox)c).Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                SiAuto.LogException("Reset_TextBox", ex);
                throw;
            }
        }
        private Control FindFocusedControl(Control container)
        {
            foreach (Control childControl in container.Controls.Cast<Control>().Where(childControl => childControl.Focused)) return childControl;
            return (from Control childControl in container.Controls select FindFocusedControl(childControl)).FirstOrDefault(maybeFocusedControl => maybeFocusedControl != null);
        }

        private void ctrlButton1_Click(object sender, EventArgs e)
        {
            PressKey('1');
        }
        private void ctrlButton2_Click(object sender, EventArgs e)
        {
            PressKey('2');
        }
        private void ctrlButton7_Click(object sender, EventArgs e)
        {
            PressKey('7');
        }
        private void ctrlButton8_Click(object sender, EventArgs e)
        {
            PressKey('8');
        }
        private void ctrlButton9_Click(object sender, EventArgs e)
        {
            PressKey('9');
        }
        private void ctrlButton4_Click(object sender, EventArgs e)
        {
            PressKey('4');
        }
        private void ctrlButton5_Click(object sender, EventArgs e)
        {
            PressKey('5');
        }
        private void ctrlButton6_Click(object sender, EventArgs e)
        {
            PressKey('6');
        }
        private void ctrlButton3_Click(object sender, EventArgs e)
        {
            PressKey('3');
        }
        private void ctrlButton0_Click(object sender, EventArgs e)
        {
            PressKey('0');
        }
        private void ctrlButtonBs_Click(object sender, EventArgs e)
        {
            PressKey('\x08');
        }

        private void cmdKeyReset_Click(object sender, EventArgs e)
        {
            this.Reset_TextBox();
        }

        private void ctrlNumPad_Click(object sender, EventArgs e)
        {

        }
    }
}
