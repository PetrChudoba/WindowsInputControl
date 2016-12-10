using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace WindowsInputControl.Native
{

    /// <summary>
    /// The KeyboardInput structure contains information about a simulated keyboard event.  (see: http://msdn.microsoft.com/en-us/library/ms646271(VS.85).aspx)
    /// Declared in Winuser.h, include Windows.h
    /// </summary>
    /// <remarks>
    /// Windows 2000/XP: INPUT_KEYBOARD supports nonkeyboard-input methodssuch as handwriting recognition or voice recognitionas if it were text input by using the KEYEVENTF_UNICODE flag. If KEYEVENTF_UNICODE is specified, SendInput sends a WM_KEYDOWN or WM_KEYUP message to the foreground thread's message queue with wParam equal to VK_PACKET. Once GetMessage or PeekMessage obtains this message, passing the message to TranslateMessage posts a WM_CHAR message with the Unicode character originally specified by wScan. This Unicode character will automatically be converted to the appropriate ANSI value if it is posted to an ANSI window.
    /// Windows 2000/XP: Set the KEYEVENTF_SCANCODE flag to define keyboard input in terms of the scan code. This is useful to simulate a physical keystroke regardless of which keyboard is currently being used. The virtual key value of a key may alter depending on the current keyboard layout or what other keys were pressed, but the scan code will always be the same.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct KeyboardInput
    {
        /// <summary>
        /// Specifies a virtual-key code. The code must be a value in the range 1 to 254. The Winuser.h header file provides macro definitions (VK_*) for each value. If the dwFlags member specifies KEYEVENTF_UNICODE, wVk must be 0. 
        /// 
        /// mapped to WORD      wVk;
        /// </summary>
        public VirtualKeyCode KeyCode;

        /// <summary>
        /// Specifies a hardware scan code for the key. If dwFlags specifies KEYEVENTF_UNICODE, wScan specifies a Unicode character which is to be sent to the foreground application. 
        /// 
        /// mapped to WORD      wScan;
        /// </summary>
        public ushort ScanCode;

        /// <summary>
        /// Specifies various aspects of a keystroke. This member can be certain combinations of the following values.
        /// KEYEVENTF_EXTENDEDKEY - If specified, the scan code was preceded by a prefix byte that has the value 0xE0 (224).
        /// KEYEVENTF_KEYUP - If specified, the key is being released. If not specified, the key is being pressed.
        /// KEYEVENTF_SCANCODE - If specified, wScan identifies the key and wVk is ignored. 
        /// KEYEVENTF_UNICODE - Windows 2000/XP: If specified, the system synthesizes a VK_PACKET keystroke. The wVk parameter must be zero. This flag can only be combined with the KEYEVENTF_KEYUP flag. For more information, see the Remarks section. 
        /// 
        /// mapped  to  :   DWORD     dwFlags;
        /// </summary>
        public KeyboardFlag Flags;

        /// <summary>
        /// Time stamp for the event, in milliseconds. If this parameter is zero, the system will provide its own time stamp. 
        /// 
        /// mapped to :  DWORD     time;
        /// </summary>
        public uint Time;

        /// <summary>
        /// Specifies an additional value associated with the keystroke. Use the GetMessageExtraInfo function to obtain this information. 
        /// 
        /// mapped to :  ULONG_PTR dwExtraInfo;
        /// </summary>
        public IntPtr ExtraInfo;





        public KeyboardInput(ushort scanCode, bool extended, ushort virtualKey, bool isUp)
        {

            this.ScanCode = scanCode;
            this.KeyCode = (VirtualKeyCode) virtualKey;
            this.Flags = 0;
            this.Time = 0;
            this.ExtraInfo = IntPtr.Zero;


            if (extended)
            {

                this.ScanCode |= 0xE000;
                this.Flags |= KeyboardFlag.ExtendedKey;
            }

            if (isUp)
            {
                this.Flags |= KeyboardFlag.KeyUp;
            }

                
        }



        public void SetExtended()
        {
            Flags |=  KeyboardFlag.ExtendedKey;
         
        }


        public void SetKeyUp()
        {
            Flags |=  KeyboardFlag.KeyUp;
        }

        public void SetScanCode(ushort scanCode)
        {

            ushort values = (ushort) (scanCode & 0xFF00);

            if ( (scanCode & 0xFF00) == 0xE000)
            {
                Flags |=  KeyboardFlag.ScanCode; 
            } 
           
            
            this.ScanCode = scanCode;
        }

        public bool IsKeyUpEvent
        {
            get { return IsSetFlag(KeyboardFlag.KeyUp); }
        }

        public bool IsKeyDownEvent
        {
            get { return !IsSetFlag(KeyboardFlag.KeyUp); }
        }


        private bool IsSetFlag(KeyboardFlag flag)
        {
           return (Flags & flag) != 0;
        }

       
        

        public override string ToString()
        {

            return $" Vk {KeyCode}, Sc {ScanCode}, flags {Flags} ";
            
        }
    }

}
