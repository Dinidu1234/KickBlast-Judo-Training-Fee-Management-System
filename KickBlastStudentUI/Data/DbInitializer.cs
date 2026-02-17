using KickBlastStudentUI.Models;
using KickBlastStudentUI.Services;
using Microsoft.EntityFrameworkCore;

namespace KickBlastStudentUI.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context, PricingService pricingService)
    {
        context.Database.EnsureCreated();

        if (context.TrainingPlans.Any())
        {
            return;
        }

        var pricing = pricingService.Pricing;
        var plans = new List<TrainingPlan>
        {
            new() { Name = "Beginner", WeeklyFee = pricing.BeginnerWeeklyFee },
            new() { Name = "Intermediate", WeeklyFee = pricing.IntermediateWeeklyFee },
            new() { Name = "Elite", WeeklyFee = pricing.EliteWeeklyFee }
        };

        context.TrainingPlans.AddRange(plans);
        context.SaveChanges();

        var planMap = context.TrainingPlans.AsNoTracking().ToDictionary(x => x.Name, x => x.Id);
        context.Athletes.AddRange(
            new Athlete { Name = "Nuwan Silva", CurrentWeight = 64.5m, CategoryWeight = 66m, TrainingPlanId = planMap["Beginner"] },
            new Athlete { Name = "Sahan Perera", CurrentWeight = 70m, CategoryWeight = 73m, TrainingPlanId = planMap["Intermediate"] },
            new Athlete { Name = "Rashmi Fernando", CurrentWeight = 57m, CategoryWeight = 57m, TrainingPlanId = planMap["Elite"] },
            new Athlete { Name = "Dilan Jayawardena", CurrentWeight = 80m, CategoryWeight = 81m, TrainingPlanId = planMap["Intermediate"] },
            new Athlete { Name = "Kavindi Rajapaksha", CurrentWeight = 49m, CategoryWeight = 52m, TrainingPlanId = planMap["Beginner"] },
            new Athlete { Name = "Minura Wijesinghe", CurrentWeight = 89m, CategoryWeight = 90m, TrainingPlanId = planMap["Elite"] }
        );

        context.SaveChanges();
    }
}
