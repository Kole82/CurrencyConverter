using Core.AppData;
using DesktopUI.Views;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace DesktopUI.Converters
{
    public class EnumToScreenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Screen)value)
            {
                case Screen.Loading:
                    return new LoadScreen();

                case Screen.Converter:
                    return new ConverterScreen();

                case Screen.Currency:
                    return new CurrencyScreen();

                default:
                    Debugger.Break();
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
