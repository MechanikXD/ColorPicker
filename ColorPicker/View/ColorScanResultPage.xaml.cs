using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class ColorScanResultPage : AnimatedPage
{
    public ColorScanResultPage(ColorScanResultViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}