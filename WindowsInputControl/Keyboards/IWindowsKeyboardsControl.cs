using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace WindowsInputControl.Keyboards
{

    [ContractClass(typeof(WindowsKeyboardscontrolContract))]
    public interface IWindowsKeyboardsControl
    {

        IKeyboard GetActiveLayout();


        IEnumerable<IKeyboard> GetAllInstalledLayouts();


        void ActivateLayout(string identifier);

        
    }


    [ContractClassFor(typeof(IWindowsKeyboardsControl))]
    internal abstract class WindowsKeyboardscontrolContract : IWindowsKeyboardsControl
    {
        

        public IKeyboard GetActiveLayout()
        {
            throw new System.NotImplementedException();
        }


        public IEnumerable<IKeyboard> GetAllInstalledLayouts()
        {
            throw new System.NotImplementedException();
        }


        public void ActivateLayout(string identifier)
        {
            throw new System.NotImplementedException();
        }
    }
}