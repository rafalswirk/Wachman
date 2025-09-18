using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.Core.Monitoring
{   
    public class ActivityReader : IActivityReader
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            uint pid;
            var id = GetWindowThreadProcessId(handle, out pid);

            var proc = Process.GetProcessById((int)pid);
            var exeName = proc.MainModule.ModuleName;

            if (GetWindowText(handle, Buff, nChars) > 0)
            {

                return $"{exeName}; {Buff.ToString()}";
            }
            

            return string.Empty;
        }
    }
}
