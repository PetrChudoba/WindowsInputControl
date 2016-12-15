using WindowsInputControl.WindowsInputs;
using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl
{
    internal class KeyboardInputBuilder
    {

        public Input GetInput(VirtualKey virtualKey, ScanCode scanCode, KeyAction action)
        {
            return new Input()
            {
                Type = InputType.Keyboard,
                Data = new MouseKeybdHardwareInput()
                {
                    Keyboard = new WindowsInputs.Keyboard.KeyboardInput()
                    {
                        VirtualKey = virtualKey,
                        ScanCode = scanCode,
                        Flags = (action == KeyAction.Up? KeyboardFlag.KeyUp : 0) | 
                                (scanCode.IsExtended?KeyboardFlag.ExtendedKey : 0)
                    }
                }
            };
        }

        public Input GetVirtualKeyInput(VirtualKey virtualKey,KeyAction action)
        {
            return new Input()
            {
                Type = InputType.Keyboard,
                Data = new MouseKeybdHardwareInput()
                {
                    Keyboard = new WindowsInputs.Keyboard.KeyboardInput()
                    {
                        VirtualKey = virtualKey,
                        ScanCode = new ScanCode(),
                        Flags = (action == KeyAction.Up ? KeyboardFlag.KeyUp : 0)
                    }
                }
            };
        }

        public Input GetScanCodeInput(ScanCode scanCode, KeyAction action)
        {
            return new Input()
            {
                Type = InputType.Keyboard,
                Data = new MouseKeybdHardwareInput()
                {
                    Keyboard = new WindowsInputs.Keyboard.KeyboardInput()
                    {
                        VirtualKey = 0,
                        ScanCode = scanCode,
                        Flags = (action == KeyAction.Up ? KeyboardFlag.KeyUp : 0) |
                                (scanCode.IsExtended ? KeyboardFlag.ExtendedKey : 0)
                    }
                }
            };

        }

        public Input GetUnicodeInput(ushort unicodeCharacter, KeyAction action)
        {
            return new Input()
            {
                Type = InputType.Keyboard,
                Data = new MouseKeybdHardwareInput()
                {
                    Keyboard = new WindowsInputs.Keyboard.KeyboardInput()
                    {
                        VirtualKey = 0,
                        ScanCode = new ScanCode(unicodeCharacter),
                        Flags = (action == KeyAction.Up ? KeyboardFlag.KeyUp : 0)  //Do not set Extended key                    
                    }
                }
            };

        }


    }
}