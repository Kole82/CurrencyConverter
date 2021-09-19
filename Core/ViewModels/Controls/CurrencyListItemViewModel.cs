using Core.AppData;
using DevExpress.Mvvm;

namespace Core.ViewModels.Controls
{
    public class CurrencyListItemViewModel : BindableBase
    {
        #region Private Fields

        private CurrencyListItemViewModel _selectedCurrency;

        #endregion

        #region Constructors

        public CurrencyListItemViewModel()
        {
            Messenger.Default.Register<CurrencyListItemViewModel>(this, OnMessage);

            ConverterScreenCommand = new DelegateCommand(() =>
            {
                _selectedCurrency.IsSelected = false;
                IsSelected = true;

                Messenger.Default.Send(this);
                Messenger.Default.Send(Screen.Converter);
            });
        }

        #endregion

        #region Public Properties

        public string CharCode
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public decimal Value
        {
            get => GetValue<decimal>();
            set => SetValue(value);
        }

        public bool IsSelected
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        #endregion

        #region Commands

        public DelegateCommand ConverterScreenCommand { get; private set; }

        #endregion

        #region Private Helpers

        private void OnMessage(CurrencyListItemViewModel selectedCurrency) => _selectedCurrency = selectedCurrency;

        #endregion
    }
}
