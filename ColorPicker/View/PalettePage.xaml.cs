using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class PalettePage : ContentPage
{
    public PalettePage(PaletteViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}