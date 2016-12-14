using WindowsInputControl.Hooks;

namespace WindowsInputControl.Helpers
{
    /// <summary>
    /// Class ServiceLocator.
    /// </summary>
    internal static class ServiceLocator
    {
        private static readonly IKeyboardLayoutNames _defaultLayoutNames = new KeyboardLayoutNames();

        /// <summary>
        /// The keyboard layout names
        /// </summary>
        public static IKeyboardLayoutNames KeyboardLayoutNames
        {
            get
            {
                return _defaultLayoutNames;
            }
        }
    }
}