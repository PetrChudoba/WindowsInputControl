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
            IntPtr layout = KeyboardsControl.GetKeyboardLayout(0);


            KeyboardsControl.ActivateKeyboardLayout(keyboardHandle, 0x00000100);

            var keyboardName = new StringBuilder(KeyboardsControl.KeyboardNameLength);
            KeyboardsControl.GetKeyboardLayoutName(keyboardName);


            //Set back
            KeyboardsControl.ActivateKeyboardLayout(layout, 0x00000100);


            return keyboardName.ToString();
        }

        #endregion

        #region Methods

        public IEnumerable<IKeyboardLayout> GetAllInstalledLayouts()
        {
            List<IKeyboardLayout> keyboards = new List<IKeyboardLayout>();

            int nElements = KeyboardsControl.GetKeyboardLayoutList(0, null);
            IntPtr[] ids = new IntPtr[nElements];
            KeyboardsControl.GetKeyboardLayoutList(ids.Length, ids);


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
            IntPtr fore = WindowInfo.GetForegroundWindow();
            int tpid = WindowInfo.GetWindowThreadProcessId(fore, IntPtr.Zero);


            //Get handle and identifier for active layout
            IntPtr layoutHandle = KeyboardsControl.GetKeyboardLayout(tpid);
            string layoutIdentifier = GetLayoutIdentifier(layoutHandle);


            IKeyboardLayout kbdLayout = new WindowsKeyboardLayout(layoutHandle, layoutIdentifier);

            return kbdLayout;
        }


        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string GetActiveLayoutIdentifier()
        {
            var keyboardName = new StringBuilder(KeyboardsControl.KeyboardNameLength);
            KeyboardsControl.GetKeyboardLayoutName(keyboardName);

            return keyboardName.ToString();
        }


        public IntPtr GetActiveLayoutHandle()
        {
            IntPtr layout = KeyboardsControl.GetKeyboardLayout(0);

            return layout;
        }

        #endregion
    }
}