using ColorPicker.Services.Theme;

namespace ColorPicker;

public partial class App : Application
{
    public App(AppShell appShell, IThemeService themeService)
    {
        InitializeComponent();
        themeService.ApplySavedTheme();
        MainPage = appShell;
    }
}