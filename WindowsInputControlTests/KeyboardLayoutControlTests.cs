using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsInputControl.Hooks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInputControl.Hooks.Tests
{
    [TestClass()]
    public class KeyboardLayoutControlTests
    {
        [TestMethod()]
        public void GetAllInstalledLayoutsTest()
        {
            KeyboardLayoutControl klc = new KeyboardLayoutControl();

            var layout = klc.GetAllInstalledLayouts();

            foreach (var keyboardLayout in layout)
            {
                Debug.WriteLine(keyboardLayout.Identifier + " " + keyboardLayout.Name);
            }

            Assert.AreEqual(3, layout.Count());
        }

        [TestMethod()]
        public void GetActiveLayoutIdentifierTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetActiveLayoutHandleTest()
        {
            Assert.Fail();
        }
    }
}