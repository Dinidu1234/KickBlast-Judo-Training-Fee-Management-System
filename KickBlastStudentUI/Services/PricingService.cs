using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace KickBlastStudentUI.Services;

public class PricingService
{
    private readonly string _settingsPath;

    public PricingService(string settingsPath)
    {
        _settingsPath = settingsPath;
        Pricing = LoadPricing();
    }

    public PricingConfig Pricing { get; private set; }

    public PricingConfig LoadPricing()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(_settingsPath) ?? AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(Path.GetFileName(_settingsPath), optional: false, reloadOnChange: false)
            .Build();

        return configuration.GetSection("Pricing").Get<PricingConfig>() ?? new PricingConfig();
    }

    public void SavePricing(PricingConfig pricing)
    {
        var root = new AppSettingsRoot { Pricing = pricing };
        var json = JsonSerializer.Serialize(root, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_settingsPath, json);
        Pricing = pricing;
    }
}

public class PricingConfig
{
    public decimal BeginnerWeeklyFee { get; set; } = 2000;
    public decimal IntermediateWeeklyFee { get; set; } = 3000;
    public decimal EliteWeeklyFee { get; set; } = 4500;
    public decimal CompetitionFee { get; set; } = 1500;
    public decimal CoachingHourlyRate { get; set; } = 1200;
}

public class AppSettingsRoot
{
    public PricingConfig Pricing { get; set; } = new();
}
