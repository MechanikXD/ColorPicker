namespace ColorPicker;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("manual_color", typeof(View.ManualColorPage));
        Routing.RegisterRoute("scan_result",  typeof(View.ColorScanResultPage));
    }
}