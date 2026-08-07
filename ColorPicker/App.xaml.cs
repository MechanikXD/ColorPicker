using ColorPicker.Services.SaveLoad;
using ColorPicker.Services.Theme;

namespace ColorPicker;

public partial class App : Application
{
    public App(AppShell appShell, IThemeService themeService, ISaveLoadService paletteLoadService)
    {
        InitializeComponent();
        themeService.ApplySavedTheme();
        paletteLoadService.Load();
        MainPage = appShell;
    }
}