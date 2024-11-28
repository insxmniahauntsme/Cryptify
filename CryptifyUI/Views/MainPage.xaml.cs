using System.Windows.Controls;
using Cryptify.ViewModels;
using CryptifyAPI.Models;

namespace Cryptify.Views;

public partial class MainPage : Page
{
	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();

		DataContext = viewModel;
	}
	
	
}