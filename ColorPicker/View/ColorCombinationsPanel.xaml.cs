using System.ComponentModel;
using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class ColorCombinationsPanel : ContentView
{
    public ColorCombinationsPanel()
    {
        InitializeComponent();
        BindingContext = IPlatformApplication.Current?.Services.GetRequiredService<ColorCombinationsPanelViewModel>();
    }
    
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (BindingContext is ColorCombinationsPanelViewModel vm)
        {
            vm.PropertyChanged += OnViewModelPropertyChanged;
            // Initial state
            ContentArea.IsVisible = vm.IsExpanded;
            ContentArea.Opacity = vm.IsExpanded ? 1.0 : 0.0;
        }
    }

    
    private async void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ColorCombinationsPanelViewModel.IsExpanded))
        {
            var vm = (ColorCombinationsPanelViewModel)BindingContext;
            await AnimatePanel(vm.IsExpanded);
        }
    }
    
    private async Task AnimatePanel(bool expanding)
    {
        if (expanding)
        {
            ContentArea.IsVisible = true;
            await ContentArea.FadeToAsync(1, 220, Easing.CubicOut);
        }
        else
        {
            await ContentArea.FadeToAsync(0, 180, Easing.CubicIn);
            ContentArea.IsVisible = false;
        }
    }
}