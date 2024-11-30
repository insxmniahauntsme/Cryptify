using CryptifyAPI.Models;

namespace CryptifyAPI.Services;

public interface ICryptocurrencyService
{
	Task<List<Currency>> GetTopTenCurrenciesAsync();
	Task<List<Market>> GetTopMarketsAsync(string currencyId);
}