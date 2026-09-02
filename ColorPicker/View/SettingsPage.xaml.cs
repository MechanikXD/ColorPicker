using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class SettingsPage : AnimatedPage
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    
    protected override bool OnBackButtonPressed() => 
        ((SettingsViewModel)BindingContext).TryGoBack() || base.OnBackButtonPressed();
}