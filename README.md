# KickBlast Judo – Training Fee Management System

A complete .NET 8 WPF desktop application using MVVM, SQLite, Entity Framework Core, and configuration-driven pricing.

## Requirements
- Visual Studio 2022 (17.8+ recommended)
- .NET 8 SDK with Windows Desktop workload
- Windows 10/11

## Open in Visual Studio
1. Open `KickBlastEliteUI.sln`.
2. Restore NuGet packages.
3. Build solution (Debug|Any CPU).

## Run
1. Set `KickBlastEliteUI` as startup project.
2. Press `F5`.
3. On first run, app creates `Data/kickblastelite.db` and seeds plans + sample athletes.

## Change prices
1. Open **Settings** page.
2. Edit fee fields.
3. Click **Save Pricing**.
4. This updates `appsettings.json` used by pricing service.

## Reset database
1. Open **Settings** page.
2. Click **Reset Database**.
3. Confirm warning dialog.
4. DB is re-created and re-seeded.

## Architecture overview
- **UI:** WPF views with modern card-based layout and sidebar navigation.
- **MVVM:**
  - `Views/` for XAML.
  - `ViewModels/` for page and shell logic.
  - `Helpers/` for observable and command utilities.
- **Services:**
  - `NavigationService` for content navigation.
  - `DataService` for async CRUD + calculations persistence.
  - `PricingService` for config-backed pricing.
  - `NotificationService` for snackbar-like status messages.
- **Data:**
  - `AppDbContext` (EF Core SQLite).
  - `DbSeeder` for first-run seed data.
- **Configuration:**
  - `appsettings.json` with pricing values.
