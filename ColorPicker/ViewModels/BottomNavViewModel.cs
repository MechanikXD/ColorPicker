using System.Windows.Input;

namespace ColorPicker.ViewModels;

public class BottomNavViewModel : BaseViewModel
{
    public string ActiveRoute
    {
        get;
        set => SetField(ref field, value);
    } = "main";

    public ICommand NavigateCommand { get; }

    public BottomNavViewModel()
    {
        NavigateCommand = new Command<string>(pageRoute =>
        {
            ActiveRoute = pageRoute;
            Shell.Current.GoToAsync( $"//{pageRoute}");
        });
    }
}
