using System;
using System.Diagnostics.Contracts;
using WindowsInputControl.Helpers;

namespace WindowsInputControl.Hooks
{
    internal class WindowsKeyboardLayouts : IKeyboardLayout
    {
        #region Fields

        private readonly IntPtr _keyboardHandle;
        private readonly string _keyboardIdentifier;

        #endregion

        #region Ctor
        
        public WindowsKeyboardLayouts(IntPtr keyboardHandle, string keyboardIdentifier)
        {
          
            Contract.Requires(keyboardHandle != null);
            Contract.Requires(!string.IsNullOrEmpty(keyboardIdentifier));
            Contract.Requires(keyboardIdentifier.Length == 8);

            _keyboardHandle = keyboardHandle;
            _keyboardIdentifier = keyboardIdentifier;
        }

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

        public ushort GetVirtualKey(ushort scanCode)
        {
            return (ushort) NativeKeyboardMethods.MapVirtualKeyEx(scanCode, 3, _keyboardHandle);
        }

        #endregion
    }
}