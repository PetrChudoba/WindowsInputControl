﻿using System;
using System.Collections.Generic;
using System.Threading;
using WindowsInputControl.Hooks;
using WindowsInputControl.Native;

namespace WindowsInputControl
{
    /// <summary>
    /// Implements the <see cref="IKeyboardSimulator"/> interface by calling the an <see cref="IInputMessageDispatcher"/> to simulate Keyboard gestures.
    /// </summary>
    public class KeyboardSimulator : IKeyboardSimulator
    {
        private readonly IInputSimulator _inputSimulator;

        /// <summary>
        /// The instance of the <see cref="IInputMessageDispatcher"/> to use for dispatching <see cref="Input"/> messages.
        /// </summary>
        private readonly IInputMessageDispatcher _messageDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardSimulator"/> class using an instance of a <see cref="WindowsInputMessageDispatcher"/> for dispatching <see cref="Input"/> messages.
        /// </summary>
        /// <param name="inputSimulator">The <see cref="IInputSimulator"/> that owns this instance.</param>
        public KeyboardSimulator(IInputSimulator inputSimulator)
        {
            if (inputSimulator == null) throw new ArgumentNullException("inputSimulator");

            _inputSimulator = inputSimulator;
            _messageDispatcher = new WindowsInputMessageDispatcher();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardSimulator"/> class using the specified <see cref="IInputMessageDispatcher"/> for dispatching <see cref="Input"/> messages.
        /// </summary>
        /// <param name="inputSimulator">The <see cref="IInputSimulator"/> that owns this instance.</param>
        /// <param name="messageDispatcher">The <see cref="IInputMessageDispatcher"/> to use for dispatching <see cref="Input"/> messages.</param>
        /// <exception cref="InvalidOperationException">If null is passed as the <paramref name="messageDispatcher"/>.</exception>
        internal KeyboardSimulator(IInputSimulator inputSimulator, IInputMessageDispatcher messageDispatcher)
        {
            if (inputSimulator == null) throw new ArgumentNullException("inputSimulator");

            if (messageDispatcher == null)
                throw new InvalidOperationException(
                    string.Format("The {0} cannot operate with a null {1}. Please provide a valid {1} instance to use for dispatching {2} messages.",
                    typeof(KeyboardSimulator).Name, typeof(IInputMessageDispatcher).Name, typeof(Input).Name));

            _inputSimulator = inputSimulator;
            _messageDispatcher = messageDispatcher;
        }
        

        private void ModifiersDown(InputBuilder builder, IEnumerable<VirtualKeyCode> modifierKeyCodes)
        {
            if (modifierKeyCodes == null) return;
            foreach (var key in modifierKeyCodes) builder.AddKeyDown(key);
        }

        private void ModifiersUp(InputBuilder builder, IEnumerable<VirtualKeyCode> modifierKeyCodes)
        {
            if (modifierKeyCodes == null) return;

            // Key up in reverse (I miss LINQ)
            var stack = new Stack<VirtualKeyCode>(modifierKeyCodes);
            while (stack.Count > 0) builder.AddKeyUp(stack.Pop());
        }

        private void KeysPress(InputBuilder builder, IEnumerable<VirtualKeyCode> keyCodes)
        {
            if (keyCodes == null) return;
            foreach (var key in keyCodes) builder.AddKeyPress(key);
        }

        /// <summary>
        /// Sends the list of <see cref="Input"/> messages using the <see cref="IInputMessageDispatcher"/> instance.
        /// </summary>
        /// <param name="inputList">The <see cref="System.Array"/> of <see cref="Input"/> messages to send.</param>
        private void SendSimulatedInput(Input[] inputList)
        {
            _messageDispatcher.DispatchInput(inputList);
        }

        /// <summary>
        /// Calls the Win32 SendInput method to simulate a KeyDown.
        /// </summary>
        /// <param name="keyCode">The <see cref="VirtualKeyCode"/> to press</param>
        public IKeyboardSimulator KeyDown(VirtualKeyCode keyCode)
        {
            var inputList = new InputBuilder().AddKeyDown(keyCode).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        /// Calls the Win32 SendInput method to simulate a KeyUp.
        /// </summary>
        /// <param name="keyCode">The <see cref="VirtualKeyCode"/> to lift up</param>
        public IKeyboardSimulator KeyUp(VirtualKeyCode keyCode)
        {
            var inputList = new InputBuilder().AddKeyUp(keyCode).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        /// Calls the Win32 SendInput method with a KeyDown and KeyUp message in the same input sequence in order to simulate a Key PRESS.
        /// </summary>
        /// <param name="keyCode">The <see cref="VirtualKeyCode"/> to press</param>
        public IKeyboardSimulator KeyPress(VirtualKeyCode keyCode)
        {
            var inputList = new InputBuilder().AddKeyPress(keyCode).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        /// Simulates a key press for each of the specified key codes in the order they are specified.
        /// </summary>
        /// <param name="keyCodes"></param>
        public IKeyboardSimulator KeyPress(params VirtualKeyCode[] keyCodes)
        {
            var builder = new InputBuilder();
            KeysPress(builder, keyCodes);
            SendSimulatedInput(builder.ToArray());
            return this;
        }

        /// <summary>
        /// Simulates a simple modified keystroke like CTRL-C where CTRL is the modifierKey and C is the key.
        /// The flow is Modifier KeyDown, Key Press, Modifier KeyUp.
        /// </summary>
        /// <param name="modifierKeyCode">The modifier key</param>
        /// <param name="keyCode">The key to simulate</param>
        public IKeyboardSimulator ModifiedKeyStroke(VirtualKeyCode modifierKeyCode, VirtualKeyCode keyCode)
        {
            ModifiedKeyStroke(new[] { modifierKeyCode }, new[] { keyCode });
            return this;
        }

        /// <summary>
        /// Simulates a modified keystroke where there are multiple modifiers and one key like CTRL-ALT-C where CTRL and ALT are the modifierKeys and C is the key.
        /// The flow is Modifiers KeyDown in order, Key Press, Modifiers KeyUp in reverse order.
        /// </summary>
        /// <param name="modifierKeyCodes">The list of modifier keys</param>
        /// <param name="keyCode">The key to simulate</param>
        public IKeyboardSimulator ModifiedKeyStroke(IEnumerable<VirtualKeyCode> modifierKeyCodes, VirtualKeyCode keyCode)
        {
            ModifiedKeyStroke(modifierKeyCodes, new[] {keyCode});
            return this;
        }

        /// <summary>
        /// Simulates a modified keystroke where there is one modifier and multiple keys like CTRL-K-C where CTRL is the modifierKey and K and C are the keys.
        /// The flow is Modifier KeyDown, Keys Press in order, Modifier KeyUp.
        /// </summary>
        /// <param name="modifierKey">The modifier key</param>
        /// <param name="keyCodes">The list of keys to simulate</param>
        public IKeyboardSimulator ModifiedKeyStroke(VirtualKeyCode modifierKey, IEnumerable<VirtualKeyCode> keyCodes)
        {
            ModifiedKeyStroke(new [] {modifierKey}, keyCodes);
            return this;
        }

        /// <summary>
        /// Simulates a modified keystroke where there are multiple modifiers and multiple keys like CTRL-ALT-K-C where CTRL and ALT are the modifierKeys and K and C are the keys.
        /// The flow is Modifiers KeyDown in order, Keys Press in order, Modifiers KeyUp in reverse order.
        /// </summary>
        /// <param name="modifierKeyCodes">The list of modifier keys</param>
        /// <param name="keyCodes">The list of keys to simulate</param>
        public IKeyboardSimulator ModifiedKeyStroke(IEnumerable<VirtualKeyCode> modifierKeyCodes, IEnumerable<VirtualKeyCode> keyCodes)
        {
            var builder = new InputBuilder();
            ModifiersDown(builder, modifierKeyCodes);
            KeysPress(builder, keyCodes);
            ModifiersUp(builder, modifierKeyCodes);

            SendSimulatedInput(builder.ToArray());
            return this;
        }

        /// <summary>
        /// Calls the Win32 SendInput method with a stream of KeyDown and KeyUp messages in order to simulate uninterrupted text entry via the keyboard.
        /// </summary>
        /// <param name="text">The text to be simulated.</param>
        public IKeyboardSimulator TextEntry(string text)
        {
            if (text.Length > UInt32.MaxValue / 2) throw new ArgumentException(string.Format("The text parameter is too long. It must be less than {0} characters.", UInt32.MaxValue / 2), "text");
            var inputList = new InputBuilder().AddCharacters(text).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        /// Simulates a single character text entry via the keyboard.
        /// </summary>
        /// <param name="character">The unicode character to be simulated.</param>
        public IKeyboardSimulator TextEntry(char character)
        {
            var inputList = new InputBuilder().AddCharacter(character).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        /// Sleeps the executing thread to create a pause between simulated inputs.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait.</param>
        public IKeyboardSimulator Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
            return this;
        }

        /// <summary>
        /// Sleeps the executing thread to create a pause between simulated inputs.
        /// </summary>
        /// <param name="timeout">The time to wait.</param>
        public IKeyboardSimulator Sleep(TimeSpan timeout)
        {
            Thread.Sleep(timeout);
            return this;
        }


        public IKeyboardSimulator SendScanCode(ushort scanCode, IKeyboardLayout keyboardLayout)
        {

            ushort vk = keyboardLayout.GetVirtualKey(scanCode);

            SendScanVirtualCode(scanCode, vk);


            return this;
        }

        public IKeyboardSimulator SendScanVirtualCode(ushort scanCode, ushort virtualKey)
        {

            Input[] arr = new Input[2];



            //Create Key down
            Input inp = new Input
            {
                Type = InputType.Keyboard,
                Data = new MouseKeybdHardwareInput { Keyboard = new KeyboardInput() }
            };
            inp.Data.Keyboard.KeyCode = (VirtualKeyCode) virtualKey;
            inp.Data.Keyboard.ScanCode = scanCode;

            arr[0] = inp;

            inp.Data.Keyboard.SetKeyUp();

            arr[1] = inp;


            _messageDispatcher.DispatchInput(arr);


            return this;

        }


        public IKeyboardSimulator Send(ushort scanCode, ushort virtualKey, KeyboardFlag flags)
        {

            Input[] arr = new Input[2];

            Input inp = new Input
            {
                Type =  InputType.Keyboard,
                Data = new MouseKeybdHardwareInput {Keyboard = new KeyboardInput()}
            };
            inp.Data.Keyboard.KeyCode = (VirtualKeyCode) virtualKey;
            inp.Data.Keyboard.ScanCode = scanCode;
            inp.Data.Keyboard.Flags =  flags;

            if ((scanCode & 0xFF00) == 0xE000)
            {
                inp.Data.Keyboard.Flags |=  KeyboardFlag.ExtendedKey;
            }

            arr[0] = inp;

            inp.Data.Keyboard.SetKeyUp();

            arr[1] = inp;


            _messageDispatcher.DispatchInput(arr);


            return this;
    }



        #region Send


        public IKeyboardSimulator Send(KeyAction action, ushort scanCode, VirtualKeyCode virtualKey)
        {

            bool isExtend = (scanCode & 0xFF00) == 0xE000;

            return SendInner(action, scanCode, isExtend, virtualKey);
        }



        #endregion






        #region Scancode


        public IKeyboardSimulator SendScanCode(ushort scanCode)
        {
            return SendScanCode(KeyAction.Press, scanCode);
        }

        public IKeyboardSimulator SendScanCode(ushort scanCode, bool isExtended)
        {

            return SendScanCode(KeyAction.Press, scanCode, isExtended);

        }

        public IKeyboardSimulator SendScanCode(KeyAction keyAction, ushort scanCode)
        {
            bool isExtended = (scanCode & 0xFF00) == 0xE000;

            return SendScanCodeInner(keyAction,scanCode, isExtended);
        }


        public IKeyboardSimulator SendScanCode(KeyAction keyAction, ushort scanCode, bool isExtended)
        {
            ushort extendedScancode = scanCode;

            if (isExtended)
            {
                extendedScancode |= 0xE000;
            }

            return SendScanCodeInner(keyAction, extendedScancode, isExtended);
        }


        #endregion


        #region Private methods

        private IKeyboardSimulator SendInner(KeyAction action, ushort scanCode, bool isExtended, VirtualKeyCode virtualKey)
        {
            if (action == KeyAction.Press) return SendScanCodeInner(scanCode, isExtended);

            Input inpDown = new Input()
            {
                Type = InputType.Keyboard,
                Data = new MouseKeybdHardwareInput()
                {
                    Keyboard = new KeyboardInput()
                    {
                        ScanCode = scanCode,
                        KeyCode = virtualKey,
                        Flags =  (isExtended ? KeyboardFlag.ExtendedKey : 0) | (action == KeyAction.Up ? KeyboardFlag.KeyUp : 0)
                    }
                }

            };

            _messageDispatcher.DispatchInput(new[] { inpDown });

            return this;
        }

        private IKeyboardSimulator SendInner( ushort scanCode, bool isExtended, VirtualKeyCode virtualKey)
        {
            Input inpDown = new Input()
            {
                Type = InputType.Keyboard,
                Data = new MouseKeybdHardwareInput()
                {
                    Keyboard = new KeyboardInput()
                    {
                        ScanCode = scanCode,
                        KeyCode = virtualKey,
                        Flags =  (isExtended ? KeyboardFlag.ExtendedKey : 0)
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
                        KeyCode = virtualKey,
                        Flags =  KeyboardFlag.KeyUp | (isExtended ? KeyboardFlag.ExtendedKey : 0)
                    }
                }

            };


            _messageDispatcher.DispatchInput(new[] { inpDown, inpUp });

            return this;
        }



        private IKeyboardSimulator SendScanCodeInner(KeyAction action, ushort scanCode, bool isExtended)
        {
            if (action == KeyAction.Press) return SendScanCodeInner(scanCode, isExtended);

            Input inpDown = new Input()
            {
                Type = InputType.Keyboard,
                Data = new MouseKeybdHardwareInput()
                {
                    Keyboard = new KeyboardInput()
                    {
                        ScanCode = scanCode,
                        Flags = KeyboardFlag.ScanCode | (isExtended ? KeyboardFlag.ExtendedKey : 0) | (action == KeyAction.Up? KeyboardFlag.KeyUp : 0)
                    }
                }

            };

            _messageDispatcher.DispatchInput(new[] { inpDown});

            return this;
        }



        private IKeyboardSimulator SendScanCodeInner(ushort scanCode, bool isExtended)
        {
            Input inpDown = new Input()
            {
                Type = InputType.Keyboard,
                Data = new MouseKeybdHardwareInput()
                {
                    Keyboard = new KeyboardInput()
                    {
                        ScanCode = scanCode,
                        Flags = KeyboardFlag.ScanCode | (isExtended? KeyboardFlag.ExtendedKey : 0)
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
                        Flags = KeyboardFlag.ScanCode | KeyboardFlag.KeyUp | (isExtended ? KeyboardFlag.ExtendedKey : 0)
                    }
                }

            };


           _messageDispatcher.DispatchInput(new []{inpDown, inpUp});

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