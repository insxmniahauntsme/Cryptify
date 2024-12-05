using System.Windows;
using Cryptify.Services;
using Cryptify.ViewModels;
using Cryptify.Views;
using CryptifyAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cryptify;

public partial class App : Application
{
    public IServiceProvider? Services { get; private set; }

    public App()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        AddCryptifyServices(serviceCollection);

        Services = serviceCollection.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        if (Services == null)
            throw new Exception("Services are not initialized.");

        var navigationService = Services.GetRequiredService<INavigationService>() as NavigationService;
        var mainWindow = Services.GetRequiredService<MainWindow>();

        navigationService?.SetFrame(mainWindow.MainFrame);

        mainWindow.Show();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<MainPageViewModel>();
        services.AddTransient<CurrencyDetailsPageViewModel>();
        services.AddTransient<MainPage>();
        services.AddTransient<CurrencyDetailsPage>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddTransient<MainWindowViewModel>();
        services.AddSingleton<MainWindow>();
    }

    private static void AddCryptifyServices(IServiceCollection services)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var configuration = builder.Build();

        services.AddSingleton<IConfiguration>(configuration);

        services.AddHttpClient("CoinGeckoClient", client =>
        {
            var baseUrl = configuration.GetSection("API").GetValue<string>("base-url");
            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception("Base URL is not configured in appsettings.json.");

            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "Cryptify");
        });

        services.AddTransient<ICryptocurrencyService, CryptocurrencyService>();
    }
}
