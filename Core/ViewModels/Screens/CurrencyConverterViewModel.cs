using Core.AppData;
using Core.Data;
using Core.Data.Models;
using Core.Services;
using Core.ViewModels.Controls;
using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core.ViewModels.Screens
{
    /// <summary>
    /// The view model class providing for the ConverterScreen and CurrencyScreen views.
    /// </summary>
    public class CurrencyConverterViewModel : BindableBase
    {
        #region Private Fields

        private readonly IRepository<Currency> _repo;
        private readonly ICurrencyProcess _currencyProcessor;

        // Indicates if a textbox is being edited.
        // Used to prevent simultaneous editing of the textboxes.
        private bool _isEditingText = false;
        private bool _isfirstCurrency;

        #endregion

        #region Constructors

        public CurrencyConverterViewModel(IRepository<Currency> repo, ICurrencyProcess currencyProcessor)
        {
            _repo = repo;
            _currencyProcessor = currencyProcessor;

            Messenger.Default.Register<CurrencyListItemViewModel>(this, OnMessage);

            Currencies = new ObservableCollection<CurrencyListItemViewModel>(
                _repo.GetAll().Select(c => new CurrencyListItemViewModel
                {
                    CharCode = c.CharCode,
                    Name = c.Name,
                    Value = c.Value
                }));

            // Setting initial values for a currency pair.
            FirstCurrency = Currencies.FirstOrDefault(c => c.CharCode == "RUB");
            SecondCurrency = Currencies.FirstOrDefault(c => c.CharCode == "USD");

            // Command initialization.
            CurrencyScreenCommand = new DelegateCommand<bool>((firstCurrency) =>
            {
                FirstCurrency.IsSelected = false;
                SecondCurrency.IsSelected = false;

                if (firstCurrency)
                {
                    _isfirstCurrency = true;
                    SelectedCurrency = FirstCurrency;
                }
                else
                {
                    _isfirstCurrency = false;
                    SelectedCurrency = SecondCurrency;
                }

                SelectedCurrency.IsSelected = true;

                // Sends a message to the CurrencyScreen view model.
                Messenger.Default.Send(SelectedCurrency);
                // Sends a message to the Application view model.
                Messenger.Default.Send(Screen.Currency);
            });
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The left-hand side currency.
        /// </summary>
        public CurrencyListItemViewModel FirstCurrency
        {
            get => GetValue<CurrencyListItemViewModel>();
            set
            {
                SetValue(value);

                // conversion is calucated when text value changes
                // this allows to convert automatically when a currency has been changed
                FirstCurrencyText = FirstCurrencyText;
            }
        }

        /// <summary>
        /// The right-hand side currency.
        /// </summary>
        public CurrencyListItemViewModel SecondCurrency
        {
            get => GetValue<CurrencyListItemViewModel>();
            set
            {
                SetValue(value);

                // conversion is calucated when text value changes
                // this allows to convert automatically when a currency has been changed
                SecondCurrencyText = SecondCurrencyText;
            }
        }

        /// <summary>
        /// Determines which one of the two currencies is presently selected.
        /// </summary>
        public CurrencyListItemViewModel SelectedCurrency { get; private set; }

        /// <summary>
        /// Represents the text form of the left-hand side currency amount.
        /// </summary>
        public string FirstCurrencyText
        {
            get => GetValue<string>();
            set
            {
                // Input validation.
                if (!_currencyProcessor.Validate(ref value))
                {
                    return;
                }

                SetValue(value);

                SetOutput(FirstCurrencyText, text => SecondCurrencyText = text, FirstCurrency.Value, SecondCurrency.Value);
            }
        }

        /// <summary>
        /// Represents the text form of the right-hand side currency amount.
        /// </summary>
        public string SecondCurrencyText
        {
            get => GetValue<string>();
            set
            {
                // Input validation.
                if (!_currencyProcessor.Validate(ref value))
                {
                    return;
                }

                SetValue(value);

                SetOutput(SecondCurrencyText, text => FirstCurrencyText = text, SecondCurrency.Value, FirstCurrency.Value);
            }
        }

        /// <summary>
        /// Indicates if an error is thrown.
        /// </summary>
        public bool IsError
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        public ObservableCollection<CurrencyListItemViewModel> Currencies { get; private set; }

        #endregion

        #region Commands

        /// <summary>
        /// Sets the selected currency and navigates to the CurrencyScreen.
        /// </summary>
        //TODO: make it async?
        public DelegateCommand<bool> CurrencyScreenCommand { get; private set; }

        #endregion

        #region Private Helpers

        /// <summary>
        /// The message handler from the <see cref="CurrencyListItemViewModel"/>.
        /// </summary>
        /// <param name="selectedCurrency">The selected currency.</param>
        private void OnMessage(CurrencyListItemViewModel selectedCurrency)
        {
            if (_isfirstCurrency)
            {
                FirstCurrency = selectedCurrency;
            }
            else
            {
                SecondCurrency = selectedCurrency;
            }
        }

        /// <summary>
        /// Produces and sets the result of the conversion.
        /// </summary>
        /// <param name="input">The text representation of the amount to be converted.</param>
        /// <param name="output">The text property to set the result to.</param>
        /// <param name="targetValue">The currency value to convert to.</param>
        /// <param name="sourceValue">The currency value to convert from.</param>
        private void SetOutput(string input, Action<string> output, decimal targetValue, decimal sourceValue)
        {
            if (!_isEditingText)
            {
                _isEditingText = true;

                // Makes sure both text fields are cleared.
                if (string.IsNullOrEmpty(input))
                {
                    output(string.Empty);
                    IsError = false;
                }
                else
                {
                    try
                    {
                        decimal result = _currencyProcessor.Exchange(targetValue, sourceValue, decimal.Parse(input));
                        result = Math.Round(result, 2);

                        output(string.Format("{0:0.##}", result));

                        IsError = false;
                    }
                    catch (OverflowException e)
                    {
                        // Catches the overflow exception when too large a value is entered.
                        IsError = true;
                    }
                }

                _isEditingText = false;
            }
        }

        #endregion
    }
}
