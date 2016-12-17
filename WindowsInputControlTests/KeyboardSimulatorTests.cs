using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsInputControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInputControl.Hooks;
using WindowsInputControl.Keyboards;
using WindowsInputControl.WindowsHooks.Keyboard;
using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl.Tests
{
    [TestClass()]
    public class KeyboardSimulatorTests
    {
        [TestMethod()]
        public void SendScanCodeTest()
        {
            IKeyboardInputControl sim = new  KeyboardInputControl();

            IWindowsKeyboardsControl cont = new WindowsKeyboardsControl();

            IKeyboard layout = cont.GetActiveLayout();

            KeyboardLogger logger = new KeyboardLogger();


            KeyEventArgs firstKey;

            KeyEventArgs secondKey;


            logger.SetHook();



            //

            sim.KeyAction(new ScanCode(45),KeyAction.Down);

            firstKey = logger.LastKeyEvent;


            sim.KeyAction(new ScanCode(45), KeyAction.Down, layout);


            secondKey = logger.LastKeyEvent;



            Assert.AreEqual(firstKey.ScanCode, secondKey.ScanCode);
            Assert.AreEqual(firstKey.VirtualKey, secondKey.VirtualKey);
        }
    }
}