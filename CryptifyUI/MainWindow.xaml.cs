using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cryptify.Services;
using Cryptify.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Cryptify;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private readonly IServiceProvider _serviceProvider;
	private readonly INavigationService _navigationService;
	public MainWindow(IServiceProvider serviceProvider, INavigationService navigationService)
	{
		InitializeComponent();
		_serviceProvider = serviceProvider;
		_navigationService = navigationService;

		MainFrame.Navigate(_serviceProvider.GetRequiredService<MainPage>());
	}

	private void DragWindow(object sender, MouseButtonEventArgs e)
	{
		if (e.ButtonState == MouseButtonState.Pressed)
		{
			DragMove();
		}
	}

	private void GoBack(object sender, RoutedEventArgs e)
	{
		_navigationService.GoBack();
	}

	private void GoForward(object sender, RoutedEventArgs e)
	{
		_navigationService.GoForward();
	}
}