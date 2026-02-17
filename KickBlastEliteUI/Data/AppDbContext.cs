using KickBlastEliteUI.Models;
using Microsoft.EntityFrameworkCore;

namespace KickBlastEliteUI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TrainingPlan> TrainingPlans => Set<TrainingPlan>();
    public DbSet<Athlete> Athletes => Set<Athlete>();
    public DbSet<MonthlyCalculation> MonthlyCalculations => Set<MonthlyCalculation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrainingPlan>()
            .HasMany(x => x.Athletes)
            .WithOne(x => x.TrainingPlan)
            .HasForeignKey(x => x.TrainingPlanId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Athlete>()
            .HasMany(x => x.MonthlyCalculations)
            .WithOne(x => x.Athlete)
            .HasForeignKey(x => x.AthleteId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TrainingPlan>()
            .Property(x => x.WeeklyFee)
            .HasPrecision(12, 2);

        modelBuilder.Entity<Athlete>().Property(x => x.CurrentWeight).HasPrecision(8, 2);
        modelBuilder.Entity<Athlete>().Property(x => x.CompetitionCategoryWeight).HasPrecision(8, 2);

        modelBuilder.Entity<MonthlyCalculation>().Property(x => x.TrainingCost).HasPrecision(12, 2);
        modelBuilder.Entity<MonthlyCalculation>().Property(x => x.CoachingCost).HasPrecision(12, 2);
        modelBuilder.Entity<MonthlyCalculation>().Property(x => x.CompetitionCost).HasPrecision(12, 2);
        modelBuilder.Entity<MonthlyCalculation>().Property(x => x.TotalCost).HasPrecision(12, 2);
        modelBuilder.Entity<MonthlyCalculation>().Property(x => x.CoachingHours).HasPrecision(4, 2);
    }
}
