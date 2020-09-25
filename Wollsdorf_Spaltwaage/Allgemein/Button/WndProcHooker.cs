namespace Allgemein.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    internal class WndProcHooker
    {
        private static Dictionary<Control, HookedProcInformation> ctlDict = new Dictionary<Control, HookedProcInformation>();
        private static Dictionary<IntPtr, HookedProcInformation> hwndDict = new Dictionary<IntPtr, HookedProcInformation>();

        private static void ctl_Disposed(object sender, EventArgs e)
        {
            Control key = sender as Control;
            if (ctlDict.ContainsKey(key))
            {
                ctlDict.Remove(key);
            }
        }

        private static void ctl_HandleCreated(object sender, EventArgs e)
        {
            Control key = sender as Control;
            if (ctlDict.ContainsKey(key))
            {
                HookedProcInformation information = ctlDict[key];
                hwndDict[key.Handle] = information;
                ctlDict.Remove(key);
                information.SetHook();
            }
        }

        private static void ctl_HandleDestroyed(object sender, EventArgs e)
        {
            Control ctl = sender as Control;
            if (hwndDict.ContainsKey(ctl.Handle))
            {
                HookedProcInformation local1 = hwndDict[ctl.Handle];
                UnhookWndProc(ctl, false);
            }
        }

        public static void HookWndProc(Control ctl, WndProcCallback callback, uint msg)
        {
            HookedProcInformation information = null;
            if (ctlDict.ContainsKey(ctl))
            {
                information = ctlDict[ctl];
            }
            else if (hwndDict.ContainsKey(ctl.Handle))
            {
                information = hwndDict[ctl.Handle];
            }
            if (information == null)
            {
                information = new HookedProcInformation(ctl, new Win32.WndProc(WndProcHooker.WindowProc));
                ctl.HandleCreated += new EventHandler(WndProcHooker.ctl_HandleCreated);
                ctl.HandleDestroyed += new EventHandler(WndProcHooker.ctl_HandleDestroyed);
                ctl.Disposed += new EventHandler(WndProcHooker.ctl_Disposed);
                if (ctl.Handle != IntPtr.Zero)
                {
                    information.SetHook();
                }
            }
            if (ctl.Handle == IntPtr.Zero)
            {
                ctlDict[ctl] = information;
            }
            else
            {
                hwndDict[ctl.Handle] = information;
            }
            information.messageMap[msg] = callback;
        }

        public static void UnhookWndProc(Control ctl, bool disposing)
        {
            HookedProcInformation information = null;
            if (ctlDict.ContainsKey(ctl))
            {
                information = ctlDict[ctl];
            }
            else if (hwndDict.ContainsKey(ctl.Handle))
            {
                information = hwndDict[ctl.Handle];
            }
            if (information == null)
            {
                throw new ArgumentException("No hook exists for this control");
            }
            if (ctlDict.ContainsKey(ctl) && disposing)
            {
                ctlDict.Remove(ctl);
            }
            if (hwndDict.ContainsKey(ctl.Handle))
            {
                information.Unhook();
                hwndDict.Remove(ctl.Handle);
                if (!disposing)
                {
                    ctlDict[ctl] = information;
                }
            }
        }

        public static void UnhookWndProc(Control ctl, uint msg)
        {
            HookedProcInformation information = null;
            if (ctlDict.ContainsKey(ctl))
            {
                information = ctlDict[ctl];
            }
            else if (hwndDict.ContainsKey(ctl.Handle))
            {
                information = hwndDict[ctl.Handle];
            }
            if (information == null)
            {
                throw new ArgumentException("No hook exists for this control");
            }
            if (!information.messageMap.ContainsKey(msg))
            {
                throw new ArgumentException(string.Format("No hook exists for message ({0}) on this control", msg));
            }
            information.messageMap.Remove(msg);
        }

        private static int WindowProc(IntPtr hwnd, uint msg, uint wParam, int lParam)
        {
            if (!hwndDict.ContainsKey(hwnd))
            {
                return Win32.DefWindowProc(hwnd, msg, wParam, lParam);
            }
            HookedProcInformation information = hwndDict[hwnd];
            if (information.messageMap.ContainsKey(msg))
            {
                WndProcCallback callback = information.messageMap[msg];
                bool handled = false;
                int num = callback(hwnd, msg, wParam, lParam, ref handled);
                if (handled)
                {
                    return num;
                }
            }
            return information.CallOldWindowProc(hwnd, msg, wParam, lParam);
        }

        internal class HookedProcInformation
        {
            private Control control;
            public Dictionary<uint, WndProcHooker.WndProcCallback> messageMap;
            private Win32.WndProc newWndProc;
            private IntPtr oldWndProc;

            public HookedProcInformation(Control ctl, Win32.WndProc wndproc)
            {
                this.control = ctl;
                this.newWndProc = wndproc;
                this.messageMap = new Dictionary<uint, WndProcHooker.WndProcCallback>();
            }

            public int CallOldWindowProc(IntPtr hwnd, uint msg, uint wParam, int lParam)
            {
                return Win32.CallWindowProc(this.oldWndProc, hwnd, msg, wParam, lParam);
            }

            public void SetHook()
            {
                IntPtr handle = this.control.Handle;
                if (handle == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Handle for control has not been created");
                }
                this.oldWndProc = Win32.SetWindowLong(handle, -4, Marshal.GetFunctionPointerForDelegate(this.newWndProc));
            }

            public void Unhook()
            {
                IntPtr handle = this.control.Handle;
                if (handle == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Handle for control has not been created");
                }
                Win32.SetWindowLong(handle, -4, this.oldWndProc);
            }
        }

        public delegate int WndProcCallback(IntPtr hwnd, uint msg, uint wParam, int lParam, ref bool handled);
    }
}

