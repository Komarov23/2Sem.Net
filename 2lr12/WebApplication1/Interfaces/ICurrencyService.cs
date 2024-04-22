namespace WebApplication1.Interfaces
{
    public interface ICurrencyService
    {
        Task<string> GetCurrencyRates();
    }
}
