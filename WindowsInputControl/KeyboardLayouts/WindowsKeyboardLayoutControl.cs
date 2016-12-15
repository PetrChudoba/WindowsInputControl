using System;
using System.Collections.Generic;
using System.Text;
using WindowsInputControl.NativeMethods;

namespace WindowsInputControl.KeyboardLayouts
{
    /// <summary>
    ///     Class WindowsKeyboardLayoutControl.
    /// </summary>
    public class WindowsKeyboardLayoutControl
    {
        #region Private methods

        private string GetLayoutIdentifier(IntPtr keyboardHandle)
        {
            //Get current layout
            IntPtr layout = NativeKeyboardLayoutControls.GetKeyboardLayout(0);


            NativeKeyboardLayoutControls.ActivateKeyboardLayout(keyboardHandle, 0x00000100);

            var keyboardName = new StringBuilder(NativeKeyboardLayoutControls.KeyboardNameLength);
            NativeKeyboardLayoutControls.GetKeyboardLayoutName(keyboardName);


            //Set back
            NativeKeyboardLayoutControls.ActivateKeyboardLayout(layout, 0x00000100);


            return keyboardName.ToString();
        }

        #endregion

        #region Methods

        public IEnumerable<IKeyboardLayout> GetAllInstalledLayouts()
        {
            List<IKeyboardLayout> keyboards = new List<IKeyboardLayout>();

            int nElements = NativeKeyboardLayoutControls.GetKeyboardLayoutList(0, null);
            IntPtr[] ids = new IntPtr[nElements];
            NativeKeyboardLayoutControls.GetKeyboardLayoutList(ids.Length, ids);


            foreach (var keybdHandle in ids)
            {
                string keybdIdentifier = GetLayoutIdentifier(keybdHandle);

                IKeyboardLayout keybd = new WindowsKeyboardLayout(keybdHandle, keybdIdentifier);

                keyboards.Add(keybd);
            }

            return keyboards;
        }


        public IKeyboardLayout GetActiveLayout()
        {
            //Find thread
            IntPtr fore = NativeWindowInfo.GetForegroundWindow();
            int tpid = NativeWindowInfo.GetWindowThreadProcessId(fore, IntPtr.Zero);


            //Get handle and identifier for active layout
            IntPtr layoutHandle = NativeKeyboardLayoutControls.GetKeyboardLayout(tpid);
            string layoutIdentifier = GetLayoutIdentifier(layoutHandle);


            IKeyboardLayout kbdLayout = new WindowsKeyboardLayout(layoutHandle, layoutIdentifier);

            return kbdLayout;
        }


        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string GetActiveLayoutIdentifier()
        {
            var keyboardName = new StringBuilder(NativeKeyboardLayoutControls.KeyboardNameLength);
            NativeKeyboardLayoutControls.GetKeyboardLayoutName(keyboardName);

            return keyboardName.ToString();
        }


        public IntPtr GetActiveLayoutHandle()
        {
            IntPtr layout = NativeKeyboardLayoutControls.GetKeyboardLayout(0);

            return layout;
        }

        #endregion
    }
}