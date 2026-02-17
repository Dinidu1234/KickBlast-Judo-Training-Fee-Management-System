using KickBlastEliteUI.Helpers;
using KickBlastEliteUI.Models;
using KickBlastEliteUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KickBlastEliteUI.ViewModels;

public class CalculatorViewModel : BasePageViewModel
{
    private readonly IDataService _dataService;
    private readonly IPricingService _pricingService;
    private readonly NotificationService _notification;

    private Athlete? _selectedAthlete;
    private decimal _coachingHours;
    private int _competitionsCount;
    private string _trainingCost = "LKR 0.00";
    private string _coachingCost = "LKR 0.00";
    private string _competitionCost = "LKR 0.00";
    private string _totalCost = "LKR 0.00";
    private string _weightStatus = "-";
    private string _secondSaturday = "-";

    public CalculatorViewModel(IDataService dataService, IPricingService pricingService, NotificationService notification)
    {
        _dataService = dataService;
        _pricingService = pricingService;
        _notification = notification;

        CalculateAndSaveCommand = new RelayCommand(async () => await CalculateAndSaveAsync(), CanCalculate);
    }

    public ObservableCollection<Athlete> Athletes { get; } = [];

    public Athlete? SelectedAthlete
    {
        get => _selectedAthlete;
        set
        {
            if (SetProperty(ref _selectedAthlete, value) && value?.TrainingPlan?.Name == "Beginner")
            {
                CompetitionsCount = 0;
            }
        }
    }

    public decimal CoachingHours
    {
        get => _coachingHours;
        set => SetProperty(ref _coachingHours, value);
    }

    public int CompetitionsCount
    {
        get => _competitionsCount;
        set => SetProperty(ref _competitionsCount, value);
    }

    public string TrainingCost { get => _trainingCost; set => SetProperty(ref _trainingCost, value); }
    public string CoachingCost { get => _coachingCost; set => SetProperty(ref _coachingCost, value); }
    public string CompetitionCost { get => _competitionCost; set => SetProperty(ref _competitionCost, value); }
    public string TotalCost { get => _totalCost; set => SetProperty(ref _totalCost, value); }
    public string WeightStatus { get => _weightStatus; set => SetProperty(ref _weightStatus, value); }
    public string SecondSaturday { get => _secondSaturday; set => SetProperty(ref _secondSaturday, value); }

    public ICommand CalculateAndSaveCommand { get; }

    public override async Task InitializeAsync()
    {
        var athletes = await _dataService.GetAthletesAsync();
        Athletes.Clear();
        foreach (var athlete in athletes)
        {
            Athletes.Add(athlete);
        }

        SecondSaturday = GetSecondSaturday(DateTime.Today).ToString("dd MMM yyyy");
    }

    private bool CanCalculate()
    {
        if (SelectedAthlete == null) return false;
        if (CoachingHours < 0 || CoachingHours > 5) return false;
        if (CompetitionsCount < 0) return false;
        if (SelectedAthlete.TrainingPlan?.Name == "Beginner" && CompetitionsCount > 0) return false;
        return true;
    }

    private async Task CalculateAndSaveAsync()
    {
        try
        {
            if (!CanCalculate())
            {
                _notification.ShowError("Invalid inputs. Coaching hours must be 0-5 and competitions cannot be negative.");
                return;
            }

            var pricing = _pricingService.GetCurrentPricing();
            var planName = SelectedAthlete!.TrainingPlan?.Name ?? "Beginner";

            if (planName == "Beginner")
            {
                CompetitionsCount = 0;
            }

            var weeklyFee = planName switch
            {
                "Beginner" => pricing.BeginnerWeeklyFee,
                "Intermediate" => pricing.IntermediateWeeklyFee,
                "Elite" => pricing.EliteWeeklyFee,
                _ => pricing.BeginnerWeeklyFee
            };

            var trainingCost = weeklyFee * 4;
            var coachingCost = CoachingHours * 4 * pricing.CoachingHourlyRate;
            var competitionCost = CompetitionsCount * pricing.CompetitionFee;
            var total = trainingCost + coachingCost + competitionCost;

            TrainingCost = CurrencyHelper.ToLkr(trainingCost);
            CoachingCost = CurrencyHelper.ToLkr(coachingCost);
            CompetitionCost = CurrencyHelper.ToLkr(competitionCost);
            TotalCost = CurrencyHelper.ToLkr(total);

            var delta = SelectedAthlete.CurrentWeight - SelectedAthlete.CompetitionCategoryWeight;
            WeightStatus = delta switch
            {
                > 0 => $"Over by {Math.Abs(delta):0.##} kg",
                < 0 => $"Under by {Math.Abs(delta):0.##} kg",
                _ => "On target"
            };

            var now = DateTime.Now;
            await _dataService.SaveCalculationAsync(new MonthlyCalculation
            {
                AthleteId = SelectedAthlete.Id,
                Month = now.Month,
                Year = now.Year,
                TrainingCost = trainingCost,
                CoachingCost = coachingCost,
                CompetitionCost = competitionCost,
                TotalCost = total,
                CompetitionsCount = CompetitionsCount,
                CoachingHours = CoachingHours,
                CreatedAt = now
            });

            _notification.ShowSuccess("Calculation saved.");
        }
        catch (Exception ex)
        {
            _notification.ShowError($"Calculation error: {ex.Message}");
        }
    }

    private static DateTime GetSecondSaturday(DateTime date)
    {
        var first = new DateTime(date.Year, date.Month, 1);
        var offset = ((int)DayOfWeek.Saturday - (int)first.DayOfWeek + 7) % 7;
        return first.AddDays(offset + 7);
    }
}
