using KickBlastStudentUI.Helpers;
using KickBlastStudentUI.Models;

namespace KickBlastStudentUI.Services;

public class FeeCalculatorService
{
    private readonly PricingService _pricingService;

    public FeeCalculatorService(PricingService pricingService)
    {
        _pricingService = pricingService;
    }

    public FeeCalculationResult Calculate(Athlete athlete, int competitionsThisMonth, decimal coachingHoursPerWeek)
    {
        var pricing = _pricingService.Pricing;

        var isBeginner = athlete.TrainingPlan?.Name == "Beginner";
        var competitionCount = isBeginner ? 0 : competitionsThisMonth;

        var trainingCost = (athlete.TrainingPlan?.WeeklyFee ?? 0) * 4;
        var coachingCost = coachingHoursPerWeek * 4 * pricing.CoachingHourlyRate;
        var competitionCost = competitionCount * pricing.CompetitionFee;
        var total = trainingCost + coachingCost + competitionCost;

        var diff = athlete.CurrentWeight - athlete.CategoryWeight;
        var status = Math.Abs(diff) < 0.1m ? "On target" : diff > 0 ? "Over target" : "Under target";

        return new FeeCalculationResult
        {
            CompetitionsApplied = competitionCount,
            TrainingCost = trainingCost,
            CoachingCost = coachingCost,
            CompetitionCost = competitionCost,
            Total = total,
            WeightDifferenceKg = diff,
            WeightStatus = status,
            SecondSaturday = DateHelper.GetSecondSaturday(DateTime.Today)
        };
    }
}

public class FeeCalculationResult
{
    public int CompetitionsApplied { get; set; }
    public decimal TrainingCost { get; set; }
    public decimal CoachingCost { get; set; }
    public decimal CompetitionCost { get; set; }
    public decimal Total { get; set; }
    public decimal WeightDifferenceKg { get; set; }
    public string WeightStatus { get; set; } = string.Empty;
    public DateTime SecondSaturday { get; set; }
}
