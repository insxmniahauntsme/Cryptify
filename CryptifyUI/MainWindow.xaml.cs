using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cryptify.ViewModels;

namespace Cryptify;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow(MainWindowViewModel viewModel)
	{
		InitializeComponent();
		
		DataContext = viewModel;
	}
	
	private void Minimize_Click(object sender, RoutedEventArgs e)
	{
		WindowState = WindowState.Minimized;
	}

	private void Restore_Click(object sender, RoutedEventArgs e)
	{
		if (WindowState == WindowState.Maximized)
		{
			WindowState = WindowState.Normal;
		}
		else
		{
			WindowState = WindowState.Maximized;
		}
	}

	private void Close_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}
	
	private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		if (e.ButtonState == MouseButtonState.Pressed)
		{
			DragMove();
		}
	}


}