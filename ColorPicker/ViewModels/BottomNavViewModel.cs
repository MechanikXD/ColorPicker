using System.Windows.Input;
using ColorPicker.Models.StaticData;
using ColorPicker.Services.Navigation;

namespace ColorPicker.ViewModels;

public class BottomNavViewModel : BaseViewModel
{
    public string ActiveRoute
    {
        get;
        set => SetField(ref field, value);
    } = Pages.Main.Dashboard;

    public ICommand NavigateCommand { get; }

    public BottomNavViewModel()
    {
        NavigateCommand = new Command<string>(async void (pageRoute) =>
        {
            ActiveRoute = pageRoute;
            await ShellNavigationService.GoToPageAsync(pageRoute);
        });
    }
}
