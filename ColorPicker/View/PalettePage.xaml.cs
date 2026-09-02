using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class PalettePage : AnimatedPage
{
    public PalettePage(PaletteViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}