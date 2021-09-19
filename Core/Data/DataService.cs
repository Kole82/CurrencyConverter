using Core.Data.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Data
{
    /// <summary>
    /// A utility class to support Json processing.
    /// </summary>
    public static class DataService
    {
        #region Public Methods

        /// <summary>
        /// Returns a list of <see cref="Currency"/> objects.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<IList<Currency>> GetCurrenciesAsync(string url)
        {
            string output = await DownloadDataAsync(url);

            dynamic array = await Task.Run(() => JsonConvert.DeserializeObject(output));

            return await ParseDynamicObjectAsync(array);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Downloads text information from the web.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task<string> DownloadDataAsync(string url)
        {
            string responseBody = string.Empty;

            using (HttpClient client = new HttpClient())
            {
                responseBody = await client.GetStringAsync(url);
            }

            return responseBody;
        }

        /// <summary>
        /// Parses an object array and instantiates <see cref="Currency"/> values.
        /// </summary>
        /// <param name="array">The array to process.</param>
        /// <returns>A <see cref="Currency"/> collection.</returns>
        private static async Task<IList<Currency>> ParseDynamicObjectAsync(object array)
        {
            if (array == null)
            {
                return null;
            }

            IList<Currency> currencies = new List<Currency>();

            await Task.Run(() =>
            {
                foreach (var element in (array as dynamic).Valute)
                {
                    foreach (var item in element)
                    {
                        currencies.Add(new Currency
                        {
                            CharCode = item.CharCode,
                            Name = item.Name,
                            Value = item.Value
                        });
                    }
                }
            });

            return currencies;
        }

        #endregion
    }
}
