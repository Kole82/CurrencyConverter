using Core.AppData;
using Core.Data;
using Core.Data.Models;
using Core.Services;
using DevExpress.Mvvm;
using System;
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

            try
            {
                await IoC.Resolve<IRepository<Currency>>().LoadDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred! The process will be terminated. Exception details: {ex}");
                Current.MainWindow.Close();
            }


            Messenger.Default.Send(Screen.Converter);
        }
    }
}
