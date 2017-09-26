using System.Runtime.InteropServices;

namespace Logoff_ScreenSaver
{
    public class LogoffInterop
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        public static bool WindowsLogOff()
        {
            return ExitWindowsEx(0, 0);
        }
    }
}
