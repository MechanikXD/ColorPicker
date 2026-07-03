namespace ColorPicker;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("manualcolor", typeof(View.ManualColorPage));
    }
}