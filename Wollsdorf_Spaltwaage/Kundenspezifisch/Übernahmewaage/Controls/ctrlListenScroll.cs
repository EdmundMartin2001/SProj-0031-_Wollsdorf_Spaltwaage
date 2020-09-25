namespace Wollsdorf.Spaltwaage.Controls
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Drawing;

    public partial class ctrlListenScroll : UserControl
    {
        [DllImport("coredll.dll")]
        internal static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public ctrlListenScroll()
        {
            InitializeComponent();

            this.cmdBildUp.Bild_Icon = Wollsdorf_Spaltwaage.Properties.Resources.ico_PGUp;
            this.cmdBildDown.Bild_Icon = Wollsdorf_Spaltwaage.Properties.Resources.ico_PGDown;
            this.cmdScrollDown.Bild_Icon = Wollsdorf_Spaltwaage.Properties.Resources.ico_ArrowDown;
            this.cmdScrollUp.Bild_Icon = Wollsdorf_Spaltwaage.Properties.Resources.ico_ArrowUp;

            this.ResetButton();
        }
        private void ResetButton()
        {
            foreach (Control b in this.Controls)
            {
                if (b.GetType() == typeof(Allgemein.Controls.ctrlButton))
                {
                    ((Allgemein.Controls.ctrlButton)b).StartColor = Color.DimGray;
                }
            }
        }
        private void cmdScrollUp_Click(object sender, EventArgs e)
        {
            const byte VK_DOWN = 0x26;
            const int KEYEVENTF_KEYUP = 0x2;
            const int KEYEVENTF_KEYDOWN = 0x0;

            keybd_event(VK_DOWN, 0, KEYEVENTF_KEYDOWN, 0);//press Key
            keybd_event(VK_DOWN, 0, KEYEVENTF_KEYUP, 0);//release Key
        }
        private void cmdScrollDown_Click(object sender, EventArgs e)
        {
            const byte VK_DOWN = 0x28;
            const int KEYEVENTF_KEYUP = 0x2;
            const int KEYEVENTF_KEYDOWN = 0x0;

            keybd_event(VK_DOWN, 0, KEYEVENTF_KEYDOWN, 0);//press Key
            keybd_event(VK_DOWN, 0, KEYEVENTF_KEYUP, 0);//release Key
        }
        private void cmdBildUp_Click(object sender, EventArgs e)
        {
            const byte VK_PageUp = 0x21;
            const int KEYEVENTF_KEYUP = 0x2;
            const int KEYEVENTF_KEYDOWN = 0x0;

            keybd_event(VK_PageUp, 0, KEYEVENTF_KEYDOWN, 0);//press Key
            keybd_event(VK_PageUp, 0, KEYEVENTF_KEYUP, 0);//release Key
        }
        private void cmdBildDown_Click(object sender, EventArgs e)
        {
            const byte VK_PageDown = 0x22;
            const int KEYEVENTF_KEYUP = 0x2;
            const int KEYEVENTF_KEYDOWN = 0x0;

            keybd_event(VK_PageDown, 0, KEYEVENTF_KEYDOWN, 0);//press Key
            keybd_event(VK_PageDown, 0, KEYEVENTF_KEYUP, 0);//release Key
        }

        private void ctrlListenScroll_Click(object sender, EventArgs e)
        {

        }

        private void ctrlListenScroll_Resize(object sender, EventArgs e)
        {
            int ih = this.Height / 4;

            this.cmdBildDown.Height = ih;
            this.cmdBildUp.Height = ih;
            this.cmdScrollDown.Height = ih;
            this.cmdScrollUp.Height = ih;

            this.cmdBildUp.Top = 0;
            this.cmdScrollUp.Top = this.cmdBildUp.Top + this.cmdBildUp.Height;
            this.cmdScrollDown.Top = this.cmdScrollUp.Top + this.cmdScrollUp.Height;
            this.cmdBildDown.Top = this.cmdScrollDown.Top + this.cmdScrollDown.Height;
        }
    }
}
