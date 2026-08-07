namespace ColorPicker.Services.Navigation;

public static class ShellNavigationService
{
    public static void GoToPage(string pageRoute) => Shell.Current.GoToAsync( $"//{pageRoute}");
    
    public static void DoToSubPage(string pageRoute, Dictionary<string, object>? routeParams=null) => 
        Shell.Current.GoToAsync(pageRoute, routeParams);

    public static void GoBack() => Shell.Current.GoToAsync("..");
    
    public static async Task GoBackAsync() => await Shell.Current.GoToAsync("..");
    
    public static async Task GoToPageAsync(string pageRoute) => await Shell.Current.GoToAsync( $"//{pageRoute}");

    public static async Task GoToSubPageAsync(string pageRoute, Dictionary<string, object>? routeParams=null) =>
        await Shell.Current.GoToAsync(pageRoute, routeParams);
}