using System.Windows;
using System.Windows.Controls;
using Cryptify.ViewModels;

namespace Cryptify.Views;

public partial class CurrencyDetailsPage : Page
{
	public CurrencyDetailsPage(CurrencyDetailsPageViewModel viewModel)
	{
		InitializeComponent();
		
		DataContext = viewModel;
	}
	
}