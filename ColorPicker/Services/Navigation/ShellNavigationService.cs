using System.Text;

namespace ColorPicker.Services.Navigation;

public static class ShellNavigationService
{
    public static void SwitchPage(string pageRoute) => Shell.Current.GoToAsync( $"//{pageRoute}");
    
    public static void SwitchSubPage(string pageRoute, Dictionary<string, object>? routeParams=null)
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

    public static void StepBack() => Shell.Current.GoToAsync("..");
    
    public static async Task StepBackAsync() => await Shell.Current.GoToAsync("..");
    
    public static async Task SwitchPageAsync(string pageRoute) => await Shell.Current.GoToAsync( $"//{pageRoute}");
    
    public static async Task SwitchSubPageAsync(string pageRoute, Dictionary<string, object>? routeParams=null)
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
}