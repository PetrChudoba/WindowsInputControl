using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInputControl.Hooks;

namespace WindowsInputLogger
{
    class MainWindowsVm
    {
        private KeyboardLogger logger;

        private List<string> _events;


        public MainWindowsVm()
        {
                 logger = new KeyboardLogger();
            logger.SetHook();
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

    }
}
