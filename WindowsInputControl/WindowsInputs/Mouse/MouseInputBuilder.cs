using System;
using System.Collections;
using System.Collections.Generic;
using WindowsInputControl.WindowsInputs;
using WindowsInputControl.WindowsInputs.Keyboard;
using WindowsInputControl.WindowsInputs.Mouse;

namespace WindowsInputControl
{





    /// <summary>
    /// Class MouseInputBuilder.
    /// </summary>
    internal class MouseInputBuilder 
    {

        /// <summary>
        /// Adds the relative mouse movement.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>Input.</returns>
        public Input AddRelativeMouseMovement(int x, int y)
        {
            var movement = new Input {Type = (uint) InputType.Mouse};
            movement.Data.Mouse.Flags = (uint) MouseFlag.Move;
            movement.Data.Mouse.X = x;
            movement.Data.Mouse.Y = y;

            

            return movement;
        }

        /// <summary>
        /// Adds the absolute mouse movement.
        /// </summary>
        /// <param name="absoluteX">The absolute x.</param>
        /// <param name="absoluteY">The absolute y.</param>
        /// <returns>Input.</returns>
        public Input AddAbsoluteMouseMovement(int absoluteX, int absoluteY)
        {
            var movement = new Input {Type = (uint) InputType.Mouse};
            movement.Data.Mouse.Flags = (uint) (MouseFlag.Move | MouseFlag.Absolute);
            movement.Data.Mouse.X = absoluteX;
            movement.Data.Mouse.Y = absoluteY;



            return movement;
        }

        /// <summary>
        /// Adds the absolute mouse movement on virtual desktop.
        /// </summary>
        /// <param name="absoluteX">The absolute x.</param>
        /// <param name="absoluteY">The absolute y.</param>
        /// <returns>Input.</returns>
        public Input AddAbsoluteMouseMovementOnVirtualDesktop(int absoluteX, int absoluteY)
        {
            var movement = new Input {Type = (uint) InputType.Mouse};
            movement.Data.Mouse.Flags = (uint) (MouseFlag.Move | MouseFlag.Absolute | MouseFlag.VirtualDesk);
            movement.Data.Mouse.X = absoluteX;
            movement.Data.Mouse.Y = absoluteY;

            

            return movement;
        }

        /// <summary>
        /// Adds the mouse button down.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>Input.</returns>
        public Input AddMouseButtonDown(MouseButton button)
        {
            var buttonDown = new Input {Type = (uint) InputType.Mouse};
            buttonDown.Data.Mouse.Flags = (uint)MouseFlagHelpers.ToMouseButtonDownFlag(button);

             

            return buttonDown;
        }

        /// <summary>
        /// Adds the mouse x button down.
        /// </summary>
        /// <param name="xButtonId">The x button identifier.</param>
        /// <returns>Input.</returns>
        public Input AddMouseXButtonDown(int xButtonId)
        {
            var buttonDown = new Input {Type = (uint) InputType.Mouse};
            buttonDown.Data.Mouse.Flags = (uint) MouseFlag.XDown;
            buttonDown.Data.Mouse.MouseData = (uint) xButtonId;
             

            return buttonDown;
        }

        /// <summary>
        /// Adds the mouse button up.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>Input.</returns>
        public Input AddMouseButtonUp(MouseButton button)
        {
            var buttonUp = new Input {Type = (uint) InputType.Mouse};
            buttonUp.Data.Mouse.Flags = (uint) MouseFlagHelpers.ToMouseButtonUpFlag(button);
             

            return buttonUp;
        }

        /// <summary>
        /// Adds the mouse x button up.
        /// </summary>
        /// <param name="xButtonId">The x button identifier.</param>
        /// <returns>Input.</returns>
        public Input AddMouseXButtonUp(int xButtonId)
        {
            var buttonUp = new Input {Type = (uint) InputType.Mouse};
            buttonUp.Data.Mouse.Flags = (uint) MouseFlag.XUp;
            buttonUp.Data.Mouse.MouseData = (uint) xButtonId;
             

            return buttonUp;
        }


        /// <summary>
        /// Adds the mouse vertical wheel scroll.
        /// </summary>
        /// <param name="scrollAmount">The scroll amount.</param>
        /// <returns>Input.</returns>
        public Input AddMouseVerticalWheelScroll(int scrollAmount)
        {
            var scroll = new Input {Type = (uint) InputType.Mouse};
            scroll.Data.Mouse.Flags = (uint) MouseFlag.VerticalWheel;
            scroll.Data.Mouse.MouseData = (uint) scrollAmount;

             

            return scroll;
        }

        /// <summary>
        /// Adds the mouse horizontal wheel scroll.
        /// </summary>
        /// <param name="scrollAmount">The scroll amount.</param>
        /// <returns>Input.</returns>
        public Input AddMouseHorizontalWheelScroll(int scrollAmount)
        {
            var scroll = new Input {Type = (uint) InputType.Mouse};
            scroll.Data.Mouse.Flags = (uint) MouseFlag.HorizontalWheel;
            scroll.Data.Mouse.MouseData = (uint) scrollAmount;

             

            return scroll;
        }

        
    }
}