namespace Allgemein.Controls
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    
    internal class ctrlButton : Button
    {
        private Bitmap bmDoubleBuffer;
        private Icon BildIcon;
        private Image BildImage;
        ////private const string constALIGNCENTER = "center";
        ////private const string constALIGNLEFT = "left";
        ////private const string constALIGNRIGHT = "right";
        ////private const string constFONTLARGE = "large";
        ////private const int constFONTLARGESIZE = 0x38;
        ////private const string constFONTMEDIUM = "medium";
        ////private const int constFONTMEDIUMSIZE = 0x24;
        ////private const string constFONTSMALL = "small";
        ////private const int constFONTSMALLSIZE = 0x12;
        private Color endColorValue = Color.Black;
        
        private GradientFill.FillDirection fillDirectionValue;
        private bool gotKeyDown;
        private bool gotMouseDown;
        //private string imageFileValue;
        private Point lastCursorCoordinates;
        private bool m_bPushed;
        private Brush colFontForeColor;
        private Brush colBackColor;
        private Brush colPressBackColor;
        //private Color m_BrushColor = Color.White;
        //private Color m_DisabledEndColor = Color.Red;
        //private Color m_DisabledForeColor = Color.Red;
        //private Color m_DisabledStartColor = Color.Red;
        private string sButtonText;
        //private bool m_TextAlwaysAtCenter = true;
        //private string oldImageFileValue = string.Empty;
        private Color startColorValue = Color.Silver;
        
        private Color startColorValueOnPush = Color.FromArgb(0x8b, 0xca, 0xcf);
        private Color endColorValueOnPush = Color.FromArgb(0x8b, 0xca, 0xcf);

        public event OnMouseDelegate EventOnMouseDown;

        public event OnMouseDelegate EventOnMouseMove;

        public event OnMouseDelegate EventOnMouseUp;

        public ctrlButton()
        {
            this.BildImage = null;
            this.BildIcon = null;
            this.colFontForeColor = new SolidBrush(Color.White);
            this.bmDoubleBuffer = new Bitmap(base.Width, base.Height);
            this.fillDirectionValue = GradientFill.FillDirection.TopToBottom;
            this.startColorValue = Color.DimGray;
            this.endColorValue = Color.DimGray;
            this.colBackColor = new SolidBrush(startColorValue);
            this.colPressBackColor = new SolidBrush(Color.Yellow);
            
            //this.m_DisabledForeColor = Color.Black;
            //this.m_DisabledEndColor = Color.Black;
            //this.m_DisabledStartColor = Color.Black;

            this.ForeColor = Color.White;
            this.m_bPushed = false;
            WndProcHooker.HookWndProc(this, new WndProcHooker.WndProcCallback(this.WM_Paint_Handler), 15);
            WndProcHooker.HookWndProc(this, new WndProcHooker.WndProcCallback(this.WM_LButtonDown_Handler), 0x201);
            WndProcHooker.HookWndProc(this, new WndProcHooker.WndProcCallback(this.WM_LButtonUp_Handler), 0x202);
            WndProcHooker.HookWndProc(this, new WndProcHooker.WndProcCallback(this.WM_MouseMove_Handler), 0x200);
            WndProcHooker.HookWndProc(this, new WndProcHooker.WndProcCallback(this.WM_KeyDown_Handler), 0x100);
            WndProcHooker.HookWndProc(this, new WndProcHooker.WndProcCallback(this.WM_KeyUp_Handler), 0x101);
            WndProcHooker.HookWndProc(this, new WndProcHooker.WndProcCallback(this.WM_LButtonDblClick_Handler), 0x203);
            WndProcHooker.HookWndProc(this, new WndProcHooker.WndProcCallback(this.WM_Paint_Handler), 10);
            WndProcHooker.HookWndProc(this, new WndProcHooker.WndProcCallback(this.WM_Paint_Handler), 0xf3);
            WndProcHooker.HookWndProc(this, new WndProcHooker.WndProcCallback(this.WM_IdleStateHandler), 0x403);

            OhneFocus();
        }

        private void OhneFocus()
        {
            const int WS_EX_NOACTIVATE = 0x08000000;
            const int GWL_EXSTYLE = -20;

            try
            {
                IntPtr handle = this.Handle;
                if (handle == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Handle for control has not been created");
                }
    
                Win32.SetWindowLong(handle, GWL_EXSTYLE, WS_EX_NOACTIVATE);
              
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (this.bmDoubleBuffer != null)
                {
                    this.bmDoubleBuffer.Dispose();
                }
                if (this.BildIcon != null)
                {
                    this.BildIcon.Dispose();
                    this.BildIcon = null;
                }
                if (this.BildImage != null)
                {
                    this.BildImage.Dispose();
                    this.BildImage = null;
                }
                if (this.colFontForeColor != null)
                {
                    this.colFontForeColor.Dispose();
                    this.colFontForeColor = null;
                }
            }
        }

        private Rectangle GetBildSize()
        {
            Rectangle x = Rectangle.Empty;

            if (this.BildIcon != null)
            {                
                x = new Rectangle(
                    (base.Width - this.BildIcon.Width) / 2,
                    (base.Height - this.BildIcon.Height) / 2, 
                    this.BildIcon.Width, 
                    this.BildIcon.Height);
            }
            else if (this.BildImage != null)
            {
                x = new Rectangle(
                    (base.Width - this.BildImage.Width) / 2,
                    (base.Height - this.BildImage.Height) / 2,
                    this.BildImage.Width,
                    this.BildImage.Height);
            }

            return x;
        }
        private void DrawButton(Graphics gr, bool pressed)
        {
            if (!base.Enabled)
            {
            }

            //Color startColorValueOnPush = base.Enabled ? this.startColorValue : this.m_DisabledStartColor;
            //Color endColorValueOnPush = base.Enabled ? this.endColorValue : this.m_DisabledEndColor;

            if (this.m_bPushed && base.Enabled)
            {
              //  startColorValueOnPush = this.startColorValueOnPush;
              //  endColorValueOnPush = this.endColorValueOnPush;
            }

            if ((base.Width != this.bmDoubleBuffer.Width) || (base.Height != this.bmDoubleBuffer.Height))
            {
                this.bmDoubleBuffer.Dispose();
                this.bmDoubleBuffer = new Bitmap(base.Width, base.Height);
            }

            Graphics graphics = Graphics.FromImage(this.bmDoubleBuffer);
            Rectangle clientRectangle = base.ClientRectangle;

            if (this.m_bPushed)
            {
                graphics.FillRectangle(
                    colPressBackColor ,
                    ClientRectangle
                    );
            }
            else
            {
                graphics.FillRectangle(
                    (pressed ? colPressBackColor : colBackColor),
                    ClientRectangle
                    );
            }
            //GradientFill.Fill(
            //    graphics,
            //    clientRectangle,
            //    pressed ? endColorValueOnPush : startColorValueOnPush,
            //    pressed ? startColorValueOnPush : endColorValueOnPush,
            //    this.fillDirectionValue);

            if ((this.Text != null) && (this.sButtonText.CompareTo("\0") == 0))
            {
                this.sButtonText = "";
            }

            SizeF ef = graphics.MeasureString(this.sButtonText, this.Font);
            //int width = 0;
           // int height = 0;
            bool flag = false;
            bool flag2 = false;
            //int x = 0;
            //int y = 0;
            float num5 = 0f;
            float num6 = 0f;

            if ((this.BildImage != null) && !string.IsNullOrEmpty(this.sButtonText))
            {
                flag = flag2 = true;
                //width = this.buttonIcon.Width;
                // height = this.buttonIcon.Height;
                //int num7 = Convert.ToInt32((float) ((ef.Height + height) + 4f));

                // if (base.Height < num7)
                // {
                //     int num1 = base.Height;
                // }

                num5 = (base.Width - ef.Width) / 2f;
                // x = (base.Width - width) / 2;
                // y = 2;
                num6 = base.Height - (ef.Height + 2f);

            }
            else if ((this.BildIcon != null) && !string.IsNullOrEmpty(this.sButtonText))
            {
                flag = flag2 = true;
                //width = this.buttonIcon.Width;
               // height = this.buttonIcon.Height;
                //int num7 = Convert.ToInt32((float) ((ef.Height + height) + 4f));
                
               // if (base.Height < num7)
               // {
               //     int num1 = base.Height;
               // }

                num5 = (base.Width - ef.Width) / 2f;
               // x = (base.Width - width) / 2;
               // y = 2;
                num6 = base.Height - (ef.Height + 2f);
            }
            else if (this.BildIcon != null)
            {
              //  width = this.buttonIcon.Width;
               // height = this.buttonIcon.Height;
                flag = true;
              //  x = (base.Width - width) / 2;
              //  y = (base.Height - height) / 2;
            }
            else
            {
                flag2 = true;
                num5 = (base.Width - ef.Width) / 2f;
                //if (this.m_TextAlwaysAtCenter)
                //{
                    num6 = (base.Height - ef.Height) / 2f;
                //}
                //else
               // {
                 //   num6 = base.Height - (ef.Height + 2f);
               // }
            }
            Rectangle RecBild = this.GetBildSize();

            //Rectangle rectangle2 = new Rectangle(x, y, width, height);
            Rectangle rectangle3 = base.ClientRectangle;
            rectangle3.Inflate(-1, -1);

            if (this.m_bPushed)
            {
                RecBild.Inflate(-1, -1);
                rectangle3.Inflate(-1, -1);
                num5++;
                num6++;
            }

            if (flag)
            {
                if (this.BildImage != null)
                {                  
                    graphics.DrawImage(this.BildImage, RecBild.Location.X, RecBild.Location.Y);
                    
                }
                else
                {
                    if (this.Enabled)
                    {
                        graphics.DrawIcon(this.BildIcon, RecBild.Location.X, RecBild.Location.Y);
                    }
                    else
                    {
                        // Zeige kein Bild an, wenn der Button Disabled ist
                    }
                }
            }

            if ( (flag2) && ( this.Enabled) )
            {
                graphics.DrawString(this.sButtonText, this.Font, this.colFontForeColor, num5, num6);
                if (this.m_bPushed && base.Enabled)
                {
                    graphics.DrawString(this.sButtonText, this.Font, new SolidBrush(Color.Black), num5, num6);
                }
            }
            if (!this.m_bPushed || !base.Enabled)
            {
                clientRectangle = base.ClientRectangle;
                clientRectangle.Width--;
                clientRectangle.Height--;
                Pen pen = this.Focused ? new Pen(SystemColors.WindowFrame, 3f) : new Pen(SystemColors.WindowFrame);
                graphics.DrawRectangle(pen, clientRectangle);
                pen.Dispose();
            }
            gr.DrawImage(this.bmDoubleBuffer, 0, 0);
            graphics.Dispose();
        }

        private void DrawButton(IntPtr hwnd, bool pressed)
        {
            IntPtr dC = Win32.GetDC(hwnd);
            Graphics gr = Graphics.FromHdc(dC);
            this.DrawButton(gr, pressed);
            gr.Dispose();
            Win32.ReleaseDC(hwnd, dC);
        }

        private Color GetColor(string sColor)
        {
            string[] strArray = sColor.Split(new char[] { ',' });
            Color color = new Color();
            if (strArray.Length == 3)
            {
                return Color.FromArgb(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), Convert.ToInt32(strArray[2]));
            }
            return Color.Black;
        }

        protected void OnDoubleClick(MouseEventArgs e)
        {
            base.OnClick(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            base.Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            base.Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this.m_bPushed = false;
            base.OnLostFocus(e);
            base.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.EventOnMouseDown != null)
            {
                this.EventOnMouseDown(this, e);
            }
            if (base.Parent != null)
            {
                Win32.SendMessage(base.Parent.Handle, 0x403, 0, 0);
            }
            if (e.Button == MouseButtons.Left)
            {
                this.gotMouseDown = true;
            }
            this.m_bPushed = true;
            base.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (this.EventOnMouseUp != null)
            {
                this.EventOnMouseUp(this, e);
            }
            this.m_bPushed = false;
            base.Invalidate();
            if ((e.Button == MouseButtons.Left) && this.gotMouseDown)
            {
                base.OnClick(EventArgs.Empty);
                this.gotMouseDown = false;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        private int WM_IdleStateHandler(IntPtr hwnd, uint msg, uint wParam, int lParam, ref bool handled)
        {
            Win32.SendMessage(base.Parent.Handle, 0x403, 0, 0);
            return -1;
        }

        private int WM_KeyDown_Handler(IntPtr hwnd, uint msg, uint wParam, int lPAram, ref bool handled)
        {
            if ((wParam == 0x20) || (wParam == 13))
            {
                this.DrawButton(hwnd, true);
                handled = true;
                this.gotKeyDown = true;
            }
            if (!handled)
            {
                return -1;
            }
            return 0;
        }

        private int WM_KeyUp_Handler(IntPtr hwnd, uint msg, uint wParam, int lParam, ref bool handled)
        {
            if (this.gotKeyDown && ((wParam == 0x20) || (wParam == 13)))
            {
                this.DrawButton(hwnd, false);
                this.OnClick(EventArgs.Empty);
                handled = true;
                this.gotKeyDown = false;
            }
            if (!handled)
            {
                return -1;
            }
            return 0;
        }

        private int WM_LButtonDblClick_Handler(IntPtr hwnd, uint msg, uint wParam, int lParam, ref bool handled)
        {
            base.Capture = true;
            base.Focus();
            this.DrawButton(hwnd, true);
            this.lastCursorCoordinates = Win32.LParamToPoint(lParam);
            this.OnDoubleClick(new MouseEventArgs(MouseButtons.Left, 1, this.lastCursorCoordinates.X, this.lastCursorCoordinates.Y, 0));
            handled = true;
            return 0;
        }

        private int WM_LButtonDown_Handler(IntPtr hwnd, uint msg, uint wParam, int lParam, ref bool handled)
        {
            base.Capture = true;
            base.Focus();
            this.DrawButton(hwnd, true);
            
            this.lastCursorCoordinates = Win32.LParamToPoint(lParam);
            this.OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, this.lastCursorCoordinates.X, this.lastCursorCoordinates.Y, 0));
            if (!handled)
            {
                return -1;
            }
            return 0;
        }

        private int WM_LButtonUp_Handler(IntPtr hwnd, uint msg, uint wParam, int lParam, ref bool handled)
        {
            base.Capture = false;
            this.DrawButton(hwnd, false);
            this.lastCursorCoordinates = Win32.LParamToPoint(lParam);
            if (base.ClientRectangle.Contains(this.lastCursorCoordinates))
            {
                this.OnMouseUp(new MouseEventArgs(MouseButtons.Left, 1, this.lastCursorCoordinates.X, this.lastCursorCoordinates.Y, 0));
            }
            handled = true;
            return 0;
        }

        private int WM_MouseMove_Handler(IntPtr hwnd, uint msg, uint wParam, int lParam, ref bool handled)
        {
            if (base.Capture)
            {
                Point point = Win32.LParamToPoint(lParam);
                if (this.EventOnMouseMove != null)
                {
                    this.EventOnMouseMove(this, new MouseEventArgs(MouseButtons.Left, 1, point.X, point.Y, 0));
                }
            }
            return -1;
        }

        private int WM_Paint_Handler(IntPtr hwnd, uint msg, uint wParam, int lParam, ref bool handled)
        {
            Win32.PAINTSTRUCT ps = new Win32.PAINTSTRUCT();
            Graphics gr = Graphics.FromHdc(Win32.BeginPaint(hwnd, ref ps));
            this.DrawButton(gr, base.Capture && base.ClientRectangle.Contains(this.lastCursorCoordinates));
            gr.Dispose();
            Win32.EndPaint(hwnd, ref ps);
            handled = true;
            return 0;
        }

        //public Color Color_Disabled_EndColor
        //{
        //    get
        //    {
        //        return this.m_DisabledEndColor;
        //    }
        //    set
        //    {
        //        this.m_DisabledEndColor = value;
        //        base.Invalidate();
        //    }
        //}

        //public Color Color_Disabled_ForeColor
        //{
        //    get
        //    {
        //        return this.m_DisabledForeColor;
        //    }
        //    set
        //    {
        //        this.m_DisabledForeColor = value;
        //        base.Invalidate();
        //    }
        //}

        //public Color Color_Disabled_Start
        //{
        //    get
        //    {
        //        return this.m_DisabledStartColor;
        //    }
        //    set
        //    {
        //        this.m_DisabledStartColor = value;
        //        base.Invalidate();
        //    }
        //}

        //public Color EndColor
        //{
        //    get
        //    {
        //        return this.endColorValue;
        //    }
        //    set
        //    {
        //        this.endColorValue = value;
        //        base.Invalidate();
        //    }
        //}

        public Color EndColorOnPush
        {
            get
            {
                return this.endColorValueOnPush;
            }
            set
            {
                this.endColorValueOnPush = value;
                this.colBackColor = new SolidBrush(value);
                base.Invalidate();
            }
        }
        public Image Bild_Image
        {
            get
            {
                return this.BildImage;
            }
            set
            {               
                this.BildImage = value;
            }
        }
        public Icon Bild_Icon
        {
            get
            {
                return this.BildIcon;
            }
            set
            {


                this.BildIcon = value;
            }
        }  
        public bool Pushed
        {
            get
            {
                return this.m_bPushed;
            }
            set
            {
                this.m_bPushed = value;
            }
        }
        public Color StartColor
        {
            get
            {
                return this.startColorValue;
            }
            set
            {
                this.startColorValue = value;
                this.colBackColor = new SolidBrush(value);
                base.Invalidate();
            }
        }
        public override string Text
        {
            get
            {
                return this.sButtonText;
            }
            set
            {
                this.sButtonText = value;
                base.Invalidate();
            }
        }

        public delegate void OnMouseDelegate(object sender, MouseEventArgs evtargs);
    }
}

