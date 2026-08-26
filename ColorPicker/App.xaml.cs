using ColorPicker.Models.StaticData;
using ColorPicker.Services.Navigation;
using ColorPicker.Services.SaveLoad;
using ColorPicker.Services.Theme;
using ColorPicker.View;

namespace ColorPicker;

public partial class App : Application
{
    public App(AppShell appShell, IThemeService themeService,
        [FromKeyedServices(KeyedServices.PALETTE_SAVE_LOAD)]
        ISaveLoadService paletteLoadService,
        [FromKeyedServices(KeyedServices.LOCALIZATION_SAVE_LOAD)]
        ISaveLoadService localizationLoadService)
    {
        InitializeComponent();
        themeService.ApplySavedTheme();
        paletteLoadService.Load();
        localizationLoadService.Load();
        MainPage = appShell;
    }

    protected override void OnStart()
    {
        base.OnStart();
        NavigationTracker.Initialize();
        PreheatPages();
    }

    private static void PreheatPages()
    {
        var s = IPlatformApplication.Current?.Services;
        if (s == null) return;

        _ = s.GetService<MainPage>();
        _ = s.GetService<PalettePage>();
        _ = s.GetService<CameraPage>();
        _ = s.GetService<HistoryPage>();

        _ = s.GetService<ManualColorPage>();
        _ = s.GetService<ColorScanResultPage>();
    }
}