using CommunityToolkit.Mvvm.ComponentModel;

namespace ColorPicker.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string _welcomeMessage = "Welcome to the Dashboard!";
}