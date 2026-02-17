namespace KickBlastStudentUI.Models;

public class MonthlyCalculation
{
    public int Id { get; set; }
    public int AthleteId { get; set; }
    public Athlete? Athlete { get; set; }
    public DateTime CalculationDate { get; set; }
    public int CompetitionsThisMonth { get; set; }
    public decimal CoachingHoursPerWeek { get; set; }
    public decimal TrainingCost { get; set; }
    public decimal CoachingCost { get; set; }
    public decimal CompetitionCost { get; set; }
    public decimal TotalCost { get; set; }
    public string WeightStatus { get; set; } = string.Empty;
    public decimal WeightDifferenceKg { get; set; }
    public DateTime SecondSaturdayDate { get; set; }
}
