using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class ColorCombinationsPanel : ContentView
{
    public ColorCombinationsPanel(ColorCombinationsPanelViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}