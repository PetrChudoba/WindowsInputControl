using WindowsInputControl.Native;
using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl.Hooks
{
    public interface IKeyboardLayout
    {
        string Identifier { get; }

        string Name { get;  }

    
        VirtualKeyCode GetVirtualKey(ScanCode scanCode);
        ScanCode GetScanCode(VirtualKeyCode virtualKey);
    }
}