using ColorPicker.Models.Settings;

namespace ColorPicker.Services.Theme;

public static class ThemeService
{
    public static ApplicationTheme CurrentTheme { get; private set; } = ApplicationTheme.System;

    public static void Initialize()
    {
        SettingsModels.ApplicationTheme.OnSettingChanged += UpdateApplicationTheme;
    }

    private static void UpdateApplicationTheme(object newIndex)
    {
        CurrentTheme = newIndex is int index ? (ApplicationTheme)index : ApplicationTheme.System;
        SetTheme(CurrentTheme);
    }
    
    public static void SetTheme(ApplicationTheme newTheme)
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
    }
}