using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsInputControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputControl.Tests
{
    [TestClass()]
    public class KeyboardInputControlTests
    {
        [TestMethod()]
        public void KeyPressTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void KeyPressTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void KeyPressTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void KeyPressTest3()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void KeyPressTest4()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void KeyActionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void KeyActionTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void KeyActionTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void KeyActionTest3()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void KeyActionTest4()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SendTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SendTest1()
        {
            KeyboardInputControl kbd = new KeyboardInputControl();

            kbd.Send(VirtualKey.LWIN);

        }


        [TestMethod()]
        public void SendTest2()
        {
            KeyboardInputControl kbd = new KeyboardInputControl();

            kbd.Send(VirtualKey.MEDIA_PLAY_PAUSE);
        }

        [TestMethod()]
        public void SendTest3()
        {
            KeyboardInputControl kbd = new KeyboardInputControl();
            
            kbd.Send(new ScanCode(34,true));
        }

        [TestMethod()]
        public void SendTest4()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SendTest5()
        {
            Assert.Fail();
        }
    }
}