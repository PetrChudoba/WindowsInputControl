using System;

namespace WindowsInputControl.Hooks
{
    public class WindowsKeyboardLayouts : IKeyboardLayout
    {

        private IntPtr _keyboardHandle;


        public string Identifier { get; }

        public string Name { get; }

        public ushort GetVirtualKey(ushort scanCode)
        {
            return (ushort) NativeKeyboardMethods.MapVirtualKeyEx(scanCode, 3, _keyboardHandle);
        }
    }
}