using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Cryptify.Commands;
using Cryptify.Services;
using Cryptify.Views;
using CryptifyAPI.Models;
using CryptifyAPI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cryptify.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly ICryptocurrencyService _cryptocurrencyService;
        private readonly IServiceProvider _serviceProvider;

        private List<Currency> _currencies = new();
        private List<Currency>? _searchResults;
        public List<Currency>? SearchResults
        {
            get => _searchResults;
            set
            {
                _searchResults = value;
                OnPropertyChanged();
            }
        }

        private string? _searchQuery;
        public string? SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged();

                    if (!string.IsNullOrWhiteSpace(_searchQuery) && _searchQuery.Length >= 2)
                    {
                        PerformSearch(_searchQuery);
                    }
                    else
                    {
                        SearchResults = null; // Close popup by setting results to null
                    }
                }
            }
        }

        private Currency? _selectedCurrency;
        public Currency? SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                _selectedCurrency = value;
                OnPropertyChanged();
                if (_selectedCurrency != null)
                {
                    NavigateToCurrencyDetails(_selectedCurrency);
                }
            }
        }

        public ICommand GoBackCommand { get; }
        public ICommand GoForwardCommand { get; }

        public MainWindowViewModel(INavigationService navigationService, ICryptocurrencyService cryptocurrencyService, IServiceProvider serviceProvider)
        {
            _navigationService = navigationService;
            _cryptocurrencyService = cryptocurrencyService;
            _serviceProvider = serviceProvider;
            GoBackCommand = new RelayCommand(_ => GoBack());
            GoForwardCommand = new RelayCommand(_ => GoForward());
            LoadAllCurrencies();
        }

        private async void LoadAllCurrencies()
        {
            _currencies = await _cryptocurrencyService.GetAllCurrenciesAsync();
        }

        private void PerformSearch(string query)
        {
            var currencies = _currencies
                .Where(c => c.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || c.Id.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            SearchResults = currencies;
        }

        private async void NavigateToCurrencyDetails(Currency selectedCurrency)
        {
            var detailsViewModel = _serviceProvider.GetRequiredService<CurrencyDetailsPageViewModel>();

            detailsViewModel.Currency = selectedCurrency;
            detailsViewModel.Markets = await detailsViewModel._cryptocurrencyService.GetTopMarketsAsync(selectedCurrency.Id);
            var detailsPage = new CurrencyDetailsPage(detailsViewModel);

            detailsPage.DataContext = detailsViewModel;

            _navigationService.NavigateTo(detailsPage);
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
