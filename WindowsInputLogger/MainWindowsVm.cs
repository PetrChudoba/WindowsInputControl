﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInputControl;
using WindowsInputControl.Hooks;
using WindowsInputControl.Native;

namespace WindowsInputLogger
{
    class MainWindowsVm
    {
        private KeyboardLogger logger;
        private IKeyboardSimulator kbdSimulator;

        private List<string> _events;

        public MainWindowsVm()
        {
                 logger = new KeyboardLogger();
            logger.SetHook();

            kbdSimulator = new KeyboardSimulator(new InputSimulator());
        }

        public ushort ScanCode { get; set;  }

        public ushort VirtualKey { get; set; }

        public byte Flags { get; set; }


        public void Send()
        {
            kbdSimulator.Send(ScanCode, VirtualKey, (KeyboardFlag) Flags);
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

        public void SendScanCode()
        {
            kbdSimulator.SendScanCode(0xE01F);
        }
    }
}
