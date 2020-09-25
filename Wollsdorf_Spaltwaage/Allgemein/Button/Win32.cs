namespace Allgemein.Controls
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public sealed class Win32
    {
        public const uint BM_SETSTATE = 0xf3;
        public const int GRADIENT_FILL_RECT_H = 0;
        public const int GRADIENT_FILL_RECT_V = 1;
        public const int GWL_WNDPROC = -4;
        public const uint VK_RETURN = 13;
        public const uint VK_SPACE = 0x20;
        public const uint WM_ENABLE = 10;
        public const int WM_IDLESTATE_RESET = 0x403;
        public const uint WM_KEYDOWN = 0x100;
        public const uint WM_KEYUP = 0x101;
        public const uint WM_LBUTTONDBLCLK = 0x203;
        public const uint WM_LBUTTONDOWN = 0x201;
        public const uint WM_LBUTTONUP = 0x202;
        public const uint WM_MOUSEMOVE = 0x200;
        public const uint WM_PAINT = 15;
        public const int WM_USER = 0x400;

        [DllImport("coredll.dll")]
        public static extern IntPtr BeginPaint(IntPtr hwnd, ref PAINTSTRUCT ps);
        [DllImport("coredll.dll")]
        public static extern int CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hwnd, uint msg, uint wParam, int lParam);
        [DllImport("coredll.dll")]
        public static extern int DefWindowProc(IntPtr hwnd, uint msg, uint wParam, int lParam);
        [DllImport("coredll.dll")]
        public static extern bool EndPaint(IntPtr hwnd, ref PAINTSTRUCT ps);
        [DllImport("coredll.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("coredll.dll", SetLastError=true)]
        public static extern bool GradientFill(IntPtr hdc, TRIVERTEX[] pVertex, uint dwNumVertex, GRADIENT_RECT[] pMesh, uint dwNumMesh, uint dwMode);
        public static Point LParamToPoint(int lParam)
        {
            uint num = (uint) lParam;
            return new Point(((int) num) & 0xffff, (int) ((num & -65536) >> 0x10));
        }

        [DllImport("coredll.dll")]
        public static extern bool ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("coredll.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("coredll.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hwnd, int nIndex, IntPtr dwNewLong);
        [DllImport("coredll.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hwnd, int nIndex, int iNewVal);

        
        [StructLayout(LayoutKind.Sequential)]
        public struct GRADIENT_RECT
        {
            public uint UpperLeft;
            public uint LowerRight;
            public GRADIENT_RECT(uint ul, uint lr)
            {
                this.UpperLeft = ul;
                this.LowerRight = lr;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            private IntPtr hdc;
            public bool fErase;
            public Rectangle rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x20)]
            public byte[] rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TRIVERTEX
        {
            public int x;
            public int y;
            public ushort Red;
            public ushort Green;
            public ushort Blue;
            public ushort Alpha;
            public TRIVERTEX(int x, int y, Color color) : this(x, y, color.R, color.G, color.B, color.A)
            {
            }

            public TRIVERTEX(int x, int y, ushort red, ushort green, ushort blue, ushort alpha)
            {
                this.x = x;
                this.y = y;
                this.Red = (ushort) (red << 8);
                this.Green = (ushort) (green << 8);
                this.Blue = (ushort) (blue << 8);
                this.Alpha = (ushort) (alpha << 8);
            }
        }

        public delegate int WndProc(IntPtr hwnd, uint msg, uint wParam, int lParam);
    }
}

