using System.Windows.Input;

namespace ColorPicker.ViewModels;

public class BottomNavViewModel : BaseViewModel
{
    private string _activeRoute = "main";

    public string ActiveRoute
    {
        get => _activeRoute;
        set => SetField(ref _activeRoute, value);
    }

    /// <summary>
    /// Called by each nav button. If current page is a non-tab modal
    /// (e.g. ManualColorPage), navigating here acts as a cancel/pop first.
    /// </summary>
    public ICommand NavigateCommand { get; }

    public BottomNavViewModel()
    {
        NavigateCommand = new Command<string>(_ => { /* TODO: wire Shell.Current.GoToAsync */ });
    }
}
