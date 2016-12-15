namespace WindowsInputControl.Keyboards.Names
{

    /// <summary>
    /// Interface IWindowsKeyboardNames is service for translating keyboard identifiers to  names.
    /// </summary>
    internal interface IWindowsKeyboardNames
    {

        /// <summary>
        /// Gets the name of the layout.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns>System.String.</returns>
        string GetLayoutName(string identifier);
    }
}