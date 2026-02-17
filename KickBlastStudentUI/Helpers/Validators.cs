namespace KickBlastStudentUI.Helpers;

public static class Validators
{
    public static string ValidateRequired(string? value, string fieldName)
        => string.IsNullOrWhiteSpace(value) ? $"{fieldName} is required." : string.Empty;

    public static string ValidateRange(decimal value, decimal min, decimal max, string field)
        => value < min || value > max ? $"{field} must be between {min} and {max}." : string.Empty;
}
