using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WindowsInputControl.Hooks
{
    /// <summary>
    /// 
    /// </summary>
    public class KeyboardLayoutControl
    {



        /// <summary>
        /// Gets the layouts.
        /// </summary>
        /// <returns>IEnumerable&lt;IKeyboardLayout&gt;.</returns>
        public IEnumerable<IKeyboardLayout> GetLayouts()
        {
            List<IKeyboardLayout> keyboards = new List<IKeyboardLayout>();

            int nElements = NativeKeyboardMethods.GetKeyboardLayoutList(0, null);
            IntPtr[] ids = new IntPtr[nElements];
            NativeKeyboardMethods.GetKeyboardLayoutList(ids.Length, ids);

            

            foreach(var khl in ids)
            {
                string keyboardIdentifier = GetLayoutIdentifier(khl);


            }



            return keyboards;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetLayoutIdentifier()
        {
            var keyboardName = new StringBuilder(NativeKeyboardMethods.KeyboardNameLength);
            NativeKeyboardMethods.GetKeyboardLayoutName(keyboardName);

            return keyboardName.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyboardHandle"></param>
        /// <returns></returns>
        public string GetLayoutIdentifier(IntPtr keyboardHandle)
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

        int GetVirtualCode(int scanCode)
        {
            IntPtr fore = NativeWindowInfoMethods.GetForegroundWindow();
            int tpid = NativeWindowInfoMethods.GetWindowThreadProcessId(fore, IntPtr.Zero);
            IntPtr layout = NativeKeyboardMethods.GetKeyboardLayout(tpid);


            int virtualCode = NativeKeyboardMethods.MapVirtualKeyEx(scanCode, 3, layout);

            return virtualCode;
        }


    }




    public interface IKeyboardLayout
    {
        string Identifier { get; }

        string Name { get;  }

    
        ushort GetVirtualKey(ushort scanCode);
    }


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