using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using WindowsInputControl.Native;

namespace WindowsInputControl.Hooks
{
    public class KeyboardLogger
    {

        private KeyboardHookHandlerDelegate proc;
        private IntPtr hookID = IntPtr.Zero;
        private const int WH_KEYBOARD_LL = 13;


        private IInputMessageDispatcher _dispatcher = new WindowsInputMessageDispatcher();

       private IList<KeyEventArgs> _events = new List<KeyEventArgs>();

        public IEnumerable<KeyEventArgs> KeyEvents { get { return _events; } }


        public KeyboardLogger()
        {
            proc = new KeyboardHookHandlerDelegate(HookCallback);
        }

        public void SetHook()
        {
            hookID = NativeWindowsHookMethods.SetWindowsHookEx(WH_KEYBOARD_LL, proc, IntPtr.Zero, 0);
        }

        public bool RemoveHook()
        {
            return NativeWindowsHookMethods.UnhookWindowsHookEx(hookID);
        }


        private IntPtr HookCallback(int nCode, KeyEventType wParam, KeyEventArgs lParam)
        {

            Debug.WriteLine(wParam.ToString() +  " "  + lParam.ToString());
            

            if (wParam == KeyEventType.WM_SYSKEYDOWN)
            {
                if (lParam.IsAltPressed && lParam.IsDownEvent)
                {
                    Debug.WriteLine("GOOD");
                }
            }

            if (wParam == KeyEventType.WM_SYSKEYUP)
            {
                if (lParam.IsAltPressed && lParam.IsUpEvent)
                {
                    Debug.WriteLine("GOOD");
                }
            }


            if (wParam == KeyEventType.WM_KEYUP)
            {
                Debug.WriteLine(lParam.Flags);
                if (!lParam.IsAltPressed && lParam.IsUpEvent)
                {
                    Debug.WriteLine("GOOD");
                }
            }
            

            if (wParam == KeyEventType.WM_KEYDOWN)
            {

                if (!lParam.IsAltPressed && lParam.IsDownEvent)
                {
                    Debug.WriteLine("GOOD");
                }
            }


            _events.Add(lParam);


            // Debug.WriteLine("computed vk  {0}, Vk {1}, Sc {2} and flags {3} and {4}", getVk, lParam.vkCode, lParam.scanCode, lParam.flags, lParam.dwExtraInfo);


            return IntPtr.Zero;
            //The event wasn't handled, pass it to next application
            return NativeWindowsHookMethods.CallNextHookEx(hookID, nCode, wParam, lParam);
            

        }

        public void Replay()
        {

            /*
            
            
            List<KeyboardInput> inas = new List<KeyboardInput>();

            foreach (var evn in _events)
            {
                
                KeyboardInput ina  = new KeyboardInput();

                ina.KeyCode = (ushort) evn.VirtualKey;
                ina.ScanCode =(ushort) evn.ScanCode;

                if (evn.IsExtended)
                {
                    ina.SetExtended();
                }

                if (evn.IsUpEvent)
                {
                    ina.SetKeyUp();
                }

                inas.Add(ina);
;

            }


            _dispatcher.DispatchKeyboardInputs(inas);

            */
    

            
            IInputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_E);
        }

        public void Dispose()
        {
            NativeWindowsHookMethods.UnhookWindowsHookEx(hookID);
        }
    }
}