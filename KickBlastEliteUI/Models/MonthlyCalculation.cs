namespace KickBlastEliteUI.Models;

public class MonthlyCalculation
{
    public int Id { get; set; }
    public int AthleteId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal TrainingCost { get; set; }
    public decimal CoachingCost { get; set; }
    public decimal CompetitionCost { get; set; }
    public decimal TotalCost { get; set; }
    public int CompetitionsCount { get; set; }
    public decimal CoachingHours { get; set; }
    public DateTime CreatedAt { get; set; }

    public Athlete? Athlete { get; set; }
}
