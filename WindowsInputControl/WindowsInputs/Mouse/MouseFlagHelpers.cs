using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInputControl.WindowsInputs.Mouse
{
    internal static class MouseFlagHelpers
    {
        /// <summary>
        /// To the mouse button down flag.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>MouseFlag.</returns>
        public static MouseFlag ToMouseButtonDownFlag(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.LeftButton:
                    return MouseFlag.LeftDown;

                case MouseButton.MiddleButton:
                    return MouseFlag.MiddleDown;

                case MouseButton.RightButton:
                    return MouseFlag.RightDown;

                default:
                    Contract.Assert(false);
                    return MouseFlag.LeftDown;
            }
        }

        /// <summary>
        /// To the mouse button up flag.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>MouseFlag.</returns>
        public static MouseFlag ToMouseButtonUpFlag(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.LeftButton:
                    return MouseFlag.LeftUp;

                case MouseButton.MiddleButton:
                    return MouseFlag.MiddleUp;

                case MouseButton.RightButton:
                    return MouseFlag.RightUp;

                default:
                    Contract.Assert(false);
                    return MouseFlag.LeftUp;
            }
        }
    }
}
