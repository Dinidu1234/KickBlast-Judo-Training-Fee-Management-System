using KickBlastEliteUI.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace KickBlastEliteUI.Services;

public class PricingService(IOptionsMonitor<PricingSettings> options) : IPricingService
{
    private readonly IOptionsMonitor<PricingSettings> _options = options;
    private static readonly string SettingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

    public PricingSettings GetCurrentPricing() => _options.CurrentValue;

    public async Task SavePricingAsync(PricingSettings pricing)
    {
        var payload = new { Pricing = pricing };
        var output = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(SettingsPath, output);
    }
}
