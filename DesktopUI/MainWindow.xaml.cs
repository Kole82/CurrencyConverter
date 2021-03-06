using Core.Services;
using Core.ViewModels;
using System.Windows;

namespace DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = IoC.Resolve<ApplicationViewModel>();
        }
    }
}
