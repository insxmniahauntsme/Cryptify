using CryptifyAPI.Models;

namespace CryptifyAPI.Services;

public interface ICryptocurrencyService
{
	Task<List<Currency>> GetTopTenCurrenciesAsync();
	Task<List<Currency>> GetAllCurrenciesAsync();
	Task<List<Market>> GetTopMarketsAsync(string currencyId);
	Task<List<ChartPoint>> GetChartDataAsync(string currencyId);

}