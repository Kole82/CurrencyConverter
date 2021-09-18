using Core.Services;
using Core.ViewModels.Screens;
using System.Windows.Controls;

namespace DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for ConverterScreen.xaml
    /// </summary>
    public partial class ConverterScreen : UserControl
    {
        public ConverterScreen()
        {
            InitializeComponent();

            DataContext = IoC.Resolve<CurrencyConverterViewModel>();
        }
    }
}
