using Core.Data;
using Core.Data.Models;
using Core.Services;
using System.Windows.Controls;

namespace DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for LoadScreen.xaml
    /// </summary>
    public partial class LoadScreen : UserControl
    {
        public LoadScreen()
        {
            InitializeComponent();

            DataContext = IoC.Resolve<IRepository<Currency>>();
        }
    }
}
