using KickBlastEliteUI.Helpers;
using KickBlastEliteUI.Models;
using KickBlastEliteUI.Services;
using System.Collections.ObjectModel;

namespace KickBlastEliteUI.ViewModels;

public class DashboardViewModel(IDataService dataService) : BasePageViewModel
{
    private readonly IDataService _dataService = dataService;
    private int _totalAthletes;
    private int _calculationsThisMonth;
    private string _totalRevenue = "LKR 0.00";

    public ObservableCollection<MonthlyCalculation> RecentCalculations { get; } = [];

    public int TotalAthletes { get => _totalAthletes; set => SetProperty(ref _totalAthletes, value); }
    public int CalculationsThisMonth { get => _calculationsThisMonth; set => SetProperty(ref _calculationsThisMonth, value); }
    public string TotalRevenue { get => _totalRevenue; set => SetProperty(ref _totalRevenue, value); }
    public string NextCompetitionDate => GetSecondSaturday(DateTime.Today).ToString("dd MMM yyyy");

    public override async Task InitializeAsync()
    {
        var athletes = await _dataService.GetAthletesAsync();
        var calculations = await _dataService.GetCalculationsAsync();
        var month = DateTime.Today.Month;
        var year = DateTime.Today.Year;
        var revenue = await _dataService.GetRevenueForMonthAsync(month, year);

        TotalAthletes = athletes.Count;
        CalculationsThisMonth = calculations.Count(x => x.Month == month && x.Year == year);
        TotalRevenue = CurrencyHelper.ToLkr(revenue);

        RecentCalculations.Clear();
        foreach (var item in calculations.Take(8))
        {
            RecentCalculations.Add(item);
        }
    }

    private static DateTime GetSecondSaturday(DateTime date)
    {
        var firstOfMonth = new DateTime(date.Year, date.Month, 1);
        var days = ((int)DayOfWeek.Saturday - (int)firstOfMonth.DayOfWeek + 7) % 7;
        return firstOfMonth.AddDays(days + 7);
    }
}
