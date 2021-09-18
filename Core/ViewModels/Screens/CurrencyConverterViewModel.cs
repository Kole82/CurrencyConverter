﻿using Core.AppData;
using Core.Data;
using Core.Data.Models;
using Core.Services;
using Core.ViewModels.Controls;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core.ViewModels.Screens
{
    public class CurrencyConverterViewModel : BindableBase
    {
        #region Private Fields

        private readonly IRepository<Currency> _repo;
        private readonly ICurrencyProcess _currencyProcessor;

        // Indicates if a textbox is being edited
        // Used to prevent simultaneous editing of the textboxes
        private bool _isEditingText = false;

        private decimal result;

        #endregion

        #region Constructors

        public CurrencyConverterViewModel(IRepository<Currency> repo, ICurrencyProcess currencyProcessor)
        {
            _repo = repo;
            _currencyProcessor = currencyProcessor;

            Currencies = new ObservableCollection<CurrencyListItemViewModel>(
                _repo.GetAll().Select(c => new CurrencyListItemViewModel
                {
                    CharCode = c.CharCode,
                    Name = c.Name,
                    Value = c.Value
                }));

            FirstCurrency = Currencies.FirstOrDefault(c => c.CharCode == "RUB");
            SecondCurrency = Currencies.FirstOrDefault(c => c.CharCode == "USD");

            CurrencyScreenCommand = new DelegateCommand<bool>((firstCurrency) =>
            {
                if (firstCurrency)
                {
                    IsFirstCurrency = true;
                    FirstCurrency.IsSelected = true;

                    // in case one and the same currency is selected
                    if (!FirstCurrency.Equals(SecondCurrency))
                        SecondCurrency.IsSelected = false;
                }
                else
                {
                    IsFirstCurrency = false;
                    SecondCurrency.IsSelected = true;

                    // in case one and the same currency is selected
                    if (!FirstCurrency.Equals(SecondCurrency))
                        FirstCurrency.IsSelected = false;
                }

                IoC.Resolve<ApplicationViewModel>().GoToPage(Screen.Currency);
            });
        }

        #endregion

        #region Public Properties

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

        public string FirstCurrencyText
        {
            get => GetValue<string>();
            set
            {
                if (!_currencyProcessor.Validate(ref value))
                {
                    return;
                }

                SetValue(value);

                //TODO: refactor into a method
                if (!_isEditingText)
                {
                    _isEditingText = true;

                    if (string.IsNullOrEmpty(FirstCurrencyText))
                    {
                        SecondCurrencyText = string.Empty;
                    }
                    else
                    {
                        //TODO: catch overflow
                        result = _currencyProcessor.Exchange(
                            FirstCurrency.Value, SecondCurrency.Value, decimal.Parse(FirstCurrencyText));

                        SecondCurrencyText = string.Format("{0:0.##}", result);
                    }

                    _isEditingText = false;
                }
            }
        }

        public string SecondCurrencyText
        {
            get => GetValue<string>();
            set
            {
                if (!_currencyProcessor.Validate(ref value))
                {
                    return;
                }

                SetValue(value);

                //TODO: refactor into a method
                if (!_isEditingText)
                {
                    _isEditingText = true;

                    if (string.IsNullOrEmpty(SecondCurrencyText))
                    {
                        FirstCurrencyText = string.Empty;
                    }
                    else
                    {
                        //TODO: catch overflow
                        result = _currencyProcessor.Exchange(
                            SecondCurrency.Value, FirstCurrency.Value, decimal.Parse(SecondCurrencyText));

                        FirstCurrencyText = string.Format("{0:0.##}", result);
                    }

                    _isEditingText = false;
                }
            }
        }

        public ObservableCollection<CurrencyListItemViewModel> Currencies { get; private set; }

        public bool IsFirstCurrency { get; private set; }

        #endregion

        #region Commands

        public DelegateCommand<bool> CurrencyScreenCommand { get; private set; }

        #endregion
    }
}