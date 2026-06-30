using ColorPicker.Models;
using MainViewModel = ColorPicker.ViewModels.MainViewModel;

namespace ColorPicker.View;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}