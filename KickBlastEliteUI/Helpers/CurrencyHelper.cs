using System.Globalization;

namespace KickBlastEliteUI.Helpers;

public static class CurrencyHelper
{
    public static string ToLkr(decimal amount)
    {
        var culture = (CultureInfo)CultureInfo.GetCultureInfo("en-LK").Clone();
        culture.NumberFormat.CurrencySymbol = "LKR ";
        return string.Format(culture, "{0:C2}", amount);
    }
}
