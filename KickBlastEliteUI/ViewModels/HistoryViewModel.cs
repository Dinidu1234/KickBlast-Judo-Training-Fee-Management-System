using KickBlastEliteUI.Models;
using KickBlastEliteUI.Services;
using System.Collections.ObjectModel;

namespace KickBlastEliteUI.ViewModels;

public class HistoryViewModel(IDataService dataService) : BasePageViewModel
{
    private readonly IDataService _dataService = dataService;
    private Athlete? _selectedAthlete;
    private int _month = DateTime.Today.Month;
    private int _year = DateTime.Today.Year;
    private MonthlyCalculation? _selectedCalculation;

    public ObservableCollection<Athlete> Athletes { get; } = [];
    public ObservableCollection<MonthlyCalculation> Calculations { get; } = [];

    public Athlete? SelectedAthlete { get => _selectedAthlete; set { if (SetProperty(ref _selectedAthlete, value)) _ = InitializeAsync(); } }
    public int Month { get => _month; set { if (SetProperty(ref _month, value)) _ = InitializeAsync(); } }
    public int Year { get => _year; set { if (SetProperty(ref _year, value)) _ = InitializeAsync(); } }
    public MonthlyCalculation? SelectedCalculation { get => _selectedCalculation; set => SetProperty(ref _selectedCalculation, value); }

    public override async Task InitializeAsync()
    {
        var athletes = await _dataService.GetAthletesAsync();
        Athletes.Clear();
        foreach (var athlete in athletes)
        {
            Athletes.Add(athlete);
        }

        var calculations = await _dataService.GetCalculationsAsync();
        var filtered = calculations.Where(c => c.Month == Month && c.Year == Year);
        if (SelectedAthlete != null)
        {
            filtered = filtered.Where(c => c.AthleteId == SelectedAthlete.Id);
        }

        Calculations.Clear();
        foreach (var calc in filtered)
        {
            Calculations.Add(calc);
        }
    }
}
