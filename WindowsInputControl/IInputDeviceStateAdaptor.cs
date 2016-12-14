using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl
{
    /// <summary>
    ///     The contract for a service that interprets the state of input devices.
    /// </summary>
    public interface IInputDeviceStateAdaptor
    {
        /// <summary>
        /// Determines whether [is key down] [the specified virtual key].
        /// </summary>
        /// <param name="virtualKey">The virtual key.</param>
        /// <returns><c>true</c> if [is key down] [the specified virtual key]; otherwise, <c>false</c>.</returns>
        bool IsKeyDown(VirtualKey virtualKey);

        /// <summary>
        ///     Determines whether the specified key is up or down.
        /// </summary>
        /// <param name="virtualKeyhe 
        /// <see cref="VirtualKey" />
        /// for the key.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the key is up; otherwise, <c>false</c>.
        /// </returns>
        bool IsKeyUp(VirtualKey virtualKey);

        /// <summary>
        ///     Determines whether the physical key is up or down at the time the function is called regardless of whether the
        ///     application thread has read the keyboard event from the message pump.
        /// </summary>
        /// <param name="virtualKeyhe 
        /// <see cref="VirtualKey" />
        /// for the key.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the key is down; otherwise, <c>false</c>.
        /// </returns>
        bool IsHardwareKeyDown(VirtualKey virtualKey);

        /// <summary>
        ///     Determines whether the physical key is up or down at the time the function is called regardless of whether the
        ///     application thread has read the keyboard event from the message pump.
        /// </summary>
        /// <param name="virtualKeyhe 
        /// <see cref="VirtualKey" />
        /// for the key.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the key is up; otherwise, <c>false</c>.
        /// </returns>
        bool IsHardwareKeyUp(VirtualKey virtualKey);

        /// <summary>
        ///     Determines whether the toggling key is toggled on (in-effect) or not.
        /// </summary>
        /// <param name="virtualKeyhe 
        /// <see cref="VirtualKey" />
        /// for the key.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the toggling key is toggled on (in-effect); otherwise, <c>false</c>.
        /// </returns>
        bool IsTogglingKeyInEffect(VirtualKey virtualKey);
    }
}