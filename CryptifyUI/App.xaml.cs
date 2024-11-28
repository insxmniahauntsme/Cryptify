using System.Windows;
using System.Windows.Controls;
using Cryptify.Services;
using Cryptify.ViewModels;
using Cryptify.Views;
using CryptifyAPI.Configuration;
using Microsoft.Extensions.DependencyInjection;
 
namespace Cryptify;

public partial class App : Application
{
	public IServiceProvider? Services { get; private set; }

	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		var serviceCollection = new ServiceCollection();
		serviceCollection.AddCryptifyServices();
		serviceCollection.AddSingleton<Frame>(serviceProvider =>
		{
			var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
			return mainWindow.MainFrame;
		});
		serviceCollection.AddSingleton<INavigationService, NavigationService>();
		serviceCollection.AddTransient<MainPageViewModel>();
		serviceCollection.AddTransient<MainPage>();
		serviceCollection.AddTransient<CurrencyDetailsPageViewModel>();
		serviceCollection.AddTransient<CurrencyDetailsPage>();
		serviceCollection.AddTransient<MainWindow>();
		Services = serviceCollection.BuildServiceProvider();

		var mainWindow = Services.GetRequiredService<MainWindow>();

		var frame = mainWindow.MainFrame;
		var navigationService = Services.GetRequiredService<INavigationService>() as NavigationService;
		navigationService?.SetFrame(frame);

		mainWindow.Show();
	}
}