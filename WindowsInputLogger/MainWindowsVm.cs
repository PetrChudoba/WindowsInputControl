using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInputControl;
using WindowsInputControl.Hooks;
using WindowsInputControl.WindowsInputs.Keyboard;

namespace WindowsInputLogger
{
    class MainWindowsVm
    {
        private KeyboardLogger logger;
        private KeyboardInputControl _kbdInputControl;

        private List<string> _events;

        public MainWindowsVm()
        {
                 logger = new KeyboardLogger();
            logger.SetHook();

            _kbdInputControl = new KeyboardInputControl();
        }

        public ushort ScanCode { get; set;  }

        public ushort VirtualKey { get; set; }

        public byte Flags { get; set; }


        public void Send()
        {

            _kbdInputControl.KeyPress((VirtualKey) VirtualKey, new ScanCode(ScanCode));
        }


        public void Recreate()
        {
            _events = new List<string>();


            foreach (var evnt in logger.KeyEvents)
            {
                _events.Add(evnt.ToString());
            }
        }


        public IEnumerable<string> Collections
        {
            get { return _events; }
        }

        public void Replay()
        {
            logger.Replay();
        }
    }
}
