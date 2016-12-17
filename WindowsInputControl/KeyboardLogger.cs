using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using WindowsInputControl.NativeMethods;
using WindowsInputControl.WindowsHooks.Keyboard;
using WindowsInputControl.WindowsInputs;
using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl.Hooks
{

    public delegate void ChangedEventHandler<TItem>(object sender, ChangedEventArgs<TItem> e);


    public class ChangedEventArgs<TItem> : EventArgs
    {
        private readonly TItem _item;

        public ChangedEventArgs(TItem item)
        {
            _item = item;
        }

        public TItem Item
        {
            get { return _item; }
        }
    }

    public class KeyboardLogger
    {
        private const int WH_KEYBOARD_LL = 13;


        private IInputMessageDispatcher _dispatcher = new WindowsInputMessageDispatcher();

        private IList<KeyEventArgs> _events = new List<KeyEventArgs>();


        private bool _isLogging = false;


        private KeyEventArgs _lastEvent;
        private IntPtr hookID = IntPtr.Zero;

        private KeyboardHookHandlerDelegate proc;

        private readonly CollectionNotifier<KeyEventArgs> _keyEventsNotifier = new CollectionNotifier<KeyEventArgs>();


        public KeyboardLogger()
        {
            proc = new KeyboardHookHandlerDelegate(HookCallback);
           
        }


        public IEnumerable<KeyEventArgs> KeyEvents
        {
            get { return _events; }
        }


        public ICollectionNotifier<KeyEventArgs> KeyEventsNotifier
        {
            get { return _keyEventsNotifier; }
        }


        public int KeyEventCount
        {
            get { return _events.Count; }
        }

        public KeyEventArgs LastKeyEvent
        {
            get { return _lastEvent; }
        }


        public bool IsLogging
        {
            get { return _isLogging; }

            set
            {
                if (_isLogging == value) 
                    return;

                if (value)
                {
                    SetHook();
                }
                else
                {
                    RemoveHook();
                }
            }
        }


        public void SetHook()
        { 

            hookID = KeyboardHooks.SetWindowsHookEx(WH_KEYBOARD_LL, proc, IntPtr.Zero, 0);

            _isLogging = true;
        }

        public bool RemoveHook()
        {

            _isLogging =  KeyboardHooks.UnhookWindowsHookEx(hookID);

            return _isLogging;
        }


        private IntPtr HookCallback(int nCode, KeyEventType wParam, KeyEventArgs lParam)
        {
            Debug.WriteLine(lParam);

            Contract.Assert(wParam == lParam.Type);
            
            _events.Add(lParam);
            
            _lastEvent = lParam;



            _keyEventsNotifier.OnAdded(lParam);




            return KeyboardHooks.CallNextHookEx(hookID, nCode, wParam, lParam);
        }


        public void Dispose()
        {
            KeyboardHooks.UnhookWindowsHookEx(hookID);
        }



    }




    public class KeyboardEventsPlayer
    {
        private IKeyboardInputControl _keybdControl;


        public KeyboardEventsPlayer()
        {
            _keybdControl = new KeyboardInputControl();
        }


        public void Replay(IEnumerable<KeyEventArgs> keyEvents)
        {
            foreach (var keyEvent in keyEvents)
            {
                _keybdControl.KeyAction(keyEvent.VirtualKey, keyEvent.ScanCode, keyEvent.KeyAction);
            }

        }

    }



    public class KeyboardMacroControl
    {
        private readonly KeyboardLogger _logger;
        private readonly KeyboardEventsPlayer _player;

        public KeyboardMacroControl(KeyboardLogger logger, KeyboardEventsPlayer player)
        {
            _logger = logger;
            _player = player;
        }


        public int StepsCount
        {
            get { return _logger.KeyEventCount; }
        }

        public void StartRecording()
        {
            _logger.SetHook();
        }

        public void StopRecording()
        {
            _logger.RemoveHook();
        }


        public void ExecuteMacro()
        {
            var events = _logger.KeyEvents;


            _player.Replay(events);


        }

    }





    public interface ICollectionRemoveListener<T>
    {
        void OnRemove(T type);
    }

    public interface ICollectionAddListener<T>
    {
        void OnAdded(T type);
    }

    public interface ICollectionListener<T>
    {

        void OnChange(T type, ChangedType changedTy);


    }


    public interface ICollectionNotifier<T>
    {
        void Register(ICollectionListener<T> listener);


        void RegisterRemove(ICollectionRemoveListener<T> action);


        void RegisterAdd(ICollectionAddListener<T> action);


        // void Cleared(Action<T> action);
    }


    public class CollectionNotifier<T> : ICollectionNotifier<T>
    {
        private ICollectionListener<T> _listener;
        private ICollectionRemoveListener<T> _remListener;
        private ICollectionAddListener<T> _addListener;

        public void Register(ICollectionListener<T> listener)
        {
            _listener = listener;
        }

        public void RegisterRemove(ICollectionRemoveListener<T> action)
        {
            _remListener = action;
        }

        public void RegisterAdd(ICollectionAddListener<T> action)
        {
            _addListener = action;
        }


        public void OnRemove(T item)
        {

            if (_remListener != null)
            {
                _remListener.OnRemove(item);
            }

            if (_listener != null)
            {
                _listener.OnChange(item, ChangedType.Remove);
            }

        }

        public void OnAdded(T item)
        {

            if (_addListener != null)
            {
                _addListener.OnAdded(item);
            }

            if (_listener != null)
            {
                _listener.OnChange(item, ChangedType.Add);
            }

        }
    }


    public enum ChangedType
    {
        Add, Remove
    }
}