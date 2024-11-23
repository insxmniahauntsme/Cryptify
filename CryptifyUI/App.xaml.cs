using System;
using System.Windows;
using Cryptify.ViewModels;
using CryptifyAPI.Configuration;
using CryptifyAPI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cryptify;

public partial class App : Application
{
	public IServiceProvider Services { get; private set; }

	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		var serviceCollection = new ServiceCollection();
		serviceCollection.AddCryptifyServices();
		serviceCollection.AddTransient<MainWindowViewModel>();
		serviceCollection.AddTransient<MainWindow>();
		Services = serviceCollection.BuildServiceProvider();

		var mainWindow = Services.GetRequiredService<MainWindow>();
		mainWindow.Show();
	}
}