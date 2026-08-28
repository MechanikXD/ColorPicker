using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class ColorScanResultPage : AnimatedPage
{
    public ColorScanResultPage(ColorScanResultViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        if (BindingContext is ColorScanResultViewModel vm) vm.SaveScannedColorToHistory();
        base.OnNavigatedFrom(args);
    }
}