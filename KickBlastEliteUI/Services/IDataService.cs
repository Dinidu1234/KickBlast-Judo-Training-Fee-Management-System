using KickBlastEliteUI.Models;

namespace KickBlastEliteUI.Services;

public interface IDataService
{
    Task<List<TrainingPlan>> GetPlansAsync();
    Task<List<Athlete>> GetAthletesAsync();
    Task<List<MonthlyCalculation>> GetCalculationsAsync();
    Task<Athlete> SaveAthleteAsync(Athlete athlete);
    Task DeleteAthleteAsync(Athlete athlete);
    Task<MonthlyCalculation> SaveCalculationAsync(MonthlyCalculation calc);
    Task<decimal> GetRevenueForMonthAsync(int month, int year);
    Task ResetDatabaseAsync();
}
