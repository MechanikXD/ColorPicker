using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class ManualColorPage : ContentPage
{
    public ManualColorPage(ManualColorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}