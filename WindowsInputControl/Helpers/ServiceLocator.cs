using WindowsInputControl.Hooks;
using WindowsInputControl.KeyboardLayouts;

namespace WindowsInputControl.Helpers
{
    /// <summary>
    ///     Class ServiceLocator.
    /// </summary>
    internal static class ServiceLocator
    {

        private static readonly IWindowsKeyboardLayoutNames _defaultLayoutNames = new WindowsKeyboardLayoutNames();

        /// <summary>
        /// The keyboard layout names
        /// </summary>
        /// <value>The windows keyboard layout names.</value>
        public static IWindowsKeyboardLayoutNames WindowsKeyboardLayoutNames
        {
            get { return _defaultLayoutNames; }
        }
    }
}