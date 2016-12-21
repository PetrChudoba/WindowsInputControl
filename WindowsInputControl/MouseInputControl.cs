using System;
using System.Threading;
using WindowsInputControl.WindowsInputs;

namespace WindowsInputControl
{

    /// <summary>
    /// Class MouseInputControl.
    /// </summary>
    public class MouseInputControl : IMouseInputControl
    {
        /// <summary>
        /// The mouse wheel click size
        /// </summary>
        private const int MouseWheelClickSize = 120;

        #region Fields

        /// <summary>
        /// The instance of the <see cref="IInputMessageDispatcher" /> to use for dispatching <see cref="Input" /> messages.
        /// </summary>
        private readonly IInputMessageDispatcher _messageDispatcher;


        /// <summary>
        /// The builder
        /// </summary>
        private readonly MouseInputBuilder _builder;


        #endregion


        #region Ctor


        /// <summary>
        /// Initializes a new instance of the <see cref="MouseInputControl" /> class using an instance of a
        /// <see cref="WindowsInputMessageDispatcher" /> for dispatching <see cref="Input" /> messages.
        /// </summary>
        public MouseInputControl()
        {
            
            _messageDispatcher = new WindowsInputMessageDispatcher();
            _builder = new MouseInputBuilder();
        }


        #endregion


        #region Move Methods

        /// <summary>
        /// Simulates mouse movement by the specified distance measured as a delta from the current mouse location in pixels.
        /// </summary>
        /// <param name="pixelDeltaX">The distance in pixels to move the mouse horizontally.</param>
        /// <param name="pixelDeltaY">The distance in pixels to move the mouse vertically.</param>
        public void MoveMouseBy(int pixelDeltaX, int pixelDeltaY)
        {
             var input =  _builder.AddRelativeMouseMovement(pixelDeltaX, pixelDeltaY);
            _messageDispatcher.DispatchInput(new[] {input});
            
        }

        /// <summary>
        /// Simulates mouse movement to the specified location on the primary display device.
        /// </summary>
        /// <param name="absoluteX">The destination's absolute X-coordinate on the primary display device where 0 is the extreme
        /// left hand side of the display device and 65535 is the extreme right hand side of the display device.</param>
        /// <param name="absoluteY">The destination's absolute Y-coordinate on the primary display device where 0 is the top of the
        /// display device and 65535 is the bottom of the display device.</param>
        public void MoveMouseTo(double absoluteX, double absoluteY)
        {
            var input = _builder.AddAbsoluteMouseMovement((int) Math.Truncate(absoluteX),(int) Math.Truncate(absoluteY));
            _messageDispatcher.DispatchInput(new[] {input});
            
        }

        /// <summary>
        /// Simulates mouse movement to the specified location on the Virtual Desktop which includes all active displays.
        /// </summary>
        /// <param name="absoluteX">The destination's absolute X-coordinate on the virtual desktop where 0 is the left hand side of
        /// the virtual desktop and 65535 is the extreme right hand side of the virtual desktop.</param>
        /// <param name="absoluteY">The destination's absolute Y-coordinate on the virtual desktop where 0 is the top of the
        /// virtual desktop and 65535 is the bottom of the virtual desktop.</param>
        public void MoveMouseToPositionOnVirtualDesktop(double absoluteX, double absoluteY)
        {
            var input = _builder.AddAbsoluteMouseMovementOnVirtualDesktop((int) Math.Truncate(absoluteX),(int) Math.Truncate(absoluteY));
            _messageDispatcher.DispatchInput(new[] {input});
            
        }

        #endregion


        #region Button Methods

        /// <summary>
        /// Buttons down.
        /// </summary>
        /// <param name="button">The button.</param>
        public void ButtonDown(MouseButton button)
        {
            var mouseDown = _builder.AddMouseButtonDown(button);

            _messageDispatcher.DispatchInput(new[] { mouseDown });

            
        }

        /// <summary>
        /// Buttons up.
        /// </summary>
        /// <param name="button">The button.</param>
        public void ButtonUp(MouseButton button)
        {
            var mouseUp = _builder.AddMouseButtonUp(button);

            _messageDispatcher.DispatchInput(new[] { mouseUp });

            

        }


        /// <summary>
        /// Buttons the click.
        /// </summary>
        /// <param name="button">The button.</param>
        public void ButtonClick(MouseButton button)
        {

            var mouseDown = _builder.AddMouseButtonDown(button);
            var mouseUp = _builder.AddMouseButtonUp(button);

            _messageDispatcher.DispatchInput(new[] { mouseDown, mouseUp });

            

        }

