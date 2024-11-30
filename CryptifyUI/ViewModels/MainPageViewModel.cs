using CryptifyAPI.Models;
using CryptifyAPI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Cryptify.Services;
using Cryptify.Views;
using Cryptify.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Cryptify.ViewModels
{
	public class MainPageViewModel
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly ICryptocurrencyService _cryptocurrencyService;
		private readonly INavigationService _navigationService;
		public ICommand CurrencyClickCommand { get; }
		public ObservableCollection<Currency> Currencies { get; set; }
		
		public MainPageViewModel(ICryptocurrencyService cryptocurrencyService, IServiceProvider serviceProvider, INavigationService navigationService)
		{
			_cryptocurrencyService = cryptocurrencyService;
			_navigationService = navigationService;
			_serviceProvider = serviceProvider;
			Currencies = new ObservableCollection<Currency>();
			CurrencyClickCommand = new RelayCommand(OnCurrencyClick);
			LoadTopCurrencies();
		}

		private async void LoadTopCurrencies()
		{
			try
			{
				List<Currency>? currencies = await _cryptocurrencyService.GetTopTenCurrenciesAsync();
				foreach (var currency in currencies)
				{
					Currencies.Add(currency);
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		
		private void OnCurrencyClick(object selectedCurrency)
		{
			var currency = (Currency)selectedCurrency;

			var detailsViewModel = _serviceProvider.GetRequiredService<CurrencyDetailsPageViewModel>();

			detailsViewModel.Currency = currency;

			var detailsPage = new CurrencyDetailsPage(detailsViewModel);
    
			detailsPage.DataContext = detailsViewModel;

			_navigationService.NavigateTo(detailsPage);
		}
	}
}