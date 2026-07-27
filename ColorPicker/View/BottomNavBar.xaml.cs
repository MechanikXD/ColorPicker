using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class BottomNavBar : ContentView
{
    public BottomNavBar()
    {
        InitializeComponent();
        BindingContext = IPlatformApplication.Current?.Services.GetRequiredService<BottomNavViewModel>();
    }
}