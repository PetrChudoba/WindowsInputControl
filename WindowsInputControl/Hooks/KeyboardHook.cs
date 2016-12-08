using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsInputControl.Helpers;
using WindowsInputControl.Native;

namespace WindowsInputControl.Hooks
{
    //https://msdn.microsoft.com/en-us/library/windows/desktop/ms644967(v=vs.85).aspx



    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardHook
    {
        /*
        DWORD vkCode;
        DWORD scanCode;
        DWORD flags;
        DWORD time;
        ULONG_PTR dwExtraInfo;
        */


        public uint VkCode;

        public uint ScanCode;

        public uint Flags;

        public uint Time;

        public IntPtr DwExtraInfo;


        public bool IsExtended
        {
            get { return IsBitSet( Flags, 0); }   
        }

        public bool IsAltPressed
        {
            get { return IsBitSet(Flags, 5); }
        }


        public bool IsUpEvent
        {
            get {  return IsBitSet(Flags,7); }
        }

        public bool IsDownEvent
        {
            get { return !IsUpEvent; }
        }




        bool IsBitSet(uint b, int pos)
        {  

            return (b & (1 << pos)) != 0;
        }

    
        public override string ToString()
        {
            return $"vk {VkCode}, sc {ScanCode} , IsUp {IsUpEvent}, Is Extended{IsExtended}, IsAlt {IsAltPressed}";
        }
    }
}
