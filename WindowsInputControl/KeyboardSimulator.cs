using System;
using System.Collections.Generic;
using System.Threading;
using WindowsInputControl.Hooks;
using WindowsInputControl.KeyboardLayouts;
using WindowsInputControl.WindowsInputs;
using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl
{
    /// <summary>
    ///     Implements the <see cref="IKeyboardSimulator" /> interface by calling the an <see cref="IInputMessageDispatcher" />
    ///     to simulate Keyboard gestures.
    /// </summary>
    public class KeyboardSimulator : IKeyboardSimulator
    {
        private readonly IInputSimulator _inputSimulator;

        /// <summary>
        ///     The instance of the <see cref="IInputMessageDispatcher" /> to use for dispatching <see cref="Input" /> messages.
        /// </summary>
        private readonly IInputMessageDispatcher _messageDispatcher;

        /// <summary>
        ///     Initializes a new instance of the <see cref="KeyboardSimulator" /> class using an instance of a
        ///     <see cref="WindowsInputMessageDispatcher" /> for dispatching <see cref="Input" /> messages.
        /// </summary>
        /// <param name="inputSimulator">The <see cref="IInputSimulator" /> that owns this instance.</param>
        public KeyboardSimulator(IInputSimulator inputSimulator)
        {
            if (inputSimulator == null) throw new ArgumentNullException("inputSimulator");

            _inputSimulator = inputSimulator;
            _messageDispatcher = new WindowsInputMessageDispatcher();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="KeyboardSimulator" /> class using the specified
        ///     <see cref="IInputMessageDispatcher" /> for dispatching <see cref="Input" /> messages.
        /// </summary>
        /// <param name="inputSimulator">The <see cref="IInputSimulator" /> that owns this instance.</param>
        /// <param name="messageDispatcher">
        ///     The <see cref="IInputMessageDispatcher" /> to use for dispatching <see cref="Input" />
        ///     messages.
        /// </param>
        /// <exception cref="InvalidOperationException">If null is passed as the <paramref name="messageDispatcher" />.</exception>
        internal KeyboardSimulator(IInputSimulator inputSimulator, IInputMessageDispatcher messageDispatcher)
        {
            if (inputSimulator == null) throw new ArgumentNullException("inputSimulator");

            if (messageDispatcher == null)
                throw new InvalidOperationException(
                    string.Format(
                        "The {0} cannot operate with a null {1}. Please provide a valid {1} instance to use for dispatching {2} messages.",
                        typeof(KeyboardSimulator).Name, typeof(IInputMessageDispatcher).Name, typeof(Input).Name));

            _inputSimulator = inputSimulator;
            _messageDispatcher = messageDispatcher;
        }

        /// <summary>
        ///     Calls the Win32 SendInput method to simulate a KeyDown.
        /// </summary>
        /// <param name="virtualKeyhe 
        /// <see cref="VirtualKey" />
        /// to press
        /// </param>
        public IKeyboardSimulator KeyDown(VirtualKey virtualKey)
        {
            var inputList = new InputBuilder().AddKeyDown(virtualKey).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Calls the Win32 SendInput method to simulate a KeyUp.
        /// </summary>
        /// <param name="virtualKeyhe 
        /// <see cref="VirtualKey" />
        /// to lift up
        /// </param>
        public IKeyboardSimulator KeyUp(VirtualKey virtualKey)
        {
            var inputList = new InputBuilder().AddKeyUp(virtualKey).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Calls the Win32 SendInput method with a KeyDown and KeyUp message in the same input sequence in order to simulate a
        ///     Key PRESS.
        /// </summary>
        /// <param name="virtualKeyhe 
        /// <see cref="VirtualKey" />
        /// to press
        /// </param>
        public IKeyboardSimulator KeyPress(VirtualKey virtualKey)
        {
            var inputList = new InputBuilder().AddKeyPress(virtualKey).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates a key press for each of the specified key codes in the order they are specified.
        /// </summary>
        /// <param name="virtualKey
        /// </param>
        public IKeyboardSimulator KeyPress(params VirtualKey[] virtualKey)
        {
            var builder = new InputBuilder();
            KeysPress(builder, virtualKey);
            SendSimulatedInput(builder.ToArray());
            return this;
        }

        /// <summary>
        ///     Simulates a simple modified keystroke like CTRL-C where CTRL is the modifierKey and C is the key.
        ///     The flow is Modifier KeyDown, Key Press, Modifier KeyUp.
        /// </summary>
        /// <param name="modifierKeyCode">The modifier key</param>
        /// <param name="virtualKeyhe key to simulate
        /// </param>
        public IKeyboardSimulator ModifiedKeyStroke(VirtualKey modifierKey, VirtualKey virtualKey)
        {
            ModifiedKeyStroke(new[] {modifierKey}, new[] {virtualKey});
            return this;
        }

        /// <summary>
        ///     Simulates a modified keystroke where there are multiple modifiers and one key like CTRL-ALT-C where CTRL and ALT
        ///     are the modifierKeys and C is the key.
        ///     The flow is Modifiers KeyDown in order, Key Press, Modifiers KeyUp in reverse order.
        /// </summary>
        /// <param name="modifierKeyCodes">The list of modifier keys</param>
        /// <param name="virtualKeyhe key to simulate
        /// </param>
        public IKeyboardSimulator ModifiedKeyStroke(IEnumerable<VirtualKey> modifierKeyCodes, VirtualKey virtualKey)
        {
            ModifiedKeyStroke(modifierKeyCodes, new[] {virtualKey});
            return this;
        }

        /// <summary>
        ///     Simulates a modified keystroke where there is one modifier and multiple keys like CTRL-K-C where CTRL is the
        ///     modifierKey and K and C are the keys.
        ///     The flow is Modifier KeyDown, Keys Press in order, Modifier KeyUp.
        /// </summary>
        /// <param name="modifierKey">The modifier key</param>
        /// <param name="keyCodes">The list of keys to simulate</param>
        public IKeyboardSimulator ModifiedKeyStroke(VirtualKey modifierKey, IEnumerable<VirtualKey> keyCodes)
        {
            ModifiedKeyStroke(new[] {modifierKey}, keyCodes);
            return this;
        }

        /// <summary>
        ///     Simulates a modified keystroke where there are multiple modifiers and multiple keys like CTRL-ALT-K-C where CTRL
        ///     and ALT are the modifierKeys and K and C are the keys.
        ///     The flow is Modifiers KeyDown in order, Keys Press in order, Modifiers KeyUp in reverse order.
        /// </summary>
        /// <param name="modifierKeyCodes">The list of modifier keys</param>
        /// <param name="keyCodes">The list of keys to simulate</param>
        public IKeyboardSimulator ModifiedKeyStroke(IEnumerable<VirtualKey> modifierKeyCodes,
            IEnumerable<VirtualKey> keyCodes)
        {
            var builder = new InputBuilder();
            ModifiersDown(builder, modifierKeyCodes);
            KeysPress(builder, keyCodes);
            ModifiersUp(builder, modifierKeyCodes);

            SendSimulatedInput(builder.ToArray());
            return this;
        }

        /// <summary>
        ///     Calls the Win32 SendInput method with a stream of KeyDown and KeyUp messages in order to simulate uninterrupted
        ///     text entry via the keyboard.
        /// </summary>
        /// <param name="text">The text to be simulated.</param>
        public IKeyboardSimulator TextEntry(string text)
        {
            if (text.Length > uint.MaxValue/2)
                throw new ArgumentException(
                    string.Format("The text parameter is too long. It must be less than {0} characters.",
                        uint.MaxValue/2), "text");
            var inputList = new InputBuilder().AddCharacters(text).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates a single character text entry via the keyboard.
        /// </summary>
        /// <param name="character">The unicode character to be simulated.</param>
        public IKeyboardSimulator TextEntry(char character)
        {
            var inputList = new InputBuilder().AddCharacter(character).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Sleeps the executing thread to create a pause between simulated inputs.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait.</param>
        public IKeyboardSimulator Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
            return this;
        }

        /// <summary>
        ///     Sleeps the executing thread to create a pause between simulated inputs.
        /// </summary>
        /// <param name="timeout">The time to wait.</param>
        public IKeyboardSimulator Sleep(TimeSpan timeout)
        {
            Thread.Sleep(timeout);
            return this;
        }


        private void ModifiersDown(InputBuilder builder, IEnumerable<VirtualKey> modifierKeyCodes)
        {
            if (modifierKeyCodes == null) return;
            foreach (var key in modifierKeyCodes) builder.AddKeyDown(key);
        }

        private void ModifiersUp(InputBuilder builder, IEnumerable<VirtualKey> modifierKeyCodes)
        {
            if (modifierKeyCodes == null) return;

            // Key up in reverse (I miss LINQ)
            var stack = new Stack<VirtualKey>(modifierKeyCodes);
            while (stack.Count > 0) builder.AddKeyUp(stack.Pop());
        }

        private void KeysPress(InputBuilder builder, IEnumerable<VirtualKey> keyCodes)
        {
            if (keyCodes == null) return;
            foreach (var key in keyCodes) builder.AddKeyPress(key);
        }

        /// <summary>
        ///     Sends the list of <see cref="Input" /> messages using the <see cref="IInputMessageDispatcher" /> instance.
        /// </summary>
        /// <param name="inputList">The <see cref="System.Array" /> of <see cref="Input" /> messages to send.</param>
        private void SendSimulatedInput(Input[] inputList)
        {
            _messageDispatcher.DispatchInput(inputList);
        }

        #region Send

        public IKeyboardSimulator Send(KeyAction action, ScanCode scanCode, VirtualKey virtualKey)
        {
            return SendInner(action, scanCode, virtualKey);
        }


        public IKeyboardSimulator Send(ScanCode scanCode, IKeyboardLayout layout)
        {
            VirtualKey virtualKey = layout.GetVirtualKey(scanCode);

            return SendInner(scanCode, virtualKey);
        }


        public IKeyboardSimulator Send(KeyAction action, ScanCode scanCode, IKeyboardLayout layout)
        {
            VirtualKey virtualKey = layout.GetVirtualKey(scanCode);
            return SendInner(action, scanCode, virtualKey);
        }


        public IKeyboardSimulator Send(VirtualKey virtualKey, IKeyboardLayout layout)
        {
            ScanCode scanCode = layout.GetScanCode(virtualKey);

            return SendInner(scanCode, virtualKey);
        }


        public IKeyboardSimulator Send(KeyAction action, VirtualKey virtualKey, IKeyboardLayout layout)
        {
            ScanCode scanCode = layout.GetScanCode(virtualKey);

            return SendInner(action, scanCode, virtualKey);
        }

        #endregion

        #region VirtualKey

        public IKeyboardSimulator SendVirtualKey(VirtualKey virtualKey)
        {
            return SendInner(new ScanCode(), virtualKey);
        }

        public IKeyboardSimulator SendVirtualKey(KeyAction action, VirtualKey virtualKey)
        {
            return SendInner(action, new ScanCode(), virtualKey);
        }

        #endregion

        #region Scancode

        public IKeyboardSimulator SendScanCode(ScanCode scanCode)
        {
            return SendScanCode(KeyAction.Press, scanCode);
        }

        public IKeyboardSimulator SendScanCode(KeyAction keyAction, ScanCode scanCode)
        {
            return SendScanCodeInner(keyAction, scanCode);
        }

        #endregion

        #region Private methods

        private IKeyboardSimulator SendInner(KeyAction action, ScanCode scanCode, VirtualKey virtualKey)
        {
            if (action == KeyAction.Press) return SendInner(scanCode, virtualKey);

            Input inpDown = new Input()
            {
                Type = InputType.Keyboard,
                Data = new MouseKeybdHardwareInput()
                {
                    Keyboard = new KeyboardInput()
                    {
                        ScanCode = scanCode,
                        virtualKey = virtualKey,
                        Flags =
                            (scanCode.IsExtended ? KeyboardFlag.ExtendedKey : 0) |
                            (action == KeyAction.Up ? KeyboardFlag.KeyUp : 0)
                    }
                }
            };

            _messageDispatcher.DispatchInput(new[] {inpDown});

            return this;
        }

        private IKeyboardSimulator SendInner(ScanCode scanCode, VirtualKey virtualKey)
        {
            Input inpDown = new Input()
            {
                Type = InputType.Keyboard,
                Data = new MouseKeybdHardwareInput()
                {
                    Keyboard = new KeyboardInput()
                    {
                        ScanCode = scanCode,
                        virtualKey = virtualKey,
                        Flags = (scanCode.IsExtended ? KeyboardFlag.ExtendedKey : 0)
                    }
                }
            };


            Input inpUp = new Input()
            {
                Type = InputType.Keyboard,
                Data = new MouseKeybdHardwareInput()
                {
                    Keyboard = new KeyboardInput()
                    {
                        ScanCode = scanCode,
                        virtualKey = virtualKey,
                        Flags = KeyboardFlag.KeyUp | (scanCode.IsExtended ? KeyboardFlag.ExtendedKey : 0)
                    }
                }
            };


            _messageDispatcher.DispatchInput(new[] {inpDown, inpUp});

            return this;
        }


        private IKeyboardSimulator SendScanCodeInner(KeyAction action, ScanCode scanCode)
        {
            if (action == KeyAction.Press) return SendScanCodeInner(scanCode);

            Input inpDown = new Input()
            {
                Type = InputType.Keyboard,
                Data = new MouseKeybdHardwareInput()
                {
                    Keyboard = new KeyboardInput()
                    {
                        ScanCode = scanCode,
                        Flags =
                            KeyboardFlag.ScanCode | (scanCode.IsExtended ? KeyboardFlag.ExtendedKey : 0) |
                            (action == KeyAction.Up ? KeyboardFlag.KeyUp : 0)
                    }
                }
            };

            _messageDispatcher.DispatchInput(new[] {inpDown});

            return this;
        }


        private IKeyboardSimulator SendScanCodeInner(ScanCode scanCode)
        {
            Input inpDown = new Input()
            {
                Type = InputType.Keyboard,
                Data = new MouseKeybdHardwareInput()
                {
                    Keyboard = new KeyboardInput()
                    {
                        ScanCode = scanCode,
                        Flags = KeyboardFlag.ScanCode | (scanCode.IsExtended ? KeyboardFlag.ExtendedKey : 0)
                    }
                }
            };


            Input inpUp = new Input()
            {
                Type = InputType.Keyboard,
                Data = new MouseKeybdHardwareInput()
                {
                    Keyboard = new KeyboardInput()
                    {
                        ScanCode = scanCode,
                        Flags =
                            KeyboardFlag.ScanCode | KeyboardFlag.KeyUp |
                            (scanCode.IsExtended ? KeyboardFlag.ExtendedKey : 0)
                    }
                }
            };


            _messageDispatcher.DispatchInput(new[] {inpDown, inpUp});

            return this;
        }

        #endregion
    }


    public enum KeyAction
    {
        Down,
        Up,
        Press
    }
}