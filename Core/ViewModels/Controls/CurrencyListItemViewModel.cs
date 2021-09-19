using Core.AppData;
using DevExpress.Mvvm;

namespace Core.ViewModels.Controls
{
    /// <summary>
    /// The view model class for the CurrencyListItem control.
    /// </summary>
    public class CurrencyListItemViewModel : BindableBase
    {
        #region Private Fields

        private CurrencyListItemViewModel _selectedCurrency;

        #endregion

        #region Constructors

        public CurrencyListItemViewModel()
        {
            Messenger.Default.Register<CurrencyListItemViewModel>(this, OnMessage);

            // Command initialization.
            ConverterScreenCommand = new DelegateCommand(() =>
            {
                _selectedCurrency.IsSelected = false;
                IsSelected = true;

                // Sends a message to the ConverterScreen view model.
                Messenger.Default.Send(this);
                // Sends a message to the Application view model.
                Messenger.Default.Send(Screen.Converter);
            });
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The currency code.
        /// </summary>
        public string CharCode
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        /// <summary>
        /// The currency name.
        /// </summary>
        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        /// <summary>
        /// The currency value.
        /// </summary>
        public decimal Value
        {
            get => GetValue<decimal>();
            set => SetValue(value);
        }

        /// <summary>
        /// Indicates if a currency is selected.
        /// Used to make the tick mark visible.
        /// </summary>
        public bool IsSelected
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Sets the selected currency and navigates to the ConverterScreen.
        /// </summary>
        //TODO: make it async?
        public DelegateCommand ConverterScreenCommand { get; private set; }

        #endregion

        #region Private Helpers

        /// <summary>
        /// The message handler from the ConverterScreen.
        /// </summary>
        /// <param name="selectedCurrency">The selected currency.</param>
        private void OnMessage(CurrencyListItemViewModel selectedCurrency) => _selectedCurrency = selectedCurrency;

        #endregion
    }
}
