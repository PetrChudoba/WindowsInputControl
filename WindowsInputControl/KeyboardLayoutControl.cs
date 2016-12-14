using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WindowsInputControl.Hooks
{



    /// <summary>
    /// Class KeyboardLayoutControl.
    /// </summary>
    public class KeyboardLayoutControl
    {
        #region Methods

        public IEnumerable<IKeyboardLayout> GetAllInstalledLayouts()
        {
            List<IKeyboardLayout> keyboards = new List<IKeyboardLayout>();

            int nElements = NativeKeyboardMethods.GetKeyboardLayoutList(0, null);
            IntPtr[] ids = new IntPtr[nElements];
            NativeKeyboardMethods.GetKeyboardLayoutList(ids.Length, ids);
            

            foreach(var keybdHandle in ids)
            {
                string keybdIdentifier = GetLayoutIdentifier(keybdHandle);

                IKeyboardLayout keybd = new WindowsKeyboardLayouts(keybdHandle,keybdIdentifier);

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
            IntPtr layoutHandle = NativeKeyboardMethods.GetKeyboardLayout(tpid);
            string layoutIdentifier = GetLayoutIdentifier(layoutHandle);


            IKeyboardLayout kbdLayout = new WindowsKeyboardLayouts(layoutHandle, layoutIdentifier);

            return kbdLayout;

        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetActiveLayoutIdentifier()
        {
            var keyboardName = new StringBuilder(NativeKeyboardMethods.KeyboardNameLength);
            NativeKeyboardMethods.GetKeyboardLayoutName(keyboardName);

            return keyboardName.ToString();
        }



        public IntPtr GetActiveLayoutHandle()
        {
            IntPtr layout = NativeKeyboardMethods.GetKeyboardLayout(0);

            return layout;
           
        }

        #endregion

        #region Private methods

        private string GetLayoutIdentifier(IntPtr keyboardHandle)
        {
            //Get current layout
            IntPtr layout = NativeKeyboardMethods.GetKeyboardLayout(0);


            NativeKeyboardMethods.ActivateKeyboardLayout(keyboardHandle, 0x00000100);

            var keyboardName = new StringBuilder(NativeKeyboardMethods.KeyboardNameLength);
            NativeKeyboardMethods.GetKeyboardLayoutName(keyboardName);


            //Set back
            NativeKeyboardMethods.ActivateKeyboardLayout(layout, 0x00000100);


            return keyboardName.ToString();


        }

        #endregion

    }
}