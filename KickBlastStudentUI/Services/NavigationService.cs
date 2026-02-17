using KickBlastStudentUI.Helpers;

namespace KickBlastStudentUI.Services;

public class NavigationService : ObservableObject
{
    private object? _currentViewModel;

    public object? CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }
}
