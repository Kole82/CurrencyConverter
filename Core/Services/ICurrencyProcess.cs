namespace Core.Services
{
    /// <summary>
    /// An interface to support currency processing.
    /// </summary>
    public interface ICurrencyProcess
    {
        decimal Exchange(decimal firstValue, decimal secondValue, decimal amount);
        bool Validate(ref string value);
    }
}
