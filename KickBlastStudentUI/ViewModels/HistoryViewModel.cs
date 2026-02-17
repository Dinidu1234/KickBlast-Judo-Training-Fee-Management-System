using System.Collections.ObjectModel;
using KickBlastStudentUI.Helpers;
using KickBlastStudentUI.Models;
using KickBlastStudentUI.Services;
using Microsoft.EntityFrameworkCore;

namespace KickBlastStudentUI.ViewModels;

public class HistoryViewModel : ObservableObject
{
    private readonly ToastService _toastService;
    private Athlete? _selectedAthlete;
    private int _selectedMonth;
    private int _selectedYear;
    private MonthlyCalculation? _selectedCalculation;
    private string _details = "Select a calculation to view details.";

    public HistoryViewModel(ToastService toastService)
    {
        _toastService = toastService;

        Athletes = new ObservableCollection<Athlete>(App.DbContext!.Athletes.OrderBy(a => a.Name).ToList());
        Calculations = new ObservableCollection<MonthlyCalculation>();
        Months = Enumerable.Range(1, 12).ToList();
        Years = Enumerable.Range(DateTime.Now.Year - 3, 5).ToList();

        SelectedMonth = DateTime.Now.Month;
        SelectedYear = DateTime.Now.Year;

        ApplyFilterCommand = new RelayCommand(_ => LoadHistory());

        LoadHistory();
    }

    public ObservableCollection<Athlete> Athletes { get; }
    public ObservableCollection<MonthlyCalculation> Calculations { get; }
    public List<int> Months { get; }
    public List<int> Years { get; }

    public Athlete? SelectedAthlete { get => _selectedAthlete; set => SetProperty(ref _selectedAthlete, value); }
    public int SelectedMonth { get => _selectedMonth; set => SetProperty(ref _selectedMonth, value); }
    public int SelectedYear { get => _selectedYear; set => SetProperty(ref _selectedYear, value); }
    public string Details { get => _details; set => SetProperty(ref _details, value); }

    public MonthlyCalculation? SelectedCalculation
    {
        get => _selectedCalculation;
        set
        {
            SetProperty(ref _selectedCalculation, value);
            if (value != null)
            {
                Details = $"Athlete: {value.Athlete?.Name}\nDate: {value.CalculationDate:yyyy-MM-dd HH:mm}\n" +
                          $"Training: {CurrencyHelper.ToLkr(value.TrainingCost)}\n" +
                          $"Coaching: {CurrencyHelper.ToLkr(value.CoachingCost)}\n" +
                          $"Competition: {CurrencyHelper.ToLkr(value.CompetitionCost)}\n" +
                          $"Total: {CurrencyHelper.ToLkr(value.TotalCost)}\n" +
                          $"Weight: {value.WeightStatus} ({value.WeightDifferenceKg:+0.00;-0.00;0.00} kg)\n" +
                          $"2nd Saturday: {value.SecondSaturdayDate:yyyy-MM-dd}";
            }
        }
    }

    public RelayCommand ApplyFilterCommand { get; }

    private void LoadHistory()
    {
        try
        {
            var query = App.DbContext!.MonthlyCalculations.Include(c => c.Athlete).AsQueryable();

            if (SelectedAthlete != null)
                query = query.Where(c => c.AthleteId == SelectedAthlete.Id);

            query = query.Where(c => c.CalculationDate.Month == SelectedMonth && c.CalculationDate.Year == SelectedYear);

            Calculations.Clear();
            foreach (var item in query.OrderByDescending(c => c.CalculationDate).ToList())
                Calculations.Add(item);

            _toastService.ShowSuccess("History loaded.");
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Could not load history: {ex.Message}");
        }
    }
}
