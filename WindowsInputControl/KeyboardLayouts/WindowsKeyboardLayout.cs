using System;
using System.Diagnostics.Contracts;
using WindowsInputControl.Helpers;
using WindowsInputControl.NativeMethods;
using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl.KeyboardLayouts
{
    /// <summary>
    /// Class WindowsKeyboardLayout.
    /// </summary>
    /// <seealso cref="WindowsInputControl.KeyboardLayouts.IKeyboardLayout" />
    internal class WindowsKeyboardLayout : IKeyboardLayout
    {

        #region Fields

        /// <summary>
        /// The keyboard handle
        /// </summary>
        private readonly IntPtr _keyboardHandle;
        /// <summary>
        /// The keyboard identifier
        /// </summary>
        private readonly string _keyboardIdentifier;

        #endregion


        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsKeyboardLayout"/> class.
        /// </summary>
        /// <param name="keyboardHandle">The keyboard handle.</param>
        /// <param name="keyboardIdentifier">The keyboard identifier.</param>
        public WindowsKeyboardLayout(IntPtr keyboardHandle, string keyboardIdentifier)
        {
            Contract.Requires(keyboardHandle != null);
            Contract.Requires(!string.IsNullOrEmpty(keyboardIdentifier));
            Contract.Requires(keyboardIdentifier.Length == 8);

            _keyboardHandle = keyboardHandle;
            _keyboardIdentifier = keyboardIdentifier;
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Identifier
        {
            get { return _keyboardIdentifier; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return ServiceLocator.WindowsKeyboardLayoutNames.GetLayoutName(_keyboardIdentifier); }
        }

        #endregion


        #region Methods

        /// <summary>
        /// Gets the virtual key for the scan code.
        /// </summary>
        /// <param name="scanCode">The scan code.</param>
        /// <returns>WindowsInputControl.WindowsInputs.Keyboard.VirtualKey.</returns>
        public VirtualKey GetVirtualKey(ScanCode scanCode)
        {
            return (VirtualKey) NativeKeyboardLayout.MapVirtualKeyEx(scanCode.Code, 3, _keyboardHandle);
        }

        /// <summary>
        /// Gets the scan code for the virtal key.
        /// </summary>
        /// <param name="virtualKey">The virtual key.</param>
        /// <returns>WindowsInputControl.WindowsInputs.Keyboard.ScanCode.</returns>
        public ScanCode GetScanCode(VirtualKey virtualKey)
        {
            ushort scanCode = (ushort) NativeKeyboardLayout.MapVirtualKeyEx((int) virtualKey, 4, _keyboardHandle);
            return new ScanCode(scanCode);
        }

        #endregion
    }
}