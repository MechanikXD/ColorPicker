using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class HistoryPage : AnimatedPage
{
    public HistoryPage(HistoryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}