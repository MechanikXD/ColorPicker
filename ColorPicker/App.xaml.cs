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
        
        Task.Run(() =>
        {
            // Force DI container to construct heavy ViewModels/Services beforehand
            _ = Handler?.MauiContext?.Services.GetService<MainPage>();
            _ = Handler?.MauiContext?.Services.GetService<MainViewModel>();
            
            _ = Handler?.MauiContext?.Services.GetService<PalettePage>();
            _ = Handler?.MauiContext?.Services.GetService<PaletteViewModel>();
            
            _ = Handler?.MauiContext?.Services.GetService<CameraPage>();
            _ = Handler?.MauiContext?.Services.GetService<CameraViewModel>();
        });
        
        NavigationTracker.Initialize();
    }
}