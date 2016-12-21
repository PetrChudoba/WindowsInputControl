using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace WindowsInputControl.Keyboards
{
    /// <summary>
    /// Interface IWindowsKeyboardsControl
    /// </summary>
    [ContractClass(typeof(WindowsKeyboardscontrolContract))]
    public interface IWindowsKeyboardsControl
    {
        /// <summary>
        /// Gets the active layout.
        /// </summary>
        /// <returns>IKeyboard.</returns>
        IKeyboard GetActiveLayout();

        /// <summary>
        /// Gets all installed layouts.
        /// </summary>
        /// <returns>IEnumerable&lt;IKeyboard&gt;.</returns>
        IEnumerable<IKeyboard> GetAllInstalledLayouts();

        /// <summary>
        /// Activates the layout.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        void ActivateLayout(string identifier);

        
    }


    /// <summary>
    /// Class WindowsKeyboardscontrolContract.
    /// </summary>
    /// <seealso cref="WindowsInputControl.Keyboards.IWindowsKeyboardsControl" />
    [ContractClassFor(typeof(IWindowsKeyboardsControl))]
    internal abstract class WindowsKeyboardscontrolContract : IWindowsKeyboardsControl
    {


        /// <summary>
        /// Gets the active layout.
        /// </summary>
        /// <returns>IKeyboard.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IKeyboard GetActiveLayout()
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Gets all installed layouts.
        /// </summary>
        /// <returns>IEnumerable&lt;IKeyboard&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<IKeyboard> GetAllInstalledLayouts()
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Activates the layout.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void ActivateLayout(string identifier)
        {
            Contract.Requires(!string.IsNullOrEmpty(identifier));
            Contract.Requires(identifier.Length == 8);

            throw new System.NotImplementedException();
        }
    }
}