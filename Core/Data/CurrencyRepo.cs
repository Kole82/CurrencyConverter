using Core.Data.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Data
{
    /// <summary>
    /// The <see cref="Currency"/> repository class.
    /// </summary>
    public class CurrencyRepo : IRepository<Currency>
    {
        #region Private Fields

        private IList<Currency> _currencies;

        #endregion

        #region IRepository<T> Members

        /// <summary>
        /// Returns a shallow-copy of the <see cref="Currency"/> private collection.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Currency> GetAll()
        {
            return new List<Currency>(_currencies);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets up the private <see cref="Currency"/> collection.
        /// </summary>
        /// <returns></returns>
        public async Task LoadDataAsync()
        {
            // Gets the web url from the config file. 
            string url = ConfigurationManager.AppSettings["currencyUrl"].ToString();

            // Initializes the collection with items from the data source.
            _currencies = await DataService.GetCurrenciesAsync(url);

            await Task.Run(() =>
            {
                // Sorts the collection by the currency name.
                _currencies = _currencies.OrderBy(c => c.Name).ToList();

                // Puts the ruble data at the beginning of the sorted collection.
                _currencies.Insert(0, new Currency
                {
                    CharCode = "RUB",
                    Name = "Российский рубль",
                    Value = 1m
                });
            });
        }

        #endregion
    }
}
