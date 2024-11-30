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

	public async Task<List<Currency>> GetTopTenCurrenciesAsync()
	{
		var client = _httpClientFactory.CreateClient("CoinGeckoClient");
		
		HttpResponseMessage response = new HttpResponseMessage();
		
		try
		{
			response = await client.GetAsync("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=10&page=1");
		}
		catch (HttpRequestException e)
		{
			Console.WriteLine(e);
			throw;
		}

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

	public async Task<List<Market>> GetTopMarketsAsync(string currencyId)
	{
		var baseUrl = "https://api.coingecko.com/api/v3/coins";
		var endpoint = $"{baseUrl}/{currencyId}/tickers?order=volume_desc&per_page=10";

		using (HttpClient client = new HttpClient())
		{
			var response = await client.GetAsync(endpoint);

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var jsonDocument = JsonDocument.Parse(jsonResponse);
				var tickersArray = jsonDocument.RootElement.GetProperty("tickers").EnumerateArray();

				var tickers = tickersArray
					.Select(ticker => new Market
					{
						Name = ticker.GetProperty("market").GetProperty("name").GetString() ?? "Undefined",
						Price = ticker.TryGetProperty("last", out var priceElement) ? priceElement.GetDecimal() : 0,
						Volume = ticker.TryGetProperty("volume", out var volumeElement) ? volumeElement.GetDecimal() : 0,
						TradeUrl = ticker.GetProperty("trade_url").GetString() ?? "N/A"
					})
					.ToList();

				return tickers;
			}
			else
			{
				throw new Exception("Failed to fetch top markets.");
			}
		}
	}
}