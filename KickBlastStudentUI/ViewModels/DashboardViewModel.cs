using KickBlastStudentUI.Helpers;
using KickBlastStudentUI.Services;
using Microsoft.EntityFrameworkCore;

namespace KickBlastStudentUI.ViewModels;

public class DashboardViewModel : ObservableObject
{
    public DashboardViewModel(ToastService toastService)
    {
        try
        {
            var db = App.DbContext!;
            TotalAthletes = db.Athletes.Count();
            TotalCalculations = db.MonthlyCalculations.Count();
            TotalPlans = db.TrainingPlans.Count();
            LastUpdated = DateTime.Now;
        }
        catch
        {
            toastService.ShowError("Dashboard data could not be loaded.");
        }
    }

    public int TotalAthletes { get; set; }
    public int TotalCalculations { get; set; }
    public int TotalPlans { get; set; }
    public DateTime LastUpdated { get; set; }
}
