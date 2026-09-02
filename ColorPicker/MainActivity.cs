using Android.App;
using Android.Content.PM;
using Android.OS;

namespace ColorPicker;

[Activity(
    Theme = "@style/Maui.MainTheme.NoActionBar", 
    MainLauncher = true, 
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        // Do not call SetContentView() here. 
        // MauiAppCompatActivity automatically grabs AppShell from the DI container and renders it.
    }
}