using System.Collections.ObjectModel;
using System.Windows.Input;
using WindowsInputControl.Hooks;
using KeyEventArgs = WindowsInputControl.WindowsHooks.Keyboard.KeyEventArgs;

namespace WindowsInputLogger
{
    class LogerVm : ICollectionAddListener<KeyEventArgs>
    {
        private KeyboardLogger _kbdLogger = new KeyboardLogger();
        private readonly ICommand _startRecording;
        private readonly ICommand _stopRecording;
        private readonly ObservableCollection<KeyEventArgs> _events;

        public LogerVm()
        {
            _startRecording = new RelayCommand(startRecording);
            _stopRecording = new RelayCommand(stopRecording);
            _events = new ObservableCollection<KeyEventArgs>();

            _kbdLogger.KeyEventsNotifier.RegisterAdd(this);




        }

        private void KbdLoggerOnChanged(object sender, ChangedEventArgs<WindowsInputControl.WindowsHooks.Keyboard.KeyEventArgs> changedEventArgs)
        {
            _events.Add(changedEventArgs.Item);
        }

        public int Count
        {
            get { return _kbdLogger.KeyEventCount; }
        }

        public ObservableCollection<KeyEventArgs> Events
        {
            get { return _events; }
        }

        public ICommand StartRecording
        {
            get { return _startRecording; }
        }

        public ICommand StopRecording
        {
            get { return _stopRecording; }
        }


        private void startRecording(object obj)
        {
            _kbdLogger.SetHook();
        }

        private void stopRecording(object obj)
        {
            _kbdLogger.RemoveHook();
        }

        public void OnAdded(KeyEventArgs type)
        {
            _events.Add(type);
        }
    }
}