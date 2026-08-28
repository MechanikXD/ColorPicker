using ColorPicker.Services.Navigation;
using ColorPicker.Services.SaveLoad;
using ColorPicker.View;

namespace ColorPicker;

public partial class App : Application
{
    public App(AppShell appShell, IEnumerable<ISaveLoadService> saveLoadServices)
    {
        InitializeComponent();
        foreach (var service in saveLoadServices) service.Load();
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
        _ = s.GetService<SettingsPage>();

        _ = s.GetService<ManualColorPage>();
        _ = s.GetService<ColorScanResultPage>();
    }
}