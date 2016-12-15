using System;
using System.Runtime.InteropServices;

namespace WindowsInputControl.NativeMethods
{

    internal static class NativeKeyboardLayout
    {
        [DllImport("user32.dll")]
        public static extern int MapVirtualKeyEx(int uCode, int uMapType, IntPtr dwhkl);
    }
}