using System.Net.Mail;
using WindowsInputControl.Hooks;
using WindowsInputControl.Keyboards;
using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl
{
    /// <summary>
    ///     The service contract for a keyboard simulator for the Windows platform.
    /// </summary>
    public interface IKeyboardInputControl
    {

        void KeyPress(VirtualKey virtualKey, ScanCode scanCode);


        void KeyPress(VirtualKey virtualKey);
        void KeyPress(VirtualKey virtualKey, IKeyboard keyboard);


        void KeyPress(ScanCode scanCode);
        void KeyPress(ScanCode scanCode, IKeyboard keyboard);

   

        void KeyAction(VirtualKey virtualKey, ScanCode scanCode, KeyAction keyAction);


        void KeyAction(VirtualKey virtualKey, KeyAction keyAction);
        void KeyAction(VirtualKey virtualKey, KeyAction keyAction, IKeyboard keyboard);

         
        void KeyAction(ScanCode scanCode, KeyAction keyAction);
        void KeyAction(ScanCode scanCode, KeyAction keyAction, IKeyboard keyboard);



        void Send(VirtualKey virtualKey);
        void Send(VirtualKey virtualKey, KeyAction keyAction);

        void Send(ScanCode scanCode);
        void Send(ScanCode scanCode, KeyAction keyAction);

        void Send(ushort unicodeCharacter);
        void Send(ushort unicodeCharacter, KeyAction keyAction);

    }
}