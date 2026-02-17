using KickBlastEliteUI.Data;
using KickBlastEliteUI.Services;
using KickBlastEliteUI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace KickBlastEliteUI;

public partial class App : Application
{
    public static IHost? Host { get; private set; }

    protected override async void OnStartup(StartupEventArgs e)
    {
        Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "Data"));

        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((_, config) =>
            {
                config.SetBasePath(AppContext.BaseDirectory);
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<PricingSettings>(context.Configuration.GetSection("Pricing"));
                services.AddSingleton<NotificationService>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<IPricingService, PricingService>();
                services.AddSingleton<IDataService, DataService>();
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlite("Data Source=Data/kickblastelite.db"));

                services.AddSingleton<MainWindowViewModel>();
                services.AddTransient<DashboardViewModel>();
                services.AddTransient<AthletesViewModel>();
                services.AddTransient<CalculatorViewModel>();
                services.AddTransient<HistoryViewModel>();
                services.AddTransient<SettingsViewModel>();
                services.AddSingleton<MainWindow>();
            })
            .Build();

        await Host.StartAsync();

        using (var scope = Host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await db.Database.EnsureCreatedAsync();
            await DbSeeder.SeedAsync(db);
        }

        var mainWindow = Host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (Host != null)
        {
            await Host.StopAsync();
            Host.Dispose();
        }

        base.OnExit(e);
    }
}
