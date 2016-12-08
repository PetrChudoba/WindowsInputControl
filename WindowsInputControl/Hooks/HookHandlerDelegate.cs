using System;
using WindowsInputControl.Native;

namespace WindowsInputControl.Hooks
{
    internal delegate IntPtr HookHandlerDelegate(int nCode, KeyEventType wParam, KeyboardHook lParam);
}