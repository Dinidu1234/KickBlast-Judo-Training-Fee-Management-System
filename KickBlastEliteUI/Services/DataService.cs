using KickBlastEliteUI.Data;
using KickBlastEliteUI.Models;
using Microsoft.EntityFrameworkCore;

namespace KickBlastEliteUI.Services;

public class DataService(AppDbContext db) : IDataService
{
    private readonly AppDbContext _db = db;

    public Task<List<TrainingPlan>> GetPlansAsync() => _db.TrainingPlans.OrderBy(x => x.Id).ToListAsync();

    public Task<List<Athlete>> GetAthletesAsync() => _db.Athletes.Include(x => x.TrainingPlan).OrderBy(x => x.Name).ToListAsync();

    public Task<List<MonthlyCalculation>> GetCalculationsAsync() => _db.MonthlyCalculations
        .Include(x => x.Athlete)
        .ThenInclude(x => x!.TrainingPlan)
        .OrderByDescending(x => x.CreatedAt)
        .ToListAsync();

    public async Task<Athlete> SaveAthleteAsync(Athlete athlete)
    {
        if (athlete.Id == 0)
        {
            await _db.Athletes.AddAsync(athlete);
        }
        else
        {
            _db.Athletes.Update(athlete);
        }

        await _db.SaveChangesAsync();
        return athlete;
    }

    public async Task DeleteAthleteAsync(Athlete athlete)
    {
        _db.Athletes.Remove(athlete);
        await _db.SaveChangesAsync();
    }

    public async Task<MonthlyCalculation> SaveCalculationAsync(MonthlyCalculation calc)
    {
        await _db.MonthlyCalculations.AddAsync(calc);
        await _db.SaveChangesAsync();
        return calc;
    }

    public async Task<decimal> GetRevenueForMonthAsync(int month, int year)
    {
        return await _db.MonthlyCalculations
            .Where(x => x.Month == month && x.Year == year)
            .SumAsync(x => (decimal?)x.TotalCost) ?? 0m;
    }

    public async Task ResetDatabaseAsync()
    {
        await _db.Database.EnsureDeletedAsync();
        await _db.Database.EnsureCreatedAsync();
        await DbSeeder.SeedAsync(_db);
    }
}
