﻿using System.Runtime.InteropServices;

namespace WindowsInputControl.WindowsInputs
{


    //https://msdn.microsoft.com/en-us/library/windows/desktop/ms646271(v=vs.85).aspx


    /// <summary>
    ///     The INPUT structure is used by SendInput to store information for synthesizing input events such as keystrokes,
    ///     mouse movement, and mouse clicks. (see: http://msdn.microsoft.com/en-us/library/ms646270(VS.85).aspx)
    ///     Declared in Winuser.h, include Windows.h
    /// </summary>
    /// <remarks>
    ///     This structure contains information identical to that used in the parameter list of the keybd_event or mouse_event
    ///     function.
    ///     Windows 2000/XP: INPUT_KEYBOARD supports nonkeyboard input methods, such as handwriting recognition or voice
    ///     recognition, as if it were text input by using the KEYEVENTF_UNICODE flag. For more information, see the remarks
    ///     section of KeyboardInput.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct Input
    {
        /// <summary>
        ///     Specifies the type of the input event. This member can be one of the following values.
        ///     <see cref="InputType.Mouse" /> - The event is a mouse event. Use the mi structure of the union.
        ///     <see cref="InputType.Keyboard" /> - The event is a keyboard event. Use the ki structure of the union.
        ///     <see cref="InputType.Hardware" /> - Windows 95/98/Me: The event is from input hardware other than a keyboard or
        ///     mouse. Use the hi structure of the union.
        /// </summary>
        public InputType Type;

        /// <summary>
        ///     The data structure that contains information about the simulated Mouse, Keyboard or Hardware event.
        /// </summary>
        public MouseKeybdHardwareInput Data;
    }

}