using System.Globalization;
using System.Text.RegularExpressions;

namespace Core.Services
{
    /// <summary>
    /// Provides basic infrastructure to work with currencies.
    /// </summary>
    public class CurrencyProcessor : ICurrencyProcess
    {
        #region Constants

        private const string REGEX_VALUE = @"^\d*\.?\d*$";

        #endregion

        #region Private Fields

        private readonly Regex _regex;

        #endregion

        #region Constructors

        public CurrencyProcessor()
        {
            _regex = new Regex(REGEX_VALUE);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs the conversion between the currencies and calculates the resulting amount.
        /// </summary>
        /// <param name="firstValue">The first currency value.</param>
        /// <param name="secondValue">The second currency value.</param>
        /// <param name="amount">The amount to exchange.</param>
        /// <returns></returns>
        public decimal Exchange(decimal firstValue, decimal secondValue, decimal amount)
        {
            return firstValue / secondValue * amount;
        }

        /// <summary>
        /// Evaluates and edits the input string to comply with the regex rules.
        /// </summary>
        /// <param name="value">The string to evaluate.</param>
        /// <returns></returns>
        public bool Validate(ref string value)
        {
            // when first loading, the value will be 'null'
            if (value == null)
            {
                return false;
            }
            else if (value.Length == 1 && value[0] == '.')
            {
                value = value.Insert(0, "0");
            }

            value = value.TrimEnd();

            if (!_regex.IsMatch(value))
            {
                value = value.Remove(value.Length - 1);

                return false;
            }

            return true;
        }

        //TODO: format

        #endregion
    }
}
