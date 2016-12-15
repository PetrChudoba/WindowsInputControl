using System;

namespace WindowsInputControl.WindowsHooks.Keyboard
{
    /// <summary>
    ///     Delegate KeyboardHookHandlerDelegate.
    ///     <seealso cref="https://msdn.microsoft.com/cs-cz/library/windows/desktop/ms644985(v=vs.85).aspx " />
    /// </summary>
    /// <param name="nCode">The n code.</param>
    /// <param name="keyType">Type of the key.</param>
    /// <param name="keyArgs">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
    /// <returns>IntPtr.</returns>
    internal delegate IntPtr KeyboardHookHandlerDelegate(int nCode, KeyEventType keyType, KeyEventArgs keyArgs);
}