using KickBlastEliteUI.Helpers;
using KickBlastEliteUI.Services;
using System.Windows.Input;

namespace KickBlastEliteUI.ViewModels;

public class MainWindowViewModel : ObservableObject
{
    private readonly INavigationService _navigation;
    private readonly NotificationService _notification;
    private ObservableObject? _currentViewModel;

    public MainWindowViewModel(INavigationService navigation, NotificationService notification)
    {
        _navigation = navigation;
        _notification = notification;
        _navigation.CurrentViewModelChanged += OnCurrentViewModelChanged;

        NavigateDashboardCommand = new RelayCommand(() => NavigateTo<DashboardViewModel>());
        NavigateAthletesCommand = new RelayCommand(() => NavigateTo<AthletesViewModel>());
        NavigateCalculatorCommand = new RelayCommand(() => NavigateTo<CalculatorViewModel>());
        NavigateHistoryCommand = new RelayCommand(() => NavigateTo<HistoryViewModel>());
        NavigateSettingsCommand = new RelayCommand(() => NavigateTo<SettingsViewModel>());

        NavigateTo<DashboardViewModel>();
    }

    public ICommand NavigateDashboardCommand { get; }
    public ICommand NavigateAthletesCommand { get; }
    public ICommand NavigateCalculatorCommand { get; }
    public ICommand NavigateHistoryCommand { get; }
    public ICommand NavigateSettingsCommand { get; }

    public ObservableObject? CurrentViewModel
    {
        get => _currentViewModel;
        private set => SetProperty(ref _currentViewModel, value);
    }

    public NotificationService Notification => _notification;

    public DateTime CurrentDate => DateTime.Now;

    private void NavigateTo<TViewModel>() where TViewModel : ObservableObject
    {
        _navigation.NavigateTo<TViewModel>();
        if (_navigation.CurrentViewModel is BasePageViewModel page)
        {
            _ = page.InitializeAsync();
        }
    }

    private void OnCurrentViewModelChanged() => CurrentViewModel = _navigation.CurrentViewModel;
}
