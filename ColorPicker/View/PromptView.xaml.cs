using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class PromptView : ContentView
{
    public PromptView()
    {
        InitializeComponent();
        BindingContext = IPlatformApplication.Current?.Services.GetRequiredService<PromptViewModel>();
    }
}