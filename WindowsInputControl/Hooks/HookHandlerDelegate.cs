using System;
using WindowsInputControl.Native;

namespace WindowsInputControl.Hooks
{
    //https://msdn.microsoft.com/cs-cz/library/windows/desktop/ms644985(v=vs.85).aspx

    internal delegate IntPtr HookHandlerDelegate(int nCode, KeyEventType wParam, KeyboardHook lParam);
}