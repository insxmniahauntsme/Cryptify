using System.Text.Json;
using CryptifyAPI.Models;
using Microsoft.Extensions.Configuration;

namespace CryptifyAPI.Services
{
    public class CryptocurrencyService : ICryptocurrencyService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public CryptocurrencyService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<List<Currency>> GetTopTenCurrenciesAsync()
        {
            var client = _httpClientFactory.CreateClient("CoinGeckoClient");
            var response = await GetApiResponseAsync(client, 
                "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=10&page=1");

            if (!response.IsSuccessStatusCode)
                return new List<Currency>();

            var currencies = await DeserializeJsonAsync<List<Currency>>(response.Content);
            return currencies?.OrderByDescending(c => c.CurrentPrice).ToList() ?? new List<Currency>();
        }

        public async Task<List<Currency>> GetAllCurrenciesAsync()
        {
            var client = _httpClientFactory.CreateClient("CoinGeckoClient");
            var response = await GetApiResponseAsync(client,
                "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=100&page=1");
            
            if (!response.IsSuccessStatusCode)
                return new List<Currency>();
            
            var currencies = await DeserializeJsonAsync<List<Currency>>(response.Content);
            return currencies?.ToList() ?? new List<Currency>();
        }

        public async Task<List<Market>> GetTopMarketsAsync(string currencyId)
        {
            var baseUrl = "https://api.coingecko.com/api/v3/coins";
            var endpoint = $"{baseUrl}/{currencyId}/tickers";

            var response = await GetApiResponseAsync(new HttpClient(), endpoint);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch top markets.");

            var tickers = await DeserializeJsonAsync<JsonDocument>(response.Content);
            var tickersArray = tickers.RootElement.GetProperty("tickers").EnumerateArray();

            return tickersArray.Take(6).Select(ticker => new Market
            {
                Name = ticker.GetProperty("market").GetProperty("name").GetString() ?? "Undefined",
                Base = ticker.GetProperty("base").GetString() ?? "Undefined",
                Target = ticker.GetProperty("target").GetString() ?? "Undefined",
                Price = ticker.TryGetProperty("last", out var priceElement) ? priceElement.GetDecimal() : 0,
                TradeUrl = ticker.GetProperty("trade_url").GetString() ?? "N/A"
            }).OrderByDescending(m => m.Price).ToList();
        }

        public async Task<Dictionary<string, int>> GetChartDataAsync(string currencyId)
        {
            var baseUrl = "https://api.coingecko.com/api/v3/coins/bitcoin/market_chart?vs_currency=usd&days=1";
        }

        private async Task<HttpResponseMessage> GetApiResponseAsync(HttpClient client, string url)
        {
            try
            {
                return await client.GetAsync(url);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e);
                throw new Exception("Error during API call.", e);
            }
        }

        private async Task<T> DeserializeJsonAsync<T>(HttpContent content)
        {
            string responseString = await content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            };

            return JsonSerializer.Deserialize<T>(responseString, options);
        }
    }
}
