using KickBlastStudentUI.Models;
using Microsoft.EntityFrameworkCore;

namespace KickBlastStudentUI.Data;

public class AppDbContext : DbContext
{
    private readonly string _dbPath;

    public AppDbContext(string dbPath)
    {
        _dbPath = dbPath;
    }

    public DbSet<Athlete> Athletes => Set<Athlete>();
    public DbSet<TrainingPlan> TrainingPlans => Set<TrainingPlan>();
    public DbSet<MonthlyCalculation> MonthlyCalculations => Set<MonthlyCalculation>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrainingPlan>().HasIndex(p => p.Name).IsUnique();

        modelBuilder.Entity<Athlete>()
            .HasOne(a => a.TrainingPlan)
            .WithMany(p => p.Athletes)
            .HasForeignKey(a => a.TrainingPlanId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MonthlyCalculation>()
            .HasOne(c => c.Athlete)
            .WithMany(a => a.MonthlyCalculations)
            .HasForeignKey(c => c.AthleteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
