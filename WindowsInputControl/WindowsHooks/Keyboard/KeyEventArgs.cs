using System;
using System.Runtime.InteropServices;
using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl.WindowsHooks.Keyboard
{
    /// <summary>
    ///     Struct KeyEventArgs <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms644967(v=vs.85).aspx" />
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class KeyEventArgs
    {

        #region Fields

        /// <summary>
        ///     The virtual key code
        ///     DWORD vkCode;
        /// </summary>
        private uint _virtualKey;

        /// <summary>
        ///     The scan code
        ///     DWORD scanCode;
        /// </summary>
        private uint _sccanCode;


        /// <summary>
        ///     The flags
        ///     DWORD flags;
        /// </summary>
        public uint Flags;


        /// <summary>
        ///     The time
        ///     DWORD time;
        /// </summary>
        public uint Time;


        /// <summary>
        ///     The dw extra information
        ///     ULONG_PTR dwExtraInfo;
        /// </summary>
        public IntPtr DwExtraInfo;

        #endregion


        #region Properties

        public VirtualKey VirtualKey
        {
            get { return (VirtualKey) _virtualKey; }
        }
        

        public ScanCode ScanCode
        {
            get
            {
                return new ScanCode((byte) _sccanCode, IsExtended);
            }
        }

    
        public bool IsExtended
        {
            get { return IsBitSet(Flags, 0); }
        }

        public bool IsAltPressed
        {
            get { return IsBitSet(Flags, 5); }
        }


        public bool IsUpEvent
        {
            get { return IsBitSet(Flags, 7); }
        }

        public bool IsDownEvent
        {
            get { return !IsUpEvent; }
        }

        public KeyEventType Type
        {
            get
            {
                if (IsAltPressed)
                {
                    return IsDownEvent ? KeyEventType.WM_SYSKEYDOWN : KeyEventType.WM_SYSKEYUP;
                }

                return IsDownEvent ? KeyEventType.WM_KEYDOWN : KeyEventType.WM_KEYUP;
            }
        }


        public KeyAction KeyAction
        {
            get { return IsUpEvent ? KeyAction.Up : KeyAction.Down; }
        }


        private bool IsBitSet(uint b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        

        #endregion


        #region Methods

        public override string ToString()
        {


            return (IsUpEvent?"UP":"DOWN") + $" {VirtualKey} x {ScanCode.Code} + {IsExtended}";
        }

        #endregion

    }
}