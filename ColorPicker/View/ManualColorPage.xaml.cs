using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class ManualColorPage : AnimatedPage
{
    public ManualColorPage(ManualColorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}