using Core.Services;
using Core.ViewModels.Screens;
using System.Windows;
using System.Windows.Controls;

namespace DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for CurrencyScreen.xaml
    /// </summary>
    public partial class CurrencyScreen : UserControl
    {
        public CurrencyScreen()
        {
            InitializeComponent();

            DataContext = IoC.Resolve<CurrencyConverterViewModel>();
        }

        private void CurrencyScreen_Loaded(object sender, RoutedEventArgs e)
        {
            double itemHeight = (itemsControl.ItemContainerGenerator.ContainerFromIndex(0) as FrameworkElement).ActualHeight;
            int index = 0;

            if (IoC.Resolve<CurrencyConverterViewModel>().IsFirstCurrency)
            {
                index = IoC.Resolve<CurrencyConverterViewModel>().Currencies.IndexOf(IoC.Resolve<CurrencyConverterViewModel>().FirstCurrency);
            }
            else
            {
                index = IoC.Resolve<CurrencyConverterViewModel>().Currencies.IndexOf(IoC.Resolve<CurrencyConverterViewModel>().SecondCurrency);
            }

            scrollViewer.ScrollToVerticalOffset(index * itemHeight);
        }
    }
}
