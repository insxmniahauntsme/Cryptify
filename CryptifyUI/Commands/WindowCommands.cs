using System.Windows;
using System.Windows.Input;

namespace Cryptify.Commands;

public static class WindowCommands
{
	public static ICommand CloseCommand => new RelayCommand((param) =>
	{
		if (param is Window window)
		{
			window.Close();
		}
	});

	public static ICommand MinimizeCommand => new RelayCommand((param) =>
	{
		if (param is Window window)
		{
			window.WindowState = WindowState.Minimized;
		}
	});

	public static ICommand ToggleMaximizeCommand => new RelayCommand((param) =>
	{
		if (param is Window window)
		{
			window.WindowState = window.WindowState == WindowState.Maximized
				? WindowState.Normal
				: WindowState.Maximized;
		}
	});

	public static ICommand DragWindow => new RelayCommand((param) =>
	{
		if (param is Window window)
		{
			window.DragMove();
		}
	});
}