using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowsInputControl.Hooks
{
    internal static class NativeKeyboardMethods
    {

        public const int KeyboardNameLength = 8;


        [DllImport("user32.dll")]
        public static extern int MapVirtualKeyEx(int uCode, int uMapType, IntPtr dwhkl);

        [DllImport("user32.dll")]
        public static extern IntPtr GetKeyboardLayout(int idThread);

        [DllImport("user32.dll")]
        public static extern long GetKeyboardLayoutName(StringBuilder pwszKLID);


        [DllImport("user32.dll")]
        public static extern IntPtr LoadKeyboardLayout(string pwszKLID, uint Flags);


        [DllImport("user32.dll")]
        public static extern IntPtr ActivateKeyboardLayout(IntPtr hkl, uint flags);


        [DllImport("user32.dll")]
        public static extern int GetKeyboardLayoutList(int nBuff, [Out] IntPtr[] lpList);
    }
}