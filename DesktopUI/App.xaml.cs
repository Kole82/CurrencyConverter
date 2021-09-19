using Core.AppData;
using Core.Data;
using Core.Data.Models;
using Core.Services;
using DevExpress.Mvvm;
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
            Messenger.Default.Send(Screen.Converter);
        }
    }
}
