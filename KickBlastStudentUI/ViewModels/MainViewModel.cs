using KickBlastStudentUI.Helpers;
using KickBlastStudentUI.Services;

namespace KickBlastStudentUI.ViewModels;

public class MainViewModel : ObservableObject
{
    private readonly NavigationService _navigationService;
    private readonly ToastService _toastService;

    public MainViewModel()
    {
        _navigationService = new NavigationService();
        _toastService = new ToastService();
        _toastService.PropertyChanged += (_, __) => OnPropertyChanged(nameof(ToastMessage));

        DashboardViewModel = new DashboardViewModel(_toastService);
        AthletesViewModel = new AthletesViewModel(_toastService);
        CalculatorViewModel = new CalculatorViewModel(_toastService);
        HistoryViewModel = new HistoryViewModel(_toastService);
        SettingsViewModel = new SettingsViewModel(_toastService);

        NavigateDashboardCommand = new RelayCommand(_ => Navigate(DashboardViewModel, "Dashboard"));
        NavigateAthletesCommand = new RelayCommand(_ => Navigate(AthletesViewModel, "Athletes"));
        NavigateCalculatorCommand = new RelayCommand(_ => Navigate(CalculatorViewModel, "Monthly Fee Calculator"));
        NavigateHistoryCommand = new RelayCommand(_ => Navigate(HistoryViewModel, "History"));
        NavigateSettingsCommand = new RelayCommand(_ => Navigate(SettingsViewModel, "Settings"));

        Navigate(DashboardViewModel, "Dashboard");
    }

    public DashboardViewModel DashboardViewModel { get; }
    public AthletesViewModel AthletesViewModel { get; }
    public CalculatorViewModel CalculatorViewModel { get; }
    public HistoryViewModel HistoryViewModel { get; }
    public SettingsViewModel SettingsViewModel { get; }

    public RelayCommand NavigateDashboardCommand { get; }
    public RelayCommand NavigateAthletesCommand { get; }
    public RelayCommand NavigateCalculatorCommand { get; }
    public RelayCommand NavigateHistoryCommand { get; }
    public RelayCommand NavigateSettingsCommand { get; }

    public object? CurrentViewModel => _navigationService.CurrentViewModel;
    public string HeaderText { get; private set; } = "Dashboard";
    public string ToastMessage => _toastService.Message;

    private void Navigate(object viewModel, string header)
    {
        _navigationService.CurrentViewModel = viewModel;
        HeaderText = header;
        OnPropertyChanged(nameof(CurrentViewModel));
        OnPropertyChanged(nameof(HeaderText));
        OnPropertyChanged(nameof(ToastMessage));
    }
}
