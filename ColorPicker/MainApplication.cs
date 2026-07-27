using Android.App;
using Android.Runtime;

namespace ColorPicker;

[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership) : base(handle, ownership) { }

    // This bootstraps your MauiProgram, which instantiates your App and AppShell
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}