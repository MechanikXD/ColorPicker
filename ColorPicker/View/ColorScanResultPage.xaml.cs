using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class ColorScanResultPage : ContentPage
{
    public ColorScanResultPage(ColorScanResultViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}