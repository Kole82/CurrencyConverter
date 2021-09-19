using Core.AppData;
using DevExpress.Mvvm;

namespace Core.ViewModels
{
    /// <summary>
    /// The main application view model class.
    /// </summary>
    public class ApplicationViewModel : BindableBase
    {
        #region Constructors

        public ApplicationViewModel()
        {
            // Sets the initial screen.
            CurrentScreen = Screen.Loading;

            // Subscribe to the Messenger. 
            Messenger.Default.Register<Screen>(this, OnMessage);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Represents the active screen.
        /// </summary>
        public Screen CurrentScreen
        {
            get => GetValue<Screen>();
            set => SetValue(value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// The handler for received messages to change the active screen.
        /// </summary>
        /// <param name="screen">The screen to display.</param>
        private void OnMessage(Screen screen) => CurrentScreen = screen;

        #endregion
    }
}
