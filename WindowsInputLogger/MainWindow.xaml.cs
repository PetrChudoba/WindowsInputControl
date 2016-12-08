using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsInputControl;

namespace WindowsInputLogger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowsVm vm = new MainWindowsVm();

            this.DataContext = vm;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            IInputSimulator sim = new InputSimulator();
          //  sim.Keyboard.TextEntry("Ahoj");

        //    sim.Keyboard.SendScanCode(21);
            sim.Keyboard.SendScanVirtualCode(21, 55);


        }
    }
}
