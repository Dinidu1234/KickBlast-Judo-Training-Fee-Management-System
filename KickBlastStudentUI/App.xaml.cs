using System.IO;
using System.Windows;
using KickBlastStudentUI.Data;
using KickBlastStudentUI.Services;

namespace KickBlastStudentUI;

public partial class App : Application
{
    public static AppDbContext? DbContext { get; private set; }
    public static PricingService? PricingService { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        Directory.CreateDirectory(dataDirectory);

        PricingService = new PricingService(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"));
        DbContext = new AppDbContext(Path.Combine(dataDirectory, "kickblast_student.db"));
        DbInitializer.Initialize(DbContext, PricingService);
    }
}
