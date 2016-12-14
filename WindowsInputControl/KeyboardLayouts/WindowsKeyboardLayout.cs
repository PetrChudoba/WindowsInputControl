using System;
using System.Diagnostics.Contracts;
using WindowsInputControl.Helpers;
using WindowsInputControl.NativeMethods;
using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl.KeyboardLayouts
{
    internal class WindowsKeyboardLayout : IKeyboardLayout
    {
        #region Ctor

        public WindowsKeyboardLayout(IntPtr keyboardHandle, string keyboardIdentifier)
        {
            Contract.Requires(keyboardHandle != null);
            Contract.Requires(!string.IsNullOrEmpty(keyboardIdentifier));
            Contract.Requires(keyboardIdentifier.Length == 8);

            _keyboardHandle = keyboardHandle;
            _keyboardIdentifier = keyboardIdentifier;
        }

        #endregion

        #region Fields

        private readonly IntPtr _keyboardHandle;
        private readonly string _keyboardIdentifier;

        #endregion

        #region Properties

        public string Identifier
        {
            get { return _keyboardIdentifier; }
        }

        public string Name
        {
            get { return ServiceLocator.KeyboardLayoutNames.GetLayoutName(_keyboardIdentifier); }
        }

        #endregion

        #region Methods

        public VirtualKey GetVirtualKey(ScanCode scanCode)
        {
            return (VirtualKey) NativeKeyboardMethods.MapVirtualKeyEx(scanCode.Code, 3, _keyboardHandle);
        }

        public ScanCode GetScanCode(VirtualKey virtualKey)
        {
            ushort scanCode = (ushort) NativeKeyboardMethods.MapVirtualKeyEx((int) virtualKey, 4, _keyboardHandle);
            return new ScanCode(scanCode);
        }

        #endregion
    }
}