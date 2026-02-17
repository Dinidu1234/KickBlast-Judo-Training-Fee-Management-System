using KickBlastEliteUI.Helpers;
using KickBlastEliteUI.Models;
using KickBlastEliteUI.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace KickBlastEliteUI.ViewModels;

public class AthletesViewModel : BasePageViewModel
{
    private readonly IDataService _dataService;
    private readonly NotificationService _notification;
    private string _filterPlan = "All";
    private Athlete? _selectedAthlete;

    public AthletesViewModel(IDataService dataService, NotificationService notification)
    {
        _dataService = dataService;
        _notification = notification;

        SaveCommand = new RelayCommand(async () => await SaveAsync(), () => Editor.IsValid);
        EditCommand = new RelayCommand<Athlete>(athlete => LoadEditor(athlete));
        DeleteCommand = new RelayCommand<Athlete>(async athlete => await DeleteAsync(athlete));
        NewCommand = new RelayCommand(() => Editor.Reset());
    }

    public ObservableCollection<Athlete> Athletes { get; } = [];
    public ObservableCollection<TrainingPlan> Plans { get; } = [];
    public AthleteEditorViewModel Editor { get; } = new();

    public ICommand SaveCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand NewCommand { get; }

    public Athlete? SelectedAthlete { get => _selectedAthlete; set => SetProperty(ref _selectedAthlete, value); }

    public string FilterPlan
    {
        get => _filterPlan;
        set
        {
            if (SetProperty(ref _filterPlan, value))
            {
                _ = InitializeAsync();
            }
        }
    }

    public override async Task InitializeAsync()
    {
        var plans = await _dataService.GetPlansAsync();
        Plans.Clear();
        foreach (var p in plans)
        {
            Plans.Add(p);
        }

        var athletes = await _dataService.GetAthletesAsync();
        var filtered = FilterPlan == "All" ? athletes : athletes.Where(x => x.TrainingPlan?.Name == FilterPlan).ToList();
        Athletes.Clear();
        foreach (var athlete in filtered)
        {
            Athletes.Add(athlete);
        }
    }

    private void LoadEditor(Athlete? athlete)
    {
        if (athlete == null) return;
        Editor.LoadFrom(athlete);
    }

    private async Task SaveAsync()
    {
        try
        {
            if (!Editor.IsValid)
            {
                _notification.ShowError("Please correct validation errors.");
                return;
            }

            await _dataService.SaveAthleteAsync(Editor.ToEntity());
            _notification.ShowSuccess("Athlete saved successfully.");
            await InitializeAsync();
            Editor.Reset();
        }
        catch (Exception ex)
        {
            _notification.ShowError($"Error saving athlete: {ex.Message}");
        }
    }

    private async Task DeleteAsync(Athlete? athlete)
    {
        if (athlete == null) return;

        var result = MessageBox.Show($"Delete athlete '{athlete.Name}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (result != MessageBoxResult.Yes) return;

        try
        {
            await _dataService.DeleteAthleteAsync(athlete);
            _notification.ShowSuccess("Athlete deleted.");
            await InitializeAsync();
        }
        catch (Exception ex)
        {
            _notification.ShowError($"Error deleting athlete: {ex.Message}");
        }
    }
}
