using KickBlastEliteUI.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace KickBlastEliteUI.Services;

public class NavigationService(IServiceProvider provider) : INavigationService
{
    private readonly IServiceProvider _provider = provider;

    public ObservableObject? CurrentViewModel { get; private set; }
    public event Action? CurrentViewModelChanged;

    public void NavigateTo<TViewModel>() where TViewModel : ObservableObject
    {
        CurrentViewModel = _provider.GetRequiredService<TViewModel>();
        CurrentViewModelChanged?.Invoke();
    }
}
