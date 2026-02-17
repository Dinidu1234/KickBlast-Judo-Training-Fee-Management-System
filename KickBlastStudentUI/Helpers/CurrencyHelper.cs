using System.Globalization;

namespace KickBlastStudentUI.Helpers;

public static class CurrencyHelper
{
    public static string ToLkr(decimal amount) => string.Format(CultureInfo.InvariantCulture, "LKR {0:N2}", amount);
}
