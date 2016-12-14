using System;
using System.Runtime.InteropServices;
using WindowsInputControl.Hooks;
using WindowsInputControl.WindowsHooks.Keyboard;

namespace WindowsInputControl.NativeMethods
{
    internal static class NativeKeyboardHooks
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookHandlerDelegate lpfn, IntPtr hMod,
            uint dwThreadId);


        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, KeyEventType wParam, KeyEventArgs lParam);

        [DllImport("user32.dll")]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);


        [DllImport("user32.dll")]
        public static extern IntPtr GetMessageExtraInfo();
    }
}