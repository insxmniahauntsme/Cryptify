using System.Windows.Controls;

namespace Cryptify.Services;

public interface INavigationService
{
	void NavigateTo(Page page);
	void GoBack();
	void GoForward();
}