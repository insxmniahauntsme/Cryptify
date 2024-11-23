using CryptifyAPI.Models;
using CryptifyAPI.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace Cryptify.ViewModels
{
	public class MainWindowViewModel
	{
		private readonly ICryptocurrencyService _cryptocurrencyService;

		public ObservableCollection<Currency> Currencies { get; set; }

		public MainWindowViewModel(ICryptocurrencyService cryptocurrencyService)
		{
			_cryptocurrencyService = cryptocurrencyService;
			Currencies = new ObservableCollection<Currency>();
			LoadTopCurrencies();
		}

		public async void LoadTopCurrencies()
		{
			List<Currency>? currencies = await _cryptocurrencyService.GetTopTenCurrencies();
			foreach (var currency in currencies)
			{
				Currencies.Add(currency);
			}
			MessageBox.Show($"Loaded {Currencies.Count} currencies.");

		}
	}
}