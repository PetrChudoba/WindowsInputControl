using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsInputControl.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInputControl.Helpers.Tests
{
    [TestClass()]
    public class UintBitOperationsTests
    {
        [TestMethod()]
        public void IsBitSetTest()
        {
            //arrange
            uint nine = 9;


            //act
            bool b0 = nine.IsBitSet(0);
            bool b1 = nine.IsBitSet(1);
            bool b2 = nine.IsBitSet(2);
            bool b3 = nine.IsBitSet(3);

            bool others = false;

            for (int i = 5; i < 32; i++)
            {
                if (nine.IsBitSet(i))
                {
                    others = true;
                    break;
                }
            }


            //assert
            Assert.AreEqual(b0,true);
            Assert.AreEqual(b1, false);
            Assert.AreEqual(b2, false);
            Assert.AreEqual(b3, true);
            Assert.AreEqual(others, false);
        }

        [TestMethod()]
        public void GetBitsTest()
        {
            //arrange 

            // 9 = 1001b
            uint nine = 9;
            string nineBinary = "1001";

            //act

            string binary = nine.GetBinaryRepresentation();


            //assert

            Assert.AreEqual(nineBinary,binary);

        }

        [TestMethod()]
        public void GetBitsPadded()
        {
            //arrange 

            // 9 = 1001b
            uint nine = 9;
            string nineBinary = "001001";
            int length = nineBinary.Length;

            //act

            string binary = nine.GetBinaryRepresentation(length);


            //assert

            Assert.AreEqual(nineBinary, binary);
        }

        [TestMethod()]
        public void GetBitsSubstring()
        {
            //arrange 

            // 9 = 1001b
            uint nine = 9;
            string nineBinary = "001";
            int length = nineBinary.Length;

            //act

            string binary = nine.GetBinaryRepresentation(length);


            //assert

            Assert.AreEqual(nineBinary, binary);
        }


        [TestMethod()]
        public void SetAllBitsFromZeroTest()
        {
            //arrange 
            uint number = 0;

            //act

            for (int i = 0; i < 32; i++)
            {
                number = number.SetBit(i);
            }


            //assert
            Assert.AreEqual(uint.MaxValue,number);
        }

        [TestMethod()]
        public void SetAllBitsFromRandTest()
        {
            //arrange 
            Random rand = new Random(4242);
            
            uint number = (uint) rand.Next();

            //act

            for (int i = 0; i < 32; i++)
            {
                number = number.SetBit(i);
            }


            //assert
            Assert.AreEqual(uint.MaxValue, number);
        }

        [TestMethod()]
        public void UnsetAllBitsFromMaxUintTest()
        {
            //arrange 
            uint number = uint.MaxValue;
            uint zero = 0;

            //act

            for (int i = 0; i < 32; i++)
            {
                number = number.UnsetBit(i);
            }


            //
            Assert.AreEqual(zero, number);
        }

        [TestMethod()]
        public void UnsetAllBitsFromRandTest()
        {
            //arrange 
            Random rand = new Random(4242);

            uint number = (uint)rand.Next();

            uint zero = 0;

            //act

            for (int i = 0; i < 32; i++)
            {
                number = number.UnsetBit(i);
            }


            //assert
            Assert.AreEqual(zero, number);
        }

        [TestMethod()]
        public void ChangeBitTest()
        {
            //arrange

            uint nine = 9;

            uint expectedEight = 8;
            uint expectedNine = 9;

            //act 

            uint unsetBit = nine.ChangeBit(false, 0);

            uint setBitBack = unsetBit.ChangeBit(true, 0);

            //assert

            Assert.AreEqual(expectedEight, unsetBit);
            Assert.AreEqual(expectedNine,setBitBack);

        }
    }
}