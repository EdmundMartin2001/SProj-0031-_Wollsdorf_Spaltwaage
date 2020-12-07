using System.Runtime.InteropServices;

namespace Wollsdorf_Spaltwaage.Allgemein.SystemHelper
{
    internal class SystemHelper
    {
        [DllImport("coredll.dll", SetLastError = true)]
        static extern int SetSystemPowerState(string psState, int StateFlags, int Options);

        const int POWER_FORCE = 4096;
        const int POWER_STATE_RESET = 0x00800000;

        public static void WarmReset()
        {
            SetSystemPowerState(null, POWER_STATE_RESET, POWER_FORCE);
        }

        public static string ExecutablePath
        {
            get
            {
                return System.IO.Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            }
        }
    
    }
}
