using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsInputControl.Hooks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            KeyboardLayoutControl klc = new KeyboardLayoutControl();



            //act
            var layout = klc.GetAllInstalledLayouts();

            
            //assert
            Assert.AreEqual(inputLanguages.Count, layout.Count());
        }


    }
}