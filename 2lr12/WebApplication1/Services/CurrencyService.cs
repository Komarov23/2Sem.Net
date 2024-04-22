using WebApplication1.Interfaces;

namespace WebApplication1.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _currencyApi = "https://api.currencyapi.com/v3/latest?apikey=cur_live_fzcR40JKt831kX95l55CTd5lEVVE1SI30OZBILJr";

        public CurrencyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetCurrencyRates()
        {
            var response = await _httpClient.GetAsync(this._currencyApi);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
