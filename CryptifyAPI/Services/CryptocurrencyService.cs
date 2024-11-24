using System.Text.Json;
using CryptifyAPI.Models;

namespace CryptifyAPI.Services;

public class CryptocurrencyService : ICryptocurrencyService
{
	private readonly IHttpClientFactory _httpClientFactory;

	public CryptocurrencyService(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	public async Task<List<Currency>> GetTopTenCurrencies()
	{
		var client = _httpClientFactory.CreateClient("CoinGeckoClient");

		HttpResponseMessage response = await client.GetAsync("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=10&page=1");

		Console.WriteLine(response.StatusCode);
		
		if (!response.IsSuccessStatusCode) return new List<Currency>();

		string responseString = await response.Content.ReadAsStringAsync();

		var options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
		};

		var currencies = JsonSerializer.Deserialize<List<Currency>>(responseString, options)!.OrderByDescending(c => c.CurrentPrice).ToList();
		return currencies ?? new List<Currency>();
	}

	
}