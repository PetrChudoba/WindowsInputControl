﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using WindowsInputControl.WindowsInputs;

namespace WindowsInputControl
{
    /// <summary>
    ///     The contract for a service that dispatches <see cref="Input" /> messages to the appropriate destination.
    /// </summary>
    [ContractClass(typeof(InputMessageDispatcherContract))]
    internal interface IInputMessageDispatcher
    {
        /// <summary>
        ///     Dispatches the specified list of <see cref="Input" /> messages in their specified order.
        /// </summary>
        /// <param name="inputs">The list of <see cref="Input" /> messages to be dispatched.</param>
        /// <exception cref="ArgumentException">If the <paramref name="inputs" /> array is empty.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="inputs" /> array is null.</exception>
        /// <exception cref="Exception">
        ///     If the any of the commands in the <paramref name="inputs" /> array could not be sent
        ///     successfully.
        /// </exception>
        void DispatchInput(Input[] inputs);
    }


    [ContractClassFor(typeof(IInputMessageDispatcher))]
    internal abstract class InputMessageDispatcherContract : IInputMessageDispatcher
    {
        public void DispatchInput(Input[] inputs)
        {
            Contract.Requires(inputs != null);
            Contract.Requires(inputs.Length != 0);
        }
    }
}