using System.Collections.Generic;

namespace WindowsInputControl.Keyboards
{
    public interface IWindowsKeyboardsControl
    {
        IEnumerable<IKeyboard> GetAllInstalledLayouts();
        IKeyboard GetActiveLayout();

        /// <summary>
        /// </summary>
        /// <returns></returns>
        string GetActiveLayoutIdentifier();
    }
}