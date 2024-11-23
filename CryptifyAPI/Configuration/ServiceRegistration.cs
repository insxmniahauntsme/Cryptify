using System;
using CryptifyAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptifyAPI.Configuration;

public static class ServiceRegistration
{
	public static IServiceCollection AddCryptifyServices(this IServiceCollection services)
	{
		var builder = new ConfigurationBuilder()
			.SetBasePath(AppContext.BaseDirectory)
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
		var configuration = builder.Build();

		services.AddSingleton<IConfiguration>(configuration);

		services.AddHttpClient("CoinGeckoClient", client =>
		{
			var baseUrl = configuration.GetSection("API").GetValue<string>("base-url");
			if (string.IsNullOrEmpty(baseUrl))
				throw new Exception("Base URL is not configured in appsettings.json.");

			client.BaseAddress = new Uri(baseUrl);
			client.DefaultRequestHeaders.Add("Accept", "application/json");
			client.DefaultRequestHeaders.Add("User-Agent", "Cryptify");
		});

		services.AddTransient<ICryptocurrencyService, CryptocurrencyService>();

		return services;
	}
}