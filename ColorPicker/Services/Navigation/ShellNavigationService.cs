using System.Text;

namespace ColorPicker.Services.Navigation;

public static class ShellNavigationService
{
    public static void GoToPage(string pageRoute) => Shell.Current.GoToAsync( $"//{pageRoute}");
    
    public static void DoToSubPage(string pageRoute, Dictionary<string, object>? routeParams=null)
    {
        var url = new StringBuilder();
        if (routeParams is { Count: > 0 })
        {
            foreach (var kvp in routeParams)
            {
                url.Append(url.Length <= 0 ? '?' : '&'); // First append '?', then only '&'
                url.Append(kvp.Key).Append('=').Append(kvp.Value);
            }
        }
        
        Shell.Current.GoToAsync(pageRoute + url);
    }

    public static void GoBack() => Shell.Current.GoToAsync("..");
    
    public static async Task GoBackAsync() => await Shell.Current.GoToAsync("..");
    
    public static async Task DoToPageAsync(string pageRoute) => await Shell.Current.GoToAsync( $"//{pageRoute}");
    
    public static async Task GoToSubPageAsync(string pageRoute, Dictionary<string, object>? routeParams=null)
    {
        var url = new StringBuilder();
        if (routeParams is { Count: > 0 })
        {
            foreach (var kvp in routeParams)
            {
                url.Append(url.Length <= 0 ? '?' : '&'); // First append '?', then only '&'
                url.Append(kvp.Key).Append('=').Append(kvp.Value);
            }
        }
        
        await Shell.Current.GoToAsync(pageRoute + url);
    }

    public static async Task GoToPageWithParamsAsync(string pageRoute, Dictionary<string, object> routeParams) =>
        await Shell.Current.GoToAsync(pageRoute, routeParams);
}