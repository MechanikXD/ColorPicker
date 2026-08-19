using ColorPicker.Models.StaticData;

namespace ColorPicker;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(Pages.Sub.ManualColorSelection, typeof(View.ManualColorPage));
        Routing.RegisterRoute(Pages.Sub.ColorScanResult, typeof(View.ColorScanResultPage));
    }
}