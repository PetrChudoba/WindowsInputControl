using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using WindowsInputControl.Native;
using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl.Hooks
{
    public class KeyboardLogger
    {

        private KeyboardHookHandlerDelegate proc;
        private IntPtr hookID = IntPtr.Zero;
        private const int WH_KEYBOARD_LL = 13;


        private IInputMessageDispatcher _dispatcher = new WindowsInputMessageDispatcher();

       private IList<KeyEventArgs> _events = new List<KeyEventArgs>();
        private KeyEventArgs _lastEvent;

        public IEnumerable<KeyEventArgs> KeyEvents { get { return _events; } }

        public KeyEventArgs LastKey { get { return _lastEvent; } }


        public KeyboardLogger()
        {
            proc = new KeyboardHookHandlerDelegate(HookCallback);
        }

        public void SetHook()
        {
            hookID = NativeKeyboardHooks.SetWindowsHookEx(WH_KEYBOARD_LL, proc, IntPtr.Zero, 0);
        }

        public bool RemoveHook()
        {
            return NativeKeyboardHooks.UnhookWindowsHookEx(hookID);
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


           

            if (!_isReplaying)
            {
                _events.Add(lParam);
                _lastEvent = lParam;
            }
         
          return NativeKeyboardHooks.CallNextHookEx(hookID, nCode, wParam, lParam);
            

        }

        private bool _isReplaying = false;

        public void Replay()
        {
            _isReplaying = true;

          KeyboardSimulator simik = new KeyboardSimulator(new InputSimulator());

            foreach (var evn in _events)
            {
                

                ScanCode sc = new ScanCode((ushort) evn.ScanCode);
                


                simik.Send(evn.KeyAction, sc, evn.VirtualKeyCode);
                simik.Sleep(500);


            }


            _isReplaying = false;

        }

        public void Dispose()
        {
            NativeKeyboardHooks.UnhookWindowsHookEx(hookID);
        }
    }
}