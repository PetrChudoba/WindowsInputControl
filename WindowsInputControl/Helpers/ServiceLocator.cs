using WindowsInputControl.Hooks;
using WindowsInputControl.Keyboards.Names;

namespace WindowsInputControl.Helpers
{
    /// <summary>
    ///     Class ServiceLocator.
    /// </summary>
    internal static class ServiceLocator
    {

        private static readonly IWindowsKeyboardNames DefaultKeyboardNames = new WindowsKeyboardNames();

        /// <summary>
        /// The keyboard layout names
        /// </summary>
        /// <value>The windows keyboard layout names.</value>
        public static IWindowsKeyboardNames WindowsKeyboardNames
        {
            get { return DefaultKeyboardNames; }
        }
    }
}