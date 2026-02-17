using KickBlastEliteUI.Models;

namespace KickBlastEliteUI.Services;

public interface IPricingService
{
    PricingSettings GetCurrentPricing();
    Task SavePricingAsync(PricingSettings pricing);
}
