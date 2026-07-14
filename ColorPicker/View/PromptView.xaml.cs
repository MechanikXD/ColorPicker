using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class PromptView : ContentView
{
    public PromptView(PromptViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}