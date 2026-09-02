using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class MainPage : AnimatedPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}