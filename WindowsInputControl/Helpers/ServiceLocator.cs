using WindowsInputControl.Hooks;

namespace WindowsInputControl.Helpers
{
    internal static class ServiceLocator
    {
        public static IKeyboardLayoutNames KeyboardLayoutNames = new KeyboardLayoutNames();
    }
}