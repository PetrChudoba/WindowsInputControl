﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WindowsInputControl.Hooks;
using WindowsInputControl.Native;
using WindowsInputControl.NativeMethods;

namespace WindowsInputControl
{
    /// <summary>
    /// Implements the <see cref="IInputMessageDispatcher"/> by calling <see cref="System.Runtime.InteropServices.NativeMethods.SendInput"/>.
    /// </summary>
    internal class WindowsInputMessageDispatcher : IInputMessageDispatcher
    {
        /// <summary>
        /// Dispatches the specified list of <see cref="Input"/> messages in their specified order by issuing a single called to <see cref="System.Runtime.InteropServices.NativeMethods.SendInput"/>.
        /// </summary>
        /// <param name="inputs">The list of <see cref="Input"/> messages to be dispatched.</param>
        /// <exception cref="ArgumentException">If the <paramref name="inputs"/> array is empty.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="inputs"/> array is null.</exception>
        /// <exception cref="Exception">If the any of the commands in the <paramref name="inputs"/> array could not be sent successfully.</exception>
        public void DispatchInput(Input[] inputs)
        {

            var successful = NativeInputs.SendInput((UInt32)inputs.Length, inputs, Marshal.SizeOf(typeof (Input)));
            if (successful != inputs.Length)
                throw new Exception("Some simulated input commands were not sent successfully. The most common reason for this happening are the security features of Windows including User Interface Privacy Isolation (UIPI). Your application can only send commands to applications of the same or lower elevation. Similarly certain commands are restricted to Accessibility/UIAutomation applications. Refer to the project home page and the code samples for more information.");
        }

        
    }
}