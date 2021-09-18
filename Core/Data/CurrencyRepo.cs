using Core.Data.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Data
{
    public class CurrencyRepo : IRepository<Currency>
    {
        #region Private Fields

        private IList<Currency> _currencies;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CurrencyRepo()
        {
        }

        #endregion

        #region IRepository<T> Members

        public IEnumerable<Currency> GetAll()
        {
            return new List<Currency>(_currencies);
        }

        #endregion

        #region Private Helpers

        //internal
        //public static async Task<IRepository<Currency>> Create()
        //{
        //    var instance = new CurrencyRepo();

        //    string url = ConfigurationManager.AppSettings["currencyUrl"].ToString();

        //    instance._currencies = await WebApi.GetCurrencies(url);

        //    await Task.Run(() =>
        //    {
        //        instance._currencies.OrderBy(c => c.Name);

        //        instance._currencies.Insert(0, new Currency
        //        {
        //            CharCode = "RUB",
        //            Name = "Российский рубль",
        //            Value = 1m
        //        });
        //    });

        //    return instance;
        //}

        public async Task LoadDataAsync()
        {
            string url = ConfigurationManager.AppSettings["currencyUrl"].ToString();

            _currencies = await DataService.GetCurrenciesAsync(url);

            await Task.Run(() =>
            {
                _currencies = _currencies.OrderBy(c => c.Name).ToList();

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
