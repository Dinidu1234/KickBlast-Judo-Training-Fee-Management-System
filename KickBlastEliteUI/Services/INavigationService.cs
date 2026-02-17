using KickBlastEliteUI.Helpers;

namespace KickBlastEliteUI.Services;

public interface INavigationService
{
    ObservableObject? CurrentViewModel { get; }
    event Action? CurrentViewModelChanged;
    void NavigateTo<TViewModel>() where TViewModel : ObservableObject;
}
