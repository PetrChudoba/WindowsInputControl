using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsInputControl.Hooks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInputControl.KeyboardLayouts;

namespace WindowsInputControl.Hooks.Tests
{
    [TestClass()]
    public class KeyboardLayoutControlTests
    {
        [TestMethod()]
        public void GetAllInstalledLayoutsTest()
        {
            //arrange
            var inputLanguages = InputLanguage.InstalledInputLanguages;

            WindowsKeyboardLayoutControl klc = new WindowsKeyboardLayoutControl();



            //act
            var layout = klc.GetAllInstalledLayouts();

            
            //assert
            Assert.AreEqual(inputLanguages.Count, layout.Count());
        }


    }
}