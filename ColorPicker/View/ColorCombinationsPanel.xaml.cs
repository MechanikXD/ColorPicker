using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class ColorCombinationsPanel : ContentView
{
    public ColorCombinationsPanel()
    {
        InitializeComponent();
        BindingContext = IPlatformApplication.Current?.Services.GetRequiredService<ColorCombinationsPanelViewModel>();
    }
}