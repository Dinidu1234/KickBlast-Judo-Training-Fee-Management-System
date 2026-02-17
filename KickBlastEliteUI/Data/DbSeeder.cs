using KickBlastEliteUI.Models;
using Microsoft.EntityFrameworkCore;

namespace KickBlastEliteUI.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.TrainingPlans.AnyAsync())
        {
            return;
        }

        var plans = new[]
        {
            new TrainingPlan { Name = "Beginner", WeeklyFee = 2500 },
            new TrainingPlan { Name = "Intermediate", WeeklyFee = 4000 },
            new TrainingPlan { Name = "Elite", WeeklyFee = 6000 }
        };

        await db.TrainingPlans.AddRangeAsync(plans);
        await db.SaveChangesAsync();

        var athletes = new[]
        {
            new Athlete { Name = "Nimal Perera", TrainingPlanId = plans[0].Id, CurrentWeight = 64.2m, CompetitionCategoryWeight = 66m },
            new Athlete { Name = "Kasun Silva", TrainingPlanId = plans[1].Id, CurrentWeight = 73.5m, CompetitionCategoryWeight = 73m },
            new Athlete { Name = "Ayesha Fernando", TrainingPlanId = plans[2].Id, CurrentWeight = 57.9m, CompetitionCategoryWeight = 57m },
            new Athlete { Name = "Ravindu Jayasuriya", TrainingPlanId = plans[1].Id, CurrentWeight = 80.1m, CompetitionCategoryWeight = 81m },
            new Athlete { Name = "Tharushi Senanayake", TrainingPlanId = plans[0].Id, CurrentWeight = 49.8m, CompetitionCategoryWeight = 52m },
            new Athlete { Name = "Dilshan Wickramasinghe", TrainingPlanId = plans[2].Id, CurrentWeight = 89.0m, CompetitionCategoryWeight = 90m }
        };

        await db.Athletes.AddRangeAsync(athletes);
        await db.SaveChangesAsync();
    }
}
