using System;

namespace WindowsInputControl.Native
{
    /// <summary>
    /// Specifies various aspects of a keystroke. This member can be certain combinations of the following values.
    /// </summary>
    [Flags]
    public enum KeyboardFlag : uint // UInt32
    {
        /// <summary>
        /// KEYEVENTF_EXTENDEDKEY = 0x0001 (If specified, the scan code was preceded by a prefix byte that has the value 0xE0 (224).)
        /// </summary>
        ExtendedKey = 1,

        /// <summary>
        /// KEYEVENTF_KEYUP = 0x0002 (If specified, the key is being released. If not specified, the key is being pressed.)
        /// </summary>
        KeyUp = 2,

        /// <summary>
        /// KEYEVENTF_UNICODE = 0x0004 (If specified, wScan identifies the key and wVk is ignored.)
        /// </summary>
        Unicode = 4,

        /// <summary>
        /// KEYEVENTF_SCANCODE = 0x0008 (Windows 2000/XP: If specified, the system synthesizes a VK_PACKET keystroke. The wVk parameter must be zero. This flag can only be combined with the KEYEVENTF_KEYUP flag. For more information, see the Remarks section.)
        /// </summary>
        ScanCode = 8,
    }




    /// <summary>
    /// Extensions methods.
    /// </summary>
    internal static class Extensions
    {
        //NOTE: Extension methods for an Enumeration could be useful. Maybe I should write an article about it?





        public static bool IsSet(this KeyboardFlag flag,uint flags)
        {
            return (flags & (uint) flag) != 0;
        }

        public static uint SetFlag(this KeyboardFlag flag,  uint flags)
        {

            return flags | (uint) flag;
        }

        public static void SetFlag(this KeyboardFlag flag, ref uint flags)
        {
            flags = flags | (uint) flag;
        }


        //TODO: RemoveFlag(...)
    }
}
