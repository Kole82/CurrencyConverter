namespace Core.Data.Models
{
    /// <summary>
    /// The model class to represent a currency as a domain object.
    /// </summary>
    public class Currency
    {
        #region Public Properties

        /// <summary>
        /// The currency code.
        /// </summary>
        public string CharCode { get; set; }

        /// <summary>
        /// The currency title.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The currency monetary value.
        /// </summary>
        public decimal Value { get; set; }

        #endregion
    }
}
