using System.Windows;
using System.Windows.Input;
using Cryptify.Services;
using Cryptify.ViewModels;
using Cryptify.Views;

namespace Cryptify;

public partial class MainWindow : Window
{
	private readonly INavigationService _navigationService;

	public MainWindow(INavigationService navigationService, MainWindowViewModel viewModel, MainPage mainPage)
	{
		InitializeComponent();
		_navigationService = navigationService;
        DataContext = viewModel;
		MainFrame.Navigate(mainPage);
	}

	private void DragWindow(object sender, MouseButtonEventArgs e)
	{
		if (e.ButtonState == MouseButtonState.Pressed)
		{
			DragMove();
		}
	}
	
}