using System.Windows.Controls;

namespace Cryptify.Services;

public class NavigationService : INavigationService
{
	private Frame? _frame;

	public void SetFrame(Frame frame)
	{
		_frame = frame;
	}

	public void NavigateTo(Page page)
	{
		_frame?.Navigate(page);
		Console.WriteLine($"Navigating to page: {page.GetType().Name}");
	}

	public void GoBack()
	{
		if (_frame?.CanGoBack == true)
		{
			_frame?.GoBack();	
		}
	}

	public void GoForward()
	{
		if (_frame?.CanGoForward == true)
		{
			_frame?.GoForward();
		}
	}
}
