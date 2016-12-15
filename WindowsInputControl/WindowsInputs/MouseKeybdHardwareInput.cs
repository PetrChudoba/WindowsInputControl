using System.Runtime.InteropServices;
using WindowsInputControl.WindowsInputs.Hardware;
using WindowsInputControl.WindowsInputs.Keyboard;
using WindowsInputControl.WindowsInputs.Mouse;

namespace WindowsInputControl.WindowsInputs
{

    /// <summary>
    ///     The combined/overlayed structure that includes Mouse, Keyboard and Hardware Input message data (see:
    ///     http://msdn.microsoft.com/en-us/library/ms646270(VS.85).aspx)
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct MouseKeybdHardwareInput
    {
        /// <summary>
        ///     The <see cref="WindowsInputs.Mouse.MouseInput" /> definition.
        /// </summary>
        [FieldOffset(0)] public Mouse.MouseInput Mouse;

        /// <summary>
        ///     The <see cref="WindowsInputs.Keyboard.KeyboardInput" /> definition.
        /// </summary>
        [FieldOffset(0)] public Keyboard.KeyboardInput Keyboard;

        /// <summary>
        ///     The <see cref="HardwareInput" /> definition.
        /// </summary>
        [FieldOffset(0)] public HardwareInput Hardware;
    }

}