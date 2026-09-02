using System.Windows.Input;
using ColorPicker.Models.StaticData;
using ColorPicker.Resources.Strings;
using ColorPicker.Services.Localization;
using ColorPicker.Services.Navigation;
using CommunityToolkit.Mvvm.Messaging;

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
        
        WeakReferenceMessenger.Default.Register<LocalizationService.CultureChangedMessage>(this, (_, _) => RefreshLocalization());
    }

    public string LocalizedNavBarHome => AppResources.navbar_home;
    public string LocalizedNavBarPalettes => AppResources.navbar_palette;
    public string LocalizedNavBarHistory => AppResources.navbar_history;
    public string LocalizedNavBarSettings => AppResources.navbar_settings;
    
    private void RefreshLocalization()
    {
        OnPropertyChanged(nameof(LocalizedNavBarHome));
        OnPropertyChanged(nameof(LocalizedNavBarPalettes));
        OnPropertyChanged(nameof(LocalizedNavBarHistory));
        OnPropertyChanged(nameof(LocalizedNavBarSettings));
    }
}
