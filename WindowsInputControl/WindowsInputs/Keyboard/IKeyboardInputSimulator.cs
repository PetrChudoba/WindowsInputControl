using System;
using System.Collections.Generic;
using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl
{
    public interface IKeyboardInputSimulator { 
        /// <summary>
        ///     Simulates a modified keystroke where there are multiple modifiers and multiple keys like CTRL-ALT-K-C where CTRL
        ///     and ALT are the modifierKeys and K and C are the keys.
        ///     The flow is Modifiers KeyDown in order, Keys Press in order, Modifiers KeyUp in reverse order.
        /// </summary>
        /// <param name="modifierKeyCodes">The list of <see cref="VirtualKey" />s for the modifier keys.</param>
        /// <param name="keyCodes">The list of <see cref="VirtualKey" />s for the keys to simulate.</param>
        IKeyboardInputControl ModifiedKeyStroke(IEnumerable<VirtualKey> modifierKeyCodes, IEnumerable<VirtualKey> keyCodes);

        /// <summary>
        ///     Simulates a modified keystroke where there are multiple modifiers and one key like CTRL-ALT-C where CTRL and ALT
        ///     are the modifierKeys and C is the key.
        ///     The flow is Modifiers KeyDown in order, Key Press, Modifiers KeyUp in reverse order.
        /// </summary>
        /// <param name="modifierKeyCodes">The list of <see cref="VirtualKey" />s for the modifier keys.</param>
        /// <param name="virtualKey 
        /// <see cref="VirtualKey" />
        /// for the key.
        /// </param>
        IKeyboardInputControl ModifiedKeyStroke(IEnumerable<VirtualKey> modifierKeyCodes, VirtualKey virtualKey);

        /// <summary>
        ///     Simulates a modified keystroke where there is one modifier and multiple keys like CTRL-K-C where CTRL is the
        ///     modifierKey and K and C are the keys.
        ///     The flow is Modifier KeyDown, Keys Press in order, Modifier KeyUp.
        /// </summary>
        /// <param name="modifierKey">The <see cref="VirtualKey" /> for the modifier key.</param>
        /// <param name="keyCodes">The list of <see cref="VirtualKey" />s for the keys to simulate.</param>
        IKeyboardInputControl ModifiedKeyStroke(VirtualKey modifierKey, IEnumerable<VirtualKey> keyCodes);

        /// <summary>
        ///     Simulates a simple modified keystroke like CTRL-C where CTRL is the modifierKey and C is the key.
        ///     The flow is Modifier KeyDown, Key Press, Modifier KeyUp.
        /// </summary>
        /// <param name="modifierKeyCode">The <see cref="VirtualKey" /> for the  modifier key.</param>
        /// <param name="virtualKey 
        /// <see cref="VirtualKey" />
        /// for the key.
        /// </param>
        IKeyboardInputControl ModifiedKeyStroke(VirtualKey modifierKey, VirtualKey virtualKey);




        /// <summary>
        ///     Simulates uninterrupted text entry via the keyboard.
        /// </summary>
        /// <param name="text">The text to be simulated.</param>
        IKeyboardInputControl TextEntry(string text);

        /// <summary>
        ///     Simulates a single character text entry via the keyboard.
        /// </summary>
        /// <param name="character">The unicode character to be simulated.</param>
        IKeyboardInputControl TextEntry(char character);

        /// <summary>
        ///     Sleeps the executing thread to create a pause between simulated inputs.
        /// </summary>
        /// <param name="millsecondsTimeout">The number of milliseconds to wait.</param>
        IKeyboardInputControl Sleep(int millsecondsTimeout);

        /// <summary>
        ///     Sleeps the executing thread to create a pause between simulated inputs.
        /// </summary>
        /// <param name="timeout">The time to wait.</param>
        IKeyboardInputControl Sleep(TimeSpan timeout);
    }
}