        /// <summary>
        /// Buttons the double click.
        /// </summary>
        /// <param name="button">The button.</param>
        public void ButtonDoubleClick(MouseButton button)
        {
            var mouseDown = _builder.AddMouseButtonDown(button);
            var mouseUp = _builder.AddMouseButtonUp(button);



            _messageDispatcher.DispatchInput(new[] { mouseDown, mouseUp, mouseDown, mouseUp });

            
        }

        #endregion


        #region LeftButton Methods


        /// <summary>
        /// Simulates a mouse left button down gesture.
        /// </summary>
        public void LeftButtonDown()
        {

            ButtonDown(MouseButton.LeftButton);

            
        }

        /// <summary>
        /// Simulates a mouse left button up gesture.
        /// </summary>
        public void LeftButtonUp()
        {

            ButtonUp(MouseButton.LeftButton);

            
        }


        /// <summary>
        /// Simulates a mouse left-click gesture.
        /// </summary>
        public void LeftButtonClick()
        {
            ButtonClick(MouseButton.LeftButton);

            
        }

        /// <summary>
        /// Simulates a mouse left-click gesture.
        /// </summary>
        public void LeftButtonDoubleClick()
        {
            ButtonDoubleClick(MouseButton.LeftButton);

            
        }

        #endregion


        #region RightButton Methods

        /// <summary>
        /// Simulates a mouse right button down gesture.
        /// </summary>
        public void RightButtonDown()
        {

            ButtonDown(MouseButton.RightButton);
             
            
        }

        /// <summary>
        /// Simulates a mouse right button up gesture.
        /// </summary>
        public void RightButtonUp()
        {

            ButtonUp(MouseButton.RightButton);

            
        }

        /// <summary>
        /// Simulates a mouse right button click gesture.
        /// </summary>
        public void RightButtonClick()
        {
            ButtonClick(MouseButton.RightButton);

            
        }

        /// <summary>
        /// Simulates a mouse right button double-click gesture.
        /// </summary>
        public void RightButtonDoubleClick()
        {

            ButtonDoubleClick(MouseButton.RightButton);

            
        }


        #endregion


        #region XButton Methods

        /// <summary>
        /// Simulates a mouse X button down gesture.
        /// </summary>
        /// <param name="buttonId">The button id.</param>
        public void XButtonDown(int buttonId)
        {
             var input =  _builder.AddMouseXButtonDown(buttonId);
            _messageDispatcher.DispatchInput(new[] {input});
            
        }

        /// <summary>
        /// Simulates a mouse X button up gesture.
        /// </summary>
        /// <param name="buttonId">The button id.</param>
        public void XButtonUp(int buttonId)
        {
             var input =  _builder.AddMouseXButtonUp(buttonId);
            _messageDispatcher.DispatchInput(new[] {input});
            
        }

        /// <summary>
        /// xes the button click.
        /// </summary>
        /// <param name="buttonId">The button identifier.</param>
        public void XButtonClick(int buttonId)
        {
            var mouseDown = _builder.AddMouseXButtonDown(buttonId);
            var mouseUp = _builder.AddMouseXButtonUp(buttonId);

            _messageDispatcher.DispatchInput(new[] { mouseDown, mouseUp });
            
        }


        /// <summary>
        /// xes the button double click.
        /// </summary>
        /// <param name="buttonId">The button identifier.</param>
        public void XButtonDoubleClick(int buttonId)
        {
            var mouseDown = _builder.AddMouseXButtonDown(buttonId);
            var mouseUp = _builder.AddMouseXButtonUp(buttonId);

            _messageDispatcher.DispatchInput(new[] { mouseDown, mouseUp, mouseDown, mouseUp });
            

        }

        #endregion


        #region Scroll Methods

        /// <summary>
        /// Simulates mouse vertical wheel scroll gesture.
        /// </summary>
        /// <param name="scrollAmountInClicks">The amount to scroll in clicks. A positive value indicates that the wheel was
        /// rotated forward, away from the user; a negative value indicates that the wheel was rotated backward, toward the
        /// user.</param>
        public void VerticalScroll(int scrollAmountInClicks)
        {
            var input = _builder.AddMouseVerticalWheelScroll(scrollAmountInClicks*MouseWheelClickSize);
            _messageDispatcher.DispatchInput(new[] {input});
            
        }

        /// <summary>
        /// Simulates a mouse horizontal wheel scroll gesture. Supported by Windows Vista and later.
        /// </summary>
        /// <param name="scrollAmountInClicks">The amount to scroll in clicks. A positive value indicates that the wheel was
        /// rotated to the right; a negative value indicates that the wheel was rotated to the left.</param>
        public void HorizontalScroll(int scrollAmountInClicks)
        {
            var input = _builder.AddMouseHorizontalWheelScroll(scrollAmountInClicks*MouseWheelClickSize);
            _messageDispatcher.DispatchInput(new[] {input});
            
        }


        #endregion

    }
}