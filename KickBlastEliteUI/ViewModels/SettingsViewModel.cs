using KickBlastEliteUI.Helpers;
using KickBlastEliteUI.Models;
using KickBlastEliteUI.Services;
using System.Windows;
using System.Windows.Input;

namespace KickBlastEliteUI.ViewModels;

public class SettingsViewModel : BasePageViewModel
{
    private readonly IPricingService _pricingService;
    private readonly IDataService _dataService;
    private readonly NotificationService _notification;

    private PricingSettings _pricing = new();

    public SettingsViewModel(IPricingService pricingService, IDataService dataService, NotificationService notification)
    {
        _pricingService = pricingService;
        _dataService = dataService;
        _notification = notification;

        SaveCommand = new RelayCommand(async () => await SaveAsync());
        ResetDatabaseCommand = new RelayCommand(async () => await ResetDatabaseAsync());
    }

    public decimal BeginnerWeeklyFee { get => _pricing.BeginnerWeeklyFee; set { _pricing.BeginnerWeeklyFee = value; OnPropertyChanged(); } }
    public decimal IntermediateWeeklyFee { get => _pricing.IntermediateWeeklyFee; set { _pricing.IntermediateWeeklyFee = value; OnPropertyChanged(); } }
    public decimal EliteWeeklyFee { get => _pricing.EliteWeeklyFee; set { _pricing.EliteWeeklyFee = value; OnPropertyChanged(); } }
    public decimal CompetitionFee { get => _pricing.CompetitionFee; set { _pricing.CompetitionFee = value; OnPropertyChanged(); } }
    public decimal CoachingHourlyRate { get => _pricing.CoachingHourlyRate; set { _pricing.CoachingHourlyRate = value; OnPropertyChanged(); } }

    public ICommand SaveCommand { get; }
    public ICommand ResetDatabaseCommand { get; }

    public override Task InitializeAsync()
    {
        _pricing = _pricingService.GetCurrentPricing();
        OnPropertyChanged(nameof(BeginnerWeeklyFee));
        OnPropertyChanged(nameof(IntermediateWeeklyFee));
        OnPropertyChanged(nameof(EliteWeeklyFee));
        OnPropertyChanged(nameof(CompetitionFee));
        OnPropertyChanged(nameof(CoachingHourlyRate));
        return Task.CompletedTask;
    }

    private async Task SaveAsync()
    {
        try
        {
            await _pricingService.SavePricingAsync(_pricing);
            _notification.ShowSuccess("Pricing saved to appsettings.json.");
        }
        catch (Exception ex)
        {
            _notification.ShowError($"Error saving pricing: {ex.Message}");
        }
    }

    private async Task ResetDatabaseAsync()
    {
        var result = MessageBox.Show("This will wipe all athletes and calculations. Continue?", "Reset Database", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (result != MessageBoxResult.Yes)
        {
            return;
        }

        try
        {
            await _dataService.ResetDatabaseAsync();
            _notification.ShowSuccess("Database reset complete.");
        }
        catch (Exception ex)
        {
            _notification.ShowError($"Database reset failed: {ex.Message}");
        }
    }
}
