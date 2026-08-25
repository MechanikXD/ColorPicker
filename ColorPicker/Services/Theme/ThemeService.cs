using ColorPicker.Models.StaticData;

namespace ColorPicker.Services.Theme;

public class ThemeService : IThemeService
{
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
        
        Preferences.Default.Set(UserStorageKeys.SELECTED_THEME_STORAGE_KEY, newTheme.ToString());
    }

    public void ApplySavedTheme()
    {
        var saved = Preferences.Default.Get(UserStorageKeys.SELECTED_THEME_STORAGE_KEY, nameof(ApplicationTheme.System));
        var theme = Enum.TryParse<ApplicationTheme>(saved, out var parsed) ? parsed : ApplicationTheme.System;
        SetTheme(theme);
    }
}