using System;
using System.Collections.Generic;
using System.Text;
using WindowsInputControl.NativeMethods;

namespace WindowsInputControl.Keyboards
{
    /// <summary>
    ///     Class WindowsKeyboardsControl.
    /// </summary>
    public class WindowsKeyboardsControl : IWindowsKeyboardsControl
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

        public IEnumerable<IKeyboard> GetAllInstalledLayouts()
        {
            List<IKeyboard> keyboards = new List<IKeyboard>();

            int nElements = KeyboardsControl.GetKeyboardLayoutList(0, null);
            IntPtr[] ids = new IntPtr[nElements];
            KeyboardsControl.GetKeyboardLayoutList(ids.Length, ids);


            foreach (var keybdHandle in ids)
            {
                string keybdIdentifier = GetLayoutIdentifier(keybdHandle);

                IKeyboard keybd = new WindowsKeyboard(keybdHandle, keybdIdentifier);

                keyboards.Add(keybd);
            }

            return keyboards;
        }


        public IKeyboard GetActiveLayout()
        {
            //Find thread
            IntPtr fore = WindowInfo.GetForegroundWindow();
            int tpid = WindowInfo.GetWindowThreadProcessId(fore, IntPtr.Zero);


            //Get handle and identifier for active layout
            IntPtr layoutHandle = KeyboardsControl.GetKeyboardLayout(tpid);
            string layoutIdentifier = GetLayoutIdentifier(layoutHandle);


            IKeyboard kbd = new WindowsKeyboard(layoutHandle, layoutIdentifier);

            return kbd;
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


        private IntPtr GetActiveLayoutHandle()
        {
            IntPtr layout = KeyboardsControl.GetKeyboardLayout(0);

            return layout;
        }

        #endregion
    }
}