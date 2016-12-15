using System;
using System.Collections.Generic;
using System.Threading;
using WindowsInputControl.Hooks;
using WindowsInputControl.Keyboards;
using WindowsInputControl.NativeMethods;
using WindowsInputControl.WindowsInputs;
using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl
{
    /// <summary>
    ///     Implements the <see cref="IKeyboardInputControl" /> interface by calling the an <see cref="IInputMessageDispatcher" />
    ///     to simulate Keyboard gestures.
    /// </summary>
    public class KeyboardInputControl : IKeyboardInputControl
    {
        private IInputMessageDispatcher _messageDispatcher = new WindowsInputMessageDispatcher();

        private KeyboardInputBuilder _builder = new KeyboardInputBuilder();


        public void KeyPress(VirtualKey virtualKey, ScanCode scanCode)
        {
            _messageDispatcher.DispatchInput(
                new Input[]
                {
                    _builder.GetInput(virtualKey,scanCode,WindowsInputControl.KeyAction.Down),
                    _builder.GetInput(virtualKey,scanCode,WindowsInputControl.KeyAction.Up)
                });
        }

        public void KeyPress(VirtualKey virtualKey)
        {
            ScanCode scanCode = getScanCode(virtualKey);

            KeyPress(virtualKey,scanCode);
        }



        public void KeyPress(VirtualKey virtualKey, IKeyboard keyboard)
        {
            ScanCode scanCode = keyboard.GetScanCode(virtualKey);

            KeyPress(virtualKey,scanCode);
        }

        public void KeyPress(ScanCode scanCode)
        {
            VirtualKey virtualKey = getVirtualKey(scanCode);

            KeyPress(virtualKey,scanCode);
        }

        public void KeyPress(ScanCode scanCode, IKeyboard keyboard)
        {
            VirtualKey virtualKey = keyboard.GetVirtualKey(scanCode);

            KeyPress(virtualKey, scanCode);

        }





        public void KeyAction(VirtualKey virtualKey, ScanCode scanCode, KeyAction keyAction)
        {
            _messageDispatcher.DispatchInput(
           new Input[]
           {
                    _builder.GetInput(virtualKey,scanCode,keyAction),
           });
        }

        public void KeyAction(VirtualKey virtualKey, KeyAction keyAction)
        {
            ScanCode scanCode = getScanCode(virtualKey);

            KeyAction(virtualKey,scanCode,keyAction);
        }

        public void KeyAction(VirtualKey virtualKey, KeyAction keyAction, IKeyboard keyboard)
        {
            ScanCode scanCode = keyboard.GetScanCode(virtualKey);

            KeyAction(virtualKey, scanCode, keyAction);
        }

        public void KeyAction(ScanCode scanCode, KeyAction keyAction)
        {
            VirtualKey virtualKey = getVirtualKey(scanCode);

            KeyAction(virtualKey, scanCode, keyAction);

        }

        public void KeyAction(ScanCode scanCode, KeyAction keyAction, IKeyboard keyboard)
        {
            VirtualKey virtualKey = keyboard.GetVirtualKey(scanCode);

            KeyAction(virtualKey,scanCode,keyAction);
        }









        public void Send(VirtualKey virtualKey)
        {
            _messageDispatcher.DispatchInput(
               new Input[]
               {
                    _builder.GetVirtualKeyInput(virtualKey,WindowsInputControl.KeyAction.Down),
                    _builder.GetVirtualKeyInput(virtualKey,WindowsInputControl.KeyAction.Up)
               }
               );
        }

        public void Send(VirtualKey virtualKey, KeyAction keyAction)
        {
            _messageDispatcher.DispatchInput(
                new Input[]
                {
                    _builder.GetVirtualKeyInput(virtualKey,keyAction)
                }
                );

        }

        public void Send(ScanCode scanCode)
        {
            _messageDispatcher.DispatchInput(
                new Input[]
                {
                    _builder.GetScanCodeInput(scanCode,WindowsInputControl.KeyAction.Down),
                    _builder.GetScanCodeInput(scanCode,WindowsInputControl.KeyAction.Up)
                }
                );
        }

        public void Send(ScanCode scanCode, KeyAction keyAction)
        {
            _messageDispatcher.DispatchInput(
               new Input[]
               {
                    _builder.GetScanCodeInput(scanCode,keyAction)
               }
               );
        }

        public void Send(ushort unicodeCharacter)
        {
            _messageDispatcher.DispatchInput(
                new Input[]
                {
                    _builder.GetUnicodeInput(unicodeCharacter,WindowsInputControl.KeyAction.Down),
                    _builder.GetUnicodeInput(unicodeCharacter,WindowsInputControl.KeyAction.Up)
                }
                );
        }

        public void Send(ushort unicodeCharacter, KeyAction keyAction)
        {
            _messageDispatcher.DispatchInput(
                new Input[]
                {
                    _builder.GetUnicodeInput(unicodeCharacter,keyAction)
                }
                );
        }






        #region Private Methods


        private VirtualKey getVirtualKey(ScanCode scanCode)
        {
            return (VirtualKey)KeyboardMapping.MapVirtualKeyEx(scanCode.Code, 3, IntPtr.Zero);

        }

        private ScanCode getScanCode(VirtualKey virtualKey)
        {
            ushort scanCode = (ushort)KeyboardMapping.MapVirtualKeyEx((int)virtualKey, 4, IntPtr.Zero);
            return new ScanCode(scanCode);
        }


        #endregion
    }
}