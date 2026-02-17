using KickBlastEliteUI.Helpers;

namespace KickBlastEliteUI.ViewModels;

public abstract class BasePageViewModel : ObservableObject
{
    public virtual Task InitializeAsync() => Task.CompletedTask;
}
