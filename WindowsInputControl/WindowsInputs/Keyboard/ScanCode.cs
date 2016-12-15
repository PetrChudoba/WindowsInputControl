using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInputControl.WindowsInputs.Keyboard
{
    public struct ScanCode
    {
        private const ushort ExtendedFlag = 0xE000;

        public ushort Code;

        public ScanCode(ushort code)
        {
            Code = code;
        }


        public ScanCode(byte code, bool isExtended)
        {
            Code = code;

            if (isExtended)
                Code |= ExtendedFlag;
        }


        public bool IsExtended
        {
            get { return (Code & 0xFF00) == ExtendedFlag; }
        }
    }
}