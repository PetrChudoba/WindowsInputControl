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
        private MainWindowsVm _viewModel;

        public MainWindow()
        {
            InitializeComponent();
             _viewModel = new MainWindowsVm();

            this.DataContext = _viewModel;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            _viewModel.Send();


        }

        private void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
        {
            _viewModel.Replay();
        }
    }
}
