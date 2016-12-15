using System;
using System.Runtime.InteropServices;

namespace WindowsInputControl.NativeMethods
{
    internal static class WindowInfo
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);
    }
}