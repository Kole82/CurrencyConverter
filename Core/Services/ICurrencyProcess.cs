namespace Core.Services
{
    public interface ICurrencyProcess
    {
        decimal Exchange(decimal firstValue, decimal secondValue, decimal amount);
        bool Validate(ref string value);
    }
}
