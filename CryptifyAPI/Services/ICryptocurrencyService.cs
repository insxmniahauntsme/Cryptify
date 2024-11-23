using CryptifyAPI.Models;

namespace CryptifyAPI.Services;

public interface ICryptocurrencyService
{
	Task<List<Currency>> GetTopTenCurrencies();
}