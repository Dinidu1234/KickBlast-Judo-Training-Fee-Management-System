using KickBlastStudentUI.Helpers;
using KickBlastStudentUI.Services;

namespace KickBlastStudentUI.ViewModels;

public class SettingsViewModel : ObservableObject
{
    private readonly ToastService _toastService;

    public SettingsViewModel(ToastService toastService)
    {
        _toastService = toastService;
        LoadFromService();
        SaveSettingsCommand = new RelayCommand(_ => SaveSettings());
    }

    public decimal BeginnerWeeklyFee { get; set; }
    public decimal IntermediateWeeklyFee { get; set; }
    public decimal EliteWeeklyFee { get; set; }
    public decimal CompetitionFee { get; set; }
    public decimal CoachingHourlyRate { get; set; }

    public RelayCommand SaveSettingsCommand { get; }

    private void LoadFromService()
    {
        var p = App.PricingService!.Pricing;
        BeginnerWeeklyFee = p.BeginnerWeeklyFee;
        IntermediateWeeklyFee = p.IntermediateWeeklyFee;
        EliteWeeklyFee = p.EliteWeeklyFee;
        CompetitionFee = p.CompetitionFee;
        CoachingHourlyRate = p.CoachingHourlyRate;
    }

    private void SaveSettings()
    {
        try
        {
            var pricing = new PricingConfig
            {
                BeginnerWeeklyFee = BeginnerWeeklyFee,
                IntermediateWeeklyFee = IntermediateWeeklyFee,
                EliteWeeklyFee = EliteWeeklyFee,
                CompetitionFee = CompetitionFee,
                CoachingHourlyRate = CoachingHourlyRate
            };

            App.PricingService!.SavePricing(pricing);

            // Keep plan fees in sync in DB for calculator and athlete forms.
            var plans = App.DbContext!.TrainingPlans.ToList();
            foreach (var plan in plans)
            {
                plan.WeeklyFee = plan.Name switch
                {
                    "Beginner" => pricing.BeginnerWeeklyFee,
                    "Intermediate" => pricing.IntermediateWeeklyFee,
                    "Elite" => pricing.EliteWeeklyFee,
                    _ => plan.WeeklyFee
                };
            }
            App.DbContext.SaveChanges();

            _toastService.ShowSuccess("Settings saved. Restart app to reload all screens.");
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Could not save settings: {ex.Message}");
        }
    }
}
