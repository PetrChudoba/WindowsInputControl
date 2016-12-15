using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowsInputControl.NativeMethods
{
    internal static class KeyboardsControl
    {
        public const int KeyboardNameLength = 8;




        [DllImport("user32.dll")]
        public static extern IntPtr GetKeyboardLayout(int idThread);

        [DllImport("user32.dll")]
        public static extern long GetKeyboardLayoutName(StringBuilder pwszKLID);

        [DllImport("user32.dll")]
        public static extern int GetKeyboardLayoutList(int nBuff, [Out] IntPtr[] lpList);


        [DllImport("user32.dll")]
        public static extern IntPtr LoadKeyboardLayout(string pwszKLID, uint Flags);


        [DllImport("user32.dll")]
        public static extern IntPtr ActivateKeyboardLayout(IntPtr hkl, uint flags);

    }
}