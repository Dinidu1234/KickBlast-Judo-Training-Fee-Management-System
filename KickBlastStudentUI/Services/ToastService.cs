using KickBlastStudentUI.Helpers;

namespace KickBlastStudentUI.Services;

public class ToastService : ObservableObject
{
    private string _message = "Ready";

    public string Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }

    public void ShowSuccess(string message) => Message = $"✅ {message}";
    public void ShowError(string message) => Message = $"⚠️ {message}";
}
