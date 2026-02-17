# KickBlast Judo – Training Fee Management System

A student-level WPF (.NET 8) MVVM app for managing athletes and monthly training fee calculations using SQLite + EF Core.

## Requirements
- Visual Studio 2022 (17.8+ recommended)
- .NET 8 SDK and .NET desktop workload

## Open in Visual Studio
1. Open `KickBlastStudentUI.sln`
2. Right-click `KickBlastStudentUI` and select **Set as Startup Project**
3. Confirm configuration is **Debug | Any CPU**

## Run
1. Press **F5** or **Ctrl+F5**
2. On first run, database is created at `KickBlastStudentUI/bin/Debug/net8.0-windows/Data/kickblast_student.db`
3. Seed data is inserted automatically (plans + 6 athletes)

## Change prices
1. Go to **Settings** screen
2. Edit fee values and click **Save Settings**
3. The app writes to `appsettings.json`; restart to refresh all dependent views cleanly

## Reset database
1. Close app
2. Delete `KickBlastStudentUI/bin/Debug/net8.0-windows/Data/kickblast_student.db`
3. Run app again to recreate and reseed database
