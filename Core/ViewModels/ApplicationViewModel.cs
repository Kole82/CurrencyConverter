using Core.AppData;
using DevExpress.Mvvm;

namespace Core.ViewModels
{
    public class ApplicationViewModel : BindableBase
    {
        #region Constructors

        public ApplicationViewModel()
        {
            CurrentScreen = Screen.Loading;

            // Subscribe to the Messenger. 
            Messenger.Default.Register<Screen>(this, OnMessage);
        }

        #endregion

        #region Public Properties

        public Screen CurrentScreen
        {
            get => GetValue<Screen>();
            set => SetValue(value);
        }

        #endregion

        #region Private Helpers

        private void OnMessage(Screen screen) => CurrentScreen = screen;

        #endregion
    }
}
