﻿using System;
using System.Threading;
using WindowsInputControl.WindowsInputs;

namespace WindowsInputControl
{
    /// <summary>
    ///     Implements the <see cref="IMouseInputControl" /> interface by calling the an <see cref="IInputMessageDispatcher" /> to
    ///     simulate Mouse gestures.
    /// </summary>
    public class MouseInputControl : IMouseInputControl
    {
        private const int MouseWheelClickSize = 120;


        /// <summary>
        ///     The instance of the <see cref="IInputMessageDispatcher" /> to use for dispatching <see cref="Input" /> messages.
        /// </summary>
        private readonly IInputMessageDispatcher _messageDispatcher;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MouseInputControl" /> class using an instance of a
        ///     <see cref="WindowsInputMessageDispatcher" /> for dispatching <see cref="Input" /> messages.
        /// </summary>
        public MouseInputControl()
        {
            
            _messageDispatcher = new WindowsInputMessageDispatcher();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MouseInputControl" /> class using the specified
        ///     <see cref="IInputMessageDispatcher" /> for dispatching <see cref="Input" /> messages.
        /// </summary>
        /// <param name="messageDispatcher">
        ///     The <see cref="IInputMessageDispatcher" /> to use for dispatching <see cref="Input" />
        ///     messages.
        /// </param>
        /// <exception cref="InvalidOperationException">If null is passed as the <paramref name="messageDispatcher" />.</exception>
        internal MouseInputControl(IInputMessageDispatcher messageDispatcher)
        {


            if (messageDispatcher == null)
                throw new InvalidOperationException(
                    string.Format(
                        "The {0} cannot operate with a null {1}. Please provide a valid {1} instance to use for dispatching {2} messages.",
                        typeof(MouseInputControl).Name, typeof(IInputMessageDispatcher).Name, typeof(Input).Name));

            _messageDispatcher = messageDispatcher;
        }



        /// <summary>
        ///     Simulates mouse movement by the specified distance measured as a delta from the current mouse location in pixels.
        /// </summary>
        /// <param name="pixelDeltaX">The distance in pixels to move the mouse horizontally.</param>
        /// <param name="pixelDeltaY">The distance in pixels to move the mouse vertically.</param>
        public IMouseInputControl MoveMouseBy(int pixelDeltaX, int pixelDeltaY)
        {
            var inputList = new InputBuilder().AddRelativeMouseMovement(pixelDeltaX, pixelDeltaY).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates mouse movement to the specified location on the primary display device.
        /// </summary>
        /// <param name="absoluteX">
        ///     The destination's absolute X-coordinate on the primary display device where 0 is the extreme
        ///     left hand side of the display device and 65535 is the extreme right hand side of the display device.
        /// </param>
        /// <param name="absoluteY">
        ///     The destination's absolute Y-coordinate on the primary display device where 0 is the top of the
        ///     display device and 65535 is the bottom of the display device.
        /// </param>
        public IMouseInputControl MoveMouseTo(double absoluteX, double absoluteY)
        {
            var inputList =
                new InputBuilder().AddAbsoluteMouseMovement((int) Math.Truncate(absoluteX),
                    (int) Math.Truncate(absoluteY)).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates mouse movement to the specified location on the Virtual Desktop which includes all active displays.
        /// </summary>
        /// <param name="absoluteX">
        ///     The destination's absolute X-coordinate on the virtual desktop where 0 is the left hand side of
        ///     the virtual desktop and 65535 is the extreme right hand side of the virtual desktop.
        /// </param>
        /// <param name="absoluteY">
        ///     The destination's absolute Y-coordinate on the virtual desktop where 0 is the top of the
        ///     virtual desktop and 65535 is the bottom of the virtual desktop.
        /// </param>
        public IMouseInputControl MoveMouseToPositionOnVirtualDesktop(double absoluteX, double absoluteY)
        {
            var inputList =
                new InputBuilder().AddAbsoluteMouseMovementOnVirtualDesktop((int) Math.Truncate(absoluteX),
                    (int) Math.Truncate(absoluteY)).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates a mouse left button down gesture.
        /// </summary>
        public IMouseInputControl LeftButtonDown()
        {
            var inputList = new InputBuilder().AddMouseButtonDown(MouseButton.LeftButton).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates a mouse left button up gesture.
        /// </summary>
        public IMouseInputControl LeftButtonUp()
        {
            var inputList = new InputBuilder().AddMouseButtonUp(MouseButton.LeftButton).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates a mouse left-click gesture.
        /// </summary>
        public IMouseInputControl LeftButtonClick()
        {
            var inputList = new InputBuilder().AddMouseButtonClick(MouseButton.LeftButton).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates a mouse left button double-click gesture.
        /// </summary>
        public IMouseInputControl LeftButtonDoubleClick()
        {
            var inputList = new InputBuilder().AddMouseButtonDoubleClick(MouseButton.LeftButton).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates a mouse right button down gesture.
        /// </summary>
        public IMouseInputControl RightButtonDown()
        {
            var inputList = new InputBuilder().AddMouseButtonDown(MouseButton.RightButton).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates a mouse right button up gesture.
        /// </summary>
        public IMouseInputControl RightButtonUp()
        {
            var inputList = new InputBuilder().AddMouseButtonUp(MouseButton.RightButton).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates a mouse right button click gesture.
        /// </summary>
        public IMouseInputControl RightButtonClick()
        {
            var inputList = new InputBuilder().AddMouseButtonClick(MouseButton.RightButton).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates a mouse right button double-click gesture.
        /// </summary>
        public IMouseInputControl RightButtonDoubleClick()
        {
            var inputList = new InputBuilder().AddMouseButtonDoubleClick(MouseButton.RightButton).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates a mouse X button down gesture.
        /// </summary>
        /// <param name="buttonId">The button id.</param>
        public IMouseInputControl XButtonDown(int buttonId)
        {
            var inputList = new InputBuilder().AddMouseXButtonDown(buttonId).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates a mouse X button up gesture.
        /// </summary>
        /// <param name="buttonId">The button id.</param>
        public IMouseInputControl XButtonUp(int buttonId)
        {
            var inputList = new InputBuilder().AddMouseXButtonUp(buttonId).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates a mouse X button click gesture.
        /// </summary>
        /// <param name="buttonId">The button id.</param>
        public IMouseInputControl XButtonClick(int buttonId)
        {
            var inputList = new InputBuilder().AddMouseXButtonClick(buttonId).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates a mouse X button double-click gesture.
        /// </summary>
        /// <param name="buttonId">The button id.</param>
        public IMouseInputControl XButtonDoubleClick(int buttonId)
        {
            var inputList = new InputBuilder().AddMouseXButtonDoubleClick(buttonId).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates mouse vertical wheel scroll gesture.
        /// </summary>
        /// <param name="scrollAmountInClicks">
        ///     The amount to scroll in clicks. A positive value indicates that the wheel was
        ///     rotated forward, away from the user; a negative value indicates that the wheel was rotated backward, toward the
        ///     user.
        /// </param>
        public IMouseInputControl VerticalScroll(int scrollAmountInClicks)
        {
            var inputList =
                new InputBuilder().AddMouseVerticalWheelScroll(scrollAmountInClicks*MouseWheelClickSize).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Simulates a mouse horizontal wheel scroll gesture. Supported by Windows Vista and later.
        /// </summary>
        /// <param name="scrollAmountInClicks">
        ///     The amount to scroll in clicks. A positive value indicates that the wheel was
        ///     rotated to the right; a negative value indicates that the wheel was rotated to the left.
        /// </param>
        public IMouseInputControl HorizontalScroll(int scrollAmountInClicks)
        {
            var inputList =
                new InputBuilder().AddMouseHorizontalWheelScroll(scrollAmountInClicks*MouseWheelClickSize).ToArray();
            SendSimulatedInput(inputList);
            return this;
        }

        /// <summary>
        ///     Sleeps the executing thread to create a pause between simulated inputs.
        /// </summary>
        /// <param name="millsecondsTimeout">The number of milliseconds to wait.</param>
        public IMouseInputControl Sleep(int millsecondsTimeout)
        {
            Thread.Sleep(millsecondsTimeout);
            return this;
        }

        /// <summary>
        ///     Sleeps the executing thread to create a pause between simulated inputs.
        /// </summary>
        /// <param name="timeout">The time to wait.</param>
        public IMouseInputControl Sleep(TimeSpan timeout)
        {
            Thread.Sleep(timeout);
            return this;
        }

        /// <summary>
        ///     Sends the list of <see cref="Input" /> messages using the <see cref="IInputMessageDispatcher" /> instance.
        /// </summary>
        /// <param name="inputList">The <see cref="System.Array" /> of <see cref="Input" /> messages to send.</param>
        private void SendSimulatedInput(Input[] inputList)
        {
            _messageDispatcher.DispatchInput(inputList);
        }
    }
}