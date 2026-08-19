namespace ColorPicker.Services.Navigation;

public static class ShellNavigationService
{
    public static async Task GoToPageAsync(string pageRoute) => await Shell.Current.GoToAsync( $"//{pageRoute}", animate:false);
    
    public static async Task GoToSubPageAsync(string pageRoute, Dictionary<string, object>? routeParams=null) =>
        await Shell.Current.GoToAsync(pageRoute, animate:false, routeParams);

    public static async Task GoBackAsync()
    {
        var currentPage = Shell.Current.CurrentPage;
        if (currentPage != null) await AnimatedPage.SlideOutAsync(currentPage);
        await Shell.Current.GoToAsync("..", animate:false);
    }
}