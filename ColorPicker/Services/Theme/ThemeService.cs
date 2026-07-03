namespace ColorPicker.Services.Theme;

public class ThemeService : IThemeService
{
    private const string PREF_KEY = "selected_app_theme";
    public ApplicationTheme CurrentTheme { get; private set; } = ApplicationTheme.System;
    
    public void SetTheme(ApplicationTheme newTheme)
    {
        CurrentTheme = newTheme;

        if (Application.Current is not null)
        {
            Application.Current.UserAppTheme = newTheme switch
            {
                ApplicationTheme.Light => AppTheme.Light,
                ApplicationTheme.Dark => AppTheme.Dark,
                _ => AppTheme.Unspecified
            };
        }
        
        Preferences.Default.Set(PREF_KEY, newTheme.ToString());
    }

    public void ApplySavedTheme()
    {
        var saved = Preferences.Default.Get(PREF_KEY, nameof(ApplicationTheme.System));
        var theme = Enum.TryParse<ApplicationTheme>(saved, out var parsed) ? parsed : ApplicationTheme.System;
        SetTheme(theme);
    }
}