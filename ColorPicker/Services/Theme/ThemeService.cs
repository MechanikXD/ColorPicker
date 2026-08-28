using ColorPicker.Models.Settings;

namespace ColorPicker.Services.Theme;

public class ThemeService : IThemeService
{
    public ApplicationTheme CurrentTheme { get; private set; } = ApplicationTheme.System;

    public ThemeService()
    {
        SettingsModels.ApplicationTheme.OnSettingChanged += UpdateApplicationTheme;
    }

    private void UpdateApplicationTheme(object newIndex)
    {
        CurrentTheme = Enum.TryParse<ApplicationTheme>(SettingsModels.ApplicationTheme.GetCurrentOption(), out var theme) 
            ? theme : ApplicationTheme.System;
    }
    
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
    }
}