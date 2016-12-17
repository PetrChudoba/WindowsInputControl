using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl.Keyboards
{


    /// <summary>
    /// Interface IKeyboard
    /// </summary>
    public interface IKeyboard
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        string Identifier { get; }


        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }


        /// <summary>
        /// Gets the virtual key for the scan code.
        /// </summary>
        /// <param name="scanCode">The scan code.</param>
        /// <returns>WindowsInputControl.WindowsInputs.Keyboard.VirtualKey.</returns>
        VirtualKey GetVirtualKey(ScanCode scanCode);

        /// <summary>
        /// Gets the scan code for the virtual key.
        /// </summary>
        /// <param name="virtualKey">The virtual key.</param>
        /// <returns>WindowsInputControl.WindowsInputs.Keyboard.ScanCode.</returns>
        ScanCode GetScanCode(VirtualKey virtualKey);
    }
}