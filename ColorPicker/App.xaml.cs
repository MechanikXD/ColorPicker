using ColorPicker.Services.Navigation;
using ColorPicker.Services.SaveLoad;
using ColorPicker.Services.Theme;
using ColorPicker.View;
using ColorPicker.ViewModels;

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
    
    protected override void OnStart()
    {
        base.OnStart();
        NavigationTracker.Initialize();
    }
}