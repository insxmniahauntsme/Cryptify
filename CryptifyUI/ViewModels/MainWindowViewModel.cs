using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Cryptify.Commands;
using Cryptify.Services;
using CryptifyAPI.Services;

namespace Cryptify.ViewModels
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		private readonly INavigationService _navigationService;
		private readonly ICryptocurrencyService _cryptocurrencyService;
		private string? _searchQuery;

		public string? SearchQuery
		{
			get => _searchQuery;
			set
			{
				if (_searchQuery != value && value != null)
				{
					_searchQuery = value;
					OnPropertyChanged();

					PerformSearch(_searchQuery);
				}
			}
		}
		
		public MainWindowViewModel(INavigationService navigationService, ICryptocurrencyService cryptocurrencyService)
		{
			_navigationService = navigationService;
			_cryptocurrencyService = cryptocurrencyService;
			PerformSearchCommand = new RelayCommand(_ => PerformSearch(_searchQuery));
			GoBackCommand = new RelayCommand(_ => GoBack());
			GoForwardCommand = new RelayCommand(_ => GoForward());
		}


		public ICommand PerformSearchCommand { get; }
		public ICommand GoBackCommand { get; }
		public ICommand GoForwardCommand { get; }


		private async void PerformSearch(string query)
		{
			
		}
		
		private void GoBack()
		{
			_navigationService.GoBack();
		}

		private void GoForward()
		{
			_navigationService.GoForward();
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


	}
}