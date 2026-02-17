using KickBlastEliteUI.Helpers;
using KickBlastEliteUI.Models;
using System.ComponentModel;

namespace KickBlastEliteUI.ViewModels;

public class AthleteEditorViewModel : ObservableObject, IDataErrorInfo
{
    private int _id;
    private string _name = string.Empty;
    private int _trainingPlanId;
    private decimal _currentWeight;
    private decimal _competitionCategoryWeight;

    public int Id { get => _id; set => SetProperty(ref _id, value); }
    public string Name { get => _name; set => SetProperty(ref _name, value); }
    public int TrainingPlanId { get => _trainingPlanId; set => SetProperty(ref _trainingPlanId, value); }
    public decimal CurrentWeight { get => _currentWeight; set => SetProperty(ref _currentWeight, value); }
    public decimal CompetitionCategoryWeight { get => _competitionCategoryWeight; set => SetProperty(ref _competitionCategoryWeight, value); }

    public string Error => string.Empty;

    public string this[string columnName]
    {
        get
        {
            return columnName switch
            {
                nameof(Name) when string.IsNullOrWhiteSpace(Name) => "Athlete name is required.",
                nameof(TrainingPlanId) when TrainingPlanId <= 0 => "Training plan is required.",
                nameof(CurrentWeight) when CurrentWeight <= 0 => "Current weight must be greater than 0.",
                nameof(CompetitionCategoryWeight) when CompetitionCategoryWeight <= 0 => "Category weight must be greater than 0.",
                _ => string.Empty
            };
        }
    }

    public bool IsValid =>
        string.IsNullOrWhiteSpace(this[nameof(Name)]) &&
        string.IsNullOrWhiteSpace(this[nameof(TrainingPlanId)]) &&
        string.IsNullOrWhiteSpace(this[nameof(CurrentWeight)]) &&
        string.IsNullOrWhiteSpace(this[nameof(CompetitionCategoryWeight)]);

    public Athlete ToEntity() => new()
    {
        Id = Id,
        Name = Name.Trim(),
        TrainingPlanId = TrainingPlanId,
        CurrentWeight = CurrentWeight,
        CompetitionCategoryWeight = CompetitionCategoryWeight
    };

    public void LoadFrom(Athlete athlete)
    {
        Id = athlete.Id;
        Name = athlete.Name;
        TrainingPlanId = athlete.TrainingPlanId;
        CurrentWeight = athlete.CurrentWeight;
        CompetitionCategoryWeight = athlete.CompetitionCategoryWeight;
    }

    public void Reset()
    {
        Id = 0;
        Name = string.Empty;
        TrainingPlanId = 0;
        CurrentWeight = 0;
        CompetitionCategoryWeight = 0;
    }
}
