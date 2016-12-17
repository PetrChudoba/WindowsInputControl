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

        private ushort _fullCode;

        public ScanCode(ushort fullCode)
        {
            _fullCode = fullCode;
        }


        public ScanCode(byte code, bool isExtended)
        {
            _fullCode = code;

            if (isExtended)
                _fullCode |= ExtendedFlag;
        }


        public byte Code
        {
            get { return (byte) _fullCode; }
        }

        public ushort FullCode
        {
            get { return _fullCode; }
        }

        public bool IsExtended
        {
            get { return (_fullCode & 0xFF00) == ExtendedFlag; }
        }
    }
}