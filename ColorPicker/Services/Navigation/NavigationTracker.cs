namespace ColorPicker.Services.Navigation;

public enum NavigationDirection { Forward, Backward, Tab }

public static class NavigationTracker
{
    public static NavigationDirection LastDirection { get; private set; }
        = NavigationDirection.Forward;
 
    public static void Initialize()
    {
        Shell.Current.Navigating += (_, e) =>
        {
            LastDirection = e.Source switch
            {
                ShellNavigationSource.Pop
                    or ShellNavigationSource.PopToRoot
                    => NavigationDirection.Backward,
 
                ShellNavigationSource.ShellItemChanged
                    or ShellNavigationSource.ShellSectionChanged
                    => NavigationDirection.Tab,
 
                _ => NavigationDirection.Forward
            };
        };
    }
}