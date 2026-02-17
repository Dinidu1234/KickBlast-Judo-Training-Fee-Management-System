using KickBlastEliteUI.Helpers;

namespace KickBlastEliteUI.Services;

public class NotificationService : ObservableObject
{
    private string _message = string.Empty;
    private bool _isError;

    public string Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }

    public bool IsError
    {
        get => _isError;
        set => SetProperty(ref _isError, value);
    }

    public void ShowSuccess(string message)
    {
        IsError = false;
        Message = message;
    }

    public void ShowError(string message)
    {
        IsError = true;
        Message = message;
    }
}
