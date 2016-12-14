using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl.KeyboardLayouts
{
    public interface IKeyboardLayout
    {
        string Identifier { get; }

        string Name { get; }


        VirtualKey GetVirtualKey(ScanCode scanCode);
        ScanCode GetScanCode(VirtualKey virtualKey);
    }
}