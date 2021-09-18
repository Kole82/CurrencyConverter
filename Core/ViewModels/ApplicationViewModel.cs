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
        }

        #endregion

        #region Public Properties

        public Screen CurrentScreen
        {
            get => GetValue<Screen>();
            set => SetValue(value);
        }

        #endregion

        #region Public Methods

        public void GoToPage(Screen screen) => CurrentScreen = screen;

        #endregion
    }
}
