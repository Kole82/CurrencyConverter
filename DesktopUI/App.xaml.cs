using Core.AppData;
using Core.Data;
using Core.Data.Models;
using Core.Services;
using Core.ViewModels;
using System.Windows;

namespace DesktopUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            await IoC.Resolve<IRepository<Currency>>().LoadDataAsync();
            IoC.Resolve<ApplicationViewModel>().GoToPage(Screen.Converter);
        }
    }
}
