using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using WindowsInputControl.NativeMethods;
using WindowsInputControl.WindowsHooks.Keyboard;
using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl.Hooks
{
    public class KeyboardLogger
    {
        private const int WH_KEYBOARD_LL = 13;


        private IInputMessageDispatcher _dispatcher = new WindowsInputMessageDispatcher();

        private IList<KeyEventArgs> _events = new List<KeyEventArgs>();

        private bool _isReplaying = false;
        private KeyEventArgs _lastEvent;
        private IntPtr hookID = IntPtr.Zero;

        private KeyboardHookHandlerDelegate proc;


        public KeyboardLogger()
        {
            proc = new KeyboardHookHandlerDelegate(HookCallback);
        }

        public IEnumerable<KeyEventArgs> KeyEvents
        {
            get { return _events; }
        }

        public KeyEventArgs LastKey
        {
            get { return _lastEvent; }
        }

        public void SetHook()
        {
            hookID = KeyboardHooks.SetWindowsHookEx(WH_KEYBOARD_LL, proc, IntPtr.Zero, 0);
        }

        public bool RemoveHook()
        {
            return KeyboardHooks.UnhookWindowsHookEx(hookID);
        }


        private IntPtr HookCallback(int nCode, KeyEventType wParam, KeyEventArgs lParam)
        {
            Debug.WriteLine(wParam.ToString() + " " + lParam.ToString());


            if (wParam == KeyEventType.WM_SYSKEYDOWN)
                if (lParam.IsAltPressed && lParam.IsDownEvent)
                    Debug.WriteLine("GOOD");

            if (wParam == KeyEventType.WM_SYSKEYUP)
                if (lParam.IsAltPressed && lParam.IsUpEvent)
                    Debug.WriteLine("GOOD");


            if (wParam == KeyEventType.WM_KEYUP)
            {
                Debug.WriteLine(lParam.Flags);
                if (!lParam.IsAltPressed && lParam.IsUpEvent)
                    Debug.WriteLine("GOOD");
            }


            if (wParam == KeyEventType.WM_KEYDOWN)
                if (!lParam.IsAltPressed && lParam.IsDownEvent)
                    Debug.WriteLine("GOOD");


            if (!_isReplaying)
            {
                _events.Add(lParam);
                _lastEvent = lParam;
            }

            return KeyboardHooks.CallNextHookEx(hookID, nCode, wParam, lParam);
        }

        public void Replay()
        {
            _isReplaying = true;

            KeyboardSimulator simik = new KeyboardSimulator(new InputSimulator());

            foreach (var evn in _events)
            {
                ScanCode sc = new ScanCode((ushort) evn.ScanCode);


                simik.Send(evn.KeyAction, sc, evn.VirtualKey);
                simik.Sleep(500);
            }


            _isReplaying = false;
        }

        public void Dispose()
        {
            KeyboardHooks.UnhookWindowsHookEx(hookID);
        }
    }
}