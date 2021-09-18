using Core.AppData;
using Core.ViewModels.Screens;
using Core.Services;
using DevExpress.Mvvm;

namespace Core.ViewModels.Controls
{
    public class CurrencyListItemViewModel : BindableBase
    {
        #region Constructors

        public CurrencyListItemViewModel()
        {
            ConverterScreenCommand = new DelegateCommand(() =>
            {
                if (IoC.Resolve<CurrencyConverterViewModel>().IsFirstCurrency)
                {
                    IoC.Resolve<CurrencyConverterViewModel>().FirstCurrency.IsSelected = false;
                    IsSelected = true;
                    IoC.Resolve<CurrencyConverterViewModel>().FirstCurrency = this;
                }
                else
                {
                    IoC.Resolve<CurrencyConverterViewModel>().SecondCurrency.IsSelected = false;
                    IsSelected = true;
                    IoC.Resolve<CurrencyConverterViewModel>().SecondCurrency = this;
                }

                IoC.Resolve<ApplicationViewModel>().GoToPage(Screen.Converter);
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
    }
}
