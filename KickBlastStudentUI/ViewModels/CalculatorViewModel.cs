using System.Collections.ObjectModel;
using KickBlastStudentUI.Helpers;
using KickBlastStudentUI.Models;
using KickBlastStudentUI.Services;
using Microsoft.EntityFrameworkCore;

namespace KickBlastStudentUI.ViewModels;

public class CalculatorViewModel : ObservableObject
{
    private readonly ToastService _toastService;
    private readonly FeeCalculatorService _calculatorService;
    private Athlete? _selectedAthlete;
    private int _competitionsThisMonth;
    private decimal _coachingHoursPerWeek;
    private string _result = "Calculate monthly fee to see breakdown.";
    private string _beginnerNote = string.Empty;
    private FeeCalculationResult? _lastResult;

    public CalculatorViewModel(ToastService toastService)
    {
        _toastService = toastService;
        _calculatorService = new FeeCalculatorService(App.PricingService!);
        Athletes = new ObservableCollection<Athlete>(App.DbContext!.Athletes.Include(a => a.TrainingPlan).OrderBy(a => a.Name).ToList());

        CalculateCommand = new RelayCommand(_ => Calculate());
        SaveCalculationCommand = new RelayCommand(_ => SaveCalculation(), _ => _lastResult != null && SelectedAthlete != null);
    }

    public ObservableCollection<Athlete> Athletes { get; }
    public Athlete? SelectedAthlete
    {
        get => _selectedAthlete;
        set
        {
            SetProperty(ref _selectedAthlete, value);
            BeginnerNote = value?.TrainingPlan?.Name == "Beginner" ? "Beginner athlete: competitions set to 0 automatically." : string.Empty;
        }
    }
    public int CompetitionsThisMonth { get => _competitionsThisMonth; set => SetProperty(ref _competitionsThisMonth, Math.Max(0, value)); }
    public decimal CoachingHoursPerWeek { get => _coachingHoursPerWeek; set => SetProperty(ref _coachingHoursPerWeek, Math.Clamp(value, 0, 5)); }
    public string ResultText { get => _result; set => SetProperty(ref _result, value); }
    public string BeginnerNote { get => _beginnerNote; set => SetProperty(ref _beginnerNote, value); }

    public RelayCommand CalculateCommand { get; }
    public RelayCommand SaveCalculationCommand { get; }

    private void Calculate()
    {
        if (SelectedAthlete == null)
        {
            _toastService.ShowError("Select an athlete first.");
            return;
        }

        _lastResult = _calculatorService.Calculate(SelectedAthlete, CompetitionsThisMonth, CoachingHoursPerWeek);
        CompetitionsThisMonth = _lastResult.CompetitionsApplied;

        ResultText = $"Training: {CurrencyHelper.ToLkr(_lastResult.TrainingCost)}\n" +
                     $"Coaching: {CurrencyHelper.ToLkr(_lastResult.CoachingCost)}\n" +
                     $"Competition: {CurrencyHelper.ToLkr(_lastResult.CompetitionCost)}\n" +
                     $"TOTAL: {CurrencyHelper.ToLkr(_lastResult.Total)}\n\n" +
                     $"Weight: {_lastResult.WeightStatus} ({_lastResult.WeightDifferenceKg:+0.00;-0.00;0.00} kg)\n" +
                     $"2nd Saturday: {_lastResult.SecondSaturday:yyyy-MM-dd}";

        _toastService.ShowSuccess("Calculation completed.");
    }

    private void SaveCalculation()
    {
        if (SelectedAthlete == null || _lastResult == null)
            return;

        try
        {
            App.DbContext!.MonthlyCalculations.Add(new MonthlyCalculation
            {
                AthleteId = SelectedAthlete.Id,
                CalculationDate = DateTime.Now,
                CompetitionsThisMonth = _lastResult.CompetitionsApplied,
                CoachingHoursPerWeek = CoachingHoursPerWeek,
                TrainingCost = _lastResult.TrainingCost,
                CoachingCost = _lastResult.CoachingCost,
                CompetitionCost = _lastResult.CompetitionCost,
                TotalCost = _lastResult.Total,
                WeightStatus = _lastResult.WeightStatus,
                WeightDifferenceKg = _lastResult.WeightDifferenceKg,
                SecondSaturdayDate = _lastResult.SecondSaturday
            });

            App.DbContext.SaveChanges();
            _toastService.ShowSuccess("Calculation saved.");
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Could not save calculation: {ex.Message}");
        }
    }
}
