namespace KickBlastStudentUI.Helpers;

public static class DateHelper
{
    public static DateTime GetSecondSaturday(DateTime date)
    {
        var firstDay = new DateTime(date.Year, date.Month, 1);
        var dayOffset = ((int)DayOfWeek.Saturday - (int)firstDay.DayOfWeek + 7) % 7;
        var firstSaturday = firstDay.AddDays(dayOffset);
        return firstSaturday.AddDays(7);
    }
}
