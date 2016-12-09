using System;

namespace WindowsInputControl.Hooks
{
    /// <summary>
    /// 
    /// </summary>
    enum KeyEventType 
    {
        /// <summary>
        /// Key down
        /// </summary>
        WM_KEYDOWN = 0x0100,

        /// <summary>
        /// Key up
        /// </summary>
        WM_KEYUP = 0x0101,

        /// <summary>
        /// System key down
        /// </summary>
        WM_SYSKEYDOWN = 0x0104,

        /// <summary>
        /// System key up
        /// </summary>
        WM_SYSKEYUP = 0x0105
    }
}