using System.Collections.ObjectModel;
using System.Windows;
using KickBlastStudentUI.Helpers;
using KickBlastStudentUI.Models;
using KickBlastStudentUI.Services;
using Microsoft.EntityFrameworkCore;

namespace KickBlastStudentUI.ViewModels;

public class AthletesViewModel : ObservableObject
{
    private readonly ToastService _toastService;
    private Athlete? _selectedAthlete;
    private string _name = string.Empty;
    private decimal _currentWeight;
    private decimal _categoryWeight;
    private TrainingPlan? _selectedPlan;
    private string _searchText = string.Empty;
    private int _selectedPlanFilter;
    private string _nameError = string.Empty;
    private string _weightError = string.Empty;

    public AthletesViewModel(ToastService toastService)
    {
        _toastService = toastService;
        Athletes = new ObservableCollection<Athlete>();
        Plans = new ObservableCollection<TrainingPlan>();

        SaveAthleteCommand = new RelayCommand(_ => SaveAthlete());
        DeleteAthleteCommand = new RelayCommand(_ => DeleteAthlete(), _ => SelectedAthlete != null);
        ClearFormCommand = new RelayCommand(_ => ClearForm());
        SearchCommand = new RelayCommand(_ => LoadAthletes());

        LoadPlans();
        LoadAthletes();
    }

    public ObservableCollection<Athlete> Athletes { get; }
    public ObservableCollection<TrainingPlan> Plans { get; }

    public Athlete? SelectedAthlete
    {
        get => _selectedAthlete;
        set
        {
            SetProperty(ref _selectedAthlete, value);
            if (value != null)
            {
                Name = value.Name;
                CurrentWeight = value.CurrentWeight;
                CategoryWeight = value.CategoryWeight;
                SelectedPlan = Plans.FirstOrDefault(p => p.Id == value.TrainingPlanId);
            }
        }
    }

    public string Name { get => _name; set => SetProperty(ref _name, value); }
    public decimal CurrentWeight { get => _currentWeight; set => SetProperty(ref _currentWeight, value); }
    public decimal CategoryWeight { get => _categoryWeight; set => SetProperty(ref _categoryWeight, value); }
    public TrainingPlan? SelectedPlan { get => _selectedPlan; set => SetProperty(ref _selectedPlan, value); }
    public string SearchText { get => _searchText; set => SetProperty(ref _searchText, value); }
    public int SelectedPlanFilter { get => _selectedPlanFilter; set => SetProperty(ref _selectedPlanFilter, value); }
    public string NameError { get => _nameError; set => SetProperty(ref _nameError, value); }
    public string WeightError { get => _weightError; set => SetProperty(ref _weightError, value); }

    public RelayCommand SaveAthleteCommand { get; }
    public RelayCommand DeleteAthleteCommand { get; }
    public RelayCommand ClearFormCommand { get; }
    public RelayCommand SearchCommand { get; }

    private void LoadPlans()
    {
        Plans.Clear();
        foreach (var p in App.DbContext!.TrainingPlans.AsNoTracking().OrderBy(p => p.Name))
        {
            Plans.Add(p);
        }
    }

    private void LoadAthletes()
    {
        try
        {
            var query = App.DbContext!.Athletes.Include(a => a.TrainingPlan).AsQueryable();
            if (!string.IsNullOrWhiteSpace(SearchText))
                query = query.Where(a => a.Name.Contains(SearchText));
            if (SelectedPlanFilter > 0)
                query = query.Where(a => a.TrainingPlanId == SelectedPlanFilter);

            Athletes.Clear();
            foreach (var a in query.OrderBy(a => a.Name).ToList())
                Athletes.Add(a);
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Could not load athletes: {ex.Message}");
        }
    }

    private bool ValidateForm()
    {
        NameError = Validators.ValidateRequired(Name, "Name");
        WeightError = string.Empty;

        if (CurrentWeight <= 0 || CategoryWeight <= 0)
            WeightError = "Weights must be greater than 0.";

        return string.IsNullOrWhiteSpace(NameError)
            && string.IsNullOrWhiteSpace(WeightError)
            && SelectedPlan != null;
    }

    private void SaveAthlete()
    {
        if (!ValidateForm())
        {
            _toastService.ShowError("Please fix validation errors.");
            return;
        }

        try
        {
            if (SelectedAthlete == null)
            {
                App.DbContext!.Athletes.Add(new Athlete
                {
                    Name = Name.Trim(),
                    CurrentWeight = CurrentWeight,
                    CategoryWeight = CategoryWeight,
                    TrainingPlanId = SelectedPlan!.Id
                });
            }
            else
            {
                var athlete = App.DbContext!.Athletes.Find(SelectedAthlete.Id);
                if (athlete != null)
                {
                    athlete.Name = Name.Trim();
                    athlete.CurrentWeight = CurrentWeight;
                    athlete.CategoryWeight = CategoryWeight;
                    athlete.TrainingPlanId = SelectedPlan!.Id;
                }
            }

            App.DbContext!.SaveChanges();
            LoadAthletes();
            ClearForm();
            _toastService.ShowSuccess("Athlete saved successfully.");
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Save failed: {ex.Message}");
        }
    }

    private void DeleteAthlete()
    {
        if (SelectedAthlete == null)
            return;

        if (MessageBox.Show($"Delete {SelectedAthlete.Name}?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
            return;

        try
        {
            var athlete = App.DbContext!.Athletes.Find(SelectedAthlete.Id);
            if (athlete != null)
            {
                App.DbContext!.Athletes.Remove(athlete);
                App.DbContext!.SaveChanges();
                LoadAthletes();
                ClearForm();
                _toastService.ShowSuccess("Athlete deleted.");
            }
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Delete failed: {ex.Message}");
        }
    }

    private void ClearForm()
    {
        SelectedAthlete = null;
        Name = string.Empty;
        CurrentWeight = 0;
        CategoryWeight = 0;
        SelectedPlan = null;
        NameError = string.Empty;
        WeightError = string.Empty;
    }
}
