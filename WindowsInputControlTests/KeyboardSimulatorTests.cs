using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsInputControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInputControl.Hooks;

namespace WindowsInputControl.Tests
{
    [TestClass()]
    public class KeyboardSimulatorTests
    {
        [TestMethod()]
        public void SendScanCodeTest()
        {
            IKeyboardSimulator sim = new KeyboardSimulator(new InputSimulator());

            KeyboardLayoutControl cont = new KeyboardLayoutControl();

            IKeyboardLayout layout = cont.GetActiveLayout();

            KeyboardLogger logger = new KeyboardLogger();


            KeyEventArgs firstKey;

            KeyEventArgs secondKey;


            logger.SetHook();



            //

            sim.SendScanCode(45);

            firstKey = logger.LastKey;


            sim.SendScanCode(45, layout);


            secondKey = logger.LastKey;



            Assert.AreEqual(firstKey.ScanCode, secondKey.ScanCode);
            Assert.AreEqual(firstKey.VirtualKey, secondKey.VirtualKey);
        }
    }
}